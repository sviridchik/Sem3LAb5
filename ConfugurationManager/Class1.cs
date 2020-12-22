﻿using System;
using System.Reflection.Emit;
using System.Reflection;
using System.Security.Permissions;
using System.Collections.Generic;
 using System.IO;
 using System.Runtime.CompilerServices;
 using System.Threading;

 namespace ConfugurationManager
{
   public struct Fields
    {
        public Type typeOfField;
        public string nameOfField;
        public object val;
    }

    public static class CreateClass
    {
       // Dictionary<string,object> storage = new Dictionary<string,object>(); 

       //  name of the default AppDomain.
     

       
      
        static AssemblyName myAssemblyName = new AssemblyName();
        
        static TypeBuilder myTypeBuilder;
        // Get the current application domain for the current thread.
        static AppDomain myCurrentDomain;
        static AssemblyBuilder myAssemblyBuilder;

        public static ModuleBuilder myModuleBuilder;
        // Define a dynamic assembly in the current application domain.


        static CreateClass()
        {
            myCurrentDomain = AppDomain.CurrentDomain;
            myAssemblyName.Name = "MyAssembly";
            // Define a dynamic assembly in the current application domain.
            myAssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(myAssemblyName, AssemblyBuilderAccess.Run);
            // Define a dynamic module in this assembly.
             myModuleBuilder = myAssemblyBuilder.DefineDynamicModule("MyModule");
        }

        //public CreateClass(string NameOfClass, Type typeOfField, string nameOfField, object val)
        //{
        //}

        public static Type CreateInstance(string NameOfClass,List<Fields>fields)
        {
         
        
            // Define a runtime class with specified name and attributes.
            myTypeBuilder = myModuleBuilder.DefineType(NameOfClass,TypeAttributes.Public);
            
            
            //fulfill with fields
            foreach (var field in fields)
            {
                myTypeBuilder.DefineField(field.nameOfField, field.typeOfField,
                    FieldAttributes.Public | FieldAttributes.Static );
            }

            var instance = myTypeBuilder.CreateType();
            object val = null;
            foreach (var field in instance.GetFields())
            {
                val = null;
                foreach (var f in fields)
                {
                    if (f.nameOfField == field.Name)
                    {
                        val = f.val;
                        break;
                    }
                    
                }
                field.SetValue(null,val );
            }

            return instance;
            // storage[nameOfField]=val;
        }

        public static void Print(Type o)
        {
            Console.WriteLine(o);
            foreach (var v in o.GetFields())
            {
                Console.WriteLine($"{v} :  {v.GetValue(o)}");
            }
            
        }
    }
}