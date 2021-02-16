using System.Collections.Generic;

namespace PicACG.Models.Response
{
    public class InitializationInfo
    {
        public string? Status { get; set; }
        public ICollection<string>? Addresses { get; set; }
        public string? Waka { get; set; }
        public string? AdKeyword { get; set; }
    }
}