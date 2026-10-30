using ClsLib;

public class Program
{
    private static void Main(string[] args)
    {
        ObjectRing emptyRing = new ObjectRing();
        Console.WriteLine($"Empty ring: {emptyRing}");

        int[] values = new int[] { 1, 2, 3, 4 };
        ObjectRing ring = values;
        Console.WriteLine($"Implicit from array: {ring.ToString()}");
        Console.WriteLine($"Top: {ring.Top()}");
        Console.WriteLine($"Head: {ring.Head}");

        
        ObjectRing copy = new ObjectRing(ring);
        Console.WriteLine($"Cop: {copy.ToString()}");
        Console.WriteLine($"== comparison: {ring == copy}");
        

        bool appended = ring < 5;
        Console.WriteLine($"After append 5: {appended}");

        ring++;
        Console.WriteLine($"After ++ : {ring.ToString()}");

        ring--;
        Console.WriteLine($"After -- : {ring.ToString()}");
        Console.WriteLine($"!= comparison with  copy: {ring != copy}");

        if (ring)
        {
            Console.WriteLine("The ring is not empty.");
        }

        if (emptyRing)
        {
            Console.WriteLine("The empty ring is not empty.");
        }
        else
        {
            Console.WriteLine("The empty ring is empty.");
        }

        string ringAsString = ring;
        Console.WriteLine($"Implicit conversion to string: {ringAsString}");

        Console.WriteLine("Enter the number of elements, then enter each element on a new line:");
        ring.Read();
        Console.WriteLine(ring.ToString());
    }
}
