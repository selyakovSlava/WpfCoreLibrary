using WpfCoreLibrary.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Interactivity;
using System.Xml.Serialization;

namespace WpfCoreLibrary.Behaviors
{
    public class WindowBehavior : Behavior<Window>
    {
        /// <summary>
        /// Название приложения.
        /// </summary>
        public string ApplicationName { get; set; }

        protected override void OnAttached()
        {
            AssociatedObject.Closing += AssociatedObject_Closing;
            AssociatedObject.Initialized += AssociatedObject_Initialized;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Closing -= AssociatedObject_Closing;
            AssociatedObject.Initialized -= AssociatedObject_Initialized;
        }

        private void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            LoadWindowSettings();
        }

        private void AssociatedObject_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveWindowSettings();
        }

        /// <summary>
        /// Сохранение настроек окна.
        /// </summary>
        private void SaveWindowSettings()
        {
            WindowSettings window = new WindowSettings();
            window.Width = AssociatedObject.Width;
            window.Height = AssociatedObject.Height;
            window.Left = AssociatedObject.Left;
            window.Top = AssociatedObject.Top;
            window.WindowState = AssociatedObject.WindowState;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(WindowSettings));
                WindowSettings saveParameters = window;
                using (TextWriter txtWriter = new StreamWriter(GetPathSetting()))
                {
                    serializer.Serialize(txtWriter, saveParameters);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(MainWindowLocalizations.MainWindowLocalizations.ErrorSavingSettings + ":\n" + ex.Message,
                //    MainWindowLocalizations.MainWindowLocalizations.ErrorSavingSettings,
                //    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Загрузить настройки окна.
        /// </summary>
        public void LoadWindowSettings()
        {
            WindowSettings window = new WindowSettings();
            window = DeserializeFromXml(GetPathSetting());
            AssociatedObject.Width = window.Width;
            AssociatedObject.Height = window.Height;
            AssociatedObject.Left = window.Left;
            AssociatedObject.Top = window.Top;
            AssociatedObject.WindowState = window.WindowState;
        }

        /// <summary>
        /// Распарсить XML с параметрами окна.
        /// </summary>
        /// <param name="fPath">Путь к файлу с настройками.</param>
        /// <returns></returns>
        public WindowSettings DeserializeFromXml(string fPath)
        {
            WindowSettings local = new WindowSettings();
            XmlSerializer serializer = new XmlSerializer(typeof(WindowSettings));
            try
            {
                using (Stream reader = new FileStream(fPath, FileMode.Open))
                {
                    // Call the Deserialize method to restore the object's state.
                    local = (WindowSettings)serializer.Deserialize(reader);
                    return local;
                }
            }
            catch { }
            return local;
        }

        /// <summary>
        /// Получить путь до папки документы пользователя.
        /// </summary>
        /// <returns></returns>
        public string GetPersonFolderPath()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Путь к документам пользователя.
            if (Directory.Exists(Path.Combine(md, ApplicationName, "Window\\Settings")) == false)
            {
                Directory.CreateDirectory(Path.Combine(md, ApplicationName, "Window\\Settings"));
            }
            return Path.Combine(md, ApplicationName, "Window\\Settings");
        }

        /// <summary>
        /// Вернуть путь к настройкам окна.
        /// </summary>
        /// <returns></returns>
        public string GetPathSetting()
        {
            return Path.Combine(GetPersonFolderPath(), "WindowSettings.xml");
        }
    }
}
