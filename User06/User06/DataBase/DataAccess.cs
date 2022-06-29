using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace User06.DataBase
{
    public class DataAccess
    {
        private SQLiteConnection _db;

        public DataAccess(string databasePath)
        {
            _db = new SQLiteConnection(databasePath);
            _db.CreateTable<User>();
        }

        public int SaveUser(User user)
        {
            if (user.Id != 0)
            {
                _db.Update(user);
                return user.Id;
            }
            else
                return _db.Insert(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return _db.Table<User>();
        }
    }
}
