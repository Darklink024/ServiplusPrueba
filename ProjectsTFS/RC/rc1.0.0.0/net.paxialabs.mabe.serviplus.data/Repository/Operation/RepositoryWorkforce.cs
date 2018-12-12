using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
           public class RepositoryWorkforce : BaseRepository, IRepositoryGET<EntityWorkforce>, IRepositorySET<EntityWorkforce>
    {
        public EntityWorkforce Get(int Id)
        {
            var data = base.DataContext.Workforce.Where(p => p.PK_WorkforceID == Id);
            if (data.Count() == 1)
                return FactoryWorkforce.Get(data.Single());
            else
                return null;
        }

        //revisar

        public EntityWorkforce GetbyWfid(int Id)
        {
            var data = base.DataContext.Workforce.Where(p => p.PK_WorkforceID == Id);
            if (data.Count() == 1)
                return FactoryWorkforce.Get(data.Single());
            else
                return null;
        }
        public List<EntityWorkforce> GetActives()
        {
            return FactoryWorkforce.GetList(base.DataContext.Workforce.Where(p => p.Status == true).ToList());
        }

        public List<EntityWorkforce> GetAll()
        {
            return FactoryWorkforce.GetList(base.DataContext.Workforce.ToList());
        }

        public List<ModelViewWorkforcePrices> GetAll(DateTime? fh)
        {
            return fh.HasValue ? (from w in base.DataContext.Workforce
                                  join p in base.DataContext.Prices on w.PK_WorkforceID equals p.FK_WorkforceID
                                  where p.ModifyDate >= fh
                                  select new ModelViewWorkforcePrices()
                                  {
                                      PK_WorkforceID = w.PK_WorkforceID,
                                      WorkforceID = w.WorkforceID,
                                      Description = w.Description,
                                      PriceID = p.PK_PriceID,
                                      TypeCondition = p.TypeCondition,
                                      SalesOrganization = p.SalesOrganization,
                                      DistributionChannel = p.DistributionChannel,
                                      ListPrice = p.ListPrice,
                                      GroupMaterial1 = p.GroupMaterial1,
                                      GroupMaterial4 = p.GroupMaterial4,
                                      Price = p.Price,
                                      Coin = p.Coin
                                  }).ToList() : (from w in base.DataContext.Workforce
                                                 join p in base.DataContext.Prices on w.PK_WorkforceID equals p.FK_WorkforceID
                                                 select new ModelViewWorkforcePrices()
                                                 {
                                                     PK_WorkforceID = w.PK_WorkforceID,
                                                     WorkforceID = w.WorkforceID,
                                                     Description = w.Description,
                                                     PriceID = p.PK_PriceID,
                                                     TypeCondition = p.TypeCondition,
                                                     SalesOrganization = p.SalesOrganization,
                                                     DistributionChannel = p.DistributionChannel,
                                                     ListPrice = p.ListPrice,
                                                     GroupMaterial1 = p.GroupMaterial1,
                                                     GroupMaterial4 = p.GroupMaterial4,
                                                     Price = p.Price,
                                                     Coin = p.Coin
                                                 }).ToList();
        }

        public EntityWorkforce Insert(EntityWorkforce data)
        {
            try
            {
                Workforce dataNew = new Workforce()
                {
                    PK_WorkforceID = data.PK_WorkforceID,
                    WorkforceID = data.WorkforceID,
                    Description = data.Description,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Workforce.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_WorkforceID = dataNew.PK_WorkforceID;

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


        public EntityWorkforce Update(EntityWorkforce data)
        {
            try
            {
                var dataUpdate = base.DataContext.Workforce.Where(p => p.PK_WorkforceID == data.PK_WorkforceID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.WorkforceID = data.WorkforceID;
                    dataUpdate.Description = data.Description;
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
