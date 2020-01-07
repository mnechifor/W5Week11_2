using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UserPosts.Domain;

namespace UserPosts.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        UserService sut;

        [Test]
        public void TestInactiveScenario1()
        {
            IUserRepository userRepository = new UserRepoStub();
            IPostRepository postRepository = new PostRepoStubForInactiveScenario();

            sut = new UserService(userRepository, postRepository);

            var expected = new UserActiveRespose()
            {
                Email = "andrei@wtf.com",
                Status = UserPostsStatus.Inactive
            };

            var actual = sut.GetUserActiveRespose(1);

            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Status, actual.Status);
        }

        [Test]
        public void TestActiveScenario1()
        {
            IUserRepository userRepository = new UserRepoStub();
            IPostRepository postRepository = new PostRepoStubForActiveScenario();

            sut = new UserService(userRepository, postRepository);

            var expected = new UserActiveRespose()
            {
                Email = "andrei@wtf.com",
                Status = UserPostsStatus.Active
            };

            var actual = sut.GetUserActiveRespose(1);

            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Status, actual.Status);
        }

        [Test]
        public void TestInactiveScenario2()
        {
            IUserRepository userRepository = NSubstitute.Substitute.For<IUserRepository>();
            IPostRepository postRepository = NSubstitute.Substitute.For<IPostRepository>();

            userRepository.GetById(1).Returns(new Domain.User {
                Id = 1,
                Email = "andrei1@wtf.com"
            });

            postRepository.GetPostsByUserId(1).Returns(new List<Post>() {
                new Post(),
                new Post(),
                new Post()
            });

            sut = new UserService(userRepository, postRepository);

            var expected = new UserActiveRespose()
            {
                Email = "andrei1@wtf.com",
                Status = UserPostsStatus.Inactive
            };

            var actual = sut.GetUserActiveRespose(1);

            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Status, actual.Status);
        }

        [Test]
        public void TestActiveScenario2()
        {
            IUserRepository userRepository = NSubstitute.Substitute.For<IUserRepository>();
            IPostRepository postRepository = NSubstitute.Substitute.For<IPostRepository>();

            userRepository.GetById(1).Returns(new Domain.User
            {
                Id = 1,
                Email = "andrei1@wtf.com"
            });

            postRepository.GetPostsByUserId(Arg.Any<int>()).Returns(new List<Post>() {
                new Post(),
                new Post(),
                new Post(),
                new Post(),
                new Post(),
                new Post(),
            });

            sut = new UserService(userRepository, postRepository);

            var expected = new UserActiveRespose()
            {
                Email = "andrei1@wtf.com",
                Status = UserPostsStatus.Active
            };

            var actual = sut.GetUserActiveRespose(1);

            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Status, actual.Status);
        }
    }
}
