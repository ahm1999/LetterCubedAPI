namespace LettercubedApi.DTOs
{
    public class ReviewResponseDTO:ReviewDTO
    {
        public Guid ReviewId { get; set; }
        public string UserId { get; set; }
        public string UserName{get ; set;}
        public Guid MovieId { get; set; }
        public string MovieName { get; set; }


    }
}
