namespace MindSpace.Application.DTOs
{
    public class PagedResultDTO<T>(int count, IReadOnlyList<T> data)
    {
        public int Count { get; private set; } = count;
        public IReadOnlyList<T> Data { get; private set; } = data;
    }
}
