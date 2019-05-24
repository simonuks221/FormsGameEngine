﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormsGameEngine;

namespace FormsGameEngineFormExample
{
    public partial class Form1 : Form
    {
        GameManager gameManager;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainGameEnginePanel mainGameEnginePanel = new MainGameEnginePanel(new Size(700, 400));
            Controls.Add(mainGameEnginePanel);
            mainGameEnginePanel.Location = new Point(0, 0);

            gameManager = new GameManager(mainGameEnginePanel);

            List<GameObject> scene1GameObjects = new List<GameObject>() { new CubeGameObject(new Point(0, 0), new Size(20, 20), Color.Red)};
            GameScene scene1 = new GameScene(scene1GameObjects);
            
            gameManager.gameScenes.Add(scene1);

            GameCycle();
        }

        void GameCycle()
        {
            Delayed(1, () => UpdateScreen());
        }

        void UpdateScreen()
        {
            gameManager.UpdateCurrentGamePanel();
            GameCycle();
        }

        public void Delayed(int delay, Action action)
        {
            Timer timer = new Timer();
            timer.Interval = delay;
            timer.Tick += (s, e) => {
                action();
                timer.Stop();
            };
            timer.Start();
        }

        private void Form1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.Out.WriteLine(e.KeyData);
        }
    }
}
