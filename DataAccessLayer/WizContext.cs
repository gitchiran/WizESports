using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public partial class WizContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public WizContext()
        {
        }

        public WizContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WizContext(DbContextOptions<WizContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("WizDatabaseConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public virtual DbSet<Clan> Clan { get; set; }
        public virtual DbSet<ContactPerson> ContactPerson { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<MatchScore> MatchScore { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamTournamentGroup> TeamTournamentGroup { get; set; }
        public virtual DbSet<Tournament> Tournament { get; set; }
        public virtual DbSet<TournamentDraw> TournamentDraw { get; set; }
        public virtual DbSet<TournamentGroup> TournamentGroup { get; set; }
        public virtual DbSet<TournamentTeam> TournamentTeam { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<AdminSettings> AdminSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clan>(entity =>
            {
                entity.Property(e => e.ClanDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ClanName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LogoPath).IsUnicode(false);

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.HasOne(d => d.ContactPersonNavigation)
                    .WithMany(p => p.Clan)
                    .HasForeignKey(d => d.ContactPerson)
                    .HasConstraintName("FK_Clan_ContactPerson");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Clan)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Clan_User");
            });

            modelBuilder.Entity<ContactPerson>(entity =>
            {
                entity.Property(e => e.Cpname)
                    .HasColumnName("CPName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nic)
                    .HasColumnName("NIC")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.MatchDate).HasColumnType("datetime");

                entity.Property(e => e.MatchDescription).IsUnicode(false);

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Match_Tournament");
            });

            modelBuilder.Entity<MatchScore>(entity =>
            {
                entity.Property(e => e.Score).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchScore)
                    .HasForeignKey(d => d.MatchId)
                    .HasConstraintName("FK_MatchScore_Match");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.MatchScore)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_MatchScore_Team");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MatchScore)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_MatchScore_User");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Nic)
                    .HasColumnName("NIC")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PlayerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_Table_1_Team");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.LogoPath).IsUnicode(false);

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.Score).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TeamDescription).IsUnicode(false);

                entity.Property(e => e.TeamName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Clan)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.ClanId)
                    .HasConstraintName("FK_Team_Clan");

                entity.HasOne(d => d.ContactPersonNavigation)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.ContactPerson)
                    .HasConstraintName("FK_Team_ContactPerson");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Team_User");
            });

            modelBuilder.Entity<TeamTournamentGroup>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TeamTournamentGroup)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_TeamTournamentGroup_TournamentGroup");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamTournamentGroup)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_TeamTournamentGroup_Team");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.SceduledDate).HasColumnType("datetime");

                entity.Property(e => e.TournamentDescription).IsUnicode(false);

                entity.Property(e => e.TournamentName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TournamentDraw>(entity =>
            {
                entity.Property(e => e.DrawDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TournamentDraw)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_TournamentDraw_User");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.TournamentDraw)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("FK_TournamentDraw_Tournament");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TournamentDraw)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_TeamTournamentDraw_Team");
        });

            modelBuilder.Entity<TournamentGroup>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TournamentGroup)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_TournamentGroup_User");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.TournamentGroup)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("FK_TournamentGroup_Tournament");
            });

            modelBuilder.Entity<TournamentTeam>(entity =>
            {
                entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentProof).IsUnicode(false);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TournamentTeam)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_TournamentTeam_Team");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.TournamentTeam)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("FK_TournamentTeam_Tournament");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegisteredDate).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasPrincipalKey(p => p.RoleId)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_UserRole");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserRole1)
                    .HasColumnName("UserRole")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AdminSettings>(entity =>
            {
                entity.Property(e => e.YoutubeUrl)
                    .HasColumnName("YoutubeUrl")
                    .HasMaxLength(500)
                    .IsUnicode(false);               
            });
        }
    }
}
