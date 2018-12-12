using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessWorkforce
    {
        public List<EntityWorkforce> GetAll()
        {
            return new RepositoryWorkforce().GetAll().Select(p => new EntityWorkforce()
            {
                PK_WorkforceID = p.PK_WorkforceID,
                WorkforceID = p.WorkforceID,
                Description = p.Description,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityWorkforce>();
        }

        public List<ModelViewWorkforcePrices> GetAll(DateTime? date)
        {
            return new RepositoryWorkforce().GetAll(date);
        }

        //revisar

       public EntityWorkforce GetByID(int Pk_WorkforceID)
        {
   
            return new RepositoryWorkforce().GetbyWfid(Pk_WorkforceID);
        }

    }
}
