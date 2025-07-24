
public class RentedCar : IDisposable
{
    public string Name { get; }
    private bool _isDisposed = false;

    public RentedCar(string name)
    {
        Name = name;
        Console.WriteLine($"   [{Name}] we've picked up the keys. The car is now our responsibility!");
    }

    public void Drive()
    {
        if (_isDisposed)
        {
            // This prevents using the object after it has been cleaned up.
            throw new ObjectDisposedException("The car has already been returned!");
        }
        Console.WriteLine($"   [{Name}] Driving around town...");
    }

    // This is the IDisposable pattern. It's the primary, explicit way to clean up resources.
    public void Dispose()
    {
        // Call the internal cleanup method. The 'true' signifies it was called by a user (deterministically).
        Cleanup(true);

        // This is a crucial optimization. It tells the Garbage Collector that we have already
        // cleaned up, so it doesn't need to waste time calling the Finalizer.
        GC.SuppressFinalize(this);
    }

    // This is a destructor
    // It's a SAFETY NET. It's only called by the Garbage Collector if we FORGOT to call Dispose().
    // It is non-deterministic - we don't know WHEN it will be called. It's a last resort.
    ~RentedCar()
    {        
        Console.WriteLine($"   [{Name}] DANGER! The repo man had to come get this abandoned car! (Finalizer called)");

        // Call the internal cleanup method. The 'false' signifies it was called by the GC (non-deterministically).
        Cleanup(false);
    }

    // A centralized cleanup method to avoid code duplication.
    protected virtual void Cleanup(bool disposing)
    {
        // The 'if' check prevents trying to clean up more than once.
        if (!_isDisposed)
        {
            if (disposing)
            {
                // --- Clean up MANAGED resources here ---
                // (e.g., other objects that implement IDisposable)
                // In this example, we have none.
                Console.WriteLine($"   [{Name}] Returning the keys. Cleaning the car. Resource released properly! (Dispose called automatically b'coz of using statement)");
            }

            // --- Clean up UNMANAGED resources here ---
            // (e.g., file handles, database connections, pointers)
            // This part runs whether called from Dispose() or the Finalizer.

            _isDisposed = true;
        }
    }
}
