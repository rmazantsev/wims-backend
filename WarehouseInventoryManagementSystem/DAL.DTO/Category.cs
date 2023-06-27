using DAL.Base;
using Domain.Base;

namespace DAL.DTO;
/// <summary>
/// DAL.DTO.v1.Category 
/// </summary>
public class Category: DomainEntityId
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

}