namespace LogicInterpreter.Utils
{
    class Splitter
    {
        public static string[] Split(string input, char[] symbols)
        {
            string[] result = new string[input.Length];
            int count = 0;
            string current = "";

            for (int i = 0; i < input.Length; i++)
            {
                bool isSeparator = false;

                for (int j = 0; j < symbols.Length; j++)
                {
                    if (input[i] == symbols[j])
                    {
                        isSeparator = true;
                        break;
                    }
                }

                if (!isSeparator)
                {
                    current += input[i];
                }
                else if (current != "")
                {
                    result[count++] = current;
                    current = "";
                }
            }

            if (current != "")
            {
                result[count++] = current;
            }

            return result;
        }
    }
}

