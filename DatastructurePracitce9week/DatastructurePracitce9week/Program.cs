using System;
using System.Collections;
using System.Collections.Generic;

namespace DatastructurePracitce9week
{
    class Program 
    {
        static void Main(string[] args)
        {
            var root = new BinaryTreeNode<int>(3);
            root.Left = new BinaryTreeNode<int>(5);
            root.Left.Left = new BinaryTreeNode<int>(7);
            root.Left.Right = new BinaryTreeNode<int>(6);
            root.Right = new BinaryTreeNode<int>(9);
            root.Right.Right = new BinaryTreeNode<int>(8);

            var tree = BST<int>.ConverToBST(root);

            var treeTest = new BinaryTree<int>(tree);
            Console.WriteLine("\n전위 순회");
            //전위
            treeTest.InorderTraversal();
            Console.WriteLine("\n후위 순회");
            //후위
            treeTest.PostorderTraversal();
            Console.WriteLine("\n중위 순회");
            //중위
            treeTest.PreoderTraversal();

            //var bst = new BST<int>();
            //bst.Add(6);
            //bst.Add(7);
            //bst.Add(2);
            //bst.Add(1);
            //bst.Add(5);
            //bst.Add(3);
            //bst.Add(4);

            //List<int> list = new List<int>();

            //list = bst.ToSortedList();

            //for (int i = 0; i < list.Count; i++)
            //{
            //    Console.WriteLine($"{list[i]}");
            //}

            ////출력 2
            //bst.LeastCommonAncestor(1, 4);

            //// 출력: 1 2 3 4 5 6 7
            //var list2 = bst.ToSortedList();
            //PrintList(list2);

            ////삭제
            //bst.Remove(2);

            ////출력 : 1 3 4 5 6 7
            //PrintList(bst.ToSortedList());

            //var bst2 = new BST<Char>();
            //bst2.Add('A');
            //bst2.Add('B');
            //bst2.Add('H');
            //bst2.Add('D');
            //bst2.Add('K');
            //bst2.Add('C');
            //bst2.Add('P');

            //bool found = bst2.Search('P');
            //Console.WriteLine(found);

            ////출력: 
            //var printlist = bst2.ToSortedList();
            //foreach (char a in printlist)
            //{
            //    Console.Write(a);
            //}
            //Console.Write("\n");

            ////삭제
            //bst2.Remove('C');

            //printlist = bst2.ToSortedList();
            ////출력: 1 3 4 5 6 7
            //foreach (char a in printlist)
            //{
            //    Console.Write(a);
            //}
            //Console.Write("\n");

            //List<int> list = new List<int>();

            //list = bst.ToSortedList();

            //for (int i = 0; i < list.Count; i++)
            //{
            //    Console.WriteLine($"{list[i]}");
            //}

            ////출력 2
            //bst.LeastCommonAncestor(1, 4);


            //bool found = bst.Search(4);
            //Console.WriteLine(found);
            /**/
            
        }


        static void PrintList(List<int> list)
        {
            
            foreach (var item in list)
            {
                Console.Write($"{item}");
            }
            Console.WriteLine();
        }
 
        public class BST<T> where T : IComparable<T>
        { //Nested Class로 구현
            private class Node<P>
            {
                public P Data { get; set; }
                public Node<P> Left { get; set; }
                public Node<P> Right { get; set; }

                public Node(P data)
                {
                    this.Data = data;
                }
            }
            //root field 
            private Node<T> root;
            //default 
            public void Add(T data)
            {
                if (root == null)
                {
                    root = new Node<T>(data);
                    return;
                }

                var node = root;
                while (node != null)
                {
                    int cmp = data.CompareTo(node.Data);
                    if (cmp == 0)
                    {
                        throw new
                            ApplicationException("Diplicate");
                    }
                    else if (cmp < 0)
                    {
                        if (node.Left == null)
                        {
                            node.Left = new Node<T>(data);
                            break;
                        }
                        else
                        {
                            node = node.Left;
                        }
                    }
                    else
                    {
                        if (node.Right == null)
                        {
                            node.Right = new Node<T>(data);
                            break;
                        }
                        else
                        {
                            node = node.Right;
                        }
                    }
                }
            }

