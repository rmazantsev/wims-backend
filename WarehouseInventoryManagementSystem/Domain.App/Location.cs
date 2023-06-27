using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Location: DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = null!;
    [MaxLength(256)]
    public string? Description { get; set; }

    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; } = null!;
}