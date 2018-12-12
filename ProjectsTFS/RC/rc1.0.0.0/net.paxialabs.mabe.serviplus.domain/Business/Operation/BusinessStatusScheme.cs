using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessStatusScheme
    {
        public EntityStatusScheme GetStatusScheme(string StatusScheme, string StatusHeadboard)
        {
            var objRepository = new RepositoryStatusScheme();
            return objRepository.GetStatusScheme(StatusScheme, StatusHeadboard);
        }
        public List< EntityStatusScheme> GetStatusSchemeID(List<string> StatusScheme, string StatusHeadboard)
        {
            var objRepository = new RepositoryStatusScheme();
            return objRepository.GetStatusSchemeID(StatusScheme, StatusHeadboard);
        }

        public List<EntityStatusScheme> GetAll()
        {
            return new RepositoryStatusScheme().GetAll().Select(p => new EntityStatusScheme()
            {
                PK_StatusSchemeID = p.PK_StatusSchemeID,
                StatusScheme1 = p.StatusScheme1,
                StatusHeadboard = p.StatusHeadboard,
                Description = p.Description,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityStatusScheme>();
        }

        public EntityStatusScheme Get(int ID)
        {
            var data = new RepositoryStatusScheme().Get(ID);

            EntityStatusScheme result = null;

            if (data != null)
            {
                result = new EntityStatusScheme()
                {
                    PK_StatusSchemeID = data.PK_StatusSchemeID,
                    StatusScheme1 = data.StatusScheme1,
                    StatusHeadboard = data.StatusHeadboard,
                    Description = data.Description,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
            }
            return result;
        }
    }
}
