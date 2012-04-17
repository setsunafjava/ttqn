
namespace CQ.SharePoint.QN.Common
{
    public static class FieldsName
    {
        public const string Title = "Title";
        public const string Id = "ID";
        #region Nested type: NewsCategory
        public static class NewsCategory
        {
            #region Nested type: ExpenseClaimEnglish
            public static class English
            {
                public const string Heading = "Heading";
                public const string ParentName = "ParentName";
                public const string ParentId = "ParentId";
                public const string ChildName = "ChildName";
                public const string Status = "Status";
            }
            #endregion
            #region Nested type: ExpenseClaimJapanese
            public static class VietNamese
            {
                public const string Heading = "Heading";
                public const string ParentName = "ParentName";
                public const string ParentId = "ParentId";
                public const string ChildName = "ChildName";
                public const string Status = "Status";
            }
            #endregion

            #region Nested type: ExpenseClaimFieldValuesDefault
            public static class FieldValuesDefault
            {
            }
            #endregion
        }
        #endregion

        #region Nested type: News Record
        public static class NewsRecord
        {

            public static class English
            {
                public const string Heading = "Heading";
                public const string Content = "Content";
                public const string CategoryName = "CategoryName";
                public const string ThumbnailImage = "Thumbnail";
                public const string ShortContent = "ShortContent";
                public const string ViewsCount = "ViewsCount"; //So luong nguoi doc
                public const string TieuBieu = "TieuBieu";
                public const string LinkToRecord = "LinkToRecord";
                public const string Status = "Status";
            }

            public static class VietNamese
            {
                public const string Heading = "Heading";
                public const string Content = "Content";
                public const string ViewsCount = "ViewsCount"; //So luong nguoi doc
                public const string TieuBieu = "TieuBieu";
                public const string CategoryName = "CategoryName";
                public const string ThumbnailImage = "Thumbnail";
                public const string ShortContent = "ShortContent";
                public const string LinkToRecord = "LinkToRecord";
                public const string Status = "Status";
            }

            public static class FieldValuesDefault
            {
                public const string TinhUy = "Tỉnh Ủy";
                public const string HoiDongNhanDan = "Hội đồng nhân dân";
                public const string UyBanNhanDan = "Ủy ban nhân dân";
                public const string CaiCachHanhChinh = "Cải cách hành chính";

            }
        }
        #endregion

        #region Nested type: SupportUser
        public static class SupportUser
        {
            public static class English
            {
                public const string UserName = "UserName";
                public const string Phone = "Phone";
                public const string TelePhone = "TelePhone";
                public const string NickType = "NickType";
                public const string NickName = "NickName";
                public const string Email = "Email";
                public const string Status = "Status";
            }

            public static class VietNamese
            {
                public const string UserName = "UserName";
                public const string Phone = "Phone";
                public const string TelePhone = "TelePhone";
                public const string NickType = "NickType";//Dropdownlist
                public const string NickName = "NickName";
                public const string Email = "Email";
                public const string Status = "Status";
            }
        }
        #endregion

        #region Nested type: Corporate
        public static class CorporateCategory
        {
            public static class English
            {
                public const string ParentId = "ParentId";
                public const string Name = "Name";
                public const string Phone = "Phone";
                public const string TelePhone = "Phone";
                public const string Information = "Information";
                public const string Email = "Email";
                public const string Status = "Status";
            }

            public static class VietNamese
            {
                public const string ParentId = "ParentId";
                public const string Name = "Name";
                public const string Phone = "Phone";
                public const string TelePhone = "Phone";
                public const string Information = "Information";
                public const string Email = "Email";
                public const string Status = "Status";
            }
        }
        #endregion

        #region Nested type: CorporateRecord
        public static class CorporateRecord
        {
            public static class English
            {
                public const string CorporateGroupId = "CorporateGroupId";
                public const string Name = "Name";
                public const string Phone = "Phone";
                public const string TelePhone = "Phone";
                public const string Information = "Information";
                public const string Email = "Email";
                public const string TieuBieu = "TieuBieu";
                public const string QuangCao = "QuangCao";//Neu la true => se hien len muc quang cao' o home page
                public const string Status = "Status";//Doanh nghiep moi la true, doanh nghiep thay doi la` false
            }

            public static class VietNamese
            {
                public const string CorporateGroupId = "CorporateGroupId";
                public const string Name = "Name";
                public const string Phone = "Phone";
                public const string TelePhone = "Phone";
                public const string Information = "Information";
                public const string Email = "Email";
                public const string TieuBieu = "TieuBieu";
                public const string QuangCao = "QuangCao";
                public const string Status = "Status";
            }
        }
        #endregion

        #region Nested type: AllDocument
        public static class AllDocument
        {
            public static class English
            {
                public const string DocumentType = "DocumentType";
                public const string Name = "Name";
                public const string Status = "Status";
            }

            public static class VietNamese
            {
                public const string DocumentType = "DocumentType";
                public const string Name = "Name";
                public const string Status = "Status";
            }
        }
        #endregion

        #region Nested type:CQQNResources
        public class CQQNResources
        {
            // English --> Internal Name
            public class English
            {
                public const string ResourceType = "ResourceType";
                public const string FileLeafRef = "FileLeafRef";
            }

            // Japanese --> Display Name
            public class Japanese
            {
                public const string ResourceType = "Resource Type";
                public const string FileLeafRef = "FileLeafRef";
            }

            // There are metadata value of field in SharePoint list
            public class FieldValuesDefault
            {
                public struct Type
                {
                    public const string CSS = "CSS";
                    public const string JS = "JS";
                    public const string IMAGE = "IMAGE";
                    public const string TEMPLATE = "TEMPLATE";
                }
            }
        }
        #endregion

    }
}
