using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Data.Mappers
{
    public class SettingsMapper
    {
        public SettingsMapper(EntityTypeBuilder<Settings> modelBuilder)
        {
            modelBuilder.HasKey(m => m.Id);

            modelBuilder
                .Property(n => n.DarkMode)
                .IsRequired(true);

            modelBuilder
                .HasOne(s => s.User)
                .WithOne(u => u.Settings);
        }
    }
}
