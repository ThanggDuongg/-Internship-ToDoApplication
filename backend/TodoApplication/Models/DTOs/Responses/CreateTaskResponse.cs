using System.ComponentModel.DataAnnotations;
using TodoApplication.Constants;
using TodoApplication.Enums;

namespace TodoApplication.Models.DTOs.Responses
{
    public class CreateTaskResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } /*= null;*/

        public string? Description { get; set; } /*= null;*/

        public string? Status { get; set; } = TaskStatusConstant.UNCOMPLETED;

        public DateTime? CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
