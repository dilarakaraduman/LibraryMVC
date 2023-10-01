namespace LibraryMVC.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Writer { get; set; }
		public string Image { get; set; }
		public bool IsInvisible { get; set; }
		public string? Borrower { get; set; }
		public DateTime? AvaliableDate { get; set; }
	}
}