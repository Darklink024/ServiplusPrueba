using net.paxialabs.mabe.serviplus.data.Repository.SAP;
using net.paxialabs.mabe.serviplus.domain.Business.Notification;
using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.SAP;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using Newtonsoft.Json;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace net.paxialabs.mabe.serviplus.domain.Business.Interface
{
    internal class BusinessInterface
    {
        
        List<entities.Entity.Interface.ODSCLIENTE> ODSCLIENTE = null;
        List<ODSMODELO> ODSMODELO = null;
        List<Material> Material = null;
        List<BOM> BOM = null;

        public void DownloadFromSFTP(bool removeOrigin, out List<string> arrFiles, out List<string> arrFilesOK, out string DownloadFolder)
        {
            DownloadFolder = "";
            arrFiles = new List<string>();
            arrFilesOK = new List<string>();

            var listProcesado = new RepositorySAPResumen().GetAll(DateTime.Today);

            var connectionInfo = new ConnectionInfo(GlobalConfiguration.SFTPMabeHost,
                                        GlobalConfiguration.SFTPMabeUser,
                                        new PasswordAuthenticationMethod(GlobalConfiguration.SFTPMabeUser, GlobalConfiguration.SFTPMabePwd),
                                        new PrivateKeyAuthenticationMethod(GlobalConfiguration.MabeSFTPRSA));

            string msg = "";
            msg += Environment.NewLine + "-------------------------INICIO_DescargaODS-------------------------" + Environment.NewLine;
            msg += string.Format("FECHA DE INICIO: {0}", DateTime.UtcNow) + Environment.NewLine;
                       
            using (var client = new SftpClient(connectionInfo))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    DownloadFolder = GlobalConfiguration.MabeSFTPFolderLocal; // Path.Combine(GlobalConfiguration.MabeSFTPFolderLocal, DateTime.Today.ToString("yyyyMMdd"));

                    if (!Directory.Exists(DownloadFolder)) Directory.CreateDirectory(DownloadFolder);
                    var x = client.ListDirectory(GlobalConfiguration.MabeSFTPFolderRemote).OrderByDescending(p => p.LastWriteTime);

                    foreach (SftpFile ftpfile in client.ListDirectory(GlobalConfiguration.MabeSFTPFolderRemote).Where(p => p.Name.Contains(".xml") == true).OrderByDescending(p => p.LastWriteTime))
                    {
                        if (ftpfile.Name.Length <= 2) continue;

                        if (listProcesado.Where(p => p.Contenedor.Equals(ftpfile.Name, StringComparison.OrdinalIgnoreCase)).Count() > 0) continue;

                        if(ftpfile.Name.Contains("OK"))
                        {
                            arrFilesOK.Add(ftpfile.Name.Replace("_OK", ""));
                            msg += string.Format("VALIDANDO ARCHIVO COMPLETO: {0} \t {1}", ftpfile.Name, ftpfile.Name.Replace("_OK", "")) + Environment.NewLine;
                            continue;
                        }

                        if(ftpfile.Name.Contains("CifrasControl"))
                        {
                            arrFilesOK.Add(ftpfile.Name);
                            msg += string.Format("VALIDANDO ARCHIVO DE CONTROL COMPLETO: {0} ", ftpfile.Name) + Environment.NewLine;
                            continue;
                        }
                    }

                    foreach (SftpFile ftpfile in client.ListDirectory(GlobalConfiguration.MabeSFTPFolderRemote).Where(p => p.Name.Contains(".xml")).OrderByDescending(p => p.LastWriteTime))
                    {
                        if (ftpfile.Name.Contains("OK")) continue;
                        if (arrFilesOK.Contains(ftpfile.Name))
                        {
                            string renFile = renameFile(ftpfile.Name);
                            using (FileStream fs = new FileStream(Path.Combine(DownloadFolder, renFile), FileMode.Create))
                            {
                                msg += string.Format("DESCARGANDO EN: {0}", Path.Combine(DownloadFolder, renFile)) + Environment.NewLine;
                                client.DownloadFile(ftpfile.FullName, fs);
                                arrFiles.Add(Path.Combine(DownloadFolder, renFile));
                            }
                            if (removeOrigin && ftpfile.LastWriteTime < DateTime.Today.AddMinutes(-30))
                            {
                                msg += string.Format("ELIMINANDO ORIGEN EN: {0}", ftpfile.FullName) + Environment.NewLine;
                                client.DeleteFile(ftpfile.FullName);
                                client.DeleteFile(ftpfile.FullName.Replace(".xml", "_OK.xml"));
                            }
                        }
                    }
                    
                    
                }
                client.Disconnect();
                
            }
            msg += string.Format("FECHA DE FIN: {0}", DateTime.UtcNow) + Environment.NewLine;
            msg += "-------------------------FIN_DescargaODS-------------------------" + Environment.NewLine;
            new BusinessImportODSLogger().WriteEntry(msg);


        }

        private string renameFile(string fileName)
        {
            string result = "";
            FileInfo file = new FileInfo(fileName);
            if (fileName.Contains("PT_Refacciones")) result = "01.-" + file.Name;
            if (fileName.Contains("BOM")) result = "02.-" + file.Name;
            if (fileName.Contains("Precios")) result = "03.-" + file.Name;
            if (fileName.Contains("LugarCompra")) result = "04.-" + file.Name;
            if (fileName.Contains("Garantias1")) result = "05.-" + file.Name;
            if (fileName.Contains("Garantias2")) result = "06.-" + file.Name;
            if (fileName.Contains("Usuarios")) result = "07.-" + file.Name;
            if (fileName.Contains("SerieMod")) result = "08.-" + file.Name;
            if (fileName.Contains("ValidaNumeroSerie")) result = "09.-" + file.Name;
            if (fileName.Contains("Fallas")) result = "10.-" + file.Name;
            if (fileName.Contains("Clientes")) result = "11.-" + file.Name;
            if (fileName.Contains("ODS")) result = "12.-" + file.Name;
            if (fileName.Contains("Ref_Man")) result = "13.-" + file.Name;
            if (fileName.Contains("InventarioTec")) result = "14.-" + file.Name;
            if (fileName.Contains("CifrasControl")) result = "15.-" + file.Name;
            return result;
        }

        private DateTime ParseDate(string dt)
        {
            dt = dt.Replace('.', '/').Trim();
            string[] formatStrings;

            DateTime dateValue;

            if (!dt.Contains("/"))
            {
                if (dt.Count() == 7) dt = "0" + dt;
                if (dt.Count() == 8)
                {
                    dt = dt.Substring(0, 2) + "/" + dt.Substring(2, 2) + "/" + dt.Substring(4);
                }
            }

            formatStrings = new string[] { "MM/dd/yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss" };
            if (DateTime.TryParseExact(dt, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                return dateValue;

            formatStrings = new string[] { "dd/MM/yyyy", "yyyy-MM-dd" };
            if (DateTime.TryParseExact(dt, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                return dateValue;

            return new DateTime(1900, 1, 1);
        }

        [ObsoleteAttribute("Este metodo esta obsoleto. Usa proc.", false)]
        public void ReadFiles(string directoryDown)
        {
            string msg = "";
            msg += Environment.NewLine + "-------------------------INICIO_ProcesandoODS-------------------------" + Environment.NewLine;
            msg += string.Format("FECHA DE INICIO: {0}", DateTime.UtcNow) + Environment.NewLine;

            DirectoryInfo di = new DirectoryInfo(directoryDown);

            List<string> arrNotification = new List<string>();

            foreach (var fi in di.GetFiles())
            {
                int fileExtPos = fi.Name.LastIndexOf(".");
                string nombre = fi.Name.Substring(0, fileExtPos);
                string nombrex = nombre.Contains("InventarioTec")? nombre.Split('-')[1].Substring(18) : nombre.Split('-')[1].Substring(14);
                var xml = File.ReadAllText(fi.FullName, Encoding.UTF8);
                string root = null;
                if(nombrex=="PT_Refacciones")
                {
                    xml = xml.Replace("&amp;", "Y");
                    xml = xml.Replace("&", "Y");
                }
                if (nombrex == "InventarioTec")
                {
                    xml = xml.Replace("ns1:", "");
                    xml = xml.Replace("xmlns:ns1=", "");
                    xml = xml.Replace("http://mabe.com/MW/CONFINAL/Mobile/InventarioTecnico", "");
                    xml = xml.Replace("MT_InventarioTecnico " + @"""", "MT_InventarioTecnico");
                    xml = xml.Replace("MT_InventarioTecnico" + @"""", "MT_InventarioTecnico");
                }


                string sourceFile = System.IO.Path.Combine(new DirectoryInfo(directoryDown).ToString(), fi.Name);
                string destFile = System.IO.Path.Combine(new DirectoryInfo(GlobalConfiguration.LocateFiles).ToString() + DateTime.Today.ToString("yyyyMMdd") , fi.Name);               
                string Ordenes2017 = new DirectoryInfo(GlobalConfiguration.LocateFiles).ToString() + DateTime.Today.ToString("yyyyMMdd");
                if (!(Directory.Exists(Ordenes2017)))
                {
                    Directory.CreateDirectory(Ordenes2017);
                }

                switch (nombrex)
                {
                    case "PT_Refacciones":
                        try
                        {
                            root = "Materiales";
                            Material = Metodo(xml, Material, root, sourceFile, destFile);
                            var NegocioProducto = new BusinessProduct();
                            List<Material> SintipoProd = Material.Where(p => p.TipoProducto == "" && p.GrupoMaterial1 != "" && p.GrupoMaterial1 != "999").ToList();
                            List<Material> ProRef = Material.Where(p => p.TipoProducto == "Refacciones").ToList();
                            List<Material> Producto = Material.Where(p => p.TipoProducto == "Servicios").ToList();
                            Producto.AddRange(SintipoProd);
                            var ProductosDistintos = ((from s in Producto
                                                       select new
                                                       {
                                                           s.IDMaterial,
                                                           s.CanalDistribucion,
                                                           s.Centro,
                                                           s.DescripcionRefaccion,
                                                           s.EstatusMaterial,
                                                           s.GrupoMaterial1,
                                                           s.GrupoMaterial4,
                                                           s.TipoProducto,
                                                           s.OrganizacionVentas
                                                       }).Distinct()).ToList();
                                var productosInsert = ProductosDistintos.Select(a =>
                                                 new EntityProduct()
                                                 {
                                                     PK_ProductID = 0,
                                                     Model = a.IDMaterial,
                                                     ProductName = a.DescripcionRefaccion,
                                                     SaleOrganization = a.OrganizacionVentas,
                                                     DistributionChannel = a.CanalDistribucion,
                                                     Center = a.Centro,
                                                     MaterialGroup1 = a.GrupoMaterial1,
                                                     MaterialGroup4 = a.GrupoMaterial4,
                                                     ProductType = a.TipoProducto,
                                                     BarCode = "",
                                                     Status = true,
                                                     CreateDate = DateTime.UtcNow.ToLocalTime(),
                                                     ModifyDate = DateTime.UtcNow.ToLocalTime()
                                                 }).ToList();
                                NegocioProducto.BulkMerge(productosInsert);    
                        }
                        catch (Exception ex)
                        {
                            // Write LOG
                            new BusinessImportODSLogger().WriteError(ex, "BusinessInterface - ReadFiles - PT Refacciones");
                        }
                        break;

                    case "BOM":
                        try
                        {
                            root = "BOMS";
                            BOM = Metodo(xml, BOM, root, sourceFile, destFile);
                            var NegocioBOM = new BusinessBuildOfMaterial();
                            var BOMsDistintos = ((from s in BOM
                                                  select new
                                                  {
                                                      s.MaterialPT,
                                                      s.IDRefaccion,
                                                      s.Centro,
                                                      s.Cantidad
                                                  }).Distinct()).ToList();
                            List<Material> Refaccion = Material.Where(p => p.TipoProducto == "Refacciones").ToList();
                            List<Material> SintipoRefc = Material.Where(p => p.TipoProducto == "" && p.GrupoMaterial1 == "999").ToList();
                            Refaccion.AddRange(SintipoRefc);
                            var RecfDistintos = ((from s in Refaccion
                                                  select new
                                                  {
                                                      s.IDMaterial,
                                                      s.GrupoMaterial1,
                                                      s.GrupoMaterial4,
                                                      s.CanalDistribucion,
                                                      s.Centro,
                                                      s.DescripcionRefaccion,
                                                      s.EstatusMaterial,
                                                      s.OrganizacionVentas,
                                                      s.TipoProducto
                                                  }).Distinct()).ToList();

                            var BomConsolidado = (from d in BOMsDistintos
                                                  join c in FacadeProduct.GetAll() on d.MaterialPT equals c.Model
                                                  join b in RecfDistintos on d.IDRefaccion equals b.IDMaterial
                                                  select new EntityBuildOfMaterial()
                                                  {
                                                      FK_ProductID = c.PK_ProductID,
                                                      Model = d.MaterialPT,
                                                      SparePartsID = d.IDRefaccion,
                                                      Quantity = (int)d.Cantidad,
                                                      StatusBOM = b.EstatusMaterial,
                                                      SparePartDescription = b.DescripcionRefaccion,
                                                      Status = true,
                                                      CreateDate = DateTime.UtcNow.ToLocalTime(),
                                                      ModifyDate = DateTime.UtcNow.ToLocalTime()
                                                  }).Distinct().ToList();

                            new BusinessBuildOfMaterial().BulkMerge(BomConsolidado);
                        }

                        catch (Exception ex)
                        {
                            // Write LOG
                            new BusinessImportODSLogger().WriteError(ex, "BusinessInterface - ReadFiles - BOM");
                        }
                        break;

                    case "Precios":
                        try
                        {
                            root = "Precios";
                            List<Precio> Precio = null;
                            Precio = Metodo(xml, Precio, root, sourceFile, destFile);
                            List<string> ListPrice = Precio.Select(p => p.Material).ToList();
                            var RefBom = new BusinessBuildOfMaterial().GetAllSparePart(ListPrice);
                            var NegocioPrecios = new BusinessPrice();
                            var Precios = (from c in Precio
                                           join p in RefBom on c.Material equals p.SparePartsID
                                           where c.Material == p.SparePartsID
                                           select new EntityPrice()
                                           {
                                               PK_PriceID = 0,
                                               FK_BuildOfMaterialsID = p.PK_BuildOfMaterialsID,
                                               FK_ProductID = p.FK_ProductID,
                                               TypeCondition = c.TipoCondicion,
                                               SalesOrganization = c.OrganizacionVentas,
                                               DistributionChannel = c.CanalDistribucion,
                                               ListPrice = c.ListaPrecios,
                                               GroupMaterial1 = c.GrupoMaterial1,
                                               GroupMaterial4 = c.GrupoMaterial4,
                                               Price = double.Parse(c.PrecioMaterial),
                                               Coin = c.Moneda,
                                               DateValidity = DateTime.ParseExact(c.FechaValidez,
                                           "yyyyMMdd",
                                            CultureInfo.InvariantCulture),
                                               DateValidityEnd = DateTime.ParseExact(c.FechaFinValidez,
                                           "yyyyMMdd",
                                            CultureInfo.InvariantCulture),
                                               Policy = "",
                                               Guaranty = "",
                                               Status = true,
                                               CreateDate = DateTime.UtcNow.ToLocalTime(),
                                               ModifyDate = DateTime.UtcNow.ToLocalTime()
                                           }).Distinct().ToList();
                            NegocioPrecios.BulkMerge(Precios);
                        }
                        catch (Exception ex)
                        {
                            // Write LOG
                            new BusinessImportODSLogger().WriteError(ex, "BusinessInterface - ReadFiles - Precios");
                        }
                        break;

                    case "LugarCompra":
                        root = "LUGARCOMPRA";
                        List<LugComp> LugComp = null;
                        LugComp = Metodo(xml, LugComp, root, sourceFile, destFile);
                        var NegocioLugar = new BusinessShopPlace();
                        try
                        {
                            var ShopInsert = LugComp.Select(p =>
                                              new EntityShopPlace()
                                              {
                                                  PK_ShopPlaceID = 0,
                                                  ShopPlaceID = p.IDSucursal,
                                                  ShopPlace1 = p.LugarCompra,
                                                  CountryAddress = p.ID_PAIS,
                                                  StateAddress = p.ESTADO,
                                                  CityAddress = p.ESTADO,
                                                  MunicipalityAddress = p.ESTADO,
                                                  StreetAddress = "",
                                                  ClientID = p.IDClienteDist,
                                                  Status = true,
                                                  CreateDate = DateTime.UtcNow.ToLocalTime(),
                                                  ModifyDate = DateTime.UtcNow.ToLocalTime()

                                              }).ToList();
                            NegocioLugar.BulkMerge(ShopInsert);
                        }
                        catch (Exception ex)
                        {
                            // Write LOG
                            new BusinessImportODSLogger().WriteError(ex, "BusinessInterface - ReadFiles - LugarCompra");
                        }
                        break;

                    case "Garantias1":
                        root = "Garantias_30_31";
                        List<Garantias1> Garantia1 = null;
                        var NegocioGarantiaProducto = new BusinessValidationGuarantyProduct();
                        Garantia1 = Metodo(xml, Garantia1, root, sourceFile, destFile);
                        //try
                        //{
                        //var guarantyproduct = (from a in Garantia1
                        //                           join b in FacadeProduct.GetAll() on a.IDProducto equals b.Model into ps
                        //                           from b in ps.DefaultIfEmpty()
                        //                           select new EntityValidationGuarantyProduct()
                        //                           {
                        //                               PK_ValidationGuarantyProductID = 0,
                        //                               FK_ProducID = b == null ? (int?)null : b.PK_ProductID,
                        //                               ClientID = a.Solicitante,
                        //                               Model = a.IDProducto,
                        //                               Months = a.Meses.Substring(1),
                        //                               Country = a.Pais,
                        //                               ValidFrom = ParseDate(a.ValidoDe),
                        //                               ValidTo = ParseDate(a.ValidoA),
                        //                               Status = true,
                        //                               CreateDate = DateTime.UtcNow.ToLocalTime(),
                        //                               ModifyDate = DateTime.UtcNow.ToLocalTime()
                        //                           }).ToList();
                        //    NegocioGarantiaProducto.BulkMerge(guarantyproduct);
                        //}
                        //catch (Exception ex)
                        //{
                        //    // Write LOG
                        //    new BusinessImportODSLogger().WriteError(ex, "BusinessInterface - ReadFiles - Garantias1");
                        //}
                        break;
                        
                    case "Garantias2":
                        root = "Garantias_33_34";
                        List<Garantias2> Garantias2 = null;
                        Garantias2 = Metodo(xml, Garantias2, root, sourceFile, destFile);
                        //var NegocioGarantiaBOM = new  BusinessValidationGuarantyBOM();
                        //try
                        //{
                        //    var guarantyBOM = (from a in Garantias2
                        //                       join b in FacadeBuildOfMaterial.GetAll() on new { X1 = a.IDProducto, X2 = a.IDProductoPedido } equals new { X1 = b.SparePartsID, X2 = b.Model }
                        //                       select new EntityValidationGuarantyBOM()
                        //                       {
                        //                           PK_ValidationGuarantySparePartID = 0,
                        //                           FK_ProducID = b.FK_ProductID,
                        //                           FK_BuildOfMaterialsID = b.PK_BuildOfMaterialsID,
                        //                           Model = b.Model,
                        //                           SalesOrganization = a.OrgVentas,
                        //                           SparePartsID = b.SparePartsID,
                        //                           ClientID = !String.IsNullOrEmpty(a.Solicitante.Trim()) ? a.Solicitante.Substring(2) : null,
                        //                           Months = a.Meses.Substring(1),
                        //                           ValidFrom =ParseDate(a.ValidoDe),
                        //                           ValidTo = ParseDate(a.ValidoA),
                        //                           Status = true,
                        //                           CreateDate = DateTime.UtcNow.ToLocalTime(),
                        //                           ModifyDate = DateTime.UtcNow.ToLocalTime()
                        //                       }).Distinct().ToList();
                        //    NegocioGarantiaBOM.BulkMerge(guarantyBOM);
                        //}
                        //catch (Exception ex)
                        //{
                        //    // Write LOG
                        //    new BusinessImportODSLogger().WriteError(ex, "BusinessInterface - ReadFiles - Garantias2");
                        //}
                        break;

                    case "Usuarios":
                        root = "Usuario_Estructura_Organizativa";
                        List<Empleado> Empleado = null;
                        Empleado = Metodo(xml, Empleado, root, sourceFile, destFile);
                        //var NegocioEmpleados = new BusinessEmployee();
                        //var NegocioUsuario = new BusinessUsers();
                        //foreach (var i in Empleado)
                        //{
                        //    if (!string.IsNullOrEmpty(i.Correo) && !string.IsNullOrEmpty(i.NIVEL3))
                        //    {
                        //        var employee = NegocioEmpleados.GetEmployeeID(i.ID_Empleado);
                        //        if (employee == null)
                        //        {
                        //            var user = NegocioUsuario.GetUserByEmail(i.Correo);
                        //            if (user == null)
                        //            {
                        //                var userIns = new ModelViewUser();
                        //                userIns.UserID = 0;
                        //                userIns.ProfileID = 4;
                        //                userIns.UserName = "tec" + i.ID_Empleado.Substring(i.ID_Empleado.Length - 4);
                        //                userIns.Name = i.Nombre + " " + i.Apellido;
                        //                userIns.Email = i.Correo;
                        //                var UserInsert = NegocioUsuario.Insert(userIns);
                        //                var EmployeeIns = new EntityEmployee();
                        //                EmployeeIns.PK_EmployeeID = 0;
                        //                EmployeeIns.FK_ModuleID = new BusinessModuleService().GetAll().Where(p => p.Module.Contains(i.NIVEL2) && p.Base.Contains(i.NIVEL3)).First().ModuleID;
                        //                EmployeeIns.FirstName = i.Nombre;
                        //                EmployeeIns.LastName = i.Apellido;
                        //                EmployeeIns.FK_UserID = UserInsert.UserID;
                        //                EmployeeIns.EmployeeID = i.ID_Empleado;
                        //                EmployeeIns.Interlocutor = i.Interlocutor;
                        //                EmployeeIns.Society = i.Sociedad;
                        //                EmployeeIns.EmployeeType = i.TipoEmpleado;
                        //                EmployeeIns.StoreProp = i.AlmacenProp;
                        //                EmployeeIns.DifferentiatorWorkshop = i.DiferenciadorTaller;
                        //                EmployeeIns.Status = true;
                        //                EmployeeIns.CreateDate = DateTime.Now;
                        //                EmployeeIns.ModifyDate = DateTime.UtcNow;
                        //                NegocioEmpleados.Insert(EmployeeIns);
                        //            }
                        //            else
                        //            {
                        //                var EmployeeIns = new EntityEmployee();
                        //                EmployeeIns.PK_EmployeeID = 0;
                        //                EmployeeIns.FK_ModuleID = new BusinessModuleService().GetAll().Where(p => p.Module.Contains(i.NIVEL2) && p.Base.Contains(i.NIVEL3)).First().ModuleID;
                        //                EmployeeIns.FirstName = i.Nombre;
                        //                EmployeeIns.LastName = i.Apellido;
                        //                EmployeeIns.FK_UserID = user.UserID;
                        //                EmployeeIns.EmployeeID = i.ID_Empleado;
                        //                EmployeeIns.Interlocutor = i.Interlocutor;
                        //                EmployeeIns.Society = i.Sociedad;
                        //                EmployeeIns.EmployeeType = i.TipoEmpleado;
                        //                EmployeeIns.StoreProp = i.AlmacenProp;
                        //                EmployeeIns.DifferentiatorWorkshop = i.DiferenciadorTaller;
                        //                EmployeeIns.Status = true;
                        //                EmployeeIns.CreateDate = DateTime.Now;
                        //                EmployeeIns.ModifyDate = DateTime.UtcNow;
                        //                NegocioEmpleados.Insert(EmployeeIns);

                        //            }
                        //        }
                        //        else
                        //        {
                        //            var userUpdate = NegocioUsuario.Get(employee.FK_UserID.Value);
                        //            employee.FK_ModuleID = new BusinessModuleService().GetAll().Where(p => p.Module.Contains(i.NIVEL2) && p.Base.Contains(i.NIVEL3)).First().ModuleID;
                        //            employee.FirstName = i.Nombre;
                        //            employee.LastName = i.Apellido;
                        //            employee.FK_UserID = userUpdate.UserID;
                        //            employee.EmployeeID = i.ID_Empleado;
                        //            employee.Interlocutor = i.Interlocutor;
                        //            employee.Society = i.Sociedad;
                        //            employee.EmployeeType = i.TipoEmpleado;
                        //            employee.StoreProp = i.AlmacenProp;
                        //            employee.DifferentiatorWorkshop = i.DiferenciadorTaller;
                        //            employee.ModifyDate = DateTime.UtcNow;
                        //            NegocioEmpleados.Update(employee);
                        //        }
                        //    }
                        //}
                        break;

                    case "SerieMod":
                        root = "NumeroSerie_VS_Modelos";
                        List<SerieMod> Serie = null;
                        Serie = Metodo(xml, Serie, root, sourceFile, destFile);
                        //List<SerieMod> SerieClean = Serie.Where(p => !string.IsNullOrEmpty(p.ValFecha) && !string.IsNullOrWhiteSpace(p.ValFecha)).ToList();
                        //var NegocioProductoSerie = new BusinessProduct();
                        //var NegocioValidaSerie = new BusinessModelSerialNumber();
                        //var Serieproduct = (from a in SerieClean
                        //                    join b in NegocioProductoSerie.GetAll() on a.IDProducto equals b.Model
                        //                    select new EntityModelSerialNumber()
                        //                    {
                        //                        PK_ModelSerialNumberID = 0,
                        //                        FK_ProducID = b.PK_ProductID,
                        //                        Model = a.IDProducto,
                        //                        ValidationFormatID = a.IDValidacion,
                        //                        InitialDate = ParseDate(a.FechaInicio),
                        //                        EndDate = ParseDate(a.FechaFin),
                        //                        ValidDate = a.ValFecha.Contains("1") ? true : false,
                        //                        Status = true,
                        //                        CreateDate = DateTime.UtcNow.ToLocalTime(),
                        //                        ModifyDate = DateTime.UtcNow.ToLocalTime()
                        //                    }).ToList();
                        //NegocioValidaSerie.BulkMerge(Serieproduct);
                        break;

                    case "ValidaNumeroSerie":
                        root = "ValidaNumeroSerie";
                        List<IDVal> ValidaSerie = null;
                        ValidaSerie = Metodo(xml, ValidaSerie, root, sourceFile, destFile);
                        //var NegocioNumValidaSerie = new BusinessModelSerialNumber();
                        //var NegocioValida = new BusinessValidationsSerialNumber();
                        //var SerieValidacion = (from a in ValidaSerie
                        //                       join b in NegocioNumValidaSerie.GetAll() on a.ID_VAL equals b.ValidationFormatID
                        //                       select new EntityValidationSerialNumber()
                        //                       {
                        //                           PK_ValidationsSerialNumberID = 0,
                        //                           FK_ModelSerialNumberID = b.PK_ModelSerialNumberID,
                        //                           ValidationFormatID = a.ID_VAL,
                        //                           ValidationName = a.NOM_VAL,
                        //                           InitialPosition = a.POSICION_INI,
                        //                           FinalPosition = a.POSICION_FIN,
                        //                           Allowed = a.VALORES,
                        //                           RankID = "",
                        //                           Status = true,
                        //                           CreateDate = DateTime.UtcNow.ToLocalTime(),
                        //                           ModifyDate = DateTime.UtcNow.ToLocalTime()
                        //                    }).Distinct().ToList();
                        //NegocioValida.BulkMerge(SerieValidacion);
                        break;

                    case "Fallas":
                        root = "FALLAS";
                        ODSMODELO = Metodo(xml, ODSMODELO, root, sourceFile, destFile);
                        var NegocioFalla = new BusinessCodeFailure();
                        var NegocioFallaProducto = new BusinessCodeFailureByProduct();
                        var modelo = (from a in ODSMODELO
                                      select a.Modelo).Distinct();
                        foreach (var item in modelo)
                        {
                            var prod = FacadeProduct.GetByModel(item);
                            var i = ODSMODELO.Where(p => p.Modelo == item).First();
                            if (prod != null)
                            {
                                var fallas = (from b in NegocioFallaProducto.GetByProductID(prod.PK_ProductID)
                                              join a in NegocioFalla.GetAll() on b.FK_CodeFailureID equals a.PK_CodeFailureID
                                              select new { IDFalla = a.PK_CodeFailureID, CodeFailure1 = a.CodeFailure1, IDProducto = b.FK_ProductID, Status = a.Status, CreateDate = a.CreateDate }).ToList();
                                if (fallas.Count() == i.Fail.Count())
                                {
                                    try
                                    {
                                        var UpdateFallas = (from a in fallas
                                                            join b in i.Fail on a.CodeFailure1 equals b.IDFalla
                                                            select new EntityCodeFailureByProduct
                                                            {
                                                                FK_CodeFailureID = a.IDFalla,
                                                                FK_ProductID = a.IDProducto,
                                                                Complexity = string.IsNullOrEmpty(b.Complejidad.Trim()) ? null : (int?)Convert.ToInt32(b.Complejidad),
                                                                Status = a.Status,
                                                                CreateDate = a.CreateDate,
                                                                ModifyDate = DateTime.UtcNow
                                                            }).ToList();
                                        NegocioFallaProducto.BulkUpdate(UpdateFallas);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Write LOG
                                        new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - Fallas 1");
                                    }
                                }
                                else if (fallas.Count() < i.Fail.Count())
                                {
                                    try
                                    {
                                        var UpdateFallas = (from a in fallas
                                                            join b in i.Fail on a.CodeFailure1 equals b.IDFalla
                                                            select new EntityCodeFailureByProduct
                                                            {
                                                                FK_CodeFailureID = a.IDFalla,
                                                                FK_ProductID = a.IDProducto,
                                                                Complexity = string.IsNullOrEmpty(b.Complejidad) ? null : (int?)Convert.ToInt32(b.Complejidad),
                                                                Status = a.Status,
                                                                CreateDate = a.CreateDate,
                                                                ModifyDate = DateTime.UtcNow
                                                            }).ToList();
                                        NegocioFallaProducto.BulkUpdate(UpdateFallas);

                                    }
                                    catch (Exception ex)
                                    {
                                        // Write LOG
                                        new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - Fallas 2 Update");
                                    }
                                    try
                                    { 
                                        var Insert = (from a in i.Fail
                                                      where !fallas.Select(p => p.CodeFailure1).ToList().Contains(a.IDFalla)
                                                      select new
                                                      {
                                                          ID = a.IDFalla,
                                                          Description = a.DescripcionFalla,
                                                          Complejidad = a.Complejidad
                                                      }).ToList();

                                        var InsertFallas = (from a in Insert
                                                            join b in NegocioFalla.GetAll() on a.ID equals b.CodeFailure1
                                                            select new EntityCodeFailureByProduct
                                                            {
                                                                FK_CodeFailureID = b.PK_CodeFailureID,
                                                                FK_ProductID = prod.PK_ProductID,
                                                                Complexity = string.IsNullOrEmpty(a.Complejidad.Trim()) ? null : (int?)Convert.ToInt32(a.Complejidad),
                                                                Status = true,
                                                                CreateDate = DateTime.UtcNow,
                                                                ModifyDate = DateTime.UtcNow
                                                            }).ToList();
                                        NegocioFallaProducto.BulkInsert(InsertFallas);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Write LOG
                                        new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - Fallas 2 Insert");
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        var UpdateFallas = (from a in fallas
                                                            join b in i.Fail on a.CodeFailure1 equals b.IDFalla
                                                            select new EntityCodeFailureByProduct
                                                            {
                                                                FK_CodeFailureID = a.IDFalla,
                                                                FK_ProductID = a.IDProducto,
                                                                Complexity = string.IsNullOrEmpty(b.Complejidad) ? null : (int?)Convert.ToInt32(b.Complejidad),
                                                                Status = a.Status,
                                                                CreateDate = a.CreateDate,
                                                                ModifyDate = DateTime.UtcNow
                                                            }).ToList();

                                        NegocioFallaProducto.BulkUpdate(UpdateFallas);
                                    }

                                    catch (Exception ex)
                                    {
                                        // Write LOG
                                        new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - Fallas 3");
                                    }
                                }
                            }
                        }
                        break;

                    case "Clientes":
                        root = "CLIENTES";
                        ODSCLIENTE = Metodo(xml, ODSCLIENTE, root, sourceFile, destFile);
                        var ODSCLIENTEDistinct = (from a in ODSCLIENTE
                                                  select a.IDCliente).Distinct();
                        foreach (var x in ODSCLIENTEDistinct)
                        {
                            EntityClient cliente = FacadeClient.GetByClientID(x);
                            var i = ODSCLIENTE.Where(p => p.IDCliente == x).First();
                            try
                            {

                                if (cliente != null)
                                {
                                    FacadeClient.Update(i, cliente);
                                }
                                else
                                {
                                    FacadeClient.Insert(i);
                                }
                            }
                            catch (Exception ex)
                            {
                                new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - Clientes - " + i.IDCliente);
                            }

                            List<srHistoryBase.DT_BaseInstalada_InItem> dataBase = new List<srHistoryBase.DT_BaseInstalada_InItem>();
                            try
                            {
                                cliente = FacadeClient.GetByClientID(i.IDCliente);
                                dataBase = new BusinessMabe().HistoryBase(i.IDCliente);
                            }

                            catch (Exception ex)
                            {
                                new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - Clientes - " + i.IDCliente);
                            }

                            //insertar datos base instalada
                            foreach (var item in dataBase)
                            {
                                try
                                {
                                    var dataBI = new BusinessInstalledBase().GetByInstalledBase(item.ID_de_Registro);
                                    var dataProduct = new BusinessProduct().GetByModel(item.ID_de_Producto);
                                    var dataShopPlace = new BusinessShopPlace().GetByShopPlace(item.Lugar_Compra);
                                    int? ShopPlaceID = null;
                                    if (dataShopPlace != null) ShopPlaceID = dataShopPlace.PK_ShopPlaceID;

                                    DateTime? ShopDate = null;
                                    try
                                    {
                                        ShopDate = ParseDate(item.Fecha_Compra);
                                    }
                                    catch { }

                                    item.No_Serie = string.IsNullOrEmpty(item.No_Serie) ? "" : item.No_Serie;

                                    if (dataBI == null)
                                    {
                                        new BusinessInstalledBase().Insert(cliente.PK_ClientID, dataProduct.PK_ProductID, ShopPlaceID, item.ID_de_Registro, item.No_Serie, item.Fecha_Compra, null, null);
                                    }
                                    else
                                    {
                                        dataBI.FK_ClientID = cliente.PK_ClientID;
                                        dataBI.FK_ProductID = dataProduct.PK_ProductID;
                                        dataBI.FK_ShopPlaceID = ShopPlaceID;
                                        dataBI.InstalledBaseID = item.ID_de_Registro;
                                        dataBI.SerialNumber = item.No_Serie;
                                        dataBI.ShopDate = ShopDate;
                                        new BusinessInstalledBase().Update(dataBI);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - Base Instalada - IDCliente (" + i.IDCliente + ") Base (" + item.ID_de_Registro + ") Producto (" + item.ID_de_Producto + ") Lugar de compra (" + item.Lugar_Compra + ") ");
                                }

                            }

                        }
                        break;


                    case "ODS":
                        root = "ORDENES";
                        List<ODS> ODS = null;
                        ODS = Metodo(xml, ODS, root, sourceFile, destFile);
                        var orden = new EntityOrder();
                        var clientes = new EntityClient();
                        var NegocioEmpleado = new BusinessEmployee();
                        foreach (var i in ODS)
                        {
                            var z = ODSCLIENTE.Where(p => p.IDOrden == i.IDOrden).FirstOrDefault();
                            var address = new LatLon();
                            if (z!= null)
                            {
                                clientes = FacadeClient.GetByClientID(z.IDCliente);
                                try
                                {
                                 address = cargarLatitudLatitud(clientes.StreetAddress + " " + clientes.NumberExternalAddress + " "
                                + clientes.BoroughAddress + " " + clientes.MunicipalityAddress + " " + clientes.PostalCodeAddress);
                                    if (address.Latitude == null && address.Longitude == null)
                                    {
                                        address = cargarLatitudLatitud(clientes.BoroughAddress + " " + clientes.MunicipalityAddress + " " + clientes.PostalCodeAddress);
                                    }
                                    if (address.Latitude == null && address.Longitude == null)
                                    {
                                        address = cargarLatitudLatitud(clientes.MunicipalityAddress + " " + clientes.PostalCodeAddress);
                                    }
                                    if (address.Latitude == null && address.Longitude == null)
                                    {
                                        address = cargarLatitudLatitud(clientes.MunicipalityAddress);
                                    }
                                }
                                catch
                                { }

                                var prod = FacadeProduct.GetByModel(i.Modelo);
                                var lugcompra = FacadeShopPlace.GetByShopPlace(i.IDLugarCompra);
                                int? LCompra = (lugcompra != null) ? (int?) lugcompra.PK_ShopPlaceID : (int?) null;
                                var ord = FacadeOrder.GetByOrderID(i.IDOrden);
                                if (ord != null)
                                {
                                        var garantia = FacadeGuaranty.GetByGuarantyID(i.TipoServicio);
                                    //var empleado = NegocioEmpleado.GetAll().Where(p => p.EmployeeID == i.IDTecnico).FirstOrDefault();
                                    var empleado = NegocioEmpleado.GetEmployeeID(i.IDTecnico);
                                    ord.FK_EmployeeID = empleado != null ? empleado.PK_EmployeeID : ord.FK_EmployeeID;
                                        ord.FK_ModuleID = empleado != null ? empleado.FK_ModuleID : ord.FK_ModuleID;
                                        ord.FK_GuarantyID = garantia.PK_GuarantyID;
                                        ord.FK_StatusSchemeID = 44;
                                        ord.SendCRM = "Pendiente";
                                        ord.Failure1 = null;
                                        ord.Failure2 = null;
                                        ord.Failure3 = null;
                                        ord.Failure4 = null;
                                        ord.Failure5 = null;
                                        var order = FacadeOrder.Update(i, ord);
                                    // se pasa al consumo del ws
                                    //var visitas = FacadeHistory.GetByOrderID(ord.OrderID);
                                    //FacadeHistory.Update(visitas, i.FechaCompra, i.TipoServicio, i.SintomaFalla);
                                    try
                                    {
                                        //var visita = new BusinessVisit().GetAll().Where(p => p.FK_OrderID == ord.PK_OrderID).FirstOrDefault();
                                        var visita = new BusinessVisit().GetByOrderID(ord.PK_OrderID);
                                        visita.LatitudeAddress = Convert.ToSingle(address.Latitude);
                                        visita.LogitudeAddress = Convert.ToSingle(address.Longitude);
                                        visita.FK_StatusVisitID = 1;
                                        visita.FK_StatusOrderID = 1;
                                        visita.FK_CauseOrderID = null;
                                        visita.StartVisitDate = null;
                                        visita.EndVisitDate = null;
                                        visita.StartOrderDate = null;
                                        visita.EndOrderDate = null;
                                        visita.StartServiceDate = null;
                                        visita.EndServiceDate = null;
                                        visita.LatitudeStartVisit = null;
                                        visita.LogitudeStartVisit = null;
                                        visita.LatitudeEndVisit = null;
                                        visita.LogitudeEndVisit = null;
                                        visita.LatitudeStartOrder = null;
                                        visita.LogitudeStartOrder = null;
                                        visita.LatitudeEndOrder = null;
                                        visita.LogitudeEndOrder = null;
                                        visita.DurationVisit = null;
                                        visita.DurationOrder = null;
                                        visita.DurationExecute = null;
                                        visita.DurationTransport = null;
                                        visita.NoteOrder = "";
                                        visita.NoteVisit = "";
                                        visita.ModifyDate = DateTime.UtcNow.ToLocalTime();
                                        FacadeVisit.Update(visita);
                                    }
                                    catch { }
                                   
                                }
                                else
                                {
                                    try
                                    {
                                        var garantia = FacadeGuaranty.GetByGuarantyID(i.TipoServicio);
                                        DateTime creacion = ParseDate(i.FechaCreacionODS);
                                        DateTime ejecucion = ParseDate(i.FechaProgramacion);
                                        //var empleado = NegocioEmpleado.GetAll().Where(p => p.EmployeeID == i.IDTecnico).FirstOrDefault();
                                        var empleado = NegocioEmpleado.GetEmployeeID(i.IDTecnico);
                                        if (empleado != null && garantia != null)
                                        {
                                            var baseInt = new BusinessInstalledBase().GetByInstalledBase(i.IDBaseInstalada);
                                            if(baseInt==null)
                                            {
                                                if(prod == null)
                                                {
                                                    baseInt = FacadeInstalledBase.Insert(clientes.PK_ClientID, null, LCompra, i.IDBaseInstalada, i.NumeroSerie, i.FechaCompra, i.Modelo, i.DescripcionModelo);
                                                }
                                                else
                                                {
                                                    baseInt = FacadeInstalledBase.Insert(clientes.PK_ClientID, prod.PK_ProductID, LCompra, i.IDBaseInstalada, i.NumeroSerie, i.FechaCompra, null, null);
                                                }
                                            }
                                                 //lugcompra.CreateDate);
                                                var order = FacadeOrder.Insert(i, clientes.PK_ClientID, baseInt.PK_InstalledBaseID, garantia.PK_GuarantyID);
                                                // se pasa al consumo del ws
                                                //FacadeHistory.Insert(baseInt.PK_InstalledBaseID, clientes.PK_ClientID, order.PK_OrderID, order.OrderID, i.FechaCompra, i.TipoServicio, i.SintomaFalla);
                                                FacadeVisit.Insert(order.PK_OrderID, 1, 1, address.Latitude, address.Longitude);
                                        }
                                        else
                                        {
                                            if (empleado == null) new BusinessImportODSLogger().WriteEntry(string.Format("ID Técnico {0} no existe. No se cargo ID Orden {1}", i.IDTecnico, i.IDOrden));
                                            //if (prod == null) new BusinessImportODSLogger().WriteEntry(string.Format("Producto {0} no existe. No se cargo ID Orden {1}", i.Modelo, i.IDOrden));
                                            if (garantia == null) new BusinessImportODSLogger().WriteEntry(string.Format("Garantia {0} no existe. No se cargo ID Orden {1}", i.TipoServicio, i.IDOrden));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // Write LOG
                                        new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - ODS");
                                    }
                                }

                                try
                                {
                                    //var dataODS = new BusinessMabe().HistoryODS(item.ID_de_Registro); // por base instalada
                                    var dataODS = FacadeOrder.GetByOrderID(i.IDOrden);
                                    var dataBaseInstalled = FacadeInstalledBase.GetByID(dataODS.FK_InstalledBaseID);
                                    var dataHistory = new BusinessMabe().HistoryODSByClient(z.IDCliente, dataBaseInstalled.InstalledBaseID); // por cliente

                                    // insertar datos order history
                                    foreach (var item in dataHistory)
                                    {

                                        var dataDBHistory = new BusinessHistory().GetByOrderID(dataODS.PK_OrderID);

                                        if (dataDBHistory.Where(p => p.OrderID == item.ID_Oper).Count() > 0)
                                        {
                                            // update
                                            var entity = dataDBHistory.Where(p => p.OrderID == item.ID_Oper).FirstOrDefault();
                                            entity.CloseDate = ParseDate(item.Fecha_Cierre_Orden);
                                            entity.Failure1 = string.IsNullOrEmpty(item.Desc_ID_Falla1) ? "" : item.Desc_ID_Falla1;
                                            entity.Failure2 = string.IsNullOrEmpty(item.Desc_ID_Falla2) ? "" : item.Desc_ID_Falla2;
                                            entity.Failure3 = string.IsNullOrEmpty(item.Desc_ID_Falla3) ? "" : item.Desc_ID_Falla3;
                                            entity.FailureID1 = string.IsNullOrEmpty(item.ID_Falla1) ? "" : item.ID_Falla1;
                                            entity.FailureID2 = string.IsNullOrEmpty(item.ID_Falla2) ? "" : item.ID_Falla2;
                                            entity.FailureID3 = string.IsNullOrEmpty(item.ID_Falla3) ? "" : item.ID_Falla3;
                                            entity.FK_ClientID = dataODS.FK_ClientID;
                                            entity.FK_InstalledBaseID = dataODS.FK_InstalledBaseID;
                                            entity.FK_OrderID = dataODS.PK_OrderID;
                                            entity.Guaranty = string.IsNullOrEmpty(item.Tipo_Serv) ? "" : item.Tipo_Serv;
                                            entity.ItemStatus = string.IsNullOrEmpty(item.Estatus_Visita) ? "" : item.Estatus_Visita;
                                            entity.ModifyDate = DateTime.UtcNow;
                                            entity.OrderID = string.IsNullOrEmpty(item.ID_Oper) ? "" : item.ID_Oper;
                                            entity.OrderStatus = string.IsNullOrEmpty(item.Estatus_Oper) ? "" : item.Estatus_Oper;
                                            entity.ShopDate = new DateTime(1980, 1, 1);
                                            entity.Status = true;
                                            new BusinessHistory().Update(entity);
                                        }
                                        else
                                        {
                                            // insert 
                                            var entity = new EntityHistory();
                                            entity.CloseDate = ParseDate(item.Fecha_Cierre_Orden);
                                            entity.CreateDate = DateTime.UtcNow;
                                            entity.Failure1 = string.IsNullOrEmpty(item.Desc_ID_Falla1) ? "" : item.Desc_ID_Falla1;
                                            entity.Failure2 = string.IsNullOrEmpty(item.Desc_ID_Falla2) ? "" : item.Desc_ID_Falla2;
                                            entity.Failure3 = string.IsNullOrEmpty(item.Desc_ID_Falla3) ? "" : item.Desc_ID_Falla3;
                                            entity.FailureID1 = string.IsNullOrEmpty(item.ID_Falla1) ? "" : item.ID_Falla1;
                                            entity.FailureID2 = string.IsNullOrEmpty(item.ID_Falla2) ? "" : item.ID_Falla2;
                                            entity.FailureID3 = string.IsNullOrEmpty(item.ID_Falla3) ? "" : item.ID_Falla3;
                                            entity.FK_ClientID = dataODS.FK_ClientID;
                                            entity.FK_InstalledBaseID = dataODS.FK_InstalledBaseID;
                                            entity.FK_OrderID = dataODS.PK_OrderID;
                                            entity.Guaranty = string.IsNullOrEmpty(item.Tipo_Serv) ? "" : item.Tipo_Serv;
                                            entity.ItemStatus = string.IsNullOrEmpty(item.Estatus_Visita) ? "" : item.Estatus_Visita;
                                            entity.ModifyDate = DateTime.UtcNow;
                                            entity.OrderID = string.IsNullOrEmpty(item.ID_Oper) ? "" : item.ID_Oper;
                                            entity.OrderStatus = string.IsNullOrEmpty(item.Estatus_Oper) ? "" : item.Estatus_Oper;
                                            entity.PK_HistoryID = 0;
                                            entity.ShopDate = new DateTime(1980, 1, 1);
                                            entity.Status = true;
                                            new BusinessHistory().Insert(entity);
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - ODS - History - ODS " + z.IDOrden);
                                }

                                arrNotification.Add(i.IDOrden);
                            }
                            
                        }

                        if (arrNotification.Count > 0)
                        {
                            List<ModelViewLoadODSResume> data = new List<ModelViewLoadODSResume>();
                            foreach (var item in arrNotification)
                            {
                                try
                                {
                                    var dataODS = new BusinessOrder().GetByOrderID(item);
                                    if (dataODS != null)
                                    {
                                        var dataEmpleado = new BusinessEmployee().GetID(dataODS.FK_EmployeeID.Value);
                                        data.Add(new ModelViewLoadODSResume() { UserID = dataEmpleado.FK_UserID.Value, OrderID = item, Estatus = dataODS.FK_StatusSchemeID.ToString() });
                                    }
                                }
                                catch
                                { }

                            }

                            foreach (var item in data.Select(p => p.UserID).Distinct())
                            {
                                // Notificar usuario
                                try
                                {
                                    string title = "Actualización ODS";
                                    string body = "";

                                    int numODS = data.Where(p => p.UserID == item).Count();
                                    if (numODS > 1)
                                        body = "Tienes " + numODS.ToString() + " nuevas ordenes de servicio, favor sincronizar para actualizar tu ruta. En caso de estar atendiendo una orden, sincronizar al cierre.";
                                    else
                                        body = "Tienes una nueva orden de servicio, favor sincronizar para actualizar tu ruta. En caso de estar atendiendo una orden, sincronizar al cierre.";

                                    List<int> usuarios = new List<int>();
                                    usuarios.Add(item);

                                    new BusinessNotification().Insert(new ModelViewNotification()
                                    {
                                        CreateDate = DateTime.UtcNow,
                                        Message = body,
                                        MessageID = 0,
                                        MessageRead = false,
                                        ModifyDate = DateTime.UtcNow,
                                        Status = true,
                                        Title = title,
                                        Url = "",
                                        UserID = item
                                    });
                                    new BusinessNotification().SendPush(usuarios, title, body);
                                }
                                catch { }

                            }
                        }

                        break;

                    case "Ref_Man":
                        root = "REFMAN";
                        List<entities.Entity.Interface.ODSREFMAN> ODSREFMAN = null;
                        ODSREFMAN = Metodo(xml, ODSREFMAN, root, sourceFile, destFile);
                        foreach (var i in ODSREFMAN)
                        {
                            var order = FacadeOrder.GetByOrderID(i.ID_ORDEN);
                            if (order != null)
                            {
                                try
                                {
                                    var dataSparePart = new BusinessSparePart().GetByOrder(order.OrderID);
                                    if (dataSparePart != null)
                                    { new BusinessSparePart().Delete(dataSparePart); }
                                }
                                catch
                                { }                               
                                foreach (var r in i.REFMANO)
                                {
                                    try
                                    {
                                        Int64 x = 0;
                                        bool result = Int64.TryParse(r.ID_RefMan, out x);
                                        var Bom = new EntityBuildOfMaterial();
                                        var workforce = new EntityWorkforce();
                                        if (!result)
                                        {
                                            var baseIns = new BusinessInstalledBase().GetByID(order.FK_InstalledBaseID);
                                            var producto = new BusinessProduct().GetByID(baseIns.FK_ProductID.Value);
                                            Bom = FacadeBuildOfMaterial.GetMaterialBySparePart(r.ID_RefMan, producto.PK_ProductID);
                                           
                                         if(Bom != null)
                                         {
                                                var refa = FacadeSparePart.GetAll().Where(p => p.FK_OrderID == order.PK_OrderID && p.FK_BuildOfMaterialsID == Bom.PK_BuildOfMaterialsID).FirstOrDefault();
                                                if (refa != null)
                                                { FacadeSparePart.Update(refa, r.Cantidad, r.PosicionItem, r.EstatusRefMan, r.EstatusEsq, r.DescripcionRefMan, r.ID_RefMan, r.Precio); }
                                                else
                                                { FacadeSparePart.Insert(order.PK_OrderID, Bom.PK_BuildOfMaterialsID, Bom.FK_ProductID, null, r.Cantidad, r.PosicionItem, r.EstatusRefMan, r.EstatusEsq, r.DescripcionRefMan,r.ID_RefMan, r.Precio); }
                                         }
                                         else
                                         {
                                                FacadeSparePart.Insert(order.PK_OrderID, null, null, null, r.Cantidad, r.PosicionItem, r.EstatusRefMan, r.EstatusEsq, r.DescripcionRefMan, r.ID_RefMan, r.Precio);
                                         }
                                        }
                                        else
                                        {
                                            workforce = new BusinessWorkforce().GetAll().Where(p => p.WorkforceID == r.ID_RefMan).FirstOrDefault();
                                            if (workforce != null)
                                            {
                                                if (workforce.PK_WorkforceID != 0)
                                                {
                                                    var manoobra = FacadeSparePart.GetAll().Where(p => p.FK_OrderID == order.PK_OrderID && p.FK_WorkforceID == workforce.PK_WorkforceID).FirstOrDefault();
                                                    if (manoobra != null)
                                                    { FacadeSparePart.Update(manoobra, r.Cantidad, r.PosicionItem, r.EstatusRefMan, r.EstatusEsq, r.DescripcionRefMan, r.ID_RefMan, r.Precio); }
                                                    else
                                                    { FacadeSparePart.Insert(order.PK_OrderID, null, null, workforce.PK_WorkforceID, r.Cantidad, r.PosicionItem, r.EstatusRefMan, r.EstatusEsq, r.DescripcionRefMan, r.ID_RefMan, r.Precio); }
                                                }
                                            }
                                        }
                                    }

                                    catch (Exception ex)
                                    {
                                        // Write LOG
                                        new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - Ref_Man");
                                    }
                                }
                            }
                        }
                        break;

                    case "InventarioTec":
                        root = "MT_InventarioTecnico";
                        List<Inventario> REFC = null;
                        REFC = Metodo(xml, REFC, root, sourceFile, destFile);
                        var NegocioRefc = new BusinessREFACCIONES();
                        var refacciones  = (from a in REFC
                                            select a.Items).FirstOrDefault();
                        //var items = REFC.Select(p => p.Items).AsQueryable();
                        //List<EntityREFACCIONES> InsertRefc = new List<EntityREFACCIONES>();
                        //EntityREFACCIONES refac = new EntityREFACCIONES();
                        //foreach (var a in refacciones)
                        //{
                        //    refac.RefaccionID = 0;
                        //    refac.ID_REF = a.IdRef;
                        //    refac.CENTRO = a.Centro;
                        //    refac.ALMACEN = a.Almacen;
                        //    refac.TOTDISP = (int)a.TotDisp;
                        //    refac.Procesado = true;
                        //    refac.Creacion = DateTime.UtcNow.ToLocalTime();
                        //    refac.Modificacion = DateTime.UtcNow.ToLocalTime();

                        //    InsertRefc.Add(refac);
                        //}
                        var InsertRefc = refacciones.Select(a =>
                                 new EntityREFACCIONES()
                                 {
                                     RefaccionID = 0,
                                     ID_REF = a.IdRef,
                                     CENTRO = a.Centro,
                                     ALMACEN = a.Almacen,
                                     TOTDISP = (int)a.TotDisp,
                                     Procesado = true,
                                     Creacion = DateTime.UtcNow.ToLocalTime(),
                                     Modificacion = DateTime.UtcNow.ToLocalTime()
                                 }).ToList();
                        NegocioRefc.BulkInsert(InsertRefc);
                        break;
                }
            }
            //try
            //{
            //    if (Directory.GetDirectories(directoryDown).Length == 0) Directory.Delete(directoryDown);
            //}
            //catch (Exception ex)
            //{
            //    // Write LOG
            //    new BusinessImportODSLogger().WriteError(ex, "BusinessInterfaca - ReadFiles - Directory.Delete");
            //}

            msg += string.Format("FECHA DE FIN: {0}", DateTime.UtcNow) + Environment.NewLine;
            msg += "-------------------------FIN_ProcesandoODS-------------------------" + Environment.NewLine;
            new BusinessImportODSLogger().WriteEntry(msg);
        }

        [ObsoleteAttribute("Este metodo esta obsoleto. Usa ImportFiles.", false)]
        public List<T> Metodo<T>(string xml, List<T> list, string root, string sourceFile, string destFile)
        {

            var serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(root));
            using (var stringReader = new StringReader(xml))
            using (var reader = XmlReader.Create(stringReader))
            {
                list = (List<T>)serializer.Deserialize(reader);
            }
           // File.Copy(sourceFile, destFile, true);
           // File.Delete(sourceFile);
            return list;
        }

        public class LatLon
        {
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
        }

        public LatLon cargarLatitudLatitud(string Address)
        {
            var address = String.Format("http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false&Key=AIzaSyAvDlFBHhBvPxzMxXsGvgmdTcw_mgyH-GA", Address.Replace(" ", "+"));
            var result = new System.Net.WebClient().DownloadString(address);
            var root = JsonConvert.DeserializeObject<RootObject>(result);
            var geo = new LatLon();
            foreach (var singleResult in root.results)
            {
                var location = singleResult.geometry.location;
                geo.Latitude = location.lat;
                geo.Longitude = location.lng;
            }
            return geo;
        }

        public void Process()
        {
            new RepositorySAPInterface().Process();
        }

        public void ImportFiles(string directoryDown)
        {
            DirectoryInfo di = new DirectoryInfo(directoryDown);

            DirectoryInfo dest = new DirectoryInfo(Path.Combine(new DirectoryInfo(GlobalConfiguration.LocateFiles).ToString(), DateTime.Today.ToString("yyyyMMdd")));

            if (!dest.Exists) dest.Create();

            List<string> lstFiles = new List<string>();

            // lista de archivos procesados
            var listProcesado = new RepositorySAPResumen().GetAll(DateTime.Today);

            foreach (var fi in di.GetFiles())
            {
                if(listProcesado.Where( p => p.Contenedor.Equals(fi.Name, StringComparison.OrdinalIgnoreCase)).Count() > 0)
                {
                    File.Delete(fi.FullName);
                    continue;
                }
                             
                string sourceFile = System.IO.Path.Combine(new DirectoryInfo(directoryDown).ToString(), fi.Name);

                Console.WriteLine(String.Format("Procesando ... {0}", fi));

                if (fi.Name.Contains("PT_Refacciones"))
                {
                    var data = PTRefProc(fi.FullName);
                    LoadDataPTRef(data, fi.Name);
                    string filename = fi.Name;
                    //01.-11052018030000PT_Refacciones.xml
                    //    01234567890123
                    filename = filename.Replace("PT_Refacciones.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 1);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "PT_Refacciones",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = data.Material.Count(),
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });
                }
                if (fi.Name.Contains("BOM"))
                {
                    var data = BOMProc(fi.FullName);
                    LoadDataBOM(data, fi.Name);
                    string filename = fi.Name;
                    //02.-11052018032000BOM.xml
                    //    01234567890123
                    filename = filename.Replace("BOM.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 1);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "BOM",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = data.BOM == null ? 0 : data.BOM.Count(),
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });
                }
                if (fi.Name.Contains("Precios"))
                {

                    var data = PrecioProc(fi.FullName);
                    LoadDataPrecio(data, fi.Name);
                    string filename = fi.Name;
                    //03.-11052018093709Precios.xml
                    //    01234567890123
                    filename = filename.Replace("Precios.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 1);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "Precios",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = data.Precio.Count(),
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });
                }
                if (fi.Name.Contains("LugarCompra"))
                {
                    var data = LugarCompraProc(fi.FullName);
                    LoadDataLugarCompra(data, fi.Name);
                    string filename = fi.Name;
                    //04.-24012018183546LugarCompra.xml
                    //    01234567890123
                    filename = filename.Replace("LugarCompra.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 1);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "LugarCompra",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = data.LugComps.Count(),
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });
                }
                if (fi.Name.Contains("Fallas"))
                {
                    int rowsCount = 0;
                    var data = FallaProc(fi.FullName);
                    LoadDataFalla(data, fi.Name, out rowsCount);
                    string filename = fi.Name;
                    //10.-13032018124653Fallas.xml
                    //    01234567890123
                    filename = filename.Replace("Fallas.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 1);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "Fallas",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = rowsCount,
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });

                }
                if (fi.Name.Contains("Clientes"))
                {
                    var data = ClientesProc(fi.FullName);
                    LoadDataCliente(data, fi.Name);
                    string filename = fi.Name;
                    //11.-28022018000115Clientes.xml
                    //    01234567890123
                    filename = filename.Replace("Clientes.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 1);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "Clientes",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = data.CLIENTES.Count(),
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });

                }
                if (fi.Name.Contains("ODS"))
                {
                    var data = ODSProc(fi.FullName);
                    LoadDataODS(data, fi.Name);

                    string filename = fi.Name;
                    //12.-28022018000115ODS.xml
                    //    01234567890123
                    filename = filename.Replace("ODS.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 1);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "ODS",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = data.ODS.Count(),
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });
                }
                if (fi.Name.Contains("Ref_Man"))
                {
                    var data = RefManProc(fi.FullName);
                    LoadDataRefMan(data, fi.Name);

                    string filename = fi.Name;
                    //13.-28022018000115Ref_Man.xml
                    //    01234567890123
                    filename = filename.Replace("Ref_Man.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 1);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "Ref_Man",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = data.ODSREFMAN.Count(),
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });
                }
                if (fi.Name.Contains("InventarioTec"))
                {
                    var data = InventoryProc(fi.FullName);
                    LoadDataInventory(data, fi.Name);

                    string filename = fi.Name;
                    //0123456
                    //14.-MX28022018000115InventarioTec.xml
                    //      01234567890123
                    filename = filename.Replace("InventarioTec.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 5);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "InventarioTec",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = data.Inventario.Items.Item.Count(),
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });
                }
                if (fi.Name.Contains("CifrasControl"))
                {
                    var data = CifrasControlProc(fi.FullName);
                    LoadDataCifrasControl(data, fi.Name);

                    string filename = fi.Name;
                    //26062018092354CifrasControl.xml
                    //    01234567890123
                    filename = filename.Replace("CifrasControl.xml", "");
                    filename = filename.Substring(filename.IndexOf("-") + 1);
                    DateTime fh = new DateTime(
                            Convert.ToInt32(filename.Substring(4, 4)),
                            Convert.ToInt32(filename.Substring(2, 2)),
                            Convert.ToInt32(filename.Substring(0, 2)),
                            Convert.ToInt32(filename.Substring(8, 2)),
                            Convert.ToInt32(filename.Substring(10, 2)),
                            Convert.ToInt32(filename.Substring(12, 2))
                        );
                    new RepositorySAPResumen().Insert(new serviplus.data.Model.Resumen()
                    {
                        ResumenID = 0,
                        Tipo = "CifrasControl",
                        Contenedor = fi.Name,
                        Fecha = fh,
                        Registros = data.CF != null ? 1 : 0,
                        Insertados = 0,
                        Actualizados = 0,
                        Procesado = false,
                        Inicio = null,
                        Termino = null,
                        BI_ODS_Udp = false,
                        Creacion = DateTime.UtcNow,
                        Modificacion = DateTime.UtcNow
                    });
                }

                File.Copy(fi.FullName, Path.Combine(dest.FullName, fi.Name), true);
                File.Delete(fi.FullName);

            }

            new RepositorySAPInterface().Process();
        }

        private void LoadDataPTRef(RootPTRef data, string contenedor)
        {
            new RepositorySAPPTRef().BulkInsert(data.Material.Select(p => new serviplus.data.Model.PTRef
            {
                PTRefID = 0,
                IDMaterial = p.IDMaterial,
                DescripcionRefaccion = p.DescripcionRefaccion,
                OrganizacionVentas = p.OrganizacionVentas,
                CanalDistribucion = p.CanalDistribucion,
                Centro = p.Centro,
                GrupoMaterial1 = p.GrupoMaterial1,
                GrupoMaterial4 = p.GrupoMaterial4,
                EstatusMaterial = p.EstatusMaterial,
                TipoProducto = p.TipoProducto,
                Contenedor = contenedor,
                Procesado = false,
                Creacion = DateTime.UtcNow,
                Modificacion = DateTime.UtcNow
            }).ToList());
        }

        private void LoadDataBOM(RootBOM data, string contenedor)
        {
            if (data.BOM != null)
            {
                new RepositorySAPBOM().BulkInsert(data.BOM.Select(p => new serviplus.data.Model.BOM
                {
                    MaterialPT = p.MaterialPT,
                    Centro = p.Centro,
                    IDRefaccion = p.IDRefaccion,
                    Cantidad = p.Cantidad,
                    Contenedor = contenedor,
                    Procesado = false,
                    Creacion = DateTime.UtcNow,
                    Modificacion = DateTime.UtcNow
                }).ToList());
            }
        }

        private void LoadDataPrecio(RootPrecio data, string contenedor)
        {
            new RepositorySAPPrecios().BulkInsert(data.Precio.Select(p => new serviplus.data.Model.PRECIOS {
                Material = p.Material,
                TipoCondicion = p.TipoCondicion,
                OrganizacionVentas = p.OrganizacionVentas,
                CanalDistribucion = p.CanalDistribucion,
                ListaPrecios = p.ListaPrecios,
                GrupoMaterial1 = p.GrupoMaterial1,
                GrupoMaterial4 = p.GrupoMaterial4,
                PrecioMaterial = p.PrecioMaterial,
                Moneda = p.Moneda,
                FechaValidez = p.FechaValidez,
                FechaFinValidez = p.FechaFinValidez,
                Contenedor = contenedor,
                Procesado = false,
                Creacion = DateTime.UtcNow,
                Modificacion = DateTime.UtcNow
            }).ToList());
        }

        private void LoadDataCliente(RootCliente data, string contenedor)
        {
            new RepositorySAPClientes().BulkInsert(data.CLIENTES.Select(p => new serviplus.data.Model.Clientes { 
                ClienteID = 0,
                IDOrden = p.IDOrden,
                IDCliente = p.IDCliente,
                Nombrecliente = p.Nombrecliente,
                ApellidoCliente = p.ApellidoCliente,
                Calle = p.Calle,
                Numero = p.Numero,
                NumeroInterior = p.NumeroInterior,
                Colonia = p.Colonia,
                Delegacion = p.Delegacion,
                CodigoPosta = p.CodigoPosta,
                Referencia1 = p.Referencia1,
                Referencia2 = p.Referencia2,
                Referencia3 = p.Referencia3,
                Referencia4 = p.Referencia4,
                Referencia5 = p.Referencia5,
                TEL_CASA = p.TEL_CASA,
                TEL_TRABAJO = p.TEL_TRABAJO,
                EXT_TRABAJO = p.EXT_TRABAJO,
                CELULAR = p.CELULAR,
                email = p.email,
                RFCCEDULA = p.RFCCEDULA,
                ODS_PADRE = p.ODS_PADRE,
                Contenedor = contenedor,
                Procesado = false,
                Creacion = DateTime.UtcNow,
                Modificacion = DateTime.UtcNow
            }).ToList());   
        }

        private void LoadDataODS(RootODS data, string contenedor)
        {
            new RepositorySAPODS().BulkInsert(data.ODS.Select(p => new serviplus.data.Model.ODS {
                ODSID = 0,
                IDOrden = p.IDOrden,
                IDTecnico = p.IDTecnico,
                Region = p.Region,
                Modulo = p.Modulo,
                ModuloN3 = p.ModuloN3,
                Base = p.Base,
                IDBaseInstalada = p.IDBaseInstalada,
                Modelo = p.Modelo,
                DescripcionModelo = p.DescripcionModelo,
                NumeroSerie = p.NumeroSerie,
                IDLugarCompra = p.IDLugarCompra,
                DescripcionLugarCompra = p.DescripcionLugarCompra,
                FechaCompra = p.FechaCompra,
                FechaCreacionODS = p.FechaCreacionODS,
                FechaProgramacion = p.FechaProgramacion,
                HoraProgramacion = p.HoraProgramacion,
                Prioridad = p.Prioridad,
                TipoServicio = p.TipoServicio,
                SintomaFalla = p.SintomaFalla,
                Notas = p.Notas,
                ODS_PADRE = p.ODS_PADRE,
                Contenedor = contenedor,
                Procesado = false,
                Creacion = DateTime.UtcNow,
                Modificacion = DateTime.UtcNow
            }).ToList());
        }

        private void LoadDataInventory(RootInventory data, string contenedor)
        {
            new RepositorySAPInventory().BulkInsert(data.Inventario.Items.Item.Select(p => new serviplus.data.Model.REFACCIONES
            {
                RefaccionID = 0,
                ID_REF = p.IdRef,
                CENTRO = p.Centro,
                ALMACEN = p.Almacen,
                TOTDISP = Convert.ToInt32(p.TotDisp.Replace(".000", "")),
                //Contenedor = contenedor,
                Procesado = false,
                Creacion = DateTime.UtcNow,
                Modificacion = DateTime.UtcNow
            }).ToList());
        }

        private void LoadDataRefMan(RootRefMan data, string contenedor)
        {
            List<serviplus.data.Model.REFMAN> result = new List<serviplus.data.Model.REFMAN>();

            foreach (var item in data.ODSREFMAN)
            {
                if (item.REFMANO == null) continue;
                foreach (var itemRefMano in item.REFMANO)
                {
                    if (itemRefMano.REFMAN == null) continue;
                    foreach (var itemRefMan in itemRefMano.REFMAN)
                    {
                        result.Add(new serviplus.data.Model.REFMAN()
                        {
                            RefManID = 0,
                            ID_ORDEN = item.ID_ORDEN,
                            ID_RefMan = itemRefMan.ID_RefMan,
                            PosicionItem = itemRefMan.PosicionItem,
                            DescripcionRefMan = itemRefMan.DescripcionRefMan,
                            EstatusRefMan = itemRefMan.EstatusRefMan,
                            EstatusEsq = itemRefMan.EstatusEsq, 
                            Cantidad = itemRefMan.Cantidad,
                            Precio = itemRefMan.Precio,
                            Contenedor = contenedor,
                            Procesado = false,
                            Creacion = DateTime.UtcNow,
                            Modificacion = DateTime.UtcNow
                        });
                    }
                   
                }
                
            }

            new RepositorySAPRefMan().BulkInsert(result);
        }

        private void LoadDataFalla(RootFalla _data, string contenedor, out int RowsCount)
        {
            var result = new List<data.Model.Fallas>();

            foreach (var item in _data.ODSMODELO)
            {
                result.AddRange(item.Fail.Fallas.Select(p => new data.Model.Fallas() {
                    FallaID = 0,
                    Modelo = item.Modelo,
                    IDFalla = p.IDFalla,
                    DescripcionFalla = p.DescripcionFalla,
                    Complejidad = p.Complejidad,
                    Contenedor = contenedor,
                    Procesado = false,
                    Creacion = DateTime.UtcNow,
                    Modificacion = DateTime.UtcNow
                }));
            }
            RowsCount = result.Count();
            new RepositorySAPFalla().BulkInsert(result);
        }

        private void LoadDataLugarCompra(RootLugarCompra data, string contenedor)
        {
            new RepositorySAPLugarCompra().BulkInsert(data.LugComps.Select(p => new serviplus.data.Model.LugarCompra() {
                LugarCompraID = 0,
                ID_PAIS = p.ID_PAIS,
                ESTADO = p.ESTADO,
                IDSucursal = p.IDSucursal,
                LugarCompra1 = p.LugarCompra,
                IDClienteDist = p.IDClienteDist,
                Contenedor = contenedor,
                Procesado = false,
                Creacion = DateTime.UtcNow,
                Modificacion = DateTime.UtcNow
            }).ToList());
        }

        private void LoadDataCifrasControl(RootCifrasControl data, string contenedor)
        {
            var lstData = new List<serviplus.data.Model.CifrasControl>();

            lstData.Add(new data.Model.CifrasControl()
            {
                CifrasControlID = 0,
                NumeroArchivos = data.CF.NumeroArchivos == "" ? 0 : Convert.ToInt32(data.CF.NumeroArchivos),
                ODS = data.CF.ODS == "" ? 0 : Convert.ToInt32(data.CF.ODS),
                Clientes = data.CF.Clientes == "" ? 0 : Convert.ToInt32(data.CF.Clientes),
                Ref_Man = data.CF.Ref_Man == "" ? 0 : Convert.ToInt32(data.CF.Ref_Man),
                Fallas = data.CF.Fallas == "" ? 0 : Convert.ToInt32(data.CF.Fallas),
                Contenedor = contenedor,
                Procesado = false,
                Creacion = DateTime.UtcNow,
                Modificacion = DateTime.UtcNow
            });

            new RepositorySAPCifrasControl().BulkInsert(lstData);
        }

        private RootPrecio PrecioProc(string fileName)
        {
            RootPrecio result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RootPrecio), new XmlRootAttribute("Precios"));

                using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string contents = sr.ReadToEnd();

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        writer.Write(contents.Replace("&", ""));
                        writer.Flush();
                        stream.Position = 0;

                        using (StreamReader sr2 = new StreamReader(stream))
                        {

                            using (var reader = XmlReader.Create(sr2))
                            {
                                result = (RootPrecio)serializer.Deserialize(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|PrecioProc");
                result = new RootPrecio() { Precio = new List<SAPPrecio>().ToArray() };
            }
            return result;
        }

        private RootBOM BOMProc(string fileName)
        {
            RootBOM result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RootBOM), new XmlRootAttribute("BOMS"));

                var content = readFile(fileName);

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (RootBOM)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|BOMProc");
                result = new RootBOM() { BOM = new List<SAPBOM>().ToArray() };
            }
            return result;
        }

        private RootPTRef PTRefProc(string fileName)
        {
            RootPTRef result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RootPTRef), new XmlRootAttribute("Materiales"));

                using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string contents = sr.ReadToEnd();

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        writer.Write(contents.Replace("&amp;", "Y").Replace("&", "Y"));

                        writer.Flush();
                        stream.Position = 0;

                        using (StreamReader sr2 = new StreamReader(stream))
                        {

                            using (var reader = XmlReader.Create(sr2))
                            {
                                result = (RootPTRef)serializer.Deserialize(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|PTRefProc");
                result = new RootPTRef() { Material = new List<SAPMaterial>().ToArray() };
            }

            return result;
        }

        private RootCliente ClientesProc(string fileName)
        {
            RootCliente result;
            try
            {
                
                XmlSerializer serializer = new XmlSerializer(typeof(RootCliente), new XmlRootAttribute("CLIENTES"));

                var content = readFile(fileName);

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (RootCliente)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|ClientesProc");
                result = new RootCliente() { CLIENTES = new List<entities.Entity.SAP.ODSCLIENTE>().ToArray() };
            }
            return result;
        }

        private RootODS ODSProc(string fileName)
        {
            RootODS result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RootODS), new XmlRootAttribute("ORDENES"));

                var content = readFile(fileName);

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (RootODS)serializer.Deserialize(reader);
                    }
                }
            }
            catch 
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(RootODS), new XmlRootAttribute("ORDENES"));

                    var content = readFile(fileName);
                    string aLine, aComplete = null;

                    using (var stringReader = new StringReader(content))
                    {

                        while (true)
                        {
                            aLine = stringReader.ReadLine();

                            if (aLine != null)
                            {
                                if (aLine.IndexOf("<Notas>") > 0)
                                {
                                    if (aLine.IndexOf("</Notas>") < 0)
                                    {
                                        if (aLine.IndexOf("</") > 0) aLine = aLine.Replace("</", "");
  
                                        aLine = aLine + "</Notas>";
                                    }
                                }

                                aComplete = aComplete + aLine + " ";
                            }
                            else
                            {
                                aComplete = aComplete + "\n";
                                break;
                            }
                        }
                    }

                    using (var stringReader = new StringReader(aComplete))
                    {
                        using (var reader = XmlReader.Create(stringReader))
                        {
                            result = (RootODS)serializer.Deserialize(reader);
                        }
                    }

                }
                catch (Exception ex)
                {
                    new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|ODSProc");
                    result = new RootODS() { ODS = new List<SAPODS>().ToArray() };                    
                }                
            }
            return result;
        }
        
        private RootInventory InventoryProc(string fileName)
        {
            RootInventory result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RootInventory));

                var content = readFile(fileName);

                content = content.Replace("xmlns:ns1=\"http://mabe.com/MW/CONFINAL/Mobile/InventarioTecnico\"", "");
                content = content.Replace("ns1:", "");

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (RootInventory)serializer.Deserialize(reader);
                    }
                }
            }
            catch(Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|InventoryProc");
                result = new RootInventory() { Inventario = new SAPInventario() { OrgVentas = "", Items = new SAPInventarioItems() { Item = new List<SAPInventarioItem>().ToArray() } } };
            }
            return result;
        }

        private RootRefMan RefManProc(string fileName)
        {
            RootRefMan result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RootRefMan), new XmlRootAttribute("REFMAN"));

                var content = readFile(fileName);

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (RootRefMan)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|RefManProc");
                result = new RootRefMan() { ODSREFMAN = new List<entities.Entity.SAP.ODSREFMAN>().ToArray() };
            }
            return result;
        }

        private RootFalla FallaProc(string fileName)
        {
            RootFalla result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RootFalla), new XmlRootAttribute("FALLAS"));

                var content = readFile(fileName);

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (RootFalla)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|FallaProc");
                result = new RootFalla() { ODSMODELO = new List<SAPODSModelo>().ToArray() };
            }
            return result;
        }

        private RootLugarCompra LugarCompraProc(string fileName)
        {
            RootLugarCompra result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RootLugarCompra), new XmlRootAttribute("LUGARCOMPRA"));

                var content = readFile(fileName);

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (RootLugarCompra)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|LugarCompraProc");
                result = new RootLugarCompra() { LugComps = new List<SAPLugComp>().ToArray() };
            }
            return result;
        }

        private RootCifrasControl CifrasControlProc(string fileName)
        {
            RootCifrasControl result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RootCifrasControl), new XmlRootAttribute("CifrasControl"));

                var content = readFile(fileName);

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (RootCifrasControl)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface|CifrasControlProc");
                result = new RootCifrasControl() { CF = new SAPCF() };
            }
            return result;
        }

        private string readFile(string fileName)
        {
            string result = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.UTF8))
                {
                    // Read the stream to a string, and write the string to the console.
                    result = sr.ReadToEnd();
                }
            }
            catch { }

            return result;

        }

        public void GetFromWS()
        {
            Console.WriteLine(string.Format("Inicio {0}", DateTime.Now.ToString()));
            Parallel.Invoke(getGeolocation, getBI, getHistoryODS);
            string msg = "";
            string newLine = "<BR />";

            foreach (var itemResumen in new RepositorySAPResumen().GetAll(false))
            {
                itemResumen.BI_ODS_Udp = true;
                new RepositorySAPResumen().Update(new data.Model.Resumen() {
                    Actualizados = itemResumen.Actualizados,
                    BI_ODS_Udp = itemResumen.BI_ODS_Udp,
                    Contenedor = itemResumen.Contenedor,
                    Creacion = itemResumen.Creacion,
                    Fecha = itemResumen.Fecha,
                    Inicio = itemResumen.Inicio,
                    Insertados = itemResumen.Insertados,
                    Modificacion = itemResumen.Modificacion,
                    Procesado = itemResumen.Procesado,
                    Registros = itemResumen.Registros,
                    ResumenID = itemResumen.ResumenID,
                    Termino = itemResumen.Termino,
                    Tipo = itemResumen.Tipo
                });

                msg += "Proceso de importación de ODS" + newLine + newLine;
                msg += "Resumen" + newLine + newLine;
                msg += "File: " + itemResumen.Contenedor + newLine;
                msg += "Fecha: " + itemResumen.Fecha.ToString() + newLine;
                msg += "Tipo: " + itemResumen.Tipo + newLine;
                msg += "Registros: " + itemResumen.Registros.ToString() + newLine;
                msg += "Actualizados: " + itemResumen.Actualizados.ToString() + newLine;
                msg += "Procesado: " + (itemResumen.Procesado == true ? "SI" : "NO") + newLine;
                msg += "BI ODS Actualizado: " + (itemResumen.BI_ODS_Udp == true ? "SI" : "NO") + newLine;
                msg += "Inicio: " + itemResumen.Inicio.ToString() + newLine;
                msg += "Termino: " + itemResumen.Termino.ToString() + newLine + newLine;

                msg += "Cadena de conexión: " + new RepositorySAPResumen().GetStringConnection() + newLine + newLine;

                var dataCifraControl = new RepositorySAPCifrasControl().GetAll(itemResumen.Contenedor);

                if(dataCifraControl.Count() > 0)
                {
                    msg += "Cifras de control: " + newLine + newLine;
                    foreach (var itemCC in dataCifraControl)
                    {
                        msg += "Contenedor: " + itemCC.Contenedor.ToString() + newLine;
                        msg += "Clientes: " + itemCC.Clientes.ToString() + newLine;
                        msg += "ODS: " + itemCC.ODS.ToString() + newLine;
                        msg += "Ref_Man: " + itemCC.Ref_Man.ToString() + newLine;
                        msg += "Fallas: " + itemCC.Fallas.ToString() + newLine;
                    }                   
                }
            }
            if(msg != "") { new BusinessInterfaceNotification().SendNotification(msg); }
            
            Console.WriteLine(string.Format("Termino {0}", DateTime.Now.ToString()));
        }

        public void getGeolocation()
        {         
            foreach (var item in new BusinessMonitor().GetListAddressWithOutLocation(DateTime.Today))
            {
                var dataODS = new BusinessOrder().GetByID(item.OrderID);
                var dataClient = new BusinessClient().GetByID(dataODS.FK_ClientID);

                Console.WriteLine(String.Format("Get Geo {0}", dataClient.ClientID));

                
                var geo = getLatitudLatitud(dataClient.StreetAddress, dataClient.NumberExternalAddress, dataClient.BoroughAddress, dataClient.MunicipalityAddress, dataClient.PostalCodeAddress, "mx");
                if (geo.Latitude != null)
                {
                    item.LatitudeAddress = float.Parse(geo.Latitude.ToString());
                    item.LogitudeAddress = float.Parse(geo.Longitude.ToString());
                    new BusinessMonitor().Update(item);
                }
            }
        }

        public void getBI()
        {
            foreach (var itemResumen in new RepositorySAPResumen().GetAll(false))
            {
                foreach (var item in new RepositorySAPClientes().GetAll(itemResumen.Contenedor))
                {             
                    Console.WriteLine(String.Format("Get BI {0}", item.IDCliente));
                    getWSBI(item.IDCliente);
                }                
            }            
        }

        public void getHistoryODS()
        {
            foreach (var itemResumen in new RepositorySAPResumen().GetAll(false))
            {
                foreach (var itemODS in new RepositorySAPODS().GetAll(itemResumen.Contenedor))
                {
                    var item = new BusinessOrder().GetByOrderID(itemODS.IDOrden);
                    if (item == null) continue;
                    Console.WriteLine(String.Format("Get ODS {0}", item.OrderID));
                    getWSHistoryODS(
                        new BusinessClient().GetByID(item.FK_ClientID).ClientID,
                        new BusinessInstalledBase().GetByID(item.FK_InstalledBaseID).InstalledBaseID,
                        item.PK_OrderID,
                        item.FK_ClientID,
                        item.FK_InstalledBaseID
                        );
                }
            }
        }

        public LatLon getLatitudLatitud(string street, string number, string borough, string municipality, string cp, string country)
        {
            string address = "";
            bool salir = false;
            var geo = new LatLon();

          
            int i = 0;

            while (!salir)
            {
                if (i == 0)
                {
                    address = getParam(street) + getParam(number) + getParam(borough) + getParam(municipality) + getParam(cp);                    
                }
                if (i == 1)
                {
                    address = getParam(street) + getParam(borough) + getParam(municipality) + getParam(cp);        
                }
                if (i == 2)
                {
                    address = getParam(street) + getParam(municipality) + getParam(cp);                  
                }

                if (i == 3)
                {
                    address = getParam(municipality) + getParam(cp);        
                    salir = true;
                }

                if (address.Contains("+")) address = address.Substring(0, address.LastIndexOf('+') - 1);

                if (address.Count() > 0)
                {                   
                    var url = String.Format("https://maps.google.com/maps/api/geocode/json?address={0}&region={1}&key={2}", address, country, GlobalConfiguration.GoogleMatrixGeoLocation);
                    try
                    {
                        var result = new System.Net.WebClient().DownloadString(url);
                        var root = JsonConvert.DeserializeObject<RootObject>(result);
                        if (root.status == "OVER_QUERY_LIMIT")
                        {
                            string msg = "-----------------------------------------------" + Environment.NewLine;
                            msg += string.Format("URL: {0}", url) + Environment.NewLine;
                            msg += string.Format("Estatus: {0}", root.status) + Environment.NewLine;
                            msg += "-----------------------------------------------" + Environment.NewLine;
                            new BusinessImportODSLogger().WriteEntry(msg);
                        }
                        if (root.results.Count() > 0)
                        {
                            foreach (var singleResult in root.results)
                            {
                                var location = singleResult.geometry.location;
                                geo.Latitude = location.lat;
                                geo.Longitude = location.lng;
                                salir = true;
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string msg = "-----------------------------------------------" + Environment.NewLine;
                        msg += string.Format("URL: {0}", url) + Environment.NewLine;
                        msg += string.Format("Error: {0}", ex.Message) + Environment.NewLine;
                        msg += string.Format("ST: {0}", ex.StackTrace) + Environment.NewLine;
                        msg += "-----------------------------------------------" + Environment.NewLine;
                        new BusinessImportODSLogger().WriteEntry(msg);
                    }
                }
                else
                {
                    salir = true;
                }
                i++;
            }
           
            return geo;
        }

        private string getParam(string data)
        {            
            data = data.Trim().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').ToLower();
            return data.Count() > 0 ? data + "+" : "";
        }
        
        private void getWSBI(string IDCliente)
        {
            List<srHistoryBase.DT_BaseInstalada_InItem> dataBase = new List<srHistoryBase.DT_BaseInstalada_InItem>();
            EntityClient cliente;
            try
            {
                cliente = FacadeClient.GetByClientID(IDCliente);
                dataBase = new BusinessMabe().HistoryBase(IDCliente);

                //insertar datos base instalada
                foreach (var item in dataBase)
                {
                    try
                    {
                        var dataBI = new BusinessInstalledBase().GetByInstalledBase(item.ID_de_Registro);
                        var dataProduct = new BusinessProduct().GetByModel(item.ID_de_Producto);
                        var dataShopPlace = new BusinessShopPlace().GetByShopPlace(item.Lugar_Compra);
                        int? ShopPlaceID = null;
                        if (dataShopPlace != null) ShopPlaceID = dataShopPlace.PK_ShopPlaceID;

                        DateTime? ShopDate = null;
                        try
                        {
                            ShopDate = ParseDate(item.Fecha_Compra);
                        }

                        catch { }

                        item.No_Serie = string.IsNullOrEmpty(item.No_Serie) ? "" : item.No_Serie;

                        if (dataBI == null)
                        {
                            new BusinessInstalledBase().Insert(cliente.PK_ClientID, dataProduct.PK_ProductID, ShopPlaceID, item.ID_de_Registro, item.No_Serie, item.Fecha_Compra, null, null);
                        }
                        else
                        {
                            dataBI.FK_ClientID = cliente.PK_ClientID;
                            dataBI.FK_ProductID = dataProduct.PK_ProductID;
                            dataBI.FK_ShopPlaceID = ShopPlaceID;
                            dataBI.InstalledBaseID = item.ID_de_Registro;
                            dataBI.SerialNumber = item.No_Serie;
                            dataBI.ShopDate = ShopDate;
                            new BusinessInstalledBase().Update(dataBI);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        new BusinessImportODSLogger().WriteError(ex, "BusinessInterface - getBI - Base Instalada - IDCliente (" + IDCliente + ") Base (" + item.ID_de_Registro + ") Producto (" + item.ID_de_Producto + ") Lugar de compra (" + item.Lugar_Compra + ") ");
                    }

                }
            }
            catch (Exception ex)
            {
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface - getBI - Clientes - " + IDCliente);
            }
            
        }

        private void getWSHistoryODS(string IDCliente, string InstalledBaseID, int PK_OrderID, int FK_ClientID, int FK_InstalledBaseID)
        {
            try
            {
                var dataHistory = new BusinessMabe().HistoryODSByClient(IDCliente, InstalledBaseID); // por cliente

                // insertar datos order history
                foreach (var item in dataHistory)
                {

                    var dataDBHistory = new BusinessHistory().GetByOrderID(PK_OrderID);

                    if (dataDBHistory.Where(p => p.OrderID == item.ID_Oper).Count() > 0)
                    {
                        // update
                        var entity = dataDBHistory.Where(p => p.OrderID == item.ID_Oper).FirstOrDefault();
                        entity.CloseDate = ParseDate(item.Fecha_Cierre_Orden);
                        entity.Failure1 = string.IsNullOrEmpty(item.Desc_ID_Falla1) ? "" : item.Desc_ID_Falla1;
                        entity.Failure2 = string.IsNullOrEmpty(item.Desc_ID_Falla2) ? "" : item.Desc_ID_Falla2;
                        entity.Failure3 = string.IsNullOrEmpty(item.Desc_ID_Falla3) ? "" : item.Desc_ID_Falla3;
                        entity.FailureID1 = string.IsNullOrEmpty(item.ID_Falla1) ? "" : item.ID_Falla1;
                        entity.FailureID2 = string.IsNullOrEmpty(item.ID_Falla2) ? "" : item.ID_Falla2;
                        entity.FailureID3 = string.IsNullOrEmpty(item.ID_Falla3) ? "" : item.ID_Falla3;
                        entity.FK_ClientID = FK_ClientID;
                        entity.FK_InstalledBaseID = FK_InstalledBaseID;
                        entity.FK_OrderID = PK_OrderID;
                        entity.Guaranty = string.IsNullOrEmpty(item.Tipo_Serv) ? "" : item.Tipo_Serv;
                        entity.ItemStatus = string.IsNullOrEmpty(item.Estatus_Visita) ? "" : item.Estatus_Visita;
                        entity.ModifyDate = DateTime.UtcNow;
                        entity.OrderID = string.IsNullOrEmpty(item.ID_Oper) ? "" : item.ID_Oper;
                        entity.OrderStatus = string.IsNullOrEmpty(item.Estatus_Oper) ? "" : item.Estatus_Oper;
                        entity.ShopDate = new DateTime(1980, 1, 1);
                        entity.Status = true;
                        new BusinessHistory().Update(entity);
                    }
                    else
                    {
                        // insert 
                        var entity = new EntityHistory();
                        entity.CloseDate = ParseDate(item.Fecha_Cierre_Orden);
                        entity.CreateDate = DateTime.UtcNow;
                        entity.Failure1 = string.IsNullOrEmpty(item.Desc_ID_Falla1) ? "" : item.Desc_ID_Falla1;
                        entity.Failure2 = string.IsNullOrEmpty(item.Desc_ID_Falla2) ? "" : item.Desc_ID_Falla2;
                        entity.Failure3 = string.IsNullOrEmpty(item.Desc_ID_Falla3) ? "" : item.Desc_ID_Falla3;
                        entity.FailureID1 = string.IsNullOrEmpty(item.ID_Falla1) ? "" : item.ID_Falla1;
                        entity.FailureID2 = string.IsNullOrEmpty(item.ID_Falla2) ? "" : item.ID_Falla2;
                        entity.FailureID3 = string.IsNullOrEmpty(item.ID_Falla3) ? "" : item.ID_Falla3;
                        entity.FK_ClientID = FK_ClientID;
                        entity.FK_InstalledBaseID = FK_InstalledBaseID;
                        entity.FK_OrderID = PK_OrderID;
                        entity.Guaranty = string.IsNullOrEmpty(item.Tipo_Serv) ? "" : item.Tipo_Serv;
                        entity.ItemStatus = string.IsNullOrEmpty(item.Estatus_Visita) ? "" : item.Estatus_Visita;
                        entity.ModifyDate = DateTime.UtcNow;
                        entity.OrderID = string.IsNullOrEmpty(item.ID_Oper) ? "" : item.ID_Oper;
                        entity.OrderStatus = string.IsNullOrEmpty(item.Estatus_Oper) ? "" : item.Estatus_Oper;
                        entity.PK_HistoryID = 0;
                        entity.ShopDate = new DateTime(1980, 1, 1);
                        entity.Status = true;
                        new BusinessHistory().Insert(entity);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                new BusinessImportODSLogger().WriteError(ex, "BusinessInterface - getWSHistoryODS - ODS - History " + IDCliente + " IB " + InstalledBaseID);
            }
        }
    }
}
