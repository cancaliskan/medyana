using Medyana.Common.Contracts;
using NUnit.Framework;

namespace Medyana.Tests.Helpers
{
    public class AssertHelper<T> where T : class
    {
        public void Assertion(Response<T> response, Response<T> result)
        {
            Assert.AreEqual(response.ErrorMessage, result.ErrorMessage);
            Assert.AreEqual(response.Result, result.Result);
            Assert.AreEqual(response.IsSucceed, result.IsSucceed);
        }
    }
}