using System;
using System.Diagnostics;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MyPlugins;

namespace MathNet3x
{
    public class MathNet3x : IMyPlugin
    {
        public MathNet3x()
        {
            Control.UseNativeMKL();
            // Using the Intel MKL native provider
            Console.WriteLine("MathNet3x uses " + Control.LinearAlgebraProvider);
        }

        private void WorkWithMKL()
        {
            Console.WriteLine("MathNet3x uses " + Control.LinearAlgebraProvider);

            var m = Matrix<double>.Build.Random(500, 500, 0);
            var v = Vector<double>.Build.Random(500, 0);

            var w = Stopwatch.StartNew();
            var y1 = m.Solve(v);
            Console.WriteLine(w.Elapsed);
            Console.WriteLine(y1);
        }

        public string GetResult(string path)
        {
            WorkWithMKL();
            return $"Hello from MathNet3x n={0}";
        }
    }
}