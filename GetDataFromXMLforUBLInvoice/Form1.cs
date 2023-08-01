using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GetDataFromXMLforUBLInvoice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Xml.XmlDocument dd = new System.Xml.XmlDocument();
            dd.Load(@"C:\Users\Yasin KOC\Desktop\faturaorj.xml");
            var deneme = dd.ChildNodes[0];

            //foreach (System.Xml.XmlElement item in deneme.ChildNodes)
            //{
            //    string name = item.Name;
            //    string value = "";
            //    
            //    if (item.ChildNodes.Count > 0)
            //    {
            //        if (item.ChildNodes.Count == 1)
            //        {
            //            value = item.ChildNodes[0].Value;
            //        }
            //        else
            //        {
            //            for (int i = 0; i < item.ChildNodes.Count; i++)
            //            {
            //                name = item.ChildNodes[i].Name;
            //                value = item.ChildNodes[i].ChildNodes[0].Value;
            //            }
            //        }
            //        
            //    }
            //}
            memoEdit1.Text = "";
            for (int i = 0; i < deneme.ChildNodes.Count; i++)
            {
                var item = deneme.ChildNodes[i];
                if (item.Name.Substring(0,3) == "cac")
                {
                    int Level = 1;
                    //memoEdit1.Text += item.Name + "\r\n";
                    //if (item.Name == "cac:InvoiceLine")
                    //{
                    //
                    //}

                    string t = "";
                    for (int j = 0; j < Level; j++)
                    {
                        t += "\t";
                    }

                    TxtDosyasinaYaz(t + item.Name);
                    LookCbc((System.Xml.XmlElement)item, ref Level);
                    //memoEdit1.Text += item.Name + "\r\n";
                    TxtDosyasinaYaz(t + item.Name);
                }
                else if (item.Name.Substring(0, 3) == "cbc")
                {
                    //memoEdit1.Text += GetNameValueFromcbcXML((System.Xml.XmlElement)item) + "\r\n";
                    TxtDosyasinaYaz(GetNameValueFromcbcXML((System.Xml.XmlElement)item));
                }
                else
                {

                }
            }
            MessageBox.Show("Kayıt İşlemi Başarılı!");
        }

        private string GetNameValueFromcbcXML(System.Xml.XmlElement pXmlElement)
        {
            return pXmlElement.Name + " => " + (pXmlElement.ChildNodes.Count == 0 ? "" : pXmlElement.ChildNodes[0].Value);
            //return pXmlElement.Name;// + " => " + (pXmlElement.ChildNodes.Count == 0 ? "" : pXmlElement.ChildNodes[0].Value);
        }
        
        private void LookCbc(System.Xml.XmlElement xmlElement, ref int pLevel)
        {
            for (int i = 0; i < xmlElement.ChildNodes.Count; i++)
            {
                System.Xml.XmlElement item = (System.Xml.XmlElement)xmlElement.ChildNodes[i];
                if (item.Name.Substring(0, 3) == "cac")
                {
                    string t = "";
                    if (i > 0 && xmlElement.ChildNodes.Count > i && xmlElement.ChildNodes[i - 1].Name.Substring(0, 3) == "cac")
                    {

                    }
                    else
                    {
                        pLevel++;
                    }
                    
                    if (pLevel > 0)
                    {
                        for (int j = 0; j < pLevel; j++)
                        {
                            t += "\t";
                        }
                    }
                    TxtDosyasinaYaz(t + item.Name);
                    LookCbc(item, ref pLevel);
                    TxtDosyasinaYaz(t + item.Name);
                }
                else if (item.Name.Substring(0, 3) == "cbc")
                {
                    string t = "";
                    for (int j = 0; j < pLevel + 1; j++)
                    {
                        t += "\t"; 
                    }
                    //memoEdit1.Text += "\t" + GetNameValueFromcbcXML(item) + "\n";
                    TxtDosyasinaYaz(t + GetNameValueFromcbcXML(item));
                }
                else
                {

                }
            }
        }

        private void TxtDosyasinaYaz(string String)
        {
            string FilePath = @"C: \Users\Yasin KOC\Desktop\fatura.txt";

            //FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Write);

            if (File.Exists(FilePath))
            {
                //StreamWriter yaz = new StreamWriter(fileStream, Encoding.UTF8);
                //yaz.WriteLine(String + Environment.NewLine);
                //yaz.Close();
                File.AppendAllText(FilePath, String + Environment.NewLine);
                //fileStream.Close();
            }
            else
            {

            }
        }
    }
}
