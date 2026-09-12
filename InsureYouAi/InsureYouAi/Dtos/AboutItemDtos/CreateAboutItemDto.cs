using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.AboutItemDtos
{
    public class CreateAboutItemDto
    {
        public int AboutItemId { get; set; }
        public string ItemDetail { get; set; }
        public int AboutId { get; set; }
    }
}
