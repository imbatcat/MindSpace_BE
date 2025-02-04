namespace MindSpace.Application.Commons.Utilities.Seeding
{
    public interface IEntityOrderProvider
    {
        IEnumerable<string> GetOrderedEntities();
    }
}
