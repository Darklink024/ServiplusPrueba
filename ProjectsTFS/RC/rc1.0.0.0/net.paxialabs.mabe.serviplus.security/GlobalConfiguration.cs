using System;
using System.Configuration;

namespace net.paxialabs.mabe.serviplus.security
{
    public static class GlobalConfiguration
    {
        #region TwilioAPI
        public static string TwilioAccountSid
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["TwilioAccountSid"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la key TwilioAccountSid en el archivo de configuración.");
                }
            }
        }

        public static string TwilioAuthToken
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["TwilioAuthToken"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la key TwilioAuthToken en el archivo de configuración.");
                }
            }
        }

        public static string TwilioFromNumber
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["TwilioFromNumber"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la key TwilioFromNumber en el archivo de configuración.");
                }
            }
        }
        #endregion

        #region GoogleMapsDistanceMatrix
        public static string GoogleMapsDistanceMatrixURL
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleMapsDistanceMatrixURL"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la key GoogleMapsDistanceMatrixURL en el archivo de configuración.");
                }
            }
        }
        public static string GoogleMapsDistanceMatrixTOKEN
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleMapsDistanceMatrixTOKEN"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la key GoogleMapsDistanceMatrixTOKEN en el archivo de configuración.");
                }
            }
        }

        public static string GoogleMatrixGeoLocation
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleMatrixGeoLocation"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la key GoogleMatrixGeoLocation en el archivo de configuración.");
                }
            }
        }
        #endregion

        #region SFTP_MABE
        public static string SFTPMabeHost
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["MabeSFTPHost"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string SFTPMabeUser
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["MabeSFTPUser"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string SFTPMabePwd
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["MabeSFTPPwd"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string MabeSFTPPwdKey
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["MabeSFTPPwdKey"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string MabeSFTPRSA
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["MabeSFTPRSA"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string MabeSFTPFolderRemote
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["MabeSFTPFolderRemote"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string MabeSFTPFolderLocal
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["MabeSFTPFolderLocal"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        public static string MabeSFTPFolderLocalProcess
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["MabeSFTPFolderLocalProcess"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }


        public static string LocateFiles
        {  get { return ConfigurationManager.AppSettings["LocateFiles"];}}
        #endregion

        #region MailConfiguration
        public static string StringMailDestiny
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["StringMailDestiny"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string StringMailDisplay
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["StringMailDisplay"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }        
        public static string StringMailUser
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["StringMailUser"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string StringMailPassword
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["StringMailPassword"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string StringMailSender
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["StringMailSender"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string StringMailPort
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["StringMailPort"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string StringMailHost
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["StringMailHost"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        public static string MailMTA
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["MailMTA"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string exchangeURL
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["exchangeURL"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        public static string exchangeUser
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["exchangeUser"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        public static string exchangePwd
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["exchangePwd"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        public static string exchangeUserCotiza
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["exchangeUser"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        public static string exchangePwdCotiza
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["exchangePwd"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        #endregion

        #region Token
        public static string TokenWEB
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["TokenWEB"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        public static string TokenMobile
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["TokenMobile"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        public static string TokenWS
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["TokenWS"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        #endregion

        #region TokenFCM
        public static string GoogleFCMServerToken
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleFCMServerToken"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string GoogleFCMSenderIDToken
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleFCMSenderIDToken"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        public static string GoogleFCMUri
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleFCMUri"];
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        #endregion

        #region MabeWSEndpoint
        public static string endPointInvoiceODS
        { get { return ConfigurationManager.AppSettings["endPointInvoiceODS"]; } }
        public static string endPointUpdateODS
        { get { return ConfigurationManager.AppSettings["endPointUpdateODS"]; } }
        public static string endPointUpdateBase
        { get { return ConfigurationManager.AppSettings["endPointUpdateBase"]; } }
        public static string endPointHistoryBase
        { get { return ConfigurationManager.AppSettings["endPointHistoryBase"]; } }
        public static string endPointHistoryODS
        { get { return ConfigurationManager.AppSettings["endPointHistoryODS"]; } }
        public static string endPointUpdateRefMan
        { get { return ConfigurationManager.AppSettings["endPointUpdateRefMan"]; } }
        public static string endPointModuleReserveSP
        { get { return ConfigurationManager.AppSettings["endPointModuleReserveSP"]; } }
        public static string endPointADRReserveSP
        { get { return ConfigurationManager.AppSettings["endPointADRReserveSP"]; } }
        public static string endPointUser
        { get { return ConfigurationManager.AppSettings["endPointUser"]; } }
        
        public static string endPointPwd
        { get { return ConfigurationManager.AppSettings["endPointPwd"]; } }
        public static string endPointReprogramingODS
        { get { return ConfigurationManager.AppSettings["endPointReprogramingODS"]; } }
        public static string endPointOrdenVentaOut
        { get { return ConfigurationManager.AppSettings["endPointOrdenVentaOut"]; } }
        

        #endregion

        public static string StringConnectionDB
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["MasterConnection"].ConnectionString;
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }
        
        public static string ProviderDB
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["MasterConnection"].ProviderName;
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido el proveedor para la base de datos en el archivo de configuración.");
                }
            }
        }

        public static string CryptoKey { get { return "uzumaki"; } }

        public static string LocateBodyMail
        { get { return ConfigurationManager.AppSettings["LocateBodyMail"]; } }

        public static string MabeAttachmentsLocal
        { get { return ConfigurationManager.AppSettings["MabeAttachmentsLocal"]; } }

        public static string urlRequest
        { get { return ConfigurationManager.AppSettings["urlRequest"]; } }

        public static string LocateEvidence
        { get { return ConfigurationManager.AppSettings["locateEvidence"]; } }

        public static string LocateContent
        { get { return ConfigurationManager.AppSettings["LocateContent"]; } }

        public static string LocateEvidenceRelative
        { get { return ConfigurationManager.AppSettings["locateEvidenceRelative"]; } }

        public static string GetLocateNotificationRelative()
        { return ConfigurationManager.AppSettings["locateNotificationRelative"]; }

        public static string GetZ_EntityFramework_Extensions_LicenseName()
        { return ConfigurationManager.AppSettings["Z_EntityFramework_Extensions_LicenseName"]; }

        public static string GetZ_EntityFramework_Extensions_LicenseKey()
        { return ConfigurationManager.AppSettings["Z_EntityFramework_Extensions_LicenseKey"]; }
    }
}
