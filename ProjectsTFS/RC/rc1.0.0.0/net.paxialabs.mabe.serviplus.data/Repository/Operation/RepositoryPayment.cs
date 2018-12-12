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
    public class RepositoryPayment : BaseRepository, IRepositoryGET<EntityPayment>, IRepositorySET<EntityPayment>
    {
        public EntityPayment Get(int Id)
        {
            var data = base.DataContext.Payment.Where(p => p.PK_PaymentID == Id);
            if (data.Count() == 1)
                return FactoryPayment.Get(data.Single());
            else
                return null;
        }

        public EntityPayment GetByOrderID(int OrderID)
        {
            var data = base.DataContext.Payment.Where(p => p.FK_OrderID == OrderID);
            if (data.Count() > 0)
                return FactoryPayment.Get(data.First());
            else
                return null;
        }
        public EntityPayment GetPolicyPayment(int OrderID, string Folio)
        {
            var data = base.DataContext.Payment.Where(p => p.FK_OrderID == OrderID && p.Folio== Folio);
            if (data.Count() > 0)
                return FactoryPayment.Get(data.First());
            else
                return null;
        }
        public EntityPayment GetPaymentByType(int OrderID, int TypeQuotation)
        {
            var data = base.DataContext.Payment.Where(p => p.FK_OrderID == OrderID && p.Fk_TypeQuotation == TypeQuotation);
            if (data.Count() > 0)
                return FactoryPayment.Get(data.First());
            else
                return null;
        }
        public List<EntityPayment> GetActives()
        {
            return FactoryPayment.GetList(base.DataContext.Payment.Where(p => p.Status == true).ToList());
        }

        public List<EntityPayment> GetAll()
        {
            return FactoryPayment.GetList(base.DataContext.Payment.ToList());
        }

        public EntityPayment Insert(EntityPayment data)
        {
            try
            {
                Payment dataNew = new Payment()
                {
                    PK_PaymentID = data.PK_PaymentID,
                    FK_OrderID = data.FK_OrderID,
                    TypePaymentID = data.TypePaymentID,
                    AuthorizationPayment = data.AuthorizationPayment,
                    DatePayment = data.DatePayment,
                    MountPayment = data.MountPayment,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate,
                    Folio=data.Folio,
                    Fk_TypeQuotation=data.Fk_TypeQuotation
                    
                };
                base.DataContext.Payment.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_PaymentID = dataNew.PK_PaymentID;

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

        public EntityPayment Update(EntityPayment data)
        {
            try
            {
                var dataUpdate = base.DataContext.Payment.Where(p => p.PK_PaymentID == data.PK_PaymentID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_PaymentID = data.PK_PaymentID;
                    //dataUpdate.FK_OrderID = data.FK_OrderID;
                    dataUpdate.TypePaymentID = data.TypePaymentID;
                    dataUpdate.AuthorizationPayment = data.AuthorizationPayment;
                    dataUpdate.DatePayment = data.DatePayment;
                    dataUpdate.MountPayment = data.MountPayment;
                    dataUpdate.Status = data.Status;
                    dataUpdate.CreateDate = data.CreateDate;
                    dataUpdate.ModifyDate = data.ModifyDate;
                    dataUpdate.Folio = data.Folio;
                    dataUpdate.Fk_TypeQuotation = data.Fk_TypeQuotation;

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
