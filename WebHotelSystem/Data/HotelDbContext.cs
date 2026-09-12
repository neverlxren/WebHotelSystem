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

    public virtual DbSet<Cleaning> Cleanings { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<GuestOrder> GuestOrders { get; set; }

    public virtual DbSet<HotelWifiSetting> HotelWifiSettings { get; set; }

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

        modelBuilder.Entity<Cleaning>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cleaning_pkey");

            entity.ToTable("cleaning");

            entity.HasIndex(e => e.AppNumb, "cleaning_app_numb_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppNumb).HasColumnName("app_numb");
            entity.Property(e => e.CleaningStatus)
                .HasMaxLength(32)
                .HasDefaultValueSql("'in process'::character varying")
                .HasColumnName("cleaning_status");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.RealisedBy)
                .HasMaxLength(32)
                .HasColumnName("realised_by");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AppNumbNavigation).WithOne(p => p.Cleaning)
                .HasPrincipalKey<Apartment>(p => p.AppNumber)
                .HasForeignKey<Cleaning>(d => d.AppNumb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cleaning_app_numb_fkey");
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

        modelBuilder.Entity<GuestOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("guest_orders_pkey");

            entity.ToTable("guest_orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppNumb).HasColumnName("app_numb");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.Guest).HasColumnName("guest");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(32)
                .HasDefaultValueSql("'new'::character varying")
                .HasColumnName("order_status");
            entity.Property(e => e.RealisedBy)
                .HasMaxLength(32)
                .HasColumnName("realised_by");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AppNumbNavigation).WithMany(p => p.GuestOrders)
                .HasPrincipalKey(p => p.AppNumber)
                .HasForeignKey(d => d.AppNumb)
                .HasConstraintName("guest_orders_app_numb_fkey");

            entity.HasOne(d => d.GuestNavigation).WithMany(p => p.GuestOrders)
                .HasForeignKey(d => d.Guest)
                .HasConstraintName("guest_orders_guest_fkey");
        });

        modelBuilder.Entity<HotelWifiSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("settings_pkey");

            entity.ToTable("hotel_wifi_settings");

            entity.HasIndex(e => e.WifiName, "settings_wifi_name_key").IsUnique();

            entity.HasIndex(e => e.WifiPassword, "settings_wifi_password_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('settings_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.AppNumb).HasColumnName("app_numb");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.WifiName)
                .HasMaxLength(32)
                .HasColumnName("wifi_name");
            entity.Property(e => e.WifiPassword)
                .HasMaxLength(32)
                .HasColumnName("wifi_password");

            entity.HasOne(d => d.AppNumbNavigation).WithMany(p => p.HotelWifiSettings)
                .HasPrincipalKey(p => p.AppNumber)
                .HasForeignKey(d => d.AppNumb)
                .HasConstraintName("settings_app_numb_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
