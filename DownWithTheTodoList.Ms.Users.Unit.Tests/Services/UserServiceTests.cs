using DownWithTheTodoList.Core.Models;
using DownWithTheTodoList.Ms.Users.Logger;
using DownWithTheTodoList.Ms.Users.Repositories;
using DownWithTheTodoList.Ms.Users.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DownWithTheTodoList.Ms.Users.Unit.Tests.Services
{
    public class UserServiceTests
    {
        private UserService _sut; 
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly ILoggerAdapter<UserService> _loggerAdapter = Substitute.For<ILoggerAdapter<UserService>>();

        public User genericUser = new()
        {
            NickName = "Ramon",
            Password = "ThisIsAHash#"
        };

        public UserServiceTests()
        {
            _sut = new UserService(_userRepository, _loggerAdapter);
        }

        [Fact]
        public async void CreateAsync_ShouldCreateAUser_WhenDetailsAreValid()
        {
            var mockRepository = _userRepository.CreateAsync(genericUser);
            //Emulate that the database crete a new guid
            genericUser.Id = Guid.NewGuid();
            mockRepository.Returns(genericUser);

            var result = await _sut.CreateAsync(genericUser);

            result.Should().BeEquivalentTo(genericUser);
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async void CreateAsync_ShouldThrowException_WhenCreateBadUser() 
        {
            string errorMessage = $"Error creating user with name {genericUser.NickName}";
            Exception ex = new (errorMessage);

            _userRepository.CreateAsync(genericUser).Throws(ex);

            var action = async () => await _sut.CreateAsync(genericUser);

            await action.Should()
                .ThrowAsync<Exception>()
                .WithMessage(errorMessage);
        }

        [Fact]
        public async Task CreateAsync_ShouldLogCorrectMessage_WhenCreateUser() 
        {

            var result = await _sut.CreateAsync(genericUser);

            _loggerAdapter.Received(2);

            _loggerAdapter.Received(1).LogDebug(
                Arg.Is<string?>(str => str!.StartsWith("Creating user with name")),
                Arg.Is<string>(genericUser.NickName));

            _loggerAdapter.Received(1).LogDebug(
              Arg.Is<string?>(str => str!.StartsWith("User created with id")),
              Arg.Any<Guid>());

        }        
        
        [Fact]
        public async Task CreateAsync_ShouldLogCorrectMessage_WhenThrownException() 
        {
            Exception ex = new($"Error creating user with name {genericUser.NickName}");

            _userRepository.CreateAsync(genericUser).Throws(ex);
            
            var action = async () => await _sut.CreateAsync(genericUser);

            await action.Should()
                .ThrowAsync<Exception>();

            _loggerAdapter.Received(2);

            _loggerAdapter.Received(1).LogError(
              Arg.Is<Exception>(ex),
              Arg.Is<string?>(str => str!.StartsWith("Error creating user with name")),
              Arg.Is<string>(genericUser.NickName));

        }
    }
}