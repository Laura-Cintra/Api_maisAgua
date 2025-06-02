using maisAgua.Domain.Persistence.Readings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maisAgua.Infrastructure.Mappings
{
    public class ReadingMapping : IEntityTypeConfiguration<Reading>
    {
        public void Configure(EntityTypeBuilder<Reading> builder)
        {
            builder.ToTable("tbl_leituras");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.LevelPct)
                .IsRequired()
                .HasColumnName("nivel_pct");

            builder.Property(r => r.TurbidityNtu)
                .IsRequired()
                .HasColumnName("turbidez_ntu");

            builder.Property(r => r.PhLevel)
                .IsRequired()
                .HasColumnName("ph_int");

            builder.Property(r => r.ReadingDatetime)
                .IsRequired()
                .HasColumnName("data_hora_leitura");

            builder.Property(r => r.IdDevice)
                .IsRequired()
                .HasColumnName("id_sensor");

            builder.HasOne(r => r.AssociatedDevice)
                .WithMany(d => d.Readings)
                .HasForeignKey(r => r.IdDevice)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
