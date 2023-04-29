
public class Item
{
    public Item(Item item)
    {
        Name = item.Name;
        SellIn = item.SellIn;
        Quality = item.Quality;
    }

    public Item()
    {
        Name = "";
    }

    public string Name { get; set; }

    public int SellIn { get; set; }

    public int Quality { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Item item &&
               Name == item.Name &&
               SellIn == item.SellIn &&
               Quality == item.Quality;
    }
}