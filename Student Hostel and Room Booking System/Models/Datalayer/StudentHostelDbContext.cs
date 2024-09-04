using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Student_Hostel_and_Room_Booking_System.Models.Datalayer
{
    public partial class StudentHostelDBContext : DbContext
    {
        public StudentHostelDBContext()
        {
        }

        public StudentHostelDBContext(DbContextOptions<StudentHostelDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Hostels> Hostels { get; set; }
        public virtual DbSet<RoomCoordinator> RoomCoordinator { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Students> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=Devs; Database=StudentHostelDB; Trusted_Connection=True; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK__Bookings__73951AEDB3A9F519");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.CheckInDate).HasColumnType("datetime");

                entity.Property(e => e.CheckOutDate).HasColumnType("datetime");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Bookings__RoomId__403A8C7D");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Bookings__Studen__3F466844");
            });

            modelBuilder.Entity<Hostels>(entity =>
            {
                entity.HasKey(e => e.HostelId)
                    .HasName("PK__Hostels__677EEB280F08782E");

                entity.Property(e => e.HostelName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<RoomCoordinator>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__RoomCoor__536C85E4EFCFE68C")
                    .IsUnique();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PK__Rooms__32863939376878A8");

                entity.Property(e => e.AvailableBedSpaces).HasDefaultValueSql("((4))");

                entity.Property(e => e.BedSpaces).HasDefaultValueSql("((4))");

                entity.Property(e => e.RoomNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.RoomType).HasMaxLength(50);

                entity.HasOne(d => d.Hostel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HostelId)
                    .HasConstraintName("FK__Rooms__HostelId__3C69FB99");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("PK__Students__32C52B99CA560F95");

                entity.HasIndex(e => e.MatricNo)
                    .HasName("UQ__Students__8E55BF888B1A263A")
                    .IsUnique();

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.JambRegNo).HasMaxLength(15);

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MatricNo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
