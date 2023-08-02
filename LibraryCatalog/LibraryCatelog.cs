public class Library
{
    protected string? Name;
    protected string? Address;
    List<Book> Books = new List<Book>();
    List<MediaItem> MediaItems = new List<MediaItem>();

    public Library(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        Books.Remove(book);
    }

    public void AddMediaItem(MediaItem mediaItem)
    {
        MediaItems.Add(mediaItem);
    }

    public void RemoveMediaItem(MediaItem mediaItem)
    {
        MediaItems.Remove(mediaItem);
    }

    public void printLibraryInfo()
    {
        Console.WriteLine("Library name: {0}, Library address: {1}", Name,Address);
    }

    public void PrintBooks()
    {
        Console.WriteLine("Books in library:");
        foreach (Book book in Books)
        {
            Console.WriteLine(book.getTitle());
            Console.WriteLine(book.getAuthor());
            Console.WriteLine(book.getISBN());
            Console.WriteLine(book.getPublicationYear());
        }

        Console.WriteLine(" ");
    }
   

    public void PrintMediaItems()
    {
        Console.WriteLine("Media items in library:");
        foreach (MediaItem mediaItem in MediaItems)
        {
            Console.WriteLine(mediaItem.getTitle());
            Console.WriteLine(mediaItem.getMediaType());
            Console.WriteLine(mediaItem.getDuration());
        }
    }

    public void PrintCatalog()
    {
        printLibraryInfo();
        PrintBooks();
        PrintMediaItems();
    }

}


public class Book{
    private string? Title;
    private string? Author;
    private string? ISBN;

    private int PublicationYear;


    public string getTitle()
    {
        return Title;
    }

    public string getAuthor()
    {
        return Author;
    }

    public string getISBN()
    {
        return ISBN;
    }

    public int getPublicationYear()
    {
        return PublicationYear;
    }


    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }

}






public class MediaItem{
    private string? Title;
    private string? MediaType;
    private int Duration;

    public MediaItem(string title, string mediaType, int duration)
    {
        Title = title;
        MediaType = mediaType;
        Duration = duration;
    }

    public string getTitle()
    {
        return Title;
    }

    public string getMediaType()
    {
        return MediaType;
    }

    public int getDuration()
    {
        return Duration;
    }

}


public class LibraryCatelog
{
    public static void Main(string[] args)
    {
        Library library = new Library("Library of Abrehot", "Ethiopa, Addis Ababa.");
        Display(library);
    }

    public static void Display(Library library)
    {
        while (true)
    {
        Console.WriteLine("Select an option:");
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. Add Media Item");
        Console.WriteLine("3. Print Books");
        Console.WriteLine("4. Print Media Items");
        Console.WriteLine("5. Print Catalog");
        Console.WriteLine("6. Exit");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Book book = addBook(); // Assuming you have a method AddBook() to get book details from the user
                library.AddBook(book);
                break;
            case "2":
                MediaItem mediaItem = addMediaItem(); // Assuming you have a method AddMediaItem() to get media item details from the user
                library.AddMediaItem(mediaItem);
                break;
            case "3":
                library.PrintBooks();
                break;
            case "4":
                library.PrintMediaItems();
                break;
            case "5":
                library.PrintCatalog();
                break;
            case "6":
                Environment.Exit(0); // Exit the program
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
    }
    public static Book addBook()
    {
        Console.WriteLine("Enter the title of the book: ");
        string title = Console.ReadLine()?? "";

        while (title == "" || int.TryParse(title, out int n))
        {
            Console.WriteLine("Title cannot be null and cannot be a number");
            title = Console.ReadLine()?? "";
        }

        Console.WriteLine("Enter the author of the book: ");
        string author = Console.ReadLine()?? "";
        while (author == "" || int.TryParse(author, out int n))
        {
            Console.WriteLine("Author cannot be null and cannot be a number");
            author = Console.ReadLine()?? "";
        }

        Console.WriteLine("Enter the ISBN of the book: ");
        string ISBN = Console.ReadLine()?? "";
        while (ISBN == ""  || int.TryParse(ISBN, out int n))
        {
            Console.WriteLine("ISBN cannot be null and cannot be a number");
            ISBN = Console.ReadLine()?? "";
        }
        
        Console.WriteLine("Enter the publication year of the book: ");
        int publicationYear = int.Parse(Console.ReadLine());

        while (publicationYear == 0  || string.IsInterned(publicationYear))
        {
            Console.WriteLine("ISBN cannot be null and cannot be a number");
            publicationYear = int.Parse(Console.ReadLine()?? "");
        }

        Book book = new Book(title, author, ISBN, publicationYear);
        
        return book;
    }

    public static MediaItem addMediaItem()
    {
        Console.WriteLine("Enter the title of the media item: ");
        string title = Console.ReadLine()?? "MediaTitle";
        Console.WriteLine("Enter the media type of the media item: ");
        string mediaType = Console.ReadLine()??"MediaType";
        Console.WriteLine("Enter the duration of the media item: ");
        int duration = int.Parse(Console.ReadLine()?? "0");

        MediaItem mediaItem = new MediaItem(title, mediaType, duration);

        return mediaItem;
    }

}