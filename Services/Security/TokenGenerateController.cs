using Microsoft.IdentityModel.Tokens;
using Services.Models;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace Services.Security
{
    [RoutePrefix("api/Auth")]
    public class TokenGenerateController : ApiController
    {
        [Route("Generate")]
        [AcceptVerbs("POST")]
        public HttpResponseMessage Authenticate(HttpRequestMessage request, [FromBody]string login, [FromBody]string password)
        {
            Credentials credentials = new Credentials();
            UserEntity user = credentials.GetCredentials(login, password);

            if (user != null)
            {
                string token = createToken(user.Login);
                return request.CreateResponse(HttpStatusCode.OK, token);
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Não autorizado");
            }
        }

        private string createToken(string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddMinutes(1);

            string secret_key = ConfigurationManager.AppSettings["secret_key"];
            string url = ConfigurationManager.AppSettings["JWT_URI"];

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
            });

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secret_key));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(issuer: url, audience: url, subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);

            return tokenHandler.WriteToken(token);
        }
    }
}