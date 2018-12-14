using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswdApi.Repositories;

namespace PasswdApi.UnitTests.Repositories
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Create_ReturnsUser()
        {
            var target = new UserRepository();
            var actual = target.Create("name", "password", 1, 2, "comment", "home", "shell");

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Equals("name", 1, 2, "comment", "home", "shell"));
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_ReturnsUsers()
        {
            var target = new UserRepository();
            target.Create("name1", "password1", 1, 2, "comment1", "home1", "shell1");
            target.Create("name2", "password2", 2, 3, "comment2", "home2", "shell2");

            var actual = target.Get().ToList();

            Assert.IsNotNull(actual);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.IsTrue(actual[i].Equals("name" + (i + 1), (i + 1), (i + 2), "comment" + (i + 1), "home" + (i + 1), "shell" + (i + 1)));
            }
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenByQuery_ReturnsUsers()
        {
            var target = new UserRepository();
            target.Create("name1", "password1", 1, 2, "comment1", "home1", "shell1");
            target.Create("name2", "password2", 2, 3, "comment2", "home2", "shell2");

            var actual = target.Get("name1", null, null, null, null, "shell1").ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual[0].Equals("name1", 1, 2, "comment1", "home1", "shell1"));
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_ReturnsNoUsers()
        {
            var target = new UserRepository();
            target.Create("name1", "password1", 1, 2, "comment1", "home1", "shell1");
            target.Create("name2", "password2", 2, 3, "comment2", "home2", "shell2");

            var actual = target.Get("name3", null, null, null, null, null).ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenById_ReturnsUser()
        {
            var target = new UserRepository();
            target.Create("name1", "password1", 1, 2, "comment1", "home1", "shell1");
            target.Create("name2", "password2", 2, 3, "comment2", "home2", "shell2");

            var actual = target.GetById(1);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Equals("name1", 1, 2, "comment1", "home1", "shell1"));
        }

        [TestMethod, TestCategory("Repositories"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenById_ReturnsNoUser()
        {
            var target = new UserRepository();
            target.Create("name1", "password1", 1, 2, "comment1", "home1", "shell1");
            target.Create("name2", "password2", 2, 3, "comment2", "home2", "shell2");

            var actual = target.GetById(3);

            Assert.IsNull(actual);
        }
    }
}
