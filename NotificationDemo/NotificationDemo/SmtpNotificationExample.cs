using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TEC.Notification;
using TEC.Notification.NotificationContents.Smtp;
using TEC.Notification.NotificationProviderConfigs.Smtp;
using TEC.Notification.NotificationProviders.Smtp;

namespace NotificationDemo
{
    /// <summary>
    /// Smtp 通知範例
    /// </summary>
    public class SmtpNotificationExample
    {
        /// <summary>
        /// 取得 SMTP 提供者設定檔
        /// </summary>
        /// <returns></returns>
        public SmtpProviderConfig getProviderConfig()
        {
            //產生 SMTP 提供者設定檔
            SmtpProviderConfig smtpProviderConfig = new SmtpProviderConfig()
            {
#warning SmtpServerHost
                SmtpServerHost = "",
                SmtpServerPort = 25,
                SmtpServerUserName = null,
                SmtpServerPassword = null,
                EnableSsl = false,
#warning 輸入寄件人
                MailFrom = new MailAddress("", "")
            };
            return smtpProviderConfig;
        }

        /// <summary>
        /// 取得準備發送的 Smtp 通知
        /// </summary>
        public Notification getNotification()
        {

            #region 內嵌圖檔
            string htmlBody = "<html><body><h1>Picture</h1><br><img src=\"cid:Pic1\"></body></html>";

            //圖檔路徑
            LinkedResource linkedResource = new LinkedResource(@"TecQRCode.png", System.Net.Mime.MediaTypeNames.Image.Jpeg);
            linkedResource.ContentId = "Pic1";

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, System.Net.Mime.MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(linkedResource);
            #endregion

            Notification notification = new Notification()
            {
                NotificationProviderType = typeof(SmtpNotificationProvider),
                ActivityID = Guid.NewGuid(),
                NotificationContent = new SmtpNotificationContent()
                {
                    Subject = "測試",
                    Body = "<h1>測試</h1>",
#warning 輸入收件人
                    To = new List<MailAddress>() { new MailAddress("", "") },
                    //Cc = new List<MailAddress>() { new MailAddress("", "") },
                    //Bcc = new List<MailAddress>() { new MailAddress("", "") },
                    //ReplyToList = new List<MailAddress>() { new MailAddress("", "") },
                    AlternateViews = new List<AlternateView>() { alternateView },
                }
            };

            return notification;
        }

        /// <summary>
        /// 使用 Smtp 通知提供者發送通知
        /// </summary>
        public void executeProviderSendNotification()
        {
            SmtpNotificationProvider smtpNotificationProvider = new SmtpNotificationProvider();

            //設定 Smtp 通知提供者設定檔
            smtpNotificationProvider.setProviderConfig(this.getProviderConfig());

            //新增通知結果物件
            NotificationResult notificationResult = new NotificationResult(this.getNotification());

            //發送通知
            smtpNotificationProvider.sendNotification(ref notificationResult);
        }
    }
}
