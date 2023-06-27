using BLL.Base;
using Domain.Base;

namespace BLL.DTO;

/// <summary>
/// BLL.DTO.Item 
/// </summary>
public class Item: DomainEntityId
{
    /// <summary>
    /// Name of item
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Optional description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Item category Id
    /// </summary>
    public Guid? CategoryId { get; set; }
    /// <summary>
    /// Item category 
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// Item unit Id
    /// </summary>
    public Guid? UnitId { get; set; }
    /// <summary>
    /// Item unit object
    /// </summary>
    public Unit? Unit { get; set; }

    /// <summary>
    /// Collection of components of this item
    /// </summary>
    public ICollection<ItemComponent>? ItemComponents { get; set; }
    
}