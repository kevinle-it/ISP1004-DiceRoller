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
            Name = "d6";
            NumSides = 6;
            Roll();
        }

        public Die(int numSides) : this()
        {
            if (numSides >= 1)
            {
                Name = "d" + numSides;
                NumSides = numSides;
                Roll();
            }
        }

        public Die(int numSides, string name)
        {
            Name = name;
            NumSides = numSides;
            Roll();
        }

        public void SetSideUp(int newSideUp)
        {
            if (newSideUp >= 1 && newSideUp <= NumSides)
            {
                CurrentSide = newSideUp;
            }
        }

        public void Roll()
        {
            Random r = new Random();
            CurrentSide = r.Next(NumSides) + 1;
        }
    }
}

