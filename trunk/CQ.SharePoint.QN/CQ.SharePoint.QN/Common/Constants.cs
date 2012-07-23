using System.Globalization;
using Microsoft.SharePoint;

namespace CQ.SharePoint.QN.Common
{
    public class Constants
    {
        public const string Title = "Title";
        public const string Modified = "Modified";
        public const string Created = "Created";
        public const string CategoryId = "CategoryId";
        public const string NewsId = "NewsId";
        public const string ListName = "ListName";
        public const string ListCategoryName = "ListCategoryName";
        public const string Draft = "Nháp";
        public const string Pending = "Đang chờ";
        public const string Approved = "Đã duyệt";
        public const string Published = "Xuất bản";
        public static string GetHomeTitle()
        {
            if (SPContext.Current.Web.Url.Contains("/en"))
            {
                return Utilities.GetConfigValue("home-title-en");
            }
            else
            {
                return Utilities.GetConfigValue("home-title");
            }
        }
        
        public class PageInWeb
        {
            public const string DefaultPage = "default";
            public const string DetailNews = "DetailNews";
            public const string SubPage = "SubPage";
            public const string DownloadPage = "Download";
            public const string GalleryPage = "Gallery";
            public const string VideoPage = "Video";
            public const string ShowGalleryPage = "ShowGallery";
            public const string ShowVideoPage = "ShowVideo";
            public const string Contact = "Contact";
            public const string TimKiem = "TimKiem";
            public const string AllRSS = "AllRSS";
            public const string RSS = "RSS";
        }

        public class ErrorMessage
        {
            public const string ResourceFileName = "CQ.SharePoint.QN";
            public const string Msg1 = "Không có bản nghi nào được tìm thấy!";
            public const string Msg2 = "MSG2";
            public const string Msg3 = "MSG3";
            public const string Msg4 = "MSG4";
            public const string Msg5 = "MSG5";
            public const string Msg6 = "MSG6";
            public const string Msg7 = "MSG7";
            public const string Msg8 = "MSG8";
            public const string Msg9 = "MSG9";
            public const string Msg10 = "MSG10";

            public const string Msg11 = "MSG11";
            public const string Msg12 = "MSG12";
            public const string Msg13 = "MSG13";
            public const string Msg14 = "MSG14";
            public const string Msg15 = "MSG15";
            public const string Msg17 = "Msg17";
            public const string Msg18 = "Msg18";
            public const string Msg19 = "MSG19";
            public const string Msg20 = "MSG20";
            public const string Msg23 = "MSG23";
            public const string Msg22 = "MSG22";
            public const string Msg24 = "MSG24";

            public const string MsgCannotApprove = "MSGCannotApprove";
            public const string MsgCannotResponse = "MSGCannotResponse";
            public const string MsgItemDeleted = "MSGItemDeleted";
            public const string MsgItemLocked = "MSGItemLocked";
            public const string MsgMustSelectApprovedOrRejected = "MSGMustSelectApprovedOrRejected";
            public const string Msg233 = "MSG233";
            public const string MsgCancel = "MSGCancel";
            public const string MsgConfigWrong = "Config data is wrong!";
            public const string SpMsg = "<br /><span class='ms-formvalidation' role='alert'>{0}</span>";
        }
    }

    public class PermissionCQQN
    {
        #region GroupName
        public class GroupName
        {
            
        }
        #endregion

        #region GroupDescription
        public class GroupDescription
        {
           

        }
        #endregion

        #region Roles
        public static class PermissionLevel
        {
            //public const string FullControl = "フル コントロール";//フル コントロール

            public const string Management = "管理";

            public const string Delete = "削除";

            public const string Edit = "編集";

            public const string Submitted = "投稿";

            public const string View = "閲覧";

            public const string Read = "Read";

            public const string Follow = "フォロー";
        }
        #endregion
    }
}
