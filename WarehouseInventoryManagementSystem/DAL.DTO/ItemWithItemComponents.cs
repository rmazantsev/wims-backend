using DAL.Base;
using Domain.App;
using Domain.Base;

namespace DAL.DTO;

public class ItemWithItemComponents: DomainEntityId
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    
    public ICollection<ItemComponent>? ItemComponents { get; set; }

    public ICollection<ItemComponent>? OtherItemsComponents { get; set; }

    public Unit? Unit { get; set; } = null!;
    public Category? Category { get; set; }
}