namespace AM.UI.StartupHelper
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }
}