namespace DownWithTheTodoList.Ms.Users.Models
{
    public class UserCreateRequest
    {
        [Required]
        public string NickName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
