namespace Core.AdminUI.Models.Commons.Interfaces
{
    public interface IBaseModel
    {
        public string? PagingToken { get; set; }
        public bool? IsLastPage { get; set; }
    }
}
