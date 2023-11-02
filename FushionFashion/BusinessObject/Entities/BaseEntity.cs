using System;

namespace BusinessObject.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Guid ApprovedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime DeleteDate { get; set; }
        public Guid DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
