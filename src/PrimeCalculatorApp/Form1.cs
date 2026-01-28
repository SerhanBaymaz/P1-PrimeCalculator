using PrimeCalculator.Logic;

namespace PrimeCalculatorApp;

public partial class Form1 : Form
{
    private Thread? _thread1;
    private Thread? _thread2;

    public Form1()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(textBox1.Text, out int maxNumber) || maxNumber < 2)
        {
            MessageBox.Show("Please enter a valid number (2 or greater).", "Invalid Input", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        listBox1.Items.Clear();
        button1.Enabled = false;

        _thread1 = new Thread(() => CalculatePrimes(maxNumber, listBox1, button1));
        _thread1.Start();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(textBox2.Text, out int maxNumber) || maxNumber < 2)
        {
            MessageBox.Show("Please enter a valid number (2 or greater).", "Invalid Input", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        listBox2.Items.Clear();
        button2.Enabled = false;

        _thread2 = new Thread(() => CalculatePrimes(maxNumber, listBox2, button2));
        _thread2.Start();
    }

    private void CalculatePrimes(int maxNumber, ListBox targetListBox, Button targetButton)
    {
        var primes = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        foreach (var prime in primes)
        {
            if (targetListBox.InvokeRequired)
            {
                targetListBox.Invoke(() => targetListBox.Items.Add(prime));
            }
            else
            {
                targetListBox.Items.Add(prime);
            }
        }

        if (targetButton.InvokeRequired)
        {
            targetButton.Invoke(() => targetButton.Enabled = true);
        }
        else
        {
            targetButton.Enabled = true;
        }
    }
}
