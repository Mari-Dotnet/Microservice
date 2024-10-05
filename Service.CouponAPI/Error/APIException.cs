namespace Service.CouponAPI
{
    public class APIException
    {
        public APIException(int statusCode,string message,string details)
        {
            statusCode = StatusCode;
            message=Message;
            details = Details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
