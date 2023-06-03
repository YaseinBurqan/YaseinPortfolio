using System;
namespace YaseinPortfolio.Models
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string ExperienceJobName { get; set; }
        public string ExperienceCompanyName { get; set; }
        public string ExperienceCompanyImg { get; set; }
        public DateTime ExperienceStartDate { get; set; }
        public DateTime ExperienceEndtDate { get; set; }
        public string ExperienceDescription { get; set; }
        public string ExperienceCertificatePdf { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;

    }
}
