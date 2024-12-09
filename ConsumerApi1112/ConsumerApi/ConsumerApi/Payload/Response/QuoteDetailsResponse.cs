namespace ConsumerApi.Payload.Response
{
    public class QuoteDetailsResponse
    {
        public string Quotes { get; set; }

        public QuoteDetailsResponse()
        {
        }
        public QuoteDetailsResponse(string quotes)
        {
            Quotes = quotes;
        }
    }
}
