using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WpfApplication;
using System.Linq;
using UnitTestProject.Driver;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        AppDriver _app;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = new AppDriver();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _app.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var entry = _app.Main;

            entry.button_click();
        }
    }
}
