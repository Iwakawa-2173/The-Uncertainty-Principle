using System;
using System.Collections.Generic;
using TUP.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TUP.WebApi.Infrastructure
{
    public partial class TUPContext : DbContext
    {
        public TUPContext()
        {
        }

        public TUPContext(DbContextOptions<TUPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Response> Responses { get; set; }

        public virtual DbSet<ScoreScale> ScoreScales { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Main");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Response>()
				.HasOne(r => r.Player)
				.WithMany()
				.HasForeignKey(r => r.PlayerId);

			modelBuilder.Entity<Response>()
				.HasOne(r => r.Event)
				.WithMany()
				.HasForeignKey(r => r.EventId);

			modelBuilder.Entity<ScoreScale>()
				.HasOne(s => s.Player)
				.WithMany()
				.HasForeignKey(s => s.PlayerId);
			
			modelBuilder.Entity<ChatMessage>()
				.HasOne(c => c.Player)
				.WithMany()
				.HasForeignKey(c => c.PlayerId);
			
		}

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
