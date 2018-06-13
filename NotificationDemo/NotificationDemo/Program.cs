using Itenso.TimePeriod;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEC.Core.Scheduler.Timers;
using TEC.Notification;
using TEC.Notification.Enums;
using TEC.Notification.Event;
using TEC.Notification.NotificationProviderConfigs.Fcm;
using TEC.Notification.NotificationProviderConfigs.Smtp;
using TEC.Notification.NotificationProviders.Fcm;
using TEC.Notification.NotificationProviders.Smtp;

namespace NotificationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //時間排程器的管理類別
            TimerManager timerManager = new TimerManager();

            //通知結果集合
            List<NotificationResult> notificationResultList;

            try
            {
                #region 設定通知管理員設定檔
                //設定通知管理員設定檔
                NotificationManager notificationManager = new NotificationManager();
                #endregion

                #region 設定 Smtp 提供者，並加入通知管理員，取得 smtp 通知
                //取得 smtp 通知提供者設定檔
                SmtpProviderConfig smtpProviderSetting = new SmtpNotificationExample().getSmtpProviderConfig();
                //將 Fcm 提供者加入通知管理員
                notificationManager.setNotificationProvider(typeof(SmtpNotificationProvider), smtpProviderSetting);
                //取得 smtp 通知
                Notification smtpNotification = new SmtpNotificationExample().getSmtpNotification();
                #endregion

                #region 設定 Fcm 提供者，並加入通知管理員，取得 Fcm 通知
                //取得 Fcm 提供者設定檔
                FcmProviderConfig fcmProviderConfig = new FcmNotificationExample().getFcmProviderConfig();
                //將 Fcm 提供者加入通知管理員
                notificationManager.setNotificationProvider(typeof(FcmNotificationProvider), fcmProviderConfig);
                //取得 Fcm 通知
                Notification fcmNotification = new FcmNotificationExample().getFcmNotification();
                #endregion

                #region 經由通知管理員 [直接] 發送 Smtp 通知
                //經由通知管理員直接發送通知，並取得發送結果
                notificationResultList = notificationManager.sendNotification(smtpNotification);
                //處理發送結果
                foreach (NotificationResult notificationResult in notificationResultList)
                {
                    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                    {
                        //log
                        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                    }
                    else
                    {
                        //log
                        Console.WriteLine($"[直接]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                    }
                }
                #endregion

                #region 經由通知管理員 [直接] 發送 FCM 通知
                //經由通知管理員直接發送通知，並取得發送結果
                notificationResultList = notificationManager.sendNotification(fcmNotification);
                //處理發送結果
                foreach (NotificationResult notificationResult in notificationResultList)
                {
                    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                    {
                        //log
                        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                    }
                    else
                    {
                        //log
                        Console.WriteLine($"[直接]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                    }
                }
                #endregion

                #region 經由通知管理員 [佇列] 發送 Smtp 通知 
                //將通知加入通知管理員 [佇列]
                notificationManager.queueNotification(smtpNotification);
                //[佇列] 發送通知，並取得發送結果
                notificationResultList = notificationManager.sendNotificationQueue();

                //處理發送結果
                foreach (NotificationResult notificationResult in notificationResultList)
                {
                    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                    {
                        //log
                        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                    }
                    else
                    {
                        //log
                        Console.WriteLine($"[直接]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                    }
                }
                #endregion

                #region 經由通知管理員 [佇列] 發送 Fcm 通知 
                //將通知加入通知管理員 [佇列]
                notificationManager.queueNotification(fcmNotification);
                //[佇列] 發送通知，並取得發送結果
                notificationResultList = notificationManager.sendNotificationQueue();

                //處理發送結果
                foreach (NotificationResult notificationResult in notificationResultList)
                {
                    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                    {
                        //log
                        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                    }
                    else
                    {
                        //log
                        Console.WriteLine($"[直接]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                    }
                }
                #endregion

                #region 經由通知管理員 [TimerManager] 執行通知管理員發送通知
                //將通知管理員放入 TimerEvent
                SendNotificationTimerEvent sendNotificationTimerEvent = new SendNotificationTimerEvent(notificationManager);
                sendNotificationTimerEvent.OnCompleted += Program.sendNotificationTimerEvent_OnCompleted;

                //設定 sendNotificationTimerStorage
                ThreadingTimerStorage sendNotificationTimerStorage = timerManager.createThreadingTimer(
                    sendNotificationTimerEvent,
                    new TimePeriodCollection(new[] { new TimeRange(DateTime.Now, DateTime.MaxValue, true) }),
                    1000,
                    NextTimeEvaluationType.ExecutionEndTime);

                sendNotificationTimerStorage.OnUnhandledExceptionThrew += Program.sendNotificationTimerStorage_OnUnhandledExceptionThrew;

                //啟用 SendNotificationTimerStorage
                sendNotificationTimerStorage.start();

                //將通知加入通知管理員的佇列
                notificationManager.queueNotification(smtpNotification);
                notificationManager.queueNotification(fcmNotification);

                #endregion

                #region 經由通知提供者發送通知
                new SmtpNotificationExample().sendSmtpNotification();

                new FcmNotificationExample().sendFcmNotification();

                #endregion

                #region 移除通知提供者
                notificationManager.removeNotificationProvider(typeof(SmtpNotificationExample));
                #endregion
            }
            catch (Exception exception)
            {
                //Log
                Console.WriteLine(exception.Message);
            }

            Console.ReadKey();
        }

        #region 經由通知管理員 [TimerManager] 執行通知管理員發送通知事件監聽
        /// <summary>
        /// 當在執行排程事件時，有未處理的例外狀況發生時引發
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void sendNotificationTimerStorage_OnUnhandledExceptionThrew(object sender, UnhandledExceptionEventArgs e)
        {
            //Log
        }

        /// <summary>
        /// 當執行完一次發送通知時發生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void sendNotificationTimerEvent_OnCompleted(object sender, NotificationEventArgs e)
        {
            foreach (NotificationResult notificationResult in e.NotificationResultList)
            {
                if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                {
                    //Log
                    Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                }
                else
                {
                    //Log
                    Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                }
            }
        }
        #endregion

    }
}

