namespace PrimeCalculatorApp;

public partial class Form1 : Form
{
    private CalculatorPanel? _calculatorPanel1;
    private CalculatorPanel? _calculatorPanel2;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        InitializeCalculatorPanels();
    }

    private void InitializeCalculatorPanels()
    {
        _calculatorPanel1 = new CalculatorPanel(
            textBox1,
            listBox1,
            button1,
            buttonCancel1,
            buttonClean1);

        _calculatorPanel2 = new CalculatorPanel(
            textBox2,
            listBox2,
            button2,
            buttonCancel2,
            buttonClean2);
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
