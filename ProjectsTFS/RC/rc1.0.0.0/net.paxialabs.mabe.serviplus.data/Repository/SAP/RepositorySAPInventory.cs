using net.paxialabs.mabe.serviplus.data.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.SAP
{
    public class RepositorySAPInventory : BaseRepository
    {
        public void BulkInsert(List<REFACCIONES> data)
        {
            try
            {
                base.DataContext.BulkInsert<REFACCIONES>(data);
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

        public List<REFACCIONES> GetAll()
        {
            return base.DataContext.REFACCIONES.ToList();
        }

        public List<REFACCIONES> GetAll(string contenedor)
        {
            return base.DataContext.REFACCIONES.ToList(); //.Where(p => p.Contenedor == contenedor)
        }
    }
}
