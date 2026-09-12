namespace InsureYouAi.Entities
{
    public class MessageReply
    {
        public int MessageReplyId { get; set; }
        public int MessageId { get; set; }
        public Message Message { get; set; }
        public string ReplyDetail { get; set; }
        public DateTime ReplyDate { get; set; }
        public bool IsSentSuccessfully { get; set; }
    }
}
