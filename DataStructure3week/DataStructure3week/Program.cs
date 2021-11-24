using System;
using System.Collections.Generic; //need LinkedList
using System.Linq; //need ToList
namespace DataStructure3week
{
    class Program
    {
        static void Main(string[] args)
        {
            {/*var list = new SinglyLinkedList<int>();
            //리스트에 0~4 추가
            for (int i = 0; i < 5; i++)
            {
                list.Add(new SinglyLinkedListNode<int>(i));
            }

            //Index에 2인 요소 삭제
            var node = list.GetNode(2);
            list.Remove(node);

            //Index 1 get
            node = list.GetNode(1);
            //index 1 next index 100 input
            list.AddAfter(node, new SinglyLinkedListNode<int>(100));

            //Index head delete
            node = list.GetNode(0);
            list.Remove(node);

            int count = list.Count();

            //All List print
            //result 1 100 3 4
            for (int i = 0; i < count; i++)
            {
                var n = list.GetNode(i);
                Console.WriteLine(n.Data);
            }*/
            } //SinglyList Test
            {/*var list = new DoublyLinkedList<int>();
            //리스트에 0~4 추가
            for (int i = 0; i < 5; i++)
            {
                list.Add(new DoublyLinkedListNode<int>(i));
            }

            //Index에 2인 요소 삭제
            var node = list.GetNode(3);
            list.Remove(node);
            
            //Index 1 get
            node = list.GetNode(1);
            //index 1 next index 100 input
            list.AddAfter(node, new DoublyLinkedListNode<int>(100));

            //Index head delete
            node = list.GetNode(0);
            list.Remove(node);

            int count = list.Count();

            //All List print
            //result 1 100 3 4
            for (int i = 0; i < count; i++)
            {
                var n = list.GetNode(i);
                Console.WriteLine(n.Data);
            }

            node = list.GetNode(3);
            //list reverse print
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(node.Data);
                node = node.Prev;
            }*/
            } //DoublyLinkedList Test       
            {/*
                var list = new CircularLinkedList<int>();
                //Add 0~5
                for (int i = 0; i < 5; i++)
                {
                    list.Add(new DoublyLinkedListNode<int>(i));
                }


                //Index head, 2 remove
                var node = list.GetNode(2);
                list.Remove(node);
                //get 1 index 
                node = list.GetNode(1);

                //1 Index after 100 Index
                list.AddAfter(node, new DoublyLinkedListNode<int>(100));

                //CircularLinkedCheck
                Console.WriteLine(CircularLinkedList<int>.IsCircular(node));

                //Check Count
                int count = list.Count();

                //check circleList for double print
                node = list.GetNode(0);
                for (int i = 0; i < count * 2; i++)
                {
                    Console.WriteLine(node.Data);
                    node = node.Next;
                }

                Console.WriteLine("~~first double print end~~");
                
                //head remove
                node = list.GetNode(0);
                list.Remove(node);

                //Check Count
                count = list.Count();
                //check circleList for double print
                node = list.GetNode(0);
                for (int i = 0; i < count * 2; i++)
                {
                    Console.WriteLine(node.Data);
                    node = node.Next;
                }

                Console.WriteLine("~~second double print end~~");*/
            } //edit int CircularLinkedList
            {
                {
                    var list = new LinkedList<string>();
                    list.AddLast("Apple");
                    list.AddLast("Banana");
                    list.AddLast("Lemon");

                    var node = list.Find("Banana");
                    var newNode = new LinkedListNode<string>("Grape");

                    //new Grape node input Banana after
                    list.AddAfter(node, newNode);

                    //print list
                    list.ToList<string>().ForEach(p => Console.WriteLine(p));

                    //delete Grape
                    list.Remove("Grape");

                    //Enumerator use print list
                    foreach (var m in list)
                    {
                        Console.WriteLine(m);
                    }
                }
            } //Net LinkedList Test
        }

        public class SinglyLinkedListNode<T>
        {
            public T Data { get; set; }
            public SinglyLinkedListNode<T> Next { get; set; }

            public SinglyLinkedListNode(T data)
            {
                this.Data = data;
                this.Next = null;
            }
        }
        public class SinglyLinkedList<T>
        {
            private SinglyLinkedListNode<T> head;

            public void Add(SinglyLinkedListNode<T> newNode)
            {
                if (head == null)
                {
                    head = newNode;
                }
                else
                {
                    var current = head;// 마지막 노드로 이동하여 추가
                    while (current != null && current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = newNode;
                }
            }

            public void AddAfter(SinglyLinkedListNode<T> current, SinglyLinkedListNode<T> newNode)
            {
                if (head == null || current == null || newNode == null)
                {
                    throw new InvalidOperationException();
                }

                newNode.Next = current.Next;
                current.Next = newNode;
            }

            public void Remove(SinglyLinkedListNode<T> removeNode)
            {
                if (head == null || removeNode == null) return;

                //삭제할 노드가 첫노드이면
                if (removeNode == head)
                {
                    head = head.Next;
                    removeNode = null;
                }
                else
                {
                    var current = head;
                    //단방량이므로 삭제할 노드 바로 이전의 노드를 검색
                    while (current != null && current.Next != removeNode)
                    {
                        current = current.Next;
                    }

                    if (current != null)
                    {
                        //이전노드의 Next에 삭제노드의 Next를 할당
                        current.Next = removeNode.Next;
                        removeNode = null;
                    }
                }
            }

            public SinglyLinkedListNode<T> GetNode(int index)
            {
                var current = head;
                for (int i = 0; i < index && current != null; i++)
                {
                    current = current.Next;
                }
                //만약 index가 리스트 카운트보다 크면 null이 리턴됨
                return current;
            }

            public int Count()
            {
                int cnt = 0;

                var current = head;
                while (current != null)
                {
                    cnt++;
                    current = current.Next;
                }

                return cnt;
            }
        }
        public class DoublyLinkedListNode<T>
        {
            public T Data { get; set; }
            public DoublyLinkedListNode<T> Prev { get; set; }
            public DoublyLinkedListNode<T> Next { get; set; }

