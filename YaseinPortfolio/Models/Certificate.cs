using System;

namespace YaseinPortfolio.Models
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public string CertificateName { get; set; }
        public string CertificateImg { get; set; }
        public string CertificateGiver { get; set; }
        public DateTime CertificateReleaseDate { get; set; }
        public string CertificatePdf { get; set; }
        public string CertificateDescription { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;

    }
}
