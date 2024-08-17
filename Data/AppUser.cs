using LettercubedApi.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LettercubedApi.Data
{
    public class AppUser:IdentityUser
    {
        public ICollection<Movie> MoviesByUser { get; set; }

        public ICollection<Reviews> ReviewsByUser { get; set; }

        
       

    }
}
