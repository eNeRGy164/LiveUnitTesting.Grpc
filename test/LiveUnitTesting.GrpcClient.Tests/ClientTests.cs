using Xunit;

namespace LiveUnitTesting.GrpcClient.Tests
{
    public class ClientTests
    {
        [Fact]
        public void TestClientMethod()
        {
            var client = new DaprClientGrpc();

            var value = client.Method();

            Assert.NotEqual(default, value);
        }
    }
}
