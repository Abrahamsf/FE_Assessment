using AventStack.ExtentReports;
using Newtonsoft.Json.Linq;
using System.Configuration;
using static FE_Assessment.Globals;


namespace FE_Assessment.Utility
{
    public class GetEnvironementData
    {
        public static string GetEnvData(ExtentTest node, string data = "WebURL")
        {
            /*
             * Use this method to get URL for the data based on enviornment
             * parameter data : Value you want to fetch from Resources/Enviornment.json file
             */

            string url = "";
            
            try
            {
                string? env = ConfigurationManager.AppSettings["Environment"];
                string? jsonstring = File.ReadAllText(_resourceDir + "Environment.json");
                var json = JToken.Parse(jsonstring);
                Object? o = json?.SelectToken(env)?.Value<object>(data);
                url = o?.ToString();
                node.Log(Status.Pass, "Data Read successfully");
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Null Reference error occured in ReadTestData Method");
            }
            catch (Exception e)
            {
                throw new Exception("Error occured in the GetEnvironment Data method, please check the parameters name");
            }

            return url;
        }
    }
}
