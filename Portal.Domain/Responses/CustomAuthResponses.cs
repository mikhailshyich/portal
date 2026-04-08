using Portal.Domain.Entities.Users;

namespace Portal.Domain.Responses
{
    public record CustomAuthResponses(bool Flag = false, string Message = null!, string Token = null!, UserView User = null!) { }
}
