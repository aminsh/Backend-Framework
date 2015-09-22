using Domain;

namespace Security.Model
{
    public class UserPermit
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Subject Subject { get; set; }
        public bool IsAllowed { get; set; }
    }
}
