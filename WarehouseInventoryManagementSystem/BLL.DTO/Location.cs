using BLL.Base;
using Domain.Base;

namespace BLL.DTO;

/// <summary>
/// BLL.DTO.Location 
/// </summary>
public class Location: DomainEntityId
{
    /// <summary>
    /// Location name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Location optional description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Warehouse id to which this location belongs
    /// </summary>
    public Guid WarehouseId { get; set; }
}