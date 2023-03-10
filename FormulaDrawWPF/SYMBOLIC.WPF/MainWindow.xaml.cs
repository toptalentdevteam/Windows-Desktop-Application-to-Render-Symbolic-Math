using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Analytics;
using Exversion;
using Exversion.Analytics;

namespace TeXConverter.WPF
{
    /// <summary>
    /// ANALYTICS and Physics project.
    /// https://vk.com/analytics_and_physics
    /// </summary>
    public partial class MainWindow : Window
    {
        private Translator translator;
        private BaseConverter converter;

        public MainWindow()
		{
			InitializeComponent();
		}

		private void LoadExtensions(string[] exts)
	    {
			int n = exts.Length;

			for (int i = 0; i < n; i++)
			{
				string sname = exts[i];

				try
				{
					Assembly.Load(sname);
				}
				catch (Exception)
				{
				}
			}
	    }

		private void InitializeAnalytics()
		{
            string[] exts = new string[] { "Analytics.Real" };
			LoadExtensions(exts);

            converter = new AnalyticsTeXConverter();
            translator = new Translator();
            inputTextBox1.Focus();
        }
        
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		    InitializeAnalytics();
        }

        private void ButtonEvaluate_Click(object sender, RoutedEventArgs e)
        {
            string text1 = inputTextBox1.Text;
            string text2 = inputTextBox2.Text;
            string result;

            if(text1 == text2)
            {
                result = "Two expressions are equivalent.";                
            }
            else
            {
                result = "Two expressions aren't equivalent.";
            }

            System.Windows.MessageBox.Show(result, "Result", MessageBoxButton.OK, MessageBoxImage.Information);                 
            
        }

        // This function is called when the text of the text editor is changed.
        private void OnTextChange1(object sender, TextChangedEventArgs e)
        {
            string text = inputTextBox1.Text;

            if(text.Contains("sqrt"))
            {
                text = text.Replace("sqrt", "√");
                inputTextBox1.Text = text;
                inputTextBox1.Select(inputTextBox1.Text.Length, 0);
            }

            if(text == null)
            {
                return;
            }

            try
            {
                string texf = converter.Convert(text);
                formulaControl1.Formula = texf;                
            }
            catch (Exception ex)
            {
                formulaControl1.Formula = ex.Message;
            }
        }

        private void OnTextChange2(object sender, TextChangedEventArgs e)
        {
            string text = inputTextBox2.Text;

            if (text.Contains("sqrt"))
            {
                text = text.Replace("sqrt", "√");
                inputTextBox2.Text = text;
                inputTextBox2.Select(inputTextBox2.Text.Length, 0);
            }

            if (text == null)
            {
                return;
            }

            try
            {
                string texf = converter.Convert(text);
                formulaControl2.Formula = texf;

            }
            catch (Exception ex)
            {
                formulaControl2.Formula = ex.Message;
            }
        }
    }

}
