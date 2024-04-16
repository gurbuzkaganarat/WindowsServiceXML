using ServiceReference1;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using System.Xml.Serialization;
using WindowsServiceXML.Entity;

namespace WindowsServiceXML
{
    public partial class Service1 : ServiceBase
    {

        Context.Contextt db = new Context.Contextt();
        public System.Timers.Timer timer;

       



        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            writeLog("Service Start" + DateTime.Now);

            this.timer = new System.Timers.Timer(10000);  // 30000 milliseconds = 30 seconds
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnElapsedTime);
            this.timer.Start();



        }

        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
            writeLog("ServiceStop" + DateTime.Now);
        }

        private void OnElapsedTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            VeriÇekAsync();

            writeLog("Servis Already Running" + DateTime.Now);
        }

        public  void writeLog(string Message)
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "/Logs";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            string txtpath = AppDomain.CurrentDomain.BaseDirectory + "/Logs/text.txt";
            if (!File.Exists(txtpath))
            {
                using (StreamWriter writer = new StreamWriter(txtpath))
                {

                    writer.WriteLine(Message);

                 }

            }
            
        }


        public async void  VeriÇekAsync()
        {
            var client = new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

            var result = await client.FullCountryInfoAllCountriesAsync();

            var tCountryInfos = result.Body.FullCountryInfoAllCountriesResult;

            CountryInfo countryInfo = new CountryInfo();


            var xmlSerializer = new XmlSerializer(typeof(tCountryInfo[]));
            var writer = new StringWriter();
            xmlSerializer.Serialize(writer, tCountryInfos);
            var xmlcontent = writer.ToString();

            StringReader reader = new StringReader(xmlcontent);
            var tCountries = (tCountryInfo[])xmlSerializer.Deserialize(reader);

            foreach (var item in tCountries)
            {
                var isoCode = item.sISOCode;
                var g = db.CountryInfo.Find(isoCode);

                if (g== null)
                {
                    countryInfo.sISOCode = item.sISOCode;
                    countryInfo.sName = item.sName;
                    countryInfo.sCapitalCity = item.sCapitalCity;
                    countryInfo.sPhoneCode = item.sPhoneCode;
                    countryInfo.sContinentCode = item.sContinentCode;
                    countryInfo.sCurrencyISOCode = item.sCurrencyISOCode;
                    countryInfo.sCountryFlag = item.sCountryFlag;
                    db.CountryInfo.Add(countryInfo);
                }
                else
                {
                    g.sName = item.sName;
                    g.sCapitalCity = item.sCapitalCity;
                    g.sPhoneCode = item.sPhoneCode;
                    g.sContinentCode = item.sContinentCode;
                    g.sCurrencyISOCode = item.sCurrencyISOCode;
                    g.sCountryFlag = item.sCountryFlag;
                }


                
                
                db.SaveChanges();
            }

        }
    }
}
