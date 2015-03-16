**Note**: This project was imported from Google-Code when they shut down, it is no longer active.

# LabSharp

Lab# is a .Net library written in C# 2.0 to interact with MATLAB® from any .Net application. 

Technically Lab# is a wrapper around the [MATLAB engine API](http://www.mathworks.com/access/helpdesk/help/techdoc/apiref/bqoqnz0.html). This API allow you to get and put matrices in MATLAB.

The target of the library is to become fully capable to send and receive data from MATLAB and Scilab, both to running instances and to compatible binary files.

## Examples

```csharp
// This example create an array with the values of the function
// f(x) = sin(x / 10); on the [0..100] range and display it in matlab.

using System;
using LabSharp;

class Program
{
    static void Main(string[] args)
    {
        double[] sin = new double[100];

        for (int i = 0; i < sin.Length; i++)
        {
            sin[i] = Math.Sin(i / 10.0);
        }
        using (Engine eng = Engine.Open())
        {
            eng.SetVariable("sin", sin);
            eng.Eval("plot(sin); figure(gcf)");
        }
    }
}
```

```csharp
// This exemple fill an array with random values, send it to MATLAB and use
// MATLAB to calculate the mean value, then display the result in C#.

using System;
using LabSharp;

class Program
{
    static void Main(string[] args)
    {
        Random rnd = new Random();
        double[] dataArray = new double[100];
        for (int i = 0; i < dataArray.Length; i++)
        {
            dataArray[i] = rnd.NextDouble() * 100;
        }
        using (Engine eng = Engine.Open())
        {
            eng.SetVariable("data", dataArray);
            eng.Eval("data_mean = mean(data)");
            Console.WriteLine("The mean is : {0}", eng.GetVariable<double>("data_mean"));

            // Clean up
            eng.Eval("clear data");
            eng.Eval("clear data_mean");
        }
        Console.ReadKey();
    }
}
```
