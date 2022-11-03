using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DTO
{
    public class ClientLoginResponse : Status
    {
        public string Name { get; set; }
        public string Username { get; set; }
        //public string Token { get; set; }
        //public string RefreshToken { get; set; }
        //public DateTime? Expiration { get; set; }
    }
}
