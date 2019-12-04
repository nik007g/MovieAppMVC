using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models
{
    public partial class MovieProjectContext : DbContext
    {
        public MovieProjectContext()
        {
        }
        public MovieProjectContext(DbContextOptions<MovieProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movie { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=FSIND-LT-43;Database=MovieProject;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.MovieName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
