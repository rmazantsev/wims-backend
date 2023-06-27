using System.Net;

namespace DAL.DTO;

/// <summary>
/// Api Error response object
/// </summary>
public class RestApiErrorResponse
{
    /// <summary>
    /// Error http status code
    /// </summary>
    public HttpStatusCode Status { get; set; }

    /// <summary>
    /// Actual error message
    /// </summary>
    public string Error { get; set; } = default!;
}