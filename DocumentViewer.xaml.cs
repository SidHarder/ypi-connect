using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Net;
using System.IO;
using System.IO.Packaging;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YPIConnect
{
    /// <summary>
    /// Interaction logic for DocumentViewer.xaml
    /// </summary>
    public partial class DocumentViewer : Window
    {
        public DocumentViewer()
        {
            InitializeComponent();
            this.Viewer.FitToWidth();
        }

        public void ViewDocument(string fileName)
        {
            WebRequest request = WebRequest.Create("http://localhost:4000");
            request.Headers.Add("content-type", "application/octet-stream");

            WebResponse response = request.GetResponse();            
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            Stream responseStream = response.GetResponseStream();
            
            MemoryStream memoryStream = new MemoryStream();
            responseStream.CopyTo(memoryStream);

            Package package = Package.Open(memoryStream);
            
            Uri packageUri = new Uri("memorystream://whatsupdoc.xps");                 
            PackageStore.AddPackage(packageUri, package);                    
            XpsDocument document = new XpsDocument(package, CompressionOption.Maximum, packageUri.ToString());                    
            FixedDocumentSequence fixedDocumentSequence = document.GetFixedDocumentSequence();
            this.Viewer.Document = fixedDocumentSequence as IDocumentPaginatorSource;

            //PackageStore.RemovePackage(packageUri);
            //doc.Close();            
        }

        public void YY()
        {
            byte[] xpsBytes = File.ReadAllBytes(@"myXps.xps");
            MemoryStream xpsStream = new MemoryStream(xpsBytes);

            using (Package package = Package.Open(xpsStream))

            {
                string inMemoryPackageName = "memorystream://myXps.xps";
                Uri packageUri = new Uri(inMemoryPackageName);

                PackageStore.AddPackage(packageUri, package);

                XpsDocument xpsDoc = new XpsDocument(package, CompressionOption.Maximum, inMemoryPackageName);

                FixedDocumentSequence fixedDocumentSequence = xpsDoc.GetFixedDocumentSequence();

                PackageStore.RemovePackage(packageUri);

                xpsDoc.Close();

            }
        }

        public void XX()
        {
            //Uri DocumentUri = new Uri("pack://currentTicket_" + new Random().Next(1000).ToString() + ".xps");
            Uri docUri = new Uri("pack://tempTicket.xps");

            var ms = new MemoryStream();
            {
                Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);

                PackageStore.RemovePackage(docUri);
                PackageStore.AddPackage(docUri, package);

                XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.SuperFast, docUri.AbsoluteUri);

                XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDocument), false);

                //DocumentPaginator docPage = ((IDocumentPaginatorSource)document).DocumentPaginator;

                //docPage.PageSize = new System.Windows.Size(PageWidth, PageHeight);
                //docPage.PageSize = new System.Windows.Size(document.PageWidth, document.PageHeight);

                //rsm.SaveAsXaml(docPage);

                //return xpsDocument;
            }
        }

        /*
        public static async Task GetXPS()
        {
            using var client = new HttpClient();
            var content = await client.GetStringAsync("http://localhost:4000");

            Console.WriteLine(content);

            //XpsDocument xpsDocument = new XpsDocument(fileName, System.IO.FileAccess.Read);
            //this.Viewer.Document = xpsDocument.GetFixedDocumentSequence();
            //xpsDocument.Close();
        
        }
        */
        
        public void LoadDocument(FixedDocument fixedDocument)
        {
            this.Viewer.Document = fixedDocument;
        }
    }
}
