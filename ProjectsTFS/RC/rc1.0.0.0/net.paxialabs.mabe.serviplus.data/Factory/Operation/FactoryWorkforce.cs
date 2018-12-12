using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{

    internal class FactoryWorkforce : BaseFactory<FactoryWorkforce, EntityWorkforce, Workforce>
    {
        public override EntityWorkforce _GetEntity(Workforce entidad)
        {
            return new EntityWorkforce()
            {
                PK_WorkforceID = entidad.PK_WorkforceID,
                WorkforceID = entidad.WorkforceID,
                Description = entidad.Description,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
