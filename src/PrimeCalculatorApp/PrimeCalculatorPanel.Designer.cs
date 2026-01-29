namespace PrimeCalculatorApp;

partial class PrimeCalculatorPanel
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

    #region Component Designer generated code

    private void InitializeComponent()
    {
        inputLabel = new Label();
        inputTextBox = new TextBox();
        calculateButton = new Button();
        cancelButton = new Button();
        cleanButton = new Button();
        resultListBox = new ListBox();
        SuspendLayout();
        // 
        // inputLabel
        // 
        inputLabel.AutoSize = true;
        inputLabel.Location = new Point(3, 5);
        inputLabel.Name = "inputLabel";
        inputLabel.Size = new Size(91, 15);
        inputLabel.TabIndex = 0;
        inputLabel.Text = "Enter a number:";
        // 
        // inputTextBox
        // 
        inputTextBox.Location = new Point(3, 28);
        inputTextBox.Name = "inputTextBox";
        inputTextBox.Size = new Size(150, 23);
        inputTextBox.TabIndex = 1;
        // 
        // calculateButton
        // 
        calculateButton.Location = new Point(173, 27);
        calculateButton.Name = "calculateButton";
        calculateButton.Size = new Size(100, 25);
        calculateButton.TabIndex = 2;
        calculateButton.Text = "Calculate";
        calculateButton.UseVisualStyleBackColor = true;
        // 
        // cancelButton
        // 
        cancelButton.BackColor = Color.Gray;
        cancelButton.Enabled = false;
        cancelButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        cancelButton.Location = new Point(283, 27);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(30, 30);
        cancelButton.TabIndex = 3;
        cancelButton.Text = "X";
        cancelButton.UseVisualStyleBackColor = false;
        // 
        // cleanButton
        // 
        cleanButton.BackColor = Color.LightSkyBlue;
        cleanButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        cleanButton.Location = new Point(318, 27);
        cleanButton.Name = "cleanButton";
        cleanButton.Size = new Size(30, 30);
        cleanButton.TabIndex = 4;
        cleanButton.Text = "🧹";
        cleanButton.UseVisualStyleBackColor = false;
        // 
        // resultListBox
        // 
        resultListBox.FormattingEnabled = true;
        resultListBox.ItemHeight = 15;
        resultListBox.Location = new Point(3, 68);
        resultListBox.Name = "resultListBox";
        resultListBox.Size = new Size(270, 349);
        resultListBox.TabIndex = 5;
        // 
        // PrimeCalculatorPanel
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(resultListBox);
        Controls.Add(cleanButton);
        Controls.Add(cancelButton);
        Controls.Add(calculateButton);
        Controls.Add(inputTextBox);
        Controls.Add(inputLabel);
        Name = "PrimeCalculatorPanel";
        Size = new Size(360, 430);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label inputLabel;
    private TextBox inputTextBox;
    private Button calculateButton;
    private Button cancelButton;
    private Button cleanButton;
    private ListBox resultListBox;
}
