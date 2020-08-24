using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Seriolization
{
    public class ToXML
    {
        /// <summary>
        /// Метод сериализации List<Worker >
        /// </summary>
        /// <param name="СoncreteWorker">Коллекция для сериализации</param>
        /// <param name="Path">Путь к файлу</param>
        public static void SerializeToXml<T>(T СoncreteWorkerList, string Path)
        {
            // Создаем сериализатор на основе указанного типа 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            // Создаем поток для сохранения данных
            Stream fStream = new FileStream(Path, FileMode.Create, FileAccess.Write);

            // Запускаем процесс сериализации
            xmlSerializer.Serialize(fStream, СoncreteWorkerList);

            // Закрываем поток
            fStream.Close();
        }

        /// <summary>
        /// Метод десериализации Worker
        /// </summary>
        /// <param name="СoncreteWorker">Экземпляр для сериализации</param>
        /// <param name="Path">Путь к файлу</param>
        public static List<T> DeserializeToXmlList<T>(string Path)
        {
            List<T> tempTCol = new List<T>();
            // Создаем сериализатор на основе указанного типа 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            // Создаем поток для чтения данных
            Stream fStream = new FileStream(Path, FileMode.Open, FileAccess.Read);

            // Запускаем процесс десериализации
            tempTCol = xmlSerializer.Deserialize(fStream) as List<T>;

            // Закрываем поток
            fStream.Close();

            // Возвращаем результат
            return tempTCol;
        }
    }
}
