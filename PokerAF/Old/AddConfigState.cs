using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RThirst.Tools;

namespace PokerAF
{
    public interface IRightSideState
    {
        public void Initialize();

        public void Update();

        public void Dissolve();

        public void AddConfig(object sender, EventArgs e);

    }

    public class AddWinningState : IRightSideState
    {
        PictureBox pictureBox = new PictureBox();
        Label label = new Label();
        Label label2 = new Label();
        TextBox textBox = new TextBox();
        TextBox textBox2 = new TextBox();
        Button button = new Button();
        Rectangle rectangle = new Rectangle();
        DataConfig DC;
        Form form;

        public AddWinningState(Form form, DataConfig DC)
        {
            this.form = form;
            this.DC = DC;
        }

        public void Initialize()
        {
            label = new Label();
            label.Text = "X";
            label.Location = new Point(385, 69);

            form.Controls.Add(label);

            label2 = new Label();
            label2.Text = "Y";
            label2.Location = new Point(497, 69);

            form.Controls.Add(label2);

            textBox = new TextBox();
            textBox.Location = new Point(385, 87);
            textBox.Size = new Size(88, 23);

            form.Controls.Add(textBox);

            textBox2 = new TextBox();
            textBox2.Location = new Point(497, 87);
            textBox2.Size = new Size(88, 23);

            form.Controls.Add(textBox2);

            pictureBox = new PictureBox();
            pictureBox.Location = new Point(385, 130);
            pictureBox.Size = new Size(88, 58);

            form.Controls.Add(pictureBox);

            button = new Button();
            button.Location = new Point(497, 130);
            button.Size = new Size(88, 26);
            button.Text = "Добавить";
            button.Click += new EventHandler(AddConfig);

            form.Controls.Add(button);
        }

        public void Update()
        {
            if (Int32.TryParse(textBox.Text, out var X) && Int32.TryParse(textBox2.Text, out var Y))
            {
                pictureBox.Image = Window.Screenshot(new Point(X, Y), new Size(25, 10));
                rectangle = new Rectangle(new Point(X, Y), new Size(25, 10));
            }
        }

        public void Dissolve()
        {
            form.Controls.Remove(label2);
            form.Controls.Remove(label);
            form.Controls.Remove(button);
            form.Controls.Remove(pictureBox);
            form.Controls.Remove(textBox);
            form.Controls.Remove(textBox2);
        }

        public void AddConfig(object sender, EventArgs e)
        {
            DC.AddWinConfig(rectangle, new Bitmap(pictureBox.Image, new Size(1, 1)).GetPixel(0, 0));
        }
    }

    public class AddLosingState : IRightSideState
    {
        public PictureBox pictureBox = new PictureBox();

        Label label = new Label();
        Label label2 = new Label();
        TextBox textBox = new TextBox();
        TextBox textBox2 = new TextBox();
        Button button = new Button();
        Button button2 = new Button();
        Rectangle rectangle = new Rectangle();
        DataConfig DC;
        Form form;

        public AddLosingState(Form form, DataConfig DC)
        {
            this.form = form;
            this.DC = DC;
        }

        public void Update()
        {
            if (Int32.TryParse(textBox.Text, out var X) && Int32.TryParse(textBox2.Text, out var Y))
            {
                pictureBox.Image = Window.Screenshot(new Point(X, Y), new Size(25, 10));
                rectangle = new Rectangle(new Point(X, Y), new Size(25, 10));
            }
        }

        public void Initialize()
        {
            label = new Label();
            label.Text = "X";
            label.Location = new Point(385, 69);

            form.Controls.Add(label);

            label2 = new Label();
            label2.Text = "Y";
            label2.Location = new Point(497, 69);

            form.Controls.Add(label2);

            textBox = new TextBox();
            textBox.Location = new Point(385, 87);
            textBox.Size = new Size(88, 23);

            form.Controls.Add(textBox);

            textBox2 = new TextBox();
            textBox2.Location = new Point(497, 87);
            textBox2.Size = new Size(88, 23);

            form.Controls.Add(textBox2);

            pictureBox = new PictureBox();
            pictureBox.Location = new Point(385, 130);
            pictureBox.Size = new Size(88, 58);

            form.Controls.Add(pictureBox);

            button = new Button();
            button.Location = new Point(497, 130);
            button.Size = new Size(88, 26);
            button.Text = "Добавить";
            button.Click += new EventHandler(AddConfig);

            form.Controls.Add(button);
        }

        public void Dissolve()
        {
            form.Controls.Remove(label2);
            form.Controls.Remove(label);
            form.Controls.Remove(button);
            form.Controls.Remove(button2);
            form.Controls.Remove(pictureBox);
            form.Controls.Remove(textBox);
            form.Controls.Remove(textBox2);
        }

        public void AddConfig(object sender, EventArgs e)
        {
            DC.AddLoseConfig(rectangle, new Bitmap(pictureBox.Image, new Size(1, 1)).GetPixel(0, 0));
        }
    }

    public class None : IRightSideState
    {
        public void Initialize()
        {

        }

        public void Update()
        {

        }
        public void Dissolve()
        {

        }

        public void AddConfig(object sender, EventArgs e)
        {

        }
    }
}
