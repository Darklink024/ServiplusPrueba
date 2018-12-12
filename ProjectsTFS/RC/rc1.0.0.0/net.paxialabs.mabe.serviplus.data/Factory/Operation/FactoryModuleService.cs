using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryModuleService : BaseFactory<FactoryModuleService, EntityModuleService, ModuleMabe>
    {
        public override EntityModuleService _GetEntity(ModuleMabe entidad)
        {
            return new EntityModuleService() {
                ModuleID = entidad.PK_ModuleID,
                ID = entidad.ModuleID,
                Module = entidad.Module,
                Base = entidad.Base,
                Region = entidad.Region,
                AddressStreet= entidad.AddressStreet,
                AddressNumber= entidad.AddressNumber,
                AddressColony= entidad.AddressColony,
                AddressCP= entidad.AddressCP,
                AddressCity= entidad.AddressCity,
                LatWorkshop=entidad.LatWorkshop,
                LongWorkshop=entidad.LongWorkshop,
                AutorizedWorkshop=entidad.AutorizatedWorkshop,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate                
            };
        }
    }
}
