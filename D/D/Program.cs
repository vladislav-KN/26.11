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
                List<List<int[]>> graphs = new List<List<int[]>>();
                while (lines.Count > 0)
                {
                    List<int[]> graph = new List<int[]>();
                    List<int> PickInTheGraph = new List<int>();
                    for (int i = 0; i< lines.Count; i++) //выделяем деревья
                    { 
                        if (PickInTheGraph.Count == 0)
                        {
                            PickInTheGraph.Add(lines[i][1]);
                            PickInTheGraph.Add(lines[i][0]);
                            graph.Add(lines[i]);
                            lines.RemoveAt(i);
                            i--;
                        }
                        else if(PickInTheGraph.Contains(lines[i][0]))
                        {
                            PickInTheGraph.Add(lines[i][1]);
                            graph.Add(lines[i]);
                            lines.RemoveAt(i);
                            i--;
                        }
                        else if(PickInTheGraph.Contains(lines[i][1]))
                        {
                            PickInTheGraph.Add(lines[i][0]);
                            graph.Add(lines[i]);
                            lines.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            continue;
                        }
                
                    }
                    graphs.Add(graph);
                }
                foreach(List<int[]> graph in graphs)//упорядочим граф(ы)
                {
                    List<int[]> grUnSorted = new List<int[]>(graph);
                    List<int[]> grSorted = new List<int[]>();
                    while(grSorted.Count != graph.Count)
                    {
                        List<int> PickInTheGraph = new List<int>();
                        for(int i = 0; i< grUnSorted.Count;i++)
                        {
                            if (PickInTheGraph.Count == 0)
                            {
                                PickInTheGraph.Add(grUnSorted[i][0]);
                                
                            }
                            
                        }
                    }
                }

            }
        }
    }
}

