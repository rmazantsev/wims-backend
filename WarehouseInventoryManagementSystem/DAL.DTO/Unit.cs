using DAL.Base;
using Domain.Base;

namespace DAL.DTO;
/// <summary>
/// DAL.DTO.v1.Unit 
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
    public string Description { get; set; }  = null!;
}