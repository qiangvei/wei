using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wei
{
    [ComVisible(true)]
    [Guid("37684C8A-08C9-42CA-9020-9EA4D39886E4")]
    public interface IMssql
    {
        bool Con(string server, string database, string uid, string pwd);
        int SQL(string sql);
        String RS(string sql);
        string GetError();
        string Version();
    }

    [ComVisible(true)]
    [Guid("2C2C60E4-4DC0-4F6A-9D1E-4225E4C444D6")]
    public class Mssql : IMssql
    {
        public SqlConnection connection;
        private string _errors="";
        
        public String Version()
        {
            return "20191209.1.0";
        }
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="server"></param>
        /// <param name="database"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool Con(string server, string database, string uid, string pwd)
        {
            try
            {
                this.connection = new SqlConnection("Connection Lifetime=300;Connection Timeout=180;server=" + server + ";database=" + database + ";uid=" + uid + ";pwd=" + pwd);
                this.connection.Open();
                return true;
            }
            catch (SqlException e)
            {
                this.SetError(e.Message);
                return false;
            }
            catch (InvalidOperationException e)
            {
                this.SetError(e.Message);
                return false;
            }
        }
        public int SQL(string sql)
        { 
            int result = 0;
            int flag = 0;
            if (connection.State == ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    flag = 1;
                }
                catch(InvalidCastException e) { this.SetError(e.Message); }
                catch (SqlException e) { this.SetError(e.Message); }
                catch(System.IO.IOException e) { this.SetError(e.Message); }
                catch(ObjectDisposedException e) { this.SetError(e.Message); }
                catch (InvalidOperationException e) { this.SetError(e.Message); }
                finally
                {
                    cmd.Dispose();
                }
            }
            if (flag == 0 && result==0) {
                result = -1;
            }
            return result;
        }

        public String RS(string sql)
        { 
            DataTable dt = new DataTable();
            if (connection.State == ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                try
                {
                    sda.Fill(dt);
                }
                catch (InvalidOperationException e) { SetError(e.Message); }
                catch (SqlException e) { SetError(e.Message); }
                finally
                {
                    cmd.Dispose();
                }
            }
            return DataTableJson(dt);
        }

        private string DataTableJson(DataTable dt)
        {
            StringBuilder sb = new StringBuilder(); 
            try
            {
                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sb.Append("\"");
                        sb.Append(dt.Columns[j].ColumnName);
                        sb.Append("\":\"");
                        sb.Append(dt.Rows[i][j].ToString());
                        sb.Append("\",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]"); 
            }
            catch (ArgumentOutOfRangeException e) { SetError(e.Message); }
            catch (ArgumentException e) { SetError(e.Message); }
            catch (DuplicateNameException e) { SetError(e.Message); }

            return sb.ToString();
        }

        private void SetError(String str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                this._errors += Encoding.UTF8.GetString(buffer)+"; ";
            }
            else
            {
                this._errors = str;
            }
        }
        public string GetError()
        {
            return _errors;
        }
    }
}
