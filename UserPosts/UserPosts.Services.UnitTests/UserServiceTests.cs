using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using UserPosts.Domain;

namespace UserPosts.Services.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void Should_Have_Inactive_Status_When_Less_Than_Five_Posts()
        {
            //Arrange

            FakeUserRepository fakeUserRepository = new FakeUserRepository();
            FakePostRepository fakePostRepository = new FakePostRepository();
            UserService sut = new UserService(fakeUserRepository, fakePostRepository);
            //Act

            var response = sut.GetUserActiveRespose(1);

            //Assert
            Assert.AreEqual(UserPostsStatus.Inactive, response.Status);
        }

        [Test]
        public void Should_Have_Inactive_Status_When_Less_Than_Five_Posts2()
        {
            //Arrange
            var fakeUserRepository = Substitute.For<IUserRepository>();

            var fakeUser = new User()
            {
                Email = "fake@fake.com",
                Id = 1,
                Name = "name",
                Username = "username"
            };

            fakeUserRepository.GetById(1).Returns(fakeUser);

            var fakePostRepository = Substitute.For<IPostRepository>();

            var dummyPosts = new List<Post>()
            {
                new Post()
                {
                    UserId = 1,
                    Body = "asdsa"
                },
                new Post()
                {
                    UserId = 1,
                    Body = "body 2"
                },
                new Post()
                {
                    UserId = 1,
                    Body = "body 2"
                }
            };

            fakePostRepository.GetPostsByUserId(1).Returns(dummyPosts);

            UserService sut = new UserService(fakeUserRepository, fakePostRepository);
            //Act

            var response = sut.GetUserActiveRespose(1);

            //Assert
            Assert.AreEqual(UserPostsStatus.Inactive, response.Status);
        }
    }
}
