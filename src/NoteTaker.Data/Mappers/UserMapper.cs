using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Data.Mappers
{
    public class UserMapper
    {
        public UserMapper(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.HasKey(m => m.Id);

            modelBuilder
                .Property(n => n.Email)
                .IsRequired(true)
                .HasMaxLength(255);

            modelBuilder
                .Property(n => n.Password)
                .IsRequired(true)
                .HasMaxLength(255);

            modelBuilder
                .HasOne(u => u.Settings)
                .WithOne(s => s.User);

            modelBuilder
                .HasMany(u => u.Notebooks)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId);
        }
    }
}
