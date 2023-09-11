using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedDatabaseAndORM_FinalAssesment.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(250, ErrorMessage = "Title cannot exceed 250 characters.")] //  length of max 250 
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters.")] 
        public string Title { get; set; }

        [Required(ErrorMessage = "Date and Time of Creation are required.")]
        [Display(Name = "Date of Creation")] // for human-readability
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime DateOfCreation { get; set; }

       
        [StringLength(1000, ErrorMessage = "The Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        // [NotMapped]
        [Required(ErrorMessage = "The Priority field is required.")]
        public Priority Priority { get; set; }
       

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        public int TodoListId { get; set; }

        public TodoList TodoList { get; set; }
       

    }
}
public enum Priority
{
    High,
    Medium,
    Low
}