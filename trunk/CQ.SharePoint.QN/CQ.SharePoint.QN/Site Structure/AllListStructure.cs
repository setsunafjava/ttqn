using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using Microsoft.SharePoint;
using CQ.SharePoint.QN.Common;
using CQ.SharePoint.QN.Core.Helpers;
using System.Web.UI.WebControls;

namespace CQ.SharePoint.QN
{
    public class NewsCategoryStructure
    {
        public static void CreateListStructure(SPWeb web)
        {
            CreateNewsCategoryList(web);
            CreateNewsRecordsList(web);
            CreateSupportUserList(web);
            CreateCorporateCategoryList(web);
            CreateMenuList(web);
            CreateLinkSite(web);
            CreateServiceInfo(web);
            CreateManagerInfo(web);
            CreateProvinceInfoList(web);
            CreateCompanyAdvList(web);
        }
        /// <summary>
        /// Se chua nhung muc tin tuc, vi du: Tin Tinh Uy, Hoi Dong Nhan Dan, Thong tin lanh dao, So ban nghanh, dia phuong, doanh nghiep
        /// </summary>
        /// <param name="web"></param>
        public static void CreateNewsCategoryList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.NewsCategory,
                Name = ListsName.English.NewsCategory,
                OnQuickLaunch = true
            };
            
            helper.AddField(new LookupFieldCreator(FieldsName.NewsCategory.English.ParentName, FieldsName.NewsCategory.VietNamese.ParentName){LookupList = ListsName.English.NewsCategory, LookupField = FieldsName.NewsCategory.English.Heading});

            helper.AddField(new LookupFieldCreator(FieldsName.NewsCategory.English.ChildName, FieldsName.NewsCategory.VietNamese.ChildName) { LookupList = ListsName.English.NewsCategory, LookupField = FieldsName.NewsCategory.English.Heading });

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();

            var title = list.Fields.GetFieldByInternalName(FieldsName.Title);
            title.Title = FieldsName.NewsCategory.English.Heading;
            title.Update();

            list.EnableAttachments = true;

