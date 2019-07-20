using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args.FirstOrDefault() ?? Path.Combine(Environment.CurrentDirectory, "contacts.vcf");

            var lines = File.ReadAllLines(path);

            using (var file = new StreamWriter("rep_contacts.vcf"))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("FN"))
                    {
                        var nextLine = lines[i + 1];

                        var lastNameStartIndex = nextLine.IndexOf(":") + 1;
                        var lastNameEndIndex = nextLine.IndexOf(";");
                        var lastName = nextLine.Substring(lastNameStartIndex, lastNameEndIndex - lastNameStartIndex);

                        var firstNameStartIndex = lastNameEndIndex + 1;
                        var firstNameEndIndex = nextLine.IndexOf(";", firstNameStartIndex);
                        var firstName = nextLine.Substring(firstNameStartIndex, firstNameEndIndex - firstNameStartIndex);

                        if (lastName == "")
                        {
                            file.WriteLine($"FN:{firstName}");
                        }
                        else
                        {
                            file.WriteLine($"FN:{lastName} {firstName}");
                        }
                    }
                    else
                    {
                        file.WriteLine(lines[i]);
                    }
                }
            }
        }
    }
}
