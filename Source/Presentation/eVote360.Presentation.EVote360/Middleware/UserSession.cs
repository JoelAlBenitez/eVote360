using eVote360.Core.Application.Contracts.Authentication.Command;
using eVote360.Core.Domain.Common.Enums;
using System.Security.Claims;

namespace eVote360.Presentation.EVote360.Middleware
{
    public class UserSession : ISessionUser
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetPoliticalParty()
        {
             var idParty = _httpContextAccessor.HttpContext?.User?.FindFirst("PartyId");
            if (idParty != null && int.TryParse(idParty.Value, out int partyId))
            {
                return partyId;
            }

            return 0;

        }

        public UserRole GetRole()
        {
            var role = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role);
            if(int.TryParse(role!.Value, out int roleDigit))
            {
                return (UserRole)roleDigit;
            }
            return (UserRole)0;
        }

        public int GetUserId()
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            return idClaim != null ? int.Parse(idClaim.Value) : 0;
        }

        public string GetUserName()
        {
            var name = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name);
            return name != null ? name.Value : "NO ENCONTRADO";
        }
    }
}
