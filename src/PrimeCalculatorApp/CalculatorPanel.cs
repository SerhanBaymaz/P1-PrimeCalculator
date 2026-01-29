namespace PrimeCalculatorApp;

/// <summary>
/// Encapsulates the logic and UI controls for a single prime calculator panel.
/// This class follows the Single Responsibility Principle by managing only one calculator instance.
/// </summary>
public class CalculatorPanel
{
    private readonly TextBox _inputTextBox;
    private readonly ListBox _resultListBox;
    private readonly Button _calculateButton;
    private readonly Button _cancelButton;
    private readonly Button _cleanButton;

    private Thread? _calculationThread;
    private volatile bool _cancelRequested;

    private const int BatchSize = 100;
    private const int FastAlgorithmThreshold = 10000;

    public CalculatorPanel(
        TextBox inputTextBox,
        ListBox resultListBox,
        Button calculateButton,
        Button cancelButton,
        Button cleanButton)
    {
        _inputTextBox = inputTextBox ?? throw new ArgumentNullException(nameof(inputTextBox));
        _resultListBox = resultListBox ?? throw new ArgumentNullException(nameof(resultListBox));
        _calculateButton = calculateButton ?? throw new ArgumentNullException(nameof(calculateButton));
        _cancelButton = cancelButton ?? throw new ArgumentNullException(nameof(cancelButton));
        _cleanButton = cleanButton ?? throw new ArgumentNullException(nameof(cleanButton));

        AttachEventHandlers();
    }

    private void AttachEventHandlers()
    {
        _calculateButton.Click += OnCalculateClick;
        _cancelButton.Click += OnCancelClick;
        _cleanButton.Click += OnCleanClick;
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

        _resultListBox.Items.Clear();
    }

    private bool ValidateInput(out int maxNumber)
    {
        if (!int.TryParse(_inputTextBox.Text, out maxNumber) || maxNumber < 2)
        {
            MessageBox.Show("Please enter a valid number (2 or greater).", "Invalid Input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private void StartCalculation(int maxNumber)
    {
        _resultListBox.Items.Clear();
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
        _calculateButton.Enabled = !isCalculating;
        SetCancelButtonState(enabled: isCalculating);
        SetCleanButtonState(enabled: !isCalculating);
    }

    private void SetCancelButtonState(bool enabled)
    {
        _cancelButton.Enabled = enabled;
        _cancelButton.BackColor = enabled ? Color.IndianRed : Color.Gray;
    }

    private void SetCleanButtonState(bool enabled)
    {
        _cleanButton.Enabled = enabled;
        _cleanButton.BackColor = enabled ? Color.LightSkyBlue : Color.Gray;
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
        }
        catch (OutOfMemoryException)
        {
            HandleMemoryError();
            return;
        }

        DisplayResults(primes);
    }

    private void CalculateWithFastAlgorithm(int maxNumber, List<int> primes)
    {
        primes.AddRange(PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpToFast(maxNumber));

        for (int i = 0; i < primes.Count; i += BatchSize)
        {
            if (_cancelRequested) break;

            int remaining = Math.Min(BatchSize, primes.Count - i);
            var batch = primes.GetRange(i, remaining);

            AddPrimesToListBox(batch);
            Thread.Sleep(1);
        }
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
        _resultListBox.Invoke(() =>
        {
            _resultListBox.BeginUpdate();
            foreach (var prime in batch)
            {
                _resultListBox.Items.Add(prime);
            }
            _resultListBox.EndUpdate();
        });
    }

    private void HandleMemoryError()
    {
        _resultListBox.Invoke(() =>
        {
            MessageBox.Show($"Number too large! Try a smaller value (below {int.MaxValue / 100}).",
                "Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        });
    }

    private void DisplayResults(List<int> primes)
    {
        bool wasCancelled = _cancelRequested;
        int totalCount = primes.Count;

        _resultListBox.Invoke(() =>
        {
            SetUIState(isCalculating: false);

            if (wasCancelled)
            {
                _resultListBox.Items.Add("--- CANCELLED ---");
            }
            else
            {
                _resultListBox.Items.Add($"--- TOTAL: {totalCount} primes found ---");
            }
        });
    }
}
