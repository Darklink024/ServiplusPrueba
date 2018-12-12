using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
    public class RepositoryTypeQuotation : BaseRepository, IRepositoryGET<EntityTypeQuotation>
    {
        public EntityTypeQuotation Get(int Id)
        {
            var data = base.DataContext.TypeQuotation.Where(p => p.PK_TypeQuotation == Id);
            if (data.Count() == 1)
                return FactoryTypeQuotation.Get(data.Single());
            else
                return null;
        }
       

        public List<EntityTypeQuotation> GetActives()
        {
            return FactoryTypeQuotation.GetList(base.DataContext.TypeQuotation.Where(p => p.Status == true).ToList());
        }

        public List<EntityTypeQuotation> GetAll()
        {
            return FactoryTypeQuotation.GetList(base.DataContext.TypeQuotation.ToList());
        }
    }
}
