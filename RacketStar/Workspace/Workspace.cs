using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RacketStar.Workspace
{
    class Workspace
    {
        public Stack<string> LastCommands;

        public LinkedList<Scope> Scopes;
        public LinkedListNode<Scope> Current;
             
        public object GetVariableValue(string name, object[] arguments)
        {
            // Keep track of whether we need values
            bool isFunction = arguments.Length != 0;


            
            return null;
        }

        public object GetLocalValue(string name, object[] arguments)
        {
            return null;
        }

        public void PushScope(Scope scope)
        {
            Scopes.AddLast(scope);
            Current = Scopes.Last;
        }

        public void PopScope()
        {
            Scopes.RemoveLast();
            Current = Scopes.Last;
        }
    }

    class DotNetWorkspace : Workspace
    {
        public List<Assembly> ReferencedAssemblies;
        public List<Assembly> ImportedAssemblies;
        public List<string> UsingNamespaces;
    }
}
