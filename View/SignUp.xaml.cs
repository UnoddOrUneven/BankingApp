using System.Windows;
using System.Windows.Controls;

namespace BankingApp.View;

public partial class SignUp : Page
{
    public SignUp()
    {
        InitializeComponent();
    }
    
    private void SignInButton_Click(object sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
        
        
    }
    
    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (tb.Text == "Name" || tb.Text == "Password" || tb.Text == "Repeat Password")
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
    private void Repeat_Password_LostFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (string.IsNullOrEmpty(tb.Text))
        {
            tb.Text = "Repeat Password";
        }
    }
    
    private void Name_LostFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (string.IsNullOrEmpty(tb.Text))
        {
            tb.Text = "Name";
        }
    }
    
    
    
    
    private void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
        
        
        
    }
}