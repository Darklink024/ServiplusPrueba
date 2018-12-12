using net.paxialabs.mabe.serviplus.domain.Facade.Interface;
using net.paxialabs.mabe.serviplus.web.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.@interface
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime fh = DateTime.Today.AddDays(-1); // considerar un dia atras para enviar ods resagadas si es manual se toma la fecha que se envia
            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["AppCulture"]);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            int maxProcess = Convert.ToInt32(ConfigurationManager.AppSettings["MaxProcess"]);

            Console.WriteLine("Iniciando interface Serviplus " + DateTime.Now.ToString());

            Console.WriteLine("Parametros ");

            foreach (var item in args)
            {
                Console.WriteLine(item);
            }

            string DownloadFolder = "";
            List<string> arrFiles = new List<string>();
            List<string> arrFilesOK = new List<string>();

            if (args.Contains("-download"))
            {
                Console.WriteLine("Iniciando proceso de descarga " + DateTime.Now.ToString());
                FacadeInterface.Download(args.Contains("-removeOrigin"), out arrFiles, out arrFilesOK, out DownloadFolder);
                Console.WriteLine(String.Format("Archivos descargados: {0}", arrFiles.Count()));
                Console.WriteLine(String.Format("Archivos descargados OK: {0}", arrFilesOK.Count()));
                Console.WriteLine("Completado proceso de descarga " + DateTime.Now.ToString());
            }

            if (args.Contains("-path"))
            {
                foreach (var item in args)
                {
                    if (item.Contains(@"\"))
                    {
                        DownloadFolder = item.Replace('"', ' ').Trim();
                        break;
                    }
                }
            }

            if (args.Contains("-processOld"))
            {
                Console.WriteLine("Iniciando proceso de carga " + DateTime.Now.ToString());
                FacadeInterface.Process(DownloadFolder);
                Console.WriteLine("Completado proceso de carga " + DateTime.Now.ToString());
            }

            if (args.Contains("-process"))
            {
                Console.WriteLine("Iniciando proceso de carga " + DateTime.Now.ToString());
                FacadeInterface.Process();
                Console.WriteLine("Completado proceso de carga " + DateTime.Now.ToString());
            }

            if (args.Contains("-import"))
            {
                Console.WriteLine("Iniciando proceso de importación de ODS " + DateTime.Now.ToString());
                FacadeInterface.Import(DownloadFolder);
                Console.WriteLine("Completado proceso de importación de ODS " + DateTime.Now.ToString());
            }

            if (args.Contains("-geo"))
            {
                Console.WriteLine("Iniciando proceso de geolocalización " + DateTime.Now.ToString());
                FacadeInterface.Geolocation();
                Console.WriteLine("Completado proceso de geolocalización " + DateTime.Now.ToString());
            }

            if (args.Contains("-adds"))
            {
                Console.WriteLine("Iniciando proceso de ws SAP " + DateTime.Now.ToString());
                FacadeInterface.GetWSAdds();
                Console.WriteLine("Completado proceso de ws SAP " + DateTime.Now.ToString());
            }

            if(args.Contains("-date"))
            {
                System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$");
                foreach (var item in args)
                {
                    if (rgx.IsMatch(item))
                    {
                        fh = Convert.ToDateTime(item);
                        break;
                    }
                }
            }

            if (args.Contains("-sendCRM"))
            {                
                FacadeMabe.SendCRM (fh, maxProcess, args.Contains("-reintent"), args.Contains("-extraKM"));
            }
                       

            if (args.Contains("-debug"))
            {
                Console.WriteLine("Presione cualquier tecla para terminar. ");
                Console.ReadKey();
            }
            
            
        }        
    }
}
