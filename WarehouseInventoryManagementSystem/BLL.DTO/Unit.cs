using BLL.Base;
using Domain.Base;

namespace BLL.DTO;
/// <summary>
/// BLL.DTO.Unit 
/// </summary>
public class Unit: DomainEntityId
{
    /// <summary>
    /// Unit name
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Unit optional description
    /// </summary>
    public string Description { get; set; } = null!;
}