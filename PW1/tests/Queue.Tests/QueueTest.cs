namespace Queue.Tests;

using Queue = global::Queue_Data_Structure.Queue;

public class QueueTest
{
    [Fact]
    public void Test_Default_Constructor()
    {
        Queue q = new Queue();


        Assert.Throws<InvalidOperationException>(() => q.Head);
        Assert.Throws<InvalidOperationException>(() => q.Tail);

    }
    [Fact]
    public void Test_Constructor_With_Array()
    {
        int[] arr = new int[] { 1, 2, 3 };
        Queue q = new Queue(arr);
        
        int? h = q.Head, t = q.Tail;

        Assert.Equal(1, h);
        Assert.Equal(3, t);
    }
    [Fact]
    public void Test_Constructor_With_Empty_Array()
    {
        int[] arr = new int[] {};
        Queue q = new Queue(arr);
        
        Assert.Throws<InvalidOperationException>(() => q.Head);
        Assert.Throws<InvalidOperationException>(() => q.Tail);
    }
    [Fact]
    public void Test_Copy_Constructor()
    {
        int[] arr = new int[] { 3, 4, 5 };
        Queue q = new Queue(arr);
        Queue copy = q;
        
        int? h = copy.Head, t = copy.Tail;
        
        Assert.Equal(3, h);
        Assert.Equal(5, t);
    }
    [Fact]
    public void Test_Independency_Of_Copy_From_Original()
    {
        int[] arr = new int[] { 3, 4, 5 };
        Queue q = new Queue(arr);
        Queue copy = new Queue(q);

        q.Pop();
        int? h = copy.Head;
        
        Assert.Equal(3, h);
    }
    [Fact]
    public void Test_Head_Access()
    {
        int[] arr = new int[] { 1, 2 };
        Queue q = new Queue(arr);
        
        int? h = q.Head;

        Assert.Equal(1, h);

    }
    [Fact]
    public void Test_Tail_Access()
    {
        int[] arr = new int[] { 1, 2 };
        Queue q = new Queue(arr);
        
        int? t = q.Tail;
        
        Assert.Equal(2, t);
    }
    [Fact]
    public void Test_Top()
    {
        int[] arr = new int[] { 1, 2 };
        Queue q = new Queue(arr);

        int? el = q.Top();
        int? h = q.Head;

        Assert.Equal(1, h);

    }[Fact]
    public void Test_Pop()
    {
        int[] arr = new int[] { 1, 2 };
        Queue q = new Queue(arr);

        int? el = q.Pop();

        Assert.Equal(1, el);
    }
    [Fact]
    public void Test_Head_After_Pop()
    {
        int[] arr = new int[] { 1, 2 };
        Queue q = new Queue(arr);

        int? el = q.Pop();
        int? h = q.Head;

        Assert.Equal(2, h);
    }
    [Fact]
    public void Test_Head_Tail_After_Pop_Of_Last_Element()
    {
        int[] arr = new int[] { 1 };
        Queue q = new Queue(arr);

        int? el = q.Pop();
        
        Assert.Throws<InvalidOperationException>(() => q.Head);
        Assert.Throws<InvalidOperationException>(() => q.Tail);
    }
    [Fact]
    public void Test_Top_From_Empty_QUeue()
    {
        int[] arr = new int[] {};
        Queue q = new Queue(arr);
        
        Assert.Throws<InvalidOperationException>(() => q.Top());
    }
    [Fact]
    public void Test_Pop_From_Empty_QUeue()
    {
        int[] arr = new int[] {};
        Queue q = new Queue(arr);
        
        Assert.Throws<InvalidOperationException>(() => q.Pop());
    }
    [Fact]
    public void Test_Push_To_Empty_Queue()
    {
        int[] arr = new int[] { };
        Queue q = new Queue(arr);

        q.Push(7);
        int? h = q.Head, t = q.Tail;

        Assert.Equal(7, h);
        Assert.Equal(7, t);
    }
    [Fact]
    public void Test_Push_To_Not_Empty_Queue()
    {
        int[] arr = new int[] { 2 };
        Queue q = new Queue(arr);

        q.Push(3);
        int? h = q.Head, t = q.Tail;

        Assert.Equal(2, h);
        Assert.Equal(3, t);
    }
    [Fact]
    public void Test_Multiple_Push()
    {
        int[] arr = new int[] {  };
        Queue q = new Queue(arr);

        q.Push(1);
        q.Push(2);
        q.Push(3);
        int? h = q.Head, t = q.Tail;

        Assert.Equal(1, h);
        Assert.Equal(3, t);
    }
    [Fact]
    public void Test_Order_Of_elements_In_Queue()
    {
        int[] arr = new int[] {  };
        Queue q = new Queue(arr);

        q.Push(1);
        q.Push(2);
        q.Push(3);
        int? el1 = q.Pop(), el2 = q.Pop(), el3 = q.Pop();

        Assert.Equal(1, el1);
        Assert.Equal(2, el2);
        Assert.Equal(3, el3);
    }
    [Fact]
    public void Test_ToString()
    {
        int[] arr = new int[] { 1, 2, 3 };
        Queue q = new Queue(arr);

        string s = q.ToString();

        Assert.Equal("(1 2 3)", s);
    }
    [Fact]
    public void Test_Equality_Of_Queues()
    {
        int[] arr = new int[] { 1, 2, 3 };
        Queue q1 = new Queue(arr), q2 = new Queue(arr);

        bool isEq = q1.IsEqual(q2);

        Assert.True(isEq);
    }
    [Fact]
    public void Test_Difference_Of_Quesues_With_Different_Elements()
    {
        int[] arr1 = new int[] { 1, 2, 3 }, arr2 = new int[] { 3, 2, 1,};
        Queue q1 = new Queue(arr1), q2 = new Queue(arr2);

        bool isEq = q1.IsEqual(q2);

        Assert.False(isEq);
    }
    [Fact]
    public void Test_Difference_Of_Quesues_With_Different_Length()
    {
        int[] arr1 = new int[] { 1, 2, 3 }, arr2 = new int[] { 3, 2, 1, 0 };
        Queue q1 = new Queue(arr1), q2 = new Queue(arr2);

        bool isEq = q1.IsEqual(q2);

        Assert.False(isEq);
    }
    

}
