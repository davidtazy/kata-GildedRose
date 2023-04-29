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


    public void AdvanceOneDayAndUpdateQuality()
    {
        foreach (Item item in Items)
            AdvanceOneDayUpdateQuality(item);
    }

    public static void AdvanceOneDayUpdateQuality(Item item)
    {
        if (item.Name == "Sulfuras, Hand of Ragnaros") return;

        AdvanceOneDay(item);

        int twice_as_normal = 2;
        int normal = 1;

        switch (item.Name)
        {
            case "Aged Brie":

                if (HasSellInHasPassed(item))
                {
                    IncreaseItemQualityBy(item, twice_as_normal);
                    break;
                }

                IncreaseItemQualityBy(item, normal);

                break;

            case "Backstage passes to a TAFKAL80ETC concert":

                switch (item.SellIn)
                {
                    case < 0:
                        item.Quality = 0;
                        break;
                    case < 5:
                        IncreaseItemQualityBy(item, 3);
                        break;
                    case < 10:
                        IncreaseItemQualityBy(item, 2);
                        break;
                    default:
                        IncreaseItemQualityBy(item, 1);
                        break;
                }
                break;

            case "Conjured":

                UpdateQualityForNormalItems(item, twice_as_normal);
                break;

            default:
                UpdateQualityForNormalItems(item);
                break;
        }
    }

    private static void UpdateQualityForNormalItems(Item item, int increment = 1)
    {
        if (HasSellInHasPassed(item))
        {
            DecreaseItemQualityBy(item, increment * 2);
            return;
        }
        DecreaseItemQualityBy(item, increment);
    }

    private static void AdvanceOneDay(Item item)
    {
        item.SellIn -= 1;
    }

    private static void IncreaseItemQualityBy(Item item, int increment)
    {
        item.Quality = Math.Min(item.Quality + increment, 50);
    }

    private static void DecreaseItemQualityBy(Item item, int increment)
    {
        item.Quality = Math.Max(item.Quality - increment, 0);
    }

    private static bool HasSellInHasPassed(Item item)
    {
        return item.SellIn < 0;
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