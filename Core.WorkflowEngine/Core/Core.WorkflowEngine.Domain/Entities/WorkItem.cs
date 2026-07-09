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
    public class WorkItem : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid InstanceId { get; set; }
        public Guid StepId { get; set; }
        public Guid AssignedUserId { get; set; }
        public Guid AssignedRoleId { get; set; }
        public Guid SelectedAction { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public Guid CompletedBy { get; set; }
        public int Status { get; set; } = 1; // Waiting
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }


        [ForeignKey(nameof(InstanceId))]
        public Instance Instance { get; set; }

        [ForeignKey(nameof(StepId))]
        public ProcessTask ProcessTask { get; set; }
    }
}
