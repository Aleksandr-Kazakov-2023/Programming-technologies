using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    enum LightState
    {
        On,
        Green,
        GreenBlink,
        Yellow,
        Red,
        RedYellow
    }

    enum LightEvent
    {
        TurnOn,
        Next
    }

    public partial class Form1 : Form
    {
        private FSM<LightState, LightEvent> fsm;
        private Graphics graphics;
        int diameter = 100;
        private int tickCount = 0;

        void Green(bool isOn)
        {
            graphics.FillEllipse(new SolidBrush(isOn ? Color.ForestGreen : Color.White),
                (pictureBox.Width / 2) - diameter / 2,
                diameter * 2 + 4, diameter, diameter);
        }

        void Red(bool isOn)
        {
            graphics.FillEllipse(new SolidBrush(isOn ? Color.Crimson : Color.White),
                (pictureBox.Width / 2) - diameter / 2,
                0, diameter, diameter);
        }

        void Yellow(bool isOn)
        {
            graphics.FillEllipse(new SolidBrush(isOn ? Color.Yellow : Color.White),
                (pictureBox.Width / 2) - diameter / 2,
                diameter + 2, diameter, diameter);
        }

        public Form1()
        {
            InitializeComponent();
            fsm = new(LightState.On);
            if (pictureBox.Image == null)
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);
            for (int i = 0; i < 3; i++)
            {
                graphics.DrawEllipse(new Pen(Color.Black, 3),
                    (pictureBox.Width / 2) - diameter / 2,
                    diameter * i + (2 * i), diameter, diameter);
            }

            graphics.DrawRectangle(new Pen(Color.Black, 5),
                (pictureBox.Width / 2) - diameter / 2,
                0, diameter, diameter * 3 + 4);

            fsm.RegisterTransitions(new Transition<LightState, LightEvent>[]
            {
                new() {FromState = LightState.On, ToState = LightState.Red, Event = LightEvent.TurnOn},
                new() {FromState = LightState.Red, ToState = LightState.RedYellow, Event = LightEvent.Next},
                new() {FromState = LightState.RedYellow, ToState = LightState.Green, Event = LightEvent.Next},
                new() {FromState = LightState.Green, ToState = LightState.GreenBlink, Event = LightEvent.Next},
                new() {FromState = LightState.GreenBlink, ToState = LightState.Yellow, Event = LightEvent.Next},
                new() {FromState = LightState.Yellow, ToState = LightState.Red, Event = LightEvent.Next}
            });

            fsm[LightState.Red].OnEnter = () =>
            {
                Red(true);
                Green(false);
                Yellow(false);
                tickCount = 0;
            };

            fsm[LightState.Red].OnUpdate = () =>
            {
                if (tickCount == 5)
                    fsm.OnEvent(LightEvent.Next);
                tickCount++;
            };

            fsm[LightState.RedYellow].OnEnter = () =>
            {
                Red(true);
                Yellow(true);
                Green(false);
                tickCount = 0;
            };
            fsm[LightState.RedYellow].OnUpdate = () =>
            {
                if (tickCount == 2)
                    fsm.OnEvent(LightEvent.Next);
                tickCount++;
            };

            fsm[LightState.Green].OnEnter = () =>
            {
                Red(false);
                Yellow(false);
                Green(true);
                tickCount = 0;
            };
            fsm[LightState.Green].OnUpdate = () =>
            {
                if (tickCount == 5)
                    fsm.OnEvent(LightEvent.Next);
                tickCount++;
            };

            fsm[LightState.GreenBlink].OnEnter = () =>
            {
                Red(false);
                Yellow(false);
                Green(false);
                tickCount = 0;
            };
            fsm[LightState.GreenBlink].OnUpdate = () =>
            {
                if (tickCount % 2 == 0)
                    Green(true);
                else
                    Green(false);

                if (tickCount == 5)
                    fsm.OnEvent(LightEvent.Next);
                tickCount++;
            };

            fsm[LightState.Yellow].OnEnter = () =>
            {
                Red(false);
                Yellow(true);
                Green(false);
                tickCount = 0;
            };
            fsm[LightState.Yellow].OnUpdate = () =>
            {
                if (tickCount == 2)
                    fsm.OnEvent(LightEvent.Next);
                tickCount++;
            };

            fsm.OnEvent(LightEvent.TurnOn);

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            fsm.Tick();
            pictureBox.Invalidate();
        }
    }
}