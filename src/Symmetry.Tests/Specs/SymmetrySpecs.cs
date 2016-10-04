using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Symmetry.Tests.Specs
{
    [TestFixture]
    public class SymmetrySpecs
    {
        [Test]
        public async Task when_invoking_run_async_it_should_execute_only_once()
        {
            var symmetryMock = new Mock<ISymmetry>();
            await symmetryMock.Object.RunAsync();
            symmetryMock.Verify(x => x.RunAsync(), Times.Once);
        }
    }
}