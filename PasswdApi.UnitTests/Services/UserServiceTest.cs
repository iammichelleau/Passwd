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
    public class UserServiceTest
    {
        private readonly User _userToReturn = new User("name", "password", 1, 2, "comment", "home", "shell");
        private readonly IEnumerable<User> _usersToReturn = new List<User>
        {
            new User("name1", "password1", 1, 2, "comment1", "home1", "shell1"),
            new User("name2", "password2", 2, 3, "comment2", "home2", "shell2")
        };

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void CreateUser_ReturnsUser()
        {
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub
                .Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_userToReturn);

            var target = new UserService(repositoryStub.Object);
            var actual = target.CreateUser("name", "password", 1, 2, "comment", "home", "shell");

            Assert.IsNotNull(actual);
            Assert.AreEqual(_userToReturn, actual);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void GetUsers_ReturnsUsers()
        {
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub
                .Setup(x => x.Get()).Returns(_usersToReturn);

            var target = new UserService(repositoryStub.Object);
            var actual = target.GetUsers().ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 2);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetUsers_ThrowsEntityNotFoundException()
        {
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub
                .Setup(x => x.Get()).Returns((IEnumerable<User>)null);

            var target = new UserService(repositoryStub.Object);
            target.GetUsers();
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void GetUsers_WhenByQuery_ReturnsUsers()
        {
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub
                .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_usersToReturn.Where(x => x.Equals("name1", null, null, null, null, "shell1")));

            var target = new UserService(repositoryStub.Object);
            var actual = target.GetUsers("name1", null, null, null, null, "shell1").ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 1);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetUsers_WhenByQuery_ThrowsEntityNotFoundException()
        {
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub
                .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>()))
                .Returns((IEnumerable<User>) null);

            var target = new UserService(repositoryStub.Object);
            target.GetUsers("name1", null, null, null, null, null);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void GetUserById_RteurnsUser()
        {
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(_userToReturn);

            var target = new UserService(repositoryStub.Object);
            var actual = target.GetUserById(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(_userToReturn, actual);
        }

        [TestMethod, TestCategory("Services"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetUserById_ThrowsEntityNotFoundException()
        {
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((User)null);

            var target = new UserService(repositoryStub.Object);
            target.GetUserById(3);
        }
    }
}
