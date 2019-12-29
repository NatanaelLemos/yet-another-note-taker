using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Data.Mappers
{
    public class NoteMapper
    {
        public NoteMapper(EntityTypeBuilder<Note> modelBuilder)
        {
            modelBuilder.HasKey(n => n.Id);

            modelBuilder
                .Property(n => n.Name)
                .HasMaxLength(255)
                .IsRequired(true);

            modelBuilder
                .HasOne(no => no.Notebook)
                .WithMany(n => n.Notes)
                .HasForeignKey(no => no.NotebookId);
        }
    }
}
