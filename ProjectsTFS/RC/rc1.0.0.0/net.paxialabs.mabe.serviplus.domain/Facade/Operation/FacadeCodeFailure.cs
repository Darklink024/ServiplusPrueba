using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
     public class FacadeCodeFailure
    {
        public static EntityCodeFailure Insert(string IDFalla, string DescripcionFalla)
        {
       
            return new BusinessCodeFailure().Insert(IDFalla, DescripcionFalla);
        }

        public static EntityCodeFailure Update(EntityCodeFailure cf, string DescripcionFalla)
        {

            return new BusinessCodeFailure().Update(cf, DescripcionFalla);
        }

        public static List<EntityCodeFailure> GetAll()
        {
            return new BusinessCodeFailure().GetAll();
        }

        public static EntityCodeFailure GetByCodeFailure(string CodeFailure)
        {
            return new BusinessCodeFailure().GetByCodeFailure(CodeFailure);
        }
    }
}
