using Coldairarrow.Business.Common;
using Coldairarrow.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Coldairarrow.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new PostDataToSjdzp().GetTickets();

            //new TimerHelper().SetInterval(PostDataToSjdzp.GetTickets, new TimeSpan(24, 0, 0), new TimeSpan(100));
        }
    }
}
