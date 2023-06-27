namespace WebApp.ApiControllers.identity;

/// <summary>
/// Logout DTO
/// </summary>
public class Logout
{
    /// <summary>
    /// Token for refreshing JWT
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}