

using DAL.Base;
using Domain.Base;

namespace DAL.DTO;

/// <summary>
/// DAL.DTO.v1.Store 
/// </summary>
public class Store: DomainEntityId
{
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