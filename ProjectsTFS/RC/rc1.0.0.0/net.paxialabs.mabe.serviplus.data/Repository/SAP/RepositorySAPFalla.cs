using net.paxialabs.mabe.serviplus.data.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.SAP
{
    public class RepositorySAPFalla : BaseRepository
    {
        public void BulkInsert(List<Fallas> data)
        {
            try
            {
                base.DataContext.BulkInsert<Fallas>(data);
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

        public List<Fallas> GetAll()
        {
            return base.DataContext.Fallas.ToList();
        }

        public List<Fallas> GetAll(string contenedor)
        {
            return base.DataContext.Fallas.Where(p => p.Contenedor == contenedor).ToList();
        }
    }
}
