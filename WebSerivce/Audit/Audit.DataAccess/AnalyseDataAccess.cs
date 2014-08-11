using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace JN.ESB.Audit.DataAccess
{
    /// <summary>
    /// 分析数据获取类
    /// </summary>
    public class AnalyseDataAccess
    {
        AuditBusinessDataClassesDataContext execptionDC = new AuditBusinessDataClassesDataContext();

        public AnalyseDataAccess() { }

        /// <summary>
        /// 获取调用次数分析数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetCallNumAnalyseData()
        {
            DataTable dt = new DataTable("Analyse");
            DbConnection conn = execptionDC.Connection;
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn as SqlConnection;
            comm.CommandText = "SELECT BusinessFullName, COUNT(*) AS NUM FROM dbo.[AuditBusinessAnalyseView] ";
            comm.CommandText += "GROUP BY BusinessFullName ";
            comm.CommandText += "ORDER BY BusinessFullName ";

            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comm;
                adapter.Fill(dt);

                comm.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("获取到调用次数分析数据时发生异常：" + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return dt;
        }

        /// <summary>
        /// 获取到按服务细分的调用次数分析数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetCallNumAnalyseDataByService()
        {
            DataTable dt = new DataTable("Analyse");
            DbConnection conn = execptionDC.Connection;
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn as SqlConnection;
            comm.CommandText = "SELECT BusinessName, ServiceName, COUNT(*) AS NUM FROM dbo.[AuditBusinessAnalyseView] ";
            comm.CommandText += "GROUP BY BusinessName,ServiceName ";
            comm.CommandText += "ORDER BY BusinessName ";

            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comm;
                adapter.Fill(dt);

                comm.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("获取到调用次数分析数据时发生异常：" + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return dt;
        }

        /// <summary>
        /// 获取到按服务、方法细分的响应时间分析数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetResTimeAnalyseData()
        {
            DataTable dt = new DataTable("Analyse");
            DbConnection conn = execptionDC.Connection;
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn as SqlConnection;
            comm.CommandText = "SELECT BusinessName,ServiceName,MethodName, AVG(DATEDIFF(ms, CallBeginTime, CallEndTime)) AS ResTimeAvg ";
            comm.CommandText += "FROM [AuditBusinessAnalyseView] ";
            comm.CommandText += "GROUP BY BusinessName,ServiceName,MethodName ";
            comm.CommandText += "ORDER BY BusinessName,ServiceName DESC ";

            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comm;
                adapter.Fill(dt);

                comm.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("获取到按服务、方法细分的响应时间时发生异常：" + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return dt;
        }

        /// <summary>
        /// 获取正常通讯与异常情况的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetExceptionAnalyseData()
        {
            DataTable dt = new DataTable("Analyse");
            DbConnection conn = execptionDC.Connection;
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn as SqlConnection;
            comm.CommandText = "SELECT CASE [Status] WHEN 0 THEN '未处理异常' WHEN 1 THEN '正常通讯' WHEN 8 THEN '已重发异常' WHEN 9 THEN '已归档异常' END AS CTYPE, ";
            comm.CommandText += "COUNT(*) AS NUM FROM dbo.[AuditBusinessAnalyseView] GROUP BY Status";
            
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comm;
                adapter.Fill(dt);

                comm.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("获取正常通讯与异常情况的数据时发生异常：" + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return dt;
        }
    }
}
