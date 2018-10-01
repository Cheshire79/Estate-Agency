using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using NUnit.Framework;
using Microsoft.AspNet.Identity;
using EstateAgency.DAL.Interface.Identity;
using EstateAgency.DAL.Entities;
using EstateAgency.BLL.Identity.Interface;
using EstateAgency.BLL.Identity.Interface.Data.Validation;
using EstateAgency.BLL.Identity.Services;
using Identity.BLL.Mapper;

namespace Identity.BLLTest
{

    [TestFixture]
    public class IdentityTest
    {
       
        IIdentityService service;

        [SetUp]
        public void SetUp()
        {			
			var roles = new List<ApplicationRole>()
            {
                new ApplicationRole(){Id = "1",Name = "admin"},
                new ApplicationRole(){Id = "2",Name = "user"},
                new ApplicationRole(){Id = "3",Name = "manager"}
            }.AsQueryable();
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser(){Id = "1",UserName = "Admin",
                    Roles =
                    {
                        new IdentityUserRole(){ RoleId = "1"},
                        new IdentityUserRole() { RoleId = "2" }
                    } },
                new ApplicationUser(){Id = "2",UserName = "11",
                    Roles = { new IdentityUserRole(){ RoleId = "2"} } },
                new ApplicationUser(){Id = "3",UserName = "22",
                    Roles = { new IdentityUserRole(){ RoleId = "2"} } },
                new ApplicationUser(){Id = "4",UserName = "33",
                    Roles = { new IdentityUserRole(){ RoleId = "2"} } },
            }.AsQueryable();
			
			Mock<IUserRepository> userRepository;
			Mock<IRoleRepository> roleRepository;
			userRepository = new Mock<IUserRepository>();
            roleRepository = new Mock<IRoleRepository>();

            roleRepository.Setup(m => m.Roles()).Returns(roles.AsQueryable());
            userRepository.Setup(m => m.Users()).Returns(users.AsQueryable());

            roleRepository.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(
                (string id) => { return roleRepository.Object.Roles().FirstOrDefault(x => x.Id == id); });

            userRepository.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(
                (string id) => { return userRepository.Object.Users().FirstOrDefault(x => x.Id == id); });

            userRepository.Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(
                (ApplicationUser user) =>
                {
                    if (userRepository.Object.Users().Contains(user))
                        return IdentityResult.Success;
                    return IdentityResult.Failed("Error");
                });

            roleRepository.As<IQueryable<ApplicationRole>>().Setup(m => m.Expression).Returns(roles.Expression);
            roleRepository.As<IQueryable<ApplicationRole>>().Setup(m => m.ElementType).Returns(roles.ElementType);
            roleRepository.As<IQueryable<ApplicationRole>>().Setup(m => m.GetEnumerator()).Returns(roles.GetEnumerator());

			Mock<IIdentityUnitOfWork> uow;
			uow = new Mock<IIdentityUnitOfWork>();
            uow.Setup(u => u.UserRepository).Returns(userRepository.Object);
            uow.Setup(u => u.RoleRepository).Returns(roleRepository.Object);


            //   uow.Setup(u => u.SaveAsync()).Returns(Task.Run(() => { }));

            service = new IdentityService(uow.Object, new IdentityMapperFactory());
        }


        [Test]
        public async Task GetUsersInRole()
        {
            string roleId = "2";
            var users = (await service.GetUsersInRoleAsync(roleId)).ToList();
            int usersInRole = 4;
            Assert.IsTrue(users.Count == usersInRole);
        }

        [Test]
        public async Task CanDeleteUser()
        {
            string currentUserId = "1";
            string UserIdToDelete = "2";
            var result= await service.DeleteUser(currentUserId, UserIdToDelete);
            var excpected = new OperationDetails(true, "user was deleted", "");

            // need to add Equals into OperationDetails or ?
            Assert.IsTrue(excpected.Equals(result));
        }

        [Test]
        public async Task CannotDeleteUser()
        {
            string currentUserId = "1";
            string UserIdToDelete = "9";
            var result = await service.DeleteUser(currentUserId, UserIdToDelete);
            var excpected = new OperationDetails(false, "Cannot delete user. Something happens", "");
            Assert.IsTrue(excpected.Equals(result));
        }

        [Test]
        public async Task CannotDeleteYourSelfUser()
        {
            string currentUserId = "1";
            string UserIdToDelete = "1";
            var result = await service.DeleteUser(currentUserId, UserIdToDelete);
            var excpected = new OperationDetails(true, "Cannot delete youself", "");
            Assert.IsTrue(excpected.Equals(result));
        }
    }
}