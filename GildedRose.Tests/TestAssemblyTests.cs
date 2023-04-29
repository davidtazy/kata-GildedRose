namespace GildedRose.Tests
{
    using GildedRose.Cli;
    using NUnit.Framework;

    [TestFixture]
    public class TestAssemblyTests
    {

        string[] MutableItemsNames = new string[] { "Aged Brie", "Backstage passes to a TAFKAL80ETC concert", "Dont care name" };


        [Test]
        public void ensure_no_regression_inserted_during_refactor()
        {
            var inn = new Inn();
            var inn_leg = new InnLegacy();

            Assert.That(inn_leg.ToString(), Is.EqualTo(inn.ToString()).NoClip);

            for (int i = 0; i < 16; i++)
            {
                inn.AdvanceOneDayAndUpdateQuality();
                inn_leg.UpdateQuality();

                Assert.That(inn_leg.ToString(), Is.EqualTo(inn.ToString()).NoClip);
            }
        }

        ///////////////// test common properties  /////////////////

        [Test]
        public void Items_quality_never_more_than_50()
        {
            var sell_in_list = new int[] { -1, 1, 0 };
            foreach (var name in MutableItemsNames)
            {
                foreach (int sell_in in sell_in_list)
                {
                    int quality = 50;
                    var item = new TestableItem { Name = name, SellIn = sell_in, Quality = quality };

                    Inn.AdvanceOneDayUpdateQuality(item);
                    Assert.LessOrEqual(item.Quality, quality, item.Name);
                }
            }
        }

        [Test]
        public void Items_sell_in_decreased_every_day()
        {
            var sell_in_list = new int[] { -1, 0, 1 };
            foreach (var name in MutableItemsNames)
            {
                foreach (int sell_in in sell_in_list)
                {
                    int quality = 50;
                    var item = new TestableItem { Name = name, SellIn = sell_in, Quality = quality };

                    Inn.AdvanceOneDayUpdateQuality(item);
                    Assert.AreEqual(sell_in - 1, item.SellIn);
                }
            }
        }

        [Test]
        public void Items_quality_is_never_negative()
        {
            int zero = 0;
            var sell_in_list = new int[] { -1, 0, 1 };
            foreach (var name in MutableItemsNames)
            {
                foreach (int sell_in in sell_in_list)
                {
                    var item = new TestableItem { Name = name, SellIn = sell_in, Quality = zero };

                    Inn.AdvanceOneDayUpdateQuality(item);
                    Assert.GreaterOrEqual(item.Quality, zero, item.Name);
                }
            }
        }

        [Test]
        public void for_normal_items_Quality_decrease_by_one_everyday_will_sell_in_has_not_passed()
        {
            var sell_in_list = new int[] { 10, 5, 1 };

            int quality = 10;

            foreach (int sell_in in sell_in_list)
            {
                var item = new TestableItem { Name = "dont care name", SellIn = sell_in, Quality = quality };

                Inn.AdvanceOneDayUpdateQuality(item);
                Assert.AreEqual(quality - 1, item.Quality, sell_in.ToString());
            }
        }

        [Test]
        public void for_normal_items_once_the_sell_by_date_has_passed_Quality_degrades_twice_as_fast()
        {
            var sell_in_list = new int[] { -1, 0, -2 };

            int quality = 10;

            foreach (int sell_in in sell_in_list)
            {
                var item = new TestableItem { Name = "dont care name", SellIn = sell_in, Quality = quality };

                Inn.AdvanceOneDayUpdateQuality(item);
                Assert.AreEqual(quality - 2, item.Quality, sell_in.ToString());
            }
        }

        /////////// Sulfuras tests /////////////////////// 

        [Test]
        public void Sulfuras_never_has_to_be_sold_or_decreases_in_Quality()
        {
            for (int sell_in = -1; sell_in < 20; sell_in++)
            {
                for (int quality = -1; quality < 80; quality++)
                {
                    var init = new TestableItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = sell_in, Quality = quality };
                    var item = new TestableItem(init);
                    Inn.AdvanceOneDayUpdateQuality(item);
                    Assert.AreEqual(init, item);
                }
            }
        }

        //////////////////   AGED BRIE UNIT TESTS ////////////////////////

        [Test]
        public void Aged_Brie_actually_increases_in_Quality_the_older_it_gets()
        {
            int quality = 20;
            var item = new TestableItem { Name = "Aged Brie", SellIn = 10, Quality = quality };

            Inn.AdvanceOneDayUpdateQuality(item);
            Assert.AreEqual(quality + 1, item.Quality);
        }

        [Test]
        public void Once_sell_in_has_passed_Aged_Brie_actually_increases_twice_faster()
        {
            int quality = 20;
            var item = new TestableItem { Name = "Aged Brie", SellIn = -1, Quality = quality };

            Inn.AdvanceOneDayUpdateQuality(item);
            Assert.AreEqual(quality + 2, item.Quality);
        }

        ///////////////   //////////////////////

        [Test]
        public void Backstage_passes_increases_in_Quality_as_its_SellIn_greater_than_10()
        {
            int quality = 20;
            var item = new TestableItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = quality };

            Inn.AdvanceOneDayUpdateQuality(item);
            Assert.AreEqual(quality + 1, item.Quality);
        }

        [Test]
        public void Backstage_passes_increases_by_2_in_Quality_as_its_SellIn_greater_than_5_and_less_than_11()
        {
            int quality = 20;
            var sell_in_list = new int[] { 6, 7, 8, 9, 10 };
            foreach (int sell_in in sell_in_list)
            {
                var item = new TestableItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sell_in, Quality = quality };
                Inn.AdvanceOneDayUpdateQuality(item);
                Assert.AreEqual(quality + 2, item.Quality, sell_in.ToString());
            }
        }

        [Test]
        public void Backstage_passes_increases_by_3_in_Quality_as_its_SellIn_greater_than_0_and_less_than_6()
        {
            int quality = 20;
            var sell_in_list = new int[] { 1, 2, 3, 4, 5 };
            foreach (int sell_in in sell_in_list)
            {
                var item = new TestableItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sell_in, Quality = quality };
                Inn.AdvanceOneDayUpdateQuality(item);
                Assert.AreEqual(quality + 3, item.Quality, sell_in.ToString());
            }
        }

        [Test]
        public void Backstage_passes_drop_to_0_in_Quality_as_its_SellIn_has_passed()
        {
            int quality = 20;
            var sell_in_list = new int[] { -10, -1, 0 };
            foreach (int sell_in in sell_in_list)
            {
                var item = new TestableItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sell_in, Quality = quality };
                Inn.AdvanceOneDayUpdateQuality(item);
                Assert.AreEqual(0, item.Quality, sell_in.ToString());
            }
        }


        ///////   Conjured items ////////

        [Test]
        public void Conjured_items_degrade_in_Quality_twice_as_fast_as_normal_items()
        {
            int quality = 10;

            var positive_sell_in_list = new int[] { 10, 5, 1 };
            foreach (int sell_in in positive_sell_in_list)
            {
                var item = new TestableItem { Name = "Conjured", SellIn = sell_in, Quality = quality };

                Inn.AdvanceOneDayUpdateQuality(item);
                Assert.AreEqual(quality - 2, item.Quality, sell_in.ToString());
            }

            var negative_sell_in_list = new int[] { -10, -5, -1 };
            foreach (int sell_in in negative_sell_in_list)
            {
                var item = new TestableItem { Name = "Conjured", SellIn = sell_in, Quality = quality };

                Inn.AdvanceOneDayUpdateQuality(item);
                Assert.AreEqual(quality - 4, item.Quality, sell_in.ToString());
            }
        }




    }
}
