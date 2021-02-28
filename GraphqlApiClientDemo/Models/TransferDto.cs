namespace GraphqlApiClientDemo.Models
{
    public class TransferDto
    {
        public int Id { get; set; }
        public int OriginalManuscriptId { get; set; }
        public string Title { get; set; }
        public string OriginTenantId { get; set; }
        public string DestinationTenantId { get; set; }
    }
}
