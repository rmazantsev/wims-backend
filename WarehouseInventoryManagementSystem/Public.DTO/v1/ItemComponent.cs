namespace Public.DTO.v1;

/// <summary>
/// Public.DTO.v1.ItemComponent 
/// </summary>
public class ItemComponent
{
    /// <summary>
    /// Id of ItemComponent
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Need amount of component item for parent item
    /// </summary>
    public int ComponentQuantity { get; set; }

    /// <summary>
    /// Component item Id
    /// </summary>
    public Guid ComponentItemId { get; set; }
    /// <summary>
    /// Component item
    /// </summary>
    public Item? ComponentItem { get; set; }
    
    /// <summary>
    /// Parent item id
    /// </summary>
    public Guid ItemId { get; set; }
}