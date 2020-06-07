using Coldairarrow.Business.Common;
using Coldairarrow.Business.LuTuTravel;
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
            new TimerBusiness().GetTickets();
        }

    }
}
