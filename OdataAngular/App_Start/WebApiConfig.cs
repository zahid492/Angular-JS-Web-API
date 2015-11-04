using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.Owin.BuilderProperties;


using ODataConventionModelBuilder = System.Web.Http.OData.Builder.ODataConventionModelBuilder;

namespace OdataAngular
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            


            EnableCorsAttribute provider = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(provider);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

         //   config.MapODataServiceRoute("odata", "odata", model:GetEdmModel());
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Basic_Information>("Basic_Information1");
            builder.EntitySet<Class>("Classes");
            builder.EntitySet<Department>("Departments");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());


            //Database.SetInitializer<DomainModel>(new DropCreateDatabaseIfModelChanges<DomainModel>());

        }


            
        }
    }

