using maisAgua.Domain.Device;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maisAgua.Infrastructure.Mappings
{
    public class DeviceMapping : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("tbl_sensores");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasColumnName("nome_dispositivo")
                .HasMaxLength(50);

            builder.HasIndex(d => d.Name)
                .IsUnique();

            builder.Property(d => d.InstallationDate)
                .IsRequired()
                .HasColumnName("data_hora");

            builder.HasMany(d => d.Readings)
                .WithOne(r => r.Device)
                .HasForeignKey(r => r.IdDevice);
        }
    }
}
