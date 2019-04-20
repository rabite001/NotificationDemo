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
            //通知管理員
            NotificationManager notificationManager = new NotificationManager();
            try
            {
                //發送通知方式
                //1.經由通知提供者發送通知
                //執行 2. 3. 4. 發送通知方式，需將提供者加入通知管理員
                //2.經由通知管理員 [直接] 發送通知
                //3.經由通知管理員 發送 [佇列] 的通知
                //4.經由 TimerManager 執行通知管理員發送通知

                #region Smtp 範例。要使用範例，請取消註解
                //#region 1.經由通知提供者發送通知
                //new SmtpNotificationExample().executeProviderSendNotification();
                //#endregion 
                //#region 執行 2. 3. 4. 發送通知方式，需將提供者加入通知管理員
                ////取得 smtp 通知提供者設定檔
                //SmtpProviderConfig smtpProviderSetting = new SmtpNotificationExample().getProviderConfig();
                ////將 smtp 提供者加入通知管理員
                //notificationManager.setNotificationProvider(typeof(SmtpNotificationProvider), smtpProviderSetting);
                ////取得 smtp 通知
                //Notification smtpNotification = new SmtpNotificationExample().getNotification();
                //#endregion
                //#region 2.經由通知管理員 [直接] 發送通知
                ////經由通知管理員直接發送通知，並取得發送結果
                //notificationResultList = notificationManager.sendNotification(smtpNotification);
                ////處理發送結果
                //foreach (NotificationResult notificationResult in notificationResultList)
                //{
                //    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                //    {
                //        //log
                //        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                //    }
                //    else
                //    {
                //        //log
                //        Console.WriteLine($"[直接]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                //    }
                //}
                //#endregion
                //#region 3.經由通知管理員發送 [佇列] 的通知 
                ////將通知加入通知管理員 [佇列]
                //notificationManager.queueNotification(smtpNotification);
                ////[佇列] 發送通知，並取得發送結果
                //notificationResultList = notificationManager.sendNotificationQueue();
                ////處理發送結果
                //foreach (NotificationResult notificationResult in notificationResultList)
                //{
                //    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                //    {
                //        //log
                //        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                //    }
                //    else
                //    {
                //        //log
                //        Console.WriteLine($"[發送佇列]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                //    }
                //}
                //#endregion
                //#region 4.經由 [TimerManager] 執行通知管理員發送通知
                ////將通知管理員放入 TimerEvent
                //SendNotificationTimerEvent sendNotificationTimerEvent = new SendNotificationTimerEvent(notificationManager);
                //sendNotificationTimerEvent.OnCompleted += Program.sendNotificationTimerEvent_OnCompleted;

                ////設定 sendNotificationTimerStorage
                //ThreadingTimerStorage sendNotificationTimerStorage = timerManager.createThreadingTimer(
                //    sendNotificationTimerEvent,
                //    new TimePeriodCollection(new[] { new TimeRange(DateTime.Now, DateTime.MaxValue, true) }),
                //    1000,
                //    NextTimeEvaluationType.ExecutionEndTime);

                //sendNotificationTimerStorage.OnUnhandledExceptionThrew += Program.sendNotificationTimerStorage_OnUnhandledExceptionThrew;

                ////啟用 SendNotificationTimerStorage
                //sendNotificationTimerStorage.start();

                ////將通知加入通知管理員的佇列
                //notificationManager.queueNotification(smtpNotification);

                //#endregion
                //#region 移除通知提供者
                //notificationManager.removeNotificationProvider(typeof(SmtpNotificationExample));
                //#endregion
                #endregion

                #region Fcm 範例。要使用範例，請取消註解
                //#region 1.經由通知提供者發送通知
                //new FcmNotificationExample().executeProviderSendNotification();
                //#endregion 
                //#region 執行 2. 3. 4. 發送通知方式，需將提供者加入通知管理員
                ////取得 fcm 通知提供者設定檔
                //FcmProviderConfig fcmProviderSetting = new FcmNotificationExample().getProviderConfig();
                ////將 fcm 提供者加入通知管理員
                //notificationManager.setNotificationProvider(typeof(FcmNotificationProvider), fcmProviderSetting);
                ////取得 fcm 通知
                //Notification fcmNotification = new FcmNotificationExample().getNotification();
                //#endregion
                //#region 2.經由通知管理員 [直接] 發送通知
                ////經由通知管理員直接發送通知，並取得發送結果
                //notificationResultList = notificationManager.sendNotification(fcmNotification);
                ////處理發送結果
                //foreach (NotificationResult notificationResult in notificationResultList)
                //{
                //    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                //    {
                //        //log
                //        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                //    }
                //    else
                //    {
                //        //log
                //        Console.WriteLine($"[直接]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                //    }
                //}
                //#endregion
                //#region 3.經由通知管理員發送 [佇列] 的通知 
                ////將通知加入通知管理員 [佇列]
                //notificationManager.queueNotification(fcmNotification);
                ////[佇列] 發送通知，並取得發送結果
                //notificationResultList = notificationManager.sendNotificationQueue();
                ////處理發送結果
                //foreach (NotificationResult notificationResult in notificationResultList)
                //{
                //    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                //    {
                //        //log
                //        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                //    }
                //    else
                //    {
                //        //log
                //        Console.WriteLine($"[發送佇列]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                //    }
                //}
                //#endregion
                //#region 4.經由 [TimerManager] 執行通知管理員發送通知
                ////將通知管理員放入 TimerEvent
                //SendNotificationTimerEvent sendNotificationTimerEvent = new SendNotificationTimerEvent(notificationManager);
                //sendNotificationTimerEvent.OnCompleted += Program.sendNotificationTimerEvent_OnCompleted;

                ////設定 sendNotificationTimerStorage
                //ThreadingTimerStorage sendNotificationTimerStorage = timerManager.createThreadingTimer(
                //    sendNotificationTimerEvent,
                //    new TimePeriodCollection(new[] { new TimeRange(DateTime.Now, DateTime.MaxValue, true) }),
                //    1000,
                //    NextTimeEvaluationType.ExecutionEndTime);

                //sendNotificationTimerStorage.OnUnhandledExceptionThrew += Program.sendNotificationTimerStorage_OnUnhandledExceptionThrew;

                ////啟用 SendNotificationTimerStorage
                //sendNotificationTimerStorage.start();

                ////將通知加入通知管理員的佇列
                //notificationManager.queueNotification(fcmNotification);

                //#endregion
                //#region 移除通知提供者
                //notificationManager.removeNotificationProvider(typeof(FcmNotificationExample));
                //#endregion
                #endregion

                #region EVERY8D 範例。要使用範例，請取消註解
                //#region 1.經由通知提供者發送通知
                ////new Every8dNotificationExample().executeProviderSendNotification();
                //#endregion 
                //#region 執行 2. 3. 4. 發送通知方式，需將提供者加入通知管理員
                ////取得 every8d 通知提供者設定檔
                //Every8dProviderConfig every8dProviderSetting = new Every8dNotificationExample().getProviderConfig();
                ////將 every8d 提供者加入通知管理員
                //notificationManager.setNotificationProvider(typeof(Every8dNotificationProvider), every8dProviderSetting);
                ////取得 every8d 通知
                //Notification every8dNotification = new Every8dNotificationExample().getNotification();
                //#endregion
                //#region 2.經由通知管理員 [直接] 發送通知
                ////經由通知管理員直接發送通知，並取得發送結果
                //notificationResultList = notificationManager.sendNotification(every8dNotification);
                ////處理發送結果
                //foreach (NotificationResult notificationResult in notificationResultList)
                //{
                //    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                //    {
                //        //log
                //        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                //    }
                //    else
                //    {
                //        //log
                //        Console.WriteLine($"[直接]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                //    }
                //}
                //#endregion
                //#region 3.經由通知管理員發送 [佇列] 的通知 
                ////將通知加入通知管理員 [佇列]
                //notificationManager.queueNotification(every8dNotification);
                ////[佇列] 發送通知，並取得發送結果
                //notificationResultList = notificationManager.sendNotificationQueue();
                ////處理發送結果
                //foreach (NotificationResult notificationResult in notificationResultList)
                //{
                //    if (notificationResult.NotificationResultType == NotificationResultType.Fail)
                //    {
                //        //log
                //        Console.WriteLine($"通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                //    }
                //    else
                //    {
                //        //log
                //        Console.WriteLine($"[發送佇列]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                //    }
                //}
                //#endregion
                //#region 4.經由 [TimerManager] 執行通知管理員發送通知
                ////將通知管理員放入 TimerEvent
                //SendNotificationTimerEvent sendNotificationTimerEvent = new SendNotificationTimerEvent(notificationManager);
                //sendNotificationTimerEvent.OnCompleted += Program.sendNotificationTimerEvent_OnCompleted;

                ////設定 sendNotificationTimerStorage
                //ThreadingTimerStorage sendNotificationTimerStorage = timerManager.createThreadingTimer(
                //    sendNotificationTimerEvent,
                //    new TimePeriodCollection(new[] { new TimeRange(DateTime.Now, DateTime.MaxValue, true) }),
                //    1000,
                //    NextTimeEvaluationType.ExecutionEndTime);

                //sendNotificationTimerStorage.OnUnhandledExceptionThrew += Program.sendNotificationTimerStorage_OnUnhandledExceptionThrew;

                ////啟用 SendNotificationTimerStorage
                //sendNotificationTimerStorage.start();

                ////將通知加入通知管理員的佇列
                //notificationManager.queueNotification(every8dNotification);

                //#endregion
                //#region 移除通知提供者
                ////notificationManager.removeNotificationProvider(typeof(Every8dNotificationExample));
                //#endregion
                //#region EVERY8D Api 呼叫
                //Every8dNotificationProvider every8dNotificationProvider = new Every8dNotificationProvider();

                ////設定 Every8d 通知提供者設定檔
                //every8dNotificationProvider.setProviderConfig(new Every8dNotificationExample().getProviderConfig());

                ////Api 皆以非同步作業的方式執行
                ////發送 MMS 簡訊
                //every8dNotificationProvider.sendMmsAsync(new SendMmsRequest() { });
                ////查詢 MMS 簡訊
                //every8dNotificationProvider.getMmsDeliveryStatusAsync(new GetMmsDeliveryStatusRequest() { });
                ////取消預約的 MMS 簡訊
                //every8dNotificationProvider.eraseMmsBookingAsync(new EraseMmsBookingRequest() { });
                ////發送 SMS 簡訊
                //every8dNotificationProvider.sendSmsAsync(new SendSmsRequest() { });
                ////查詢 SMS 簡訊
                //every8dNotificationProvider.getSmsDeliveryStatusAsync(new GetSmsDeliveryStatusRequest() { });
                ////取消預約的 SMS 簡訊
                //every8dNotificationProvider.eraseSmsBookingAsync(new EraseSmsBookingRequest() { });
                ////查詢餘額
                //every8dNotificationProvider.getCreditAsync();
                //#endregion
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
                    Console.WriteLine($"[TimerEvent]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}/t錯誤訊息:{JsonConvert.SerializeObject(notificationResult.NotificationErrorCollection)}");
                }
                else
                {
                    //Log
                    Console.WriteLine($"[TimerEvent]通知 ID :{notificationResult.Notification.ActivityID} 執行結果:{ notificationResult.NotificationResultType}");
                }
            }
        }
        #endregion

    }
}

