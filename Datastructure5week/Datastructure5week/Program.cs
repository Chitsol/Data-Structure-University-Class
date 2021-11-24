using System;
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Concurrent;
//using System.Threading;
//using System.Threading.Tasks;

namespace Datastructure5week
{
    class Program
    {
        
        static void Main(string[] args)
        {
            {
                // Queue q = new Queue();
                // q.Enqueue(11);
                // q.Enqueue(21);
                // q.Enqueue(33);

                // object data = q.Dequeue();
                // Console.WriteLine((int)data);

                // foreach (var item in q)
                // {
                //     Console.WriteLine(item);
                // }
            }//.Net Queue 예제
            {
                /*var s = new ConcurrentStack<int>();

                //데이타를 스택에 넣는 쓰레드
                Task tPush = Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        s.Push(i);
                        Thread.Sleep(100);
                    }
                });
                //데이타를 스택에서 읽는 쓰레드
                Task tPop = Task.Factory.StartNew(() =>
                {
                    int n = 0;
                    int result;
                    while (n < 100)
                    {
                        if (s.TryPop(out result))
                        {
                            Console.WriteLine(result);
                            n++;
                        }
                        Thread.Sleep(150);
                    }
                });

                //두쓰레드 끝날때까지 대기
                Task.WaitAll(tPush, tPop);*/
            }//.Net 스택 예제

            StackCalculator stackCalculator = new StackCalculator();

            string[] operation= { "+", "-", "*", "/", "(", ")" };

            List<string> postfix = new List<string>();
            Stack<String> opStack = new Stack<String>(); //연산자스택
            int num = 2; //연산자 구분 수 1*/, 2+-, 3)( 순위
            
            string input = Console.ReadLine();
            string[] arrayinput = input.Split();

            bool calCheck = false; 

            for(int i = 0; i < arrayinput.Length; i++)
            {
                calCheck = false;
                for (int j = 0; j < operation.Length; j++)
                {
                    if (arrayinput[i] == operation[j])
                    {
                        calCheck = true;
                        if (operation[j] == "*"||operation[j] == "/")
                        {
                            if (num <= 1) // 우선순위가 1보다 낮거나 같다면 postfix로 푸쉬
                            {
                                int count = opStack.Count;
                                for (int s = 0; s < count; s++)
                                {
                                    postfix.Add(opStack.Pop());
                                }
                            }
                            opStack.Push(operation[j]);
                            num = 1;
                        }else if (operation[j] == "+" || operation[j] == "-")
                        {
                            if (num <= 2) // 우선순위가 2보다 낮거나 같다면 postfix로 푸쉬
                            {
                                int count = opStack.Count;
                                for (int s = 0; s < count; s++)
                                {
                                    postfix.Add(opStack.Pop());
                                }
                            }
                            opStack.Push(operation[j]);
                            num = 2;
                        }else if(operation[j] == "(" || operation[j] == ")")
                        {
                            int count = opStack.Count;
                            for (int s = 0; s < count; s++) //나온 순간 무조건 푸쉬
                            {
                                postfix.Add(opStack.Pop());
                            }
                        }
                    }
                }
                if (calCheck) { } //연산자임
                else postfix.Add(arrayinput[i]);
            }
            postfix.Add(opStack.Pop());

            for(int i = 0; i < postfix.Count; i++)
            {
                Console.WriteLine(postfix[i]);
            }
            int value = stackCalculator.Calculate(postfix);
            Console.WriteLine(value);
        }
    }

    public class StackCalculator
    {

        public int Calculate(List<string> token)
        {
            Stack<int> stack = new Stack<int>();
            try
            {
                for (int i = 0; i < token.Count; i++)
                {
                    switch (token[i])
                    {
                        case "+": stack.Push(stack.Pop() + stack.Pop()); break;
                        case "-": stack.Push(-(stack.Pop() - stack.Pop())); break;
                        case "*": stack.Push(stack.Pop() * stack.Pop()); break;
                        case "/":
                            int rv = stack.Pop();
                            stack.Push(stack.Pop() / rv); break;
                        default: stack.Push(int.Parse(token[i])); break;
                    }
                }

                return stack.Pop();
            }
            catch
            {
                return 0;
            }
        }
    }


    public class QueueUsingCircularArray
    {
        //단순화를 위해 obj 데이타 타입 사용
        private object[] a;
        private int front;
        private int rear;

        //디폴트 큐 크기는 16 이지만 지정 가능
        public QueueUsingCircularArray(int queueSize = 16)
        {
            a = new object[queueSize];
            front = -1;
            rear = -1;
        }

        public void Enqueue(object data)
        {
            //큐가 가득차 있는지 체크
            if ((rear + 1) % a.Length == front)
            {
                //에러처리
                throw new ApplicationException("Full");
            }
            else
            {
                //비어있는경우
                if (front == -1)
                {
                    front++;
                }
            }
            // 데이타 추가
            rear = (rear + 1) % a.Length;
            a[rear] = data;
        }
        public Object Dequeue()
        {
            //큐가 비어있는지 체크
            if (front == -1 && rear == -1)
            {
                throw new ApplicationException("Empty");
            }
            else
            {
                //데이타 읽기
                object data = a[front];

                //마지막 요소를 읽은 경우
                if (front == rear)
                {
                    //특수값 -1로 설정
                    front = -1;
                    rear = -1;
                }
                else
                {
                    //Front 이동
                    front = (front + 1) % a.Length;
                }

                return data;
            }
        }
    }
    public class QueueUsingCircularArray2
    {
        //단순화를 위해 obj 데이타 타입 사용
        private object[] a;
        private int front = 0;
        private int rear = 0;

