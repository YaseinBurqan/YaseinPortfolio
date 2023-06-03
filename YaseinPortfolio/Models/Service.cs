using System;

namespace YaseinPortfolio.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;

    }
}
