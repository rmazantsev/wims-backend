namespace DAL.DTO;

/// <summary>
/// Refresh token object
/// </summary>
public class RefreshTokenModel
{
    /// <summary>
    /// JWT token
    /// </summary>
    public string Jwt { get; set; } = default!;

    /// <summary>
    /// Refresh token to refresh JWT
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}