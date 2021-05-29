using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace RateLimiter
{
    public class RateLimit
    {
        public ulong Rate { get; private set; }

        private TimeSpan _timeSpanRate;
        private TransientCounter _invokeCounter;

        public RateLimit(ulong rate, TimeSpan timeSpan)
        {
            _invokeCounter = new TransientCounter();
            Rate = rate;
            _timeSpanRate = timeSpan;
        }
        public InvokeStatus Invoke()
        {
            if (_invokeCounter.Count >= Rate)
            {
                return InvokeStatus.Exceeded;
            }

            _invokeCounter.Increment(_timeSpanRate);
            return InvokeStatus.Okay;
        }
    }
}
