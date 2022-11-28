namespace MyBGList.DTO.v1
{
    public class RestDTO<T>
    {
        public T Data { get; set; } = default!;

        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}
