using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Data.Mappers
{
    public class NotebookMapper
    {
        public NotebookMapper(EntityTypeBuilder<Notebook> modelBuilder)
        {
            modelBuilder.HasKey(n => n.Id);

            modelBuilder
                .Property(n => n.Name)
                .HasMaxLength(255)
                .IsRequired(true);

            modelBuilder
                .HasMany(n => n.Notes)
                .WithOne(no => no.Notebook)
                .HasForeignKey(no => no.NotebookId);

            modelBuilder
                .HasOne(n => n.User)
                .WithMany(u => u.Notebooks)
                .HasForeignKey(n => n.UserId);
        }
    }
}
