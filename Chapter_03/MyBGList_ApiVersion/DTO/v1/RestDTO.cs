namespace MyBGList.DTO.v1
{
    public class RestDTO<T>
    {
        public T Data { get; set; } = default(T)!;

        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}
