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
          public class RepositoryCodeFailure : BaseRepository, IRepositoryGET<EntityCodeFailure>, IRepositorySET<EntityCodeFailure>
    {
        public EntityCodeFailure Get(int Id)
        {
            var data = base.DataContext.CodeFailure.Where(p => p.PK_CodeFailureID == Id);
            if (data.Count() == 1)
                return FactoryCodeFailure.Get(data.Single());
            else
                return null;
        }

        public List<EntityCodeFailure> GetActives()
        {
            return FactoryCodeFailure.GetList(base.DataContext.CodeFailure.Where(p => p.Status == true).ToList());
        }

        public List<EntityCodeFailure> GetAll()
        {
            return FactoryCodeFailure.GetList(base.DataContext.CodeFailure.ToList());
        }

        public EntityCodeFailure Insert(EntityCodeFailure data)
        {
            try
            {
                CodeFailure dataNew = new CodeFailure()
                {
                    PK_CodeFailureID = data.PK_CodeFailureID,
                    CodeFailure1 = data.CodeFailure1,
                    Failure = data.Failure,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.CodeFailure.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_CodeFailureID = dataNew.PK_CodeFailureID;

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

        public EntityCodeFailure Update(EntityCodeFailure data)
        {
            try
            {
                var dataUpdate = base.DataContext.CodeFailure.Where(p => p.PK_CodeFailureID == data.PK_CodeFailureID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    //dataUpdate.PK_CodeFailureID = data.PK_CodeFailureID;
                    dataUpdate.CodeFailure1 = data.CodeFailure1;
                    dataUpdate.Failure = data.Failure;
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

        public EntityCodeFailure GetByCodeFailure(string CodeFailure)
        {
            var data = base.DataContext.CodeFailure.Where(p => p.CodeFailure1 == CodeFailure);
            if (data.Count() == 1)
                return FactoryCodeFailure.Get(data.Single());
            else
                return null;
        }
    }
}
