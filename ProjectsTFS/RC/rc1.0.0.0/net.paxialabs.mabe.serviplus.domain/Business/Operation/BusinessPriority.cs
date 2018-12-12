using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{

    internal class BusinessPriority
    {

        public List<ModelViewPriority> GetListPriority(ModelViewUserG objCred)
        {
            var NegocioUsuario = new BusinessUsers();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            
            var NegocioProgramacion = new BusinessSchedule();
            //var dataPriority = GetAll();
            var dataPriority = new List<EntityPriority>();
            if(objCred.Date == null)
            { dataPriority = GetAll(); }
            else
            { dataPriority = GetAll().Where(p=> p.ModifyDate >= objCred.Date).ToList(); }
            var dataschedule = NegocioProgramacion.GetAll();
            var lt  = (from c in dataPriority
                        join p in dataschedule on c.FK_ScheduleID equals p.PK_ScheduleID
                        select new ModelViewPriority()
                        { Priority = c.Priority1, ScheduleStart = p.ScheduleStart, ScheduleEnd = p.ScheduleEnd }).ToList();
            return lt;
        }


        public List<EntityPriority> GetAll()
        {
            return new RepositoryPriority().GetAll().Select(p => new EntityPriority()
            {
                PK_PriorityID = p.PK_PriorityID,
                FK_ScheduleID = p.FK_ScheduleID,
                Priority1 = p.Priority1,
                Description = p.Description,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate,
            }).ToList<EntityPriority>();
        }

        public void SetStatus(List<int> IDs)
        {
            foreach (var item in IDs)
            {
                var data = new RepositoryPriority().Get(item);
                data.Status = !data.Status;
                data.ModifyDate = DateTime.UtcNow;
                new RepositoryPriority().Update(data);
            }
        }
        public List<EntityPriority> GetActives()
        {
            return new RepositoryPriority().GetActives();
        }
    }
}
