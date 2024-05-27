using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Enums;

namespace WiFi_Analyzer.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> models, string attribute, OrderBy orderBy)
        => models.AsQueryable().OrderBy($"{attribute} {orderBy}");
}