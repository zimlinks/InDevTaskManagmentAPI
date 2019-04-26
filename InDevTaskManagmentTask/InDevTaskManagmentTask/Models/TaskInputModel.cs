using System.ComponentModel.DataAnnotations;

namespace InDevTaskManagmentTask.Models
{
    public class TaskInputModel
    {
        public int Id { get; set; }

        [Required]
        public string TaskName { get; set; }

        [Required]
        public string AssignedTo { get; set; }

        [Required]
        public string Assignedby { get; set; }

        [Required]
        public int ProjectMembers { get; set; }

        [Required]
        public string TaskDetails { get; set; }
    }
}
