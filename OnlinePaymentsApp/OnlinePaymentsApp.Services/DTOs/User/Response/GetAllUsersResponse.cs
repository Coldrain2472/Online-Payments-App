namespace OnlinePaymentsApp.Services.DTOs.User.Response
{
    public class GetAllUsersResponse
    {
        public List<UserInfo>? Users { get; set; }

        public int TotalCount { get; set; }
    }
}
