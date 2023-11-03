using System;

namespace BusinessObject.Entities
{
    public class BaseEntity
    {   
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public DateTime ApprovedDate { get; set; } = DateTime.Now;
        public Guid ApprovedBy { get; set; } = Guid.Empty;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public Guid ModifiedBy { get; set; } = Guid.Empty;
        public DateTime DeleteDate { get; set; } = DateTime.Now;
        public Guid DeleteBy { get; set; } = Guid.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
