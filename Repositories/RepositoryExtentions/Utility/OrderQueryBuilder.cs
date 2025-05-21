using System.Reflection;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace Repositories.RepositoryExtentions.Utility;

public static class OrderQueryBuilder
{
    public static string CreateOrderQuery<T>(string orderByQeryString)
    {
        var orderParams = orderByQeryString.Trim().Split(',');
        var properyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if(string.IsNullOrWhiteSpace(param))
                continue;
            var propertyFromQuery = param.Split(" ")[0];
            var objectProperty = properyInfos.FirstOrDefault(p =>
                p.Name.Equals(propertyFromQuery, StringComparison.InvariantCultureIgnoreCase));
            if(objectProperty is null)
                continue;

            var direction = param.EndsWith("desc") ? "descending" : "ascending";

            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
        }
        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',');
        return orderQuery;
    }
}