namespace LibrarySystem.Models;

public class Loan
{
    public int BookId { get; set; }
    public int BorrowerId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Book Book { get; set; } = null!;
    public Borrower Borrower { get; set; } = null!;
}
