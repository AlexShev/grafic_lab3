using grafic_lab3.Algorithm;
using grafic_lab3.Image;
using grafic_lab3.Model;

namespace grafic_lab3;

public partial class Form1 : Form
{
    ScaledBitmap bitmapA;
    ScaledBitmap bitmapB;

    Smilic smilic = new Smilic(new Point(18, 18), 18, 4, 10, 60, 30);

    public Form1()
    {
        InitializeComponent();

        InitPictureBox();

        textBox1.Text = smilic.MouthAngleAlfa.ToString();
        textBox2.Text = smilic.MouthAngleBeta.ToString();
        textBox3.Text = smilic.MouthRadius.ToString();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        checkBox1.Enabled = false;
        button1.Enabled = false;
        textBox1.Enabled = false;
        textBox2.Enabled = false;
        textBox3.Enabled = false;

        InitPictureBox();

        new Thread(() => {
            var picture1Info = smilic.Draw(new AlgorithmA(), bitmapA, Color.Black);
            pictureBox1.Image = bitmapA.ToBitmap();
            pictuerBoxInfor1.Invoke(()=> pictuerBoxInfor1.Text = picture1Info.ToString());

            var picture2Info = smilic.Draw(new AlgorithmB(), bitmapB, Color.Black);
            pictureBox2.Image = bitmapB.ToBitmap();
            pictuerBoxInfor2.Invoke(() => pictuerBoxInfor2.Text = picture2Info.ToString());

            pictureBox3.Image = new ScaledBitmap(BitmapComporator.Compare(bitmapA, bitmapB), 10).ToBitmap();

            checkBox1.Invoke(() => { checkBox1.Enabled = true; });
            button1.Invoke(() => { button1.Enabled = true; });
            textBox1.Invoke(() => { textBox1.Enabled = true; });
            textBox2.Invoke(() => { textBox2.Enabled = true; });
            textBox3.Invoke(() => { textBox3.Enabled = true; });

        }).Start();
    }

    void InitPictureBox()
    {
        bitmapA = new ScaledBitmap(390, 390, 10, Color.White);
        bitmapB = new ScaledBitmap(390, 390, 10, Color.White);

        pictureBox1.Image = bitmapA.ToBitmap();
        pictureBox2.Image = bitmapB.ToBitmap();

        if (checkBox1.Checked)
        {
            bitmapA.Function = (bitmap) => pictureBox1.Image = bitmap.ToBitmap();
            bitmapB.Function = (bitmap) => pictureBox2.Image = bitmap.ToBitmap();
        }
        else
        {
            bitmapA.Function = null;
            bitmapB.Function = null;
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        if (int.TryParse(textBox1.Text, out int pared))
        {
            int mouthAngleAlfa = pared;

            if (mouthAngleAlfa < 0)
            {
                mouthAngleAlfa = 0;
            }
            else if (mouthAngleAlfa > 180)
            {
                mouthAngleAlfa = 180;
            }

            smilic.MouthAngleAlfa = mouthAngleAlfa;

            textBox1.Text = mouthAngleAlfa.ToString();
        }
        else
        {
            MessageBox.Show("Вышел и зашёл нормально");
        }
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {
        if (int.TryParse(textBox2.Text, out int pared))
        {
            int mouthAngleBeta = pared;

            if (mouthAngleBeta < 0)
            {
                mouthAngleBeta = 0;
            }
            else if (mouthAngleBeta > 180)
            {
                mouthAngleBeta = 180;
            }

            smilic.MouthAngleBeta = mouthAngleBeta;

            textBox2.Text = mouthAngleBeta.ToString();
        }
        else
        {
            MessageBox.Show("Вышел и зашёл нормально");
        }
    }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {
        if (int.TryParse(textBox3.Text, out int pared))
        {
            smilic.MouthRadius = pared;

            textBox3.Text = smilic.MouthRadius.ToString();
        }
        else
        {
            MessageBox.Show("Вышел и зашёл нормально");
        }
    }
}