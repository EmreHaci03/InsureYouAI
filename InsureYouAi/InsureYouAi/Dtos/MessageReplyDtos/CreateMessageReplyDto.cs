using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.MessageReplyDtos
{
    public class CreateMessageReplyDto
    {
        public int MessageId { get; set; }
        public string ReplyDetail { get; set; }
        public DateTime ReplyDate { get; set; }
        public bool IsSentSuccessfully { get; set; }
    }
}
