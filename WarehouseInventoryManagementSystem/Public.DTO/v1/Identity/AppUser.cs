namespace Public.DTO.v1.Identity;

public class AppUser
{
    /// <summary>
    /// Id of AppUser
    /// </summary>
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}