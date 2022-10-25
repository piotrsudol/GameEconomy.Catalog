using GameEconomy.Catalog.Dtos;
using GameEconomy.Catalog.Entities;
using GameEconomy.Catalog.Extensions;
using GameEconomy.Common;
using Microsoft.AspNetCore.Mvc;

namespace GameEconomy.Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> itemsRepository;

        public ItemsController(IRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> Get()
        {
            var items = await itemsRepository.GetAllAsync();
            var castedToDto = items.Select(i => i.asDto());
            return castedToDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.asDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Post(CreateItemDto createItem)
        {
            var item = new Item
            {
                Name = createItem.Name,
                Description = createItem.Description,
                Price = createItem.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateItemDto updateItem)
        {
            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItem.Name;
            existingItem.Description = updateItem.Description;
            existingItem.Price = updateItem.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            await itemsRepository.RemoveAsync(existingItem.Id);

            return NoContent();
        }
    }
}
