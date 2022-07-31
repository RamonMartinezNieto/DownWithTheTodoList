using DownWithTheTodoList.Ms.Users.Helpers;

namespace DownWithTheTodoList.Ms.Users.Unit.Tests.Services
{
    public class UserServiceTests
    {
        private UserService _sut; 
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly IUserMemoryCache _userMemCache = Substitute.For<IUserMemoryCache>();
        private readonly ILoggerAdapter<UserService> _loggerAdapter = Substitute.For<ILoggerAdapter<UserService>>();

        public User genericUser = new()
        {
            NickName = "Ramon",
            Password = "ThisIsAHash#"
        };

        public UserServiceTests()
        {
            _sut = new UserService(_userRepository, _userMemCache, _loggerAdapter);
        }

        [Fact]
        public async void CreateAsync_ShouldCreateAUser_WhenDetailsAreValid()
        {
            var mockRepository = _userRepository.CreateAsync(genericUser);
            //Emulate that the database crete a new guid
            genericUser.Id = Guid.NewGuid();
            mockRepository.Returns(genericUser);

            var result = await _sut.CreateAsync(genericUser);

            result.Should().BeEquivalentTo(genericUser.ToUserResponse());
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
            _userRepository.CreateAsync(genericUser).Returns(genericUser);

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
        
        [Fact]
        public async Task DeleteByIdAsync_ShouldDeleteUser_WhenIdAreValid_RetunTrue()
        {
            Guid someGuid = Guid.NewGuid();

            _userRepository.DeleteByIdAsync(someGuid).Returns(true);

            var result = await _sut.DeleteByIdAsync(someGuid);

            result.Should().BeTrue();
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldThrowException_WhenSomethingWasWrong() 
        {
            Guid userToDelete = Guid.NewGuid();
            Exception ex = new($"Error deleting user with id {userToDelete}");

            _userRepository.DeleteByIdAsync(userToDelete).Throws(ex);

            var action = async () => await _sut.DeleteByIdAsync(userToDelete);

            await action.Should()
                .ThrowAsync<Exception>()
                .WithMessage(ex.Message);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldThrowKeyException_WhenUserDosntExist() 
        {
            Guid userToDelete = Guid.NewGuid();
            KeyNotFoundException ex = new($"Not found any item with id {userToDelete}");

            _userRepository.DeleteByIdAsync(userToDelete).Returns(false);

            var action = async () => await _sut.DeleteByIdAsync(userToDelete);

            await action.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage(ex.Message);
        }        
        
        [Fact]
        public async Task DeleteByIdAsync_ShouldLogCorrectMessage_WhenThrownException() 
        {
            Guid userToDelete = Guid.NewGuid();
            Exception ex = new($"Error deleting user with id {userToDelete}");
            _userRepository.DeleteByIdAsync(userToDelete).Throws(ex);

            var action = async () => await _sut.DeleteByIdAsync(userToDelete);

            await action.Should()
                .ThrowAsync<Exception>();

            _loggerAdapter.Received(2);

            _loggerAdapter.Received(1)
                .LogDebug(Arg.Is<string?>(str => str!.StartsWith("Start deleting user")));

            _loggerAdapter.Received(1).LogError(
              Arg.Is<Exception>(ex),
              Arg.Is<string?>(str => str!.StartsWith("Error deleting user with id")),
              Arg.Is<Guid>(userToDelete));
        }  

        [Fact]
        public async void GetAllAsync_ShouldGetTheUsers_WhenThereAreUsers()
        {
            var expectedUsers = new[]
            {
                Substitute.For<User>(),
                Substitute.For<User>()
            };

            _userRepository.GetAllAsync().Returns(expectedUsers);

            var result = await _userRepository.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedUsers);
            result.Should().HaveCount(2);
        }

        [Fact]
        public async void GetAllAsync_ShouldBeEmpty_WhenNoUsersExistt() 
        {
            _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());
            var result = await _sut.GetAllAsync();
            result.Should().BeEmpty();
        }

        [Fact]
        public async void GetAllAsync_ShouldThrowException_WhenSomethingWasWrong() 
        {
            Exception ex = new("Error retrieving all users");

            _userRepository.GetAllAsync().Throws(ex);

            var action = async () => await _sut.GetAllAsync();

            await action.Should()
                .ThrowAsync<Exception>()
                .WithMessage(ex.Message);
        }

        [Fact]
        public async Task GetAllAsync_ShouldLogCorrectMessage_WhenNotThrownException() 
        {
            var expectedUsers = new[]
            {
                Substitute.For<User>(),
                Substitute.For<User>()
            };

            _userRepository.GetAllAsync().Returns(expectedUsers);

            var result = await _sut.GetAllAsync();

            _loggerAdapter.Received(2);

            _loggerAdapter.Received(1)
                .LogDebug(Arg.Is<string?>(str => str!.StartsWith("Retrieving all users")));

            _loggerAdapter.Received(1).LogDebug(
                Arg.Is<string?>(str => str!.StartsWith("Users founded")),
                Arg.Is<int>(expectedUsers.Count()));
        }

        [Fact]
        public async Task GetAllAsync_ShouldLogCorrectMessage_WhenThrownException() 
        {
            Exception ex = new ("Error retrieving all users");
            _userRepository.GetAllAsync().Throws(ex);

            var action = async () => await _sut.GetAllAsync();

            await action.Should()
                .ThrowAsync<Exception>();


            _loggerAdapter.Received(2);

            _loggerAdapter.Received(1).LogError(
                Arg.Is<Exception>(ex),
                Arg.Is<string>("Error retrieving all users"));
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturn_User_WhenIdAreValid()
        {
            genericUser.Id = Guid.NewGuid();
            
            _userRepository.GetByIdAsync(genericUser.Id).Returns(genericUser);
           
            var result = await _sut.GetByIdAsync(genericUser.Id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(genericUser.ToUserResponse());
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
            Guid guid = Guid.NewGuid();
            
            _userRepository.GetByIdAsync(guid).Returns(genericUser);

            var result = await _sut.GetByIdAsync(guid);

            _loggerAdapter.Received(2);

            _loggerAdapter.Received(1).LogDebug(
                Arg.Is<string?>(str => str!.StartsWith("Retrieving user with id")),
                Arg.Any<Guid>());

            _loggerAdapter.Received(1).LogDebug(
              Arg.Is<string?>(str => str!.StartsWith("User found:")),
              Arg.Is<string>(genericUser.NickName));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldLogCorrectMessage_WhenThrownException() 
        {
            Guid guid = Guid.NewGuid();
            _userRepository.GetByIdAsync(guid).Throws(new Exception());
            
            var action = async () => await _sut.GetByIdAsync(guid);

            await action.Should()
                .ThrowAsync<Exception>();

            _loggerAdapter.Received(2);

            _loggerAdapter.Received(1).LogError(
                Arg.Any<Exception>(),
                Arg.Is<string>(x => x.StartsWith("Error retrieving the user with id")),
                Arg.Is<Guid>(guid));
        }

        [Fact]
        public async void UpdateAsync_ShouldUpdateUser_WhenUserIsValid()
        {
            User userToUpdate = Substitute.For<User>();

            _userRepository.UpdateAsync(userToUpdate).Returns(userToUpdate);

            var result = await _sut.UpdateAsync(userToUpdate);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userToUpdate.ToUserResponse());
        }

        [Fact]
        public async void UpdateAsync_ShouldThrowException_WhenSomethingWentWrong() 
        {
            User userToUpdate = Substitute.For<User>();

            Exception ex = new($"Error updating user with id {userToUpdate.Id} and name {userToUpdate.NickName}");

            _userRepository.UpdateAsync(userToUpdate).Throws(ex);

            var action = async () => await _sut.UpdateAsync(userToUpdate);

            await action.Should()
                .ThrowAsync<Exception>()
                .Where(ex => ex.Message.StartsWith("Error updating user"));
        }

        [Fact]
        public async void UpdateAsync_ShouldThrowKeyNotFoundException_WhenKeyWasInvalid() 
        {
            User userToUpdate = Substitute.For<User>();

            KeyNotFoundException ex = new("Error updating user");

            _userRepository.UpdateAsync(userToUpdate).Throws(ex);

            var action = async () => await _sut.UpdateAsync(userToUpdate);

            await action.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage(ex.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldLogCorrectMessage_WhenUpdatedUser() 
        {
            User user = Substitute.For<User>();

            _userRepository.UpdateAsync(user).Returns(user);

            var result = await _sut.UpdateAsync(user);

            _loggerAdapter.Received(2);

            _loggerAdapter.Received(1).LogDebug(
                Arg.Is<string?>(str => str.StartsWith("Updating user with id")),
                Arg.Is<Guid>(user.Id),
                Arg.Is<string>(user.NickName));

            _loggerAdapter.Received(1).LogDebug(
              Arg.Is<string?>(str => str!.StartsWith("User updated with id")),
              Arg.Is<Guid>(user.Id));
        }

        [Fact]
        public async Task UpdateAsync_ShouldLogCorrectMessage_WhenThrownException() 
        {
            User user = Substitute.For<User>();
            _userRepository.UpdateAsync(user).Throws(new Exception());

            var action = async () => await _sut.UpdateAsync(user);

            await action.Should().ThrowAsync<Exception>();

            _loggerAdapter.Received(2);

            _loggerAdapter.Received(1).LogError(
              Arg.Any<Exception>(),
              Arg.Is<string?>(str => str!.StartsWith("Error updating user with")),
              Arg.Is<Guid>(user.Id),
              Arg.Is<string>(user.NickName));
        }
    }
}