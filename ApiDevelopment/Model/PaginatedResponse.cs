namespace ApiDevelopment.Model
{
    public class PaginatedResponse<T>
    {
        public T Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
