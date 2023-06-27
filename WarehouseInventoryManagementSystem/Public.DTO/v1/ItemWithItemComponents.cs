namespace Public.DTO.v1;

public class ItemWithItemComponents
{
    /// <summary>
    /// Id of Item
    /// </summary>
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    
    public ICollection<ItemComponent>? ItemComponents { get; set; }

    public ICollection<ItemComponent>? OtherItemsComponents { get; set; }

    public Unit? Unit { get; set; } = null!;
    public Category? Category { get; set; }
}