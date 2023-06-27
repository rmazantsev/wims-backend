
namespace Public.DTO.v1;

/// <summary>
/// Public.DTO.v1.Category 
/// </summary>
public class Category
{
    /// <summary>
    /// Category id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Category name
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Description of category
    /// </summary>
    public string? Description { get; set; }
}