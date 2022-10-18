using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace ReceiptList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String sourcePath;
        List<Recipe> recipes;
        public MainWindow()
        {   InitializeComponent();  }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            image.Source = new BitmapImage(new Uri(recipes[listBox.SelectedIndex].Image));
            FlowDocument doc = new FlowDocument();
            
            doc.Blocks.Add(new Paragraph(new Run(recipes[listBox.SelectedIndex].Name)));
            doc.Blocks.Add(new Paragraph(new Run(recipes[listBox.SelectedIndex].Composition)));
            doc.Blocks.Add(new Paragraph(new Run(recipes[listBox.SelectedIndex].Instruction)));
            flowDoc.Document = doc;
        }

        private void flowDoc_Loaded(object sender, RoutedEventArgs e)
        {
            using (var sr = new StreamReader(sourcePath))
            {
                recipes = JsonSerializer.Deserialize<List<Recipe>>(sr.ReadToEnd());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            recipes = new List<Recipe>();
            sourcePath = @"C:\Users\3\source\repos\ReceiptList\ReceiptList\all_recipes.json";
        }
    }

    class Recipe
    {
        public String Name { get; set; }
        public String Composition { get; set; }
        public String Image { get; set; }
        public String Instruction { get; set; }  
    }
}
            