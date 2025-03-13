namespace SkeletonKit.Common.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedDate = DateTime.UtcNow;
            this.CreatedBy = "SYS";
        }

        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
