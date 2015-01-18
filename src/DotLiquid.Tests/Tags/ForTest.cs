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

        [Test]
        public void TestFor3()
        {
            Template t = Template.Parse("{% for a in (0..aa.sizeminusone) %}{{ aa[a] }}{% endfor %}");
            Assert.AreEqual("t1t2t3t4",
                t.Render(Hash.FromAnonymousObject(new { aa = new List<string>() { "t1", "t2", "t3", "t4" } })));
        }

        [Test]
        public void TestFor4()
        {
            Template t = Template.Parse("{% assign q=5 %}{% assign w=10 %}{% for a in (q..w) %}{{ a }}{% endfor %}");
            Assert.AreEqual("5678910",
                t.Render(Hash.FromAnonymousObject(new { aa = new List<string>() { "t1", "t2", "t3", "t4" } })));
        }
    }
}
