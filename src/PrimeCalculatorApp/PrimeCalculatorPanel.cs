namespace PrimeCalculatorApp;

/// <summary>
/// A self-contained user control for calculating prime numbers.
/// Encapsulates all UI elements and calculation logic.
/// </summary>
public partial class PrimeCalculatorPanel : UserControl
{
    private Thread? _calculationThread;
    private volatile bool _cancelRequested;

    private const int BatchSize = 100;
    private const int FastAlgorithmThreshold = 10000;

    public PrimeCalculatorPanel()
    {
        InitializeComponent();
        AttachEventHandlers();
    }

    private void AttachEventHandlers()
    {
        calculateButton.Click += OnCalculateClick;
        cancelButton.Click += OnCancelClick;
        cleanButton.Click += OnCleanClick;
    }

    private void OnCalculateClick(object? sender, EventArgs e)
    {
        if (!ValidateInput(out int maxNumber))
        {
            return;
        }

        StartCalculation(maxNumber);
    }

    private void OnCancelClick(object? sender, EventArgs e)
    {
        CancelCalculation();
    }

    private void OnCleanClick(object? sender, EventArgs e)
    {
        if (IsCalculationRunning())
        {
            MessageBox.Show("Cannot clear while calculation is running!", "Operation Not Allowed",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        resultListBox.Items.Clear();
    }

    private bool ValidateInput(out int maxNumber)
    {
        if (!int.TryParse(inputTextBox.Text, out maxNumber) || maxNumber < 2)
        {
            MessageBox.Show("Please enter a valid number (2 or greater).", "Invalid Input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private void StartCalculation(int maxNumber)
    {
        resultListBox.Items.Clear();
        SetUIState(isCalculating: true);
        _cancelRequested = false;

        _calculationThread = new Thread(() => CalculatePrimes(maxNumber))
        {
            IsBackground = true
        };
        _calculationThread.Start();
    }

    private void CancelCalculation()
    {
        _cancelRequested = true;
        SetCancelButtonState(enabled: false);
    }

    private bool IsCalculationRunning()
    {
        return _calculationThread != null && _calculationThread.IsAlive;
    }

    private void SetUIState(bool isCalculating)
    {
        calculateButton.Enabled = !isCalculating;
        SetCancelButtonState(enabled: isCalculating);
        SetCleanButtonState(enabled: !isCalculating);
    }

    private void SetCancelButtonState(bool enabled)
    {
        cancelButton.Enabled = enabled;
        cancelButton.BackColor = enabled ? Color.IndianRed : Color.Gray;
    }

    private void SetCleanButtonState(bool enabled)
    {
        cleanButton.Enabled = enabled;
        cleanButton.BackColor = enabled ? Color.LightSkyBlue : Color.Gray;
    }

    private void CalculatePrimes(int maxNumber)
    {
        var primes = new List<int>();

        try
        {
            if (maxNumber > FastAlgorithmThreshold)
            {
                CalculateWithFastAlgorithm(maxNumber, primes);
            }
            else
            {
                CalculateWithStandardAlgorithm(maxNumber, primes);
            }

            DisplayResults(primes);
        }
        catch (OutOfMemoryException)
        {
            HandleMemoryError();
        }
    }

    private void CalculateWithFastAlgorithm(int maxNumber, List<int> primes)
    {
        primes.AddRange(PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpToFast(maxNumber));
        DisplayPrimesInBatches(primes);
    }

    private void CalculateWithStandardAlgorithm(int maxNumber, List<int> primes)
    {
        for (int i = 2; i <= maxNumber; i++)
        {
            if (_cancelRequested) break;

            if (PrimeCalculator.Logic.PrimeCalculator.IsPrime(i))
            {
                primes.Add(i);

                if (primes.Count % BatchSize == 0)
                {
                    var currentBatch = primes.GetRange(primes.Count - BatchSize, BatchSize);
                    AddPrimesToListBox(currentBatch);
                }
            }
        }

        if (!_cancelRequested)
        {
            AddRemainingPrimes(primes);
        }
    }

    private void DisplayPrimesInBatches(List<int> primes)
    {
        for (int i = 0; i < primes.Count; i += BatchSize)
        {
            if (_cancelRequested) break;

            int remaining = Math.Min(BatchSize, primes.Count - i);
            var batch = primes.GetRange(i, remaining);

            AddPrimesToListBox(batch);
            Thread.Sleep(1);
        }
    }

    private void AddRemainingPrimes(List<int> primes)
    {
        int remainingCount = primes.Count % BatchSize;
        if (remainingCount > 0)
        {
            var lastBatch = primes.GetRange(primes.Count - remainingCount, remainingCount);
            AddPrimesToListBox(lastBatch);
        }
    }

    private void AddPrimesToListBox(List<int> batch)
    {
        resultListBox.Invoke(() =>
        {
            resultListBox.BeginUpdate();
            foreach (var prime in batch)
            {
                resultListBox.Items.Add(prime);
            }
            resultListBox.EndUpdate();
        });
    }

    private void HandleMemoryError()
    {
        resultListBox.Invoke(() =>
        {
            MessageBox.Show($"Number too large! Try a smaller value (below {int.MaxValue / 100}).",
                "Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        });
    }

    private void DisplayResults(List<int> primes)
    {
        bool wasCancelled = _cancelRequested;
        int totalCount = primes.Count;

        resultListBox.Invoke(() =>
        {
            SetUIState(isCalculating: false);

            if (wasCancelled)
            {
                resultListBox.Items.Add("--- CANCELLED ---");
            }
            else
            {
                resultListBox.Items.Add($"--- TOTAL: {totalCount} primes found ---");
            }
        });
    }
}
