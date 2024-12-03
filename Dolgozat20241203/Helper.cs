using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolgozat20241203;

internal class Helper
{
    private static readonly Random random = new();

    public static string GenerateRandomISBN()
    {
        string isbn = "";
        for (int i = 0; i < 10; i++)
            isbn += random.Next(0, 10).ToString();
        return isbn;
    }
}
