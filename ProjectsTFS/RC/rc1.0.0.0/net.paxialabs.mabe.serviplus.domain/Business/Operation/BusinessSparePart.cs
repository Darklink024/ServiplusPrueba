using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Interface;
using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessSparePart
    {
        public void Insert(int FK_OrderID, int? FK_BuildOfMaterialsID, int? FK_ProductID, int? FK_WorkforceID, float Cantidad, string PosicionItem, string EstatusRefMan, string EstatusEsquema, string DescripcionRefMan, string RefManID, float Precio)
        {
            var objRepository = new RepositorySparePart();

            EntitySparePart data = new EntitySparePart()
            {
                FK_OrderID = FK_OrderID,
                FK_BuildOfMaterialsID = FK_BuildOfMaterialsID,
                FK_ProductID = FK_ProductID,
                FK_WorkforceID = FK_WorkforceID,
                RefManID = RefManID,
                ItemRefMan = DescripcionRefMan,
                Quantity = (int)Cantidad,
                Price = Precio,
                Status = true,
                PosicionItem = PosicionItem.TrimStart('0'),
                SparePartStatus = EstatusRefMan,
                StatusSchema = EstatusEsquema,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);
        }

        public void Update(EntitySparePart refa, float Cantidad, string PosicionItem, string EstatusRefMan, string EstatusEsquema, string DescripcionRefMan, string RefManID, float Precio)
        {
            var objRepository = new RepositorySparePart();

            EntitySparePart data = new EntitySparePart()
            {
                PK_SparePartsID = refa.PK_SparePartsID,
                FK_OrderID = refa.FK_OrderID,
                FK_BuildOfMaterialsID = refa.FK_BuildOfMaterialsID,
                FK_ProductID = refa.FK_ProductID,
                RefManID = RefManID,
                FK_WorkforceID = refa.FK_WorkforceID,
                ItemRefMan = DescripcionRefMan,
                Price = Precio,
                Quantity = (int)Cantidad,
                Status = refa.Status,
                PosicionItem = PosicionItem.TrimStart('0'),
                SparePartStatus = EstatusRefMan,
                StatusSchema = EstatusEsquema,
                CreateDate = refa.CreateDate,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Update(data);
        }

        public void Update(EntitySparePart refa)
        {
            var objRepository = new RepositorySparePart();
            objRepository.Update(refa);
        }

        public List<EntitySparePart> GetAll()
        {
            return new RepositorySparePart().GetAll().Select(p => new EntitySparePart()
            {
                PK_SparePartsID = p.PK_SparePartsID,
                FK_OrderID = p.FK_OrderID,
                FK_BuildOfMaterialsID = p.FK_BuildOfMaterialsID,
                FK_ProductID = p.FK_ProductID,
                FK_WorkforceID = p.FK_WorkforceID,
                RefManID = p.RefManID,
                ItemRefMan = p.ItemRefMan,
                Quantity = p.Quantity,
                Price = p.Price,
                Status = p.Status,
                PosicionItem = p.PosicionItem,
                SparePartStatus = p.SparePartStatus,
                StatusSchema = p.StatusSchema,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntitySparePart>();
        }

        public List<EntitySparePart> GetAllByOrderID(int OrderID)
        {
            return new RepositorySparePart().GetAllByOrderID(OrderID);
        }


        public void Delete(List<EntitySparePart> data)
        {
           new RepositorySparePart().Delete(data);
        }
        public EntitySparePart Get(int SparePartsID)
        {
            return new RepositorySparePart().Get(SparePartsID);
        }

        public List<EntitySparePart> Get(int OrderID, string SparePartsID)
        {
            return new RepositorySparePart().Get(OrderID, SparePartsID);
        }

        public List<ModelViewSparePartsODS> GetByOrderID(int OrderID)
        {

            var x = new RepositorySparePart().GetByPK_OrderID(OrderID);
            return new RepositorySparePart().GetByPK_OrderID(OrderID).Select(a =>
                   new ModelViewSparePartsODS
                   {
                       RefManID = a.RefManID,
                       SparePartDescription = a.ItemRefMan,
                       Quantity = a.Quantity,
                       Price = a.FK_BuildOfMaterialsID != null ?( new BusinessPrice().GetPrice(a.FK_BuildOfMaterialsID.Value, a.FK_ProductID.Value) != null ? new BusinessPrice().GetPrice(a.FK_BuildOfMaterialsID.Value, a.FK_ProductID.Value).Price.Value : 0) : a.Price.Value,
                       PosicionItem = a.PosicionItem,
                       SparePartStatus = a.SparePartStatus,
                       StatusSchema = a.StatusSchema,
                       StatusDescription = a.SparePartStatus != null ? new RepositoryStatusScheme().GetStatusScheme(a.SparePartStatus, a.StatusSchema).Description : ""
                    }).ToList();
        }
        
        public List<EntitySparePart> GetListByRefManID(int OrderID, string RefManID)
        {
            return new RepositorySparePart().GetListByRefManID(OrderID, RefManID);
        }

        public EntitySparePart GetByRefManID(int OrderID, string RefManID)
        {
            return new RepositorySparePart().GetByRefManID(OrderID, RefManID);
        }

        public EntitySparePart GetByRefManID(int OrderID, string RefManID, string Estatus)
        {
            return new RepositorySparePart().GetByRefManID(OrderID, RefManID, Estatus);
        }

        public List<EntitySparePart> GetByOrder(string OrderID)
        {
            return new RepositorySparePart().GetByOrder(OrderID);
        }

        public  void UpdateSparePartsODS(List<SparePart> SpareParts)
        {
            foreach (var itemSP in SpareParts)
            {
                //var dataSpareParts = new BusinessSparePart().GetByRefManID(dataODS.PK_OrderID, itemSP.SparePartsCode);
                //if (dataSpareParts != null)
                //{
                //    dataSpareParts.Status = itemSP.SparePartsStatus == "1";
                //    dataSpareParts.Quantity = itemSP.SparePartsQuantity;
                //    dataSpareParts.Price = itemSP.SparePartsPrice;
                //    dataSpareParts.SparePartStatus = itemSP.SparePartsStatus;
                //    new BusinessSparePart().Update(dataSpareParts);
                //}
                //else
                //{
                //    new BusinessSparePart().Insert(dataODS.PK_OrderID, itemSP.BuildOfMaterialsID, dataInstallBase.FK_ProductID, null, itemSP.SparePartsQuantity, "0", itemSP.SparePartsStatus, "ZMX00003", itemSP.SparePartsDescription, itemSP.SparePartsCode, float.Parse(itemSP.SparePartsPrice.ToString()));
                //}
            }

            try
            {
                // actualización de refacciones 
                List<srUpdateRefMan.DT_ActRefMan_OutItem> itemsRefMan = new List<srUpdateRefMan.DT_ActRefMan_OutItem>();
                srUpdateRefMan.DT_ActRefMan_OutItem itemRefManHeader = new srUpdateRefMan.DT_ActRefMan_OutItem();
                List<srUpdateRefMan.DT_ActRefMan_OutItemRefMan> itemRefManDetail = new List<srUpdateRefMan.DT_ActRefMan_OutItemRefMan>();


                //var dataSparePart = new BusinessSparePart().GetByOrder(ods);

                //foreach (var itemSP in dataSparePart)
                //{
                //    itemRefManDetail.Add(new srUpdateRefMan.DT_ActRefMan_OutItemRefMan()
                //    {
                //        Cantidad = itemSP.Quantity.ToString(),
                //        Item_Ref_Man = itemSP.RefManID,
                //        NumeroPosicion = itemSP.PosicionItem == "0" ? "" : itemSP.PosicionItem,
                //        EstatusItem_RefMan = itemSP.SparePartStatus,
                //        MotivoRechazo = itemSP.SparePartStatus == "E0003" ? "04" : "",
                //        NumeroFactura = "",
                //        Proveedor = ""
                //    });
                //}

                //itemRefManHeader.IDOrden = dataODS.OrderID;
                itemRefManHeader.RefMan = itemRefManDetail.ToArray();
                itemsRefMan.Add(itemRefManHeader);

                new BusinessMabe().UpdateRefMan(itemsRefMan);
            }
            catch (Exception ex)
            {
                new BusinessLogger().WriteError(ex, "UpdateRefMan");
            }
           
        }
    }
}
