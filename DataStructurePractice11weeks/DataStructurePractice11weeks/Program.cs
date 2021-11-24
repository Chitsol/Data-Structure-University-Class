using System;
using System.Collections.Generic; // Dictionary
using System.Collections.Concurrent; // ConcurrentDictionary
using System.Threading.Tasks; // Task
using System.Threading;

namespace DataStructurePractice11weeks
{
    class Program
    {
        static void Main(string[] args)
        {
            { /*
            var dict = new ConcurrentDictionary<int, string>();

            Task t1 = Task.Factory.StartNew(() =>
            {
                int key = 1;
                while (key <= 100)
                {
                    if (dict.TryAdd(key, "D" + key))
                    {
                        key++;
                    }

                    Thread.Sleep(100);
                }
            });

            Task t2 = Task.Factory.StartNew(() =>
            {
                int key = 1;
                string val;
                while (key <= 100)
                {
                    if (dict.TryGetValue(key, out val))
                    {
                        Console.WriteLine($"{key}:{val}");
                        key++;
                    }

                    Thread.Sleep(100);
                }
            });

            Task.WaitAll(t1, t2); 
            */
            } //HashTable 
            {/*
                //var dict = new Dictionary<string, int>();
                var dict = new ConcurrentDictionary<string, int>();

                dict.TryAdd("James", 25);
                dict.TryAdd("Tom", 35);
                dict.TryAdd("Jane", 46);

                bool updated = dict.TryUpdate("Jane", 47, 46);

                int age;
                bool exists = dict.TryGetValue("jane", out age);
                if (exists)
                {
                    Console.WriteLine(age);
                }
                //삭제
                bool deleted = dict.TryRemove("Tom", out age);

                foreach (var kv in dict)
                {
                    Console.WriteLine($"{kv.Key}: {kv.Value}");
                }*/
                //if (dict.ContainsKey("Jane"))
                //{
                //    int age = dict["Jane"];
                //    Console.WriteLine(age);
                //}

                //dict.Remove("Tom");

                //foreach (KeyValuePair<string, int> kv in dict)
                //{
                //    Console.WriteLine($"{kv.Key}: {kv.Value}");
                //}


                HashTable ht = new HashTable(4);

                ht.Add("james", "425-423-2323");
                ht.Add("tom", "425-323-1336");
                ht.Add("jane", "425-733-9853");
                ht.Add("sam", "425-834-4357");
                ht.Add("kate", "425-212-3757");
                ht.Add("ted", "425-744-5557");

                ht.DebugPrintBuckets();
                Console.WriteLine();

                object val = ht.Get("jane");
                Console.WriteLine(val);

                if (ht.Contains("samuel"))
                {
                    Console.WriteLine("samuel: found");
                }
                else
                {
                    Console.WriteLine("samuel: not found");
                }
            }
            {/*
                Graph<int> g = new Graph<int>();
                var n1 = g.AddNode(10);
                var n2 = g.AddNode(20);
                var n3 = g.AddNode(30);
                var n4 = g.AddNode(40);
                var n5 = g.AddNode(50);

                g.AddEdge(n1, n3);
                g.AddEdge(n2, n4);
                g.AddEdge(n3, n4);
                g.AddEdge(n3, n5);

                g.DebugPrintLinks();
            */
            } //Graph
        }
    }

    public class Node
    {
        public object Key { get; set; }
        public object Value { get; set; }
        public Node Next { get; set; }
        public Node(object key, object value)
        {
            this.Key = key;
            this.Value = value;
            this.Next = null;

        }
    }

    public class HashTable
    {
        //Bucket 배열
        private Node[] buckets;
        private int size;

        public HashTable(int size = 32)
        {
            this.buckets = new Node[size];
            this.size = size;
        }

        //Key/value entry add
        public void Add(object key, object value)
        {
            //hash method bucket indes print
            int index = HashFunction(key);

            if (buckets[index] == null)
            {
                buckets[index] = new Node(key, value);
            }
            else
            {
                //linked list front add
                Node node = new Node(key, value);
                node.Next = buckets[index];
                buckets[index] = node;
            }
        }

        public object Get(object key)
        {
            int index = HashFunction(key);

            Node node = buckets[index];
            while (node != null)
            {
                if (node.Key == key)
                {
                    return node.Value;
                }
                node = node.Next;
            }

            throw new ApplicationException("Not found");
        }

        public bool Contains(object key)
        {
            int index = HashFunction(key);

            Node node = buckets[index];
            while (node != null)
            {
                if (node.Key == key)
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        //hash function
        private int HashFunction(object key)
        {
            int h = Math.Abs(key.GetHashCode());

            int hash = h & size;
            hash += (h >> 8) & 0xff; 
            hash += (h >> 16) & 0xff; 
            hash += (h >> 24) & 0xff; 

            return hash % size;
        }

        internal void DebugPrintBuckets()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                Console.Write($"{i}:");

                Node node = buckets[i];
                while (node != null)
                {
                    Console.Write($"{node.Key} -> Length");
                    node = node.Next;
                }

                Console.WriteLine();
            }
        }
    }

    public class GraphNode<T>
    {
        private List<GraphNode<T>> _neighbors;
        private List<int> _weights;

        public T Data { get; set; }
        public GraphNode(T data)
        {
            Data = data;
        }
        public List<GraphNode<T>> Neighbors
        {
            get
            {
                _neighbors = _neighbors ?? new List<GraphNode<T>>();
                return _neighbors;
            }
        }
        public List<int> Weights
        {
            get
            {
                _weights ??= new List<int>();
                return _weights;
            }
        }
    }

    public class Graph<T>
    {
        private List<GraphNode<T>> _nodeList;
        public Graph()
        {
            _nodeList = new List<GraphNode<T>>();
        }
        public GraphNode<T> AddNode(T data)
        {
            GraphNode<T> n = new GraphNode<T>(data);
            _nodeList.Add(n);
            return n;
        }
        public GraphNode<T> AddNode(GraphNode<T> node)
        {
            _nodeList.Add(node);
            return node;
        }
        public void AddEdge(GraphNode<T> from, GraphNode<T> to, bool oneway = true, int weight = 0)
        {
            from.Neighbors.Add(to);
            from.Weights.Add(weight);

            if (!oneway)
            {
                to.Neighbors.Add(from);
                to.Weights.Add(weight);
            }
        }
        internal void DebugPrintLinks()
        {
            foreach (GraphNode<T> graphNode in _nodeList)
            {
                foreach (var n in graphNode.Neighbors)
                {
                    string s = graphNode.Data + "-" + n.Data;
                    Console.WriteLine(s);
                }
            }
        }
    }
}
