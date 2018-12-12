using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Security
{
    internal class BusinessPermission
    {
        public List<ModelViewPermission> GetAll(int ProfileID)
        {
            return new RepositoryPermission().GetAll(ProfileID).Select(p => new ModelViewPermission()
            {
                ProfileID = p.ProfileID,
                ModuleID = p.ModuleID,
                Access = p.Access,
                Add = p.Add,
                Delete = p.Delete,
                Export = p.Export,
                Read = p.Read,
                Update = p.Update,
                Module = new RepositoryModule().Get(p.ModuleID).Module,
                Section = new RepositoryModule().Get(p.ModuleID).Section
            }).ToList<ModelViewPermission>();
        }

        public ModelViewPermission Insert(ModelViewPermission model)
        {
            EntityPermission data = new EntityPermission()
            {
                ProfileID = model.ProfileID,
                ModuleID = model.ModuleID,
                Access = model.Access,
                Add = model.Add,
                Delete = model.Delete,
                Export = model.Export,
                Read = model.Read,
                Update = model.Update
            };

            data = new RepositoryPermission().Insert(data);
            
            return model;
        }

        public ModelViewPermission Update(ModelViewPermission model)
        {
            EntityPermission data = new EntityPermission()
            {
                ProfileID = model.ProfileID,
                ModuleID = model.ModuleID,
                Access = model.Access,
                Add = model.Add,
                Delete = model.Delete,
                Export = model.Export,
                Read = model.Read,
                Update = model.Update
            };

            data = new RepositoryPermission().Update(data);
            
            return model;
        }

        public bool Validate(string token, string URL)
        {
            try
            {
                var dataUsuario = new RepositoryUser().GetToken(token);
                var dataPermiso = GetAll(dataUsuario.ProfileID);
                var dataModulo = new RepositoryModule().GetURL(URL);
                return dataPermiso.Where(p => p.ModuleID == dataModulo.ModuleID).Single().Access;
            }
            catch
            {
                return true;
            }
        }

        public bool Validate(string token, string URL, AuditAction action)
        {
            try
            {
                var dataUsuario = new RepositoryUser().GetToken(token);
                var dataPermiso = GetAll(dataUsuario.ProfileID);
                var dataModulo = new RepositoryModule().GetURL(URL);
                bool AllowAccess = false;

                switch (action)
                {
                    case AuditAction.Access:
                        AllowAccess = dataPermiso.Where(p => p.ModuleID == dataModulo.ModuleID).Single().Access;
                        break;
                    case AuditAction.Read:
                        AllowAccess = dataPermiso.Where(p => p.ModuleID == dataModulo.ModuleID).Single().Read;
                        break;
                    case AuditAction.Add:
                        AllowAccess = dataPermiso.Where(p => p.ModuleID == dataModulo.ModuleID).Single().Add;
                        break;
                    case AuditAction.Update:
                        AllowAccess = dataPermiso.Where(p => p.ModuleID == dataModulo.ModuleID).Single().Update;
                        break;
                    case AuditAction.Delete:
                        AllowAccess = dataPermiso.Where(p => p.ModuleID == dataModulo.ModuleID).Single().Delete;
                        break;
                    case AuditAction.Export:
                        AllowAccess = dataPermiso.Where(p => p.ModuleID == dataModulo.ModuleID).Single().Export;
                        break;
                    default:
                        AllowAccess = false;
                        break;
                }

                if (AllowAccess) new BusinessAudit().Insert(new ModelViewAudit() { ModuleID = dataModulo.ModuleID, UserID = dataUsuario.UserID, Action = action.ToString() });

                return AllowAccess;
            }
            catch
            {
                return true;
            }
        }

        public void Set(int? ProfileID, int? ModuleID)
        {
            if(ProfileID.HasValue)
            {
                var data = new RepositoryPermission().GetAll(ProfileID.Value);
                foreach (var item in new BusinessModule().GetAll().Where(p => !data.Select(q => q.ModuleID).ToList<int>().Contains(p.ModuleID)))
                {
                    new RepositoryPermission().Insert(new EntityPermission() {
                        ProfileID = ProfileID.Value,
                        ModuleID = item.ModuleID,
                        Access = false,
                        Read = false,
                        Export = false,
                        Add = false,
                        Update = false,
                        Delete = false
                    });    
                }
            }

            if(ModuleID.HasValue)
            {
                
                foreach (var item in new BusinessProfile().GetAll())
                {
                    var dataPermission = new RepositoryPermission().GetAll().Where(p => p.ProfileID == item.ProfileID & p.ModuleID == ModuleID.Value);
                    if(dataPermission.Count() == 0)
                    {
                        new RepositoryPermission().Insert(new EntityPermission()
                        {
                            ProfileID = item.ProfileID,
                            ModuleID = ModuleID.Value,
                            Access = false,
                            Read = false,
                            Export = false,
                            Add = false,
                            Update = false,
                            Delete = false
                        });
                    }
                }
            }
        }
    }
}
