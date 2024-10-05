namespace Services.AuthAPI.Dto
{
    public class RegisterationRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNUmber { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
