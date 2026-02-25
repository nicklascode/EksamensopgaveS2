using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pitch> Pitches { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            });

            modelBuilder.Entity<Pitch>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Index).IsRequired();
            });

            Pitch[] pitches = new Pitch[21];
            for(int i = 0; i < pitches.Length; ++i)
            {
                pitches[i] = new Pitch { Id = i + 1, Index = i + 1 };
            }

            modelBuilder.Entity<Pitch>().HasData(pitches);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.PitchId).IsRequired();
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.EndTime).IsRequired();
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Pitch)
                      .WithMany()
                      .HasForeignKey(e => e.PitchId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
