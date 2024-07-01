using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaModelo.Modelos
{
    public class Usuario
    {
        public int id { get; set; }
        public int idTenant { get; set; }
        public int idCliente { get; set; }
        public string  correo { get; set; }
        public string  nombre { get; set; }
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
