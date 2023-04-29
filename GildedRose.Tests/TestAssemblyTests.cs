namespace GildedRose.Tests
{
    using GildedRose.Cli;
    using NUnit.Framework;

    [TestFixture]
    public class TestAssemblyTests
    {
        [Test]
        public void ensure_no_regression_inserted_during_refactor()
        {
            var inn = new Inn();
            var inn_leg = new InnLegacy();

            System.Console.WriteLine(inn);
            System.Console.WriteLine(inn_leg);

            Assert.That(inn_leg.ToString(), Is.EqualTo(inn.ToString()).NoClip);

            for (int i = 0; i < 16; i++)
            {
                inn.UpdateQuality();
                inn_leg.UpdateQuality();

                Assert.That(inn_leg.ToString(), Is.EqualTo(inn.ToString()).NoClip);
            }
        }

        [Test]
        public void Sulfuras_never_has_to_be_sold_or_decreases_in_Quality()
        {
            for (int sell_in = -1; sell_in < 20; sell_in++)
            {
                for (int quality = -1; quality < 80; quality++)
                {
                    var init = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = sell_in, Quality = quality };
                    var item = new Item(init);
                    Inn.UpdateQuality(item);
                    Assert.AreEqual(init, item);
                }
            }
        }



        [Test]
        public void Aged_Brie_actually_increases_in_Quality_the_older_it_gets()
        {
            int quality = 20;
            var item = new Item { Name = "Aged Brie", SellIn = 10, Quality = quality };

            Inn.UpdateQuality(item);
            Assert.AreEqual(quality + 1, item.Quality);
        }

        [Test]
        public void Once_sell_in_has_passed_Aged_Brie_actually_increases_twice_faster()
        {
            int quality = 20;
            var item = new Item { Name = "Aged Brie", SellIn = -1, Quality = quality };

            Inn.UpdateQuality(item);
            Assert.AreEqual(quality + 2, item.Quality);
        }

        [Test]
        public void Aged_Brie_quality_never_more_than_50()
        {
            var sell_in_list = new int[] { -1, 0, 1 };
            foreach (int sell_in in sell_in_list)
            {
                int quality = 50;
                var item = new Item { Name = "Aged Brie", SellIn = sell_in, Quality = quality };

                Inn.UpdateQuality(item);
                Assert.AreEqual(quality, item.Quality);
            }
        }


    }
}
