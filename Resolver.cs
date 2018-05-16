using System;
using System.Collections.Generic;
using System.Linq;

namespace ManualDependencyInjection
{
    partial class Program
    {
        public class Resolver
        {

            //We need is something to hold some types that get registered.  We need to register some types they're gonna tell us what the interface/type is and what type to resolve that as so that we can get all the types mapped correctly.
            //So we're going to create a dictionary that will map types (say, ICreditCard and Visa and ICreditCard and MasterCard.  The below line defines the dictionary and "news it up" in one line.
            private Dictionary<Type, Type> dependencyMap = new Dictionary<Type, Type>();

            public T Resolve<T>() //return whatever type they're asking for     
            {
                return (T)Resolve(typeof (T));//this one needs to use a real type, not the generic type <T> because we're going to need to find the parameters for the constructor and things like that.  "typeof" gets the type of T.  The "(T)" in front is doing a cast
            }

            private object Resolve(Type typeToResolve)
            {
                Type resolvedType = null;
                try
                {
                    resolvedType = dependencyMap[typeToResolve]; //Is the typeToResolve that we've been passed in our dictionary?  For example, let's say we passed in ICreditCard and it was mapped to MasterCard.  OK, we return MasterCard.
                }
                catch
                {
                    throw new Exception(string.Format("Could not resole type {0}",typeToResolve.FullName));
                }
                //now, here, in order for us to create this type, if it has no constructor, we're just going to create a new instance of the type.  
                //If it has a constructor that has parameters, we have to resolve each parameter and then invoke the constructor with the parameters and then return that back.

                //let's grab the first constructor
                var firstConstructor = resolvedType.GetConstructors().First();  //Here, we get the constructor of MasterCard.  MasterCard has a parameterless constructor, so we will just create and return a mastercard below.
                var constructorParameters = firstConstructor.GetParameters();//gives us parameters that the constructor takes.  
                //If there aren't any, simple
                if (constructorParameters.Count() == 0)
                    return Activator.CreateInstance(resolvedType);//reflection way of creating a type.  Remember we're getting the type, not the class, it's not an object, it's the type of the class.

                IList<object> parameters = new List<object>();
                foreach (var parameterToResolve in constructorParameters)//loop through each constructor parameter
                {
                    parameters.Add(Resolve(parameterToResolve.ParameterType)); // resolve all types of all parameters required for all non parameterless constructors.  This will chain out like a tree until we get to the root nodes where they don't have any parameters in the constructors and then it will hit the activate code and activate them.
                }

                //Once we have the list, create our object with the list.

                return firstConstructor.Invoke(parameters.ToArray()); // Invoke requires an array, but parameters is a list, so we just .ToArray() it.
            }

            public void Register<TFrom, TTo>()
            {
                dependencyMap.Add(typeof(TFrom), typeof(TTo));
            }

            public ICreditCard ResolveCreditCard()
            {
                if (new Random().Next(2) == 1)//means give me a random value less than 2
                    return new Visa();

                return new MasterCard();

            }
        }
    }
    
}
