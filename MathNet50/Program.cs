using System;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Providers.LinearAlgebra;
using MyPlugins;

namespace MathNet50
{
    public class MathNet50 : IMyPlugin
    {       
        public MathNet50()
        {
            Control.UseNativeMKL();
            // Using the Intel MKL native provider
            Console.WriteLine("MathNet50 uses " + LinearAlgebraControl.Provider);
        }
        private void WorkWithMKL()
        {
            Console.WriteLine("MathNet50 uses " + LinearAlgebraControl.Provider);

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
            return $"Hello from MathNet50 n={0}";
        }
    }
}