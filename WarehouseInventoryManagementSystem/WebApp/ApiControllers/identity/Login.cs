using System.ComponentModel.DataAnnotations;

namespace WebApp.ApiControllers.identity;

/// <summary>
/// Login DTO
/// </summary>
public class Login
{
    /// <summary>
    /// Email for login
    /// </summary>
    [StringLength(maximumLength:128, MinimumLength = 5, ErrorMessage = "Wrong length on email")] 
    public string Email { get; set; } = default!;
    /// <summary>
    /// Password for login
    /// </summary>
    public string Password { get; set; } = default!;

}