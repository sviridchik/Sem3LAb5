using System;
using System.IO;
using  System.Text.RegularExpressions;
using System.Collections.Generic;


namespace ConfugurationManager
{
    public struct FieldsOfNameAndFields
    {
        public Type typeOfField ; 
        public string nameOfField;
        public object val;
    }
    
    
    
    public struct NameAndFields
    {
        public string name;
       // public List<FieldsOfNameAndFields> fieldz;
       public List<Fields> fieldz;
    }


    public class JParser
    {
        private static string pathJ = @"DataJson.json";
       // [pathJ("scratch.json")]
        public static string GetDataFromJson(string pathJ)
        {
            string jsonData;
            using (StreamReader sr = new StreamReader(pathJ))
            {
                 jsonData = sr.ReadToEnd();
               // Console.Write(jsonData);
            }

            return jsonData;
        }

        //static string jsonData = GetDataFromJson(path);
        public static List<NameAndFields> GetEmpty(string jsonData)
        {
            string patternAncestor = @"""(\w*[^""{}])"": {([^}]*)} ?";
            Regex rgx = new Regex(patternAncestor);

            //Match match = rgx.Match(sentence);
            MatchCollection matches = Regex.Matches(jsonData, patternAncestor, RegexOptions.Singleline);

            string ancestor = "";
            //= Convert.ToString(match[13]);
            //List<FieldsOfNameAndFields> fieldz = new List<FieldsOfNameAndFields>();
            List<Fields> fieldz = new List<Fields>();
            List<NameAndFields> nameAndFields = new List<NameAndFields>(); 
            //elements(name list of fields)
            NameAndFields knot = new NameAndFields();
            // FieldsOfNameAndFields elem = new FieldsOfNameAndFields();
            //Fields elem = new Fields();
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    knot = new NameAndFields();

                    List<string> storage = new List<string>(match.Value.Split('{'));
                    if (storage.Count == 3)
                    {

                        ancestor = storage[0];
                        storage.RemoveAt(0);

                    }

                    storage[0] = storage[0].Replace(" ", "");
                    storage[0] = storage[0].Replace(":", "");
                    storage[0] = storage[0].Replace("\n", "");
                    storage[0] = storage[0].Replace("\r", "");
                    knot.name = storage[0];

                }

                knot.fieldz = null;
                nameAndFields.Add(knot);

            }

            return nameAndFields;
        }

        public static List<NameAndFields> GetProccesedData(string jsonData)
        {
             string patternAncestor = @"""(\w*[^""{}])"": {([^}]*)} ?";
            Regex rgx = new Regex(patternAncestor);

            //Match match = rgx.Match(sentence);
             MatchCollection matches = Regex.Matches(jsonData, patternAncestor, RegexOptions.Singleline);

            string ancestor = ""; 
            //= Convert.ToString(match[13]);
            //List<FieldsOfNameAndFields> fieldz = new List<FieldsOfNameAndFields>();
            List<Fields> fieldz = new List<Fields>();
            List<NameAndFields> nameAndFields = new List<NameAndFields>(); 
            //elements(name list of fields)
            NameAndFields knot = new NameAndFields();
           // FieldsOfNameAndFields elem = new FieldsOfNameAndFields();
            Fields elem = new Fields();
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    knot = new NameAndFields();

                    List<string> storage = new List<string>(match.Value.Split('{'));
                    if (storage.Count == 3)
                    {

                        ancestor = storage[0];
                        storage.RemoveAt(0);

                    }

                    storage[0] = storage[0].Replace(" ", "");
                    storage[0] = storage[0].Replace(":", "");
                    storage[0] = storage[0].Replace("\n", "");
                    storage[0] = storage[0].Replace("\r", "");
                    knot.name = storage[0];
                    List<string> storageFields = new List<string>(storage[1].Split('\n'));
                    storageFields.RemoveAt(0);
                    int index = storageFields.Count - 1;
                    storageFields.RemoveAt(index);
                    //fieldz = new List<FieldsOfNameAndFields>();
                    fieldz = new List<Fields>();
                    foreach (var v in storageFields)
                    {

                       // elem = new FieldsOfNameAndFields();
                        elem = new Fields();
                        elem.nameOfField = v.Split(':')[0].Replace(" ", "");
                        string tmp = v.Split(':')[1];
                        tmp = tmp.Replace(",", "");
                        tmp = tmp.Replace("\n", "");
                        tmp = tmp.Replace("\r", "");
                        tmp = tmp.Replace("\t", "");
                        // elem.val = v.Split(":")[1];
                        //elem.typeOfField=pub

                        int valParcedI;
                       
                        bool valParcedB;
                        if (int.TryParse(tmp, out valParcedI))
                        {
                            elem.val = valParcedI;
                            elem.typeOfField = typeof(int);
                        }
                        else
                        {
                            if (bool.TryParse(tmp, out valParcedB))
                            {
                                elem.val = valParcedB;
                                elem.typeOfField =  typeof(bool);
                            }
                            else
                            {
                                elem.val = tmp;
                                elem.typeOfField = typeof(string);
                            }
                        }
                        fieldz.Add(elem);
                    }

                    knot.fieldz = fieldz;
                    nameAndFields.Add(knot);
                }


            }
            else
            {
                Console.WriteLine("no matching");
            }

            return nameAndFields;
        }
        

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
    }

  
}

