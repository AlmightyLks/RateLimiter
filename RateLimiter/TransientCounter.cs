using System;
using System.Threading.Tasks;

namespace RateLimiter
{
    public class TransientCounter
    {
        public ulong Count { get; private set; }

        public void Increment(TimeSpan time, ulong amount = 1)
        {
            Count += amount;
            _ = Task.Delay(time)
                    .ContinueWith(_ => Count--);
        }
    }
}
