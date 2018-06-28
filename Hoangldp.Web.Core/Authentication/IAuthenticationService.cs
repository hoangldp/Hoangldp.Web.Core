namespace Hoangldp.Web.Core.Authentication
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        void SignIn(IUser user, bool isPersistent);

        /// <summary>
        /// Sign out
        /// </summary>
        void SignOut();

        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <returns>User</returns>
        IUser GetAuthenticatedUser();
    }
}
