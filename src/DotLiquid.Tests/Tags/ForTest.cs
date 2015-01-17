using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DotLiquid.Tests.Tags
{
    [TestFixture]
    public class ForTest
    {
        [Test]
        public void TestFor1()
        {
            Template t = Template.Parse("{% for a in (1..aa.size) %}b{% endfor %}");
            Assert.AreEqual("bbbb",
                t.Render(Hash.FromAnonymousObject(new {aa = new List<string>() {"t1", "t2", "t3", "t4"}})));
        }

        [Test]
        public void TestFor2()
        {
            Template t = Template.Parse("{% for a in aa %}b{% endfor %}");
            Assert.AreEqual("bbbb",
                t.Render(Hash.FromAnonymousObject(new { aa = new List<string>() { "t1", "t2", "t3", "t4" } })));
        }
    }
}
