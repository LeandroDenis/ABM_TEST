using Microsoft.Owin.Security;
using Proyecto_ABM.Services;
using Proyecto_ABM.WebSite.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Security.Authentication;
using System.Security.Claims;

namespace Proyecto_ABM.WebSite.Services
{
    public class AdAuthenticationService
    {
        public class AuthenticationResult
        {
            public AuthenticationResult(string errorMessage = null)
            {
                ErrorMessage = errorMessage;
            }

            public String ErrorMessage { get; private set; }
            public Boolean IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        }

        private readonly IAuthenticationManager authenticationManager;

        public AdAuthenticationService(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public AuthenticationResult SignIn(string username, string password)
        {
            PrincipalContext principalContext;

            try
            {
                principalContext = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["ADDomain"]); //pasar a web config
            }
            catch (PrincipalServerDownException e)
            {
                return new AuthenticationResult("Servidor no disponible");
            }
            bool isAuthenticated = false;
            UserPrincipal userPrincipal = null;
            try
            {
                userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                if (userPrincipal != null)
                {
                    isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
                }
            }
            catch (AuthenticationException ex)
            {
                return new AuthenticationResult("Usuario o Contraseña no son correctas");
            }
            catch (Exception ex)
            {
                return new AuthenticationResult("Error inesperado");
            }
            if (!isAuthenticated || userPrincipal == null)
            {
                return new AuthenticationResult("Usuario o Contraseña no son correctas");
            }

            var identity = CreateIdentity(userPrincipal);

            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

            return new AuthenticationResult();
        }

        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.Name));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));

            var groups = userPrincipal.GetGroups();
            CustomRolManager roleManager = new CustomRolManager();
            foreach (var @role in groups)
            {
                foreach (var rol in roleManager.getRoles(@role.Name))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, rol));
                }
            }
            return identity;
        }

    }
}