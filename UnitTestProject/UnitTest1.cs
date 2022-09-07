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

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
