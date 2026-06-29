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
    public class ProcessTask : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProcessId { get; set; }
        public string StepName { get; set; }
        public bool IsStartStep { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public Guid CreatedBy { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
        public Guid UpdatedBy { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public ProcessDefinition ProcessDefinition { get; set; }
        public ICollection<ProcessTaskAction> ProcessTaskActions { get; set; } = new List<ProcessTaskAction>();
        public ICollection<Instance> Instances { get; set; } = new List<Instance>();
        public ICollection<ProcessTaskTransition> ProcessTaskTransitions { get; set; } = new List<ProcessTaskTransition>();
    }
}
