using Core.WorkflowEngine.Domain.Abstractions;
using Core.WorkflowEngine.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Domain.Entities
{
    public class Instance : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? InitiatorWorkItemId { get; set; }
        public Guid ProcessId { get; set; }
        public Guid TaskId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        public int Status { get; set; } = 1;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        [ForeignKey(nameof(InitiatorWorkItemId))]
        public WorkItem InitiatorWorkItem { get; set; } // Navigation Property

        public ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
        public ProcessTask ProcessTask { get; set; }
    }
}
