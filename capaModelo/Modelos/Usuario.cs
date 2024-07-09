using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaModelo.Modelos
{
    public class Usuario
    {
        [Key]
        public int id { get; set; } 
        public int idTenant { get; set; }
        public int idCliente { get; set; }
        [Required]
        public string  correo { get; set; }
        [Required]
        public string  nombre { get; set; }
        [Required]
        public string clave { get; set; }
        public bool flag { get; set; }

        public Usuario() { }    

        public Usuario(int id, string nombre, string clave, string correo, bool flag, int idTenant, int idCliente)
        {
            this.id = id; 
            this.idTenant = idTenant;
            this.idCliente = idCliente;
            this.nombre = nombre;
            this.correo = correo;
            this.clave = clave;
            this.flag = flag;
        }
    }
}
