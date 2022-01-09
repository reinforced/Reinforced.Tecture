using System;
using System.Threading.Tasks;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Services;
using Xunit;

namespace Reinforced.Tecture.Tests.Services
{
    public class ServiceLifecycleTests
    {
        class DisposeService : TectureService
        {
            private DisposeService(){}
            
            public void Action(){}
            public static int Disposed { get; private set; }

            protected override void OnDispose()
            {
                Disposed++;
            }
        }

        [Fact]
        public void Tecture_Calls_Dispose_On_All_Services_When_Being_Disposed_Itself()
        {
            var tb = new TectureBuilder();
            using (var tecture = tb.Build())
            {
                tecture.Let<DisposeService>().Action();
            }
            
            Assert.Equal(1,DisposeService.Disposed);
        }
        
        class InitService : TectureService
        {
            private InitService(){}
            
            public void Action(){}
            public static int Initialized { get; private set; }

            protected override void Init()
            {
                Initialized++;
            }
        }
        
        [Fact]
        public void Tecture_Calls_Init_Exactly_Once_For_Each_Service_In_Instance()
        {
            var tb = new TectureBuilder();
            using (var tecture = tb.Build())
            {
                tecture.Let<InitService>().Action();
                tecture.Let<InitService>().Action();
            }
            
            Assert.Equal(1,InitService.Initialized);
        }
        
        class SaveFinallyService : TectureService
        {
            private SaveFinallyService(){}

            public int Counter { get; private set; }
            
            public void Action()
            {
                Counter++;
                Then(() =>
                {
                    Counter++;
                });
            }
            public int SaveCount { get; private set; }
            public int SaveAsyncCount { get; private set; }
            public int FinallyCount { get; private set; }
            public int FinallyAsyncCount { get; private set; }

            protected override void OnSave() => SaveCount++;
            protected override void OnFinally(Exception ex) => FinallyCount++;
            protected override Task OnSaveAsync()
            {
                SaveAsyncCount++;
                return Task.CompletedTask;
            }
            
            protected override Task OnFinallyAsync(Exception ex)
            {
                FinallyAsyncCount++;
                return Task.CompletedTask;
            }
        }
        
        [Fact]
        public void Tecture_Calls_OnSave_Every_Save_And_Calls_Finally_When_SaveChain_Ends()
        {
            var tb = new TectureBuilder();
            SaveFinallyService sfs = null;
            using (var tecture = tb.Build())
            {
                sfs = tecture.Let<SaveFinallyService>();
                tecture.Let<SaveFinallyService>().Action();
                tecture.Save();
                
                tecture.Let<SaveFinallyService>().Action();
                tecture.Save();
            }
            
            Assert.Equal(4,sfs.Counter);
            Assert.Equal(2,sfs.SaveCount);
            Assert.Equal(2,sfs.FinallyCount);
            // and does not call async saves!
            Assert.Equal(0,sfs.SaveAsyncCount);
            Assert.Equal(0,sfs.FinallyAsyncCount);
        }
        
        [Fact]
        public async Task Tecture_Calls_OnSave_Every_Save_And_Calls_Finally_When_SaveChain_Ends_ASYNC()
        {
            var tb = new TectureBuilder();
            SaveFinallyService sfs = null;
            using (var tecture = tb.Build())
            {
                sfs = tecture.Let<SaveFinallyService>();
                
                tecture.Let<SaveFinallyService>().Action();
                await tecture.SaveAsync();
                
                tecture.Let<SaveFinallyService>().Action();
                await tecture.SaveAsync();
            }
            
            Assert.Equal(4,sfs.Counter);
            Assert.Equal(2,sfs.SaveAsyncCount);
            Assert.Equal(2,sfs.FinallyAsyncCount);
            // and does not call sync saves!
            Assert.Equal(0,sfs.SaveCount);
            Assert.Equal(0,sfs.FinallyCount);
        }
        
        [Fact]
        public void Tecture_Ensures_Single_Instance_Of_Services()
        {
            var tb = new TectureBuilder();
            using (var tecture = tb.Build())
            {
                var s1 = tecture.Let<SaveFinallyService>();
                var s2 = tecture.Let<SaveFinallyService>();
                Assert.True(object.ReferenceEquals(s1,s2));
            }
        }
    }
}