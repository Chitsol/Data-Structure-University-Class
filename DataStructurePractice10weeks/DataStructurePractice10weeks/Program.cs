using System;
using System.Collections.Generic;

namespace DataStructurePractice10weeks
{
    class Program
    {
        static void Main(string[] args)
        {
            int index = int.Parse(Console.ReadLine());

            if (index == 0)
            {
                var heap = new MaxHeap();
                //new heap data ADD
                heap.Add(20);
                heap.Add(15);
                heap.Add(12);
                heap.Add(13);
                heap.Add(10);
                heap.Add(9);
                heap.Add(11);
                heap.Add(7);
                heap.Add(6);

                //print = 20 15 12 13 10 9 11 7 6
                heap.DebugDisplayArray();

                heap.Add(17);

                //print = 20 17 12 13 15 9 11 7 6 10
                heap.DebugDisplayArray();

                //highest data export
                int max = heap.Remove();
                Console.WriteLine(max);

                //print = 17 15 12 13 10 10 9 11 7 6
                heap.DebugDisplayArray();
            }//heap
            else if (index == 1)
            {
                var trie = new SimpleTrie();

                trie.Insert("cat");
                trie.Insert("cam");
                trie.Insert("tea");
                trie.Insert("tee");
                trie.Insert("team");

                bool found = trie.Find("tea");
                Console.WriteLine($"tea: {found}");

                found = trie.Find("teen");
                Console.WriteLine($"teen : {found}");
            }//Simpletrie
            else if (index == 2)
            {
                var trie = new Trie();

                trie.Insert("프로");
                trie.Insert("프로그래밍");
                trie.Insert("프로그램");
                trie.Insert("안녕하신가요");
                trie.Insert("안녕하세요");

                bool found = trie.Find("프로그래밍");
                Console.WriteLine($"프로그래밍:{found}");

                found = trie.Find("안녕");
                Console.WriteLine($"안녕: {found}");
            }//Trie
            else if (index == 3)
            {
                var trie = new Trie();

                trie.Insert("프로");
                trie.Insert("프로그래밍");
                trie.Insert("프로그램");
                trie.Insert("안녕하신가요");
                trie.Insert("안녕하세요");
                trie.Insert("프로");

                var results = trie.AutoComplete("프로");
                foreach (var item in results)
                {
                    Console.WriteLine(item);
                }
            }//Trie AutoComplete
            else if (index == 4)
            {
                var trie = new Trie2();

                trie.Insert("프로");
                trie.Insert("프로그래밍");
                trie.Insert("프로그램");
                trie.Insert("안녕하신가요");
                trie.Insert("안녕하세요");
                trie.Insert("프로");

                var results = trie.AutoComplete("프로");
                foreach (var item in results)
                {
                    Console.WriteLine(item);
                }

                results = trie.AutoComplete("안녕");
                foreach (var item in results)
                {
                    Console.WriteLine(item);
                }
            }//Trie AutoComplete ver2
        }
    }

    public class MaxHeap
    {
        //Daynamic array
        private List<int> arr = new List<int>();

        public void Add(int data)
        {
            arr.Add(data);

            int i = arr.Count - 1;
            //Reheapification upward
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (arr[i] > arr[parent])
                {
                    //Swap
                    int tmp = arr[i];
                    arr[i] = arr[parent];
                    arr[parent] = tmp;

                    //Continue Check
                    i = parent;
                }
                else
                {
                    break;
                }
            }
        }

        public int Remove()
        {
            if (arr.Count == 0)
            {
                throw new ApplicationException();
            }

            //root highest data save
            int data = arr[0];

            //last node -> first node
            arr[0] = arr[arr.Count - 1];
            //last node delete
            arr.RemoveAt(arr.Count - 1);

            //Reheapification downward
            int i = 0;
            int last = arr.Count - 1;
            while (i < last)
            {
                //Left child node
                int child = 2 * i + 1;
                //if Right child node higher than left ndoe, select right node
                if (child < last && arr[child] < arr[child + 1])
                {
                    child++;
                }

                //if index reset or if index >= child node break
                if (child > last || arr[i] >= arr[child])
                {
                    break;
                }

                //Swap
                int tmp = arr[i];
                arr[i] = arr[child];
                arr[child] = tmp;

                //continue check
                i = child;
            }

            return data;
        }

