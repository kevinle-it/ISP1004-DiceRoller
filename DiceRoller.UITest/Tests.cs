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
            // Halt the test and enable console environment tool to inspect
            // the app ui layout structure against xamarin.uitest platform
            // and experiment with UITest expressions dynamically.
            // app.Repl();
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

        [Test]
        [Category("UI")]
        public void RollButtonsAreDisplayed()
        {
            //Assert.IsTrue(app.Query("Display one result").Any());
            //Assert.IsTrue(app.Query("Display two results").Any());

            AppResult[] results = app.Query(c => c.Property("text").Like("Display * result*"));
            //AppResult[] results = app.Query(c => c.Property("text").Contains("Display "));
            //AppResult[] results = app.Query(c => c.Property("text").StartsWith("Display "));
            Assert.IsTrue(results.Length == 2);
        }

        [Test]
        [Category("UI")]
        public void DisplayOneResultButtonClickShouldShowOnlyOneRelevantResult()
        {
            // -------------------- Case: use D4 die --------------------
            app.Tap(c => c.Marked("d4"));
            Assert.IsTrue(app.Query(c =>
                c.Marked("d4")          // look for items marked d4
                .Invoke("isChecked"))   // call the isChecked method of the RadioButton
                .FirstOrDefault()       // get the first result (there should only be one)
                .Equals(true)           // check that the view is checked (checked == true)
            );
            app.Tap(c => c.Button("Display one result"));

            // Get Result1 Label
            AppResult result1 = app.Query(c =>
                    c.Property("ContentDescription")    // ContentDescription <=> AutomationId
                    .Like("Die_Rolling_Result_1")
                ).FirstOrDefault();
            Assert.IsNotNull(result1);
            int rollResult1 = 0;
            int.TryParse(result1.Text, out rollResult1);
            Assert.IsTrue(rollResult1 >= 1 && rollResult1 <= 4);

            // Get Result2 Label
            AppResult[] result2 = app.Query(
                    c => c.Property("ContentDescription")
                    .Like("Die_Rolling_Result_2")
                );
            Assert.IsFalse(result2.Any());  // Result2 Label should not be exist

            // -------------------- Case: use D20 die --------------------
            app.Tap(c => c.Marked("d20"));
            Assert.IsTrue(app.Query(c =>
                c.Marked("d20")         // look for items marked d20
                .Invoke("isChecked"))   // call the isChecked method of the RadioButton
                .FirstOrDefault()       // get the first result (there should only be one)
                .Equals(true)           // check that the view is checked (checked == true)
            );
            app.Tap(c => c.Button("Display one result"));

            // Get Result1 Label
            result1 = app.Query(c =>
                    c.Property("ContentDescription")    // ContentDescription <=> AutomationId
                    .Like("Die_Rolling_Result_1")
                ).FirstOrDefault();
            Assert.IsNotNull(result1);
            rollResult1 = 0;
            int.TryParse(result1.Text, out rollResult1);
            Assert.IsTrue(rollResult1 >= 1 && rollResult1 <= 20);

            // Get Result2 Label
            result2 = app.Query(
                    c => c.Property("ContentDescription")
                    .Like("Die_Rolling_Result_2")
                );
            Assert.IsFalse(result2.Any());  // Result2 Label should not be exist
        }

        [Test]
        [Category("UI")]
        public void DisplayTwoResultsButtonClickShouldShowTwoRelevantResults()
        {
            // -------------------- Case: use D4 die --------------------
            app.Tap(c => c.Marked("d4"));
            Assert.IsTrue(app.Query(c =>
                c.Marked("d4")          // look for items marked d4
                .Invoke("isChecked"))   // call the isChecked method of the RadioButton
                .FirstOrDefault()       // get the first result (there should only be one)
                .Equals(true)           // check that the view is checked (checked == true)
            );
            app.Tap(c => c.Button("Display two results"));

            // Get Result1 Label
            AppResult result1 = app.Query(c =>
                    c.Property("ContentDescription")    // ContentDescription <=> AutomationId
                    .Like("Die_Rolling_Result_1")
                ).FirstOrDefault();
            Assert.IsNotNull(result1);
            int rollResult1 = 0;
            int.TryParse(result1.Text, out rollResult1);
            Assert.IsTrue(rollResult1 >= 1 && rollResult1 <= 4);

            // Get Result2 Label
            AppResult result2 = app.Query(
                    c => c.Property("ContentDescription")
                    .Like("Die_Rolling_Result_2")
                ).FirstOrDefault();
            Assert.IsNotNull(result2);
            int rollResult2 = 0;
            int.TryParse(result2.Text, out rollResult2);
            Assert.IsTrue(rollResult2 >= 1 && rollResult2 <= 4);  // Result2 Label should be displayed

            // -------------------- Case: use D20 die --------------------
            app.Tap(c => c.Marked("d20"));
            Assert.IsTrue(app.Query(c =>
                c.Marked("d20")         // look for items marked d20
                .Invoke("isChecked"))   // call the isChecked method of the RadioButton
                .FirstOrDefault()       // get the first result (there should only be one)
                .Equals(true)           // check that the view is checked (checked == true)
            );
            app.Tap(c => c.Button("Display two results"));

            // Get Result1 Label
            result1 = app.Query(c =>
                    c.Property("ContentDescription")    // ContentDescription <=> AutomationId
                    .Like("Die_Rolling_Result_1")
                ).FirstOrDefault();
            Assert.IsNotNull(result1);
            rollResult1 = 0;
            int.TryParse(result1.Text, out rollResult1);
            Assert.IsTrue(rollResult1 >= 1 && rollResult1 <= 20);

            // Get Result2 Label
            result2 = app.Query(
                    c => c.Property("ContentDescription")
                    .Like("Die_Rolling_Result_2")
                ).FirstOrDefault();
            Assert.IsNotNull(result2);
            rollResult2 = 0;
            int.TryParse(result2.Text, out rollResult2);
            Assert.IsTrue(rollResult2 >= 1 && rollResult2 <= 20);  // Result2 Label should be displayed
        }
    }
}
