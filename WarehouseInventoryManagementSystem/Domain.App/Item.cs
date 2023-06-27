using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;
public class Item: DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = null!;
    [MaxLength(256)]
    public string? Description { get; set; }

    [InverseProperty("Item")]
    public ICollection<ItemComponent>? ItemComponents { get; set; }
    [InverseProperty("ComponentItem")]
    public ICollection<ItemComponent>? OtherItemsComponents { get; set; }

    public Guid UnitId { get; set; }
    public Unit? Unit { get; set; } = null!;

    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    
}
