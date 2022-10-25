
using System.ComponentModel.DataAnnotations;

namespace GameEconomy.Catalog.Dtos
{
    public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);
    public record CreateItemDto([Required]string Name, string Description, [Range(0, Double.PositiveInfinity)] decimal Price);
    public record UpdateItemDto([Required]string Name, string Description, [Range(0, Double.PositiveInfinity)]  decimal Price);
}
