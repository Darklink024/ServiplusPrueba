using System;
using System.Linq;
using System.Web.Mvc;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System.Web.Script.Serialization;
using net.paxialabs.mabe.serviplus.security.Filters;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;
using net.paxialabs.mabe.serviplus.domain.Facade.Interface;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.Commons;
using System.Collections.Generic;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System.Net;
using System.IO;

using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.domain.Facade.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Interface;

namespace net.paxialabs.mabe.serviplus.web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Orden
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetDistanceMatrix(string dataRequest)
        {
            try
            {
                return Json(FacadeGoogle.GetDistanceMatrix(new JavaScriptSerializer().Deserialize<ModelViewGoogleMapsDistanceMatrixRequest>(dataRequest)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetListVisit(string ListVisit)
        {
            ModelViewUserVisits objCred = new JavaScriptSerializer().Deserialize<ModelViewUserVisits>(ListVisit);
            try
            {
                string Usuario =  FacadeUsers.GetUserByToken(objCred.TokenUser).UserName;
                FacadeLog.MobileWriteEntry(Usuario + " INICIO_DescargaODS ");
                var lt = FacadeOrder.GetListOrden(objCred);
                
                FacadeLog.MobileWriteEntry(Usuario + " FIN_DescargaODS ");
             
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Generic_Exception(ex, Generic_Exception.ErrorCodes.ErrorGoogleMaps);
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetListODS(string ODS)
        {
            ModelViewUserVisits objCred = new JavaScriptSerializer().Deserialize<ModelViewUserVisits>(ODS);
            try
            {
                var lt = FacadeOrder.GetListODS(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetProducts(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeProduct.GetListProduct(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public ActionResult GetSpareParts(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeBuildOfMaterial.GetListSpareParts(objCred);
                var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var result = new ContentResult
                {
                    Content = serializer.Serialize(lt),
                    ContentType = "application/json"
                };
                return result;

                //return Json(lt, JsonRequestBehavior.AllowGet);
                //var result = Json(lt, JsonRequestBehavior.AllowGet);
                //result.MaxJsonLength = int.MaxValue;
                //return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetGuaranty(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeGuaranty.GetListGuaranty(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetFailure(string value)
        {
            try
            {
             ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
             var lt = FacadeCodeFailureByProduct.GetListCodeFailure(objCred);
             return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetShopPlace(string value)
        {
            try
            {
                
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);

                if (!objCred.Date.HasValue)
                {
                    FacadeLog.MobileWriteEntry("Order/GetShopPlace|FirstSync|" + value);

                    var detail = new List<ModelViewDetail>(); 
                    detail.Add(new ModelViewDetail()
                    {
                        Battery = "--",
                        ConnectionType = "--",
                        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        InnerException = "--",
                        Message = "Primera sincronización",
                        Module = "app.activities.SynchronizationActivity", 
                        OrderID = "--",
                        SignPercentage = "--",
                        SignType = "--",
                        StackTrace = "--",
                        TokenLog = "",
                        Type = "Information",
                        version = "--"
                    });

                    ModelViewLog dataODS = new ModelViewLog()
                    {
                        TokenApp = objCred.TokenApp,
                        TokenUser = objCred.TokenUser,
                        Detail = detail
                    };
                    var token = FacadeLogMobile.Insert(dataODS);
                }

                var lt = FacadeShopPlace.GetListShopPlace(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetStates(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeStates.GetListStates(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetCountries(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeCountries.GetListCountries(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [AllowCrossSiteJson]
        [JsonErrorHandler]
        public JsonResult GetPriority(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);

                var lt = FacadePriority.GetListPriority(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetCauseOrder(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeCauseOrder.GetListCauseOrder(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetBlackList()
        {
            try
            {
               
                var lt = FacadeBlacklist.GetAll().Select(x=>x.BlackListName);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetCauseVisit(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeStatusCauseVisit.GetListStatusVisit(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]

        public JsonResult GetSerialNumber(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeModelSerialNumber.GetListModel(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetGuarantyProduct(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeValidationGuarantyProduct.GetLisValidationProduct(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetGuarantyBOM(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeValidationGuarantyBOM.GetLisValidationBOM(objCred);

                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public ActionResult GetSparePartModel(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeBuildOfMaterial.GetListSpareParts().Where(p=> p.Model==objCred.Model);
                var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var result = new ContentResult
                {
                    Content = serializer.Serialize(lt),
                    ContentType = "application/json"
                };
                return result;
                //return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public ActionResult GetPricesModel(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadePrice.GetListPrices(objCred);

                var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var result = new ContentResult
                {
                    Content = serializer.Serialize(lt),
                    ContentType = "application/json"
                };
                return result;

                //return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetPricesWorkForce(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadePrice.GetListPricesWorkforce(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetLastFolio(string value)
        {
            try
            {
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                var lt = FacadeQuotation.GetListFolios(objCred);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetInvoicing(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetInvoicing|" + data);
                ModelViewInvoicing model = new JavaScriptSerializer().Deserialize<ModelViewInvoicing>(data);
                
                
                var result = FacadeInvoicing.SetInvoicing(model);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetPayment(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetPayment|" + data);
                ModelViewPayment model = new JavaScriptSerializer().Deserialize<ModelViewPayment>(data);
                var result = FacadePayment.SetPayment(model);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetBilling(string value, string Billing)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetBilling|" + Billing);
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                ModelViewBilling obj = new JavaScriptSerializer().Deserialize<ModelViewBilling>(Billing);

                if (obj.EstimatedTipe == 2)
                {
                    var result = FacadePolicy.SetPolicyQuotation(objCred, obj);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (obj.EstimatedTipe == 3)
                {
                    var result = FacadePolicy.SetRefQuotation(objCred, obj);
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    var result = FacadeOrder.SetQuotation(objCred, obj);
                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //[HttpPost]
        //[JsonErrorHandler]
        //[AllowCrossSiteJson]
        //public JsonResult CreateContrat(string value, string Contrat)
        //{
        //    try
        //    {
        //        FacadeLog.MobileWriteEntry("Order/CreateContrat|" + Contrat);
        //        ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
        //        ModelViewContrat obj = new JavaScriptSerializer().Deserialize<ModelViewContrat>(Contrat);
        //        var result = FacadePolicy.CreateContrat(obj);
        //        return Json(result, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPost]
        //[JsonErrorHandler]
        //[AllowCrossSiteJson]
        //public JsonResult CreateReceipt(string value, string SpareParts)
        //{
        //    try
        //    {
        //        ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
        //        FacadeLog.MobileWriteEntry("Order/CreatReceipt|" + SpareParts);
        //        ModelViewReceiptRef obj = new JavaScriptSerializer().Deserialize<ModelViewReceiptRef>(SpareParts);
        //        var result = FacadePolicy.CreateReceipt(objCred, obj);
        //        return Json(result, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetOrder(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetOrder|" + data);

                ModelViewODSMovilUploadList dataODS = new JavaScriptSerializer().Deserialize<ModelViewODSMovilUploadList>(data);
                 var ordens = FacadeOrder.Complete(dataODS);

                ModelViewResultREST result = new ModelViewResultREST();
                result.Result = "Success" + "|" + ordens;
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult UpdateStatusOrder(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/UpdateStatusOrder|" + data);

                ModelViewUpdateStatusOrder dataODS = new JavaScriptSerializer().Deserialize<ModelViewUpdateStatusOrder>(data);
                FacadeOrder.UpdateStatusOrder(dataODS);

                ModelViewResultREST result = new ModelViewResultREST();
                result.Result = "Success";

                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult UpdateSparePartsODS(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/UpdateSparePartsODS|" + data);

                List<SparePart> dataSparePartsODS = new JavaScriptSerializer().Deserialize<List<SparePart>>(data);
                FacadeSparePart.UpdateSparePartsODS(dataSparePartsODS);

                ModelViewResultREST result = new ModelViewResultREST();
                result.Result = "Success";

                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetValidationODS(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry(data);

                ModelViewUserVisits objCred = new JavaScriptSerializer().Deserialize<ModelViewUserVisits>(data);
                
                var result = FacadeOrder.GetReprogramingODS(objCred);


                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetEvidence(string data)
        {
            try
            {
                //FacadeLog.MobileWriteEntry("Order/SetEvidence|" + data);

                ModelViewOrderEvidenceUpload dataEvidence = new JavaScriptSerializer().Deserialize<ModelViewOrderEvidenceUpload>(data);
                FacadeOrderEvidence.RegisterEvidence(dataEvidence);
                //ModelViewResultREST result = new ModelViewResultREST();
                ModelViewResultREST result = new ModelViewResultREST();
                result.Result = "Success" + "|" + dataEvidence.OrderID +"|"+ dataEvidence.EvidenceID;
                             

                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetPreOrder(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetPreOrder|" + data);

                ModelViewPreODS dataODS = new JavaScriptSerializer().Deserialize<ModelViewPreODS>(data);
                FacadeOrder.Complete(dataODS);

                ModelViewResultREST result = new ModelViewResultREST();
                result.Result = "Success";

                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetBilling2(string value, string Billing,DateTime date)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetBilling|" + Billing);
                ModelViewUserG objCred = new JavaScriptSerializer().Deserialize<ModelViewUserG>(value);
                ModelViewBilling obj = new JavaScriptSerializer().Deserialize<ModelViewBilling>(Billing);

                if (obj.EstimatedTipe == 2)
                {
                    var result = FacadePolicy.SetPolicyQuotation(objCred, obj);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (obj.EstimatedTipe == 3)
                {
                    var result = FacadePolicy.SetRefQuotation(objCred, obj);
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    var result = FacadeOrder.SetQuotation2(objCred, obj,date);
                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetModuleReserveSP(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetModuleReserveSP|" + data);

                List<ModelViewApartadoModulo> dataRef = new JavaScriptSerializer().Deserialize<List<ModelViewApartadoModulo>>(data);
                ModelViewResultREST result = FacadeInterface.SetModuleReserveSP(dataRef);

                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetADRReserveSP(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetADRReserveSP|" + data);

                List<ModelViewApartadoADR> dataRef = new JavaScriptSerializer().Deserialize<List<ModelViewApartadoADR>>(data);
                ModelViewResultREST result = FacadeInterface.SetADRReserveSP(dataRef);
                
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetLogMobile(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetLogMobile|" + data);

                ModelViewLog dataODS = new JavaScriptSerializer().Deserialize<ModelViewLog>(data);
                var token = FacadeLogMobile.Insert(dataODS);

                ModelViewResultREST result = new ModelViewResultREST();
                result.Result = "Success" + "|" + token;
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult SetOrderSecuence(string data)
        {
            try
            {
                FacadeLog.MobileWriteEntry("Order/SetOrderSecuence|" + data);

                ModelViewUserSecuence Data = new JavaScriptSerializer().Deserialize<ModelViewUserSecuence>(data);
                FacadeOrder.SetOrderSecuence(Data);

                return Json("Sucess");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetInventory(string data)
        {
            try
            {
                ModelViewUserG Data = new JavaScriptSerializer().Deserialize<ModelViewUserG>(data);
                var lt = FacadeREFACCIONES.GetInventory(Data);
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetString()
        {
            try
            {

                //var dataSparePart = FacadeSparePart.GetAll("9406972995");



                List<string>  arrFilesOK1 = new List<string>();
                List<string> arrFilesOK = new List<string>(new string[] { "21022018003003ODS.xml", "21022018003003Clientes.xml", "21022018003003Fallas.xml", "21022018003003Ref_Man.xml", "21022018000102Fallas.xml", "21022018000102Ref_Man.xml", "21022018000102Clientes.xml", "21022018000102ODS.xml", "20022018194500Fallas.xml", "20022018194500Clientes.xml", "20022018194500ODS.xml", "20022018194500Ref_Man.xml", "20022018191500Clientes.xml", "20022018191500Fallas.xml", "20022018191500ODS.xml", "20022018191500Ref_Man.xml", "20022018184500Ref_Man.xml", "20022018184500Clientes.xml", "20022018184500Fallas.xml", "20022018184500ODS.xml", "20022018181500Clientes.xml", "20022018181500Fallas.xml", "20022018181500ODS.xml", "20022018181500Ref_Man.xml", "20022018180010Clientes.xml", "20022018180010Fallas.xml", "20022018180010ODS.xml", "20022018180010Ref_Man.xml", "20022018174516Fallas.xml", "20022018174516Clientes.xml", "20022018174516ODS.xml", "20022018174516Ref_Man.xml" });
                List<string> Files = new List<string>(new string[] { "Fallas", "Clientes", "ODS", "Ref_Man" });
                string valor = arrFilesOK.Count > 1 ? arrFilesOK[0].Substring(0, 14):null;
                foreach (var item in arrFilesOK)
                {
                    if(item.Contains(valor))
                    {
                        arrFilesOK1.Add(item);
                    }

                }

                wait(10);

                string completo = "2018-01-31 17:33:55,556 [27] INFO  MabeInterfaceLog - -------------------------INICIO_UpdateODS-------------------------" +
"TIPO_MENSAJE: Exito" +
"CLASE_MENSAJE: ZCRMCM_ACTUALIZACION" +
"DESCRIPCION: Campo Gratina Actualizada" +
"MENSAJE: 002" +
"------------------------ - FIN_UpdateODS------------------------ -" +
"2018 - 01 - 31 17:33:55,682[27] INFO MabeInterfaceLog --------------------------INICIO_UpdateODS------------------------ -" +
"  TIPO_MENSAJE: Exito" +
"  CLASE_MENSAJE: ZCRMCM_ACTUALIZACION" +
"  DESCRIPCION: Campo Estatus Actualizado" +
"  MENSAJE: 005" +
"  ------------------------ - FIN_UpdateODS------------------------ -" +
"  -------Actualizar EnvioCRM---- -" +
" Actualizo: OK" +
" OrderID: 9407097232" +
" ------------------------ - FIN Actualizar EnvioCRM-------------------------" +
"   2018 - 01 - 31 17:33:55,696[27] INFO MabeInterfaceLog --------------------------INICIO_UpdateODS------------------------ -" +
"     TIPO_MENSAJE: Error" +
"     CLASE_MENSAJE: ZCRMCM_ACTUALIZACION" +
"     DESCRIPCION: Error al Actualizar Notas" +
"MENSAJE: 012" +
"------------------------ - FIN_UpdateODS------------------------ -" +
"2018 - 01 - 31 17:33:55,709[27] INFO MabeInterfaceLog --------------------------INICIO_UpdateODS------------------------ -" +
"  TIPO_MENSAJE: Aviso" +
"  CLASE_MENSAJE: ZCRMCM_ACTUALIZACION" +
"  DESCRIPCION: Sin datos para Vía de pago" +
"MENSAJE: 014" +
"------------------------ - FIN_UpdateODS------------------------ - ";
                var tam = completo.Length;
                string MENSAJE = "No se pudo actualizar la orden 9406315491";                
                if (MENSAJE.Contains("No se pudo actualizar"))
                {
                    string buscar = "orden";
                    int Index = MENSAJE.IndexOf(buscar);
                    string OrderID = MENSAJE.Substring(Index + 5).Trim();
                    var order = FacadeOrder.GetByOrderID(OrderID);
           
                    //NegocioOrder.Update(order);
                }

                string IDOrden = "Posición: 50 | Modelo: WW01F00483 | Cantidad: 4 | Estatus: E0006 |||";
                string MensajeCRM = "Se creo correctamente la posición 50 de la orden 9406314934";
                if (MensajeCRM.Contains("correctamente"))
                {
                   string posicion = IDOrden.Split('|')[0].Split(':')[1].Trim();
                   posicion = posicion.PadLeft(10, '0');
                    string RefManID = IDOrden.Split('|')[1].Split(':')[1].Trim();
                   string Cantidad = IDOrden.Split('|')[2].Split(':')[1].Trim();
                   string Estatus = IDOrden.Split('|')[3].Split(':')[1].Trim();
                   string buscar = "orden";
                   int Index = MensajeCRM.IndexOf(buscar);
                   string substring = MensajeCRM.Substring(Index + 5).Trim();
                }
                 
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void wait(int x)
        {
            DateTime time = DateTime.Now;
            DateTime timefinal = DateTime.Now.AddSeconds(x);

            while (time < timefinal)
            {
                time = DateTime.Now;
            }
        }
    }
}