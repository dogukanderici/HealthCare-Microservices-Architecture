namespace HealthCare.Operations.WebAPI.Wrappers
{
    public class GenericApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }

    public class GenericApiResponse<TEntity>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public TEntity Data { get; set; }
        public List<TEntity> Datas { get; set; }
    }
}