            public bool Search(T data)
            {
                var node = root;

                while (node != null)
                {
                    int cmp = data.CompareTo(node.Data);
                    if (cmp == 0)
                    {
                        return true;
                    }
                    else if (cmp < 0)
                    {
                        node = node.Left;
                    }
                    else
                    {
                        node = node.Right;
                    }
                }

                return false;
            }

            public bool SearchRecursive(T data)
            {
                return SearchRecursive(root, data);
            }

            private bool SearchRecursive(Node<T> node, T data)
            {
                if (node == null) return false;

                int cmp = data.CompareTo(node.Data);
                if (cmp == 0)
                {
                    return true;
                }

                return (cmp < 0) ? SearchRecursive(node.Left, data) : SearchRecursive(node.Right, data);
            }

            public bool Remove(T data)
            {
                var node = root;
                Node<T> prev = null;

                //삭제할 노드 검색
                while (node != null)
                {
                    int cmp = data.CompareTo(node.Data);
                    if (cmp == 0)
                    {
                        Console.WriteLine("Find delete data = " + node.Data);
                        break;
                    }
                    else if (cmp < 0)
                    {
                        prev = node;
                        node = node.Left;
                    }
                    else
                    {
                        prev = node;
                        node = node.Right;
                    }
                }

                if (node == null) return false;

                //삭제처리
                if (node.Left == null && node.Right == null)
                {
                    if (prev.Left == node)
                    {
                        prev.Left = null;
                    }
                    else
                    {
                        prev.Right = null;
                    }
                    node = null;
                }
                else if (node.Left == null || node.Right == null)
                {
                    var child = (node.Left != null) ? node.Left : node.Right;

                    if (prev.Left == node)
                    {
                        prev.Left = child;
                    }
                    else
                    {
                        prev.Right = child;
                    }

                    node = null;
                }
                else
                {
                    var pre = node;
                    var min = node.Right;
                    while (min.Left != null)
                    {
                        pre = min;
                        min = min.Left;
                    }

                    //min노드값으로 대치
                    node.Data = min.Data;

                    //min 노드의 오른쪽 노드 처리
                    if (pre.Left == min)
                    {
                        pre.Left = min.Right;
                    }
                    else
                    {
                        pre.Right = min.Right;
                    }
                }

                return true;
            }

            public List<T> ToSortedList()
            {
                var list = new List<T>();
                Traversal(root, list);
                return list;
            }

            private void Traversal(Node<T> node, List<T> list)
            {
                if (node == null) return;

                Traversal(node.Left, list);
                list.Add(node.Data);
                Traversal(node.Right, list);
            }

            public void FindKthSmallest(int k)
            {
                int count = 0;
                FindKthSmallest(root, k, ref count);
            }

            private bool FindKthSmallest(Node<T> node, int k, ref int count)
            {
                if (node == null) return false;
                bool found = FindKthSmallest(node.Left, k, ref count);

                if (found) return true;
                count++; //왜 k번째 수가 작은수가 되는지 알아보기 내일하자
                if (count == k)
                {
                    Console.WriteLine(node.Data);
                    return true;
                }

                return FindKthSmallest(node.Right, k, ref count);
            }

            public void InorderSuccessor(T key)
            {
                Node<T> node = root;
                Node<T> prev = null;

                while (node != null)
                {
                    int cmp = node.Data.CompareTo(key);
                    if (cmp == 0)
                    {
                        if (node.Right == null)
                        {
                            if (prev != null)
                            {
                                Console.WriteLine(prev.Data);
                            }
                        }
                        else
                        {//오른쪽 서브트리에서 가장 왼쪽 노드 검색
                            node = node.Right;
                            while (node.Left != null)
                            {
                                node = node.Left;
                            }
                            Console.WriteLine(node.Data);
                        }

                        break;
                    }
                    else if (cmp > 0)
                    {
                        prev = node;
                        node = node.Left;
                    }
                    else
                    {
                        prev = node;
                        node = node.Right;
                    }
                }
            }

