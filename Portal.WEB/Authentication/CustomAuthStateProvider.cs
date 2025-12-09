using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Portal.WEB.Authentication
{
    public class CustomAuthStateProvider(ProtectedLocalStorage localStorage) : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //var token = await userService.LoginAsync(loginDTO);
                var token = await localStorage.GetAsync<string>("authToken");
                var identity = string.IsNullOrEmpty(token.Value) ? new ClaimsIdentity() : GetClaimsIdentity(token.Value);
                var expDate = identity.Claims.FirstOrDefault(c => c.Type == "exp");
                var user = new ClaimsPrincipal();
                if (expDate != null)
                {
                    var exp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expDate.Value));
                    if (exp.LocalDateTime <= DateTime.Now)
                    {
                        user = new ClaimsPrincipal();
                        await localStorage.DeleteAsync("authToken");
                        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
                    }
                    else
                    {
                        user = new ClaimsPrincipal(identity);
                    }
                }
                return new AuthenticationState(user);
            }
            catch
            {
                var identity = new ClaimsIdentity();
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await localStorage.SetAsync("authToken", token);
            var identity = GetClaimsIdentity(token);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }

        public async Task MarkUserAsLoggedOut()
        {
            await localStorage.DeleteAsync("authToken");
            await localStorage.DeleteAsync("userId");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}