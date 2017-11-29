using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using ClientEditAPI.Models;

namespace ClientEditAPI.Controllers
{

    public class ClientController : ApiController
    {

        #region variables
        public static Client client;
        #endregion


        // GET: api/Client
        [EnableCors("*", "*", "*")]
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Client")]
        public IHttpActionResult Get()
        {
            string Error = string.Empty;
            if (!Initialize(ref Error))
            {
                throw new Exception("XML file with the data not found");
            }
            return Json<Client>(client);
        }

        // GET: api/Client/5

        [HttpPost]
        [Route("api/Client")]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage SaveChanges([FromBody]Client client)
        {

            if (!UpDateClient(client))
            {
                throw new Exception("Data not Updated");
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Location = new Uri("http://localhost:52291/api/Client");
            return response;
        }

        static bool UpDateClient(Client client)
        {
            try
            {
                //XmlDocument xml = new XmlDocument();
                //xml.SelectSingleNode("//Client/FirstName").InnerText = client.FirstName;
                //xml.SelectSingleNode("//Client/Surname").InnerText = client.Surname;
                //xml.SelectSingleNode("//Client/IdentityType").InnerText = client.IdentityType;
                //xml.SelectSingleNode("//Client/IdentityNumber").InnerText = client.IdentityNumber;
                //xml.SelectSingleNode("//Client/DOB").InnerText = client.DOB;
                //xml.Save(Vars.SettingsPath);

                XmlDocument XMLdoc = new XmlDocument();
                Vars.SettingsPath = Settings.SetSettingsPath();
                XMLdoc.Load(Vars.SettingsPath);

                XmlNodeList aNodes = XMLdoc.SelectNodes("settings/Clients");
                foreach (XmlNode node in aNodes)
                {
                    XmlNode Xn = node.SelectSingleNode("Client");
                    //XmlNode child2 = node.SelectSingleNode("Node2");

                    Xn.Attributes["FirstName"].InnerXml = client.FirstName;
                    Xn.Attributes["Surname"].InnerXml = client.Surname;
                    Xn.Attributes["IdentityType"].InnerXml = client.IdentityType;
                    Xn.Attributes["IdentityNumber"].InnerXml = client.IdentityNumber;
                    Xn.Attributes["DOB"].InnerXml = client.DOB;
                    //child2.InnerText = "Value2";
                }
                //foreach (XmlNode Xn in Settings.XMLdoc.SelectNodes("settings/Clients/Client"))
                //{
                //    Xn.Attributes["FirstName"].InnerXml = client.FirstName;
                //    Xn.Attributes["Surname"].InnerXml = client.Surname;
                //    Xn.Attributes["IdentityType"].InnerXml = client.IdentityType;
                //    Xn.Attributes["IdentityNumber"].InnerXml = client.IdentityNumber;
                //    Xn.Attributes["DOB"].InnerXml = client.DOB;
                //}
                // File.Delete(Vars.SettingsPath);
                XMLdoc.Save(Vars.SettingsPath);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
            return true;
        }

        static bool Initialize(ref string Error)
        {
            try
            {
                Settings.ReadSettings();
                foreach (XmlNode Xn in Settings.XMLdoc.SelectNodes("settings/Clients/Client"))
                {
                    client = new Client()
                    {
                        FirstName = Xn.Attributes["FirstName"].Value.ToString(),
                        Surname = Xn.Attributes["Surname"].Value.ToString(),
                        IdentityType = Xn.Attributes["IdentityType"].Value.ToString(),
                        IdentityNumber = Xn.Attributes["IdentityNumber"].Value.ToString(),
                        DOB = Xn.Attributes["DOB"].Value.ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message.ToString();
                return false;
            }
            return true;
        }

    }

    public class Vars
    {
        public static string SettingsPath;
    }

}
