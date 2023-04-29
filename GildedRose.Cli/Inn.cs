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

    public static void UpdateQuality(Item v)
    {
        if (v.Name != "Aged Brie" && v.Name != "Backstage passes to a TAFKAL80ETC concert")
        {
            if (v.Quality > 0)
            {
                if (v.Name != "Sulfuras, Hand of Ragnaros")
                {
                    v.Quality = v.Quality - 1;
                }
            }
        }
        else
        {
            if (v.Quality < 50)
            {
                v.Quality = v.Quality + 1;

                if (v.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (v.SellIn < 11)
                    {
                        if (v.Quality < 50)
                        {
                            v.Quality = v.Quality + 1;
                        }
                    }

                    if (v.SellIn < 6)
                    {
                        if (v.Quality < 50)
                        {
                            v.Quality = v.Quality + 1;
                        }
                    }
                }
            }
        }

        if (v.Name != "Sulfuras, Hand of Ragnaros")
        {
            v.SellIn = v.SellIn - 1;
        }

        if (v.SellIn < 0)
        {
            if (v.Name != "Aged Brie")
            {
                if (v.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (v.Quality > 0)
                    {
                        if (v.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            v.Quality = v.Quality - 1;
                        }
                    }
                }
                else
                {
                    v.Quality = v.Quality - v.Quality;
                }
            }
            else
            {
                if (v.Quality < 50)
                {
                    v.Quality = v.Quality + 1;
                }
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