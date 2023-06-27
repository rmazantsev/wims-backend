using Domain.App.Identity;
using Domain.Base;

namespace Public.DTO.v1;

/// <summary>
/// Public.DTO.v1.Store 
/// </summary>
public class Store
{
    /// <summary>
    /// Id of Store
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of Store
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Address of Store
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// Store Inventories
    /// </summary>
    public ICollection<StoreInventory>? StoreInventories { get; set; }
}