namespace InsureYouAi.Dtos.ToxicityDtos
{
    public class ToxicityResultDto
    {
        public string OriginalText { get; set; }
        public string TranslatedText { get; set; }
        public bool IsToxic { get; set; }
        public double ToxicScore { get; set; }
    }
}
