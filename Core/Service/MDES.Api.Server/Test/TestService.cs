using MDES.Api.Dao.Test;
using MDES.Api.Model.Arg;
using MDES.Api.Model.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDES.Api.Server.Test
{
    public class TestService
    {
        public List<TestResultModel> GetTestProxyData(TestArg arg)
        {
            TestProxyDao testProxyDao = new TestProxyDao();
            List<TestResultModel> result = testProxyDao.GetTestProxyData(arg);
            return result;
        }

        public List<TestResultModel> GetTestSqlData(TestArg arg)
        {
            TestCoreSqlDao testSqlDao = new TestCoreSqlDao();
            List<TestResultModel> result = testSqlDao.GetTestSqlData(arg);
            return result;
        }

        //public List<TestResultModel> ExecSP(TestArg arg)
        //{
        //    TestCoreSqlDao testSqlDao = new TestCoreSqlDao();
        //    List<TestResultModel> result = testSqlDao.ExecSP(arg);
        //    return result;
        //}


    }
}
