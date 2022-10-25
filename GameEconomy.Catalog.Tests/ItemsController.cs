using GameEconomy.Catalog.Controllers;
using GameEconomy.Catalog.Entities;
using GameEconomy.Common;
using Moq;

namespace GameEconomy.Catalog.Tests
{
    public class ItemController
    {
        [Test]
        public async Task GetAll()
        {
            var guid = Guid.NewGuid();
            var mockedValues = new List<Item>() {
                new Item() {
                    Id = guid,
                    Name = "Test",
                    Description = "Test description"
                }
            };
            var repositoryMock = new Mock<IRepository<Item>>();
            repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(mockedValues);
            var controller = new ItemsController(repositoryMock.Object);

            var returnedValue = (await controller.Get()).ToList();

            Assert.IsTrue(returnedValue.Count == 1);
            Assert.IsTrue(returnedValue.Single(i => i.Id == guid) != null);
        }
    }
}