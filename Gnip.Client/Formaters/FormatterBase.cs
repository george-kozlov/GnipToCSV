
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>10/06/2013</date>
// <summary>FormatterBase class</summary>

using System.IO;

namespace Gnip.Client.Formaters
{
    public interface IFormatter
    {
        bool IsValid(string rawText);

        string Normalize(string rawText);

        int FindLastDelimiter(string data);

        bool HasDelimiter(string data);
    }
}
