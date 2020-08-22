using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing
{
    class TabsManager
    {
        public static readonly SyntaxTrivia TabSymbol = SyntaxFactory.Whitespace("\t");
        private int _tabsCount = 0;

        public SyntaxTrivia TabTrivia
        {
            get { return TabSymbol; }
        }

        public SyntaxTriviaList Tabs()
        {
            var arr = new SyntaxTrivia[_tabsCount];
            for (int i = 0; i < _tabsCount; i++)
            {
                arr[i] = TabSymbol;
            }
            return SyntaxFactory.TriviaList(arr);
        }

        public SyntaxTriviaList BrTabs()
        {
            var arr = new SyntaxTrivia[_tabsCount+1];
            arr[0] = LineFeed;
            for (int i = 0; i < _tabsCount; i++)
            {
                arr[i+1] = TabSymbol;
            }
            return SyntaxFactory.TriviaList(arr);
        }
        public SyntaxTriviaList BrTabs_Prev()
        {
            var arr = new SyntaxTrivia[_tabsCount];
            arr[0] = LineFeed;
            for (int i = 0; i < _tabsCount-1; i++)
            {
                arr[i + 1] = TabSymbol;
            }
            return SyntaxFactory.TriviaList(arr);
        }

        public SyntaxTriviaList TabsBr()
        {
            var arr = new SyntaxTrivia[_tabsCount + 1];
            arr[_tabsCount] = LineFeed;
            for (int i = 0; i < _tabsCount; i++)
            {
                arr[i] = TabSymbol;
            }
            return SyntaxFactory.TriviaList(arr);
        }
        public SyntaxTriviaList TabsBr_Prev()
        {
            var arr = new SyntaxTrivia[_tabsCount];
            arr[_tabsCount-1] = LineFeed;
            for (int i = 0; i < _tabsCount-1; i++)
            {
                arr[i] = TabSymbol;
            }
            return SyntaxFactory.TriviaList(arr);
        }

        public void Tab()
        {
            _tabsCount++;
        }

        public void Untab()
        {
            _tabsCount--;
        }

    }

    class TabsScope : IDisposable
    {
        private readonly TabsManager _tm;

        public TabsScope(TabsManager tm)
        {
            _tm = tm;
            _tm.Tab();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _tm.Untab();
        }
    }
}