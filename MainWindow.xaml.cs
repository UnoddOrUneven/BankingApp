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
using BankingApp.View;

namespace BankingApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
    }
    
    
    private void ForgotPassword_Click(object sender, RoutedEventArgs e)
    {
        ResetPassword resetWindow = new ResetPassword();
        resetWindow.ShowDialog();  // modal
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (tb.Text == "Password" || tb.Text == "Login")
        {
            tb.Text = "";
        }
    }
    
    private void Password_LostFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (string.IsNullOrEmpty(tb.Text))
        {
            tb.Text = "Password";
        }
    }
    
    private void Login_LostFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (string.IsNullOrEmpty(tb.Text))
        {
            tb.Text = "Login";
        }
    }
   
    

}