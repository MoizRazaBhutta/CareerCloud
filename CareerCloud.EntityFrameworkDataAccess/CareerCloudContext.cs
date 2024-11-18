using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext:DbContext
    {
        private readonly string _connStr;
        public CareerCloudContext(DbContextOptions<CareerCloudContext> options):base(options)
        {
            _connStr = "Data Source=MoizLenovo;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True; TrustServerCertificate=True;";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connStr);
        }
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(ae => ae.ApplicantProfile)
                .WithMany(ap => ap.ApplicantEducations)
                .HasForeignKey(ae => ae.Applicant);
            modelBuilder.Entity<ApplicantJobApplicationPoco>((e) =>
            {
                e.HasOne(aja => aja.ApplicantProfile)
                .WithMany(ap => ap.ApplicantJobApplications)
                .HasForeignKey(aja => aja.Applicant);

                e.HasOne(aja => aja.CompanyJob)
                .WithMany(ap => ap.ApplicantJobApplications)
                .HasForeignKey(aja => aja.Job);
            });

            modelBuilder.Entity<ApplicantProfilePoco>((e) =>
            {
                e.HasOne(ap => ap.SecurityLogins)
                .WithMany(sc => sc.ApplicantProfiles)
                .HasForeignKey(ap => ap.Login);

                e.HasOne(ap => ap.SystemCountryCode)
                .WithMany(sc => sc.ApplicantProfiles)
                .HasForeignKey(ap => ap.Country);
            });

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(ae => ae.ApplicantProfile)
                .WithMany(ap => ap.ApplicantResumes)
                .HasForeignKey(ae => ae.Applicant);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(ae => ae.ApplicantProfile)
                .WithMany(ap => ap.ApplicantSkills)
                .HasForeignKey(ae => ae.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>((e) =>
            {
                e.HasOne(ap => ap.ApplicantProfile)
                .WithMany(sc => sc.ApplicantWorkHistorys)
                .HasForeignKey(ap => ap.Applicant);

                e.HasOne(ap => ap.SystemCountryCode)
               .WithMany(sc => sc.ApplicantWorkHistory)
               .HasForeignKey(ap => ap.CountryCode);
            });

            modelBuilder.Entity<CompanyDescriptionPoco>((e) =>
            {
                e.HasOne(ap => ap.CompanyProfile)
                .WithMany(sc => sc.CompanyDescriptions)
                .HasForeignKey(ap => ap.Company);

                e.HasOne(ap => ap.SystemLanguageCode)
               .WithMany(sc => sc.CompanyDescriptions)
               .HasForeignKey(ap => ap.LanguageId);
            });
            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(ae => ae.CompanyJob)
                .WithMany(ap => ap.CompanyJobDescriptions)
                .HasForeignKey(ae => ae.Job);

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(ae => ae.CompanyJob)
                .WithMany(ap => ap.CompanyJobEducations)
                .HasForeignKey(ae => ae.Job);
            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(ae => ae.CompanyJob)
                .WithMany(ap => ap.CompanyJobSkills)
                .HasForeignKey(ae => ae.Job);
            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(ae => ae.CompanyProfile)
                .WithMany(ap => ap.CompanyLocations)
                .HasForeignKey(ae => ae.Company);
            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(ae => ae.CompanyProfile)
                .WithMany(ap => ap.CompanyJobs)
                .HasForeignKey(ae => ae.Company);
            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(ae => ae.SystemCountryCode)
                .WithMany(ap => ap.CompanyLocations)
                .HasForeignKey(ae => ae.CountryCode);
            modelBuilder.Entity<SecurityLoginsLogPoco>()
               .HasOne(ae => ae.SecurityLogin)
               .WithMany(ap => ap.SecurityLoginsLogs)
               .HasForeignKey(ae => ae.Login);
            modelBuilder.Entity<SecurityLoginsRolePoco>()
               .HasOne(ae => ae.SecurityLogin)
               .WithMany(ap => ap.SecurityLoginsRoles)
               .HasForeignKey(ae => ae.Login);
            modelBuilder.Entity<SecurityLoginsRolePoco>()
               .HasOne(ae => ae.SecurityRole)
               .WithMany(ap => ap.SecurityLoginsRoles)
               .HasForeignKey(ae => ae.Role);
            

        }



    }
}
