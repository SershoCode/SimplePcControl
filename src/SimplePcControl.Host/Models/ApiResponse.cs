namespace SimplePcControl.Host.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; }
        public string Comment { get; }

        public ApiResponse(bool isSuccess, string comment = default)
        {
            IsSuccess = isSuccess;
            Comment = comment;
        }
    }
}
