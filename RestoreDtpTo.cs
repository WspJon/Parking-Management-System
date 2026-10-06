using System;
using System.IO;

class Program
{
    static void Main()
    {
        string file = "frmadmindashboard.Designer.vb";
        string content = File.ReadAllText(file);
        
        if (!content.Contains("Me.dtpTo = New System.Windows.Forms.DateTimePicker()"))
        {
            content = content.Replace("Me.dtpFrom = New System.Windows.Forms.DateTimePicker()", 
                "Me.dtpFrom = New System.Windows.Forms.DateTimePicker()\r\n        Me.dtpTo = New System.Windows.Forms.DateTimePicker()");
                
            content = content.Replace("Me.pnlHeader.Controls.Add(Me.dtpFrom)", 
                "Me.pnlHeader.Controls.Add(Me.dtpFrom)\r\n        Me.pnlHeader.Controls.Add(Me.dtpTo)");
                
            string dtpFromDef = @"        'dtpFrom
        Me.dtpFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(805, 13)
        Me.dtpFrom.Name = ""dtpFrom""
        Me.dtpFrom.Size = New System.Drawing.Size(100, 20)
        Me.dtpFrom.TabIndex = 7
        Me.dtpFrom.Visible = False";

            string dtpToDef = dtpFromDef + @"
        'dtpTo
        Me.dtpTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(915, 13)
        Me.dtpTo.Name = ""dtpTo""
        Me.dtpTo.Size = New System.Drawing.Size(100, 20)
        Me.dtpTo.TabIndex = 8
        Me.dtpTo.Visible = False";

            content = content.Replace(dtpFromDef, dtpToDef);

            content = content.Replace("Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker", 
                "Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker\r\n    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker");

            File.WriteAllText(file, content);
            Console.WriteLine("Added dtpTo successfully!");
        }
        else
        {
            Console.WriteLine("dtpTo already exists in the file.");
        }
    }
}
