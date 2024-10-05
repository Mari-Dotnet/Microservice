namespace Service.AuthAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSucessfull { get; set; }
        public string Message { get; set; }
    }
}
