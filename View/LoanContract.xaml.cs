using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BankingApp.Models;

namespace BankingApp.View;

public partial class LoanContract : Page
{
    private User _user;
    private Account _account;
    private decimal _loanAmount;

    public LoanContract(User user, Account account, decimal loanAmount)
    {
        _user = user;
        _account = account;
        _loanAmount = loanAmount;

        InitializeComponent();
        SetLoanAmount();
        SetAgreement();
        ConfigureCanvas();
    }

    private void ConfigureCanvas()
    {
        SignatureCanvas.DefaultDrawingAttributes.Color = Color.FromArgb(200, 255, 30, 15);
        SignatureCanvas.Background = Brushes.Transparent;
    }

    private void SetAgreement()
    {
        Agreement.Text = $"I, {_user.Name}, agree";
    }

    private void SetLoanAmount()
    {
        LoanAmountTextBlock.Text = $"${_loanAmount.ToString("N")}";
    }

    private bool IsSigned()
    {
        return SignatureCanvas.Strokes.Count > 0;
    }

    private void RefreshSaveSignature()
    {
        SaveSignatureButton.IsEnabled = IsSigned();
    }

    private void SignatureCanvas_OnMouseMove(object sender, MouseEventArgs e)
    {
        RefreshSaveSignature();
    }

    private void SaveSignatureButton_OnClick(object sender, RoutedEventArgs e)
    {
        GiveLoan();
        Window.GetWindow(this)?.Close();
    }

    private void GiveLoan()
    {
        _account.Deposit(_loanAmount);
        _user.Debt.Date = DateTime.Now;
        _user.Debt.Amount = _loanAmount;
        _user.Debt.MonthlyInterestRate = 0.2m;
    }
    
}