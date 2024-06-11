using System.Collections;
using WiFi_Analyzer.Enums;

namespace WiFi_Analyzer.Comparers;

public class DynamicComparer<T> : IComparer<T>
{
    readonly string propertyPath;
    readonly OrderBy orderBy;

    public DynamicComparer(string propertyPath, OrderBy orderBy)
        => (this.propertyPath, this.orderBy) = (propertyPath,  orderBy);

    public int Compare(T? x, T? y)
    {
        var xValue = GetPropertyValue(x, propertyPath);
        var yValue = GetPropertyValue(y, propertyPath);

        if (xValue is null && yValue is null)
            return 0; // equal
        else if (xValue is null)
            return 1; // y > x
        else if(yValue is null)
            return -1; // x > y

        int result = Comparer.Default.Compare(xValue, yValue);
        return orderBy == OrderBy.Ascending ? result : -result;
    }
    
    static object? GetPropertyValue(T? model, string propertyPath)
    {
        if (model is null)
            return null;

        object? propertyValue = model;

        string[] properties = propertyPath.Split('.');

        foreach (string property in properties)
        {
            if (propertyValue is null)
                return null;

            var propertyInfo = propertyValue!.GetType().GetProperty(property);
            if (propertyInfo is null)
                return null;

            propertyValue = propertyInfo.GetValue(propertyValue);
        }

        return propertyValue;
    }
}
