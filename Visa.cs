using System;

namespace ManualDependencyInjection
{
    public class Visa : ICreditCard
    {
        public string Charge()
        {
            return "Swiping the Visa!";
        }
    }
}