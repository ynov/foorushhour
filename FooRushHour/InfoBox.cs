using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace FooRushHour
{
    public partial class InfoBox : UserControl
    {
        public int MovementCount { get; private set; }
        public int TimerCount { get; private set; }
        private Task _timerTask = null;
        private bool _timerStop = false;

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
            if (_timerTask != null)
            {
                ResumeTimer();
                return;
            }

            _timerTask = new Task(() => {
                while (!_timerStop)
                {
                    Thread.Sleep(100);
                    Invoke(new Action(() =>
                    {
                        IncrementTimer();
                    }));
                }
            });

            _timerTask.Start();
        }

        public void StopTimer()
        {
            _timerStop = true;
        }

        public void ResumeTimer()
        {
            _timerStop = false;
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
    }
}
