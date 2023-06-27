using DAL.Base;
using Domain.Base;

namespace DAL.DTO;

/// <summary>
/// DAL.DTO.v1.Location 
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