using System;
using System.Data.Entity;
using System.IO;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Reinforced.Storage.Playground.Entities;
using Reinforced.Storage.Playground.Services;
using Reinforced.Storage.Services;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.TestCodeMaker.StoryValidation;
using Reinforced.Storage.Testing;
using Reinforced.Storage.Testing.Namer;
using Reinforced.Storage.Testing.Stories;
using Reinforced.Storage.Testing.Stories.Sql;
using static Reinforced.Storage.Testing.Stories.Common.CommonAssertions;
using static Reinforced.Storage.Testing.Stories.Sql.SqlAssertions;
using static Reinforced.Storage.Testing.Stories.Add.AddAssertions;
using static Reinforced.Storage.Testing.Stories.Remove.RemoveAssertions;

namespace Reinforced.Storage.Playground
{
    struct Ticket:IDisposable
    {
        

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Console.WriteLine("Disposed");
        }
    }

    class SomeService
    {
        private Ticket MakeTicket()
        {
            return new Ticket();
        }
        public void MakeThings()
        {
            Console.WriteLine("Obtainig ticket");
            var t = MakeTicket();
            Console.WriteLine("Making things");
        }
    }

    static class Program
    {
        private const string _namerFileName = @"w:\Work\Reinforced\Storage\names.json";
        static void Main(string[] args)
        {
            SomeService ss = new SomeService();
            ss.MakeThings();

            Database.SetInitializer<TestDataContext>(null);

            //using (var dc = new TestDataContext())
            //{
            //    var rc = new NamerRepositoryConstructor(dc);
            //    var repo = rc.ExtractNames();
            //    var namesFile = JsonConvert.SerializeObject(repo, Formatting.Indented);
            //    File.WriteAllText(_namerFileName,namesFile);
            //}

            var namerFile = File.ReadAllText(_namerFileName);
            var namer = JsonConvert.DeserializeObject<MapperRepository>(namerFile);

            TestingEnvironment environment = new TestingEnvironment(namer);
            environment.Sqls.SkipMissingAssumptions();

            var storage = environment.Create();


            storage.Do<UsersService>().RemoveById(50);
            storage.Let<UserSpace>().Within(150).UpdateDocuments();

            var story = environment.TellAbout(storage);


            var text = story.ToText();

            var x = story.GenerateValidation(d =>
            {
                d.ForEffect<DirectSqlSideEffect>()
                    .Annotation()
                    .ExactCommand()
                    .ExactParameters();
                //d.ForEffect<SaveChangesSideEffect>().Validate();
                d.ForEffect<CommentSideEffect>()
                    .Validate();
                d.ForEffect<BulkSideEffect>().Annotation().Validate();
                d.ForEffect<AsyncBulkSideEffect>().Annotation().Validate();
                d.ForEffect<AddSideEffect>().Annotation().Validate();
                d.ForEffect<RemoveSideEffect>().Annotation().Validate();
                d.ForEffect<UpdateSideEffect>().Annotation().Validate();
            });

            story.Begins()
                .Then(Remove<User>())
                .Then
                (
                    Annotated(@"blah"),
                    SqlExactCommand(@"UPDATE [Documents] SET [Status] = @p0 WHERE [UserId] = @p1"),
                    SqlExactParameters(0, 50)
                )
                .Then(Comment(@"Lets stir-fry the cat"))
                .Then
                (
                    Annotated(@"new document"),
                    Add<Document>()
                )
                .Then
                (
                    Annotated(@"reset titles"),
                    SqlExactCommand(@"UPDATE [Documents] SET [Title] = ([Title] + @p0) WHERE [UserId] = @p1"),
                    SqlExactParameters("Vasya", 50)
                )
                .SomethingHappens()
                .Then(SqlExactCommand(@"DELETE FROM [Documents]"))
                .Then(Comment(@"Something else"))
                .SomethingHappens()
                .TheEnd();
        }
    }
}
