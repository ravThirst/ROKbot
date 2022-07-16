using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RThirst.Tools;

namespace PokerAF
{
    public class GameProcessing
    {
        public DataConfig DC;
        private List<Desk> desks;

        public GameProcessing(List<Desk> desks, Label label)
        {
            DC = new DataConfig(label);
            this.desks = desks;
        }

        public void ProcessWindow(int index)
        {
            Window.SetForegroundWindow(desks[index].Handle);
            //CardsProcessing(index);
            Click(AI.AIDecision(desks[index]), index);
        }

        public void Click(Decision decision, int index)
        {
            switch (decision)
            {
                case Decision.Call:
                    {
                        //MouseSim.ClickOnPoint(desks[index].Handle,
                        //    new Point(511, 384));
                        ////CR.SetKnownCard(index);
                        return;
                    }
                case Decision.Fold:
                    {
                        //MouseSim.ClickOnPoint(desks[index].Handle,
                        //    new Point(415, 384));
                        //CR.SetKnownCard(index);
                        return;
                    }
                case Decision.Nothing:
                    {
                        var desk = desks[index];
                        if (desk.FirstCard == Card.NA && desk.SecondCard != Card.NA)
                            UnknownCard(index, 1);
                        else if (desk.FirstCard != Card.NA && desk.SecondCard == Card.NA)
                            UnknownCard(index, 2);
                        
                            //CR.SetKnownCard(index);
                        return;
                    }
            }
        }

        public void GetData(ListBox listBox)
        {
            for (int i = 0; i < desks.Count; i++)
            {
                var a = desks[i].FirstCard.ToString();
                var b = desks[i].SecondCard.ToString();
                var c = desks[i].SameSuit ? "S" : "NS";
                var d = desks[i].Decision.ToString()[0];
                listBox.Items[i] = $"{a} {b} | {c} | {d}";
            }
        }

        private void UnknownCard(int index, int i)
        {
            var point = WindowMover.GetPoint(index);
            var bitmap = i == 1 ? Window.Screenshot(new Point(point.X + 245, point.Y + 312), new Size(16, 19)) :
                                  Window.Screenshot(new Point(point.X + 275, point.Y + 310), new Size(16, 19));

            if (GetHash(bitmap)[0] != 'F') { }
                //CR.SetUnknownCard(bitmap, index);
        }

        //private void CardsProcessing(int index)
        //{
        //    Thread.Sleep(250);
        //    var point = WindowMover.GetPoint(index);
        //    Window.SetForegroundWindow(desks[index].Pointer);

        //    var f = DC.Contains(GetHash(Window.Screenshot(new Point(point.X + 245, point.Y + 312), new Size(16, 19))));
        //    var s = DC.Contains(GetHash(Window.Screenshot(new Point(point.X + 275, point.Y + 310), new Size(16, 19))));

        //    if (Enum.TryParse<Card>(f, out var card))
        //        desks[index].FirstCard = card;
        //    else
        //        desks[index].FirstCard = Card.NA;

        //    if (Enum.TryParse<Card>(s, out var cardS))
        //        desks[index].SecondCard = cardS;
        //    else
        //        desks[index].SecondCard = Card.NA;

        //    if (ColorMatch(index))
        //        desks[index].SameSuit = true;
        //    else
        //        desks[index].SameSuit = false;

        //}

        public string GetHash(Bitmap bmpSource)
        {
            StringBuilder sb = new StringBuilder();
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 19));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    if (bmpMin.GetPixel(i, j).GetBrightness() < 0.9f)
                        sb.Append(1);
                    else
                        sb.Append(0);
                }
            }
            return BinaryStringToHexString(sb.ToString());
        }

        public Bitmap TESTPoint(Point point, Size size)
        {
            var point1 = WindowMover.GetPoint(0);
            return Window.Screenshot(new Point(point.X + point1.X, point.Y + point1.Y), size);
        }

        public bool ColorMatch(int index)
        {
            var point = WindowMover.GetPoint(index);
            var s = ColorModification(Window.Screenshot(new Point(point.X + 297, point.Y + 355), new Size(1, 1)).GetPixel(0, 0));
            var f = ColorModification(Window.Screenshot(new Point(point.X + 267, point.Y + 350), new Size(1, 1)).GetPixel(0, 0));
            if (f.R == s.R && f.G == s.G && f.B == s.B)
                return true;
            else
                return false;
        }

        public (Bitmap, string) TESTCardSaving(int i)
        {
            if (i == 1)
            {
                var point1 = WindowMover.GetPoint(0);
                return TESTGetHash(Window.Screenshot(new Point(point1.X + 245, point1.Y + 312), new Size(16, 19)));
            }
            if (i == 2)
            {
                var point1 = WindowMover.GetPoint(0);
                return TESTGetHash(Window.Screenshot(new Point(point1.X + 275, point1.Y + 310), new Size(16, 19)));
            }
            throw new Exception();
        }

        public (Bitmap, string) TESTGetHash(Bitmap bmpSource)
        {
            StringBuilder sb = new StringBuilder();
            //create new image with 16x16 pixel

            for (int j = 0; j < bmpSource.Height; j++)
            {
                for (int i = 0; i < bmpSource.Width; i++)
                {
                    //reduce colors to true / false

                    if (bmpSource.GetPixel(i, j).GetBrightness() < 0.9f)
                        bmpSource.SetPixel(i, j, Color.Black);
                    else 
                        bmpSource.SetPixel(i, j, Color.White);
                }
            }

            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 19));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false
                    if (bmpMin.GetPixel(i, j).GetBrightness() < 0.9f)
                        sb.Append(1);
                    else
                        sb.Append(0);
                }
            }
            return (bmpSource, BinaryStringToHexString(sb.ToString()));
        }

        public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        public Color ColorModification(Color color)
        {
            var r = color.R > 127 ? 1 : 0;
            var g = color.G > 105 ? 1 : 0;
            var b = color.B > 127 ? 1 : 0;
            return Color.FromArgb(r, g, b);
        }
    }
}