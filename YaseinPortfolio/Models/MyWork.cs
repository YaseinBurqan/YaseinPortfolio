using System;

namespace YaseinPortfolio.Models
{
    public class MyWork
    {
        public int MyWorkId { get; set; }
        public string MyWorkName { get; set; }
        public string MyWorkImg { get; set; }
        public string MyWorkUrl { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;

    }
}
