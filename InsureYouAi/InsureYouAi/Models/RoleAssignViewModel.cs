namespace InsureYouAi.Models
{
    public class RoleAssignViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleCheckItemViewModel> roleChecks { get; set; }
    }
}
