using System;

namespace ManualDependencyInjection
{
    public class MasterCard : ICreditCard
    {
        public string Charge()
        {
            return "Swiping the MasterCard!";
        }
    }
}