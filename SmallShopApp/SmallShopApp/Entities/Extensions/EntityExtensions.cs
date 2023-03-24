using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SmallShopApp.Entities.Extensions
{
    public static class EntityExtensions
    {
        public static T? Copy<T>(this T itemToCopy) where T : IEntity
        {
            if (itemToCopy == null)
                return default(T?);
            var json = JsonSerializer.Serialize<T>(itemToCopy);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
