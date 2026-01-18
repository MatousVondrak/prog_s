using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rgb_michacka
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void UpdateColor()
        {
            
            byte r = (byte)sldRed.Value;
            byte g = (byte)sldGreen.Value;
            byte b = (byte)sldBlue.Value;

    
            Color c = Color.FromRgb(r, g, b);
            recColor.Fill = new SolidColorBrush(c);

          
            lblHex.Content = c.ToString();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
         
            Slider slider = sender as Slider;

        
           if (slider == sldRed) txtRed.Text = slider.Value.ToString();
            if (slider == sldGreen) txtGreen.Text = slider.Value.ToString();
            if (slider == sldBlue) txtBlue.Text = slider.Value.ToString();

     
            UpdateColor();
        }

      
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            
            if (string.IsNullOrEmpty(textBox.Text)) return;


            if (int.TryParse(textBox.Text, out int cislo))
            {
              
                if (cislo > 255)
                {
                    MessageBox.Show("Můžeš zadávat jen hodnoty v rozmezí 0-255.", "Chyba");
                    textBox.Text = "255"; 
                    cislo = 255;          

             
                    textBox.CaretIndex = textBox.Text.Length;
                }

                
                if (textBox == txtRed && sldRed.Value != cislo) sldRed.Value = cislo;
                 if (textBox == txtGreen && sldGreen.Value != cislo) sldGreen.Value = cislo;
                if (textBox == txtBlue && sldBlue.Value != cislo) sldBlue.Value = cislo;
            }
        }


        private void Integer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
         
        e.Handled = !e.Text.All(cc => Char.IsNumber(cc));
        }
    }
}