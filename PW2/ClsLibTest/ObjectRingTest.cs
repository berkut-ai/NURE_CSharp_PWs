using ClsLib;

namespace ClsLibTest;

public class ObjectRingTest
{
    [Fact]
    public void Test_Empty_Constructor()
    {
        ObjectRing ring = new ObjectRing();

        string res = ring.ToString();

        Assert.Equal("[]", res);
    }

    [Fact]
    public void Test_Head_When_Ring_Is_Empty()
    {
        ObjectRing ring = new ObjectRing();

        Assert.Throws<InvalidOperationException>(() => { int head = ring.Head; });
    }

    [Fact]
    public void Test_Top_When_Ring_Is_Empty()
    {
        ObjectRing ring = new ObjectRing();

        Assert.Throws<InvalidOperationException>(() => ring.Top());
    }

    [Fact]
    public void Test_Constructor_With_Array()
    {
        int[] arr = new int[] { 4, 7, 2 };
        ObjectRing ring = new ObjectRing(arr);

        string res = ring.ToString();

        Assert.Equal("[4, 7, 2]", res);
    }

    [Fact]
    public void Test_Top_Returns_First_Element()
    {
        ObjectRing ring = new ObjectRing(new int[] { 4, 7, 2 });

        int res = ring.Top();

        Assert.Equal(4, res);
    }

    [Fact]
    public void Test_Copy_Constructor()
    {
        ObjectRing original = new ObjectRing(new int[] { 1, 3, 5 });

        ObjectRing copy = new ObjectRing(original);

        Assert.Equal("[1, 3, 5]", copy.ToString());
    }

    [Fact]
    public void Test_Copy_Constructor_Creates_Independent_Ring()
    {
        ObjectRing original = new ObjectRing(new int[] { 1, 3, 5 });
        ObjectRing copy = new ObjectRing(original);
        TextReader originalInput = Console.In;
        Console.SetIn(new StringReader("2\n8\n9\n"));

        try
        {
            original.Read();
        }
        finally
        {
            Console.SetIn(originalInput);
        }

        Assert.Equal("[8, 9]", original.ToString());
        Assert.Equal("[1, 3, 5]", copy.ToString());
    }

    [Fact]
    public void Test_Copy_Constructor_With_Invalid_Argument()
    {
        object val = new object();

        Assert.Throws<ArgumentException>(() => new ObjectRing(val));
    }

    [Fact]
    public void Test_Read_Replaces_Existing_Elements()
    {
        ObjectRing ring = new ObjectRing(new int[] { 1, 2, 3 });
        TextReader originalInput = Console.In;
        Console.SetIn(new StringReader("2\n8\n9\n"));

        try
        {
            ring.Read();
        }
        finally
        {
            Console.SetIn(originalInput);
        }

        Assert.Equal("[8, 9]", ring.ToString());
    }

    [Fact]
    public void Test_Read_With_Zero_Elements()
    {
        ObjectRing ring = new ObjectRing(new int[] { 1, 2, 3 });
        TextReader originalInput = Console.In;
        Console.SetIn(new StringReader("0\n"));

        try
        {
            ring.Read();
        }
        finally
        {
            Console.SetIn(originalInput);
        }

        Assert.Equal("[]", ring.ToString());
    }

    [Fact]
    public void Test_Implicit_Conversion_From_Array()
    {
        int[] arr = new int[] { 6, 2 };

        ObjectRing ring = arr;

        Assert.Equal("[6, 2]", ring.ToString());
    }

    [Fact]
    public void Test_Implicit_Conversion_To_String()
    {
        ObjectRing ring = new ObjectRing(new int[] { 6, 2 });

        string res = ring;

        Assert.Equal("[6, 2]", res);
    }

    [Fact]
    public void Test_Equals_With_Matching_Contents()
    {
        ObjectRing fir = new ObjectRing(new int[] { 1, 2, 3 });
        ObjectRing sec = new ObjectRing(new int[] { 1, 2, 3 });

        bool res = fir.Equals(sec);

        Assert.True(res);
    }

    [Fact]
    public void Test_Equals_With_Different_Contents()
    {
        ObjectRing first = new ObjectRing(new int[] { 1, 2, 3 });
        ObjectRing differentValue = new ObjectRing(new int[] { 1, 2, 4 });
        ObjectRing differentLength = new ObjectRing(new int[] { 1, 2 });

        bool valuesResult = first.Equals(differentValue);
        bool lengthResult = first.Equals(differentLength);

        Assert.False(valuesResult);
        Assert.False(lengthResult);
    }

    [Fact]
    public void Test_Equality_Operators()
    {
        ObjectRing first = new ObjectRing(new int[] { 1, 2 });
        ObjectRing equal = new ObjectRing(new int[] { 1, 2 });
        ObjectRing different = new ObjectRing(new int[] { 2, 1 });
        ObjectRing? missing = null;

        bool equalResult = first == equal;
        bool notEqualResult = first != different;
        bool nullEqualityResult = first == missing;
        bool nullInequalityResult = first != missing;

        Assert.True(equalResult);
        Assert.True(notEqualResult);
        Assert.False(nullEqualityResult);
        Assert.True(nullInequalityResult);
    }

    [Fact]
    public void Test_True_And_False_Operators()
    {
        ObjectRing populated = new ObjectRing(new int[] { 1 });
        ObjectRing empty = new ObjectRing();

        bool populatedResult = populated ? true : false;
        bool emptyResult = empty ? true : false;

        Assert.True(populatedResult);
        Assert.False(emptyResult);
    }

    [Fact]
    public void Test_Increment_Operator()
    {
        ObjectRing ring = new ObjectRing(new int[] { 1, 2, 3 });

        ring++;

        Assert.Equal("[2, 3, 1]", ring.ToString());
    }

    [Fact]
    public void Test_Decrement_Operator()
    {
        ObjectRing ring = new ObjectRing(new int[] { 1, 2, 3 });

        ring--;

        Assert.Equal("[3, 1, 2]", ring.ToString());
    }
}
