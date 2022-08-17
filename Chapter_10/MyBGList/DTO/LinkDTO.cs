namespace MyBGList.DTO
{
    public class LinkDTO
    {
        public LinkDTO(string href, string rel, string type)
        {
            Href = href;
            Rel = rel;
            Type = type;
        }

        public string Href { get; private set; }

        public string Rel { get; private set; }

        public string Type { get; private set; }
    }
}
