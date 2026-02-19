using Microsoft.AspNetCore.Mvc;
using WebApplicationMVChw.ViewModels;

namespace WebApplicationMVChw.Controllers
{
    public class MatrixController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View(new MatrixViewModel());

        [HttpPost]
        public IActionResult Index(MatrixViewModel vm)
        {
            if (vm.Size != 3 && vm.Size != 6 && vm.Size != 9) vm.Size = 3;
            if (vm.Op != "+" && vm.Op != "*") vm.Op = "+";

            var n = vm.Size;
            var count = n * n;

            vm.A = EnsureLength(vm.A, count);
            vm.B = EnsureLength(vm.B, count);

            vm.R = new double[count];

            if (vm.Op == "+")
            {
                for (var i = 0; i < count; i++)
                    vm.R[i] = vm.A[i] + vm.B[i];
            }
            else
            {
                for (var i = 0; i < count; i++)
                    vm.R[i] = vm.A[i] * vm.B[i];
            }

            vm.ShowResult = true;
            return View(vm);
        }

        private static double[] EnsureLength(double[] src, int len)
        {
            if (src == null) return new double[len];
            if (src.Length >= len) return src;

            var dst = new double[len];
            Array.Copy(src, dst, src.Length);
            return dst;
        }
    }
}
