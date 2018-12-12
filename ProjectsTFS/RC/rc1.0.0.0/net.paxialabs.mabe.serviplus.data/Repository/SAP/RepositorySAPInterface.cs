using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.SAP
{
    public class RepositorySAPInterface : BaseRepository
    {
        public void Process()
        {
            try
            {
                base.DataContext.Database.CommandTimeout = 10800;
                base.DataContext.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, "[SAP].[sp_exe_Interface]");
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
    }
}
