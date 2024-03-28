using System.Text;

namespace MediClubApp.Extensions;

public static class EnumerableExtensions
{
    public static string AsHtml<T>(this IEnumerable<T> collection) {
        Type itemType = typeof(T);
        var properties = itemType.GetProperties();

        var builder = new StringBuilder();

        foreach (var item in collection)
        {
            builder.Append("<div>");
            foreach (var itemPropertyInfo in properties)
            {
                builder.Append($"<p> <i>{itemPropertyInfo.Name}: </i>{itemPropertyInfo.GetValue(item)} </p>");
            }
            builder.Append("</div><hr>");
        }

        return builder.ToString();
    }
}