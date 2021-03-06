﻿namespace Parents.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Interfaces;
    using Models;
    using Parents.Models.ActivitiesManagement.Helpers;
    using SQLite;
    using SQLite.Net;
    using Xamarin.Forms;


    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();

            connection = new SQLiteConnection(config.Platform, System.IO.Path.Combine(config.DirectoryDB, "Parents.db3"));
            connection.CreateTable<Children>();
            connection.CreateTable<ActivityType>();
            //connection.CreateTable<Parent>();
            connection.CreateTable<ActivityParents>();
            connection.CreateTable<TokenResponse>();
        }

        public void Insert<T>(T model)
        {
            connection.Insert(model);
        }

        public void Update<T>(T model)
        {
            connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }

        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                //return connection.GetAllWithChildren<T>().FirstOrDefault();
                return connection.Table<T>().FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }

        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                //return connection.GetAllWithChildren<T>().ToList();
                return connection.Table<T>().ToList();
            }
            else
            {
                var tt = connection.Table<T>().ToList();
                return connection.Table<T>().ToList();
            }
        }

        //public List<Children> GetListChildren()
        //{
        //    return connection.Table<Children>().ToList();
        //}

        //public List<ActivityParents> GetListOfActivities()
        //{
        //    return connection.Table<ActivityParents>().ToList();
        //}

        public T Find<T>(int pk, bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                //return connection.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
                return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return connection.Table<T>()
                                 .FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
