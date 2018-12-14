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
    public class GroupsControllerTest
    {
        private readonly IEnumerable<Group> _groupsToReturn = new List<Group>
        {
            new Group("name1", "password1", 1, "name1"),
            new Group("name2", "password2", 2, "name2")
        };

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_ReturnsAllGroups()
        {
            var groupServiceStub = new Mock<IGroupService>();
            groupServiceStub.Setup(x => x.GetGroups()).Returns(_groupsToReturn);

            var target = new GroupsController(groupServiceStub.Object);
            var expected = target.Ok("[" + string.Join(", ", _groupsToReturn.Select(x => x.ToString())) + "]");
            var actual = target.Get() as OkObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_ThrowsEntityNotFoundException()
        {
            var groupServiceStub = new Mock<IGroupService>();
            groupServiceStub.Setup(x => x.GetGroups()).Throws(new EntityNotFoundException());

            var target = new GroupsController(groupServiceStub.Object);
            var expected = target.NotFound(new EntityNotFoundException().Message);
            var actual = target.Get() as NotFoundObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenByQuery_ReturnsGroups()
        {
            var groupServiceStub = new Mock<IGroupService>();
            groupServiceStub
                .Setup(x => x.GetGroups(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns(_groupsToReturn.Where(x => x.Equals("name1", 1, null, null)));

            var target = new GroupsController(groupServiceStub.Object);
            var expected = target.Ok("[" + string.Join(", ",
                                         _groupsToReturn.Where(x => x.Equals("name1", 1, null, null))
                                             .Select(x => x.ToString())) + "]");
            var actual = target.Get("name1", 1, null) as OkObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenByQuery_ThrowsEntityNotFoundException()
        {
            var groupServiceStub = new Mock<IGroupService>();
            groupServiceStub
                .Setup(x => x.GetGroups(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Throws(new EntityNotFoundException());

            var target = new GroupsController(groupServiceStub.Object);
            var expected = target.NotFound(new EntityNotFoundException().Message);
            var actual = target.Get("name1", 1, null) as NotFoundObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenById_ReturnsGroup()
        {
            var groupServiceStub = new Mock<IGroupService>();
            groupServiceStub.Setup(x => x.GetGroupById(It.IsAny<int>()))
                .Returns(_groupsToReturn.Where(x => x.Equals(1)).FirstOrDefault);

            var target = new GroupsController(groupServiceStub.Object);
            var expected = target.Ok(string.Join(", ", _groupsToReturn.FirstOrDefault(x => x.Equals(1))?.ToString()));
            var actual = target.Get(1) as OkObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [TestMethod, TestCategory("Controllers"), TestCategory("UnitTests"), TestCategory("PasswdApi")]
        public void Get_WhenById_ThrowsEntityNotFoundException()
        {
            var groupServiceStub = new Mock<IGroupService>();
            groupServiceStub.Setup(x => x.GetGroupById(It.IsAny<int>()))
                .Throws(new EntityNotFoundException());

            var target = new GroupsController(groupServiceStub.Object);
            var expected = target.NotFound(new EntityNotFoundException().Message);
            var actual = target.Get(1) as NotFoundObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }
    }
}
