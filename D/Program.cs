using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace D
{
    class Program
    {
        static int numEnter(string row)
        {
            int res = 0;
            bool next = false;
            do
            {
                Console.WriteLine($"Введите {row}");
                next = int.TryParse(Console.ReadLine(), out res);
            } while (!next);
            return res;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                List<int[]> lines = new List<int[]>();
                int n = numEnter("количество задач");
                int k = numEnter("количество работников");
                Console.WriteLine("Ввод рёбер в формате [вершина1] [вершина2] \n Для окончания ввода введите 'всё'");
                while (true)
                {
                    try
                    {

                        string str = Console.ReadLine();
                        if (str.ToLower() == "всё" || str.ToLower() == "все")
                        {
                            break;
                        }
                        else
                        {
                            int[] arr = new int[2];
                            arr = Array.ConvertAll<string, int>(str.Split(' '), int.Parse);
                            if (arr.Length != 2)
                            {
                                Console.WriteLine("ERROR");
                                continue;
                            }
                            else
                            {
                                lines.Add(arr);
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("ERROR");

                    }
                }
                Dictionary<int, int> order = new Dictionary<int, int>();
                List<int[]> save = new List<int[]>(lines);

                for (int orderNum = 1; orderNum <= n;)
                {
                    List<int> lui = LevelUnInclude(lines);
                    int ord = orderNum;
                    for (int i = 0; i < lui.Count; i++)
                    {
                        for (int j = 0; j < lines.Count; j++)
                        {
                            if (lines[j][1] == lui[i])
                            {
                                if (!order.Values.Contains(lines[j][1]))
                                {
                                    order.Add(orderNum, lines[j][1]);
                                    orderNum++;
                                }
                                break;
                            }
                        }
                    }
                    for (; ord <= orderNum; ord++)
                    {
                        for (int j = 0; j < lines.Count; j++)
                        {
                            if (lines[j][1] == order[ord])
                            {
                                order.Add(orderNum, lines[j][0]);
                                orderNum++;
                                lines.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                }
                lines = new List<int[]>(save);
                List<List<int>> gantDiogram = new List<List<int>>();
                for (int empl = 1; empl <= k; empl++)
                {
                    gantDiogram.Add(new List<int>());
                }
                int worker = 0;
                List<int> key = order.Keys.ToList();
                List<int> works = new List<int>();
                List<int> id = new List<int>();
                List<int> ids = new List<int>();
                List<int> ids2 = new List<int>();
                for (int i = key.Count - 1; i >= 0; i--)
                {

                    if (worker == 0)
                    {
                        works = CanAddWorks(lines, out id);
                        if (works.Count == 0)
                        {
                            for (int j = 0; j < key.Count; j++)
                            {
                                int index = findLastetsIndex(save, order[key[j]], gantDiogram);
                                if (index != gantDiogram[0].Count && gantDiogram.Count>1) {
                                    for (int ij = index; ij < gantDiogram[0].Count; ij++)
                                    {
                                        bool br = false;
                                        for (int ji = 0; ji < gantDiogram.Count; ji++)
                                        {
                                            if (gantDiogram[ji][ij] == -1)
                                            {
                                                gantDiogram[ji][ij] = order[key[j]];
                                                br = true;
                                                break;
                                            }
                                        }
                                        if (br)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    List<int> list = new List<int>();
                                    gantDiogram[0].Add(order[key[j]]);
                                    for (int h = 1; h < gantDiogram.Count; h++)
                                    {
                                        gantDiogram[h].Add(-1);
                                    }
                                }
                            }
                            break;
                        }
                    }

                    if (works.Contains(order[key[i]]))
                    {
                        gantDiogram[worker].Add(order[key[i]]);
                        ids.Add(id[works.IndexOf(order[key[i]])]);
                        ids2.Add(i);
                        if (worker == works.Count - 1 && worker != k - 1)
                        {
                            while (worker != k - 1)
                            {
                                worker++;
                                gantDiogram[worker].Add(-1);

                            }
                            worker++;
                        }
                        else
                        {
                            worker++;
                        }
                    }
                    else if (i == 0 && (key.Count - 1) > 0)
                    {
                        i = key.Count;
                    }
                    if (worker == k)
                    {
                        ids.Sort();
                        ids2.Sort();
                        ids.Reverse();
                        ids2.Reverse();
                        foreach (int t in ids)
                        {
                            lines.RemoveAt(t);
                        }
                        foreach (int t in ids2)
                        {
                            key.RemoveAt(t);
                        }
                        ids = new List<int>();
                        ids2 = new List<int>();
                        worker = 0;
                        i = key.Count;
                    }

                }
                worker = 1;
                foreach (List<int> elem in gantDiogram)
                {
                    Console.Write($"Работник #{worker}:");
                    foreach (int num in elem)
                    {
                        if (num == -1)
                            Console.Write(" -");
                        else
                            Console.Write(" " + num);
                    }
                    worker++;
                    Console.WriteLine();
                }
            }
        }
        static List<int> uninclude(List<int> mass1, List<int> mass2) 
        {
            for(int i = 0; i < mass1.Count;i++)
            {
                if (mass2.Contains(mass1[i]))
                {

                }
                else
                {
                    mass1.RemoveAt(i);
                }
            }
            return mass1;
        }
        static int findLastetsIndex(List<int[]> lines, int num , List<List<int>> findin) 
        {
            List<int> numbers = new List<int>(); 
            for (int i = 0;i< lines.Count;i++)
            {
                if(lines[i][1] == num)
                {
                    numbers.Add(lines[i][0]);
                } 
            }
            int ret = 0;
            for(int i = findin[0].Count-1; i >= 0; i--)
            {
                for (int j = findin.Count-1; j >= 0; j--)
                {
                    if (numbers.Contains(findin[j][i]))
                    {
                        ret = i+1;
                        return ret;
                    }
                }
            }
            return ret;
        } 
        static List<int> LevelUnInclude(List<int[]> lines)
        {
            List<int> p = new List<int>();
            for (int i = 0; i < lines.Count; i++)
            {
                p.Add(lines[i][1]);
                int count = 0;
                for (int j = 0; j < lines.Count; j++)
                    if (p.Contains(lines[j][0]))
                        count++;
                if (count >= 1)
                {
                    p.Remove(lines[i][1]);
                }
            }
            p = p.GroupBy(x => x).Select(x => x.First()).ToList();
            return p;
        }
        static List<int> CanAddWorks(List<int[]> lines, out List<int> id )
        {
            List<int> p = new List<int>();
            id = new List<int>();
            for (int i = lines.Count-1; i >= 0 ; i--)
            {
                p.Add(lines[i][0]);
                id.Add(i);
                int count = 0;
                for (int j = 0; j < lines.Count; j++)
                    if (p.Contains(lines[j][1]))
                        count++;
                if (count >= 1)
                {
                    p.Remove(lines[i][0]);
                    id.Remove(i);
                }
            }
            p = p.GroupBy(x => x).Select(x => x.First()).ToList();
            return p;
        }
    }
}

