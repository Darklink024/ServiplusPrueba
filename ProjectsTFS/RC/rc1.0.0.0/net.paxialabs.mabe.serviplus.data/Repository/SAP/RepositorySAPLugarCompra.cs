using net.paxialabs.mabe.serviplus.data.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.SAP
{
    public class RepositorySAPLugarCompra : BaseRepository
    {
        public void BulkInsert(List<LugarCompra> data)
        {
            try
            {
                base.DataContext.BulkInsert<LugarCompra>(data);
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

        public List<LugarCompra> GetAll()
        {
            return base.DataContext.LugarCompra.ToList();
        }

        public List<LugarCompra> GetAll(string contenedor)
        {
            return base.DataContext.LugarCompra.Where(p => p.Contenedor == contenedor).ToList();
        }
    }
}
