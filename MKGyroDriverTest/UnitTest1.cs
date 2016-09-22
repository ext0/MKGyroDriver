using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MKGyroDriver;

namespace MKGyroDriverTest
{
    [TestClass]
    public class GyroTest
    {
        [TestMethod]
        public void RegisterHubTest()
        {
            Assert.IsTrue(Gyro.RegisterHub());
            Assert.IsTrue(Gyro.RegisterGyroscope());
        }
    }
}
