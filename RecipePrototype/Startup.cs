using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipePrototype.Startup))]
namespace RecipePrototype
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
