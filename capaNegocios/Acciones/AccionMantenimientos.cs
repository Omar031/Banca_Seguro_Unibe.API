using capaDatos.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaNegocios.Acciones
{
    public class AccionMantenimientos : AccionConexion
    {

        /// Metodos statment
        /// 
        public List<TM_Tenant> GetTenants(int? id = 0) 
        { 
            var listar = dbSeguroContext.TM_Tenants.ToList();

            return listar == null ? new List<TM_Tenant>() : listar ;
        
        }
        

    }
}
