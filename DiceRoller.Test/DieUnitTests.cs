using DiceRoller.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiceRoller.Test
{
    [TestClass]
    public class DieUnitTests
    {
        private Die def = new Die();

        [TestMethod]
        public void DieNotNull()
        {
            def.Should().NotBeNull();
        }

        [TestMethod]
        public void DieHasAllDefaultValues()
        {
            /*
             * Default values should be:
             * name:        d6
             * numSides:    6
             * currentSide: ???
             */
            def.Name.Should().Be("d6");
            def.NumSides.Should().Be(6);
            def.CurrentSide.Should().BeInRange(1, 6);
        }

        [TestMethod]
        public void DefaultRollSetsSideCorrectly()
        {
            for (int i = 0; i < 1000; i++)
            {
                def.Roll();
                def.CurrentSide.Should().BeInRange(1, 6);
            }
        }

        [TestMethod]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(8)]
        [DataRow(10)]
        [DataRow(12)]
        [DataRow(20)]
        public void RollSetsSideCorrectlyForCustomSides(int sides)
        {
            Die d = new Die(sides);
            for (int i = 0; i < 1000; i++)
            {
                d.Roll();
                d.CurrentSide.Should().BeInRange(1, sides);
            }
        }

        [TestMethod]
        [DataRow(3, "d3")]
        [DataRow(4, "d4")]
        [DataRow(8, "d8")]
        [DataRow(10, "d10")]
        [DataRow(12, "d12")]
        [DataRow(20, "d20")]
        public void DieHasCustomSides(int sides, string name)
        {
            Die d = new Die(sides);
            d.Name.Should().Be(name);
            d.NumSides.Should().Be(sides);
            d.CurrentSide.Should().BeInRange(1, sides);
        }

        [TestMethod]
        [DataRow(3, "a")]
        [DataRow(4, "b")]
        [DataRow(8, "c")]
        [DataRow(10, "d")]
        [DataRow(12, "e")]
        [DataRow(20, "f")]
        public void DieHasCustomName(int sides, string name)
        {
            Die d = new Die(sides, name);
            d.Name.Should().Be(name);
            d.NumSides.Should().Be(sides);
            d.CurrentSide.Should().BeInRange(1, sides);
        }

        [TestMethod]
        [DataRow(3, 2)]
        [DataRow(4, 2)]
        [DataRow(8, 2)]
        [DataRow(10, 2)]
        [DataRow(12, 2)]
        [DataRow(20, 2)]
        public void SetSideUpChangesSide(int sides, int newSide)
        {
            Die d = new Die(sides);
            d.SetSideUp(newSide);
            d.CurrentSide.Should().Be(newSide);
        }

        [TestMethod]
        [DataRow(3, -1)]
        [DataRow(4, 0)]
        [DataRow(8, 3)]
        [DataRow(10, 5)]
        [DataRow(12, 15)]
        [DataRow(20, 20)]
        public void SetSideUpSetsValidSide(int sides, int newSide)
        {
            Die d = new Die(sides);
            d.SetSideUp(newSide);
            if (newSide < 1 || newSide > sides)
            {
                d.CurrentSide.Should().BeInRange(1, sides);
            }
            else
            {
                d.CurrentSide.Should().Be(newSide);
            }
        }

        [TestMethod]
        [DataRow(-6, "d6", 6)]
        [DataRow(-3, "d6", 6)]
        [DataRow(0, "d6", 6)]
        [DataRow(10, "d10", 10)]
        [DataRow(12, "d12", 12)]
        [DataRow(20, "d20", 20)]
        public void NumSidesShouldNotBeNegative(int sides, string name, int expectedNumSides)
        {
            Die d = new Die(sides);
            d.Name.Should().Be(name);
            d.NumSides.Should().Be(expectedNumSides);
            d.CurrentSide.Should().BeInRange(1, expectedNumSides);
        }
    }
}

