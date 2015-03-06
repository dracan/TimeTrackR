using System.Linq;
using NUnit.Framework;
using TimeTrackR.Core.Tags;

namespace TimeTrackR.Core.Tests.Tags
{
    [TestFixture]
    class TagSetProviderTests
    {
        [Test]
        public void AddTagCreatesNewEntry()
        {
            var tsp = new TagSetProvider();

            tsp.AddTag(new Tag {Name = "mytag"});

            var tags = tsp.GetCurrentTagSet().ToList();

            Assert.That(tags, Is.Not.Null);
            Assert.That(tags.Count(), Is.EqualTo(1));
            Assert.That(tags[0].Name, Is.EqualTo("mytag"));
        }

        [Test]
        public void ClearEmptiesList()
        {
            var tsp = new TagSetProvider();

            tsp.AddTag(new Tag {Name = "mytag"});
            tsp.Clear();

            var tags = tsp.GetCurrentTagSet().ToList();

            Assert.That(tags, Is.Not.Null);
            Assert.That(tags.Count(), Is.EqualTo(0));
        }

        [Test]
        public void AddFromDelimitedStringWithSingleEntry()
        {
            var tsp = new TagSetProvider();

            tsp.AddFromDelimitedString("mytag");

            var tags = tsp.GetCurrentTagSet().ToList();

            Assert.That(tags, Is.Not.Null);
            Assert.That(tags.Count(), Is.EqualTo(1));
            Assert.That(tags[0].Name, Is.EqualTo("mytag"));
        }

        [Test]
        public void AddFromDelimitedStringWithTwoEntriesAndNoSpacesAfterComma()
        {
            var tsp = new TagSetProvider();

            tsp.AddFromDelimitedString("myfirsttag,mysecondtag");

            var tags = tsp.GetCurrentTagSet().ToList();

            Assert.That(tags, Is.Not.Null);
            Assert.That(tags.Count(), Is.EqualTo(2));
            Assert.That(tags[0].Name, Is.EqualTo("myfirsttag"));
            Assert.That(tags[1].Name, Is.EqualTo("mysecondtag"));
        }

        [Test]
        public void AddFromDelimitedStringWithTwoEntriesAndSpaceAfterComma()
        {
            var tsp = new TagSetProvider();

            tsp.AddFromDelimitedString("myfirsttag, mysecondtag");

            var tags = tsp.GetCurrentTagSet().ToList();

            Assert.That(tags, Is.Not.Null);
            Assert.That(tags.Count(), Is.EqualTo(2));
            Assert.That(tags[0].Name, Is.EqualTo("myfirsttag"));
            Assert.That(tags[1].Name, Is.EqualTo("mysecondtag"));
        }
    }
}