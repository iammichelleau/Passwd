using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswdApi.Repositories;

namespace PasswdApi.UnitTests.Repositories
{
    [TestClass]
    public class GroupRepositoryTest
    {
        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Create_ReturnsGroup()
        {
            var target = new GroupRepository();
            var actual = target.Create("name", "password", 1, "member1", "member2");

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Equals("name", 1, "member1", "member2"));
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_ReturnsGroups()
        {
            var target = new GroupRepository();
            target.Create("name1", "password1", 1, "member1");
            target.Create("name2", "password2", 2, "member2");

            var actual = target.Get().ToList();

            Assert.IsNotNull(actual);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.IsTrue(actual[i].Equals("name" + (i + 1), (i + 1), "member" + (i + 1)));
            }
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenByQuery_ReturnsGroups()
        {
            var target = new GroupRepository();
            target.Create("name1", "password1", 1, "member1");
            target.Create("name2", "password2", 2, "member2");

            var actual = target.Get("name1", null, null).ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual[0].Equals("name1", 1, "member1"));
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenByQuery_ReturnsNoGroups()
        {
            var target = new GroupRepository();
            target.Create("name1", "password1", 1, "member1");
            target.Create("name2", "password2", 2, "member2");

            var actual = target.Get("name3", null, null).ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenById_ReturnsGroup()
        {
            var target = new GroupRepository();
            target.Create("name1", "password1", 1, "member1");
            target.Create("name2", "password2", 2, "member2");

            var actual = target.GetById(1);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Equals("name1", 1, "member1"));
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenById_ReturnsNoGroup()
        {
            var target = new GroupRepository();
            target.Create("name1", "password1", 1, "member1");
            target.Create("name2", "password2", 2, "member2");

            var actual = target.GetById(3);

            Assert.IsNull(actual);
        }
    }
}
