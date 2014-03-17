using System;


using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            myarr = new System.Collections.ArrayList();
            InitializeComponent();
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.Thumbnail_Paint);
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
        }

        private void KeyDown_1(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (i == myarr.Count - 1)
                {
                    i = 0;
                }
                else
                {
                    i = i + 1;
                }
                flag = 1;
                pictureBox1.Invalidate();
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (i == 0)
                {
                    i = myarr.Count - 1;
                }
                else
                {
                    i = i - 1;
                }
                flag = 1;
                pictureBox1.Invalidate();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                String temp = (String)myarr[i];
                if (i == myarr.Count - 1)
                {
                    i = 0;
                }
                else
                {
                    i = i + 1;
                }
                flag = 1;
                pictureBox1.Invalidate();
                GC.Collect();

                if (System.IO.File.Exists(temp))
                {
                    try
                    {
                        MessageBox.Show("Deleting " + temp);
                        System.IO.File.Delete(temp);
                        if (i == 0)
                        {
                            myarr.RemoveAt(myarr.Count - 1);
                        }
                        else
                        {
                            myarr.RemoveAt(i - 1);
                            i = i - 1;
                        }
                    }
                    catch (System.IO.IOException k)
                    {
                        MessageBox.Show(k.Message);
                    }
                }

                if (System.IO.File.Exists(temp.Substring(0, temp.Length - 3) + "CR2"))
                {
                    try
                    {
                        //MessageBox.Show("deleting " + temp.Substring(0, temp.Length - 3) + "CR2");
                        System.IO.File.Delete(temp.Substring(0, temp.Length - 3) + "CR2");
                    }
                    catch (System.IO.IOException k)
                    {
                        MessageBox.Show(k.Message);
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void opendialog_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                myarr.Clear();
                foreach (String k in Directory.GetFiles(folderBrowserDialog2.SelectedPath, "*.JPG"))
                {
                    myarr.Add(k);
                }

                label1.Text = folder_name;
                flag = 1;
                pictureBox1.Invalidate();

                //   pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
                //    pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.Thumbnail_Paint);
            }
        }

        private void Thumbnail_Paint(object sender, PaintEventArgs e)
        {
            //GetThumbnail(e);
            ShowImage(e);
        }

        void ShowImage(PaintEventArgs e)
        {
                try
                {
                    pictureBox1.Image = new Bitmap((String)myarr[i]);
                 
               // Image.GetThumbnailImageAbort callback =
               //       new Image.GetThumbnailImageAbort(ThumbnailCallback);
               /* Image image = new Bitmap((String)myarr[i]);
                Image thumb_im = image.GetThumbnailImage(600,400, callback, new
                           IntPtr());
                    e.Graphics.DrawImage(
                               thumb_im,
                               0,
                               0);
                    image = null;
                    GC.Collect();
               **/
                }
                catch
                {
                }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        public bool ThumbnailCallback()
        {
            return true;
        }

        private void GetThumbnail(PaintEventArgs e)
        {
            Image.GetThumbnailImageAbort callback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);
            if (flag == 1)
            {
                for (int j = 0; j < 4; ++j)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        Image image = new Bitmap(filePaths[j * 4 + i]);
                        Image pThumbnail = image.GetThumbnailImage(200, 150, callback, new
                           IntPtr());
                        //label1.Text = filePaths[j*2 +i];
                        e.Graphics.DrawImage(
                           pThumbnail,
                           i * 230 + 20,
                           j * 160 + 10,
                           pThumbnail.Width,
                           pThumbnail.Height);
                        image = null;
                        pThumbnail = null;
                        GC.Collect();
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
