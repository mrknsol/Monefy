using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MonefyAppView.Services.Classes
{
    public class DataManager
    {
        private readonly string DataFilePath = "data.json";

        public void SaveData(Dictionary<Color, double> chartValues, Dictionary<DateTime, Dictionary<Color, double>> chartValuesByDate)
        {
            var dataToSave = new
            {
                ChartValues = chartValues,
                ChartValuesByDate = chartValuesByDate
            };

            string jsonData = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);

            File.WriteAllText(DataFilePath, jsonData);

            MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public (Dictionary<Color, double>, Dictionary<DateTime, Dictionary<Color, double>>) LoadData()
        {
            if (File.Exists(DataFilePath))
            {
                string jsonData = File.ReadAllText(DataFilePath);

                var loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);

                if (loadedData.ContainsKey("ChartValues") && loadedData.ContainsKey("ChartValuesByDate"))
                {
                    var chartValues = JsonConvert.DeserializeObject<Dictionary<Color, double>>(loadedData["ChartValues"].ToString());

                    var chartValuesByDateRaw = JsonConvert.DeserializeObject<Dictionary<string, object>>(loadedData["ChartValuesByDate"].ToString());

                    var chartValuesByDate = chartValuesByDateRaw.ToDictionary(
                        kvp => DateTime.Parse(kvp.Key),
                        kvp => JsonConvert.DeserializeObject<Dictionary<Color, double>>(kvp.Value.ToString())
                    );

                    MessageBox.Show("Данные успешно загружены", "Загрузка данных", MessageBoxButton.OK, MessageBoxImage.Information);

                    return (chartValues, chartValuesByDate);
                }
            }

            return (new Dictionary<Color, double>(), new Dictionary<DateTime, Dictionary<Color, double>>());
        }
    }

}
