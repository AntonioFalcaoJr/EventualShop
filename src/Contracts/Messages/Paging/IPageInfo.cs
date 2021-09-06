namespace Messages.Paging
{
    public interface IPageInfo
    {
        int Current { get; }
        int Size { get; }
        bool HasPrevious { get; }
        bool HasNext { get; }
    }
}