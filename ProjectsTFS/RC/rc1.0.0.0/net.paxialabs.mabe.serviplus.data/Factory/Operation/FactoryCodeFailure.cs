using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
     internal class FactoryCodeFailure : BaseFactory<FactoryCodeFailure, EntityCodeFailure,CodeFailure>
    {
        public override EntityCodeFailure _GetEntity(CodeFailure entidad)
        {
            return new EntityCodeFailure()
            {
                PK_CodeFailureID = entidad.PK_CodeFailureID,
                CodeFailure1 = entidad.CodeFailure1,
                Failure = entidad.Failure,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
