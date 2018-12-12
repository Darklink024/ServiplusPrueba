using net.paxialabs.mabe.serviplus.security.ManagerExceptions.Commons;
using System;
using System.Collections.Generic;

namespace net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions
{
    internal sealed class ParseToMasterException
    {
        public static Dictionary<string, string> GetErroCodeAndErrorGUI(Exception ex)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            switch (ex.GetType().Name)
            {
                case "UnauthorizedAccess_Exception":
                    dic.Add("ErroCode", ((int)(ex as UnauthorizedAccess_Exception).ErrorCodeException).ToString());
                    dic.Add("ErrorDescription", (ex as UnauthorizedAccess_Exception).Message.ToString());
                    dic.Add("ErrorGUI", "");
                    break;
                case "CustomExceptions":
                    dic.Add("ErroCode", ((int)(ex as MasterException<CustomExceptions.ErrorCodes>).ErrorCode).ToString());
                    dic.Add("ErrorDescription", (ex as MasterException<CustomExceptions.ErrorCodes>).ErrorDescription.ToString());
                    dic.Add("ErrorGUI", (ex as MasterException<CustomExceptions.ErrorCodes>).ErrorGUI.ToString());
                    break;
                
                //TODO: Se agregara un case por cada nuevo tipo de excepcion que se genere para obtener el codigo de error y el GUI identificador

                default: throw new Exception("Error al obtener el código de error porque no está definido el tipo de excepción que se está intentando manejar.");
            }

            return dic;
        }
    }
}
