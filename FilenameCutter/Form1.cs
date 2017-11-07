using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilenameCutter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            textBox1.Text = folderBrowserDialog1.SelectedPath;

            cutname(folderBrowserDialog1.SelectedPath, 100);
        }


        private void cutname(String path,int size)
        {
            System.IO.DirectoryInfo Directory = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] files = Directory.GetFiles("*", System.IO.SearchOption.AllDirectories);

            foreach(System.IO.FileInfo file in files)
            {
                if (file.Attributes == System.IO.FileAttributes.Directory)
                    continue;

                if (file.Name.Length < size)
                    continue;

                int ext = file.Extension.Length;

                int namesize = size - ext;

                String newname = file.Name.Substring(0,namesize);

                for (int i=2; System.IO.File.Exists(file.DirectoryName + "\\" + newname + file.Extension);i++)
                {
                    newname = newname.Substring(0, newname.Length - (1+i.ToString().Length)) + "_" + i.ToString();
                }

                System.IO.File.Move(file.FullName, file.DirectoryName + "\\" + newname + file.Extension);

            }
        }
    }
}
