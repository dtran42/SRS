using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.ViewModels
{
    public class RequestHistory
    {
        [DisplayName("Seq:")]
        public int Id { get; set; }
        [DisplayName("Step Name:")]
        public string StepName { get; set; }
        [DisplayName("Step Status:")]
        public string StepStatus { get; set; }
        [DisplayName("Completed By:")]
        public string CompletedBy { get; set; }
        [DisplayName("Date:")]
        public DateTime? CompletedDate { get; set; }
    }
}
