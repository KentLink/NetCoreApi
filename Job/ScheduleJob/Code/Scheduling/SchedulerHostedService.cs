using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Utility.Log;
using MDES.Api.Model.Arg;
using MDES.Api.Model.Test;
using MDES.Api.Server.Test;
using Newtonsoft.Json;
using NLog;
using ScheduleJob.Code.Cron;

namespace ScheduleJob.Code.Scheduling
{
    public class SchedulerHostedService : HostedService
    {
        public event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;
            
        private readonly List<SchedulerTaskWrapper> _scheduledTasks = new List<SchedulerTaskWrapper>();

        public SchedulerHostedService(IEnumerable<IScheduledTask> scheduledTasks)
        {
            var referenceTime = DateTime.UtcNow;
            
            foreach (var scheduledTask in scheduledTasks)
            {
                _scheduledTasks.Add(new SchedulerTaskWrapper
                {
                    Schedule = CrontabSchedule.Parse(scheduledTask.Schedule),
                    Task = scheduledTask,
                    NextRunTime = referenceTime
                });
            }
        }


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ExecuteOnceAsync(cancellationToken);

                await Task.Delay(Convert.ToInt32(ConfigProvider.Time), cancellationToken);
                //await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);



                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(1000);
                    LogUtility.Info(i.ToString());
                    Console.WriteLine(i.ToString());
                }

                try
                {
                    TestArg arg = new TestArg();
                    arg.Id = "1";

                    TestService test = new TestService();

                    //todo
                    List<TestResultModel> result = test.GetTestSqlData(arg);
                    LogUtility.Message(JsonConvert.SerializeObject(result), "Info");
                }
                catch (Exception ex)
                {
                    LogUtility.Message(ex.ToString(), "Error");
                }




                ////單執行會變成多執行續
                //var tasks = new List<Task>();
                //tasks.Add(Task.Factory.StartNew(() => {

                //    for (int i = 0; i < 20; i++)
                //    {
                //        Thread.Sleep(1000);
                //        LogUtility.Info(i.ToString());
                //        Console.WriteLine(i.ToString());
                //    }

                //    try
                //    {
                //        TestArg arg = new TestArg();
                //        arg.Id = "1";

                //        TestService test = new TestService();

                //        //todo
                //        List<TestResultModel> result = test.GetTestSqlData(arg);
                //        LogUtility.Message(JsonConvert.SerializeObject(result), "Info");
                //    }
                //    catch (Exception ex)
                //    {
                //        LogUtility.Message(ex.ToString(), "Error");
                //    }

                //}, cancellationToken));

                //寫在裡面會變成單執行續
                //var finalTask = Task.Factory.ContinueWhenAll(tasks.ToArray(), wordCountTasks=> { });
                //finalTask.Wait();



            }
        }

        private async Task ExecuteOnceAsync(CancellationToken cancellationToken)
        {
            var taskFactory = new TaskFactory(TaskScheduler.Current);
            var referenceTime = DateTime.UtcNow;
            
            var tasksThatShouldRun = _scheduledTasks.Where(t => t.ShouldRun(referenceTime)).ToList();

            foreach (var taskThatShouldRun in tasksThatShouldRun)
            {
                taskThatShouldRun.Increment();

                await taskFactory.StartNew(
                    async () =>
                    {
                        try
                        {
                            await taskThatShouldRun.Task.ExecuteAsync(cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            var args = new UnobservedTaskExceptionEventArgs(
                                ex as AggregateException ?? new AggregateException(ex));
                            
                            UnobservedTaskException?.Invoke(this, args);
                            
                            if (!args.Observed)
                            {
                                throw;
                            }
                        }
                    },
                    cancellationToken);
            }
        }

        private class SchedulerTaskWrapper
        {
            public CrontabSchedule Schedule { get; set; }
            public IScheduledTask Task { get; set; }

            public DateTime LastRunTime { get; set; }
            public DateTime NextRunTime { get; set; }

            public void Increment()
            {
                LastRunTime = NextRunTime;
                NextRunTime = Schedule.GetNextOccurrence(NextRunTime);
            }

            public bool ShouldRun(DateTime currentTime)
            {
                return NextRunTime < currentTime && LastRunTime != NextRunTime;
            }
        }
    }
}