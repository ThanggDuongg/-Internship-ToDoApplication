using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TodoApplication.Entities
{
    //[Index(nameof(Email), IsUnique = true)]
    public class UserEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }

        //public string? Email { get; set; } /*= null;*/
        [MaxLength(20)]
        [Column("username")]
        public string? Username { get; set; } /*= null;*/

        [MaxLength(20)]
        [Column("password")]
        public string? Password { get; set; } /*= null;*/

        [JsonIgnore]
        public virtual ICollection<TaskEntity>? TaskEntities { get; set; } /*= null;*/
    }
}
