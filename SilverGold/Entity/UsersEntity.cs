using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class UsersEntity
    {
    }

    public static class UserFactory
    {
        public static void Insert(String _UserId, String _Pwd, String _UserType, String _Company, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO USERS(UserId,Pwd,UserType,Company)VALUES(@UserId,@Pwd,@UserType,@Company)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.Parameters.AddWithValue("@Pwd", _Pwd);
            cmdInsert.Parameters.AddWithValue("@UserType", _UserType);
            cmdInsert.Parameters.AddWithValue("@Company", _Company);
            cmdInsert.ExecuteNonQuery();
        }
    }
}
