namespace InsureYouAi.Dtos.AppUserDtos
{
    public class GetAppUserByIdDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
    }
}
