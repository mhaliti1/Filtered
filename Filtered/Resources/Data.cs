using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Filtered.Resources
{
    public static class Constants
    {
        public const string Connect = "Data Source=servername;Initial Catalog=database;Integrated Security=True;";
    }


    public class dbConnect
    {

        private SqlConnection connect()
        {
            SqlConnection cnn = new SqlConnection(Constants.Connect);
            return cnn;
        }
        private DataTable execDT(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlDataReader rd = cmd.ExecuteReader();
            dt.Load(rd);

            return dt;

        }

        public DataTable GetData(string query, long Value = 0)
        {

            using (SqlConnection cnn = connect())
            {
                using (SqlCommand cmd = new SqlCommand(@"storeprocedure", cnn))
                {
                    cnn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.Add("@query", SqlDbType.VarChar).Value = query;
                    cmd.Parameters.Add("@Value", SqlDbType.VarChar).Value = Value;

                    DataTable dt = execDT(cmd);

                    cnn.Close();
                    return dt;


                }
            }


        }



        //public void BulkInsertLoans(DataTable DT)
        //{

        //    try
        //    {
        //        using (SqlConnection cnn = connect())
        //        {
        //            cnn.Open();

        //            SqlBulkCopy bulkCopy = new SqlBulkCopy
        //            (
        //            cnn,
        //            SqlBulkCopyOptions.TableLock |
        //            SqlBulkCopyOptions.FireTriggers |
        //            SqlBulkCopyOptions.UseInternalTransaction,



        //            null
        //            );

        //            bulkCopy.DestinationTableName = @"[schema].[TableName]";

        //            bulkCopy.WriteToServer(DT);
        //            cnn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }




        //}

        //public void UploadFiles(string sourcePath, int RID, int TID)
        //{
        //    try
        //    {
        //        FileStream fs = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);

        //        string fileName = Path.GetFileName(sourcePath);

        //        BinaryReader reader = new BinaryReader(fs);
        //        int rec;
        //        byte[] BlobValue = reader.ReadBytes((int)fs.Length);

        //        fs.Close();

        //        using (SqlConnection cnn = connect())
        //        {
        //            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO table (RID, TID , Name, Data) Values(@RID, @TID, @Name, @Data)", cnn))
        //            {
        //                cnn.Open();
        //                cmd.CommandType = CommandType.Text;
        //                cmd.CommandTimeout = 300;
        //                cmd.Parameters.AddWithValue("@RID", RID);
        //                cmd.Parameters.AddWithValue("@TID", TID);
        //                cmd.Parameters.AddWithValue("@Name", fileName);
        //                cmd.Parameters.AddWithValue("@Data", BlobValue);


        //                rec = cmd.ExecuteNonQuery();


        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {


        //        MessageBox.Show(ex.Message);
        //    }

        //}

        //public int DeleteTable()
        //{
        //    int rec = 0;
        //    using (SqlConnection cnn = connect())
        //    {
        //        using (SqlCommand cmd = new SqlCommand(@"Delete From table Where NetUser=Session_User", cnn))

        //        {
        //            cnn.Open();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandTimeout = 300;
        //            SqlDataReader rd = cmd.ExecuteReader();


        //            while (rd.Read())
        //            {
        //                rec = Convert.ToInt16(rd[0].ToString());
        //            }


        //            rd.Close();
        //            cnn.Close();
        //        }
        //    }


        //    return rec;
        //}


        //public int UpdateUser(User data)
        //{
        //    int rec = 0;
        //    using (SqlConnection cnn = connect())
        //    {
        //        using (SqlCommand cmd = new SqlCommand(@"UPDATE table SET UserRole =@UserRole, IsAdmin =@IsAdmin, Color =@Color WHERE ID =@ID", cnn))
        //        {
        //            cnn.Open();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandTimeout = 300;
        //            cmd.Parameters.Add("@UserRole", SqlDbType.VarChar).Value = data.UserRole;
        //            cmd.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = data.isAdmin;
        //            cmd.Parameters.Add("@Color", SqlDbType.VarChar).Value = data.Color;
        //            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = data.ID;


        //            SqlDataReader rd = cmd.ExecuteReader();

        //            while (rd.Read())
        //            {
        //                rec = Convert.ToInt32(rd[0].ToString());
        //            }


        //            cnn.Close();


        //        }
        //    }

        //    return rec;
        //}

    }


    public class datafactory
    {
        dbConnect db = new dbConnect();


        //public User GetCurrentUser()
        //{
        //    DataTable dt = new DataTable();
        //    dt = db.GetData("User");

        //    User _list = new User();


        //    foreach (DataRow _dataRow in dt.Rows)
        //    {

        //        _list.NetUser = _dataRow["NetUser"].ToString();
        //        _list.Name = _dataRow["DispName"].ToString();
        //        _list.IsManager = Convert.ToBoolean(_dataRow["Mgrdes"].ToString());
        //        _list.CanApprove = Convert.ToBoolean(_dataRow["CanApprove"].ToString());
        //        _list.CanModify = Convert.ToBoolean(_dataRow["CanMod"].ToString());

        //    }

        //    return _list;
        //}

        //public List<Letter> GetLetters()
        //{
        //    DataTable dt = new DataTable();
        //    dt = db.GetData("Letters");

        //    List<Letter> _list = new List<Letter>();


        //    foreach (DataRow _dataRow in dt.Rows)
        //    {
        //        _list.Add(new Letter
        //        {


        //            Id = Convert.ToInt64(_dataRow["Id"].ToString()),
        //            LetterID = _dataRow["LetterID"].ToString(),
        //            Brand = _dataRow["Brand"].ToString(),
        //            Desc = _dataRow["LetterDesc"].ToString(),
        //            Step = _dataRow["Step"].ToString(),
        //            Vendor = _dataRow["Vendor"].ToString(),



        //        });
        //    }

        //    return new List<Letter>(_list);
        //}
    }
}
