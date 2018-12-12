using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessValidationsSerialNumber
    {
        public List<EntityValidationSerialNumber> GetAll()
        {
            return new RepositoryValidationsSerialNumber().GetAll().Select(p => new EntityValidationSerialNumber()
            {
                PK_ValidationsSerialNumberID = p.PK_ValidationsSerialNumberID,
                FK_ModelSerialNumberID = p.FK_ModelSerialNumberID,
                ValidationFormatID = p.ValidationFormatID,
                ValidationName = p.ValidationName,
                InitialPosition = p.InitialPosition,
                FinalPosition = p.FinalPosition,
                Allowed = p.Allowed,
                RankID = p.RankID,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityValidationSerialNumber>();
        }

        public void BulkMerge(List<EntityValidationSerialNumber> data)
        {
            new RepositoryValidationsSerialNumber().BulkMerge(data);
        }
    }
}
