namespace DownWithTheTodoList.Ms.Users.Models
{
    public class UserCreateRequest
    {
        [Required]
        [MinLength(3)]
        public string NickName { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
