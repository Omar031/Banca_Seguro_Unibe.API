using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaModelo.Modelos
{
    public class Token
    {
        public int idUsuario { get; set; }
        public int idTenant { get; set; }
        public int codigoCliente { get; set; }
        public string usuario { get; set; }
        public string correo { get; set; }
        public string tipo { get; set; }
        public string token { get; set; }
        public DateTime expiracionJWT { get; set; }
        public bool isAuthorize { get; set; }

        public Token()
        {

        }

        public Token(int idUsuario, int idTenant, int codigoCliente, string usuario, string correo, string tipo, string token, DateTime expiracionJWT, bool isAuthorize)
        {
            this.idUsuario = idUsuario;
            this.idTenant = idTenant;
            this.codigoCliente = codigoCliente;
            this.usuario = usuario;
            this.correo = correo;
            this.tipo = tipo;
            this.token = token;
            this.expiracionJWT = expiracionJWT;
            this.isAuthorize = isAuthorize;
        }



    }
}
