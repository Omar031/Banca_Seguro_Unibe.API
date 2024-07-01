using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaModelo.Modelos
{
    public class JWT
    {
        public string Key { get; set; }
        public string Subject { get; set; }
        public string Issuer { get; set; } 
        public string Audience { get; set; }
        public string Authority { get; set; }
        public int Minutes { get; set; }
        public bool IsExpirate { get; set; }

        public JWT()
        {

        }

    }
}
