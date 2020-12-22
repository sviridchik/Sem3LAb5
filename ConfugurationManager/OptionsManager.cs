using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ConfugurationManager
{
    public class OptionsManager

    {
        public static DataAccessLayerClass DataAccess { get; set; }
        public static string WorkWithAppD(string pathSearch)
        {
           
            string callingDomainName = Thread.GetDomain().FriendlyName;
            // full name of the EXE assembly.
            string exeAssembly = Assembly.GetEntryAssembly().FullName;
       
            DirectoryInfo source = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var elem in source.GetFiles())
            {
                if (elem.Name.Contains(pathSearch))
                {
                    return Path.Combine(source.FullName, pathSearch);
                }
            }
            return null;
        }
        
        static string pathJ = @"DataJson.json";
        [pathJ("scratch.json")] 
       // static string res = WorkWithAppD(pathJ);
       
        public static List<Type>  GetlistOfInstances()
       {
            //var mem = OptionsManager.GetMember
            //string pathJ=pathJAttribute.GetCustomAttribute(typeof(string));
           
          // string pathJ = (GetMyAttr<pathJAttribute>(typeof(OptionsManager)) as pathJAttribute).path;
           //static string pathJ = 
          
           string jsonData = JParser.GetDataFromJson(pathJ);
            List<NameAndFields> nameFields = JParser.GetProccesedData(jsonData);
            List<Type>  listOfInstances = JParser.ProssedClass(nameFields);
            return listOfInstances;
        }
        static List<Type> etlJsonOptions = GetlistOfInstances();
        
        public static Type GetOptions<T>()
        {
            foreach (var VARIABLE in etlJsonOptions)
            {
                
                StringBuilder sb = new StringBuilder("ConfugurationManager.");

                if (sb.AppendFormat(VARIABLE.FullName.Replace("\"", "")).ToString() == typeof(T).ToString())
                {
                    return VARIABLE;
                }
            }

            return null;
        }



        public static Attribute GetMyAttr<T>(object o) 
        {
            foreach (var mem in o.GetType().GetMembers())
            {
                foreach (var possibleGoal in mem.GetCustomAttributes(false)) //no inherit
                {
                    if (possibleGoal is Attribute)
                    {
                        return (Attribute) possibleGoal;
                    }
                }
            }

            return null;
        }
    }
}