using Microsoft.EntityFrameworkCore;
using TodoApplication.Entities;
using TodoApplication.Enums;

namespace TodoApplication
{
    public class TodoDBContext : DbContext
    {
        public TodoDBContext(DbContextOptions<TodoDBContext> options) : base(options) { }

        #region DbSet
        public virtual DbSet<TaskEntity>? TaskEntities { get; set; } /*= null;*/
        public virtual DbSet<UserEntity>? UserEntities { get; set; } /*= null;*/
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Username).IsRequired();
                entity.Property(u => u.Password).IsRequired();
            });

            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.ToTable("task");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.CreatedDate).HasDefaultValueSql("getutcdate()");
                entity.Property(t => t.Status).HasDefaultValue(TaskStatusEnum.UNCOMPLETED);
                entity.Property(t => t.CreatedDate).IsRequired();
                entity.Property(t => t.Name).IsRequired();
                //entity.HasOne(t => t.User).WithMany().HasForeignKey(u => u.Id).HasConstraintName("FK_task_user");
            });
        }
    }
}
