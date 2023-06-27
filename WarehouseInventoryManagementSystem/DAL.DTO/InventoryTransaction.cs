using DAL.Base;
using Domain.App.Identity;
using Domain.Base;

namespace DAL.DTO;

/// <summary>
/// DAL.DTO.v1.InventoryTransaction 
/// </summary>
public class InventoryTransaction: DomainEntityId
{
    /// <summary>
    /// Quantity of items affected in the transaction
    /// </summary>
    public long Quantity { get; set; }

    /// <summary>
    /// Enum. 'A' for addition of item, 'W' for withdrawing of item, 'M' for movement of item
    /// </summary>
    public char TransactionType { get; set; }

    /// <summary>
    /// Optional description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Transaction date
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    /// Is it customer pickup
    /// </summary>
    public bool? IsCustomerPickup { get; set; }

    /// <summary>
    /// Id of location item moved from
    /// </summary>
    public Guid? FromLocation { get; set; }

    /// <summary>
    /// Id of location item moved to
    /// </summary>
    public Guid? ToLocation { get; set; }

    /// <summary>
    /// Id of store item moved from
    /// </summary>
    public Guid? FromStore { get; set; }

    /// <summary>
    /// Id of store item moved to
    /// </summary>
    public Guid? ToStore { get; set; }

    /// <summary>
    /// Id of warehouse item moved from
    /// </summary>
    public Guid? FromWarehouse { get; set; }

    /// <summary>
    /// Id of warehouse item moved to
    /// </summary>
    public Guid? ToWarehouse { get; set; }
    
    public Guid? StoreId { get; set; }
    public Store? Store { get; set; }
    
    public Guid? WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    /// <summary>
    /// Item id
    /// </summary>
    public Guid ItemId { get; set; }
    /// <summary>
    /// Transaction item
    /// </summary>
    public Item? Item { get; set; }
    /// <summary>
    /// Transaction user id
    /// </summary>
    public Guid AppUserId { get; set; }
    /// <summary>
    /// Transaction user
    /// </summary>
    public AppUser? AppUser { get; set; }
}