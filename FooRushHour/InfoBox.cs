using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace FooRushHour
{
    public partial class InfoBox : UserControl
    {
        public int MovementCount { get; private set; }
        public int TimerCount { get; private set; }
        private Timer _timer;

        private static InfoBox _instance = null;

        public static InfoBox Instance {
            get
            {
                if (_instance == null)
                {
                    _instance = new InfoBox();
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        private InfoBox()
        {
            InitializeComponent();
            ResetMovementCount();
            ResetTimer();
            StartTimer();
        }

        public void ResetAll()
        {
            LabelGoal.Text = "-";
            StopTimer();
            ResetTimer();
            ResetMovementCount();
            StartTimer();
        }

        public void ResetTimer()
        {
            TimerCount = 0;
            LabelTimer.Text = TimerCount.ToString();
        }

        public void IncrementTimer()
        {
            TimerCount++;
            LabelTimer.Text = ((double)((double)TimerCount / (double)10)).ToString() + "s";
        }

        public void StartTimer()
        {
            if (_timer != null)
                return;

            _timer = new Timer();
            _timer.Tick += new EventHandler((s, e) => IncrementTimer());
            _timer.Start();
        }

        public void StopTimer()
        {
            if (_timer != null)
                _timer.Stop();
            _timer = null;
        }

        public void ResetMovementCount()
        {
            MovementCount = 0;
            LabelMvCount.Text = MovementCount.ToString();
        }

        public void IncementMovementCount()
        {
            MovementCount++;
            LabelMvCount.Text = MovementCount.ToString();
        }

        public void GoalReached()
        {
            StopTimer();
            LabelGoal.Text = "Goal Reached!";
        }
    }
}