            public DoublyLinkedListNode(T data) : this(data, null, null)
            {
            }

            public DoublyLinkedListNode(T data, DoublyLinkedListNode<T> prev, DoublyLinkedListNode<T> next)
            {
                this.Data = data;
                this.Prev = prev;
                this.Next = next;
            }
        }
        public class DoublyLinkedList<T>
        {
            private DoublyLinkedListNode<T> head;


            public void Add(DoublyLinkedListNode<T> newNode)
            {
                if (head == null) //리스트가 비어있다면
                {
                    head = newNode;
                }
                else //비어있지 않으면, 마지막으로 이동하여 추가
                {
                    var current = head;
                    while (current != null && current.Next != null)
                    {
                        current = current.Next;
                    }

                    //추가할 때 양방향 연결
                    current.Next = newNode;
                    newNode.Prev = current;
                    newNode.Next = null;
                }
            }
            public void AddAfter(DoublyLinkedListNode<T> current, DoublyLinkedListNode<T> newNode)
            {
                if (head == null || current == null || newNode == null)
                {
                    throw new InvalidOperationException();
                }

                newNode.Next = current.Next;
                current.Next.Prev = newNode;
                newNode.Prev = current;
                current.Next = newNode;
            }
            public void Remove(DoublyLinkedListNode<T> removeNode)
            {
                if (head == null || removeNode == null)
                {
                    Console.WriteLine("리턴안됨");
                    return;
                }

                //삭제 노드가 첫 노드이면
                if (removeNode == head)
                {
                    Console.WriteLine("head 확인");
                    head = head.Next;
                    if (head != null)
                    {
                        Console.WriteLine("head리무브 시작");
                        head.Prev = null;
                    }
                }
                else
                {
                    if (removeNode != null)
                    {
                        //이전노드의 Next에 삭제노드의 Next를 할당
                        removeNode.Next.Prev = removeNode.Prev;
                        removeNode.Prev.Next = removeNode.Next;

                        removeNode = null;
                    }
                }
            }
            public DoublyLinkedListNode<T> GetNode(int index)
            {
                var current = head;
                for (int i = 0; i < index && current != null; i++)
                {
                    current = current.Next;
                }

                //if index > listCount return null
                return current;
            }

            public int Count()
            {
                int cnt = 0;

                var current = head;
                while (current != null)
                {
                    cnt++;
                    current = current.Next;
                }

                return cnt;
            }
        }
        public class CircularLinkedList<T>
        {
            private DoublyLinkedListNode<T> head;
            public void checkhead()
            {
                if (head == null)
                {
                    Console.WriteLine("head 없음");
                }
                else Console.WriteLine("head 있음");
                Console.WriteLine("head.Prev : " + head.Prev);
                Console.WriteLine("head.Next : " + head.Next);
            }
            public void Add(DoublyLinkedListNode<T> newNode)
            {
                if (head == null) //리스트가 비어 있으면
                {
                    head = newNode;
                    head.Next = head;
                    head.Prev = head;
                }
                else
                {
                    var tail = head.Prev;

                    head.Prev = newNode;
                    tail.Next = newNode;
                    newNode.Prev = tail;
                    newNode.Next = head;
                }
            }

            public void AddAfter(DoublyLinkedListNode<T> current, DoublyLinkedListNode<T> newNode)
            {
                if (head == null || current == null || newNode == null)
                {
                    throw new InvalidOperationException();
                }

                newNode.Next = current.Next;
                current.Next.Prev = newNode;
                newNode.Prev = current;
                current.Next = newNode;
            }

            public void Remove(DoublyLinkedListNode<T> removeNode)
            {
                if (head == null || removeNode == null) return;

                //삭제할 노드가 헤드노드이고 노드가 1개이면
                if (removeNode == head && head == head.Next)
                {
                    head = null;
                }
                else //아니면 Prev 노드와 Next 노드를 연결
                {
                    if (removeNode == head) head = removeNode.Next;
                    removeNode.Prev.Next = removeNode.Next;
                    removeNode.Next.Prev = removeNode.Prev;
                }

                removeNode = null;
            }

            public DoublyLinkedListNode<T> GetNode(int index)
            {
                if (head == null) return null;

                int cnt = 0;
                var current = head;
                while (cnt < index)
                {
                    current = current.Next;
                    cnt++;
                    if (current == head)
                    {
                        return null;
                    }
                }

                return current;
            }

            public int Count()
            {
                if (head == null) return 0;

                int cnt = 0;
                var current = head;
                do
                {
                    cnt++;
                    current = current.Next;
                } while (current != head);

                return cnt;
            }

            public static bool IsCircular(DoublyLinkedListNode<T> head)
            {
                //빈 리스트는 원형리스트임
                if (head == null) return true;

                var current = head;
                while (current != null)
                {
                    current = current.Next;
                    if (current == head)
                    {
                        return true;
                    }
                }
                return false;
            }

            public static bool IsCyclic(SinglyLinkedListNode<int> head)
            {
                var p1 = head;
                var p2 = head;

                do
                {
                    p1 = p1.Next;
                    p2 = p2.Next;
                    if (p1 == null || p2 == null || p2.Next == null)
                    {
                        return false;
                    }
                    p2 = p2.Next;
                } while (p1 != p2);

                return true;
            }
        }
    }
}
