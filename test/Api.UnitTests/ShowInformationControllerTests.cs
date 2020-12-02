using System.Threading.Tasks;
using Api.Adapters;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Api.UnitTests
{
    public class ShowInformationControllerTests
    {
        [Test]
        [TestCase(0, 10)]
        [TestCase(1, 0)]
        [TestCase(1, 51)]
        [TestCase(-1, 10)]
        [TestCase(1, -1)]
        public async Task CannotQueryInvalidPaginationParameters(
            int pageNumber,
            int pageSize)
        {
            ShowInformationController subject =
                new ShowInformationController(
                    new InMemoryShowInformationRepository());

            IActionResult result = await subject.Get(pageNumber, pageSize);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }
    }
}
