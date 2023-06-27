namespace Public.DTO.v1;

/// <summary>
/// Public.DTO.v1.Location 
/// </summary>
public class Location
{
    /// <summary>
    /// Id of location
    /// </summary>
    public Guid Id { get; set; }

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