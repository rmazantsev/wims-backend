using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Store: DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = null!;
    [MaxLength(256)]
    public string Address { get; set; } = null!;

    public ICollection<StoreInventory>? StoreInventories { get; set; }
    public ICollection<InventoryTransaction>? InventoryTransactions { get; set; }
}