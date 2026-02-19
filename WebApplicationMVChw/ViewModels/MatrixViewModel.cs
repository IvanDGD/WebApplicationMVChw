namespace WebApplicationMVChw.ViewModels
{
    public class MatrixViewModel
    {
        public int Size { get; set; } = 3;
        public string Op { get; set; } = "+";
        public bool ShowResult { get; set; } = false;
        public double[] A { get; set; } = new double[81];
        public double[] B { get; set; } = new double[81];
        public double[] R { get; set; } = new double[81];
    }
}
