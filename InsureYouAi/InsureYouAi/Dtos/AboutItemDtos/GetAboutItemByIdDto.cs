using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.AboutItemDtos
{
    public class GetAboutItemByIdDto
    {
        public int AboutItemId { get; set; }
        public string ItemDetail { get; set; }
        public string About { get; set; }
    }
}
