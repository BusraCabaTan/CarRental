using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constans;
using Microsoft.AspNetCore.Http;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContexAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContexAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation ınovication)
        {
            var roleClaims = _httpContexAccessor.HttpContext.User.ClaimRoles();

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; 
                }
                throw new Exception(Messages.AuthorizationDenied);
            }
            
        }
    }
}
