using Entity.Models;

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
}