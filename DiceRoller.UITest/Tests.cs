using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace DiceRoller.UITest
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        //[Test]
        //public void AppLaunches()
        //{
        //    app.Screenshot("First screen.");
        //}

        [Test]
        [Category("UI")]
        public void PromptLabelIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Select a die:"));

            Assert.IsTrue(results.Any());
        }

        [Test]
        [Category("UI")]
        public void OptionsAreDisplayed()
        {
            // Single line option
            Assert.IsTrue(app.Query(c => c.Marked("d4")).Any());
            Assert.IsTrue(app.Query(c => c.Marked("d6")).Any());
            Assert.IsTrue(app.Query(c => c.Marked("d8")).Any());
            Assert.IsTrue(app.Query(c => c.Marked("d10")).Any());
            Assert.IsTrue(app.Query(c => c.Marked("d12")).Any());

            // Multi-line option
            AppResult[] results = app.WaitForElement(c => c.Marked("d20"));
            Assert.IsTrue(results.Any());
        }

        [Test]
        [Category("UI")]
        public void OptionsCanBeChecked()
        {
            app.Tap(c => c.Marked("d4"));

            Assert.IsTrue(app.Query(c =>
                c.Marked("d4")          // look for items marked d4
                .Invoke("isChecked"))   // call the isChecked method of the RadioButton
                .FirstOrDefault()       // get the first result (there should only be one)
                .Equals(true)           // check that the view is checked (checked == true)
            );

            app.Tap(c => c.Marked("d6"));
            Assert.IsTrue(app.Query(c =>
                c.Marked("d6")          // look for items marked d6
                .Invoke("isChecked"))   // call the isChecked method of the RadioButton
                .FirstOrDefault()       // get the first result (there should only be one)
                .Equals(true)           // check that the view is checked (checked == true)
            );
            Assert.IsTrue(app.Query(c =>
                c.Marked("d4")          // look for items marked d4
                .Invoke("isChecked"))   // call the isChecked method of the RadioButton
                .FirstOrDefault()       // get the first result (there should only be one)
                .Equals(false)           // check that the view is checked (checked == false)
            );
        }
    }
}
