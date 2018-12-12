using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
   internal  class FactoryBlackList : BaseFactory<FactoryBlackList,EntityBlackList, BlackList>
    {
        public override EntityBlackList _GetEntity(BlackList entidad)
        {
            return new EntityBlackList()
            {
                PK_BlackListID = entidad.PK_BlackListID,
                BlackListName = entidad.BlackListName,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,
            };
        }
    }
}
