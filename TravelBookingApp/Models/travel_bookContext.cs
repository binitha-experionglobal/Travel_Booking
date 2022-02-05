using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TravelBookingApp.Models
{
    public partial class travel_bookContext : DbContext
    {
        public travel_bookContext()
        {
        }

        public travel_bookContext(DbContextOptions<travel_bookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authentication> Authentication { get; set; }
        public virtual DbSet<FlightData> FlightData { get; set; }
        public virtual DbSet<FlightRoute> FlightRoute { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<TicketAmnt> TicketAmnt { get; set; }
        public virtual DbSet<TotalTicktCharge> TotalTicktCharge { get; set; }
        public virtual DbSet<Traveler> Traveler { get; set; }
    /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source= BINITHAS\\SQLEXPRESS; Initial Catalog= travel_book; Integrated security=True");
            }
        }
    */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authentication>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__authenti__CB9A1CDF00B615DA");

                entity.ToTable("authentication");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FlightData>(entity =>
            {
                entity.HasKey(e => e.FlightId)
                    .HasName("PK__flight_d__E373AB5D67AC921C");

                entity.ToTable("flight_data");

                entity.Property(e => e.FlightId)
                    .HasColumnName("flight_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdultCharge).HasColumnName("adult_charge");

                entity.Property(e => e.AirlineName)
                    .IsRequired()
                    .HasColumnName("airline_name")
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.ArrivalTime)
                    .HasColumnName("arrival_time")
                    .HasColumnType("date");

                entity.Property(e => e.ChildCharge).HasColumnName("child_charge");

                entity.Property(e => e.DepartTime)
                    .HasColumnName("depart_time")
                    .HasColumnType("date");

                entity.Property(e => e.FlightRouteId).HasColumnName("flight_routeID");

                entity.HasOne(d => d.FlightRoute)
                    .WithMany(p => p.FlightData)
                    .HasForeignKey(d => d.FlightRouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__flight_da__fligh__3C69FB99");
            });

            modelBuilder.Entity<FlightRoute>(entity =>
            {
                entity.ToTable("flight_route");

                entity.Property(e => e.FlightRouteId)
                    .HasColumnName("flight_routeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DepartLocation)
                    .IsRequired()
                    .HasColumnName("depart_location")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DestinLocation)
                    .IsRequired()
                    .HasColumnName("destin_location")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TravelerId).HasColumnName("travelerID");

                entity.HasOne(d => d.Traveler)
                    .WithMany(p => p.FlightRoute)
                    .HasForeignKey(d => d.TravelerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__flight_ro__trave__398D8EEE");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => e.PassengerListId)
                    .HasName("PK__passenge__4DC00072270DC8A4");

                entity.ToTable("passenger");

                entity.Property(e => e.PassengerListId)
                    .HasColumnName("passenger_listID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdultTotal).HasColumnName("adult_total");

                entity.Property(e => e.ChildTotal).HasColumnName("child_total");

                entity.Property(e => e.CoTravellrTotal).HasColumnName("co_travellr_total");

                entity.Property(e => e.FlightId).HasColumnName("flight_ID");

                entity.Property(e => e.TravelerId).HasColumnName("traveler_ID");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Passenger)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__passenger__fligh__4316F928");

                entity.HasOne(d => d.Traveler)
                    .WithMany(p => p.Passenger)
                    .HasForeignKey(d => d.TravelerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__passenger__trave__4222D4EF");
            });

            modelBuilder.Entity<TicketAmnt>(entity =>
            {
                entity.ToTable("ticket_amnt");

                entity.Property(e => e.TicketAmntId)
                    .HasColumnName("ticket_amntID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaggageChrg).HasColumnName("baggage_chrg");

                entity.Property(e => e.FlightId).HasColumnName("flight_ID");

                entity.Property(e => e.HandlingChrg).HasColumnName("handling_chrg");

                entity.Property(e => e.TaxAmnt).HasColumnName("tax_amnt");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.TicketAmnt)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ticket_am__fligh__3F466844");
            });

            modelBuilder.Entity<TotalTicktCharge>(entity =>
            {
                entity.HasKey(e => e.ChargeId)
                    .HasName("PK__total_ti__F71EF9930849C39F");

                entity.ToTable("total_ticktCharge");

                entity.Property(e => e.ChargeId)
                    .HasColumnName("chargeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FlightId).HasColumnName("flight_ID");

                entity.Property(e => e.PassengerListId).HasColumnName("passenger_listID");

                entity.Property(e => e.TicketAmountId).HasColumnName("ticket_AmountID");

                entity.Property(e => e.TotalAmnt).HasColumnName("total_Amnt");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.TotalTicktCharge)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__total_tic__fligh__47DBAE45");

                entity.HasOne(d => d.PassengerList)
                    .WithMany(p => p.TotalTicktCharge)
                    .HasForeignKey(d => d.PassengerListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__total_tic__passe__45F365D3");

                entity.HasOne(d => d.TicketAmount)
                    .WithMany(p => p.TotalTicktCharge)
                    .HasForeignKey(d => d.TicketAmountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__total_tic__ticke__46E78A0C");
            });

            modelBuilder.Entity<Traveler>(entity =>
            {
                entity.ToTable("traveler");

                entity.Property(e => e.TravelerId)
                    .HasColumnName("travelerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TravelDate)
                    .HasColumnName("travelDate")
                    .HasColumnType("date");

                entity.Property(e => e.TravelerName)
                    .IsRequired()
                    .HasColumnName("travelerName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
