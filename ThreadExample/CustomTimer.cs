using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample
{

    public delegate void TimerTickedEventHandler(object sender, int currentTime);

    public class CustomTimer
    {
        private int currentTime;
        private Thread timerThread;
        private bool running;

        public event TimerTickedEventHandler OnTicked;

        public CustomTimer()
        {
            currentTime = 0;
            running = false;
        }

        public void Start()
        {
            if (timerThread == null) 
            {
                running = true;
                timerThread = new Thread(() => { TimerProcedure(); });
                timerThread.Start();
            }            
        }

        public void Stop()
        {
            //niet aanbevolen! Misschien onderbreek je de thread midden in zijn taak!
            //gooit een exception
            //if (timerThread != null) timerThread.Abort();
            //timerThread = null;

            running = false;
            timerThread = null;
        }

        private void TimerProcedure()
        {
            //wordt uitgevoerd in subthread
            while (running)
            {
                Tick();
            }
        }

        private void Tick()
        {
            Thread.Sleep(1000);
            currentTime++;

            OnTicked?.Invoke(this, currentTime);
        }
    }
}
