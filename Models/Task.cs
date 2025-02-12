using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class TaskItem
    {
        [Key]  // Ensures TaskID is the Primary Key
        public int TaskID { get; set; }

        [Required]  // Ensures Title is required
        public string Title { get; set; }

        public string Description { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
}
