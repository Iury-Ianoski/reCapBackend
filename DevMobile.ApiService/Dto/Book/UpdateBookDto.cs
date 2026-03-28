namespace DevMobile.ApiService.Dto.Book;

public record UpdateBookDto( //realmente necessário????
    int Id,
    string Title,
    string Author,
    int PublicationYear,
    string CoverImageUrl,
    int Chapters,
    string Summary
);
