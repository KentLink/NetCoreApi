using Dapper;
using MDES.Api.Model.Arg;
using MDES.Api.Model.Test;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MDES.Api.Dao.Test
{
    public class TestCoreSqlDao: BaseDao
    {
        /// <summary>
        /// 查詢 Get
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<TestResultModel> GetTestSqlData(TestArg arg)
        {
            string sql = @"select ID as Id,NAME as Name,SEQ as Seq from TEST where ID = @Id ";
            if (string.IsNullOrEmpty(arg.Id))
            {
                sql = @"select ID as Id,NAME as Name,SEQ as Seq from TEST ";
            }
            DynamicParameters paramter = new DynamicParameters();
            paramter.Add("@Id", arg.Id);

            var result = new List<TestResultModel>();
            using (SqlConnection conexao = new SqlConnection(ConfigProvider.Default))
            {
                result = conexao.Query<TestResultModel>(sql, paramter).AsList<TestResultModel>();
            }

            return result;

        }



        /// <summary>
        /// 更新Update
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool UpdateTestData(TestArg arg)
        {
            string sql = @"update ...";

            DynamicParameters paramter = new DynamicParameters();
            paramter.Add("@Id", "XXXXX");

            using (SqlConnection conexao = new SqlConnection(ConfigProvider.Default))
            {
                IDbTransaction tran = conexao.BeginTransaction();
                try
                {
                    conexao.Execute(sql, paramter, tran);
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }

        }





    }
}
