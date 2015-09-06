﻿using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;

namespace ASPNetMVC5Identity
{
    //To initialize the OWIN identity components we need to add a Startup class to the project.
    //This class should have a method Configuration that takes an OWIN IAppBuilder instance as a parameter. 
    public class Startup
    {
        //This class will be automatically located and initialized by the OWIN host
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType =  DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/auth/login")
            });
        }
    }
}