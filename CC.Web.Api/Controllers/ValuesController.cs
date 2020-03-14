using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json.Linq;

namespace CC.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var idToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            //var code = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.Code);

            var token = HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync("https://localhost:5002/identity");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await ReNewTokenAsync();
                    return RedirectToAction();
                }
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = "accessToken :" + accessToken + "\n\nidToken :" + idToken + "\n\nrefreshToken :";
            if (!string.IsNullOrEmpty(content))
            {
                result += JArray.Parse(content);
            }

            return result;
        }

        private async Task ReNewTokenAsync()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Redirect("Account/AccessDenied");
            }

            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "CCWebClient",
                ClientSecret = "CCWebSecret",
                RefreshToken = refreshToken,
                //GrantType = OpenIdConnectParameterNames.RefreshToken,
                Scope = $"api1 openid profile roles {OidcConstants.StandardScopes.OfflineAccess}"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            var tokens = new[]
            {
                new AuthenticationToken()
                {
                    Name = OpenIdConnectParameterNames.IdToken,
                    Value = tokenResponse.IdentityToken
                },
                new AuthenticationToken()
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = tokenResponse.AccessToken
                },
                new AuthenticationToken()
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = tokenResponse.RefreshToken
                }
            };

            var currentAuthenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            currentAuthenticateResult.Properties.StoreTokens(tokens);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                currentAuthenticateResult.Principal, currentAuthenticateResult.Properties);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Roles = "管理员")]
        public ActionResult<string> Get(int id)
        {
            var user = HttpContext.User;
            
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
