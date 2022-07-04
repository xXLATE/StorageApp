using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _db.CreateTable<Project>();
            _db.CreateTable<Type>();
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

        public int SaveProject(Project project)
        {
            if (project.Id != 0)
            {
                _db.Update(project);
                return project.Id;
            }
            else
                return _db.Insert(project);
        }

        public int SaveType(Type type)
        {
            if (type.Id != 0)
            {
                _db.Update(type);
                return type.Id;
            }
            else
                return _db.Insert(type);
        }

        public void DeleteProject(Project project)
        {
            _db.Delete(project);
        }

        public IEnumerable<User> GetUsers()
        {
            return _db.Table<User>();
        }

        public IEnumerable<Project> GetProjects()
        {
            return _db.Table<Project>().ToList();
        }

        public IEnumerable<Type> GetTypes()
        {
            return _db.Table<Type>().ToList();
        }

        public IEnumerable<Type> GetTypesByUser(int idUser)
        {
            return GetTypes().Where(type => type.User_Id == idUser);
        }

        public IEnumerable<Project> GetProjectsByUser(int idUser)
        {
            return GetProjects().Where(project => project.User_Id == idUser);
        }
    }
}
