using net.paxialabs.mabe.serviplus.domain.Business.Notification;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeConfiguration
    {
        public static ModelViewConfiguration Get(int Id)
        {
            return new BusinessConfiguration().Get(Id);
        }

        public static List<ModelViewConfiguration> GetActives()
        {
            return new BusinessConfiguration().GetActives();
        }

        public static List<ModelViewConfiguration> GetAll()
        {            
            return new BusinessConfiguration().GetAll();
        }

        public static ModelViewConfiguration InsertURLY(ModelViewConfiguration data, int[] Usuarios)
        {
            return new BusinessConfiguration().InsertURLY(data,  Usuarios);
        }

        public static ModelViewConfiguration UpdateURLY(ModelViewConfiguration data, int[] Usuarios)
        {
            return new BusinessConfiguration().UpdateURLY(data, Usuarios);
        }
        public static ModelViewConfiguration Insert(string fileUpload,ModelViewConfiguration data, string path, int[] Usuarios)
        {
            return new BusinessConfiguration().Insert(fileUpload, data, path, Usuarios);
        }

        public static ModelViewConfiguration Update(string fileUpload, ModelViewConfiguration data, string path, int[] Usuarios)
        {
            return new BusinessConfiguration().Update(fileUpload, data, path, Usuarios);
        }

        public static List<ModelViewConfiguration> GetListAll()
        {
            return new BusinessConfiguration().GetListAll();
        }

        public static ModelViewConfiguration Update(ModelViewConfiguration data)
        {
            return new BusinessConfiguration().Update(data);
        }
    }
}
