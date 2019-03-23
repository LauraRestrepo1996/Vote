
namespace Vote.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //  public string Ocupattion { get; set; }

        // public string stratum { get; set; }

        // public string gender { get; set; }

    }
}