            public void LeastCommonAncestor(T a, T b)
            {
                Node<T> lca = LCA(root, a, b);

                if (lca != null)
                {
                    Console.WriteLine(lca.Data);
                }
            }
            //Recursive 방식
            private Node<T> LCA(Node<T> node, T a, T b)
            {
                if (node == null) return null;

                if (a.CompareTo(node.Data) < 0 && b.CompareTo(node.Data) < 0)
                {
                    return LCA(node.Left, a, b);
                }
                else if (a.CompareTo(node.Data) > 0 && b.CompareTo(node.Data) > 0)
                {
                    return LCA(node.Right, a, b);
                }

                return node;
            }
            //Iterative 방식
            private Node<T> IterativeLCA(Node<T> node, T a, T b)
            {
                while (node != null)
                {
                    if (a.CompareTo(node.Data) < 0 && b.CompareTo(node.Data) < 0)
                    {
                        node = node.Left;
                    }
                    else if (a.CompareTo(node.Data) > 0 && b.CompareTo(node.Data) > 0)
                    {
                        node = node.Right;
                    }
                    else
                    {
                        break;
                    }
                }

                return node;
            }

            public static BinaryTreeNode<T> ConverToBST(BinaryTreeNode<T> root)
            {
                if (root == null) return null;

                //키를 배열로 저장
                List<T> keys = new List<T>();
                ExtractKeys(root, keys);

                //소트
                keys.Sort();

                //순서대로 키 대입
                int index = 0;
                ReplaceKeys(root, keys, ref index);
                return root;
            }

            private static void ExtractKeys(BinaryTreeNode<T> root, List<T> keys)
            {
                if (root == null) return;
                ExtractKeys(root.Left, keys);
                keys.Add(root.Data);
                ExtractKeys(root.Right, keys);
            }

            private static void ReplaceKeys(BinaryTreeNode<T> root, List<T> keys, ref int index)
            {
                if (root == null) return;
                ReplaceKeys(root.Left, keys, ref index);
                root.Data = keys[index];
                index++;
                ReplaceKeys(root.Right, keys, ref index);
            }
        }

        public class BinaryTreeNode<T>
        {
            public T Data { get; set; }
            public BinaryTreeNode<T> Left { get; set; }
            public BinaryTreeNode<T> Right { get; set; }
            public BinaryTreeNode(T data)
            {
                this.Data = data;
            }
        }
        //이진트리 클래스
        //연결리스트를 이용한 구현
        public class BinaryTree<T>
        {
            public BinaryTreeNode<T> Root
            {
                get; private set;
            }

            public BinaryTree(T root)
            {
                Root = new BinaryTreeNode<T>(root);
            }

            public BinaryTree(BinaryTreeNode<T> root)
            {
                Root = root;
            }

            //트리데이터 출력 예
            public void PreoderTraversal()
            {
                PreoderTraversal(Root);
            }
            //귀재방식
            private void PreoderTraversal(BinaryTreeNode<T> node)
            {
                if (node == null) return;

                Console.Write("{0}", node.Data);
                PreoderTraversal(node.Left);
                PreoderTraversal(node.Right);
            }

            public void InorderTraversal()
            {
                InorderTraversal(Root);
            }
            private void InorderTraversal(BinaryTreeNode<T> node)
            {
                if (node == null) return;

                InorderTraversal(node.Left);
                Console.Write("{0}", node.Data);
                InorderTraversal(node.Right);
            }
            public void PostorderTraversal()
            {
                PostorderTraversal(Root);
            }
            private void PostorderTraversal(BinaryTreeNode<T> node)
            {
                if (node == null) return;

                PostorderTraversal(node.Left);
                PostorderTraversal(node.Right);
                Console.Write("{0}", node.Data);
            }

            public void PreorderIterative() //반복방식을 사용한 이진트리 순회
            {
                if (Root == null) return;

                var stack = new Stack<BinaryTreeNode<T>>();
                //루트를 스택에 저장
                stack.Push(Root);

                while (stack.Count > 0)
                {
                    //스택에서 노드 가져옴
                    var node = stack.Pop();

                    //Visit
                    Console.WriteLine(node.Data);

                    //오른쪽 노드 스택에 저장
                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }

                    //왼쪽에 노드 스택에 저장
                    if (node.Left != null)
                    {
                        stack.Push(node.Left);
                    }
                }
            }

        }
    }
}
