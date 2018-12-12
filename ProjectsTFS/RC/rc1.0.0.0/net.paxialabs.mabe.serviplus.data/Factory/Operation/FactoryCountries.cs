using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{

    internal class FactoryCountries : BaseFactory<FactoryCountries, EntityCountries, Countries>
    {
        public override EntityCountries _GetEntity(Countries entidad)
        {
            return new EntityCountries()
            {
                CountryID = entidad.PK_CountryID,
                CountryName = entidad.CountryName,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,

            };
        }
    }
}
