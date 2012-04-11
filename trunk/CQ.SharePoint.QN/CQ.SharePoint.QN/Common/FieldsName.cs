
namespace CQ.SharePoint.QN.Common
{
    public static class FieldsName
    {
        public const string Title = "Title";
        #region Nested type: NewsCategory
        public static class NewsCategory
        {
            #region Nested type: ExpenseClaimEnglish
            public static class English
            {
                public const string Heading = "Heading";
                public const string ParentId = "ParentId";
                public const string ChildNumber = "ChildNumber";
                public const string Status = "Status";
            }
            #endregion
            #region Nested type: ExpenseClaimJapanese
            public static class VietNamese
            {
                public const string Heading = "Heading";
                public const string ParentId = "ParentId";
                public const string ChildNumber = "ChildNumber";
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
                public const string CategoryId = "CategoryId";
                public const string Status = "Status";
            }

            public static class VietNamese
            {
                public const string Heading = "Heading";
                public const string Content = "Content";
                public const string CategoryId = "CategoryId";
                public const string Status = "Status";
            }

            public static class FieldValuesDefault
            {
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
    }
}
