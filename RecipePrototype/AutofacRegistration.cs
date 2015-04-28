using Autofac;
using RecipePrototype.Models;
using RecipePrototype.Services;
using RecipePrototype.Services.Interfaces;

namespace RecipePrototype
{
    public class AutofacRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RecipeRepository>().As<IRepository<Recipe>>();
        }
    }
}