using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.Defaults.EntityFramework;
using Reinforced.Storage.Playground.Entities;
using Reinforced.Storage.Services;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Playground.Services
{
    public class UsersService : StorageService<User>, INoContext
    {
        private UsersService() { }

        private DirectSqlSideEffect DeleteById<T>(int id) where T : IEntity
        {
            return Sql<T>(d => $"DELETE FROM {d} WHERE {d.Id == id}");
        }

        public async void RemoveById(int id)
        {
            //DeleteById<User>(id);
            Remove(new User() { Id = id });

            var doc = Do<Documents>();
            Do<Documents>().OutdateDocumentsOfUser(id);
        }
    }

    public class Documents : StorageService<Document,User>, INoContext
    {
        private Documents()
        {
            
        }
        public async Task<Document> Do1()
        {
            Document n = new Document();
            await Save;
            return n;
        }

        public async Task<Document> Do2()
        {
            Document n = new Document();
            await Save;
            return n;
        }

        public void OutdateDocumentsOfUser(int userId)
        {
            //Sql<Document>(d => $"UPDATE {d} SET {d.Status == 0} WHERE {d.UserId == userId}")
            //    .Annotate("blah");
            //Add(new User()).Annotate("poshel nahuy");
            var users = 
                from u in Get<User>().All
                where u.Id == 10
                select new {u.FirstName, u.IsActive};

            
            Sql<Document,User>((d,u) => $"DELETE FROM {d} INNER JOIN {u} ON {u.Id==d.UserId} WHERE {d.Status==0}");
        }

        public void MakeSomethingElse()
        {
            Comment("Something else");
        }
    }


    public class UserSpace : StorageService<Document>, IContext<int>
    {
        private UserSpace() { }

        private User _myNewUser;

        /// <summary>
        /// Imports context into service
        /// </summary>
        public void Context(int userId)
        {
            if (userId == 50)
                throw new Exception("Invalid user!");
            _myNewUser = new User(){FirstName = "Vasya", Id = 50}; //Get<User>().All.FirstOrDefault(d => d.Id == userId);
        }
        
        public async void UpdateDocuments()
        {
            Comment("Lets stir-fry the cat");
            Add(new Document()).Annotate("new document");

            if (_myNewUser == null) return;

            Sql<Document>(d =>
                $"UPDATE {d} SET {d.Title == d.Title + _myNewUser.FirstName} WHERE {d.UserId == _myNewUser.Id}").Annotate("reset titles");

            await Save;
            Sql<Document>(d => $"DELETE FROM {d}");
            Do<Documents>().MakeSomethingElse();
        }
    }
}
