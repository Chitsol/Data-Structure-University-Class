using System;
using System.Collections.Generic;
using System.Collections;

namespace DatastructurePracitce7week
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var bt = new BinaryTree<int>(1);

                bt.Root.Left = new BinaryTreeNode<int>(2);
                bt.Root.Right = new BinaryTreeNode<int>(3);
                bt.Root.Left.Left = new BinaryTreeNode<int>(4);
                //출력 1243

                Console.WriteLine("전위순회");
                bt.PreoderTraversal();
                Console.WriteLine("\n후위순회");
                bt.PostorderTraversal();
                Console.WriteLine("\n중위순회");
                bt.InorderTraversal();
            }

            //연결리스트 이진트리

            {//생물 이진 트리 구성
             //      A
             //    B   C
             //  D   F
             //
                /* var bt = new binaryTreeUsingArray(7);
                 bt.Root = "A";
                 bt.SetLeft(0, "B");
                 bt.SetRight(0, "C");
                 bt.SetLeft(1, "D");
                 bt.SetLeft(2, "F");
                 //출력: A B C D - F -
                 bt.PrintTree();

                 var data = bt.GetParent(5);
                 //출력: C
                 Console.WriteLine(data);

                 data = bt.GetLeft(2);
                 //출력: F
                 Console.WriteLine(data); */
            }//배열 이진트리
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
        public void InorderIterative()//중위 순회를 스택을 사용하여 구현하는 기본로직
        {
            var stack = new Stack<BinaryTreeNode<T>>();
            var node = Root;
            //Leftmost  노드까지 스택에 저장
            while (node != null)
            {
                stack.Push(node);
                node = node.Left;
            }

            while (stack.Count > 0)
            {
                //스택에서 노드 가져옴
                node = stack.Pop();

                //Visit
                Console.WriteLine(node.Data);

                //오른쪽 노드 있으면 루프를 돌아서 leftmost노드까지 스택에 저장
                //Leftmost 노드까지 스택에 저장
                if (node.Right != null)
                {
                    node = node.Right;
                    //Leftmost 노드까지 스택에 저장
                    while (node != null)
                    {
                        stack.Push(node);
                        node = node.Left;
                    }
                }
            }
        }

        public void PostorderIterative()//후위 순회를 스택을 사용하여 구현하는 기본로직
        {
            var stack = new Stack<BinaryTreeNode<T>>();
            var node = Root;

            //Leftmost 노드까지 오른쪽 자식노드와
            //루트를 스택에 저장
            while (node != null)
            {
                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }
                stack.Push(node);
                node = node.Left;
            }

            while (stack.Count > 0)
            {
                //스택에서 노드 가져옴
                node = stack.Pop();

                //스택 Top이 오른쪽 자식노드와 같으면
                if (node.Right != null && stack.Count > 0 && node.Right == stack.Peek())
                {
                    //오른쪽 자식노드를 Pop
                    var right = stack.Pop();
                    //루트 노드를 다시 Push
                    stack.Push(node);
                    node = right;
                }
            }
        }
    }


    //배열을 이용한 구현
    public class binaryTreeUsingArray
    {
        private object[] arr;

        public binaryTreeUsingArray(int capacity = 15)
        {
            arr = new object[capacity];
        }
        public object Root
        {
            get { return arr[0]; }
            set { arr[0] = value; }
        }
        public void SetLeft(int parentIndex, object data)
        {
            int leftIndex = parentIndex * 2 + 1;

            //부모노드가 없거나 배열이 Full인 경우
            if (arr[parentIndex] == null || leftIndex >= arr.Length)
            {
                throw new ApplicationException("Error");
            }

            arr[leftIndex] = data;
        }
        public void SetRight(int parentIndex, object data)
        {
            int rightIndex = parentIndex * 2 + 2;

            if (arr[parentIndex] == null || rightIndex >= arr.Length)
            {
                throw new ApplicationException("Error");
            }

            arr[rightIndex] = data;
        }
        public object GetParent(int childIndex)
        {
            //루트노드의 부모는 없음
            if (childIndex == 0) return null;

            int parentIndex = (childIndex - 1) / 2;
            return arr[parentIndex];
        }
        public object GetLeft(int parentIndex)
        {
            int leftIndex = parentIndex * 2 + 1;
            return arr[leftIndex];
        }
        public object GetRight(int parentIndex)
        {
            int rightIndex = parentIndex * 2 + 2;
            return arr[rightIndex];
        }
        public void PrintTree()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write("{0}", arr[i] ?? "-");
            }
            Console.WriteLine();
        }
    }

}
