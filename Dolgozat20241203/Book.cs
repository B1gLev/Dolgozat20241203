using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolgozat20241203;

internal class Book
{
    public string ISBN { get; private set; }
    public List<Author> Authors { get; private set; }
    public string Title { get; private set; }
    public int PublicationYear { get; private set; }
    public string Language { get; private set; }
    public int Stock { get; set; }
    public int Price { get; private set; }

    public Book(string iSBN, string title, string language, int publicationYear, int stock, int price, params string[] authors)
    {
        if (string.IsNullOrWhiteSpace(iSBN) || iSBN.Length != 10)
            throw new ArgumentException("Az ISBN egy 10 számjegyű érvényes számsor kell legyen.");


        if (string.IsNullOrWhiteSpace(title) || title.Length < 3 || title.Length > 64)
            throw new ArgumentException("A cím minimum 3, maximum 64 karakter hosszú lehet.");

        if (publicationYear < 2007 || publicationYear > DateTime.Now.Year)
            throw new ArgumentException("A kiadás éve 2007 és a jelen év közé kell essen.");

        if (language != "angol" && language != "német" && language != "magyar")
            throw new ArgumentException("Csak az angol, német vagy magyar nyelv elfogadott.");

        if (price < 1000 || price > 10000 || price % 100 != 0)
            throw new ArgumentException("Az ár 1000 és 10000 közötti, kerek 100-as szám kell legyen.");

        if (stock < 0)
            throw new ArgumentException("A készlet nem lehet kevesebb mint 0-a.");

        if (authors.Length < 1 || authors.Length > 3)
            throw new ArgumentException("A szerzők száma 1 és 3 közé kell essen.");

        ISBN = iSBN;
        Title = title;
        PublicationYear = publicationYear;
        Language = language;
        Price = price;
        Stock = stock;

        Authors = [];
        foreach (var author in authors)
            Authors.Add(new Author(author));
    }

    public Book(string author, string title): this("", title, "magyar", 2024, 0, 4500)
    {
        this.ISBN = Helper.GenerateRandomISBN();
        this.Authors = [];
        this.Authors.Add(new Author(author));
    }

    public override string? ToString() => $"{(Authors.Count == 1 ? "szerző" : "szerzők")}: {string.Join(", ", Authors)}\n" +
               $"Cím: {Title}\n" +
               $"Kiadás éve: {PublicationYear}\n" +
               $"Nyelv: {Language}\n" +
               $"Ár: {Price} Ft\n" +
               $"Készlet: {(Stock > 0 ? $"{Stock} db" : "beszerzés alatt")}\n" +
               $"ISBN: {ISBN}";

}
