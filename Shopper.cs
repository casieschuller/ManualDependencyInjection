using System;

namespace ManualDependencyInjection
{
    public class Shopper
    {
        private readonly ICreditCard creditCard;

        //Constructor here is taking in it's dependency and setting it to it's own internal variable for cc

        public Shopper(ICreditCard creditCard)
        {
            this.creditCard = creditCard;
        }

        public void Charge()
        {
            //created the charge method on the ICreditCard interface
            var chargeMessage = creditCard.Charge();
            Console.WriteLine(chargeMessage);
        }
    }
    
}
