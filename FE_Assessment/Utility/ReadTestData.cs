using System.Xml;
using static FE_Assessment.Globals;

namespace FE_Assessment.Utility
{
    public class ReadTestData
    {
        public static string? Value { get; set; }

        public static string GetTestData(string testcase_id, string node_element="")
        {
            /*
             * Use this method to read testdata for respective testcase
             * Pass the testcase-id and element name for which user need a data. 
             * Ex: Node Element = "TestData" , Node Element = "Username"
             */
            Value = "";
            try
            {
                testcase_id = testcase_id.Split("_")[0];
                
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(_resourceDir + "TestData.xml");
                XmlNodeList? nodeList = xmldoc.SelectNodes("TestCases/TestCase");

                foreach (XmlNode node in nodeList)
                {
                    string? testcase = node["TestCaseId"]?.InnerText;

                    if (testcase != null && testcase.Contains(testcase_id))
                    {
                        Value = Newtonsoft.Json.JsonConvert.SerializeXmlNode(node);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Null Reference error occured in ReadTestData Method");
            }
            catch (Exception e1)
            {
                throw new Exception("Error occured in ReadTest Data Method, please check parameteres name");
            }

            return Value;
        }

        //Better way to read all data at once into a JObject
        //public static void ReadAllData()
        //{

        //    _testData = (JObject)(_testType.ToLower().Contains("smoke")
        //       ? ReadData("Smoke.json")
        //       : ReadData("Regression.json"));
        //    _userData = (JObject)ReadData("Users.json");
        //}

        ////Read a file
        //public static JToken ReadData(string fileName)
        //{
        //    try
        //    {
        //        JToken data = JToken.Parse(File.ReadAllText(_dataDir + _seperator + fileName));
        //        Log.Information("Successfully read data file = " + fileName);
        //        return data;
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error("Error readng data = " + e.Message);
        //        return null;
        //    }
        //}
    }
}
