# Prime Number Calculator - Windows Forms Application

![Demo Video](https://github.com/user-attachments/assets/aec7413a-77f3-4d1e-92fd-a5286f78b571)

A multi-threaded Windows Forms desktop application built with C# .NET 8 that calculates prime numbers concurrently without blocking the UI.

## 📋 Problem Statement

Create a Windows Forms application with two independent calculation panels that can:

- Accept user input for maximum number
- Calculate all prime numbers up to the entered number
- Run calculations in **separate threads** (not Tasks)
- Display results in real-time without blocking the UI
- Execute both calculations simultaneously

## 🏗️ Solution Architecture

### Clean Separation of Concerns

```text
PrimeCalculator/
└─── src/
    ├─── PrimeCalculator.Logic/       # Core business logic
    │   └─── PrimeCalculator.cs       # Prime calculation algorithms
    └─── PrimeCalculatorApp/          # UI layer
        ├─── PrimeCalculatorPanel.cs  # Reusable calculation control
        └─── PrimeCalculatorMainForm.cs
tests/
    └─── PrimeCalculatorTests/        # Unit tests
```

**Key Design Principles:**

- **DRY (Don't Repeat Yourself):** Reusable `PrimeCalculatorPanel` UserControl eliminates code duplication
- **Single Responsibility:** Logic layer handles algorithms; UI layer handles presentation
- **Encapsulation:** Each panel manages its own thread lifecycle independently

## 🧵 Threading Implementation

### Core Threading Features

1. **Native Thread Usage**
   - Uses `System.Threading.Thread` (not Tasks) as per requirements
   - Background threads that don't block application exit
   - Independent thread per calculation panel

2. **Thread-Safe UI Updates**

   ```csharp
   resultListBox.Invoke(() =>
   {
       resultListBox.BeginUpdate();
       // UI updates here
       resultListBox.EndUpdate();
   });
   ```

3. **Cancellation Support**
   - Volatile `_cancelRequested` flag for thread-safe signaling
   - Immediate thread termination without race conditions

4. **Concurrent Operations**
   - Both panels can run calculations simultaneously
   - No blocking or interference between threads
   - Each thread manages its own state

### Thread Lifecycle Management

```csharp
// Thread creation and start
_calculationThread = new Thread(() => CalculatePrimes(maxNumber))
{
    IsBackground = true  // Ensures clean application shutdown
};
_calculationThread.Start();

// Thread cancellation
_cancelRequested = true;  // Volatile field for thread safety
```

## ⚡ Performance Optimizations

### Dual Algorithm Strategy

1. **Standard Algorithm** (for numbers < 10,000)
   - Trial division with square root boundary
   - Batch updates every 100 primes for smooth UI
   - Responsive cancellation checks

2. **Sieve of Eratosthenes** (for numbers ? 10,000)
   - Significantly faster for large ranges
   - Memory-efficient boolean array
   - O(n log log n) complexity

### Batching for Responsiveness

```csharp
private const int BatchSize = 100;

// Updates UI every 100 primes instead of per-prime
if (primes.Count % BatchSize == 0)
{
    AddPrimesToListBox(currentBatch);
}
```

## ✨ Clean Code Practices

### 1. **Meaningful Method Names**

```csharp
ValidateInput()
StartCalculation()
SetUIState()
CalculateWithFastAlgorithm()
```

### 2. **DRY Principle**

- Single `PrimeCalculatorPanel` UserControl reused twice
- Shared calculation logic in `PrimeCalculator.Logic` namespace
- Centralized UI state management methods

### 3. **Input Validation**

```csharp
// Validates input is a number >= 2
if (!int.TryParse(input, out maxNumber) || maxNumber < 2)
{
    ShowWarning("Please enter a valid number (2 or greater).");
}

// Enforces maximum limit for performance and memory safety
if (maxNumber > MaxAllowedNumber)
{
    ShowWarning($"Maximum allowed value is {MaxAllowedNumber:N0}.");
}
```

### 4. **Error Handling**

```csharp
try
{
    CalculateWithStandardAlgorithm(maxNumber, primes);
}
catch (OutOfMemoryException)
{
    HandleMemoryError();
}
```

### 5. **Constants for Magic Numbers**

```csharp
private const int BatchSize = 100;
private const int FastAlgorithmThreshold = 10000;
private const int MaxAllowedNumber = 10000000; // 10 million limit
```

### 6. **Small, Focused Methods**

- Each method has a single responsibility
- Average method length: 5-15 lines
- Easy to read, test, and maintain

## 🛠️ Technical Stack

- **Framework:** .NET 8
- **UI:** Windows Forms
- **Threading:** System.Threading.Thread
- **Architecture:** Multi-layered (Logic + UI)
- **Testing:** xUnit (optional)

## 💡 Key Takeaways

• **Thread Safety:** Proper use of `Invoke()` for cross-thread UI updates
• **Concurrency:** Multiple calculations run simultaneously without conflicts
• **Responsiveness:** UI remains interactive during heavy computations
• **Maintainability:** Clean separation enables easy testing and extension
• **Performance:** Intelligent algorithm selection based on input size  

## 📚 Learning Highlights

This project demonstrates:

- Manual thread management and lifecycle control
- Cross-thread communication in Windows Forms
- Volatile fields for thread synchronization
- Component reusability through UserControls
- Clean architecture in desktop applications
