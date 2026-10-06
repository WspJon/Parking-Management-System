using System;
using System.IO;

class Program
{
    static void Main()
    {
        string text = File.ReadAllText("ParkingSystemProject.vbproj");
        text = System.Text.RegularExpressions.Regex.Replace(text, @"<Reference Include=""MySql\.Data.*?<\/Reference>", "", System.Text.RegularExpressions.RegexOptions.Singleline);
        text = System.Text.RegularExpressions.Regex.Replace(text, @"<Compile Include=""dbConnect\.vb""\s*\/>", "", System.Text.RegularExpressions.RegexOptions.Singleline);
        text = System.Text.RegularExpressions.Regex.Replace(text, @"<Compile Include=""DummyMySql\.vb""\s*\/>", "", System.Text.RegularExpressions.RegexOptions.Singleline);
        File.WriteAllText("ParkingSystemProject.vbproj", text);
    }
}
