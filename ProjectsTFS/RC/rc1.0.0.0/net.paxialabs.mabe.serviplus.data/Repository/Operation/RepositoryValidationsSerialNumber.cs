using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{ 
    public class RepositoryValidationsSerialNumber : BaseRepository, IRepositoryGET<EntityValidationSerialNumber>, IRepositorySET<EntityValidationSerialNumber>
{

    public EntityValidationSerialNumber Get(int Id)
    {
        var data = base.DataContext.ValidationsSerialNumber.Where(p => p.PK_ValidationsSerialNumberID == Id);
        if (data.Count() == 1)
            return FactoryValidationsSerialNumber.Get(data.Single());
        else
            return null;
    }

    public List<EntityValidationSerialNumber> GetActives()
    {
        return FactoryValidationsSerialNumber.GetList(base.DataContext.ValidationsSerialNumber.Where(p => p.Status == true).ToList());
    }

    public List<EntityValidationSerialNumber> GetAll()
    {
        return FactoryValidationsSerialNumber.GetList(base.DataContext.ValidationsSerialNumber.ToList());
    }

        public void BulkMerge(List<EntityValidationSerialNumber> data)
        {

            base.DataContext.BulkMerge<ValidationsSerialNumber>(data.Select(p => new ValidationsSerialNumber()
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
            }), options => options.ColumnPrimaryKeyExpression = c => new { c.ValidationFormatID});
        }
        public EntityValidationSerialNumber Insert(EntityValidationSerialNumber data)
    {
        try
        {
            ValidationsSerialNumber dataNew = new ValidationsSerialNumber()
            {
                PK_ValidationsSerialNumberID = data.PK_ValidationsSerialNumberID,
                FK_ModelSerialNumberID = data.FK_ModelSerialNumberID,
                ValidationFormatID = data.ValidationFormatID,
                ValidationName = data.ValidationName,
                InitialPosition = data.InitialPosition,
                FinalPosition = data.FinalPosition,
                Allowed = data.Allowed,
                RankID = data.RankID,
                Status = data.Status,
                CreateDate = data.CreateDate,
                ModifyDate = data.ModifyDate
            };
            base.DataContext.ValidationsSerialNumber.Add(dataNew);
            base.DataContext.SaveChanges();

            data.PK_ValidationsSerialNumberID = dataNew.PK_ValidationsSerialNumberID;

            return data;
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

    public EntityValidationSerialNumber Update(EntityValidationSerialNumber data)
    {
        try
        {
            var dataUpdate = base.DataContext.ValidationsSerialNumber.Where(p => p.PK_ValidationsSerialNumberID == data.PK_ValidationsSerialNumberID).SingleOrDefault();

            if (dataUpdate != null)
            {

              //dataUpdate.PK_ValidationsSerialNumberID = data.PK_ValidationsSerialNumberID;
              //dataUpdate.FK_ModelSerialNumberID = data.FK_ModelSerialNumberID;
                dataUpdate.ValidationFormatID = data.ValidationFormatID;
                dataUpdate.ValidationName = data.ValidationName;
                dataUpdate.InitialPosition = data.InitialPosition;
                dataUpdate.FinalPosition = data.FinalPosition;
                dataUpdate.Allowed = data.Allowed;
                dataUpdate.RankID = data.RankID;
                dataUpdate.Status = data.Status;
                dataUpdate.CreateDate = data.CreateDate;
                dataUpdate.ModifyDate = data.ModifyDate;


                base.DataContext.Entry(dataUpdate).State = EntityState.Modified;
                base.DataContext.SaveChanges();
            }
            else
            {
                throw new Exception("No se encontró el registro en la base de datos a modificar.");
            }

            return data;
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



