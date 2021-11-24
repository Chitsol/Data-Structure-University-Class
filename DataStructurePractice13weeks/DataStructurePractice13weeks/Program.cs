using System;
using System.Collections.Generic;

namespace DataStructurePractice13weeks
{
    class Program
    {
        static void Main(string[] args)
        {
            var gr = new Graph<string>();
            var A = gr.AddVertex("A");
            var B = gr.AddVertex("B");
            var C = gr.AddVertex("C");
            var D = gr.AddVertex("D");
            var E = gr.AddVertex("E");
            var F = gr.AddVertex("F");
            var G = gr.AddVertex("G");
            // 비연결 그래프 -- 부분 X-Y
            var X = gr.AddVertex("X");
            var Y = gr.AddVertex("Y");

            gr.AddEdge(A, B);
            gr.AddEdge(A, D);
            gr.AddEdge(A, E);
            gr.AddEdge(B, C);
            gr.AddEdge(E, F);
            gr.AddEdge(E, G);
            gr.AddEdge(F, G);
            gr.AddEdge(X, Y);

            gr.DFS();
            gr.DFSIteraive();
            gr.BFS();

            Console.WriteLine("----------------------");
            string[] vertices = { "A", "B", "C", "D", "E", "F" };
            var g = new GraphV2(vertices);
            g.AddEdge("A", "B", 2);
            g.AddEdge("A", "C", 3);
            g.AddEdge("B", "C", 5);
            g.AddEdge("B", "D", 3);
            g.AddEdge("B", "E", 4);
            g.AddEdge("C", "E", 4);
            g.AddEdge("D", "E", 2);
            g.AddEdge("D", "F", 3);
            g.AddEdge("E", "F", 5);

            var mst = g.KruskalMST();
            foreach (var m in mst)
            {
                Console.WriteLine($"{m.From}-{m.To} {m.Weight}");
            }
        }
    }

    public class Node<T>
    {
        public T Data { get; set; }
        public List<Node<T>> Neighbors { get; private set; }
        public List<int> Weights { get; private set; } //Optional
        public Node()
        {
            Neighbors = new List<Node<T>>();
            Weights = new List<int>();
        }

        public Node(T data) : this()
        {
            this.Data = data;
        }
    }
    public class Graph<T>
    {
        private List<Node<T>> nodes = new List<Node<T>>();
        private bool directedGraph;
        // -- 중복 코드 생략 -- 
        // AddVertex
        // AddEdge
        public Graph(bool directedGraph = false)
        {
            this.nodes = new List<Node<T>>();
            this.directedGraph = directedGraph;
        }
        public Node<T> AddVertex(T data)
        {
            return AddVertex(new Node<T>(data));
        }
        public Node<T> AddVertex(Node<T> node)
        {
            nodes.Add(node);
            return node;
        }
        public void AddEdge(Node<T> from, Node<T> to, int weight = 1)
        {
            from.Neighbors.Add(to);
            from.Weights.Add(weight);
            if (!directedGraph)
            {
                to.Neighbors.Add(from);
                to.Weights.Add(weight);
            }
        }

        public void DFS()
        {
            //방문 여부를 표시하는 방문 테이블
            var visited = new HashSet<Node<T>>();

            //For Disconnected Graph 
            //방문하지 않은 노듣르 모두 체크
            foreach (var node in nodes)
            {
                if (!visited.Contains(node))
                {
                    DFSRescursive(node, visited);
                    Console.WriteLine();
                }
            }

        }

        private void DFSRescursive(Node<T> node, HashSet<Node<T>> visited)
        {
            //Node visit
            Console.Write("{0}", node.Data);
            visited.Add(node);

            foreach (var adjNode in node.Neighbors)
            {
                if (!visited.Contains(adjNode))
                {
                    //재귀호출
                    DFSRescursive(adjNode, visited);
                }
            }
        }

        public void DFSIteraive()
        {
            var visited = new HashSet<Node<T>>();

            //For Disconnected Graph
            //방문하지 않은 노드들 모두 체크
            foreach (var node in nodes)
            {
                if (!visited.Contains(node))
                {
                    DFSUsingStack(node, visited);
                }
            }
        }

        private void DFSUsingStack(Node<T> node, HashSet<Node<T>> visited)
        {
            var stack = new Stack<Node<T>>();
            stack.Push(node);

            while (stack.Count > 0)
            {
                var vertex = stack.Pop();
                if (!visited.Contains(vertex))
                {
                    Console.Write("{0}", vertex.Data);
                    visited.Add(vertex);
                }

                //표현 A
                foreach (var adjNode in vertex.Neighbors)
                {
                    if (!visited.Contains(adjNode))
                    {
                        stack.Push(adjNode);
                    }
                }
                //표현(B)
                //int cnt = vertex.Neighbors.Count;
                //for(int i = cnt -1; i>= 0; i--)
                //{
                //if(!visited.Contains(vertex.Neighbors[i]))
                // {
                //  stack.Push(vertex.Neighbors[i]);
                // }
                //}
            }
            Console.WriteLine();
        }

