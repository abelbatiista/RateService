using ApiService;
using ApiService.Models;
using EmailSender;
using RateService.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RateService
{
    public partial class RateService : ServiceBase
    {

        private readonly Timer timer;
        private readonly Api _service;
        private CRateModel _rates;
        private readonly DatabaseService _databaseService;

        public RateService()
        {
            InitializeComponent();
            timer = new Timer();
            _service = new Api();
            _databaseService = new DatabaseService();
        }

        protected override void OnStart(string[] args)
        {

            WrittingToFile($"The service was started at {DateTime.Now:dd/MM/yyyy hh:mm:ss}");

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000; //ONLY FOR EXAMPLE.
            //timer.Interval = (1000 * 60 * 60); //Miliseconds value.
            timer.Enabled = true;
            
        }

        protected override void OnStop()
        {

            WrittingToFile($"The service was stopped at {DateTime.Now:dd/MM/yyyy hh:mm:ss}");

        }

        private async void OnElapsedTime(object source, ElapsedEventArgs e)
        {

            WrittingToFile($"The service was executed again at {DateTime.Now:dd/MM/yyyy hh:mm:ss}");
            _rates = _service.ApiCall();
            await _databaseService.DoWork(_rates.Rates.JPY);

        }

        private void WrittingToFile(string message)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".txt";

            if (!File.Exists(filepath))
            {
                using (StreamWriter streamWriter = File.CreateText(filepath))
                {
                    streamWriter.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(filepath))
                {
                    streamWriter.WriteLine(message);
                }
            }

        }

    }
}
