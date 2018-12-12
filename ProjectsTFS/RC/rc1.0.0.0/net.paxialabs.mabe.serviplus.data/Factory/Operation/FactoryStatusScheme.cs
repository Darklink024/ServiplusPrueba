using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
      internal class FactoryStatusScheme : BaseFactory<FactoryStatusScheme, EntityStatusScheme, StatusScheme>
    {
        public override EntityStatusScheme _GetEntity(StatusScheme entidad)
        {
            return new EntityStatusScheme()
            {
                PK_StatusSchemeID = entidad.PK_StatusSchemeID,
                StatusScheme1 = entidad.StatusScheme1,
                StatusHeadboard = entidad.StatusHeadboard,
                Description = entidad.Description,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
