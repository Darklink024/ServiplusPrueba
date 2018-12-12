using net.paxialabs.mabe.serviplus.data.Factory.SAP;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.SAP;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.SAP
{
    public class RepositorySAPResumen : BaseRepository
    {
        public void Insert(Resumen data)
        {
            try
            {
               
                base.DataContext.Resumen.Add(data);
                base.DataContext.SaveChanges();
                
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

        public List<EntitySAPResumen> GetAll()
        {
            return FactorySAPResumen.GetList(base.DataContext.Resumen.ToList());
        }

        public List<EntitySAPResumen> GetAll(DateTime fh)
        {
            DateTime fhIni = fh.AddDays(-3);
            DateTime fhFin = new DateTime(fh.Year, fh.Month, fh.Day, 23, 59, 59).AddHours(6);
            return FactorySAPResumen.GetList(base.DataContext.Resumen.Where( p => p.Creacion >= fhIni && p.Creacion <= fhFin ).ToList());
        }

        public List<EntitySAPResumen> GetAll(bool BI_ODS_Udp)
        {
            return FactorySAPResumen.GetList(base.DataContext.Resumen.Where(p => p.BI_ODS_Udp == BI_ODS_Udp & p.Procesado == true).ToList());
        }

        public void Update(Resumen data)
        {
            try
            {
                var dataUpdate = base.DataContext.Resumen.Where(p => p.ResumenID == data.ResumenID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.Actualizados = data.Actualizados;
                    dataUpdate.BI_ODS_Udp = data.BI_ODS_Udp;
                    dataUpdate.Contenedor = data.Contenedor;
                    dataUpdate.Fecha = data.Fecha;
                    dataUpdate.Inicio = data.Inicio;
                    dataUpdate.Insertados = data.Insertados;
                    dataUpdate.Modificacion = DateTime.UtcNow;
                    dataUpdate.Procesado = data.Procesado;
                    dataUpdate.Registros = data.Registros;
                    dataUpdate.Termino = data.Termino;
                    dataUpdate.Tipo = data.Tipo;
                    
                    base.DataContext.Entry(dataUpdate).State = EntityState.Modified;
                    base.DataContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el registro en la base de datos a modificar.");
                }
                
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

        public string GetStringConnection()
        {
            return base.DataContext.Database.Connection.DataSource;
        }

    }
}
