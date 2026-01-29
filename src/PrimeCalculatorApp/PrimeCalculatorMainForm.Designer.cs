namespace PrimeCalculatorApp;

partial class PrimeCalculatorMainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        primeCalculatorPanel1 = new PrimeCalculatorPanel();
        primeCalculatorPanel2 = new PrimeCalculatorPanel();
        SuspendLayout();
        // 
        // primeCalculatorPanel1
        // 
        primeCalculatorPanel1.Location = new Point(12, 12);
        primeCalculatorPanel1.Name = "primeCalculatorPanel1";
        primeCalculatorPanel1.Size = new Size(360, 430);
        primeCalculatorPanel1.TabIndex = 0;
        // 
        // primeCalculatorPanel2
        // 
        primeCalculatorPanel2.Location = new Point(390, 12);
        primeCalculatorPanel2.Name = "primeCalculatorPanel2";
        primeCalculatorPanel2.Size = new Size(360, 430);
        primeCalculatorPanel2.TabIndex = 1;
        // 
        // PrimeCalculatorMainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(764, 461);
        Controls.Add(primeCalculatorPanel2);
        Controls.Add(primeCalculatorPanel1);
        Name = "PrimeCalculatorMainForm";
        Text = "Prime Calculator - Multithreaded";
        ResumeLayout(false);
    }

    #endregion

    private PrimeCalculatorPanel primeCalculatorPanel1;
    private PrimeCalculatorPanel primeCalculatorPanel2;
}
