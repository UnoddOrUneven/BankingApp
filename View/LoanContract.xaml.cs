using System.Windows.Controls;
using System.Windows.Media;
using BankingApp.Models;

namespace BankingApp.View;

public partial class LoanContract : Page
{
    public LoanContract(User? user , Account? account)
    {
        InitializeComponent();
        SignatureCanvas.DefaultDrawingAttributes.Color = Color.FromArgb(200, 255, 30, 15);
        SignatureCanvas.Background = Brushes.Transparent; 
        Agreement.Text = $"I, {user.Name}, agree";
    }
}