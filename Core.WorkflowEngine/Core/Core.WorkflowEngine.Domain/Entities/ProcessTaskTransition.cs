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
    public class ProcessTaskTransition : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProcessTaskId { get; set; }
        public Guid NextTaskId { get; set; }
        public Guid ActionId { get; set; }
        public string ConditionExpression { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }


        public ProcessTask ProcessTask { get; set; }
        public ProcessTask NextTask { get; set; }
        public ProcessTaskAction ProcessTaskAction { get; set; }
    }
}
