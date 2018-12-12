using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
   internal class FactoryModelSerialNumber : BaseFactory<FactoryModelSerialNumber, EntityModelSerialNumber, ModelSerialNumber>
    {
        public override EntityModelSerialNumber _GetEntity(ModelSerialNumber entidad)
        {
            return new EntityModelSerialNumber()
            {
                PK_ModelSerialNumberID = entidad.PK_ModelSerialNumberID,
                FK_ProducID = entidad.FK_ProducID,
                Model = entidad.Model,
                ValidationFormatID = entidad.ValidationFormatID,
                InitialDate = entidad.InitialDate,
                EndDate = entidad.EndDate,
                ValidDate = entidad.ValidDate,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
