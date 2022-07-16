using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using RThirst.Tools;

namespace PokerAF
{
    public class DataConfig
    {
        private SerializedData SD;
        string? d = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public DataConfig(Label label)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(SerializedData));

            if (File.Exists($"{d}\\data.json"))
            {
                using (FileStream fs = new FileStream($"{d}\\data.json", FileMode.Open))
                    SD = (SerializedData)jsonFormatter.ReadObject(fs);
                label.Text = "Загружено";
            }
            else
            {
                SD = new SerializedData();
                SD.Lose = new Dictionary<Rectangle, Color>();
                SD.Win = new Dictionary<Rectangle, Color>();
                label.Text = "Ошибка загрузки";
            }
        }

        public void Save()
        {
            using (FileStream fs = new FileStream($"{d}\\data.json", FileMode.Create))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(SerializedData));
                jsonFormatter.WriteObject(fs, SD);
            }
        }

        public void AddWinConfig(Rectangle rectangle, Color color)
        {
            SD.Win.TryAdd(rectangle, color);
        }

        public void AddLoseConfig(Rectangle rectangle, Color color)
        {
            SD.Lose.TryAdd(rectangle, color);
        }

        public void ClearCardConfig()
        {
            SD.Win.Clear();
            SD.Lose.Clear();
        }

        public bool IsWin()
        {
            foreach (var key in SD.Win.Keys)
            {
                var b = Window.Screenshot(key.Location, key.Size);
                if (new Bitmap(b, new Size(1, 1)).GetPixel(0, 0) == SD.Win[key])
                    return true;
            }
            return false;
        }

        public bool IsLose()
{
            foreach (var key in SD.Lose.Keys)
            {
                var b = Window.Screenshot(key.Location, key.Size);
                if (new Bitmap(b, new Size(1, 1)).GetPixel(0, 0) == SD.Lose[key])
                    return true;
            }
            return false;
        }
    }
}