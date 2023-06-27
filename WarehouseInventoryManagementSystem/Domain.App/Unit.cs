using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Unit: DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = null!;
    [MaxLength(256)]
    public string Description { get; set; } = null!;

    public ICollection<Item>? Items { get; set; }
}