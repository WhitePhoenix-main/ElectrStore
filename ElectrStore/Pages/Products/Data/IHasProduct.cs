// ReSharper disable once CheckNamespace
namespace ElectrStore
{
    public interface IHasProduct
    {
        ProductRecord? ProductRecord { get; set; }
        bool IsNewRec { get; set; }
    }
}