using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PasswdApi.Controllers;
using PasswdApi.Exceptions;
using PasswdApi.Models;
using PasswdApi.Repositories;
using PasswdApi.Services;

namespace PasswdApi.UnitTests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        private readonly IEnumerable<User> _usersToReturn = new List<User>
        {
            new User("name1", "password1", 1, 2, "comment1", "home1", "shell1") , 
            new User("name2", "password2", 2, 3, "comment2", "home2", "shell2")
        };

        private readonly IEnumerable<Group> _groupsToReturn = new List<Group>
        {
            new Group("name1", "password1", 1, "name1"),
            new Group("name2", "password2", 2, "name2")
        };

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_ReturnsAllUsers()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetUsers()).Returns(_usersToReturn);

            var target = new UsersController(userServiceStub.Object, new GroupService(new GroupRepository()));
            var expected = target.Ok(string.Join(", ", _usersToReturn.Select(x => x.ToString())));
            var actual = target.Get() as OkObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_ThrowsEntityNotFoundException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetUsers()).Throws(new EntityNotFoundException());

            var target = new UsersController(userServiceStub.Object, new GroupService(new GroupRepository()));
            var expected = target.NotFound(new EntityNotFoundException().Message);
            var actual = target.Get() as NotFoundObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenByQuery_ReturnsUsers()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetUsers(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_usersToReturn.Where(x => x.Equals("name1", null, null, null, null, null)));

            var target = new UsersController(userServiceStub.Object, new GroupService(new GroupRepository()));
            var expected = target.Ok("[" + string.Join(", ",
                                         _usersToReturn.Where(x => x.Equals("name1", null, null, null, null, null))
                                             .Select(x => x.ToString())) + "]");
            var actual = target.Get("name1", null, null, null, null, null) as OkObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenByQuery_ThrowsEntityNotFoundException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetUsers(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new EntityNotFoundException());

            var target = new UsersController(userServiceStub.Object, new GroupService(new GroupRepository()));
            var expected = target.NotFound(new EntityNotFoundException().Message);
            var actual = target.Get("name1", null, null, null, null, null) as NotFoundObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenById_ReturnsUser()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetUserById(It.IsAny<int>()))
                .Returns(_usersToReturn.Where(x => x.Equals(1)).FirstOrDefault);

            var target = new UsersController(userServiceStub.Object, new GroupService(new GroupRepository()));
            var expected = target.Ok(string.Join(", ", _usersToReturn.FirstOrDefault(x => x.Equals(1))?.ToString()));
            var actual = target.Get(1) as OkObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenById_ThrowsEntityNotFoundException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetUserById(It.IsAny<int>()))
                .Throws(new EntityNotFoundException());

            var target = new UsersController(userServiceStub.Object, new GroupService(new GroupRepository()));
            var expected = target.NotFound(new EntityNotFoundException().Message);
            var actual = target.Get(1) as NotFoundObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void GetGroups_WhenById_ReturnsGroups()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetUserById(It.IsAny<int>()))
                .Returns(_usersToReturn.Where(x => x.Equals(null, 1, null, null, null, null)).FirstOrDefault);

            var groupServiceStub = new Mock<IGroupService>();
            groupServiceStub.Setup(x =>
                    x.GetGroups(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns(_groupsToReturn.Where(x => x.Equals(null, null, "name1")));

            var target = new UsersController(userServiceStub.Object, groupServiceStub.Object);
            var expected = target.Ok("[" + string.Join(", ",
                                         _groupsToReturn.Where(x => x.Equals(null, null, "name1"))
                                             .Select(x => x.ToString())) + "]");
            var actual = target.GetGroups(1) as OkObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void GetGroups_WhenById_ThrowsEntityNotFoundException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(x => x.GetUserById(It.IsAny<int>()))
                .Throws(new EntityNotFoundException());

            var groupServiceStub = new Mock<IGroupService>();
            groupServiceStub.Setup(x =>
                    x.GetGroups(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns(_groupsToReturn.Where(x => x.Equals(null, null, "name3")));

            var target = new UsersController(userServiceStub.Object, groupServiceStub.Object);
            var expected = target.NotFound(new EntityNotFoundException().Message);
            var actual = target.GetGroups(3) as NotFoundObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }
    }
}
