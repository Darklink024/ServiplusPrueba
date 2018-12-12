using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessCodeFailure
    {
       public EntityCodeFailure Insert(string IDFalla, string DescripcionFalla)
        {
            var objRepository = new RepositoryCodeFailure();
            EntityCodeFailure data = new EntityCodeFailure()
            {
                PK_CodeFailureID = 0,
                CodeFailure1 = IDFalla,
                Failure = DescripcionFalla,
                Status = true,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);
            return data;
        }

        public EntityCodeFailure Update(EntityCodeFailure cf, string DescripcionFalla)
        {
            var objRepository = new RepositoryCodeFailure();
            EntityCodeFailure data = new EntityCodeFailure()
            {
                PK_CodeFailureID = cf.PK_CodeFailureID,
                CodeFailure1 = cf.CodeFailure1,
                Failure = DescripcionFalla,
                Status = cf.Status,
                CreateDate = cf.CreateDate,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Update(data);
            return data;
        }

        public EntityCodeFailure GetByID(int ID)
        {
            var objRepository = new RepositoryCodeFailure();
            return objRepository.Get(ID);
        }
        public List<EntityCodeFailure> GetAll()
        {
            return new RepositoryCodeFailure().GetAll().Select(p => new EntityCodeFailure()
            {
                PK_CodeFailureID = p.PK_CodeFailureID,
                CodeFailure1 = p.CodeFailure1,
                Failure = p.Failure,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityCodeFailure>();
        }

        public EntityCodeFailure GetByCodeFailure(string CodeFailure)
        {
            var objRepository = new RepositoryCodeFailure();
            return objRepository.GetByCodeFailure(CodeFailure);
        }
    }
}
