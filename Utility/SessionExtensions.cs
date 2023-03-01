using System.Text.Json;

namespace Beml.ECommerce.App.Utility
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, string value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T Get<T>(this ISession session, string key)
        {
            string readVal = session.GetString(key ?? default);

            return JsonSerializer.Deserialize<T>(readVal ?? default);
        }
    }
}