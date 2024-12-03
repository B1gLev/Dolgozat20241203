using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolgozat20241203;

internal class Author
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Guid Id { get; private set; }

    public Author(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
             throw new ArgumentException("A név nem lehet üres vagy csak szóközöket tartalmazhat.");

        var nameParts = fullName.Split(' ');

        if (nameParts.Length != 2)
            throw new ArgumentException("A név pontosan két részből (keresztnév és vezetéknév) kell álljon, szóközzel elválasztva.");

        if (nameParts[0].Length < 3 || nameParts[0].Length > 32)
            throw new ArgumentException("A keresztnév minimum 3, maximum 32 karakter hosszú lehet.");

        if (nameParts[1].Length < 3 || nameParts[1].Length > 32)
            throw new ArgumentException("A vezetéknév minimum 3, maximum 32 karakter hosszú lehet.");

        FirstName = nameParts[0];
        LastName = nameParts[1];
        Id = Guid.NewGuid();
    }
}
