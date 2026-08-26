using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebHotelSystem.Models;

namespace WebHotelSystem.Data;

public partial class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("apartments_pkey");

            entity.ToTable("apartments");

            entity.HasIndex(e => e.AppNumber, "apartments_app_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppNumber).HasColumnName("app_number");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.IsBalcon)
                .HasDefaultValue(false)
                .HasColumnName("is_balcon");
            entity.Property(e => e.IsFront)
                .HasDefaultValue(true)
                .HasColumnName("is_front");
            entity.Property(e => e.IsReady).HasColumnName("is_ready");
            entity.Property(e => e.PricePerNight).HasColumnName("price_per_night");
            entity.Property(e => e.Rooms)
                .HasDefaultValue((short)1)
                .HasColumnName("rooms");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bookings_pkey");

            entity.ToTable("bookings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppBookId).HasColumnName("app_book_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(64)
                .HasDefaultValueSql("'empty'::character varying")
                .HasColumnName("description");
            entity.Property(e => e.Status)
                .HasMaxLength(16)
                .HasDefaultValueSql("'inactive'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AppBook).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.AppBookId)
                .HasConstraintName("bookings_app_book_id_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.HasIndex(e => e.Email, "clients_email_key").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "clients_phone_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientName)
                .HasMaxLength(64)
                .HasColumnName("client_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("email");
            entity.Property(e => e.GuestCount)
                .HasDefaultValue((short)1)
                .HasColumnName("guest_count");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(16)
                .HasColumnName("phone_number");
            entity.Property(e => e.RegStatus)
                .HasMaxLength(16)
                .HasDefaultValueSql("'unregistred'::character varying")
                .HasColumnName("reg_status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
