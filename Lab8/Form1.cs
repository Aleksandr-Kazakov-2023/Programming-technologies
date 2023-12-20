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
        Off,
        Green,
        GreenBlink,
        Yellow,
        Red,
        RedYellow
    }

    enum LightEvent
    {
        TurnOn,
        TurnOff,
        ShortWait,
        LongWait,
        Next
    }

    public partial class Form1 : Form
    {
        private FSM<LightState, LightEvent> fsm;
        private Graphics graphics;
        int diameter = 100;
        private int tickCount = 0;

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
                new() {FromState = LightState.Red, ToState = LightState.RedYellow, Event = LightEvent.LongWait},
                new() {FromState = LightState.RedYellow, ToState = LightState.Green, Event = LightEvent.ShortWait},
                new() {FromState = LightState.Green, ToState = LightState.GreenBlink, Event = LightEvent.LongWait},
                new() {FromState = LightState.GreenBlink, ToState = LightState.Red, Event = LightEvent.ShortWait}
            });

            fsm[LightState.Red].OnEnter = () =>
            {
                graphics.FillEllipse(new SolidBrush(Color.Crimson),
                    (pictureBox.Width / 2) - diameter / 2,
                    0, diameter, diameter);
                graphics.FillEllipse(new SolidBrush(Color.White),
                    (pictureBox.Width / 2) - diameter / 2,
                    diameter + 2, diameter, diameter);
                graphics.FillEllipse(new SolidBrush(Color.White),
                    (pictureBox.Width / 2) - diameter / 2,
                    diameter * 2 + 4, diameter, diameter);
            };

            fsm[LightState.Red].OnUpdate = () =>
            {
                tickCount++;
                if (tickCount == 5)
                    fsm.OnEvent(LightEvent.LongWait);
            };

            fsm[LightState.RedYellow].OnEnter = () =>
            {
                graphics.FillEllipse(new SolidBrush(Color.Crimson),
                    (pictureBox.Width / 2) - diameter / 2,
                    0, diameter, diameter);
                graphics.FillEllipse(new SolidBrush(Color.Yellow),
                    (pictureBox.Width / 2) - diameter / 2,
                    diameter + 2, diameter, diameter);
                graphics.FillEllipse(new SolidBrush(Color.White),
                    (pictureBox.Width / 2) - diameter / 2,
                    diameter * 2 + 4, diameter, diameter);
            };
            fsm[LightState.RedYellow].OnUpdate = () =>
            {
                tickCount++;
                if (tickCount == 7)
                    fsm.OnEvent(LightEvent.ShortWait);
            };

            fsm[LightState.Green].OnEnter = () =>
            {
                graphics.FillEllipse(new SolidBrush(Color.White),
                    (pictureBox.Width / 2) - diameter / 2,
                    0, diameter, diameter);
                graphics.FillEllipse(new SolidBrush(Color.White),
                    (pictureBox.Width / 2) - diameter / 2,
                    diameter + 2, diameter, diameter);
                graphics.FillEllipse(new SolidBrush(Color.ForestGreen),
                    (pictureBox.Width / 2) - diameter / 2,
                    diameter * 2 + 4, diameter, diameter);
            };
            fsm[LightState.Green].OnUpdate = () =>
            {
                tickCount++;
                if (tickCount == 12)
                {
                    fsm.OnEvent(LightEvent.LongWait);
                }
            };

            fsm[LightState.GreenBlink].OnEnter = () =>
            {
                graphics.FillEllipse(new SolidBrush(Color.White),
                    (pictureBox.Width / 2) - diameter / 2,
                    0, diameter, diameter);
                graphics.FillEllipse(new SolidBrush(Color.White),
                    (pictureBox.Width / 2) - diameter / 2,
                    diameter + 2, diameter, diameter);
                graphics.FillEllipse(new SolidBrush(Color.White),
                    (pictureBox.Width / 2) - diameter / 2,
                    diameter * 2 + 4, diameter, diameter);
            };
            fsm[LightState.GreenBlink].OnUpdate = () =>
            {
                tickCount++;
                if (tickCount % 2 == 0)
                    graphics.FillEllipse(new SolidBrush(Color.ForestGreen),
                        (pictureBox.Width / 2) - diameter / 2,
                        diameter * 2 + 4, diameter, diameter);
                else
                    graphics.FillEllipse(new SolidBrush(Color.White),
                        (pictureBox.Width / 2) - diameter / 2,
                        diameter * 2 + 4, diameter, diameter);

                if (tickCount == 18)
                {
                    fsm.OnEvent(LightEvent.ShortWait);
                    tickCount = 0;
                }
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