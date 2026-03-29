namespace InventoryManagementSystem.Helpers
{
    public class Pager
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Skip => (PageNumber - 1) * PageSize;

        public bool isValid => PageNumber > 0 && PageSize > 0;
    }
}
