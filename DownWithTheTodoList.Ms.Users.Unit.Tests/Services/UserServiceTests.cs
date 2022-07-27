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
        
        //DELETE ZONE 
        [Fact]
        public async Task DeleteByIdAsync_ShouldDeleteUser_WhenIdAreValid_RetunTrue()
        {
            Guid someGuid = Guid.NewGuid();

            _userRepository.DeleteByIdAsync(someGuid).Returns(true);

            var result = await _sut.DeleteByIdAsync(someGuid);

            result.Should().BeTrue();
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnFalse_WhenIdIsNotFound() 
        {
            Guid someGuid = Guid.NewGuid();

            _userRepository.DeleteByIdAsync(someGuid).Returns(false);

            var result = await _sut.DeleteByIdAsync(someGuid);

            result.Should().BeFalse();
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldThrowException_WhenSomethingWasWrong() 
        {
            //TODO
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldLogCorrectMessage_WhenDeleteUser() 
        {
            //TODO
        }        
        
        [Fact]
        public async Task DeleteByIdAsync_ShouldLogCorrectMessage_WhenThrownException() 
        {
            //TODO
        }  

        //Get All async 
        [Fact]
        public async void GetAllAsync_ShouldDeleteUser_WhenIdAreValid_RetunTrue()
        {
            //TODO
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnFalse_WhenIdIsNotFound() 
        {
            //TODO
        }

        [Fact]
        public async void GetAllAsync_ShouldThrowException_WhenSomethingWasWrong() 
        {
            //TODO
        }

        [Fact]
        public async Task GetAllAsync_ShouldLogCorrectMessage_WhenDeleteUser() 
        {
            //TODO
        }

        [Fact]
        public async Task GetAllAsync_ShouldLogCorrectMessage_WhenThrownException() 
        {
            //TODO
        }

        //GetByIdAsync 
        [Fact]
        public async void GetByIdAsync_ShouldReturn_User_WhenIdAreValid()
        {
            genericUser.Id = Guid.NewGuid();
             _userRepository.GetByIdAsync(genericUser.Id).Returns(genericUser);
           
            var result = await _sut.GetByIdAsync(genericUser.Id);

            result.Should().NotBeNull();
            result.Should().Be(genericUser);
        }

        [Fact]
        public async void GetByIdAsync_ShouldThrowKeyNotFounException_WhenIdNotFound() 
        {
            _userRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsForAnyArgs((User)null!);

            var action = async () => await _sut.GetByIdAsync(Guid.NewGuid());

            await action.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage("There isn't any User with this id");

        }

        [Fact]
        public async void GetByIdAsync_ShouldThrowException_WhenSomethingWasWrong() 
        {
            Exception ex = new($"Error retrieving the user with id {genericUser.Id}");

            _userRepository.GetByIdAsync(Arg.Any<Guid>()).Throws(ex);

            var action = async () => await _sut.GetByIdAsync(Arg.Any<Guid>());

            await action.Should()
                .ThrowAsync<Exception>()
                .Where(ex => ex.Message.StartsWith("Error retrieving the user with id"));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldLogCorrectMessage_WhenGetTheUser() 
        {
            _userRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(genericUser);

            var result = await _sut.GetByIdAsync(Arg.Any<Guid>());

            _loggerAdapter.Received(2);

            //_loggerAdapter.Received(1).LogDebug(
            //    Arg.Is<string?>(str => str!.StartsWith("Retrieving user with id")),
            //    Arg.Any<Guid>());

            //_loggerAdapter.Received(1).LogDebug(
            //  Arg.Is<string?>(str => str!.StartsWith("User found")),
            //  Arg.Any<string>());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldLogCorrectMessage_WhenThrownException() 
        {
            //TODO
        }

        //UpdateAsync 
        [Fact]
        public async void UpdateAsync_ShouldDeleteUser_WhenIdAreValid_RetunTrue()
        {
            //TODO
        }

        [Fact]
        public async void UpdateAsync_ShouldReturnFalse_WhenIdIsNotFound() 
        {
            //TODO
        }

        [Fact]
        public async void UpdateAsync_ShouldThrowException_WhenSomethingWasWrong() 
        {
            //TODO
        }

        [Fact]
        public async Task UpdateAsync_ShouldLogCorrectMessage_WhenDeleteUser() 
        {
            //TODO
        }

        [Fact]
        public async Task UpdateAsync_ShouldLogCorrectMessage_WhenThrownException() 
        {
            //TODO
        }
    }
}