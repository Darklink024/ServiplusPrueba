using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;

namespace net.paxialabs.mabe.serviplus.data.Factory.Security
{
    internal class FactoryUser : BaseFactory<FactoryUser, EntityUser, User>
    {
        public override EntityUser _GetEntity(User entidad)
        {
            return new EntityUser() {
                UserID = entidad.UserID,
                ChangePassword = entidad.ChangePassword,
                DateCreate = entidad.DateCreate,
                DateLastAccess = entidad.DateLastAccess,
                DateModification = entidad.DateModification,
                Email = entidad.Email,
                Name = entidad.Name,
                Password = entidad.Password,
                ModuleID=entidad.FK_ModuleID,
                ProfileID = entidad.ProfileID,
                Status = entidad.Status,
                Token = entidad.Token,               
                UserName = entidad.UserName                
            };
        }
    }
}
