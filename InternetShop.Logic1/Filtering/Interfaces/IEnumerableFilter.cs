namespace InternetShop.Logic.Filtering.Interfaces
{
    public interface IEnumerableFilter<T>
    {
        public delegate bool FilterDelegate(T item);
        public IEnumerable<T> Filtering(IEnumerable<T> items, FilterDelegate filter);
    }
}
