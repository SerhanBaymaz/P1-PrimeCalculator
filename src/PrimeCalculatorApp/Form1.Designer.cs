namespace PrimeCalculatorApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        textBox1 = new TextBox();
        textBox2 = new TextBox();
        button1 = new Button();
        button2 = new Button();
        buttonCancel1 = new Button();
        buttonCancel2 = new Button();
        buttonClean1 = new Button();
        buttonClean2 = new Button();
        listBox1 = new ListBox();
        listBox2 = new ListBox();
        label1 = new Label();
        label2 = new Label();
        SuspendLayout();
        // 
        // textBox1
        // 
        textBox1.Location = new Point(30, 40);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(150, 23);
        textBox1.TabIndex = 0;
        // 
        // textBox2
        // 
        textBox2.Location = new Point(420, 40);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(150, 23);
        textBox2.TabIndex = 1;
        // 
        // button1
        // 
        button1.Location = new Point(200, 39);
        button1.Name = "button1";
        button1.Size = new Size(100, 25);
        button1.TabIndex = 2;
        button1.Text = "Calculate 1";
        button1.UseVisualStyleBackColor = true;
        // 
        // button2
        // 
        button2.Location = new Point(590, 39);
        button2.Name = "button2";
        button2.Size = new Size(100, 25);
        button2.TabIndex = 3;
        button2.Text = "Calculate 2";
        button2.UseVisualStyleBackColor = true;
        // 
        // buttonCancel1
        // 
        buttonCancel1.BackColor = Color.Gray;
        buttonCancel1.Enabled = false;
        buttonCancel1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        buttonCancel1.Location = new Point(310, 39);
        buttonCancel1.Name = "buttonCancel1";
        buttonCancel1.Size = new Size(30, 30);
        buttonCancel1.TabIndex = 8;
        buttonCancel1.Text = "✖";
        buttonCancel1.UseVisualStyleBackColor = false;
        // 
        // buttonCancel2
        // 
        buttonCancel2.BackColor = Color.Gray;
        buttonCancel2.Enabled = false;
        buttonCancel2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        buttonCancel2.Location = new Point(700, 39);
        buttonCancel2.Name = "buttonCancel2";
        buttonCancel2.Size = new Size(30, 30);
        buttonCancel2.TabIndex = 9;
        buttonCancel2.Text = "✖";
        buttonCancel2.UseVisualStyleBackColor = false;
        // 
        // buttonClean1
        // 
        buttonClean1.BackColor = Color.LightSkyBlue;
        buttonClean1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        buttonClean1.Location = new Point(345, 39);
        buttonClean1.Name = "buttonClean1";
        buttonClean1.Size = new Size(30, 30);
        buttonClean1.TabIndex = 10;
        buttonClean1.Text = "🗑";
        buttonClean1.UseVisualStyleBackColor = false;
        // 
        // buttonClean2
        // 
        buttonClean2.BackColor = Color.LightSkyBlue;
        buttonClean2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        buttonClean2.Location = new Point(735, 39);
        buttonClean2.Name = "buttonClean2";
        buttonClean2.Size = new Size(30, 30);
        buttonClean2.TabIndex = 11;
        buttonClean2.Text = "🗑";
        buttonClean2.UseVisualStyleBackColor = false;
        // 
        // listBox1
        // 
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 15;
        listBox1.Location = new Point(30, 80);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(270, 349);
        listBox1.TabIndex = 4;
        listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        // 
        // listBox2
        // 
        listBox2.FormattingEnabled = true;
        listBox2.ItemHeight = 15;
        listBox2.Location = new Point(420, 80);
        listBox2.Name = "listBox2";
        listBox2.Size = new Size(270, 349);
        listBox2.TabIndex = 5;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(30, 17);
        label1.Name = "label1";
        label1.Size = new Size(91, 15);
        label1.TabIndex = 6;
        label1.Text = "Enter a number:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(420, 17);
        label2.Name = "label2";
        label2.Size = new Size(91, 15);
        label2.TabIndex = 7;
        label2.Text = "Enter a number:";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(820, 450);
        Controls.Add(buttonClean2);
        Controls.Add(buttonClean1);
        Controls.Add(buttonCancel2);
        Controls.Add(buttonCancel1);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(listBox2);
        Controls.Add(listBox1);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(textBox2);
        Controls.Add(textBox1);
        Name = "Form1";
        Text = "Prime Calculator - Multithreaded";
        Load += Form1_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox textBox1;
    private TextBox textBox2;
    private Button button1;
    private Button button2;
    private Button buttonCancel1;
    private Button buttonCancel2;
    private Button buttonClean1;
    private Button buttonClean2;
    private ListBox listBox1;
    private ListBox listBox2;
    private Label label1;
    private Label label2;
}
