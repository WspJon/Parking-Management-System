using System;
using System.IO;

class Program
{
    static void Main()
    {
        string text = File.ReadAllText("ParkingSystemProject.vbproj");
        text = System.Text.RegularExpressions.Regex.Replace(text, @"<Reference Include=""MySql\.Data.*?<\/Reference>", "", System.Text.RegularExpressions.RegexOptions.Singleline);
        if (!text.Contains(@"<Compile Include=""LocalDbCompat.vb"" />"))
        {
            text = text.Replace(@"<Compile Include=""dbConnect.vb"" />", @"<Compile Include=""dbConnect.vb"" />" + "\r\n    " + @"<Compile Include=""LocalDbCompat.vb"" />");
        }
        File.WriteAllText("ParkingSystemProject.vbproj", text);
    }
}
