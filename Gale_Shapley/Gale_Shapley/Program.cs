#define DEBUG


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Gale_Shapley
{
    class Program
    {
        const int single = -1;
        static void Main(string[] args)
        {
            int n;
#if DEBUG
            n = 4;
#elif !DEBUG
            n = int.Parse(Console.ReadLine());
#endif
            //n = 4;
            int[,] manlist = new int[n, n];
            int[,] womanlist = new int[n, n];
#if DEBUG
            manlist = new int[,] { { 0, 1, 2, 3 }, { 0, 1, 2, 3 }, { 0, 1, 2, 3 }, { 0, 1, 2, 3 } };
            womanlist = new int[,] { { 0, 1, 2, 3 }, { 0, 1, 2, 3 }, { 0, 1, 2, 3 }, { 0, 1, 2, 3 } };
#endif
            int[] temp = new int[n];
            int[] temp2 = new int[n];
            int[] man = new int[n];
            int[] woman = new int[n];
            for (int i = 0; i < n; i++)
            {
                temp2[i] = i;
                man[i] = single;
                woman[i] = single;
            }
#if !DEBUG
            for (int g = 0; g < n; g++)
            {
                temp = temp2.OrderBy(i => Guid.NewGuid()).ToArray();
                for (int h = 0; h < n; h++)
                {
                    manlist[g, h] = temp[h];
                }
                temp = temp2.OrderBy(i => Guid.NewGuid()).ToArray();
                for (int h = 0; h < n; h++)
                {
                    womanlist[g, h] = temp[h];
                }
            }
#endif
            // finish initialize
            int t = 0;
            while (true)
            {
            TOP:
                if (man[t] != single)//男性が既婚
                {
                    t++;
                    t = t % n;
                    continue;
                }
                else//男性が未婚
                {
                    for (int j = 0; j < n; j++)//リストを走査する
                    {
                        if (woman[manlist[t, j]] == single) //女性が独身の時
                        {
                            woman[manlist[t, j]] = t;//結婚
                            man[t] = manlist[t, j];
                            break;
                        }
                        else　//女性が既婚の時
                        {
                            int BefMan = woman[manlist[t, j]];
                            for (int k = 0; k < n; k++)
                            {
                                if (womanlist[manlist[t, j], k] == BefMan)//今の夫の方が優先順位が高い
                                {
                                    break;//次の女性に
                                }
                                else if (womanlist[manlist[t, j], k] == t)//彼氏の方が優先順位が高い
                                {
                                    woman[manlist[t, j]] = t;//離婚して結婚
                                    man[BefMan] = single;
                                    man[t] = manlist[t, j];
                                    t = 0;
                                    goto JUMP;
                                }
                            }
                        }
                    }
                }
            JUMP:
#if DEBUG
                Console.WriteLine("man is");
                foreach (var item in man)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("\n woman is");
                foreach (var item in woman)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("");
#endif
                //終了条件　馬鹿
                for (int j = 0; j < n; j++)
                {
                    if (man[j] == -1 || woman[j] == -1)
                    {
                        t++;
                        t = t % n;
                        goto TOP;
                    }
                }
                break;
            }
            Console.WriteLine("man is");
            foreach (var item in man)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n woman is");
            foreach (var item in woman)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("-finish---");
            Console.WriteLine("Initial State is");
            Console.WriteLine("Man :");
            for (int i = 0; i < n; i++)
            {
                Console.Write("\n man " + i + ":");
                for (int j = 0; j < n; j++)
                {
                    Console.Write(manlist[i, j] + " ");
                }
            }
            for (int i = 0; i < n; i++)
            {
                Console.Write("\n woman " + i + ":");
                for (int j = 0; j < n; j++)
                {
                    Console.Write(womanlist[i, j] + " ");
                }
            }
            Console.ReadLine();
        }

    }
}
