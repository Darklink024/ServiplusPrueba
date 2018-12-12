using net.paxialabs.mabe.serviplus.data.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.SAP
{
    public class RepositorySAPCifrasControl : BaseRepository
    {
        public void BulkInsert(List<CifrasControl> data)
        {
            try
            {
                base.DataContext.BulkInsert<CifrasControl>(data);
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

        public List<CifrasControl> GetAll()
        {
            return base.DataContext.CifrasControl.ToList();
        }

        public List<CifrasControl> GetAll(string contenedor)
        {
            return base.DataContext.CifrasControl.Where(p => p.Contenedor == contenedor).ToList();
        }
    }
}
