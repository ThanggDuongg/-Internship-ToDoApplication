using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoApplication.Enums;

namespace TodoApplication.Entities
{
    public class TaskEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; init; }

        [Column("name")]
        [MaxLength(20)]
        public string? Name { get; set; } /*= null;*/

        [Column("description")]
        [MaxLength(50)]
        public string? Description { get; set; } /*= null;*/

        [Column("status")]
        [MaxLength(14)]  
        public string? Status { get; set; }

        [Column("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("dueDate")]
        public DateTime? DueDate { get; set; }

        // public Guid UserId { get; set; }
        [ForeignKey("userId")]
        public UserEntity? userEntity { get; set; } 
    }
}
