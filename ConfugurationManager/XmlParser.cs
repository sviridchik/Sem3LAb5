using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ConfugurationManager
{
    public class XmlParser
    {
        //static string path = @"/Users/victoriasviridchik/Library/Preferences/Rider2019.3/scratches/scratch.xml";
        
        //static string path = @"scratch.xml";

       //
       //[path("scratch.xml")]
        public static string GetDataFromXml(string path)
        {
            string xmlData;
            using (StreamReader sr = new StreamReader(path))
            {
                xmlData = sr.ReadToEnd();
                // Console.Write(jsonData);
                //Console.WriteLine(xmlData);
            }

            return xmlData;
        }
        
        /////////////////////
        ///
        public static List<string> GetXmlStr(int start, string ss)
            {
                int end;
                int counterLeftScoba = 0;
                //int counterRightScoba = 0;
                
                List<string> storage = new List<string>();
                
                for (int i = 38; i < ss.Length; i++)
                {
                    string ancestorXml;
                    if (i == 38)
                    {
                        while (ss[i]!='>')
                        {
                            i += 1;
                        }
                        start = i;
                    }
                   
                    
                    if (ss[i] == '<')
                    {
                        if (ss[i + 1] == '/')
                        {
                            counterLeftScoba -= 1;
                            if (counterLeftScoba == 0)
                            {
                                while (ss[i]!='>')
                                {
                                    i += 1;
                                }
                                end = i;
                                string ans = ss.Substring(start, end-start);
                                //return ans;
                                start = end ;
                                storage.Add(ans);
                            }
                        }
                        else
                        {
                            counterLeftScoba += 1;
                        }

                    }
                }

                return storage;
            }
            
            //static string xmlData = GetDataFromXml(path);
            static int indesEnd;
            //xml=ss.Replace("\n", """);
            //Console.WriteLine(ss.Substring(196,231-196));
            int indesStart = 38;
            //static List<string> storage = GetXmlStr(indesStart, ss);
 
            //changing
            public static List<NameAndFields> GetProccesedData(List<string> data)
        {
             
            string ancestor = ""; //= Convert.ToString(match[13]);
            //List<FieldsOfNameAndFields> fieldz = new List<FieldsOfNameAndFields>();
            List<Fields> fieldz = new List<Fields>();
            List<NameAndFields> nameAndFields = new List<NameAndFields>(); //elements(name list of fields)
            NameAndFields knot = new NameAndFields();
           // FieldsOfNameAndFields elem = new FieldsOfNameAndFields();
            Fields elem = new Fields();
           
                foreach (var match in data)
                {
                    knot = new NameAndFields();

                    List<string> storage = new List<string>(match.Split('<'));
                    storage.RemoveAt(0);

                    storage[0] = storage[0].Replace(" ", "");
                    storage[0] = storage[0].Replace(">", "");
                    storage[0] = storage[0].Replace("<", "");
                    storage[0] = storage[0].Replace("\n", "");
                    knot.name = storage[0];
                    
                    storage.RemoveAt(0);
                    
                    storage.RemoveAt(storage.Count-1);

                    fieldz = new List<Fields>();
                    
                    for (int i = 0; i < storage.Count; i+=2)
                    {
                        List<string> tmp = new List<string>(storage[i].Split('>'));
                        
                        storage[i] = storage[i].Replace(" ", "");
                        //storage[i] = storage[i].Replace(">", "");
                        //storage[i] = storage[i].Replace("<", "");
                        storage[i] = storage[i].Replace("\n", ""); 
                        
                        elem = new Fields();
                        elem.nameOfField = tmp[0];
                        int valParcedI;
                      
                        bool valParcedB;
                        string temp = tmp[1];
                        //elem.val = tmp[1];
                        
                        if (int.TryParse(temp, out valParcedI))
                        {
                            elem.val = valParcedI;
                            elem.typeOfField = typeof(int);
                        }
                        else
                        {
                            if (bool.TryParse(temp, out valParcedB))
                            {
                                elem.val = valParcedB;
                                elem.typeOfField =  typeof(bool);
                            }
                            else
                            {
                                elem.val = temp;
                                elem.typeOfField = typeof(string);
                            }
                        }
                        fieldz.Add(elem);
                    }
                    knot.fieldz = fieldz;
                    nameAndFields.Add(knot);
                }

            return nameAndFields;
        }
             //List<NameAndFields> nameAndFields = GetProccesedData(storage);
             

             public static List<Type> ProssedClass(List<NameAndFields> l)
        {
            List<Type>  listOfInstances = new List<Type>();
            foreach (var knot in l)
            {
                var aa = CreateClass.CreateInstance(knot.name, knot.fieldz);
                listOfInstances.Add(aa);
            }
            return listOfInstances;
        }
              //public static List<Type>  listOfInstances = XmlParser.ProssedClass(nameAndFields);
             // foreach (var varRes in listOfInstances)
             // {
                 //  Console.WriteLine($" {varRes} : \n ");
               //                 CreateClass.Print(varRes);
             // }
       // }
        

     
    }

  
}