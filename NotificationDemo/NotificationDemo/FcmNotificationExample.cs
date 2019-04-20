using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEC.Notification;
using TEC.Notification.Model.Fcm;
using TEC.Notification.NotificationContents.Fcm;
using TEC.Notification.NotificationProviderConfigs.Fcm;
using TEC.Notification.NotificationProviders.Fcm;

namespace NotificationDemo
{
    /// <summary>
    /// Fcm 通知範例
    /// </summary>
    public class FcmNotificationExample
    {
        /// <summary>
        /// 取得 Fcm 提供者設定檔
        /// </summary>
        /// <returns></returns>
        public FcmProviderConfig getProviderConfig()
        {
            //產生 Fcm 提供者設定檔
            FcmProviderConfig fcmProviderConfig = new FcmProviderConfig()
            {
#warning 輸入伺服器金鑰
                //從 Firebase [設定] > [CLOUD MESSAGING] 中的 [伺服器金鑰]
                ServerKey = ""
            };
            return fcmProviderConfig;
        }

        /// <summary>
        /// 取得準備發送的 Fcm 通知
        /// </summary>
        public Notification getNotification()
        {
            Notification notification = new Notification()
            {
                NotificationProviderType = typeof(FcmNotificationProvider),
                ActivityID = Guid.NewGuid(),
                NotificationContent = new FcmNotificationContent()
                {
#warning 輸入收件人
                    //發送多個 Firebase 註冊 Token
                    //RegistrationIDs=new List<string>() {  },
                    //發送多主題條件 $"{custom topic name} in topics"
                    //Condition= $"Hello in topics||World in topics",
                    //發送單一主題 $"/topics/{custom topic name}
                    //To= $"/topics/Hello";
                    //To = "",

                    FcmNotificationMessage = new FcmNotificationMessage()
                    {
                        Title = "測試標題",
                        Body = "測試內容",
                    }
                }
            };
            return notification;
        }

        /// <summary>
        /// 使用 Fcm 通知提供者發送通知
        /// </summary>
        public void executeProviderSendNotification()
        {
            FcmNotificationProvider fcmNotificationProvider = new FcmNotificationProvider();

            //設定 Fcm 通知提供者設定檔
            fcmNotificationProvider.setProviderConfig(this.getProviderConfig());

            //新增通知結果物件
            NotificationResult notificationResult = new NotificationResult(this.getNotification());

            //發送通知
            fcmNotificationProvider.sendNotification(ref notificationResult);
        }
    }
}
