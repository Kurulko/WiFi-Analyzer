using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using WiFi_Analyzer.Enums;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Comparers;

namespace WiFi_Analyzer.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> models, string attribute, OrderBy orderBy)
        => models.AsQueryable().OrderBy($"{attribute} {orderBy}");

    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> models, string attribute, OrderBy orderBy, IComparer comparer)
        => models.AsQueryable().OrderBy($"{attribute} {orderBy}", comparer);

    public static IEnumerable<T> DynamicOrderBy<T>(this IEnumerable<T> models, string attribute, OrderBy orderBy)
    {
        IComparer<T> comparer = new DynamicComparer<T>(attribute, orderBy);
        return models.OrderBy(m => m, comparer);
    }
}

