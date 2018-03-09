using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DB.Helpers
{
    public class DbConnections
    {
        public static string GetAppHarborConnection()
        {
            return
                "Server=e0cd6fe1-4341-449a-9793-a89900b711c5.sqlserver.sequelizer.com;" +
                "Database=dbe0cd6fe14341449a9793a89900b711c5;" +
                "User ID=mdrrlpuwheqcposj;" +
                "Password=QvVGwAieCdF3supFZNKhEQp8FBnpmA5BNHorA6dc7Lmxe3J64fn66JX8gWa4n2Zb;	";
        }
    }
}
