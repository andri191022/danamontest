namespace Testing.DanamonNew.Models
{
    public class DanamonRequestHeader
    {
        public string BDISignature { get; set; }
        public string BDIKey { get; set; }
        public string BDITimestamp { get; set; }
        public string Authorization { get; set; }
    }
}
