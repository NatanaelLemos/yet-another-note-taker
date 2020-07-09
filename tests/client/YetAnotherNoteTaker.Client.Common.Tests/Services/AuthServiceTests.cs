using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.Client.Common.State;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.UnitTests.Services
{
    public class AuthServiceTests
    {
        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(false, false)]
        public void ConstructorShouldThrowIfParamsAreNull(bool mockRepository, bool mockUserState)
        {
            var repository = mockRepository ? Mock.Of<IAuthRepository>() : null;
            var userState = mockUserState ? Mock.Of<IUserState>() : null;

            this.Invoking(_ => new AuthService(repository, userState))
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateUserShouldSendDtoToRepository()
        {
            var expectedResult = new UserDto { Email = "foo@bar" };

            var repository = new Mock<IAuthRepository>();
            repository.Setup(r => r.CreateUser(It.IsAny<NewUserDto>()))
                .ReturnsAsync(expectedResult);

            var service = new AuthService(repository.Object, Mock.Of<IUserState>());
            var actualResult = await service.CreateUser(new NewUserDto());

            repository.Verify(
                r => r.CreateUser(It.IsAny<NewUserDto>()),
                Times.Once);

            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public void LoginShouldThrowIfCouldntFetchToken()
        {
            var service = new AuthService(Mock.Of<IAuthRepository>(), Mock.Of<IUserState>());
            service.Awaiting(s => s.Login("foo@bar", "123"))
                .Should().Throw<InvalidOperationException>();
        }
    }
}
