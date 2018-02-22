using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDES.Api.Model.Entity
{
    public class TestEntityMap : ClassMapper<Entity.TestEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public TestEntityMap()
        {
            Table("TEST");
            Map(m => m.Id).Column("ID");
            Map(m => m.Name).Column("NAME");
            Map(m => m.Seq).Column("SEQ");
        }
    }
}
