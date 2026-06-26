using Core.WorkflowEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.WorkflowEngine.Persistence.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ProcessTaskId -> ProcessTask
            modelBuilder.Entity<ProcessTaskTransition>()
            .HasOne(ptt => ptt.ProcessTask)
            .WithMany(pt => pt.ProcessTaskTransitions)
            .HasForeignKey(ptt => ptt.ProcessTaskId)
            .OnDelete(DeleteBehavior.Cascade); // Task Silinirse Task'e Ait Geçişleri de Siler.

            // NextTaskId -> ProcessTask
            modelBuilder.Entity<ProcessTaskTransition>()
            .HasOne(ptt => ptt.NextTask)
            .WithMany()
            .HasForeignKey(ptt => ptt.NextTaskId)
            .OnDelete(DeleteBehavior.Restrict); // Çift Yönlü Silme Çakışmasını Önler.

            // ActionId -> ProcessTaskAction
            modelBuilder.Entity<ProcessTaskTransition>()
            .HasOne(ptt => ptt.ProcessTaskAction)
            .WithMany(pta => pta.ProcessTaskTransitions)
            .HasForeignKey(ptt => ptt.ActionId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instance>()
            .HasOne(wi => wi.ProcessTask)
            .WithMany(pt => pt.Instances)
            .HasForeignKey(wi => wi.TaskId)
            .OnDelete(DeleteBehavior.Restrict);

            // KURAL 1: Her workitem'ın bir instance'ı vardır, her instance'ın birden fazla workitem'ı olabilir.
            modelBuilder.Entity<WorkItem>()
                .HasOne(w => w.Instance)                  // Her WorkItem bir Instance'a bağlıdır (Zorunlu)
                .WithMany(i => i.WorkItems)               // Bir Instance'ın birden fazla WorkItem'ı vardır
                .HasForeignKey(w => w.InstanceId)         // Yabancı anahtar sütunu
                .OnDelete(DeleteBehavior.Cascade);        // Ana süreç (Instance) silinirse, içindeki tüm işler (WorkItem) silinsin.

            // KURAL 2: Her instance'ın bir başlatıcısı (WorkItem) olabilir (Opsiyonel).
            modelBuilder.Entity<Instance>()
                .HasOne(i => i.InitiatorWorkItem)         // Her Instance'ın (eorik olarak) bir başlatıcı WorkItem'ı vardır
                .WithMany()                               // Ters navigasyon özelliği (WorkItem içinde "InitiatedInstances" gibi bir liste) olmadığı için boş bırakıyoruz
                .HasForeignKey(i => i.InitiatorWorkItemId) // Yabancı anahtar sütunu
                .OnDelete(DeleteBehavior.Restrict);       // ÇOK KRİTİK: Veritabanında döngüsel silme (Circular Cascade) hatası almamak için Restrict yapmalıyız.

        }

        public DbSet<Instance> Instances { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<ProcessDefinition> ProcessDefinitions { get; set; }
        public DbSet<ProcessTask> ProcessTasks { get; set; }
        public DbSet<ProcessTaskAction> ProcessTaskActions { get; set; }
        public DbSet<ProcessTaskTransition> ProcessTaskTransitions { get; set; }
    }
}