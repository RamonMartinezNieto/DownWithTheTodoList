namespace DownWithTheTodoList.Ms.Users.Helpers;

public static class UserMapper 
{
    public static User ToUser(this UserCreateRequest userModel) 
        => CreateUser(userModel.NickName, userModel.Password);

        
    public static User ToUser(this UserUpdateRequest userModel, Guid id)
        => CreateUser(userModel.NickName, userModel.Password, id);

    public static UserResponse ToUserResponse(this User user)
        => new UserResponse()
        {
            Id = user.Id,
            NickName = user.NickName,   
        };

    private static User CreateUser(string nick, string pass, Guid id)
    {
        var user = CreateUser(nick, pass);
        user.Id = id;
        return user;
    }

    private static User CreateUser(string nick, string pass) 
    {
        return new()
        {
            NickName = nick,
            Password = pass
        };
    }  

}
