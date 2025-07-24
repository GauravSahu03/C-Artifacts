public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- C# Resource Management Demo ---");

        Console.WriteLine("\nScenario 1: The responsible person. Using the 'using' statement.");
        // The 'using' statement guarantees that Dispose() is called, even if errors occur.
        // It's syntactic sugar for a try/finally block.
        using (var rental = new RentedCar("Slavia"))
        {
            rental.Drive();
            Console.WriteLine("Finished my trip with the 'using' statement.");
        } // <-- The compiler automatically calls rental.Dispose() here.
        Console.WriteLine("-----------------------------------");


        Console.WriteLine("\nScenario 2: The forgetful person. No using, no Dispose().");
        var abandonedRental = new RentedCar("Fortuner");
        abandonedRental.Drive();
        Console.WriteLine("I'm done with this car, I'll just leave it here...");
        abandonedRental = null; // Let the object go out of scope.

        Console.WriteLine("\nForcing a Garbage Collection to see if the Finalizer runs (shouldn't be done in the reality!).");
        GC.Collect();
        GC.WaitForPendingFinalizers(); // Waits for the finalizer thread to finish.
        Console.WriteLine("-----------------------------------");
    }
}
