using net.paxialabs.mabe.serviplus.data.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.SAP
{
    public class RepositorySAPClientes : BaseRepository
    {

        public void BulkInsert(List<Clientes> data)
        {
            try
            {
                base.DataContext.BulkInsert<Clientes>(data);
            }
            catch (DbException dbex)
            {
                throw dbex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Clientes> GetAll()
        {
            return base.DataContext.Clientes.ToList();
        }

        public List<Clientes> GetAll(string contenedor)
        {
            return base.DataContext.Clientes.Where(p => p.Contenedor == contenedor).ToList();
        }
    }
}
