namespace OnlinePaymentsApp.Services.DTOs.Authentication.Response
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }

        public int? UserId { get; set; }

        public string? Name { get; set; }

        public string? Username { get; set; }
    }
}
