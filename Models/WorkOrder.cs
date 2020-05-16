using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Models
{
    [Table("WorkOrder")]
    public partial class WorkOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrder()
        {
            WorkOrderTransitionHistories = new HashSet<WorkOrderTransitionHistory>();
            //TODO: get workflow schema and propulate initial state below
            State = "VacationRequestCreated";
            StateName = "Vacation request created";
        }
        public Guid Id { get; set; }

        public string WorkflowCode { get; set; }

        [Required]
        [StringLength(1024)]
        public string State { get; set; }

        [StringLength(1024)]
        public string StateName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrderTransitionHistory> WorkOrderTransitionHistories { get; set; }

        #region WorkOrder props
        public string Title { get; set; }
        public string ClientWorkRequestId { get; set; }
        public string BusinessUnitId { get; set; }
        public string RelatedWorkOrderRequest { get; set; }
        public Guid CreatedById { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        public Guid RequestorId { get; set; }
        public virtual ApplicationUser Requestor { get; set; }
        #endregion
    }
}
