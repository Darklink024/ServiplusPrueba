using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactorySparePartODS : BaseFactory<FactorySparePartODS, ModelViewSparePartsODS, SpareParts>
    {
        public override ModelViewSparePartsODS _GetEntity(SpareParts entidad)
        {
            var data = new RepositoryStatusScheme().GetStatusScheme(entidad.SparePartStatus, entidad.StatusSchema);
            string statusDescription = "";
            if (data != null) statusDescription = data.Description;

            return new ModelViewSparePartsODS()
            {              
                RefManID = entidad.RefManID,
                Quantity = entidad.Quantity,               
                PosicionItem = entidad.PosicionItem,
                SparePartStatus = entidad.SparePartStatus,
                StatusSchema = entidad.StatusSchema,
                Price = entidad.Price.HasValue ? entidad.Price.Value : 0,
                SparePartDescription = entidad.SparePartDescription,
                StatusDescription = statusDescription
            }; 
        }
    }
}
