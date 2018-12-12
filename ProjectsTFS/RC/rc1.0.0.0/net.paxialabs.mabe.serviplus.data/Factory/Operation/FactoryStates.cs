using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryStates : BaseFactory<FactoryStates, EntityStates, States>
    {
        public override EntityStates _GetEntity(States entidad)
        {
            return new EntityStates()
            {
                StateID = entidad.PK_StateID,
                CountryID = entidad.FK_CountryID,
                StateName = entidad.StateName,
                Abbreviation = entidad.Abbreviation,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,

            };
        }
    }
}
