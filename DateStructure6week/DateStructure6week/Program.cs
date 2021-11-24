using System;
using System.Collections.Generic;

namespace DateStructure6week
{
    class Program
    {
        static void Main(string[] args)
        {
            {/*var A = new TreeNode("A");
            var B = new TreeNode("B");
            var C = new TreeNode("C");
            var D = new TreeNode("D");

            A.Links.Add(B);
            A.Links.Add(C);
            A.Links.Add(D);

            B.Links.Add(new TreeNode("E"));
            B.Links.Add(new TreeNode("F"));

            D.Links.Add(new TreeNode("G"));

            //A 의 자식 노드들 출력
            foreach (var node in A.Links)
            {
                Console.WriteLine(node.Data);
            }
            */
            }//N-Link Expression
            {/* var A = new LCRSNode("A");
            var B = new LCRSNode("B");
            var C = new LCRSNode("C");
            var D = new LCRSNode("D");
            var E = new LCRSNode("E");
            var F = new LCRSNode("F");
            var G = new LCRSNode("G");

            A.LeftChild = B;
            B.RightSibling = C;
            C.RightSibling = D;
            D.LeftChild = E;
            E.RightSibling = F;
            D.LeftChild = G;

            // A의 자식노드들 출력
            if (A.LeftChild != null)
            {
                var tmp = A.LeftChild;
                Console.WriteLine(tmp.Data);

                while (tmp.RightSibling != null)
                {
                    tmp = tmp.RightSibling;
                    Console.WriteLine(tmp.Data);
                }
            }*/
            }//
            {
                LCRSTree tree = new LCRSTree("A");
                var A = tree.Root;
                var B = tree.AddChild(A, "B");

                tree.AddChild(A, "C");
                var D = tree.AddSibling(B, "D");
                tree.AddChild(B, "E");
                tree.AddChild(B, "F");
                tree.AddChild(D, "G");

                //출력
                tree.PrintIndentTree();
                tree.PrintLevelOrder();

            }
            Console.WriteLine("\n");
            BSTNodeTree BSTtree = new BSTNodeTree(50);

            BSTtree.ADDChild(15);
            BSTtree.ADDChild(62);
            BSTtree.ADDChild(80);
            BSTtree.ADDChild(7);
            BSTtree.ADDChild(54);
            BSTtree.ADDChild(11);

            BSTtree.PrintIndenTTREE();
            Console.WriteLine(BSTtree.SearchNode(BSTtree.root, 54).Data);
        }
    }

    //N-Link Expression
    class TreeNode
    {
        public object Data { get; set; }
        //public TreeNode[] Link {get; private set;}
        public List<TreeNode> Links { get; private set; }

        public TreeNode(object data) //int maxDegree = 3
        {
            Data = data;
            Links = new List<TreeNode>();
        }
    }

    class BSTNode
    {
        public int Data { get; set; }
        public BSTNode LChild { get; set; }
        public BSTNode RChild { get; set; }

        public BSTNode(int data)
        {
            this.Data = data;
        }
    }

    class BSTNodeTree
    {
        public BSTNode root { get; private set; }
        public BSTNodeTree(int rootData)
        {
            this.root = new BSTNode(rootData);
        }

        public BSTNode ADDChild(int data)
        {
            BSTNode parent = this.root;
            if (parent == null) return null;
            BSTNode child = new BSTNode(data);
            bool addnotChild = true;

            while (addnotChild)
            {
                if (parent.Data > child.Data ? true : false)
                {
                    if (parent.RChild == null)
                    {
                        parent.RChild = child;
                        addnotChild = false;
                    }
                    else parent = parent.RChild;
                }
                else
                {
                    if (parent.Data == child.Data) return null; //이진탐색트리는 같은값을 가질 수 없다.

                    if (parent.LChild == null)
                    {
                        parent.LChild = child;
                        addnotChild = false;
                    }
                    else parent = parent.LChild;
                    
                }
            }
            return child;
        }

        public void PrintIndenTTREE()
        {
            PrintIndenT(this.root, 1);
        }
        public void PrintIndenT(BSTNode node, int indent)
        {
            if (node == null) return;
            //현재노드 출력
            string pad = " ".PadLeft(indent);
            Console.WriteLine($"{pad}{node.Data}");

            //왼쪽 자식
            //(자식이므로 Indent 증가)
            PrintIndenT(node.LChild, indent + 1);

            //오른쪽 자식
            PrintIndenT(node.RChild, indent + 1);
        }

        public BSTNode SearchNode(BSTNode rootnode, int key)
        {
            //root값이 널이거나 key값이라면 종료
            if (rootnode == null || rootnode.Data == key)
            {
                return rootnode;
            }
            //Key가 root.data보다 작으면 왼쪽 서브트리로 재귀
            if (rootnode.Data > key) return SearchNode(rootnode.LChild, key);
            else return SearchNode(rootnode.RChild, key);
        }
    }

    class LCRSNode
    {
        public object Data { get; set; }
        public LCRSNode LeftChild { get; set; }
        public LCRSNode RightSibling { get; set; }

        public LCRSNode(object data)
        {
            Data = data;
        }
    }

    class LCRSTree
    {
        public LCRSNode Root { get; private set; }
        public LCRSTree(object rootData)
        {
            Root = new LCRSNode(rootData);
        }

        //자식노드 추가
        public LCRSNode AddChild(LCRSNode parent, object data)
        {
            if (parent == null) return null;

            LCRSNode child = new LCRSNode(data);
            if (parent.LeftChild == null)
            {
                parent.LeftChild = child;
            }
            else
            {
                var node = parent.LeftChild;
                while (node.RightSibling != null)
                {
                    node = node.RightSibling;
                }
                node.RightSibling = child;
            }

            return child;
        }

        //형제노드추가
        public LCRSNode AddSibling(LCRSNode node, object data)
        {
            if (node == null) return null;

            while (node.RightSibling != null)
            {
                node = node.RightSibling;
            }
            var sibling = new LCRSNode(data);
            node.RightSibling = sibling;

            return sibling;
        }

        //레벨순으로 트리노드 출력
        public void PrintLevelOrder()
        {
            var q = new Queue<LCRSNode>();
            q.Enqueue(this.Root);

            while (q.Count > 0)
            {
                var node = q.Dequeue();

                while (node != null)
                {
                    Console.Write($"{node.Data}");
                    //node = root -> q = leftChild, node = rightSibling(null)
                    //node = leftchild -> 
                    //

                    if (node.LeftChild != null)
                    {
                        q.Enqueue(node.LeftChild);
                    }

                    node = node.RightSibling;
                }
            }
        }

        // 들여쓰기 방식으로 트리 출력
        public void PrintIndentTree()
        {
            PrintIndent(this.Root, 1);
        }

        private void PrintIndent(LCRSNode node, int indent)
        {
            if (node == null) return;
            //현재노드 출력
            string pad = " ".PadLeft(indent);
            Console.WriteLine($"{pad}{node.Data}");

            //왼쪽 자식
            //(자식이므로 Indent 증가)
            PrintIndent(node.LeftChild, indent + 1);

            //오른쪽 형제
            // (형제이므로 동일 Indent 사용)
            PrintIndent(node.RightSibling, indent);
        }
    }


}
