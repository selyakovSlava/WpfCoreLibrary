using System.Windows;
using WpfCoreLibrary.Interfaces;

namespace WpfCoreLibrary.DialogServices
{
    public class StandartDialogService : IDialog
    {
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="title">Заголовок сообщения.</param>
        public void ErrorMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Информационное сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="title">Заголовок сообщения.</param>
        public void InfoMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