            list.Update();
        }

        public static void CreateNewsRecordsList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.NewsRecord,
                Name = ListsName.English.NewsRecord,
                OnQuickLaunch = true
            };
            
            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsRecord.English.Content, FieldsName.NewsRecord.VietNamese.Content){RichText = true, RichTextMode = SPRichTextMode.FullHtml});

            helper.AddField(new NumberFieldCreator(FieldsName.NewsRecord.English.ViewsCount, FieldsName.NewsRecord.VietNamese.ViewsCount));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsRecord.English.TieuBieu, FieldsName.NewsRecord.VietNamese.TieuBieu));

            //helper.AddField(new NumberFieldCreator(FieldsName.NewsRecord.English.CategoryId, FieldsName.NewsRecord.VietNamese.CategoryId));
            helper.AddField(new LookupFieldCreator(FieldsName.NewsRecord.English.CategoryName, FieldsName.NewsRecord.VietNamese.CategoryName) { LookupList = ListsName.English.NewsCategory, LookupField = FieldsName.NewsCategory.English.Heading });

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsRecord.English.ThumbnailImage, FieldsName.NewsRecord.VietNamese.ThumbnailImage));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsRecord.English.ShortContent, FieldsName.NewsRecord.VietNamese.ShortContent));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsRecord.English.LinkToRecord, FieldsName.NewsRecord.VietNamese.LinkToRecord));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();

            var title = list.Fields.GetFieldByInternalName(FieldsName.Title);
            title.Title = FieldsName.NewsRecord.English.Heading;
            title.Update();

            list.EnableAttachments = true;

            list.Update();
        }
        public static void CreateSupportUserList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.SupportUser,
                Name = ListsName.English.SupportUser,
                OnQuickLaunch = true
            };

            //helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.UserName, FieldsName.SupportUser.English.UserName));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.Phone, FieldsName.SupportUser.VietNamese.Phone));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.TelePhone, FieldsName.SupportUser.VietNamese.TelePhone));

            helper.AddField(new NumberFieldCreator(FieldsName.SupportUser.English.NickType, FieldsName.SupportUser.VietNamese.NickType));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.NickName, FieldsName.SupportUser.VietNamese.NickName));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.Email, FieldsName.SupportUser.VietNamese.Email));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();
            var title = list.Fields.GetFieldByInternalName(FieldsName.Title);
            title.Title = FieldsName.SupportUser.English.UserName;
            title.Update();
            list.EnableAttachments = true;

            list.Update();
        }

        public static void CreateCorporateCategoryList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.CorporateCategory,
                Name = ListsName.English.CorporateCategory,
                OnQuickLaunch = true
            };

            helper.AddField(new NumberFieldCreator(FieldsName.CorporateCategory.English.ParentId, FieldsName.CorporateCategory.VietNamese.ParentId));

            //helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateCategory.English.Name, FieldsName.CorporateCategory.VietNamese.Name));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateCategory.English.Phone, FieldsName.CorporateCategory.VietNamese.Phone));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateCategory.English.TelePhone, FieldsName.CorporateCategory.VietNamese.TelePhone));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.CorporateCategory.English.Information, FieldsName.CorporateCategory.VietNamese.Information));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateCategory.English.Email, FieldsName.CorporateCategory.VietNamese.Email));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();

            var title = list.Fields.GetFieldByInternalName(FieldsName.Title);
            title.Title = FieldsName.CorporateCategory.English.Name;
            title.Update();

            list.EnableAttachments = true;

            list.Update();
        }

        public static void CreateCorporateRecordList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.CorporateRecord,
                Name = ListsName.English.CorporateRecord,
                OnQuickLaunch = true
            };

            helper.AddField(new NumberFieldCreator(FieldsName.CorporateRecord.English.CorporateGroupId, FieldsName.CorporateRecord.English.CorporateGroupId));

            //helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateRecord.English.Name, FieldsName.CorporateRecord.VietNamese.Name));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateRecord.English.Phone, FieldsName.CorporateRecord.VietNamese.Phone));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateRecord.English.TelePhone, FieldsName.CorporateRecord.VietNamese.TelePhone));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.CorporateRecord.English.Information, FieldsName.CorporateRecord.VietNamese.Information));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateRecord.English.Email, FieldsName.CorporateRecord.VietNamese.Email));

            helper.AddField(new BooleanFieldCreator(FieldsName.CorporateRecord.English.TieuBieu, FieldsName.CorporateRecord.VietNamese.TieuBieu));//= true => doanh nghiep la tieu bieu

            helper.AddField(new BooleanFieldCreator(FieldsName.CorporateRecord.English.QuangCao, FieldsName.CorporateRecord.VietNamese.QuangCao));//= true => doanh nghiep duoc hien trong muc quang cao

            helper.AddField(new MultipleChoiceFieldCreator(FieldsName.CorporateRecord.English.Status, FieldsName.CorporateRecord.VietNamese.Status));//se co 3 trang thai cho field nay: Doanh nghiep moi thanh lap. doanh nghiep dang ky lai, doanh nghiep giai the

            SPList list = helper.Apply();
            var title = list.Fields.GetFieldByInternalName(FieldsName.Title);
            title.Title = FieldsName.CorporateRecord.English.Name;
            title.Update();
            list.EnableAttachments = true;

            list.Update();
        }

        public static void CreateMenuList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.MenuList,
                Name = ListsName.English.MenuList,
                OnQuickLaunch = true
            };

            helper.AddField(new SingleLineTextFieldCreator("Description", "Description"));
            helper.AddField(new SingleLineTextFieldCreator("Url", "Url"));
            helper.AddField(new NumberFieldCreator("Position", "Position") { Required = true });
            helper.AddField(new LookupFieldCreator("ParentId", "ParentId") { LookupList = ListsName.English.MenuList, LookupField = "Title" });

            SPList list = helper.Apply();

            list.Update();
        }

        public static void CreateLinkSite(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.LinkSite,
                Name = ListsName.English.LinkSite,
                OnQuickLaunch = true
            };

            helper.AddField(new SingleLineTextFieldCreator("Description", "Description"));
            helper.AddField(new SingleLineTextFieldCreator("Url", "Url"));
            helper.AddField(new NumberFieldCreator("Position", "Position") { Required = true });

            SPList list = helper.Apply();

            list.Update();
        }

        public static void CreateServiceInfo(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ServiceInfo,
                Name = ListsName.English.ServiceInfo,
                OnQuickLaunch = true
            };

            helper.AddField(new SingleLineTextFieldCreator("Description", "Description"));
            helper.AddField(new MultipleLinesTextFieldCreator("Body", "Body"){RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6});

            SPList list = helper.Apply();
            list.EnableAttachments = true;
            list.Update();
        }

        public static void CreateManagerInfo(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ManagerInfo,
                Name = ListsName.English.ManagerInfo,
                OnQuickLaunch = true
            };

            helper.AddField(new SingleLineTextFieldCreator("Description", "Description"));
            helper.AddField(new DateTimeFieldCreator("Date", "Date"));
            helper.AddField(new MultipleLinesTextFieldCreator("Body", "Body") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });

            SPList list = helper.Apply();
            list.EnableAttachments = true;
            list.Update();
        }

        public static void CreateProvinceInfoList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ProvinceInfoList,
                Name = ListsName.English.ProvinceInfoList,
                OnQuickLaunch = true
            };

            helper.AddField(new SingleLineTextFieldCreator("Description", "Description"));
            helper.AddField(new DateTimeFieldCreator("Date", "Date"));
            helper.AddField(new MultipleLinesTextFieldCreator("Body", "Body") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });

            SPList list = helper.Apply();
            list.EnableAttachments = true;
            list.Update();
        }

        public static void CreateCompanyAdvList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ConpanyAdvList,
                Name = ListsName.English.ConpanyAdvList,
                OnQuickLaunch = true
            };

            helper.AddField(new SingleLineTextFieldCreator("Description", "Description"));
            helper.AddField(new DateTimeFieldCreator("Date", "Date"));
            helper.AddField(new MultipleLinesTextFieldCreator("Body", "Body") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });

            SPList list = helper.Apply();
            list.EnableAttachments = true;
            list.Update();
        }
    }
}
