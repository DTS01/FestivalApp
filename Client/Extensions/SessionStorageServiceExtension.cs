using Blazored.SessionStorage;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace FestivalApp.Client.Extensions
{
    // Extension methods for ISessionStorageService to simplify encryption and decryption of items
    public static class SessionStorageServiceExtension
    {
        // Extension method to asynchronously save an item encrypted in the session storage
        public static async Task SaveItemEncryptedAsync<T>(this ISessionStorageService sessionStorageService, string key, T item)
        {
            // Serialize the item to JSON
            var itemJson = JsonSerializer.Serialize(item);

            // Convert the JSON to UTF-8 bytes
            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);

            // Convert the UTF-8 bytes to Base64 string
            var base64Json = Convert.ToBase64String(itemJsonBytes);

            // Save the Base64-encoded string in the session storage
            await sessionStorageService.SetItemAsync(key, base64Json);
        }

        // Extension method to asynchronously read and decrypt an item from the session storage
        public static async Task<T> ReadEncryptedItemAsync<T>(this ISessionStorageService sessionStorageService, string key)
        {
            // Read the Base64-encoded string from the session storage
            var base64Json = await sessionStorageService.GetItemAsync<string>(key);

            // Convert the Base64 string to UTF-8 bytes
            var itemJsonBytes = Convert.FromBase64String(base64Json);

            // Convert the UTF-8 bytes to JSON string
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);

            // Deserialize the JSON string to the specified type
            var item = JsonSerializer.Deserialize<T>(itemJson);

            // Return the deserialized item
            return item;
        }
    }
}
