using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace WebAppEnterprise
{
    public partial class PurchaseOfMaterials
    {
        public int Id { get; set; }
        public byte Material { get; set; }
        public double Quantity { get; set; }
        public decimal? Amount { get; set; }
        public DateTime Date { get; set; }

        public virtual RawMaterials MaterialNavigation { get; set; }

        public bool getPermission()
        {
            string connection_string = "Data Source=DESKTOP-K0V0O3J\\SQLEXPRESS;Initial Catalog=Enterprise;Integrated Security=SSPI;User ID = sa; Password =;";
            SqlConnection connection = new SqlConnection(connection_string);
            SqlCommand cmd = new SqlCommand("sp_purchase_of_materials", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@amount", Amount);            
            SqlParameter result = new SqlParameter
            {
                ParameterName = "@result",
                SqlDbType = System.Data.SqlDbType.Bit
            };
            connection.Open();
            result.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(result);
            cmd.ExecuteNonQuery();
            connection.Close();

            if (Convert.ToInt32(cmd.Parameters["@result"].Value) == 1)
            {
                return true;
            }
            else return false;
        }
    }
}
