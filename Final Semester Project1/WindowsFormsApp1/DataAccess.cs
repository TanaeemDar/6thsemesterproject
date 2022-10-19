using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;


namespace WindowsFormsApp1
{
    class DataAccess
    {
        public int InsertDataIntoFB( string email, string password, string name, string bd1, string bd2, string bd3, FormGender gender)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.connectionValue("dbConnectionString")))
            {
                int value = connection.Execute("Insert Into fbsignup values( '" + email + "', '" + password + "', '" + name + "', '" + bd1 + bd2 + bd3 + "' , '" + gender + "')");
                return value;
            }
            
        }

        public int InsertDataIntoLogIn(string email, string password)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.connectionValue("dbConnectionString")))
            {
                int value = connection.Execute("Insert Into fblogin values( '" + email + "', '" + password + "')");
                return value;
            }

        }

        public List<Class1> getAllsignup()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.connectionValue("dbConnectionString")))
            {
                var value = connection.Query<Class1>("select * from fbsignup").ToList();
                return value;
            }
        }
        
    }
}
