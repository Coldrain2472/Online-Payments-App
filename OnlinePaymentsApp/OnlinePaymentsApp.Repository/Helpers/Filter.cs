namespace OnlinePaymentsApp.Repository.Helpers
{
    public class Filter
    {
        public static Filter Empty => new Filter();

        public Dictionary<string, object> Conditions { get; set; } = new();

        public Filter AddCondition(string field, object value)
        {
            Conditions[field] = value;
            return this;
        }
    }
}
