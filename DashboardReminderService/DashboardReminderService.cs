using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DeliveryPortalDL;
using System.IO;
using System.Configuration;
using System.Timers;

namespace DashboardReminderService
{
    public partial class DashboardReminderService : ServiceBase
    {
        private  Timer timer_DeliveryDashboard;
        public DashboardReminderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            try
            {
                this.timer_DeliveryDashboard = new Timer();
                this.timer_DeliveryDashboard.Elapsed += timer_DeliveryDashboard_Elapsed;

                WindowsServiceSchedler(this.timer_DeliveryDashboard);

                if(!this.timer_DeliveryDashboard.Enabled)
                    this.timer_DeliveryDashboard.Start();

                EventLog.WriteEntry("timer_DeliveryDashboard_Elapsed = " + this.timer_DeliveryDashboard.Interval.ToString());
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message + " " + ex.InnerException);
                StreamWriter sr = new StreamWriter("d:\\errorLog.txt", true);
                sr.WriteLine(DateTime.Now);
                sr.WriteLine(ex.Message.ToString());
                sr.WriteLine(ex.InnerException);
                sr.WriteLine("-------------------------------------------");
                sr.WriteLine();

                sr.Close();
                sr.Dispose();


            }

        }

        void timer_DeliveryDashboard_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                EventLog.WriteEntry("timer_DeliveryDashboard_Elapsed started.");

                
                timer_DeliveryDashboard .Stop();
                //Call the ProcessFeefoResults                     
                ProjectDL projectDL = new ProjectDL();

                projectDL.SendReminderMails();

                WindowsServiceSchedler(timer_DeliveryDashboard);

                
            }

            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message + " " + ex.InnerException);
                StreamWriter sr = new StreamWriter("d:\\errorLog.txt", true);
                sr.WriteLine(DateTime.Now);
                sr.WriteLine(ex.Message.ToString());
                sr.WriteLine(ex.InnerException);
                sr.WriteLine("-------------------------------------------");
                sr.WriteLine();

                sr.Close();
                sr.Dispose();

                timer_DeliveryDashboard.Start();

            }


        }

        protected override void OnStop()
        {
            timer_DeliveryDashboard.Stop();
        }

        // This the static method  can be used for WindowsServiceSchedler 
        private static void WindowsServiceSchedler(System.Timers.Timer _timer)
        {

            string _runweekly = Convert.ToString(ConfigurationManager.AppSettings["Weekly"]);
            string _weeklyeventTriggerTime = Convert.ToString(ConfigurationManager.AppSettings["WeeklyeventTriggerTime"]);
            string _dayOfWeek = Convert.ToString(ConfigurationManager.AppSettings["DayOfWeek"]);
            DayOfWeek MyDays = (DayOfWeek)DayOfWeek.Parse(typeof(DayOfWeek), _dayOfWeek);
            string _DailyEventTriggerTime = Convert.ToString(ConfigurationManager.AppSettings["DailyEventTriggerTime"]);
            Scheduler sch = new Scheduler();
            if (_runweekly == "true")
            {

                sch.ScheduleWeekly(MyDays, _weeklyeventTriggerTime, _timer);
            }
            else
            {
                sch.ScheduleDaily(_DailyEventTriggerTime, _timer);
            }
        }

        static void Main(string[] args)
        {
            
            System.ServiceProcess.ServiceBase[] ServicesToRun;

            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new DashboardReminderService() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
            //if (Environment.UserInteractive)
            //{
            //    service.OnStart(args);
            //    Console.WriteLine("Press any key to stop program");
            //    Console.Read();
            //    service.OnStop();
            //}
            //else
            //{
                //ServiceBase.Run(service);
            //}

        }
    }
}
