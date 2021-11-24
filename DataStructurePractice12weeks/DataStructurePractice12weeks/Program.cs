using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace DataStructurePractice12weeks
{
    class Program
    {
        static void Main(string[] args)
        {
            {/*
                var gr = new Graph<string>();
                var seoul = gr.AddVertex("서울");
                var daejun = gr.AddVertex("대전");
                var daeku = gr.AddVertex("대구");
                var pusan = gr.AddVertex("부산");
                var kangrung = gr.AddVertex("강릉");

                gr.AddEdge(seoul, daejun, 6);
                gr.AddEdge(seoul, daeku, 7);
                gr.AddEdge(seoul, kangrung, 10);
                gr.AddEdge(daejun, pusan, 7);
                gr.AddEdge(daeku, pusan, 3);
                gr.AddEdge(kangrung, daeku, 4);

                gr.DebugPrintGraph();
            */
            }//Node0 Test
            {/*
                var gr = new Graph1();
                gr.AddVertex("A");
                gr.AddVertex("B");
                gr.AddVertex("C");
                gr.AddVertex("D");
                gr.AddEdge("A", "B", 5);
                gr.AddEdge("A", "D", 9);
                gr.AddEdge("B", "D", 6);
                gr.AddEdge("D", "C", 7);

                gr.DebugPrintGraph();*/
            }//Node1 Test
            {/*
                var gr = new Graph2();
                gr.AddVertex("A");
                gr.AddVertex("B");
                gr.AddVertex("C");
                gr.AddVertex("D");
                gr.AddEdge("A", "B", 5);
                gr.AddEdge("A", "D", 9);
                gr.AddEdge("B", "D", 6);
                gr.AddEdge("D", "C", 7);

                gr.DebugPrintGraph();
            */
            }//Node2 Test
            string[] vertices = { "A", "A", "B", "C", "D" };

            AdjacencyGraph gr = new AdjacencyGraph(vertices);
            gr.AddEdge("A", "B");
            gr.AddEdge("B", "D");
            gr.AddEdge("A", "D");
            gr.AddEdge("C", "D");

            gr.DebugPrintGraph();
        }


        public class Node0<T>
        {
            public T Data { get; set; }
            public List<Node0<T>> Neighbors { get; private set; }
            public List<int> Weights { get; private set; } //Optional
            public Node0()
            {
                Neighbors = new List<Node0<T>>();
                Weights = new List<int>();
            }

            public Node0(T data) : this()
            {
                this.Data = data;
            }
        }
        public class Graph<T>
        {
            private List<Node0<T>> nodes;
            private bool directedGraph;

            public Graph(bool directedGraph = false)
            {
                this.nodes = new List<Node0<T>>();
                this.directedGraph = directedGraph;
            }
            public Node0<T> AddVertex(T data)
            {
                return AddVertex(new Node0<T>(data));
            }
            public Node0<T> AddVertex(Node0<T> node)
            {
                nodes.Add(node);
                return node;
            }
            public void AddEdge(Node0<T> from, Node0<T> to, int weight = 1)
            {
                from.Neighbors.Add(to);
                from.Weights.Add(weight);
                if (!directedGraph)
                {
                    to.Neighbors.Add(from);
                    to.Weights.Add(weight);
                }
            }
            internal void DebugPrintGraph()
            {
                foreach (var vertex in nodes)
                {
                    int cnt = vertex.Neighbors.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        Console.WriteLine("{0}-- ({1}) --{2}", vertex.Data, vertex.Weights[i], vertex.Neighbors[i].Data);
                    }
                }
            }
        }//Node
        public class Node1
        {
            public string Key { get; set; }
            public int Weight { get; set; }
            public Node1(string key, int weight = 1)
            {
                Key = key;
                Weight = weight;
            }
        }
        public class Graph1
        {
            private Dictionary<string, List<Node1>> nodes = new Dictionary<string, List<Node1>>();

            public void AddVertex(string key)
            {
                if (!nodes.ContainsKey(key))
                {
                    var edgeList = new List<Node1>();
                    //Add empty edge list
                    nodes.Add(key, edgeList);
                }
            }

            public void AddEdge(string from, string to, int weight = 1)
            {
                var edgeList = nodes[from];
                if (edgeList == null)
                {
                    throw new ApplicationException("Not found");
                }
                edgeList.Add(new Node1(to, weight));
            }

            internal void DebugPrintGraph()
            {
                foreach (var kv in nodes)
                {
                    //시작 정점 키
                    string from = kv.Key;

                    //kv.Value: 시작정점과 연관된 간선들 리스트
                    foreach (var edge in kv.Value)
                    {
                        Console.WriteLine($"{from}--({edge.Weight})--{edge.Key}");
                    }
                }
            }
        }
        public class Node2
        {
            public string Key { get; }
            public LinkedList<Edge> EdgeList { get; }
            public Node2(string key)
            {
                this.Key = key;
                this.EdgeList = new LinkedList<Edge>();
            }
        }
        public class Edge
        {
            public string From { get; } // OPtional
            public string To { get; }
            public int Weight { get; set; }
            public Edge(string from, string to, int weight = 1)
            {
                this.From = from;
                this.To = to;
                this.Weight = weight;
            }
        }
        public class Graph2
        {
            private List<Node2> nodes = new List<Node2>();

            public Node2 AddVertex(string key)
            {
                var node = new Node2(key);
                nodes.Add(node);
                return node;
            }

            public void AddEdge(string from, string to, int weight = 1)
            {
                var fromVertex = nodes.Find(s => s.Key == from);

                var edge = new Edge(from, to, weight);
                fromVertex.EdgeList.AddFirst(edge);
            }
            internal void DebugPrintGraph()
            {
                foreach (var vertex in nodes)
                {
                    string from = vertex.Key;

                    foreach (var edge in vertex.EdgeList)
                    {
                        Console.WriteLine($"{from}--({edge.Weight})--{edge.To}");
                    }
                }
            }
        }
        public class AdjacencyGraph
        {
            //Adhacency Matrix 2space array
            private int[,] mat;
            //정점 레이블 배열
            private List<string> vertexList;

            private int size;
            private bool digraph;

            //인접행렬 초기화
            public AdjacencyGraph(string[] vertexLabels, bool digraph = false)
            {
                this.vertexList = new List<string>(vertexLabels);
                this.size = vertexList.Count;
                this.mat = new int[size, size];
                this.digraph = digraph;
            }

            //Add Edge
            public void AddEdge(string from, string to, int weight = 1)
            {
                int iFrom = vertexList.FindIndex(s => s == from);
                Console.WriteLine("FindIndex s => s == from : " + vertexList.FindIndex(s => s == from));
                int iTo = vertexList.FindIndex(s => s == to);
                Console.WriteLine("FindIndex s => s == to : " + vertexList.FindIndex(s => s == to));
                AddEdge(iFrom, iTo, weight);
            }

            public void AddEdge(int fromIndex, int toIndex, int weight = 1)
            {
                mat[fromIndex, toIndex] = weight;
                if (!digraph)
                {
                    mat[toIndex, fromIndex] = weight;
                }
            }

            public void RemoveEdge(string from, string to)
            {
                int iFrom = vertexList.FindIndex(s => s == from);
                int iTo = vertexList.FindIndex(s => s == to);
                RemoveEdge(iFrom, iTo);
            }

            public void RemoveEdge(int fromIndex, int toIndex)
            {
                mat[fromIndex, toIndex] = 0;
                if (!digraph)
                {
                    mat[toIndex, fromIndex] = 0;
                }
            }

            internal void DebugPrintGraph()
            {
                //Matrix 상단
                Console.Write(" ");
                for (int i = 0; i < size; i++)
                {
                    Console.Write($"{vertexList[i]}");
                }
                Console.WriteLine();


                //Matrix Lines
                for (int i = 0; i < size; i++)
                {
                    Console.Write($"{vertexList[i]}");
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write($"{mat[i, j]}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
