namespace PicACG.Models.Request
{
    public class Register
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Question1 { get; set; }
        public string Answer1 { get; set; }
        public string Question2 { get; set; }
        public string Answer2 { get; set; }
        public string Question3 { get; set; }
        public string Answer3 { get; set; }
    }

    public enum Gender
    {
        m,
        f,
        bot
    }
}
