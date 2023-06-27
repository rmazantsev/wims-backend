using Domain.Base;

namespace BLL.DTO;

/// <summary>
/// BLL.DTO.ItemComponent 
/// </summary>
public class ItemComponent: DomainEntityId
{
    /// <summary>
    /// Need amount of component item for parent item
    /// </summary>
    public int ComponentQuantity { get; set; }

    /// <summary>
    /// Component item Id
    /// </summary>
    public Guid ComponentItemId { get; set; }
    /// <summary>
    /// Component item object
    /// </summary>
    public Item? ComponentItem { get; set; }
    /// <summary>
    /// Parent item id
    /// </summary>
    public Guid ItemId { get; set; }
}