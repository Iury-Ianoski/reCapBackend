namespace DevMobile.ApiService.Dto.Book;

public record BookDto(
    int Id,
    string Title,
    string Author,
    int PublicationYear,
    string CoverImageUrl,
    int Chapters,
    string Summary
);
