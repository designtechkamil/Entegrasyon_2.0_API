using Domain.Entities.Auth;

namespace Domain.Entities;

public class RefreshToken : Security.Entities.RefreshToken<Guid, Guid>
{
    public virtual User User { get; set; } = default!;
}
