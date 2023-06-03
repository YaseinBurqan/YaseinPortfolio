
using System;
using System.ComponentModel.DataAnnotations;

namespace YaseinPortfolio.Models
{
    public class ContactMeByEmail
    {
        public int ContactMeByEmailId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string ContactMeByEmailEmail { get; set; }

        [DataType(DataType.MultilineText)]
        public string ContactMeByEmailSubject { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;

    }
}
