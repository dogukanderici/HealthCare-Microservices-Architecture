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
    public class ProcessDefinition : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProcessName { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public Guid CreatedBy { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
        public Guid UpdatedBy { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public ICollection<ProcessTask> processTasks { get; set; } = new List<ProcessTask>();
    }
}
