namespace Hoangldp.Web.Core.Configuration
{
    public class CoreConfig
    {
        public string LibLoadingPattern { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether all pages will be forced to use SSL (no matter of a specified [HttpsRequirementAttribute] attribute)
        /// </summary>
        public bool ForceSslForAllPages { get; set; }
    }
}
