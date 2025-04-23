using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Tasks
    {
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public int Status {  get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("CreatedBy")]
        public Guid CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
    }
}
