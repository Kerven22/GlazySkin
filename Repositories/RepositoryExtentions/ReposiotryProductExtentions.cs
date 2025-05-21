using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Entity.Models;
using Repositories.RepositoryExtentions.Utility;

namespace Repositories.RepositoryExtentions;

public static class ReposiotryProductExtentions
{
    public static IQueryable<Product> FilterProducts(this IQueryable<Product> products, uint costMax, uint costMin) =>
        products.Where(p => (p.Cost >= costMin && p.Cost <= costMax));

    public static IQueryable<Product> Search(this IQueryable<Product> products, string serachTerm)
    {
        if (string.IsNullOrEmpty(serachTerm))
            return products;
        var lowerCaseTerm = serachTerm.Trim().ToLower();
        return products.Where(p => p.Name.ToLower().Contains(serachTerm)); 
    }

    public static IQueryable<Product> Sorting(this IQueryable<Product> products, string orderByQueryString)
    {
        if (string.IsNullOrEmpty(orderByQueryString))
            return products.OrderBy(p => p.Name);
        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString);
        if (string.IsNullOrWhiteSpace(orderQuery))
            return products.OrderBy(p => p.Name);

        return products.OrderBy(orderQuery);
    }
}