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
    }
}

