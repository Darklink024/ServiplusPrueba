using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryOrder : BaseFactory<FactoryOrder, EntityOrder, Orders>
    {
        public override EntityOrder _GetEntity(Orders entidad)
        {
            return new EntityOrder()
            {  
                PK_OrderID = entidad.PK_OrderID,
                FK_InstalledBaseID = entidad.FK_InstalledBaseID,
                FK_ClientID = entidad.FK_ClientID,
                FK_EmployeeID = entidad.FK_EmployeeID,
                FK_ModuleID = entidad.FK_ModuleID,
                FK_GuarantyID = entidad.FK_GuarantyID,
                FK_StatusSchemeID = entidad.FK_StatusSchemeID,
                OrderID = entidad.OrderID,
                TechnicalID = entidad.TechnicalID,
                PreOrder = entidad.PreOrder,
                URLPreOrder = entidad.URLPreOrder,
                Symptom = entidad.Symptom,
                Failure1 = entidad.Failure1,
                Failure2 = entidad.Failure2,
                Failure3 = entidad.Failure3,
                Failure4 = entidad.Failure4,
                Failure5 = entidad.Failure5,
                Note = entidad.Note,
                OrderFahter = entidad.OrderFahter,
                SendCRM = entidad.SendCRM,
                Status = entidad.Status,
                OrderCreateDate = entidad.OrderCreateDate,
                OrderExecuteDate = entidad.OrderExecuteDate,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,
                ExtraKilometres=entidad.ExtraKilometres
            };
        }
    }
}
