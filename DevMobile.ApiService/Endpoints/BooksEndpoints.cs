// using DevMobile.ApiService.Dto.Book;

// namespace DevMobile.ApiService.Endpoints;

// public static class BooksEndpoints
// {
//     private static readonly List<BookDto> books =
//     [
//         new( 1, "The Great Gatsby", "F. Scott Fitzgerald", 1925, "https://example.com/gatsby.jpg", 9, "A novel about the American dream and the decadence of the Jazz Age." ),
//         new( 2, "To Kill a Mockingbird", "Harper Lee", 1960, "https://example.com/mockingbird.jpg", 31, "A novel about racial injustice in the Deep South." ),
//         new( 3, "1984", "George Orwell", 1948, "https://example.com/1984.jpg", 12, "A dystopian social science fiction novel." )
//     ];

//     public static void MapBooksEndpoints(this WebApplication app)
//     {
//         var group = app.MapGroup("/books");

//         const string getBooksEndpointName = "/books";

//         group.MapGet("/", () => books)
//             .WithName(getBooksEndpointName);

//         group.MapGet("/{id}", (int id) =>
//         {
//             var book = books.FirstOrDefault(b => b.Id == id);
//             return book is not null ? Results.Ok(book) : Results.NotFound();
//         });

//         group.MapPost("/", (CreateBookDto newBook) =>
//         {
//             var book = new BookDto(
//                 books.Max(b => b.Id) + 1,
//                 newBook.Title,
//                 newBook.Author,
//                 newBook.PublicationYear,
//                 newBook.CoverImageUrl,
//                 newBook.Chapters,
//                 newBook.Summary
//             );
//             books.Add(book);
//             return Results.CreatedAtRoute(getBooksEndpointName, new { id = book.Id }, book);
//         });

//         group.MapPut("/{id}", (int id, UpdateBookDto updatedBook) =>
//         {
//             var index = books.FindIndex(b => b.Id == id);
//             if (index == -1)
//             {
//                 return Results.NotFound();
//             }

//             books[index] = new BookDto(
//                 id,
//                 updatedBook.Title,
//                 updatedBook.Author,
//                 updatedBook.PublicationYear,
//                 updatedBook.CoverImageUrl,
//                 updatedBook.Chapters,
//                 updatedBook.Summary     
//             );

//             return Results.NoContent();
//         });

//         group.MapDelete("/{id}", (int id) =>
//         {
//             var index = books.FindIndex(b => b.Id == id);
//             if (index == -1)
//             {
//                 return Results.NotFound();
//             }

//             books.RemoveAt(index);
//             return Results.NoContent();
//         });
//     }
// }
