
namespace Domain
{
    public abstract class Entity : IEntity, ICreateProperty, IChangeProperty
    {
        public int Id { get ; set ; }
        public int CreateBy { get; set; }
        public long RowVersion { get; set; }
        public DateTime CreateDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
