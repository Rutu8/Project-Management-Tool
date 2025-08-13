using projectmanagementtoolProject.Models;

namespace projectmanagementtoolProject.DTOs
{
    public class LoginStatus
    {
        public string status {  get; set; }
        public string message { get; set; }
        public string usertype { get; set; }
        public User user { get; set; }
    }
}