        //디폴트 큐 크기는 16 이지만 지정 가능
        public QueueUsingCircularArray2(int queueSize = 16)
        {
            a = new object[queueSize];
        }

        public void Enqueue(object data)
        {
            //큐가 가득차 있는지 체크
            if ((rear + 1) % a.Length == front)
            {
                //에러처리
                throw new ApplicationException("Full");
            }
            /*else
            {
                //비어있는경우
                if (front == -1)
                {
                    front++;
                }
            }*/ //이만큼 줄일 수 있음

            // 데이타 추가
            rear = (rear + 1) % a.Length;
            a[rear] = data;
        }
        public Object Dequeue()
        {
            //큐가 비어있는지 체크
            if (front == rear)
            {
                throw new ApplicationException("Empty");
            }
            else
            {
                //데이타 읽기
                object data = a[front];

                /*if (front == rear)
                 {
                     //특수값 -1로 설정
                     front = -1;
                     rear = -1;
                 }
                 else
                 {
                     //Front 이동
                     front = (front + 1) % a.Length;
                 }*/ //이만큼 조건식을 줄일 수 있음

                front = (front + 1) % a.Length;

                return data;
            }
        }
    }
    public class QueueUsingCircularArray3
    {
        //단순화를 위해 obj 데이타 타입 사용
        private object[] a;
        private int front;
        private int rear;

        public int Count { get; private set; } = 0;
        //디폴트 큐 크기는 16 이지만 지정 가능
        public QueueUsingCircularArray3(int queueSize = 16)
        {
            a = new object[queueSize];
        }

        public void Enqueue(object data)
        {
            //큐가 가득차 있는지 체크
            if (Count == a.Length)
            {
                //에러처리
                throw new ApplicationException("Full");
            }

            // 데이타 추가
            a[rear] = data;
            rear = (rear + 1) % a.Length;
            Count++; //증가
        }
        public Object Dequeue()
        {
            //큐가 비어있는지 체크
            if (Count == 0)
            {
                throw new ApplicationException("Empty");
            }

            //데이타 읽기
            object data = a[front];
            front = (front + 1) % a.Length;
            Count--; //감소
            return data;

        }
    }
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            this.Data = data;
            this.Next = null;
        }
    }
    public class QueueUsingLinkedList<T>
    {
        private Node<T> head = null;
        private Node<T> tail = null;

        public void Enqueue(T data)
        {
            //Queue == null
            if (head == null)
            {
                head = new Node<T>(data);
                tail = head;
            }
            else
            {
                tail.Next = new Node<T>(data);
                tail = tail.Next;
            }
        }

        public object Dequeue()
        {
            //Queue == null
            if (head == null)
            {
                throw new ApplicationException("Empty");
            }

            object data = head.Data;

            //Queue에 하나남은 경우
            if (head == tail)
            {
                head = tail = null;
            }
            else
            {
                head = head.Next;
            }
            return data;
        }
    }
    public class StackUsingArray
    {
        private object[] a;
        private int top;

        public StackUsingArray(int capacity = 16)
        {
            a = new object[capacity];
            top = -1;
        }

        public void Push(object data)
        {
            if (top == a.Length - 1)
            {
                //throw and down
                ResizeStack();
            }

            a[++top] = data;
        }

        private void ResizeStack()
        {
            int capacity = 2 * a.Length;
            var tempArray = new object[capacity];
            Array.Copy(a, tempArray, a.Length);
            a = tempArray;
        }

        public object Pop()
        {
            if (this.IsEmpty)
            {
                throw new ApplicationException("Empty");
            }

            return a[top--];
        }

        public object Peek()
        {
            if (this.IsEmpty)
            {
                throw new ApplicationException("Empty");
            }

            return a[top];
        }

        public bool IsEmpty { get { return top == -1; } }

        public int Capacity { get { return a.Length; } }
    }
    public class StackUsingLinkedList<T>
    {
        private StackNode top = null;

        public void Push(object data)
        {
            if (top == null)
            {
                top = new StackNode(data);
            }
            else
            {
                // 노드추가
                var node = new StackNode(data);
                node.Next = top;
                top = node;
            }
        }

        public object Pop()
        {
            if (this.IsEmpty)
            {
                throw new ApplicationException("Empty");
            }

            object data = top.Data;
            top = top.Next;
            return data;
        }

        public object Peek()
        {
            if (this.IsEmpty)
            {
                throw new ApplicationException("Empty");
            }

            return top.Data;
        }

        public bool IsEmpty { get { return top == null; } }

        //스택리스트에서만 사용하는 노드
        private class StackNode
        {
            public object Data { get; set; }
            public StackNode Next { get; set; }

            public StackNode(object data)
            {
                this.Data = data;
                this.Next = null;
            }
        }

    }
}
