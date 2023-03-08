using System.Text.Json;
using MalmöTradera.api.Model;
namespace MalmöTradera.api.Data
{
    public class SeedData
    {
        public static async Task LoadItems (MalmöTraderaContext context)
        {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Item.Any()) return;

            var Json = System.IO.File.ReadAllText("Data/Json/items.json");

            var Items = JsonSerializer.Deserialize<List<ItemsModel>>(Json, options);

            if (Items is not null && Items.Count() > 0)
            {
                await context.AddRangeAsync(Items);
                await context.SaveChangesAsync();
            }
        }
    }
}