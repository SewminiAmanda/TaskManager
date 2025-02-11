using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskItem
    {
        [Key]  // Ensures TaskID is the Primary Key
        public int TaskID { get; set; }

        [Required]  // Ensures Title is required
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
