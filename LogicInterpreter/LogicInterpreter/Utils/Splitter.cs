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
                char c = input[i];

             
                bool isSeparator = false;
                for (int j = 0; j < symbols.Length; j++)
                {
                    if (c == symbols[j])
                    {
                        isSeparator = true;
                        break;
                    }
                }

              
                if (!isSeparator && (c == ' ' || c == '\t' || c == '\r' || c == '\n'))
                    continue;

       
                if (!isSeparator)
                {
                    current += c;
                }
                else
                {
                    if (current != "")
                    {
                        result[count++] = current;
                        current = "";
                    }
                }
            }

            if (current != "")
                result[count++] = current;

           
            string[] trimmed = new string[count];
            for (int i = 0; i < count; i++)
                trimmed[i] = result[i];

            return trimmed;
        }
    }
}