using System;

namespace YaseinPortfolio.Models
{
   public class ErrorViewModel
   {
      public string RequestId { get; set; }

      public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
   }
}
