using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MeteoApp.Models;
using SQLite;

namespace MeteoApp.db
{
    public class DBManager
    {
        readonly SQLiteAsyncConnection database;

        public DBManager()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestSQLite.db3");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<EntryToSave>().Wait();
            EntryToSave temp=new EntryToSave();
            temp.key = "token";
            temp.value = "";
            database.InsertAsync(temp);
            Console.WriteLine("INSERISCO LA PRIMA RIGA NEL DB");


        }

        /*
         * Ritorna una lista con tutti gli items.
         */
        public Task<List<EntryToSave>> GetItemsAsync()
        {
            return database.Table<EntryToSave>().ToListAsync();
        }

        /*
         * Query con query SQL.
         */
        public Task<List<EntryToSave>> GetItemsWithWhere(string key)
        {
            return database.QueryAsync<EntryToSave>("SELECT * FROM [EntryToSave] WHERE [key] =?", key);
        }

        /*
         * Query con LINQ.
         */
        public Task<EntryToSave> GetItemAsync(string key)
        {
            return database.Table<EntryToSave>().Where(i => i.key.Equals(key)).FirstOrDefaultAsync();
        }

        /*
         * Salvataggio.
         */
        public Task<int> SaveItemAsync(EntryToSave item)
        {
            
                return database.UpdateAsync(item);

            //return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(EntryToSave item)
        {
            return database.DeleteAsync(item);
        }
    }
}
