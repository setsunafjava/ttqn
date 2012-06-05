
namespace CQ.SharePoint.QN.Common
{
    public static class FieldsName
    {
        public const string Title = "Title";
        public const string Modified = "Modified";
        public const string Created = "Created";
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
                public const string TypeCategory = "TypeCategory"; //code here: chua add vao list - fields multichoice
                public const string Position = "Position";
                public const string Status = "Status";
            }
            #endregion
            #region Nested type: ExpenseClaimJapanese
            public static class VietNamese
            {
                public const string Heading = "Tên mục tin";
                public const string ParentName = "Tên mục tin cấp cha";
                public const string TypeCategory = "Kiểu mục tin";
                public const string Status = "Trạng Thái";
            }
            #endregion

            #region Nested type: ExpenseClaimFieldValuesDefault
            public static class FieldValuesDefault
            {
                public const string TinTuc = "Tin Tức";
                public const string DoanhNghiep = "Doanh Nghiệp";
                public const string TinhUy = "Tỉnh Ủy";
                public const string SelectType = "SelectType";
                public const string SelectType1 = "SelectType1";
                public const string SelectType2 = "SelectType2";
                public const string SelectType3 = "SelectType3";
                public const string DuLich = "Du Lịch";
                public const string BanNenBiet = "Bạn Nên Biết";
                public const string CacCapChinhQuyen = "Các Cấp Chính Quyền";
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
                public const string ShortDescription = "ShortDescription";
                public const string ViewsCount = "ViewsCount"; //So luong nguoi doc
                public const string FocusNews = "FocusNews";
                public const string ShowInHomePage = "ShowInHomePage";
                public const string LinkAdv = "LinkAdv";
                public const string Status = "Status";
            }

            public static class VietNamese
            {
                public const string Heading = "Tiêu đề";
                public const string Content = "Nội dung";
                public const string ViewsCount = "Số lượng xem"; //So luong nguoi doc
                public const string FocusNews = "Tin tiêu biểu";
                public const string CategoryName = "CategoryName";
                public const string ThumbnailImage = "Ảnh đại diện";
                public const string ShortContent = "Mô tả ngắn gọn bài viết";
                public const string ShowInHomePage = "Hiển thị ở trang chủ";
                public const string LinkAdv = "Liên kết trang web";
                public const string Status = "Trạng thái";
            }

            public static class FieldValuesDefault
            {
                public const string TinhUy = "Tỉnh Ủy";
                public const string HoiDongNhanDan = "Hội đồng nhân dân";
                public const string UyBanNhanDan = "Ủy ban nhân dân";
                public const string CaiCachHanhChinh = "Cải cách hành chính";
                public const string SoBanNganh = "Sở ban ngành";
                public const string DiaPhuong = "Địa Phương";
                public const string DoanhNghiep = "Doanh nghiệp";

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
        public static class CompanyCategory
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
        public static class CompanyRecord
        {
            public static class English
            {
                public const string CorporateGroupId = "CorporateGroupId";
                public const string Name = "Name";
                public const string Phone = "Phone";
                public const string TelePhone = "Phone";
                public const string Information = "Information";
                public const string Email = "Email";
                public const string FocusNews = "FocusNews";
                public const string QuangCao = "QuangCao";//Neu la true => se hien len muc quang cao' o home page
                public const string Dissolved = "Dissolved";
                public const string ChangeInformation = "ChangeInformation";
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
                public const string FocusNews = "FocusNews";
                public const string QuangCao = "QuangCao";
                public const string Dissolved = "Dissolved";//Giai the
                public const string ChangeInformation = "ChangeInformation";//Thay doi thong tin
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

        #region AdvertisingBotton
        public static class ProductCategory
        {
            public static class English
            {
                public const string Name = "Name";
                public const string ParentId = "ParentId";
                public const string Description = "Description";
                public const string Status = "Status";
            }

            public static class VietNamese
            {
                public const string Name = "Name";
                public const string ParentId = "ParentId";
                public const string Description = "Description";
                public const string Status = "Status";
            }
        }
        #endregion

        #region AdvertisingBotton
        public static class ProductDetail
        {
            public static class English
            {
                public const string CategoryId = "CategoryId";
                public const string Name = "Name";
                public const string Price = "Price";
                public const string Description = "Description";
                public const string Status = "Status";
            }

            public static class VietNamese
            {
                public const string CategoryId = "CategoryId";
                public const string Price = "Price";
                public const string Description = "Description";
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
                    public const string XML = "XML";
                }
            }
        }
        #endregion

    }
}
