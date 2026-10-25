using System;
namespace Queue_Data_Structure;

public class Queue : IQueue{
    private class Node
    {
        private int el;
        private Node? next;

        public int val
        {
            get {
                return el;
            }
            set
            {
      			el = value;
            }
        }

        public Node? nx
        {
            get
            {
                return next;
            }
            set
            {
                if (value is not Node && value is not null)
                {
                    throw new ArgumentException("obj must be Node");
                }
                else
                {
                    next = value;
                }
            }
        }
        public Node(int val, Node? nx = null)
        {
            this.el = val;
            this.next = nx;
        }
    }

    private Node? head;
    private Node? tail;

    public int Head
    {
        get { return head?.val ?? throw new InvalidOperationException("queu is empty"); }
    }

    public int Tail
    {
        get { return tail?.val ?? throw new InvalidOperationException("queu is empty"); }
    }
    
    public Queue()
	{
		head = null;
		tail = null;	
	}
    public Queue(int[] mas)
    {
        head = null;
        tail = null;
        foreach ( int el in mas)
        {
            Node node = new Node(el);
            if (head is null)
                head = node;
            if  (tail is null)
                tail = node;
            else
            {
                tail.nx = node;
                tail = node;
            }
        }
    }

    public Queue(object obj)
    {
        Queue other;
        if (obj is not Queue)
        {
            throw new ArgumentException("obj must be Queue");
        }
        else
        {
            other = (Queue)obj;
        }
        head = null;
        tail = null;

        Node? cur = other.head;
        while (cur is not null)
        {
            Node node = new Node(cur.val);
            if (head is null)
                head = node;
            if  (tail is null)
                tail = node;
            else
            {
                tail.nx = node;
                tail = node;
            }

            cur = cur.nx;
        }
    }

    public override string ToString()
    {
        string s = "(";

        Node? cur = head;
        while (cur is not null)
        {
            s += cur.val.ToString();
            if (cur.nx is not null)
                s += " ";
            cur = cur.nx;
        }

        s += ")";
        return s;
    }

    public bool IsEqual(object obj)
    {
        Queue other;
        if (obj is not Queue)
        {
            throw new ArgumentException("obj must be Queue");
        }
        else
        {
            other = (Queue)obj;
        }

        Node? cur1 = head;
        Node? cur2 = other.head;
        while (cur1 is not null && cur2 is not null)
        {
            if (cur1.val != cur2.val)
            {
                return false;
            }

            cur1 = cur1.nx;
            cur2 = cur2.nx;
        }

        if (cur1 is not null || cur2 is not null)
            return false;

        return true;
    }
    public int Top()
    {
        if (head is null)
        {
            throw new InvalidOperationException("queue is empty");
        }
        return head.val;
    }

    public int Pop()
    {
        if (head is null)
        {
            throw new InvalidOperationException("queue is empty");
        }
        Node tmp = head;
        if (tail == head)
        {
            tail = tail.nx;
        }
        head = head.nx;
        tmp.nx = null;
        return tmp.val;
    }

    public void Push(int value)
    {

        Node node = new Node(value);
        
        if (head is null)
            head = node;
        if  (tail is null)
            tail = node;
        else
        {
            tail.nx = node;
            tail = node;
        }
    }
}
