using LettercubedApi.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LettercubedApi.Models
{
    [PrimaryKey(nameof(Id))]
    public class PromotionsLogs
    {
       
        public Guid Id { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string AdminId { get; set; }

        [ForeignKey(nameof(AppUser))]       
        public string UserId { get; set; }

        public string Promotion   { get; set; }
    }
}
