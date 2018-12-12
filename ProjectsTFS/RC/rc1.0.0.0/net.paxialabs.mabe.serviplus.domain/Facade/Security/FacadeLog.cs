using net.paxialabs.mabe.serviplus.domain.Business.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Security
{
    public static class FacadeLog
    {
        public static void MobileWriteEntry(string msg)
        {
            new BusinessMobileLogger().WriteEntry(msg);
        }

        public static void WSWriteEntry(string msg)
        {
            new BusinessWSLogger().WriteEntry(msg);
        }

        public static void WSWriteEntry(Exception ex, string section)
        {
            new BusinessWSLogger().WriteError(ex, section);
        }
    }
}
