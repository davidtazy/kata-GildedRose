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
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 1;
                }

            }
            return;
        }

        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;


                if (item.SellIn < 11)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }

                if (item.SellIn < 6)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
            }
            item.SellIn = item.SellIn - 1;
            if (item.SellIn < 0)
            {
                item.Quality = item.Quality - item.Quality;
            }
            return;
        }


        if (item.Quality > 0)
        {
            item.Quality = item.Quality - 1;
        }



        item.SellIn = item.SellIn - 1;


        if (item.SellIn < 0)
        {
            if (item.Quality > 0)
            {

                item.Quality = item.Quality - 1;

            }
        }
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