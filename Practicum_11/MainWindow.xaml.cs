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
using System.IO;
using System.Drawing;

namespace Practicum_11
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            InitializeComponent();
        }
        private string OpenDialogWithFilter(String filter)
        {
            Microsoft.Win32.OpenFileDialog odlg = new Microsoft.Win32.OpenFileDialog();
            odlg.InitialDirectory = @"C:\Users\904\Desktop\Папка 11";
            odlg.Filter = filter;
            if (!odlg.ShowDialog() == true) 
                return null;
            return odlg.FileName;
        }

        private void OpenAsUnicodeEncoding(object sender, RoutedEventArgs e)
        {
            string file = OpenDialogWithFilter("Text files (*.txt)|*.txt");
            var information = new FileInfo(file);
            if (information.Length < 6)
                MessageBox.Show("Файл пустой");
            Box.Document.Blocks.Clear();
            Box.AppendText(File.ReadAllText(file));
        }

        private void OpenAsWinEncoding(object sender, RoutedEventArgs e)
        {
            string file = OpenDialogWithFilter("Text Files (*.txt)|*.txt");
            var information = new FileInfo(file);
            if (information.Length < 6)
                MessageBox.Show("Файл пустой");
            Box.Document.Blocks.Clear();
            string myText = File.ReadAllText(file, Encoding.GetEncoding("windows-1251"));
            Box.AppendText(myText);
        }

        private void OpenAsRtfEncoding(object sender, RoutedEventArgs e)
        {
            string file = OpenDialogWithFilter("RichText Files (*.rtf)|*.rtf");
            var information = new FileInfo(file);
            if (information.Length < 6)
                MessageBox.Show("Файл пустой");
            Box.Document.Blocks.Clear();
            TextRange txtRange = null;
            FileStream stream = new FileStream(file, FileMode.Open);
            txtRange = new TextRange(Box.Document.ContentStart, Box.Document.ContentEnd);
            txtRange.Load(stream, DataFormats.Rtf);
        }

        private void OpenAsBinaryEncoding(object sender, RoutedEventArgs e)
        {
            string file = OpenDialogWithFilter("Binary Files (*.dat)|*.dat");
            var information = new FileInfo(file);
            if (information.Length < 6)
                MessageBox.Show("Файл пустой");
            Box.Document.Blocks.Clear();
            byte[] fileBytes = File.ReadAllBytes(file);
            string FileString = Encoding.UTF8.GetString(fileBytes);
            Box.AppendText(FileString);
        }
        private string SaveDialogWithFilter(String filter)
        {
            Microsoft.Win32.SaveFileDialog sdlg = new Microsoft.Win32.SaveFileDialog();
            sdlg.InitialDirectory = @"C:\Users\904\Desktop\Папка 11";
            sdlg.Filter = filter;
            if (!sdlg.ShowDialog() == true)
                return null;
            return sdlg.FileName;
        }

        private void SaveAsUnicodeEncoding(object sender, RoutedEventArgs e)
        {
            string file = SaveDialogWithFilter("Text Files (*.txt)|*.txt");
            string text = new TextRange(Box.Document.ContentStart, Box.Document.ContentEnd).Text;
            File.WriteAllText(file, text);
        }

        private void SaveAsWinEncoding(object sender, RoutedEventArgs e)
        {
            string file = SaveDialogWithFilter("Text Files (*.txt)|*.txt");
            string text = new TextRange(Box.Document.ContentStart, Box.Document.ContentEnd).Text;
            File.WriteAllText(file, text, Encoding.GetEncoding("windows-1251"));
        }

        private void SaveAsRtfEncoding(object sender, RoutedEventArgs e)
        {
            string file = SaveDialogWithFilter("RichText Files (*.rtf)|*.rtf");
            new TextRange(Box.Document.ContentStart, Box.Document.ContentEnd).Save(File.Create(file), DataFormats.Rtf);
        }

        private void SaveAsBinaryEncoding(object sender, RoutedEventArgs e)
        {
            string file = SaveDialogWithFilter("Binary Files (*.dat)|*.dat");
            string text = new TextRange(Box.Document.ContentStart, Box.Document.ContentEnd).Text;
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            File.WriteAllBytes(file, bytes);
        }
        private void PrintFile(object sender, RoutedEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument doc = Box.Document;
                double pageHeight = doc.PageHeight;
                double pageWidth = doc.PageWidth;
                Thickness pagePadding = doc.PagePadding;
                double columnGap = doc.ColumnGap;
                double columnWidth = doc.ColumnWidth;
                doc.PageHeight = printDialog.PrintableAreaHeight;
                doc.PageWidth = printDialog.PrintableAreaWidth;
                doc.PagePadding = new Thickness(0);

                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "Печать");

                doc.PageHeight = pageHeight;
                doc.PageWidth = pageWidth;
                doc.PagePadding = pagePadding;
                doc.ColumnGap = columnGap;
                doc.ColumnWidth = columnWidth;
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            const string message = "Are you sure you want to close the application?";
            const string caption = "Form closing";
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Environment.Exit(0);
        }
    }
}
