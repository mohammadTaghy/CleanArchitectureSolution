using Xunit.Abstractions;

namespace Application
{
    public class UnitTestBase
    {
        protected readonly ITestOutputHelper _testOutputHelper;
        public UnitTestBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}