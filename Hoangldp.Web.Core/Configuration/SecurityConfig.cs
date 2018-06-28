namespace Hoangldp.Web.Core.Configuration
{
    /// <summary>
    /// Security config
    /// </summary>
    public class SecurityConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether all pages will be forced to use SSL (no matter of a specified [HttpsRequirementAttribute] attribute)
        /// </summary>
        public bool ForceSslForAllPages { get; set; }
    }
}
