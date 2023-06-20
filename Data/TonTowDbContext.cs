using Microsoft.EntityFrameworkCore;
using TonTow.API.Models;

namespace TonTow.API.Data
{
    public class TonTowDbContext : DbContext
    {
        public TonTowDbContext(DbContextOptions<TonTowDbContext> options) : base(options)
        {
        }
        public DbSet<Adjuster> Adjuster { get; set; } 
        public DbSet<TonTowUser> TonTowUser { get; set; }
        public DbSet<Appraiser> Appraiser { get; set; }
        public DbSet<FileClaims> FileClaims { get; set; }
        public DbSet<CustInsuranceCompUpdate> CustInsuranceCompUpdate { get; set; }
        public DbSet<CustomerPaymentDtls> CustomerPayment { get; set; }
        public DbSet<CustomerPaymentDtls> CustomerPaymentDtl { get; set; }
        public DbSet<PoliceReport> PoliceReport { get; set; }
        public DbSet<PoliceReportVehicleDtls> PoliceReportVehicleDtls { get; set; }
        public DbSet<PoliceReportOperatorDtls> PoliceReportOperatorDtls { get; set; }
        public DbSet<PoliceReportWitness> PoliceReportWitness { get; set; }
        public DbSet<PoliceReportPropertyDamage> PoliceReportPropertyDamage { get; set; }
        public DbSet<PoliceReportTruckAndBusDtls> PoliceReportTruckAndBusDtls { get; set; }
        public DbSet<PoliceReportGeneral> PoliceReportGeneral { get; set; }
        public DbSet<PoliceReportOperatorOwnerVehicleDtls> PoliceReportOperatorOwnerVehicleDtls { get; set; }
        public DbSet<TonTowFileUpload> TonTowFileUpload { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PoliceReport>()
                .HasMany(s => s.PoliceReportVehicleDtl)
                .WithOne(sd => sd.policeReport)
                .HasForeignKey(sd => sd.TonTowRptId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PoliceReport>()
                .HasMany(s => s.PoliceReportOperatorDtls)
                .WithOne(sd => sd.policeReport)
                .HasForeignKey(sd => sd.TonTowRptId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PoliceReport>()
                .HasMany(s => s.PoliceReportWitness)
                .WithOne(sd => sd.policeReport)
                .HasForeignKey(sd => sd.TonTowRptId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PoliceReport>()
                .HasMany(s => s.PoliceReportPropertyDamage)
                .WithOne(sd => sd.policeReport)
                .HasForeignKey(sd => sd.TonTowRptId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PoliceReport>()
                .HasMany(s => s.PoliceReportTruckAndBusDtl)
                .WithOne(sd => sd.policeReport)
                .HasForeignKey(sd => sd.TonTowRptId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PoliceReport>()
                .HasMany(s => s.PoliceReportGeneral)
                .WithOne(sd => sd.policeReport)
                .HasForeignKey(sd => sd.TonTowRptId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PoliceReport>()
                .HasMany(s => s.PoliceReportOperatorOwnerVehicleDtls)
                .WithOne(sd => sd.policeReport)
                .HasForeignKey(sd => sd.TonTowRptId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
