﻿namespace GildedRose.Tests
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


    }
}
