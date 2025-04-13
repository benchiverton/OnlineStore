using System;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Website.Authentication.Testing;

internal class FakeAccessTokenProviderAccessor(IServiceProvider provider) : IAccessTokenProviderAccessor
{
    private IAccessTokenProvider _tokenProvider;

    public IAccessTokenProvider TokenProvider => _tokenProvider ??= provider.GetRequiredService<IAccessTokenProvider>();
}
