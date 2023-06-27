using Domain.Base;

namespace BLL.DTO;
/// <summary>
/// BLL.DTO.WarehouseInventory 
/// </summary>
public class Category: DomainEntityId
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid AppUserId { get; set; }
}