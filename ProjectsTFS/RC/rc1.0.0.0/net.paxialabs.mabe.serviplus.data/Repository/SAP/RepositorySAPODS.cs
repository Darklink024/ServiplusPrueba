using net.paxialabs.mabe.serviplus.data.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.SAP
{
    public class RepositorySAPODS : BaseRepository
    {
        public void BulkInsert(List<ODS> data)
        {
            try
            {
                base.DataContext.BulkInsert<ODS>(data);
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

        public List<ODS> GetAll()
        {
            return base.DataContext.ODS.ToList();
        }

        public List<ODS> GetAll(string contenedor)
        {
            return base.DataContext.ODS.Where(p => p.Contenedor == contenedor).ToList();
        }
    }
}
