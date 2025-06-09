namespace OnlinePaymentsApp.Web.Helpers
{
    public class IdHelper
    {
        public static int GetUserId(HttpContext httpContext)
        {
            return httpContext.Session.GetInt32("UserId").Value;
        }
    }
}
