using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mykintai_Indentỉy.AuthenticationFilters
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return base.AuthorizeCore(httpContext);
            var ckToken = httpContext.Request.Cookies["access_token"];
            var access_token = ckToken?.Value;
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return false;
            }

            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                JwtSecurityToken securityToken = securityTokenHandler.ReadToken(access_token) as JwtSecurityToken;
                if (securityToken == null)
                {
                    return false;
                }
                var claims = securityToken.Claims;
                var payLoad = securityToken.Payload;
                if (string.IsNullOrWhiteSpace(payLoad.Sub) 
                    || payLoad.Iss != "FJP" 
                    || securityToken.ValidTo < DateTime.Now)
                {
                    return false;
                }

                if (!httpContext.User.Identity.IsAuthenticated)
                {

                    //httpContext.User = new ClaimsPrincipal(new FormsIdentity(
                    //        new FormsAuthenticationTicket(payLoad.Sub, true, (int)securityToken.ValidTo.Subtract(DateTime.Now).TotalMinutes))
                    //    );
                    httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(
                         new Claim[]
                         {
                             new Claim(ClaimTypes.Name, payLoad.Sub),
                             new Claim("accessToken", access_token),
                             new Claim(ClaimTypes.NameIdentifier, payLoad.Sub),
                             new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", Environment.MachineName)
                         }, "Bearer"
                    ));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                if (!filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                    && !filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                {
                    HandleUnauthorizedRequest(filterContext);
                }
            }
            else
            {
                
            }
        }
    }
}