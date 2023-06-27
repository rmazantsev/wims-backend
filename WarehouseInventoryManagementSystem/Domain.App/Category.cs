using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;
using Domain.Contracts.Base;

namespace Domain.App;

public class Category: DomainEntityId
{
    [MaxLength(128)] 
    public string Name { get; set; } = default!;
    [MaxLength(256)]
    public string? Description { get; set; }

    public ICollection<Item>? Items { get; set; }
}