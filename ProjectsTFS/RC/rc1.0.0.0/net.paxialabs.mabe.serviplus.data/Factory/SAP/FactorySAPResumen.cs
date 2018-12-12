using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.SAP
{
    internal class FactorySAPResumen : BaseFactory<FactorySAPResumen, EntitySAPResumen, Resumen>
    {
        public override EntitySAPResumen _GetEntity(Resumen entidad)
        {
            return new EntitySAPResumen() {
                ResumenID = entidad.ResumenID,
                Tipo = entidad.Tipo,
                Contenedor = entidad.Contenedor,
                Fecha = entidad.Fecha,
                Registros = entidad.Registros,
                Actualizados = entidad.Actualizados,
                Insertados = entidad.Insertados,
                Procesado = entidad.Procesado,
                BI_ODS_Udp = entidad.BI_ODS_Udp,
                Inicio = entidad.Inicio,
                Termino = entidad.Termino,
                Creacion = entidad.Creacion,
                Modificacion = entidad.Modificacion            
            };
        }
    }
}
