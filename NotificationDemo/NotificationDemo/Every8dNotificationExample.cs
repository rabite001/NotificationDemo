using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEC.Notification;
using TEC.Notification.NotificationContents.Sms;
using TEC.Notification.NotificationProviderConfigs.Sms;
using TEC.Notification.NotificationProviders.Sms;

namespace NotificationDemo
{
    /// <summary>
    /// EVERY8D 通知範例
    /// </summary>
    public class Every8dNotificationExample
    {
        /// <summary>
        /// 取得 Every8d 提供者設定檔
        /// </summary>
        /// <returns></returns>
        public Every8dProviderConfig getProviderConfig()
        {
            Every8dProviderConfig every8DProviderConfig = new Every8dProviderConfig()
            {
#warning 輸入 Every8d 帳號、密碼
                UserID = null,
                Password = null
            };
            return every8DProviderConfig;
        }

        /// <summary>
        /// 取得準備發送的 Every8d 通知
        /// </summary>
        /// <returns></returns>
        public Notification getNotification()
        {
            Notification notification = new Notification()
            {
                NotificationProviderType = typeof(Every8dNotificationProvider),
                ActivityID = Guid.NewGuid(),
                NotificationContent = new Every8dNotificationContent()
                {
                    Subject = "主旨",
                    Message = "內容",

#warning 修改成收件人電話號碼
                    Destinataires = new List<string>() { "收件人電話號碼" },
                    //RetryTime
                    //SendTime
                    //發送 MMS 簡訊必須設定 AttachmentImageBase64String 、 ImageType
                    //AttachmentImageBase64String = "Image Base64String",
                    //ImageType = Enums.Sms.Every8d.ImageType.Jpeg
                }
            };

            return notification;
        }

        /// <summary>
        /// 使用 Every8d 通知提供者發送通知
        /// </summary>
        public void executeProviderSendNotification()
        {
            Every8dNotificationProvider every8dNotificationProvider = new Every8dNotificationProvider();

            //設定 Every8d 通知提供者設定檔
            every8dNotificationProvider.setProviderConfig(this.getProviderConfig());

            //新增通知結果物件
            NotificationResult notificationResult = new NotificationResult(this.getNotification());

            //發送通知
            every8dNotificationProvider.sendNotification(ref notificationResult);
        }
    }
}
