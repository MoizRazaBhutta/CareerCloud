using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Resumes")]
    public class ApplicantResumePoco:IPoco
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("Applicant")]
        public Guid Applicant { get; set; }
        [Column("Resume")]
        public string Resume { get; set; }
        [Column("Last_Updated")]
        public DateTime? LastUpdated { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }

    }
}
