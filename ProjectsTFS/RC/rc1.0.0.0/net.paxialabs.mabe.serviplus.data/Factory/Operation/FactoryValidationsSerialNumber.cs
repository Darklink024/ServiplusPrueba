using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryValidationsSerialNumber : BaseFactory<FactoryValidationsSerialNumber, EntityValidationSerialNumber, ValidationsSerialNumber>
    {
        public override EntityValidationSerialNumber _GetEntity(ValidationsSerialNumber entidad)
        {
            return new EntityValidationSerialNumber()
            {
                PK_ValidationsSerialNumberID = entidad.PK_ValidationsSerialNumberID,
                FK_ModelSerialNumberID = entidad.FK_ModelSerialNumberID,
                ValidationFormatID = entidad.ValidationFormatID,
                ValidationName = entidad.ValidationName,
                InitialPosition = entidad.InitialPosition,
                FinalPosition = entidad.FinalPosition,
                Allowed = entidad.Allowed,
                RankID = entidad.RankID,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
