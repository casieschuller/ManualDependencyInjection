namespace ManualDependencyInjection
{
    public interface ICreditCard
    {
        //So now anyone implementing ICreditCard needs to implement the charge method that returns a string.
        string Charge();
    }
}