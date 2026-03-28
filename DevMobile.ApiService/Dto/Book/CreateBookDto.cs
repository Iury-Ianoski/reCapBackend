namespace DevMobile.ApiService.Dto.Book;

public record CreateBookDto
(
    string Title,
    string Author,
    int PublicationYear,
    string CoverImageUrl,
    int Chapters,
    string Summary
);
