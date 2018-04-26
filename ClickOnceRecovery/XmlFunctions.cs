using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClickOnceRecovery
{

    public class XmlFunctions
    {

        public static string ToXMLString(object item)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(item.GetType());
            serializer.Serialize(stringwriter, item);
            return stringwriter.ToString();
        }



        public static object ObjectFromXMLString(Type typeOfObject, string xmlFile)
        {
            try
            {
                if (String.IsNullOrEmpty(xmlFile))
                    return Activator.CreateInstance(typeOfObject);

                XmlSerializer serializer = new XmlSerializer(typeOfObject);
                StringReader rdr = new StringReader(xmlFile);
                return serializer.Deserialize(rdr);
            }
            catch
            {
                //Something went wrong.  
                //The parent method will need to be able to handle a null object return
            }

            return null;


        }
    }
}
