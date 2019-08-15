namespace Plumsail.NaughtyCat.Domain.Models.ListViews
{
    public class RabbitListView
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public RabbitListViewFilter Filter { get; set; }
    }
}
