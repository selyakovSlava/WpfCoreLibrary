
namespace WpfCoreLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс поиска файлов.
    /// </summary>
    public interface ISearcher
    {
        /// <summary>
        /// Поиск файлов по указанному шаблону.
        /// </summary>
        /// <param name="path">Директория, в которой нужно искать.</param>
        /// <param name="pattern">Шаблон поиска</param>
        /// <param name="searchOption">Опция поиска.</param>
        /// <returns></returns>
        string[] SearchFile(string path, string pattern, System.IO.SearchOption searchOption);
    }
}
