using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace RateLimiter.Tests
{
    public class RateLimiterTests
    {
        private const ulong TestingRate = 5;
        private const double TestingMilliseconds = 35;

        [Test]
        public void Invoke_ReturnOkay_NotExceedingRate()
        {
            RateLimit rateLimit = new RateLimit(TestingRate, TimeSpan.FromMilliseconds(TestingMilliseconds));
            bool expected = false;
            for (int i = 0; i < 5; i++)
            {
                if (rateLimit.Invoke() == InvokeStatus.Exceeded)
                {
                    expected = false;
                    break;
                }
                else
                {
                    expected = true;
                }
            }
            Assert.IsTrue(expected);
        }
        [Test]
        public void Invoke_ReturnExceed_ExceedingRate()
        {
            RateLimit rateLimit = new RateLimit(TestingRate, TimeSpan.FromMilliseconds(TestingMilliseconds));
            bool expected = false;
            for (int i = 0; i < 6; i++)
            {
                if (rateLimit.Invoke() == InvokeStatus.Exceeded)
                {
                    expected = true;
                    break;
                }
                else
                {
                    expected = false;
                }
            }
            Assert.IsTrue(expected);
        }
    }
}