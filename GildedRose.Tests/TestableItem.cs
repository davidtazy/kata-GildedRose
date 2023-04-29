namespace GildedRose.Cli;

class TestableItem : Item
{
    public TestableItem(Item item)
    {
        Name = item.Name;
        SellIn = item.SellIn;
        Quality = item.Quality;
    }

    public TestableItem()
    {
        Name = "";
    }

    public override bool Equals(object? obj)
    {
        return obj is Item item &&
               Name == item.Name &&
               SellIn == item.SellIn &&
               Quality == item.Quality;
    }

}