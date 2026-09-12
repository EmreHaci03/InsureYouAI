using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.MessageReplyDtos
{
    public class UpdateMessageReplyDto
    {
        public int MessageReplyId { get; set; }
        public string Message { get; set; }
        public string ReplyDetail { get; set; }
        public DateTime ReplyDate { get; set; }
        public bool IsSentSuccessfully { get; set; }
    }
}
