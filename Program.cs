using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ManualDependencyInjection
{
    partial class Program
    {

        static void Main(string[] args)
        {
           
            
            ////Shopper requires a credit card that implements/of type ICreditCard.  In this case, we're instantiating an instance of a master card which does implement ICreditCard
            ////and then passing that credig card to the constructor for Shopper.
            //ICreditCard creditCard = new MasterCard();
            //ICreditCard otherCreditCard = new Visa();
            //We have done dependendy injection because the constructor for Shopper takes in the dependency it requires and because shopper (lower level class) depends on ICreditCard, and ICreditCard doesn't depend on Shopper.  It doesn't know about shopper.  And it is NOT dependent on master card, only on the ICreditCard interface.
            //var shopper = new Shopper(creditCard);
            ////Then we call the charge method in the shopper class, which calls the charge method on the creditcard object it was passed.  In this case, it was a master card, so it will run the charge method in the mastercard class, which returns "Swiping the MasterCard!";
            //shopper.Charge();  


            ////Then, if we want to do Visa:
            //var shopper2 = new Shopper(otherCreditCard);
            //shopper2.Charge();

            //Console.Read();

            ////All of the above works, but it's not ideal because we have to decide before we instantiate our shopper what the concrete type is going to be.  It's not very
            ////configurable and it's not very smart.  And do we really want to resolve our dependency right before we use it in the Shopper constructor?  It kind of seems silly
            ////because shopper doesn't depend on the concrete implementation.  So let's get closer to a container.

            //Resolver resolver = new Resolver();
            //var shopper = new Shopper(resolver.ResolveCreditCard());//since we don't want to pass in a concrete implementation, let's pass in a resolver, this method will resolve the credit card for us.

            //shopper.Charge();
            //Console.Read();

            ////All of the above works but we want to use a contaoiner because we don't want to have to have resolver.ResolveCreditCard() all over the place.
            Resolver resolver = new Resolver();
            resolver.Register<Shopper, Shopper>(); //When we ask for a shoppper, give us a shopper.
            resolver.Register<ICreditCard, MasterCard>();
            resolver.Register<ICreditCard, Visa>();//If you want it to be a visa, run this line instead of the above.
            var shopper = resolver.Resolve<Shopper>(); //we don't want to pass anything in here.  We want the container to resolve the entire chain.  The idea is, we'll tell you the type <Shopper>, and the container will know that it needs a credit card (because the shopper constructor requires an ICreditCard and then it will look to see what type is registered, and it will find that it's a mastercard, and it will resolve the master card.
            shopper.Charge();
            Console.Read();
        }
    }
    
}
