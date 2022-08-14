using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace DiceRoller.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            return ConfigureApp
                    .Android
                    .InstalledApp("ca.mycambrian.a00249054.diceroller")
                    .StartApp();
        }
    }
}
