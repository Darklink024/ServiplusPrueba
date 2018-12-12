using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessLogCRM
    {
        public EntityLogCRM Get(int Id)
        {
            return new RepositoryLogCRM().Get(Id);
        }
        public EntityLogCRM GetByOrderID(int Id)
        {
            return new RepositoryLogCRM().GetByOrderID(Id);
        }
        public List<EntityLogCRM> GetActives()
        {
            return new RepositoryLogCRM().GetActives();
        }
        public List<EntityLogCRM> GetAll()
        {
            return new RepositoryLogCRM().GetAll();
        }
        public EntityLogCRM Insert(EntityLogCRM data)
        {
            return new RepositoryLogCRM().Insert(data);
        }
        public EntityLogCRM Update(EntityLogCRM data)
        {
            return new RepositoryLogCRM().Update(data);
        }

       
    }
}
