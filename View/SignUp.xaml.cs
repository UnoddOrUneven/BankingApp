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
    private void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
        
        
        
    }
}