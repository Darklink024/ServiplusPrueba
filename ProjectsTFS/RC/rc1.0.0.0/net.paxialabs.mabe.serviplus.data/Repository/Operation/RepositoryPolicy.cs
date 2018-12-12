using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
    //class RepositoryPolicy
    //{
    //}

    public class RepositoryPolicy : BaseRepository, IRepositoryGET<EntityPolicy>, IRepositorySET<EntityPolicy>
    {
        public EntityPolicy Get(int Id)
        {
            var data = base.DataContext.Policy.Where(p => p.PK_PolicyID == Id);
            if (data.Count() == 1)
                return FactoryPolicy.Get(data.Single());
            else
                return null;
        }
        public EntityPolicy GetPolicyFolio(int OrderID, string Folio)
        {
            var data = base.DataContext.Policy.Where(p => p.FK_OrderID == OrderID && p.IDPolicy == Folio);
            if (data.Count() > 0)
                return FactoryPolicy.Get(data.First());
            else
                return null;
        }
        public EntityPolicy GetByOrder(int OrderID)
        {
            var data = base.DataContext.Policy.Where(p => p.FK_OrderID== OrderID);
            if (data.Count() == 1)
                return FactoryPolicy.Get(data.Single());
            else
                return null;
        }
        public List<EntityPolicy> GetActives()
        {
            return FactoryPolicy.GetList(base.DataContext.Policy.Where(p => p.Status == true).ToList());
        }

        //public List<EntityPolicy> GetAll(string SendCRM, DateTime syncDate)
        //{
        //    return FactoryPolicy.GetList(base.DataContext.PolicyRefMan.Where(p => p.SendCRM == SendCRM & p.FK_StatusSchemeID != 44 & p.OrderExecuteDate >= syncDate).ToList());
        //}

        public List<EntityPolicy> GetAll()
        {
            return FactoryPolicy.GetList(base.DataContext.Policy.ToList());
        }
        //
        //PolicyCreateDate cuestionar
        public List<EntityPolicy> GetByRange(DateTime Inicio, DateTime Fin)
        {
            return FactoryPolicy.GetList(base.DataContext.Policy.Where(p => DbFunctions.TruncateTime(p.CreateDate) >= Inicio && DbFunctions.TruncateTime(p.CreateDate) <= Fin).ToList());
        }
        //p.FK_EmployeeID.Value
        public List<EntityPolicy> GetAll(List<int> EmployeeID, DateTime fhExecute)
        {
            return FactoryPolicy.GetList(base.DataContext.Policy.Where(p => EmployeeID.Contains(p.FK_EmployeeID) && DbFunctions.TruncateTime(p.CreateDate) == fhExecute ).ToList());
        }

        public List<EntityPolicy> GetAllListODS(List<int> EmployeeID, DateTime fhExecute, DateTime MaxfhExecute, bool PreODS)
        {
            return FactoryPolicy.GetList(base.DataContext.Policy.Where(p => EmployeeID.Contains(p.FK_EmployeeID) && DbFunctions.TruncateTime(p.CreateDate) > fhExecute && DbFunctions.TruncateTime(p.CreateDate) <= MaxfhExecute).ToList());
        }

        public List<EntityPolicy> GetAllListODSJourney(List<int> EmployeeID, DateTime MinfhExecute, DateTime MaxfhExecute, bool PreODS)
        {
            return FactoryPolicy.GetList(base.DataContext.Policy.Where(p => EmployeeID.Contains(p.FK_EmployeeID) && DbFunctions.TruncateTime(p.CreateDate) >= MinfhExecute && DbFunctions.TruncateTime(p.CreateDate) <= MaxfhExecute).ToList());
        }
        //
        public List<EntityPolicy> GetByEmpoyeeDate(int UserID, DateTime date)
        {
            return FactoryPolicy.GetList(base.DataContext.Policy.Where(p => p.FK_EmployeeID == UserID && DbFunctions.TruncateTime( p.CreateDate) == date).OrderByDescending(o=>o.CreateDate).ToList());
        }
        public EntityPolicy Insert(EntityPolicy data)
        {
            try
            {
                Policy datanew = new Policy()
                {
                    PK_PolicyID=data.PK_PolicyID,
                    FK_OrderID = data.FK_OrderID,
                    FK_ClientID = data.FK_ClientID,
                    FK_EmployeeID = data.FK_EmployeeID,
                    FK_PaymentID = data.FK_PaymentID,
                    FK_Invoice_ID = data.FK_Invoice_ID,
                    FK_QuotationID = data.FK_QuotationID,
                    FK_ProductID = data.FK_ProductID,
                    FK_ShopPlaceID=  data.FK_ShopPlace,
                    IDPolicy = data.IDPolicy,
                    PolicyPrice = data.PolicyPrice,
                    SerialNumber=data.SerialNumber,
                    PriceList= data.PriceList,
                    Coin = data.Coin,
                    SalesOrg = data.SalesOrg,
                    ccGroup = data.ccGroup,
                    MaterialGroup4=data.MaterialGroup4,
                    GuarantyEnd = data.GuarantyEnd,
                    GuarantyStart = data.GuarantyStart,
                    PolicyDate = data.PolicyDate,
                    Status=data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate,
                };
                base.DataContext.Policy.Add(datanew);
                base.DataContext.SaveChanges();

                data.PK_PolicyID = datanew.PK_PolicyID;

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

        public EntityPolicy Update(EntityPolicy data)
        {
            try
            {
                var dataUpdate = base.DataContext.Policy.Where(p => p.PK_PolicyID == data.PK_PolicyID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.FK_OrderID = data.FK_OrderID;
                    dataUpdate.FK_ClientID = data.FK_ClientID;
                    dataUpdate.FK_EmployeeID = data.FK_EmployeeID;
                    dataUpdate.FK_PaymentID = data.FK_PaymentID;
                    dataUpdate.FK_Invoice_ID = data.FK_Invoice_ID;
                    dataUpdate.FK_QuotationID = data.FK_QuotationID;
                    dataUpdate.FK_ProductID = data.FK_ProductID;
                    dataUpdate.FK_ShopPlaceID = data.FK_ShopPlace;
                    dataUpdate.IDPolicy = data.IDPolicy;
                    dataUpdate.PolicyPrice = data.PolicyPrice;
                    dataUpdate.Coin = data.Coin;
                    dataUpdate.SalesOrg = data.SalesOrg;
                    dataUpdate.ccGroup = data.ccGroup;
                    dataUpdate.SerialNumber = data.SerialNumber;
                    dataUpdate.PriceList = data.PriceList;
                    dataUpdate.MaterialGroup4 = data.MaterialGroup4;
                    dataUpdate.GuarantyEnd = data.GuarantyEnd;
                    dataUpdate.GuarantyStart = data.GuarantyStart;
                    dataUpdate.PolicyDate = data.PolicyDate;
                    dataUpdate.Status = true;
                    dataUpdate.CreateDate = DateTime.UtcNow;
                    dataUpdate.ModifyDate = DateTime.UtcNow;
                   


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
