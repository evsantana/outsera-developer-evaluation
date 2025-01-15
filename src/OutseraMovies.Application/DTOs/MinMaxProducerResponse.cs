namespace OutseraMovies.Application.DTOs
{
    public class MinMaxProducerResponse
    {
        public IEnumerable<ProducerResponse> Min { get; set; } = new List<ProducerResponse>();
        public IEnumerable<ProducerResponse> Max { get; set; } = new List<ProducerResponse>();
    }
}
