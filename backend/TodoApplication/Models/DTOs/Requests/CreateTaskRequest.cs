using System.ComponentModel.DataAnnotations;
using TodoApplication.Constants;
using TodoApplication.Enums;

namespace TodoApplication.Models.DTOs.Requests
{
    public record CreateTaskRequest
    {
        [Required]
        public string? Name { get; set; } /*= null;*/

        public string? Description { get; set; } /*= null;*/

        public string? Status { get; set; } = TaskStatusConstant.UNCOMPLETED;

        [Required]
        public DateTime? CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
