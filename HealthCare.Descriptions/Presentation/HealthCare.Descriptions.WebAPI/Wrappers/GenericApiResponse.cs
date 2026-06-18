namespace HealthCare.Descriptions.WebAPI.Wrappers
{
    public class GenericApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }

    public class GenericApiResponse<TQueryResult> : GenericApiResponse
    {
        public List<TQueryResult> Datas { get; set; }
        public TQueryResult Data { get; set; }
    }
}