        internal void DebugDisplayArray()
        {
            for (int i = 0; i < arr.Count; i++)
            {
                Console.Write("{0}", arr[i]);
            }
            Console.WriteLine();
        }
    }

    public class SimpleTrie
    {
        private class Node
        {
            public Node[] Children { get; private set; }
            public bool EndOfWord { get; set; }

            public Node()
            {
                //Edit node array in Alphabet number
                Children = new Node[26];
            }
        }

        //Edit root node 
        private Node root = new Node();

        public void Insert(string str)
        {
            string s = str.ToLower();
            Node node = root;

            foreach (char ch in s)
            {
                //문자 위치 구하기
                int index = ch - 'a';

                //not have link, link new node
                if (node.Children[index] == null)
                {
                    node.Children[index] = new Node();
                }

                node = node.Children[index];
            }
            //word end check
            node.EndOfWord = true;
        }

        public bool Find(string str)
        {
            string s = str.ToLower();
            Node node = root;

            foreach (char ch in s)
            {
                int index = ch - 'a';
                if (node.Children[index] == null)
                {
                    return false;
                }

                node = node.Children[index];
            }
            return node != null && node.EndOfWord;
        }
    }

    public class Trie
    {
        private class Node
        {
            //hashtable class use link field
            public Dictionary<char, Node> Children { get; private set; }
            public bool EndOfWord { get; set; }
            public Node()
            {
                Children = new Dictionary<char, Node>();
            }
        }

        //root node field
        private Node root = new Node();

        public void Insert(string str)
        {
            Node node = root;

            foreach (char ch in str)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    node.Children[ch] = new Node();
                }

                node = node.Children[ch];
            }

            node.EndOfWord = true;
        }

        public bool Find(string str)
        {
            Node node = root;

            foreach (char ch in str)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return false;
                }

                node = node.Children[ch];
            }
            return node != null && node.EndOfWord;
        }

        public List<string> AutoComplete(string prefix)
        {
            //Move to the node up to Prefix
            var node = root;
            foreach (var ch in prefix)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return null;
                }
                node = node.Children[ch];
            }

            //Prefix node start front tour
            var results = new List<string>();
            Preorder(node, prefix, results);

            return results;
        }

        private void Preorder(Node node, string nodeStr, List<string> results)
        {
            if (node == null) return;

            //if word end, add in list
            if (node.EndOfWord)
            {
                results.Add(nodeStr);
            }

            //Children들을 Preorder 재귀 호출
            foreach (char key in node.Children.Keys)
            {
                Preorder(node.Children[key], nodeStr + key, results);
            }
        }
    }
    
    public class Trie2 //in Node Add string word variable
    {
        private class Node
        {
            //hashtable class use link field
            public Dictionary<char, Node> Children { get; private set; }
            public bool EndOfWord { get; set; }
            public string Word { get; set; }
            public Node()
            {
                Children = new Dictionary<char, Node>();
            }
        }

        //root node field
        private Node root = new Node();

        public void Insert(string str)
        {
            Node node = root;

            foreach (char ch in str)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    node.Children[ch] = new Node();
                    
                }

                node = node.Children[ch];
            }

            node.Word = str;
        }

        public bool Find(string str)
        {
            Node node = root;

            foreach (char ch in str)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return false;
                }

                node = node.Children[ch];
            }
            return node != null && node.EndOfWord;
        }

        public List<string> AutoComplete(string prefix)
        {
            //Move to the node up to Prefix
            var node = root;
            foreach (var ch in prefix)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return null;
                }
                node = node.Children[ch];
            }

            //Prefix node start front tour
            var results = new List<string>();
            Preorder(node, results);

            return results;
        }

        private void Preorder(Node node, List<string> results)
        {
            if (node == null) return;

            //if word end, add in list
            if (node.Word != null)
            {
                results.Add(node.Word);
            }

            //Children들을 Preorder 재귀 호출
            foreach (char key in node.Children.Keys)
            {
                Preorder(node.Children[key], results);
            }
        }
    }
}

