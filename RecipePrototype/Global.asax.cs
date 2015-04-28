using System.Configuration;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using RecipePrototype.Controllers;

namespace RecipePrototype
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //http://blogs.msdn.com/b/webdev/archive/2014/04/09/ef-code-first-migrations-deployment-to-an-azure-cloud-service.aspx
            if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
            {
                var configuration = new RecipePrototype.Migrations.Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }

            //http://docs.autofac.org/en/latest/integration/mvc.html
            var builder = new ContainerBuilder();
            builder.RegisterType<HomeController>().InstancePerRequest();
            builder.RegisterModule<AutofacRegistration>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
