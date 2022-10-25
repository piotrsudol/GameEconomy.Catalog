using GameEconomy.Catalog.Dtos;
using GameEconomy.Catalog.Entities;

namespace GameEconomy.Catalog.Extensions
{
    public static class ItemMapper
    {
        public static ItemDto asDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}
