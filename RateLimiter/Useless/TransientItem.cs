using System.Threading.Tasks;

namespace RateLimiter
{
    public class TransientItem<T>
    {
        public T Item { get; set; }
        public Task Task { get; set; }
    }
}
