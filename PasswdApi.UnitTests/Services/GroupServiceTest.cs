using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PasswdApi.Exceptions;
using PasswdApi.Models;
using PasswdApi.Repositories;
using PasswdApi.Services;

namespace PasswdApi.UnitTests.Services
{
    [TestClass]
    public class GroupServiceTest
    {
        private readonly Group _groupToReturn = new Group("name", "password", 1, "member");
        private readonly IEnumerable<Group> _groupsToReturn = new List<Group>
        {
            new Group("name1", "password1", 1, "member1"),
            new Group("name2", "password2", 2, "member2")
        };

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void CreateGroup_ReturnsGroup()
        {
            var repositoryStub = new Mock<IGroupRepository>();
            repositoryStub
                .Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                .Returns(_groupToReturn);

            var target = new GroupService(repositoryStub.Object);
            var actual = target.CreateGroup("name", "password", 1, "member");

            Assert.IsNotNull(actual);
            Assert.AreEqual(_groupToReturn, actual);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void GetGroups_ReturnsGroups()
        {
            var repositoryStub = new Mock<IGroupRepository>();
            repositoryStub
                .Setup(x => x.Get()).Returns(_groupsToReturn);

            var target = new GroupService(repositoryStub.Object);
            var actual = target.GetGroups().ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 2);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetGroups_ThrowsEntityNotFoundException()
        {
            var repositoryStub = new Mock<IGroupRepository>();
            repositoryStub
                .Setup(x => x.Get()).Returns((IEnumerable<Group>) null);

            var target = new GroupService(repositoryStub.Object);
            target.GetGroups();
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void GetGroups_WhenByQuery_ReturnsGroups()
        {
            var repositoryStub = new Mock<IGroupRepository>();
            repositoryStub
                .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns(_groupsToReturn.Where(x => x.Equals("name1", null, null)));

            var target = new GroupService(repositoryStub.Object);
            var actual = target.GetGroups("name1", null, null, null).ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetGroups_WhenByQuery_ThrowsEntityNotFoundException()
        {
            var repositoryStub = new Mock<IGroupRepository>();
            repositoryStub
                .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns((IEnumerable<Group>)null);

            var target = new GroupService(repositoryStub.Object);
            target.GetGroups("name1", null, null, null);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void GetGroupById_RteurnsGroup()
        {
            var repositoryStub = new Mock<IGroupRepository>();
            repositoryStub
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(_groupToReturn);

            var target = new GroupService(repositoryStub.Object);
            var actual = target.GetGroupById(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(_groupToReturn, actual);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetGroupById_ThrowsEntityNotFoundException()
        {
            var repositoryStub = new Mock<IGroupRepository>();
            repositoryStub
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((Group)null);

            var target = new GroupService(repositoryStub.Object);
            target.GetGroupById(3);
        }
    }
}
