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
    }
}
