

using System.Text.Json;

namespace FoodDelivery.FrontEnd
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
        public static void Remove(this ISession session, string key)
        {
            session.Remove(key);
        }
    }
}
