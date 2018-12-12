using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessQuotation
    {
        public EntityQuotation Insert(int FK_OrdenID, string SubTotal, string IVA, string Total, string Folio, string URL, int typeQuotation, int FK_EmployeeID)
        {

            var objRepository = new RepositoryQuotation();
            EntityQuotation data = new EntityQuotation()
            {
                PK_QuotationID = 0,
                FK_OrdenID = FK_OrdenID,
                SubTotal = SubTotal,
                IVA = IVA,
                Total = Total,
                Folio = Folio,
                URL = URL,
                Status = true,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                TypeQuotation= typeQuotation,
                FK_EmployeeID=FK_EmployeeID
               
                
            };
            data = objRepository.Insert(data);
            return data;

        }

        public EntityQuotation Insert2(int FK_OrdenID, string SubTotal, string IVA, string Total, string Folio, string URL, int typeQuotation, int FK_EmployeeID,DateTime date )
        {

            var objRepository = new RepositoryQuotation();
            EntityQuotation data = new EntityQuotation()
            {
                PK_QuotationID = 0,
                FK_OrdenID = FK_OrdenID,
                SubTotal = SubTotal,
                IVA = IVA,
                Total = Total,
                Folio = Folio,
                URL = URL,
                Status = true,
                CreateDate = date,
                ModifyDate = date,
                TypeQuotation = typeQuotation,
                FK_EmployeeID = FK_EmployeeID


            };
            data = objRepository.Insert(data);
            return data;

        }

        public EntityQuotation Update(EntityQuotation data)
        {
            return new RepositoryQuotation().Update(data);
        }

        public List<EntityQuotation> GetAll()
        {
            return new RepositoryQuotation().GetAll();
        }

        public EntityQuotation GetByOrder(int OrderID)
        { 
            return new RepositoryQuotation().GetByOrder(OrderID);
        }
        public EntityQuotation GetByID(int QuotationID)
        {
            return new RepositoryQuotation().GetByID(QuotationID);
        }
        public EntityQuotation GetByOrderFolio(int OrderID, string Folio)
        {
            return new RepositoryQuotation().GetByOrderFolio(OrderID, Folio);
        }
        public EntityQuotation GetByOrderType(int OrderID, int Type)
        {
            return new RepositoryQuotation().GetByOrdertype(OrderID, Type);
        }

        public List<EntityQuotation> GetByEmpoyeeDate(int UserID,DateTime date )
        {
            return new RepositoryQuotation().GetByEmpoyeeDate(UserID, date );
        }
        public List<EntityQuotation> GeListByOrder(List<int> OrderIDs)
        {
            return new RepositoryQuotation().GeListByOrder(OrderIDs).Select(p => new EntityQuotation()
            {

                PK_QuotationID = p.PK_QuotationID,
                FK_OrdenID = p.FK_OrdenID,
                SubTotal = p.SubTotal,
                Total = p.Total,
                IVA = p.Total,
                Folio = p.Folio,
                URL = p.URL,
                OrdenVenta =p.OrdenVenta,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate,
                TypeQuotation=p.TypeQuotation,
                FK_EmployeeID=p.FK_EmployeeID
            }).ToList<EntityQuotation>();
        }


        public List<ModelViewFolios> GetLastFolio(ModelViewUserG objCred) {
            var NegocioPoliza = new BusinessPolicy();
            var NegocioRef = new BusinessRefsell();
            var NegocioOrden = new BusinessOrder();
            var NegocioUsuario = new BusinessUsers();
            var NegocioEmpleado = new BusinessEmployee();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);

            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");
            

            DateTime date= objCred.Date.Value;
           

            List<ModelViewFolios> x = new List<ModelViewFolios>();
            
            var Empleado2 = NegocioEmpleado.GetEmployeUser(dataUsuario.UserID);
            var Empleado = Empleado2[0];
            
            int Conteo;
            List<EntityQuotation> Cotizaciones;
            List<EntityPolicy> Polizas;
            List<EntityRefSell> Refacciones;

            string Tipo;
            int count = 1;
            while (count<4)
                switch (count)
                {
                   
                     
                    case 1:
                         
                        Tipo = "Cotizacion";
                        Conteo = GetByEmpoyeeDate(Empleado.PK_EmployeeID, date).Count;
                        Cotizaciones = GetByEmpoyeeDate(Empleado.PK_EmployeeID, date);
                        
                       var LastFolio = Cotizaciones.Count is 0 ? "": Cotizaciones[0].Folio;
                       
                        x.Add(new ModelViewFolios(Tipo,Conteo,LastFolio));
                        count++;

                        break;
                    case 2:
                        Tipo = "Polizas";
                        Conteo = NegocioPoliza.GetByEmpoyeeDate(Empleado.PK_EmployeeID,  date).Count;
                        Polizas = NegocioPoliza.GetByEmpoyeeDate(Empleado.PK_EmployeeID, date);
                        
                        LastFolio = Polizas.Count is 0 ? "" : Polizas[0].IDPolicy;

                        x.Add(new ModelViewFolios(Tipo, Conteo,LastFolio));
                        count++;
                        break;
                    case 3:
                        Tipo = "Refacciones";
                        Conteo = NegocioRef.GetByEmpoyeeDate(Empleado.PK_EmployeeID, date).Count;
                        Refacciones = NegocioRef.GetByEmpoyeeDate(Empleado.PK_EmployeeID, date);
                        LastFolio = Refacciones.Count is 0 ? "" : Refacciones[0].IDRefSell;

                        x.Add(new ModelViewFolios(Tipo,Conteo,LastFolio));
                        count++;
                        break;
                    default:

                        count++;
                        break;
                }






            return x;

        
        }

    }
}
