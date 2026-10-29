using JwtProject_UI.Services.Tokens;
using Microsoft.AspNetCore.Authentication;

namespace JwtProject_UI.Handlers
{
    public class JwtTokenHandler(ITokenService _tokenService,
                                 IHttpContextAccessor _httpContextAccessor) : DelegatingHandler
    {
        override protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _tokenService.GetToken;
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var response =  await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                _httpContextAccessor.HttpContext.Response.Redirect("/Auth/Login");
            }

            return response;
        }
    }
}
