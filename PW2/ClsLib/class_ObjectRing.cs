using System;

namespace ClsLib;

public class ObjectRing : IObjectRing
{
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

    public int Head
    {
        get { return head?.val ?? throw new InvalidOperationException("queu is empty"); }
    }
    
    public ObjectRing()
    {
        head = null;
    }

    public ObjectRing(int[] arr)
    {
        head = null;
        Node? cur = null;
        foreach (int el in arr)
        {
            Node node = new Node(el);
            if (head is null)
            {
                head = node;
                cur = node;
            }
            else
            {
                cur.nx = node;
                cur = node;
            }
            
        }
    }

    public ObjectRing(object obj)
    {
        if (obj is not ObjectRing other)
        {
            throw new ArgumentException("obj must be ObjectRing");
        }

        head = null;
        Node? cur = null;

        Node? ocur = other.head;
        while (ocur is not null)
        {
            Node node = new Node(ocur.val);
            if (head is null)
            {
                head = node;
                cur = node;
            }
            else
            {
                cur.nx = node;
                cur = node;
            }

            ocur = ocur.nx;
        }

    }

    

    public void Read()
    {
        head = null;
        Node? cur = null;
        int n = int.Parse(Console.ReadLine());
        
        
        for (int i = 0; i < n; i++)
        {
            int x = int.Parse(Console.ReadLine());
            Node node = new Node(x);
            if (head is null)
            {
                head = node;
                cur = node;
            }
            else
            {
                cur.nx = node;
                cur = node;
            }
            
        }
        
    }
    
    public string ToString()
    {
        string ans = "[";
        Node? cur = head;

        while (cur is not null)
        {
            ans += cur.val.ToString();
            if (cur.nx is not null)
            {
                ans += ", ";
            }

            cur = cur.nx;
        }
        ans += "]";
        return ans;
    }
    
    public int Top()
    {
        if (head is null)
        {
            throw new InvalidOperationException("ring is empty");
        }
        return head.val;
    }
    
    public static bool operator >(ObjectRing ring, int x)
    {
        if (ring.head is null)
        {
            throw new InvalidOperationException("ring is empty");
        }
        x = ring.head.val;
        ring.head = ring.head.nx;
        return true;
    }
    public static bool operator <(ObjectRing ring, int x)
    {
        Node? cur = ring.head;

        while (cur.nx is not null)
        {
            cur = cur.nx;
        }

        cur.nx = new Node(x);
        return true;
    }

    public void operator ++()
    {
        if (head is null || head.nx is null)
            return;
        
        Node? node = head;

        head = head.nx;
        node.nx = null;

        Node? cur = head;
        while (cur.nx is not null)
        {
            cur = cur.nx;
        }

        cur.nx = node;

    }

    public 
        void operator --()
    {
        if (head is null || head.nx is null)
            return;
        
        Node? cur = head;
        while (cur.nx.nx is not null)
        {
            cur = cur.nx;
        }

        Node? node = cur.nx;
        cur.nx = null;

        node.nx = head;
        head = node;
    }

    public static bool operator true(ObjectRing ring)
    {
        try
        {
            int? v = ring.Head;
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool operator false(ObjectRing ring)
    {
        if (ring)
            return false;
        else
            return true;

    }
    
    public static implicit operator ObjectRing(int[] arr)
    {
        return new ObjectRing(arr);
    }
    public static implicit operator string(ObjectRing ring)
    {
        return ring.ToString();
    }


    public override bool Equals(object? obj)
    {
        if (obj is not ObjectRing other)
            return false;
        Node? cur1 = head;
        Node? cur2 = other.head;

        while (cur1 is not null && cur2 is not null)
        {
            if (cur1.val != cur2.val)
                return false;

            cur1 = cur1.nx;
            cur2 = cur2.nx;
        }

        return cur1 is null && cur2 is null;
    }

    public static bool operator ==(ObjectRing? ring, ObjectRing?other)
    {
        if (ReferenceEquals(ring, other))
            return true;

        if (ring is null || other is null)
            return false;

        return ring.Equals(other);
    }
    
    public static bool operator !=(ObjectRing? ring, ObjectRing? other)
    {
        return !(ring == other);
    }
    
    
    
}
