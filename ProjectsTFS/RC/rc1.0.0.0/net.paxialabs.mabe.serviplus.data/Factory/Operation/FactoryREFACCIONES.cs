using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryREFACCIONES : BaseFactory<FactoryREFACCIONES, EntityREFACCIONES, net.paxialabs.mabe.serviplus.data.Model.REFACCIONES>
    {
        public override EntityREFACCIONES _GetEntity(net.paxialabs.mabe.serviplus.data.Model.REFACCIONES entidad)
        {
            return new EntityREFACCIONES()
            {
                RefaccionID = entidad.RefaccionID,
                ID_REF = entidad.ID_REF,
                CENTRO = entidad.CENTRO,
                ALMACEN = entidad.ALMACEN,
                TOTDISP = entidad.TOTDISP,
                Procesado = entidad.Procesado,
                Creacion = entidad.Creacion,
                Modificacion = entidad.Modificacion,
            };
        }
    }
}
