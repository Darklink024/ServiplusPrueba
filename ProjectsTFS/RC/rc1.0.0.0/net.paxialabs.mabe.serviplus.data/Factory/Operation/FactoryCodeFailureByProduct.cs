using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryCodeFailureByProduct : BaseFactory <FactoryCodeFailureByProduct, EntityCodeFailureByProduct, CodeFailureByProduct>
    {
        public override EntityCodeFailureByProduct _GetEntity(CodeFailureByProduct entidad)
        {
            return new EntityCodeFailureByProduct()
            {
                FK_CodeFailureID = entidad.FK_CodeFailureID,
                FK_ProductID = entidad.FK_ProductID,
                Complexity = entidad.Complexity,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
