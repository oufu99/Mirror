using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Model;

namespace WebCore.Services
{
    public class AuthService
    {

        private readonly RequestDelegate _next;
        public AuthService(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, Person p)
        {
            //p.Age = 88;
            return _next(httpContext);

        }
    }
}