        /* DFSIterative 실행결과:
         * A E G F D B C
         * X Y
         */
        public void BFS()
        {
            var visited = new HashSet<Node<T>>();

            //For Disconnected Graph
            //방문하지 않은 노드를 모두 체크

            foreach (var node in nodes)
            {
                if (!visited.Contains(node))
                {
                    BFS(node, visited);
                }
            }
        }

        private void BFS(Node<T> node, HashSet<Node<T>> visited)
        {
            var q = new Queue<Node<T>>();
            q.Enqueue(node);

            while (q.Count > 0)
            {
                var vertex = q.Dequeue();
                if (!visited.Contains(vertex))
                {
                    Console.Write("{0}", vertex.Data);
                    visited.Add(vertex);
                }

                foreach (var adjNode in vertex.Neighbors)
                {
                    if (!visited.Contains(adjNode))
                    {
                        //Add Queue
                        q.Enqueue(adjNode);
                    }
                }
            }
        }
        /*BFS 실행결과:
         * A B D E C F G X Y
         */
        private void TopoSort(Node<T> node, HashSet<Node<T>> visited, Stack<Node<T>> result)
        {
            foreach (var nbr in node.Neighbors)
            {
                if (!visited.Contains(nbr))
                {
                    TopoSort(nbr, visited, result);
                }
            }
            //(A) 스택에 저장 저장하는 경우
            result.Push(node);
            //(B) 연결리스트에 저장하는 경우
            //result.AddFirst(node);
            visited.Add(node);
        }
        public Stack<Node<T>> TopologicalSort()
        {
            var visted = new HashSet<Node<T>>();
            //(A)스택에 저장하는 경우
            var result = new Stack<Node<T>>();
            //(B) 연결리스트에 저장하는 경우
            //var result = new LinkedList <Node<T>>();

            //All Nodes
            //not visit and 위상정력 수정
            foreach (var vertex in nodes)
            {
                if (!visted.Contains(vertex))
                {
                    TopoSort(vertex, visted, result);
                }
            }
            return result;
        }
    }
    public class DisjointSet
    {
        //HashTable
        private Dictionary<string, string> ht;

        public DisjointSet()
        {
            ht = new Dictionary<string, string>();
        }

        public void CreateSet(string element)
        {
            //부모가 요소와 동일
            ht.Add(element, element);
        }

        public string Find(string element)
        {
            if (ht[element] == element)
            {
                //부모가 요소와 동일하면 최상위 부모
                return element;
            }
            else
            {
                //재귀호출로 최상위 부모 찾음
                return Find(ht[element]);
            }
        }

        public void Union(string elem1, string elem2)
        {
            //병합: elem1의 부모를 elem2로 지정
            ht[elem1] = elem2;
        }
    }
    public class GraphV2
    {
        public class Edge
        {
            public string From { get; }
            public string To { get; }
            public int Weight { get; }
            public Edge(string from, string to, int weight)
            {
                this.From = from;
                this.To = to;
                this.Weight = weight;
            }
        }

        // G = (V, E)
        private readonly List<string> vertices;
        private readonly List<Edge> edges;
        private bool directedGraph = false;

        public GraphV2(IEnumerable<string> vertices, bool directedGraph = false)
        {
            this.vertices = new List<string>(vertices);
            this.edges = new List<Edge>();
            this.directedGraph = directedGraph;
        }

        public void AddEdge(string from, string to, int weight)
        {
            var edge = new Edge(from, to, weight);
            edges.Add(edge);
            if (!directedGraph)
            {
                edges.Add(new Edge(to, from, weight));
            }
        }

        public List<Edge> KruskalMST()
        {
            //결과 집합
            var mst = new List<Edge>();

            //정점수만큼 분리집합 생성
            DisjointSet djset = new DisjointSet();
            foreach (var vtx in vertices)
            {
                djset.CreateSet(vtx);
            }

            //가중치 오름차순으로 Edge 정력
            edges.Sort((elem1, elem2) => elem1.Weight - elem2.Weight);

            //정렬된 Edge 순으로
            foreach (var edge in edges)
            {
                //시작정점과 목표정점의 부모정점 검색
                var fromParent = djset.Find(edge.From);
                var toParent = djset.Find(edge.To);
                //부모정점이 다르면 즉 분리집합이 다르면
                if (fromParent != toParent)
                {
                    //분리집합병합
                    djset.Union(fromParent, toParent);
                    //결과집합 추가
                    mst.Add(edge);
                }
            }

            //결과집합 리턴
            return mst;
        }
    }

    //프림 알고리즘 최단경로 다익스트라 알고맂므 프로세스 해봐야함

}
