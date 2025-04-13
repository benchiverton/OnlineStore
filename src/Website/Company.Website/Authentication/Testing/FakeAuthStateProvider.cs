using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Company.Website.Authentication.Testing;

public class FakeAuthStateProvider : AuthenticationStateProvider, IAccessTokenProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity([
            new Claim(ClaimTypes.Name, ">> TEST USER <<")
        ], "Fake authentication type");

        var user = new ClaimsPrincipal(identity);

        return Task.FromResult(new AuthenticationState(user));
    }

    public ValueTask<AccessTokenResult> RequestAccessToken() => ValueTask.FromResult(
        new AccessTokenResult(AccessTokenResultStatus.Success,
            new AccessToken { Expires = DateTime.Now.AddDays(365) }, "", null));

    public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options) =>
        ValueTask.FromResult(new AccessTokenResult(AccessTokenResultStatus.Success,
            new AccessToken { Expires = DateTime.Now.AddDays(365) }, "", null));
}
