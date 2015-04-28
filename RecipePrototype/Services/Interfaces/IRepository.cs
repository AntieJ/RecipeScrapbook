using System.Collections.Generic;
using System.Linq;
using RecipePrototype.Models;

namespace RecipePrototype.Services.Interfaces
{
    public interface IRepository<T>
    {
        T Create(T item);
        IEnumerable<T> Get();
        IEnumerable<T> Get(int skip, int take);
        PagedItem<T> GetPaged(int pageSize, int pageNumber);
        IQueryable<Recipe> GetQueryable();
        T Get(int? id);
        T Update(T item);
        int Delete(int id);
    }
}