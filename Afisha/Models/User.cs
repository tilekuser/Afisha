using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Afisha.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }
        public string DateOfBirth { get; set; } 

    }
}
