using InterfaceCustomer;
namespace MiddleLayer
{
    public class Customer : CustomerBase
    {
        public Customer(IValidation<ICustomer> obj, string _CustType) : base(obj)
        {
            CustomerType = _CustType;
        }
    }
}
