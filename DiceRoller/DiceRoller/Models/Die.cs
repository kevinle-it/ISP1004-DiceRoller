using System;
namespace DiceRoller.Models
{
    public class Die
    {
        // name, type, or colour of the die
        public string Name { get; set; }

        // how many sides are the die
        public int NumSides { get; set; }

        // which number is currently up
        public int CurrentSide { get; set; }

        public Die()
        {
        }
    }
}

