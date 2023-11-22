﻿using static System.Formats.Asn1.AsnWriter;

namespace Testing.DanamonNew.Models
{
    public class AuthResponseDto
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
    }
}
