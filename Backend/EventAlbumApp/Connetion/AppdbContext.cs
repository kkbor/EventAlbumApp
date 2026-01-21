using EventAlbumApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventAlbumApp.Connetion
{
    public class AppdbContext: DbContext
    {
        public AppdbContext(DbContextOptions<AppdbContext> options)
    : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Qr> Qrs { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modele
            modelBuilder.Entity<Users>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Qr>()
                .HasKey(q => q.Id);
            modelBuilder.Entity<Qr>()
                .HasIndex(q => q.Token)
                .IsUnique();
            modelBuilder.Entity<Album>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Photos>()
                .HasKey(p => p.Id);
            //nawigacja
            modelBuilder.Entity<Album>()
                .HasOne(u => u.Users)
                .WithMany(a => a.Albums)
                .HasForeignKey(a => a.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Album>()
                .HasOne(a => a.Qr)
                .WithOne(q => q.Album)
                .HasForeignKey<Album>(a => a.IdQr)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Photos>()
                .HasOne(p => p.Album)
                .WithMany(a => a.Photos)
                .HasForeignKey(p => p.IdAlbum)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
