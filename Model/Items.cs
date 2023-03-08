namespace MalmöTradera.api.Model
{
    public class ItemsModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int value { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool ISSold { get; set; }
    }
}