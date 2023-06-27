using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class ItemComponent: DomainEntityId
{
    public int ComponentQuantity { get; set; }
    
    public Guid ComponentItemId { get; set; }
    public Item? ComponentItem { get; set; } = null!;
    public Guid ItemId { get; set; }
    public Item? Item { get; set; } = null!;
}               