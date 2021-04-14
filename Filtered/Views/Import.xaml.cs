using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Filtered.Views
{
    /// <summary>
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : UserControl
    {

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler DataChanged;


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(Import));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        public Import()
        {
            InitializeComponent();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {

        }


        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // LoadingGrid.Children.Clear();
            
        }



        DataTable DT = new DataTable();

        private void PasteData()
        {
            string clipboardContent = Clipboard.GetText();


            var rows = clipboardContent
       .Split(new string[] { "\r\n" }, StringSplitOptions.None)
       .Where(x => !string.IsNullOrEmpty(x))
       .ToList();


            if (string.IsNullOrEmpty(clipboardContent)) return;


            DT = new DataTable();




            DT.Columns.Add("LoanNumber");




            foreach (var row in rows)
            {


                var columns = row.Split('\t');


                var rowdt = DT.NewRow();


                rowdt["LoanNumber"] = columns[0];


                DT.Rows.Add(rowdt);

                


            }



        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataChangedEventHandler handler = DataChanged;

            //db.DeleteTable();
            //db.BulkInsertLoans(DT);


            if (handler != null)
            {
                handler(this, new EventArgs());
            }

        }

        private void OpenImport(object sender, RoutedEventArgs e)
        {
            //ImportLoans loan = new SRI.ImportLoans();
            //loan.DataChanged += Validate;
            //loan.ShowDialog();
        }

        private void Validate(object sender, EventArgs e)
        {

        }


        private void SendEmail(DataTable dt, string Title, string Body)
        {

            try
            {
                MailMessage mail = new MailMessage();
                MailAddress fromMail = new MailAddress("email@gmail.com");
                mail.From = fromMail;

                foreach (DataRow _dtrow in dt.Rows)
                {
                    mail.To.Add(new MailAddress(_dtrow["email"].ToString(), _dtrow["DisplayName"].ToString()));
                }

                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "email local host";
                mail.Subject = Title;
                mail.Body = Body.ToString();
                mail.IsBodyHtml = true;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    Attach x = (Attach)Lst.SelectedItem;

        //    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

        //    // dlg.Filter = "Word Docs (*.docx; *.doc)|*.docx; *.doc|PDF (*.pdf;)|*.pdf|Text (*.txt;)|*.txt| Excel (*.xls*;)|*.xls*"; // Filter files by extension
        //    dlg.FileName = x.Name;
        //    // Show save file dialog box
        //    Nullable<bool> result = dlg.ShowDialog();

        //    // Process save file dialog box results
        //    if (result == true)
        //    {
        //        // Save document
        //        string filename = dlg.FileName;
        //        try
        //        {
        //            try
        //            {
        //                db.DownloadFile(x.ID, filename);
        //            }
        //            catch (Exception ex)
        //            {

        //                MessageBox.Show(ex.Message, "Downloading Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //                return;
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            MessageBox.Show(ex.Message);
        //        }

        //    }
        //}


        //private ObservableCollection<string> _files = new ObservableCollection<string>();
        //public ObservableCollection<string> Files
        //{
        //    get
        //    {
        //        return _files;
        //    }
        //}

        //private void Upload_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    //ofd.Filter = "Word Docs (*.docx; *.doc)|*.docx; *.doc|PDF (*.pdf;)|*.pdf|Text (*.txt;)|*.txt| Excel (*.xls*;)|*.xls*";
        //    ofd.RestoreDirectory = true;
        //    ofd.Multiselect = true;
        //    if (ofd.ShowDialog() == true)
        //    {


        //        foreach (string file in ofd.FileNames)
        //        {
        //            _files.Add(file);
        //        }

        //        DropBox.ItemsSource = _files;
        //    }



        //}
    }
}
