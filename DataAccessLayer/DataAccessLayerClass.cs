using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using ClassLibraryLab3;

namespace DataAccessLayer
{

    public struct DataEntity
    {
        public List<string> names;
        public List<object[]> values;
    }


    public struct AdressKnot
    {
        int businessEID;
        int addressid;
        int addressTypeid;
        string city;
        string name;
        string addressLine1;
    }

    public class DataAccessLayerClass
    {
        string ser;
        string db;
        bool con;

        public DataAccessLayerClass(string ser, string db, bool con)
        {
            this.ser = ser;
            this.db = db;
            this.con = con;
        }

        SqlConnection connection;

        public async Task StartDbAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // "Data Source=USER-PC\SQLEXPRESS;Initial Catalog=AdventureWorks2012;Integrated Security=True"
            //string connectionString = $"Server={ser}; Database={db}; Trusted_Connection ={con}";
            string connectionString = $"Data Source={ser.Replace("\"", "").Replace(@"\\", @"\")}; Initial Catalog={db.Replace("\"", "").Replace(@"\\", @"\")}; Integrated Security ={con}";
            connection = new SqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);
        }


        public async Task<DataEntity> GetAddAsync(string nameOfProc, CancellationToken cancellationToken = new CancellationToken(), params SqlParameter[] sqlParameters)
        {
            DataEntity dataEntity = new DataEntity();
            SqlCommand command = new SqlCommand(nameOfProc, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddRange(sqlParameters);
            SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
            int colCount = reader.FieldCount;
            List<object[]> table = new List<object[]>();
            while (await reader.ReadAsync(cancellationToken))
            {
                if (cancellationToken.IsCancellationRequested)
                    return new DataEntity();
                table.Add(new object[colCount]);
                reader.GetValues(table.Last());

            }            
            dataEntity.values = table;
            dataEntity.names = new List<string>();
            for (int i = 0; i < colCount; i++)
            {
                dataEntity.names.Add(reader.GetName(i));
            }
            await reader.CloseAsync();
            return dataEntity;
        }

    }
}


/*
 * Person.PhoneNumberType
 * 
 SELECT TOP (1000) [PhoneNumberTypeID]
      ,[Name]
      ,[ModifiedDate]
  FROM [AdventureWorks2017].[Person].[PhoneNumberType]
 
 */
