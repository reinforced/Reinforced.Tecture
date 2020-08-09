using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.Defaults.EntityFramework;
using Reinforced.Storage.Playground.Entities;

namespace Reinforced.Storage.Playground.Pattern
{
    public class DocumentsReport
    {
        public DateTime Date { get; set; }
        public int DocumentsCount { get; set; }
    }
    public static class QueryExtensions
    {
        public static IQueryable<DocumentsReport> ByUsers(this IQueryFor<DocumentsReport> q, int[] userIds)
        {
            var documents = q.Joined<Document>();
            var users = q.Joined<User>();

            return 
                from d in documents
                join user in users on d.UserId equals user.Id
                where userIds.Contains(user.Id)
                group d by d.Date
                into grp
                select new DocumentsReport()
                {
                    Date = grp.Key,
                    DocumentsCount = grp.Count()
                };
        }
    }

    public static class Test
    {
        public static void QueryTest(TestDataContext dc)
        {
            var user = dc.Users.ById(10);
        }
    }
}
