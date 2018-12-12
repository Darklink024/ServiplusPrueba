using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
    public class RepositoryBlackList: BaseRepository
    {
        public List<EntityBlackList> GetAll()
        {
            return FactoryBlackList.GetList(base.DataContext.BlackList.ToList());
        }
    }
}
