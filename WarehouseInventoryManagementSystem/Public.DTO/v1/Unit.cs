namespace Public.DTO.v1;
/// <summary>
/// Public.DTO.v1.Unit 
/// </summary>
public class Unit
{
    /// <summary>
    /// Unit id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Unit name
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Unit  description
    /// </summary>
    public string Description { get; set; } = null!;
}