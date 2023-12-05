using DDD.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DDDTest.Tests
{
    [TestClass]
    public class TemperatureTest
    {
        [TestMethod]
        public void 小数点以下2桁で丸めて表示できる()
        {
            var t = new Temperature(12.3f);
            Assert.AreEqual(12.3f, t.Value);
            Assert.AreEqual("12.30℃", t.DisplayValueWithUnit);
        }

        [TestMethod]
        public void 温度Equals()
        {
            var t1 = new Temperature(12.3f);
            var t2 = new Temperature(12.3f);
            Assert.AreEqual(true, t1.Equals(t2));
        }

        [TestMethod]
        public void 温度EqualsEquals()
        {
            var t1 = new Temperature(12.3f);
            var t2 = new Temperature(12.3f);
            Assert.AreEqual(true, t1 == t2);
        }


        [TestMethod]
        public void 値型Equals()
        {
            float f1 = 12.3f;
            float f2 = 12.3f;
            Assert.AreEqual(true, f1.Equals(f2));
        }
    }
}
