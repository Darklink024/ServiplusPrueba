using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
    public class RepositoryGuaranty : BaseRepository, IRepositoryGET<EntityGuaranty>, IRepositorySET<EntityGuaranty>
    {
        public EntityGuaranty Get(int Id)
        {
            var data = base.DataContext.Guaranty.Where(p => p.PK_GuarantyID == Id);
            if (data.Count() == 1)
                return FactoryGuaranty.Get(data.Single());
            else
                return null;
        }

        public List<EntityGuaranty> GetActives()
        {
            return FactoryGuaranty.GetList(base.DataContext.Guaranty.Where(p => p.Status == true).ToList());
        }

        public List<EntityGuaranty> GetAll()
        {
            return FactoryGuaranty.GetList(base.DataContext.Guaranty.ToList());
        }

        public EntityGuaranty Insert(EntityGuaranty data)
        {
            try
            {
                Guaranty dataNew = new Guaranty()
                {
                    PK_GuarantyID = data.PK_GuarantyID,
                    FK_GuarantyTypeID = data.FK_GuarantyTypeID,
                    GuarantyID = data.GuarantyID,
                    Guaranty1 = data.Guaranty1,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Guaranty.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_GuarantyID = dataNew.PK_GuarantyID;

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

        public EntityGuaranty Update(EntityGuaranty data)
        {
            try
            {
                var dataUpdate = base.DataContext.Guaranty.Where(p => p.PK_GuarantyID == data.PK_GuarantyID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_GuarantyID = data.PK_GuarantyID;
                    //dataUpdate.FK_GuarantyTypeID = data.FK_GuarantyTypeID;
                    dataUpdate.GuarantyID = data.GuarantyID;
                    dataUpdate.Guaranty1 = data.Guaranty1;
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

        public EntityGuaranty GetByGuarantyID(string GuarantyID)
        {
            var data = base.DataContext.Guaranty.Where(p => p.GuarantyID == GuarantyID);
            if (data.Count() == 1)
                return FactoryGuaranty.Get(data.Single());
            else
                return null;
        }
        public List<EntityGuaranty> GetByIDs(List<string> GuarantyID)
        {

            return FactoryGuaranty.GetList(base.DataContext.Guaranty.Where(p=>GuarantyID.Contains(p.GuarantyID)).ToList());
           
        }
    }
}
