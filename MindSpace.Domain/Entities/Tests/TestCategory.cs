namespace MindSpace.Domain.Entities.Tests
{
    public class TestCategory : BaseEntity
    {
        public string Name { get; set; }

        //Navigation props
        public virtual ICollection<Test> Tests { get; set; } = new HashSet<Test>();
    }
}