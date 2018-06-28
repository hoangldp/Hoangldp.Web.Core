namespace Hoangldp.Web.Core.Authentication
{
    public interface IUserService
    {
        IUser GetCustomerByUsername(string username);
    }
}
