using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Infrastructure.Configurations;

public class MeetingRoomConfiguration : IEntityTypeConfiguration<MeetingRoom>
{
    public void Configure(EntityTypeBuilder<MeetingRoom> builder)
    {
        builder.ToTable("MeetingRooms", "dbo");
        builder.Property(mr => mr.RoomId)
            .HasMaxLength(512);

        builder.Property(mr => mr.MeetUrl)
            .HasMaxLength(512);

        builder.HasIndex(mr => mr.MeetUrl)
            .IsUnique();

        builder.HasIndex(mr => mr.RoomId)
            .IsUnique();
    }
}