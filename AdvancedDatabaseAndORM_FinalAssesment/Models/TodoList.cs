using System.ComponentModel.DataAnnotations;

namespace AdvancedDatabaseAndORM_FinalAssesment.Models
{
    public class TodoList
    {
        public int Id { get; set; }  //As it is a primary key, 'Required' is not needed as it is automatically recognized .

        [Required(ErrorMessage = "Title is required.")] // its kind of client-side validation(also server-side) as its provide a error msg to users before senting it to server
        // which makes the users to correct and follow the requirements. essentials for security and data intregrity
        [StringLength(250, ErrorMessage = "Title cannot exceed 250 characters.")] //  length of max 250
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters.")] // and min 3 
        public string Title { get; set; }

        [Required(ErrorMessage = "Date and Time of Creation are required.")]
        [Display(Name = "Date of Creation")] // UI displays date of Creation instead of DateofCreation which makes it more user friendly 
        public DateTime DateOfCreation { get; set; }

        public HashSet<TodoItem> TodoItems { get; set; } // A collection of TodoItems associated with todoList
    }
}
