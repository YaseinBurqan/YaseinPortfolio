using System;

namespace YaseinPortfolio.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public string SkillImg { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;

    }
}
