namespace Public.DTO.v1;

/// <summary>
/// Public.DTO.v1.Item 
/// </summary>
public class Item
{
    /// <summary>
    /// Id of item
    /// </summary>
    public Guid Id { get; set; }

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
    public Unit? Unit { get; set; }
    
    /// <summary>
    /// Collection of components of this item
    /// </summary>
    public ICollection<ItemComponent>? ItemComponents { get; set; }
}