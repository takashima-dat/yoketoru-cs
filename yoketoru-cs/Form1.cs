using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yoketoru_cs
{
    enum SCENE
    {
        TITLE,
        GAME,
        CLEAR,
        GAMEOVER,
        NONE
    }

    public partial class Form1 : Form
    {
        const int ITEM_COUNT = 5;

        SCENE nowscene;
        SCENE nextscene;

        Label[] labels = new Label[ITEM_COUNT];

        int[] vx = new int[ITEM_COUNT];
        int[] vy = new int[ITEM_COUNT];

        int left;

        private static Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            nextscene = SCENE.TITLE;
            nowscene = SCENE.NONE;

            for (int i = 0; i < ITEM_COUNT; i++)
            {
                labels[i] = new Label();
                labels[i].AutoSize = true;
                labels[i].Text = "★";
                Controls.Add(labels[i]);
                labels[i].Visible = false;
            }
        }

        void initScene()
        {
            if (nextscene == SCENE.NONE) return;

            nowscene = nextscene;
            nextscene = SCENE.NONE;

            switch (nowscene)
            {
                case SCENE.TITLE:
                    titleLabel.Visible = true;
                    startButton.Visible = true;
                    break;
                case SCENE.GAME:
                    titleLabel.Visible = false;
                    startButton.Visible = false;
                    left = ITEM_COUNT;
                    leftLabel.Text = "残り:" + left + "個";
                    for (int i = 0; i < ITEM_COUNT; i++)
                    {
                        labels[i].Visible = true;
                        labels[i].Left = rand.Next(ClientSize.Width - labels[i].Width);
                        labels[i].Top = rand.Next(ClientSize.Height - labels[i].Height);
                        vx[i] = rand.Next(-5, 5);
                        vy[i] = rand.Next(-5, 5);
                    }
                    break;

            }


        }

        void updateScene()
        {
            if (nowscene != SCENE.GAME) return;

            for (int i = 0; i < ITEM_COUNT; i++)
            {
                labels[i].Left += vx[i];
                labels[i].Top += vy[i];

                if (labels[i].Left < 0)
                {
                    vx[i] = Math.Abs(vx[i]);
                }
                if (labels[i].Right > ClientSize.Width)
                {
                    vx[i] = -Math.Abs(vx[i]);
                }
                if (labels[i].Top < 0)
                {
                    vy[i] = Math.Abs(vy[i]);
                }
                if (labels[i].Bottom > ClientSize.Height)
                {
                    vy[i] = -Math.Abs(vy[i]);
                }

                Point mp = PointToClient(MousePosition);
                if (
                      (mp.X >= labels[i].Left)
                   && (mp.X <= labels[i].Right)
                   && (mp.Y >= labels[i].Top)
                   && (mp.Y <= labels[i].Bottom)
                    )
                {
                    labels[i].Visible = false;
                    left--;
                    leftLabel.Text = "残り:" + left + "個";
                }
            }

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            nextscene = SCENE.GAME;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            initScene();
            updateScene();
        }
    }
}
