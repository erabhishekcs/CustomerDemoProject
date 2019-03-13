using MiddleLayer;
using InterfaceCustomer;
using ValidationAlgorithms;
using Unity;
using Unity.Injection;

namespace FactoryCustomer
{
    public static class Factory<AnyType> //Generic Simple factory pattern
    {
        private static IUnityContainer ObjectsofOurProjects = null; // Unity container

        public static AnyType Create(string Type)
        {
            // Lazy loading.
            if (ObjectsofOurProjects == null)
            {
                ObjectsofOurProjects = new UnityContainer();

                IValidation<ICustomer> custValidation = new PhoneValidation(
                                                        new CustomerBasicValidation());

                // Registering in unity container
                ObjectsofOurProjects.RegisterType<CustomerBase, Customer>
                                    ("Lead"
                                    , new InjectionConstructor(
                                        custValidation, "Lead"));

                custValidation = new PhoneValidation(
                                 new CustomerBillValidation(
                                 new CustomerAddressValidation(
                                   new CustomerBasicValidation())));
                ObjectsofOurProjects.RegisterType<CustomerBase, Customer>
                                    ("Customer"
                                    , new InjectionConstructor(
                                        custValidation, "Customer"));
            }
            //RIP Replace If with Poly
            return ObjectsofOurProjects.Resolve<AnyType>(Type);
        }
    }
}
