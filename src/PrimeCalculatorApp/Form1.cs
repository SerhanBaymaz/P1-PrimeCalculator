using PrimeCalculator.Logic;

namespace PrimeCalculatorApp;

public partial class Form1 : Form
{
    private Thread? _thread1;
    private Thread? _thread2;
    private volatile bool _cancelThread1;
    private volatile bool _cancelThread2;

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
        buttonCancel1.Enabled = true; // Cancel butonunu aktif et
        buttonCancel1.BackColor = Color.IndianRed; // Kýrmýzý yap (thread çalýþýyor)
        buttonClean1.Enabled = false; // Clean butonunu deaktif et (hesaplama çalýþýyor)
        buttonClean1.BackColor = Color.Gray; // Gri yap (deaktif)
        _cancelThread1 = false;

        _thread1 = new Thread(() => CalculatePrimes(maxNumber, listBox1, button1, buttonCancel1, 1));
        _thread1.IsBackground = true; // Form kapandýðýnda thread otomatik sonlansýn
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
        buttonCancel2.Enabled = true; // Cancel butonunu aktif et
        buttonCancel2.BackColor = Color.IndianRed; // Kýrmýzý yap (thread çalýþýyor)
        buttonClean2.Enabled = false; // Clean butonunu deaktif et (hesaplama çalýþýyor)
        buttonClean2.BackColor = Color.Gray; // Gri yap (deaktif)
        _cancelThread2 = false;

        _thread2 = new Thread(() => CalculatePrimes(maxNumber, listBox2, button2, buttonCancel2, 2));
        _thread2.IsBackground = true; // Form kapandýðýnda thread otomatik sonlansýn
        _thread2.Start();
    }

    private void buttonCancel1_Click(object sender, EventArgs e)
    {
        _cancelThread1 = true;
        buttonCancel1.Enabled = false;
        buttonCancel1.BackColor = Color.Gray; // Gri yap (thread durdu)
    }

    private void buttonCancel2_Click(object sender, EventArgs e)
    {
        _cancelThread2 = true;
        buttonCancel2.Enabled = false;
        buttonCancel2.BackColor = Color.Gray; // Gri yap (thread durdu)
    }

    private void buttonClean1_Click(object sender, EventArgs e)
    {
        // Thread çalýþýyorsa temizleme yapma
        if (_thread1 != null && _thread1.IsAlive)
        {
            MessageBox.Show("Cannot clear while calculation is running!", "Operation Not Allowed",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        listBox1.Items.Clear();
    }

    private void buttonClean2_Click(object sender, EventArgs e)
    {
        // Thread çalýþýyorsa temizleme yapma
        if (_thread2 != null && _thread2.IsAlive)
        {
            MessageBox.Show("Cannot clear while calculation is running!", "Operation Not Allowed",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        listBox2.Items.Clear();
    }

    private void CalculatePrimes(int maxNumber, ListBox targetListBox, Button targetButton, Button cancelButton, int threadNumber)
    {
        var primes = new List<int>();
        const int batchSize = 100; // Her 100 asal sayýda bir UI'ý güncelle
        const int useFastAlgorithmThreshold = 10000; // 10,000'den büyük sayýlarda hýzlý algoritma kullan

        // Ýlgili cancel flag ve clean button'u belirle
        ref bool cancelFlag = ref (threadNumber == 1 ? ref _cancelThread1 : ref _cancelThread2);
        Button cleanButton = threadNumber == 1 ? buttonClean1 : buttonClean2;

        // Büyük sayýlar için Eratosthenes Kalburu kullan (çok daha hýzlý)
        if (maxNumber > useFastAlgorithmThreshold)
        {
            try
            {
                primes = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpToFast(maxNumber);

                // Sonuçlarý batch halinde ekle
                for (int i = 0; i < primes.Count; i += batchSize)
                {
                    if (cancelFlag) break;

                    int remaining = Math.Min(batchSize, primes.Count - i);
                    var batch = primes.GetRange(i, remaining);

                    targetListBox.Invoke(() =>
                    {
                        targetListBox.BeginUpdate();
                        foreach (var prime in batch)
                        {
                            targetListBox.Items.Add(prime);
                        }
                        targetListBox.EndUpdate();
                    });

                    // UI'ýn nefes almasýný saðla
                    Thread.Sleep(1);
                }
            }
            catch (OutOfMemoryException)
            {
                targetListBox.Invoke(() =>
                {
                    MessageBox.Show($"Number too large! Try a smaller value (below {int.MaxValue / 100}).",
                        "Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }
        else
        {
            // Küçük sayýlar için normal algoritma
            for (int i = 2; i <= maxNumber; i++)
            {
                if (cancelFlag) break;

                if (PrimeCalculator.Logic.PrimeCalculator.IsPrime(i))
                {
                    primes.Add(i);

                    // Batch size'a ulaþtýðýnda UI'ý güncelle
                    if (primes.Count % batchSize == 0)
                    {
                        var currentBatch = primes.GetRange(primes.Count - batchSize, batchSize);
                        targetListBox.Invoke(() =>
                        {
                            targetListBox.BeginUpdate();
                            foreach (var prime in currentBatch)
                            {
                                targetListBox.Items.Add(prime);
                            }
                            targetListBox.EndUpdate();
                        });
                    }
                }
            }

            // Kalan sonuçlarý ekle
            if (!cancelFlag)
            {
                int remainingCount = primes.Count % batchSize;
                if (remainingCount > 0)
                {
                    var lastBatch = primes.GetRange(primes.Count - remainingCount, remainingCount);
                    targetListBox.Invoke(() =>
                    {
                        targetListBox.BeginUpdate();
                        foreach (var prime in lastBatch)
                        {
                            targetListBox.Items.Add(prime);
                        }
                        targetListBox.EndUpdate();
                    });
                }
            }
        }

        bool wasCancelled = cancelFlag;
        int totalCount = primes.Count;
        targetButton.Invoke(() =>
        {
            targetButton.Enabled = true;
            cancelButton.Enabled = false; // Cancel butonunu deaktif et
            cancelButton.BackColor = Color.Gray; // Gri yap (thread durdu)
            cleanButton.Enabled = true; // Clean butonunu aktif et (hesaplama bitti)
            cleanButton.BackColor = Color.LightSkyBlue; // Mavi yap (aktif)
            if (wasCancelled)
            {
                targetListBox.Items.Add("--- CANCELLED ---");
            }
            else
            {
                targetListBox.Items.Add($"--- TOTAL: {totalCount} primes found ---");
            }
        });
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }
}
