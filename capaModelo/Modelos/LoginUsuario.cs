using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaModelo.Modelos
{
    public class LoginUsuario
    {
        [Required]
        public string usuario { get; set; }

        [Required]
        [MaxLength(8)]
        public string clave { get; set; }

        public LoginUsuario() { }   
    }
}
