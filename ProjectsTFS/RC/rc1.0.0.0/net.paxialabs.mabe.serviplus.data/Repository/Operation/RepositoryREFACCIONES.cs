using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
    public class RepositoryREFACCIONES : BaseRepository, IRepositoryGET<EntityREFACCIONES>, IRepositorySET<EntityREFACCIONES>
    {
        public EntityREFACCIONES Get(int Id)
        {
            var data = base.DataContext.REFACCIONES.Where(p => p.RefaccionID == Id);
            if (data.Count() == 1)
                return FactoryREFACCIONES.Get(data.Single());
            else
                return null;
        }

        public List<EntityREFACCIONES> GetActives()
        {
            return FactoryREFACCIONES.GetList(base.DataContext.REFACCIONES.Where(p => p.Procesado == true).ToList());
        }

        public List<EntityREFACCIONES> GetAll()
        {
            this.DataContext.Database.CommandTimeout = 180;
            return FactoryREFACCIONES.GetList(base.DataContext.REFACCIONES.ToList());
        }


        public List<EntityREFACCIONES> GetAllRefc(DateTime Fecha, List<string> Centro, List<string> Almacen)
        {
            this.DataContext.Database.CommandTimeout = 180;
            return FactoryREFACCIONES.GetList(base.DataContext.REFACCIONES.Where(p=>  DbFunctions.TruncateTime(p.Creacion.Value) == Fecha && Centro.Contains(p.CENTRO) && Almacen.Contains(p.ALMACEN)).ToList());
        }
        public EntityREFACCIONES Insert(EntityREFACCIONES data)
        {
            try
            {
                net.paxialabs.mabe.serviplus.data.Model.REFACCIONES  dataNew = new net.paxialabs.mabe.serviplus.data.Model.REFACCIONES ()
                {
                RefaccionID = data.RefaccionID,
                ID_REF = data.ID_REF,
                CENTRO = data.CENTRO,
                ALMACEN = data.ALMACEN,
                TOTDISP = data.TOTDISP,
                Procesado = data.Procesado,
                Creacion = data.Creacion,
                Modificacion = data.Modificacion,
                };
                base.DataContext.REFACCIONES.Add(dataNew);
                base.DataContext.SaveChanges();

                data.RefaccionID = dataNew.RefaccionID;

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

        public void BulkInsert(List<EntityREFACCIONES> data)
        {
            try
            {

                base.DataContext.BulkInsert<net.paxialabs.mabe.serviplus.data.Model.REFACCIONES>(data.Select(p => new net.paxialabs.mabe.serviplus.data.Model.REFACCIONES()
                {
                    RefaccionID = p.RefaccionID,
                    ID_REF = p.ID_REF,
                    CENTRO = p.CENTRO,
                    ALMACEN = p.ALMACEN,
                    TOTDISP = p.TOTDISP,
                    Procesado = p.Procesado,
                    Creacion = p.Creacion,
                    Modificacion = p.Modificacion
                }));
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

        public EntityREFACCIONES Update(EntityREFACCIONES data)
        {
            try
            {
                var dataUpdate = base.DataContext.REFACCIONES.Where(p => p.RefaccionID == data.RefaccionID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.ID_REF = data.ID_REF;
                    dataUpdate.CENTRO = data.CENTRO;
                    dataUpdate.ALMACEN = data.ALMACEN;
                    dataUpdate.TOTDISP = data.TOTDISP;
                    dataUpdate.Procesado = data.Procesado;
                    dataUpdate.Creacion = data.Creacion;
                    dataUpdate.Modificacion = data.Modificacion;


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
