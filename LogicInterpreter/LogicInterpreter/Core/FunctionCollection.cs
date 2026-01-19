using LogicInterpreter.Core;

namespace LogicInterpreter.Core
{
    class FunctionCollection
    {
        private LogicalFunction[] functions = new LogicalFunction[20];
        private int count = 0;

        public void Add(string name, LogicalFunction function)
        {
            functions[count++] = function;
        }

        public LogicalFunction Get(string name)
        {
            for (int i = 0; i < count; i++)
            {
                if (functions[i].Name == name)
                    return functions[i];
            }
            return null;
        }
    }
}
