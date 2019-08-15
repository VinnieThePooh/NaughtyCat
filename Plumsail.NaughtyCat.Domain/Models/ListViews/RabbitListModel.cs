namespace Plumsail.NaughtyCat.Domain.Models.ListViews
{
    public class RabbitListModel
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public RabbitListModelFilter Filter { get; set; }
    }
}
