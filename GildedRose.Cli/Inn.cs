namespace GildedRose.Cli;

public class Inn
{
    public IList<Item> Items { get; }

    public Inn()
    {
        Items = new List<Item>
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
            };
    }


    public void UpdateQuality()
    {
        foreach (Item item in Items)
            UpdateQuality(item);
    }

    public static void UpdateQuality(Item item)
    {
        if (item.Name == "Sulfuras, Hand of Ragnaros")
            return;

        if (item.Name == "Aged Brie")
        {
            IncreaseItemQualityBy(item, 1);

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0) IncreaseItemQualityBy(item, 1);

            return;
        }

        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
        {
            if (item.SellIn < 6) IncreaseItemQualityBy(item, 3);
            else if (item.SellIn < 11) IncreaseItemQualityBy(item, 2);
            else IncreaseItemQualityBy(item, 1);

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0) item.Quality = 0;

            return;
        }

        if (item.Name == "Conjured")
        {
            int factor = 2;
            NormalItemStrategy(item, factor);
            return;
        }

        NormalItemStrategy(item);

    }

    private static void NormalItemStrategy(Item item, int factor = 1)
    {
        DecreaseItemQualityBy(item, factor);

        item.SellIn -= 1;

        if (item.SellIn < 0) DecreaseItemQualityBy(item, factor);
    }

    private static void IncreaseItemQualityBy(Item item, int increment)
    {
        item.Quality = Math.Min(item.Quality + increment, 50);
    }

    private static void DecreaseItemQualityBy(Item item, int increment)
    {
        item.Quality = Math.Max(item.Quality - increment, 0);
    }

    public override string ToString()
    {
        string ret = "";
        foreach (Item item in Items)
        {
            ret += $"{item.Name}/{item.SellIn}/{item.Quality}\n";
        }
        return ret;
    }


}