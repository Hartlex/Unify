using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unify.Helpers;
using UnifyTestConsole;

namespace UnifyTest
{
    [TestClass]
    public class UnifyPerformance
    {
        [TestMethod]
        public void GeneralReadWrite()
        {
            var basicInfo = new BasicInfo("Info1",100f,200f);

            for (int i = 0; i < 10000; i++)
            {
                var buffer = new ByteBuffer();
                basicInfo.Serialize<BasicInfo>(ref buffer);
                var data = buffer.GetData();

                var buffer2 = new ByteBuffer(data);
                var newBasicInfo = new BasicInfo();
                newBasicInfo.Deserialize<BasicInfo>(ref buffer2);
                Console.Out.WriteLine(newBasicInfo);
            }
        }
        [TestMethod]
        public void SpecificReadWrite()
        {
            var basicInfo2 = new BasicInfo2("Info1", 100f, 200f);
            for (int i = 0; i < 10000; i++)
            {
                var buffer = new ByteBuffer();
                basicInfo2.Serialize<BasicInfo2>(ref buffer);
                var data = buffer.GetData();

                var buffer2 = new ByteBuffer(data);
                var newBasicInfo = new BasicInfo2(ref buffer2);
                Console.Out.WriteLine(newBasicInfo);

            }
        }

        [TestMethod]
        public void GeneralWrite()
        {
            var basicInfo = new BasicInfo("Info1",100f,200f);

            for (int i = 0; i < 10000; i++)
            {
                var buffer = new ByteBuffer();
                basicInfo.Serialize<BasicInfo>(ref buffer);
                var data = buffer.GetData();
            }
        }
        [TestMethod]
        public void SpecificWrite()
        {
            var basicInfo2 = new BasicInfo2("Info1", 100f, 200f);
            for (int i = 0; i < 10000; i++)
            {
                var buffer = new ByteBuffer();
                basicInfo2.Serialize<BasicInfo2>(ref buffer);
                var data = buffer.GetData();

            }
        }
    }
}
