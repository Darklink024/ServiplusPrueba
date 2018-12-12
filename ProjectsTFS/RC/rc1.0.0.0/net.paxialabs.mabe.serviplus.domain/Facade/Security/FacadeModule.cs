using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System.Collections.Generic;


namespace net.paxialabs.mabe.serviplus.domain.Facade.Security
{
    public static class FacadeModule
    {
        public static List<ModelViewModule> GetAll()
        {
            return new BusinessModule().GetAll();
        }
        public static List<ModelViewModule> GetModuleByID(int ModuleID)
        {
            return new BusinessModule().GetAll();
        }
        public static List<ModelViewModule> GetActives()
        {
            return new BusinessModule().GetActives();
        }

        public static ModelViewModule Insert(ModelViewModule model)
        {
            return new BusinessModule().Insert(model);
        }

        public static ModelViewModule Update(ModelViewModule model)
        {
            return new BusinessModule().Update(model);
        }

        public static void SetStatus(List<int> IDs)
        {
            new BusinessModule().SetStatus(IDs);
        }
    }
}
