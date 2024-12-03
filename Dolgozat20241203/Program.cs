using Dolgozat20241203;

List<string> bookTitlesHungarian = ["Az Élet Titkai", "Sötét Szavak", "Fénytörés", "Szélárnyék", "A Nap Fényében"];
List<string> bookTitlesEnglish = ["The Silent Journey", "Beyond the Horizon", "Mystery of the Stars", "Hidden Truths", "Echoes of Tomorrow"];
Random random = new();
var isbnSet = new HashSet<string>();

List<Book> GenerateRandomBooks(int count)
{
    var books = new List<Book>();

    while (books.Count < count)
    {
        var language = random.NextDouble() < 0.8 ? "magyar" : "angol";
        string title;
        if (language == "magyar")
            title = bookTitlesHungarian[random.Next(bookTitlesHungarian.Count)];
        else
            title = bookTitlesEnglish[random.Next(bookTitlesEnglish.Count)];


        var authorCount = random.NextDouble() < 0.7 ? 1 : (random.NextDouble() < 0.5 ? 2 : 3);
        var authors = GenerateRandomAuthors(authorCount);

        int price = random.Next(10, 101) * 100;

        int publicationYear = random.Next(2007, DateTime.Now.Year + 1);
        int stock = random.Next(0, 101) <= 30 ? 0 : random.Next(5, 11);

        string isbn;
        do
        {
            isbn = Helper.GenerateRandomISBN();
        } while (isbnSet.Contains(isbn));
        isbnSet.Add(isbn);

        var book = new Book(isbn, title, language, publicationYear, stock, price, authors.ToArray());
        books.Add(book);
    }

    return books;
}

List<string> GenerateRandomAuthors(int count)
{
    List<string> authors = [];
    for (int i = 0; i < count; i++)
    {
        string firstName = GenerateRandomName();
        string lastName = GenerateRandomName();
        authors.Add($"{firstName} {lastName}");
    }
    return authors;
}

string GenerateRandomName()
{
    List<string> names = ["János", "Péter", "Katalin", "Anna", "László", "István", "Zoltán", "Gabriella", "Gábor", "Mária"];
    return names[random.Next(names.Count)];
}

var books = GenerateRandomBooks(15);

decimal totalRevenue = 0;
int outOfStockCount = 0;
int initialStockCount = books.Sum(b => b.Stock);
int finalStockCount = initialStockCount;

for (int i = 0; i < 100; i++)
{
    var book = books[random.Next(books.Count)];

    if (book.Stock > 0)
    {
        book.Stock--;
        totalRevenue += book.Price;
    }
    else
    {
        if (random.NextDouble() < 0.5)
        {
            int restockAmount = random.Next(1, 11);
            book.Stock += restockAmount;
            finalStockCount += restockAmount;
        }
        else
        {
            books.Remove(book);
            outOfStockCount++;
        }
    }
}

Console.WriteLine($"Bruttó bevétel: {totalRevenue} Ft");
Console.WriteLine($"Elfogyott a nagykerből: {outOfStockCount} könyv");
Console.WriteLine($"Készlet változás: Kezdeti készlet = {initialStockCount}, Jelenlegi készlet = {finalStockCount}");
Console.WriteLine($"Készlet változása: {finalStockCount - initialStockCount}");
