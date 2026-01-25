using LogicInterpreter.Core;
using System.IO;

namespace LogicInterpreter.Commands
{
    class SaveLoad
    {
        public static void Save(string path, string[] defines, int count)
        {
            using (StreamWriter w = new StreamWriter(path))
            {
                for (int i = 0; i < count; i++)
                    w.WriteLine(defines[i]);
            }
        }

        public static void Load(string path, FunctionCollection functions)
        {
            try
            {

                using (StreamReader r = new StreamReader(path))
                {
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (line != null || line != "")
                            Define.Execute(line, functions);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new Exception($"File: {path} not found.");

            }
            catch (Exception e)
            {
                throw new Exception($"Error while loading file: - {e.Message}");
            }
        }
    }
}