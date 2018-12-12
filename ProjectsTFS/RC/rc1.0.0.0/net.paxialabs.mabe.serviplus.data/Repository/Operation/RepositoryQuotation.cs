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

    public class RepositoryQuotation : BaseRepository, IRepositoryGET<EntityQuotation>, IRepositorySET<EntityQuotation>
    {
        public EntityQuotation Get(int Id)
        {
            var data = base.DataContext.Quotation.Where(p => p.PK_QuotationID == Id);
            if (data.Count() == 1)
                return FactoryQuotation.Get(data.Single());
            else
                return null;
        }

        public List<ModelViewQuotation> GetList(string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, string User, string TypeQuotation)
        {
            try
            {
                base.DataContext.Database.CommandTimeout = 10800;
                return base.DataContext.sp_get_Quotation(StatusVisitID, ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate, User, TypeQuotation).Select(p => new ModelViewQuotation() {
                    FK_OrderID = p.FK_OrdenID.Value,
                    TypeQuotation = p.TypeQuotation,
                    Folio = p.Folio,
                    OrdenVenta = p.OrdenVenta,
                    OrderID = p.OrderID,
                    FK_CauseOrderID = p.FK_CauseOrderID.Value,
                    ClientName = p.ClientName,
                    EmployeeName = p.EmployeeName,
                    Total = p.Total,
                    URL = p.URL,
                    Contract = p.Contract,
                    StatusQuotation = p.FK_CauseOrderID.Value,
                    QuotationID = p.PK_QuotationID
                }).ToList();
               
               
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

        public List<EntityQuotation> GetActives()
        {
            return FactoryQuotation.GetList(base.DataContext.Quotation.Where(p => p.Status == true).ToList());
        }

        public List<EntityQuotation> GetAll()
        {
            return FactoryQuotation.GetList(base.DataContext.Quotation.ToList());
        }
        public List<EntityQuotation> GeListByOrder(List<int> OrderIDs)
        {
            return FactoryQuotation.GetList(base.DataContext.Quotation.Where(p => OrderIDs.Contains(p.FK_OrdenID.Value)).ToList());
        }

        public EntityQuotation Insert(EntityQuotation data)
        {
            try
            {
                Quotation dataNew = new Quotation()
                {
                    PK_QuotationID = data.PK_QuotationID,
                    FK_OrdenID = data.FK_OrdenID,
                    SubTotal = data.SubTotal,
                    IVA = data.IVA,
                    Total = data.Total,
                    Folio = data.Folio,
                    URL = data.URL,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate,
                    FK_TypeQuotation=data.TypeQuotation,
                    FK_EmployeeID=data.FK_EmployeeID
                    
                    
                };
                base.DataContext.Quotation.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_QuotationID = dataNew.PK_QuotationID;

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
        

        public EntityQuotation Update(EntityQuotation data)
        {
            try
            {
                var dataUpdate = base.DataContext.Quotation.Where(p => p.PK_QuotationID == data.PK_QuotationID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_QuotationID = data.PK_QuotationID;
                    dataUpdate.FK_OrdenID = data.FK_OrdenID;
                    dataUpdate.SubTotal = data.SubTotal;
                    dataUpdate.IVA = data.IVA;
                    dataUpdate.Total = data.Total;
                    dataUpdate.Folio = data.Folio;
                    dataUpdate.URL = data.URL;
                    dataUpdate.Status = data.Status;
                    dataUpdate.OrdenVenta = data.OrdenVenta;
                    dataUpdate.CreateDate = data.CreateDate;
                    dataUpdate.ModifyDate = data.ModifyDate;
                    dataUpdate.FK_EmployeeID = data.FK_EmployeeID;
                    //dataUpdate.FK_TypeQuotation = data.TypeQuotation;


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

        public EntityQuotation GetByOrder(int OrderID)
        {
            var data = base.DataContext.Quotation.Where(p => p.FK_OrdenID == OrderID);
            if (data.Count() == 1)
                return FactoryQuotation.Get(data.Single());
            else
                return null;
        }
        public EntityQuotation GetByID(int QuotationID)
        {
            var data = base.DataContext.Quotation.Where(p => p.PK_QuotationID ==QuotationID);
            if (data.Count() == 1)
                return FactoryQuotation.Get(data.Single());
            else
                return null;
        }
        public EntityQuotation GetByOrderFolio(int OrderID, string Folio)
        {
            var data = base.DataContext.Quotation.Where(p => p.FK_OrdenID == OrderID && p.Folio== Folio);
            if (data.Count() == 1)
                return FactoryQuotation.Get(data.Single());
            else
                return null;
        }
        public EntityQuotation GetByOrdertype(int OrderID, int type)
        {
            var data = base.DataContext.Quotation.Where(p => p.FK_OrdenID == OrderID && p.FK_TypeQuotation == type);
            if (data.Count() == 1)
                return FactoryQuotation.Get(data.Single());
            else
                return null;
        }
        public List<EntityQuotation> GetByEmpoyeeDate(int UserID, DateTime date)
        {
           return FactoryQuotation.GetList(base.DataContext.Quotation.Where(p =>p.FK_EmployeeID==UserID && DbFunctions.TruncateTime(p.CreateDate)== date).OrderByDescending(o=>o.CreateDate).ToList());
        }
    }
}
