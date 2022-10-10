namespace ETrade.Application.Extensions;

public static class GetEnumValueByDescriptionExtensions
{
    public static T GetEnumValueByDescription<T>(this string description) where T : Enum
    {
        foreach (Enum enumItem in Enum.GetValues(typeof(T)))
        {
            if (enumItem.GetEnumDescription() == description)
            {
                return (T)enumItem;
            }
        }
        throw new ArgumentException("Bulunamadı.", nameof(description));
    }
}