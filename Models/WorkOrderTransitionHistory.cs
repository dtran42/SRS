using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Models
{
    [Table("WorkOrderTransitionHistory")]
    public partial class WorkOrderTransitionHistory
    {
        public Guid Id { get; set; }

        public Guid DocumentId { get; set; }

        public Guid? EmployeeId { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string AllowedToEmployeeNames { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TransitionTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Order { get; set; }

        [Required]
        [StringLength(1024)]
        public string InitialState { get; set; }

        [Required]
        [StringLength(1024)]
        public string DestinationState { get; set; }

        [Required]
        [StringLength(1024)]
        public string Command { get; set; }

        public virtual WorkOrder WorkOrder { get; set; }

        public virtual ApplicationUser Employee { get; set; }
    }
}
