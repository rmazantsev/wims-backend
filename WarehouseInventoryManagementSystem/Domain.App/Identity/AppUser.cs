using System.ComponentModel.DataAnnotations;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser: IdentityUser<Guid>, IDomainEntityId
{
    [MaxLength(128)] 
    public string FirstName { get; set; } = default!;
    [MaxLength(128)] 
    public string LastName { get; set; } = default!;

    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
    public ICollection<InventoryTransaction>? InventoryTransactions { get; set; }
}