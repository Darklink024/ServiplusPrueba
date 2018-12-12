using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
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
    internal class BusinessREFACCIONES
    {
        public void BulkInsert(List<EntityREFACCIONES> Refacciones)
        {
            var objRepository = new RepositoryREFACCIONES();
            objRepository.BulkInsert(Refacciones);

        }

        public  List<ModelViewInventory> GetInventory(ModelViewUserG Data)
        {
            var NegocioUsuario = new BusinessUsers();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioModulo = new BusinessModuleService();
            var NegocioRecf = new BusinessREFACCIONES();
            var dataUsuario = NegocioUsuario.GetUserByToken(Data.TokenUser);
            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
      
            if (Data.TokenApp != GlobalConfiguration.TokenWEB)
                if (Data.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            var modulo = NegocioModulo.GetAll().Where(a => empleado.Select(p => p.FK_ModuleID).ToList().Contains(a.ModuleID));

            var lt = NegocioRecf.GetAllRefc(DateTime.Now.Date, modulo.Select(p=> p.ID).ToList(), empleado.Select(p=> p.StoreProp).ToList()).Select(p=> new ModelViewInventory ()
            {
                RefManID = p.ID_REF,
                Quantity = p.TOTDISP.Value
            }).ToList();

            if(lt.Count==0)
            {
                ModelViewInventory model = new ModelViewInventory();
                lt.Add(model);
            }
            return lt;

        }

        public List<EntityREFACCIONES> GetAllRefc(DateTime Fecha, List<string> Centro, List<string> Almacen)
        {
            return new RepositoryREFACCIONES().GetAllRefc(Fecha, Centro, Almacen);
        }
    }
}
