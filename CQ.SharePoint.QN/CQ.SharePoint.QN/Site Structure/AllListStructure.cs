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
            CreateCompanyCategoryList(web);
            CreateCompanyRecordList(web);
            CreateMenuList(web);
            CreateLinkSite(web);
            CreateServiceInfo(web);
            CreateManagerInfo(web);
            CreateProvinceInfoList(web);
            CreateCompanyAdvList(web);
            CreateImageCatList(web);
            CreateImageAlbumList(web);
            CreateImagesList(web);
            CreateVideoCatList(web);
            CreateVideosList(web);
            CreateCustomerAdvList(web);
            CreateAdvList(web);
            CreateProductCategory(web);
            CreateProductDetail(web);
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

            helper.AddField(new LookupFieldCreator(FieldsName.NewsCategory.English.ParentName, FieldsName.NewsCategory.VietNamese.ParentName) { LookupList = ListsName.English.NewsCategory, LookupField = FieldsName.NewsCategory.English.Heading });

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

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsRecord.English.Content, FieldsName.NewsRecord.VietNamese.Content) { RichText = true, RichTextMode = SPRichTextMode.FullHtml });

            helper.AddField(new NumberFieldCreator(FieldsName.NewsRecord.English.ViewsCount, FieldsName.NewsRecord.VietNamese.ViewsCount));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsRecord.English.FocusNews, FieldsName.NewsRecord.VietNamese.FocusNews));

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

        public static void CreateCompanyCategoryList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.CompanyCategory,
                Name = ListsName.English.CompanyCategory,
                OnQuickLaunch = true
            };

            helper.AddField(new NumberFieldCreator(FieldsName.CompanyCategory.English.ParentId, FieldsName.CompanyCategory.VietNamese.ParentId));

            //helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateCategory.English.Name, FieldsName.CorporateCategory.VietNamese.Name));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CompanyCategory.English.Phone, FieldsName.CompanyCategory.VietNamese.Phone));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CompanyCategory.English.TelePhone, FieldsName.CompanyCategory.VietNamese.TelePhone));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.CompanyCategory.English.Information, FieldsName.CompanyCategory.VietNamese.Information));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CompanyCategory.English.Email, FieldsName.CompanyCategory.VietNamese.Email));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();

            var title = list.Fields.GetFieldByInternalName(FieldsName.Title);
            title.Title = FieldsName.CompanyCategory.English.Name;
            title.Update();

            list.EnableAttachments = true;

            list.Update();
        }

        public static void CreateCompanyRecordList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.CompanyRecord,
                Name = ListsName.English.CompanyRecord,
                OnQuickLaunch = true
            };

            helper.AddField(new NumberFieldCreator(FieldsName.CompanyRecord.English.CorporateGroupId, FieldsName.CompanyRecord.English.CorporateGroupId));

            //helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateRecord.English.Name, FieldsName.CorporateRecord.VietNamese.Name));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CompanyRecord.English.Phone, FieldsName.CompanyRecord.VietNamese.Phone));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CompanyRecord.English.TelePhone, FieldsName.CompanyRecord.VietNamese.TelePhone));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.CompanyRecord.English.Information, FieldsName.CompanyRecord.VietNamese.Information));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CompanyRecord.English.Email, FieldsName.CompanyRecord.VietNamese.Email));

            helper.AddField(new BooleanFieldCreator(FieldsName.CompanyRecord.English.FocusNews, FieldsName.CompanyRecord.VietNamese.FocusNews));//= true => doanh nghiep la tieu bieu

            helper.AddField(new BooleanFieldCreator(FieldsName.CompanyRecord.English.Dissolved, FieldsName.CompanyRecord.VietNamese.Dissolved));

            helper.AddField(new BooleanFieldCreator(FieldsName.CompanyRecord.English.ChangeInformation, FieldsName.CompanyRecord.VietNamese.ChangeInformation));

            helper.AddField(new BooleanFieldCreator(FieldsName.CompanyRecord.English.QuangCao, FieldsName.CompanyRecord.VietNamese.QuangCao));//= true => doanh nghiep duoc hien trong muc quang cao

            helper.AddField(new MultipleChoiceFieldCreator(FieldsName.CompanyRecord.English.Status, FieldsName.CompanyRecord.VietNamese.Status));//se co 3 trang thai cho field nay: Doanh nghiep moi thanh lap. doanh nghiep dang ky lai, doanh nghiep giai the

            SPList list = helper.Apply();
            var title = list.Fields.GetFieldByInternalName(FieldsName.Title);
            title.Title = FieldsName.CompanyRecord.English.Name;
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
            helper.AddField(new MultipleLinesTextFieldCreator("Body", "Body") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });

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

        public static void CreateImageCatList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ImageCatList,
                Name = ListsName.English.ImageCatList,
                OnQuickLaunch = true,
                ListTemplateType = SPListTemplateType.PictureLibrary
            };
            SPList list = helper.Apply();
            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.Required = true;
            titleField.Update();
            list.Update();
        }

        public static void CreateImageAlbumList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ImageAlbumList,
                Name = ListsName.English.ImageAlbumList,
                OnQuickLaunch = true,
                ListTemplateType = SPListTemplateType.PictureLibrary
            };
            SPList list = helper.Apply();
            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.Required = true;
            titleField.Update();
            list.Update();
        }

        public static void CreateImagesList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ImagesList,
                Name = ListsName.English.ImagesList,
                OnQuickLaunch = true,
                ListTemplateType = SPListTemplateType.PictureLibrary
            };
            helper.AddField(new LookupFieldCreator("CatID", "Chuyên mục") { LookupList = ListsName.English.ImageCatList, LookupField = "Title" });
            helper.AddField(new LookupFieldCreator("AlbumID", "Album ảnh") { LookupList = ListsName.English.ImageAlbumList, LookupField = "Title" });
            SPList list = helper.Apply();
            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.Required = true;
            titleField.Update();
            list.Update();
        }

        public static void CreateVideoCatList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.VideoCatList,
                Name = ListsName.English.VideoCatList,
                OnQuickLaunch = true
            };

            helper.AddField(new MultipleLinesTextFieldCreator("Description", "Description") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });

            SPList list = helper.Apply();
            list.EnableAttachments = true;
            list.Update();
        }

        public static void CreateVideosList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.VideosList,
                Name = ListsName.English.VideosList,
                OnQuickLaunch = true,
                ListTemplateType = SPListTemplateType.DocumentLibrary
            };
            helper.AddField(new LookupFieldCreator("CatID", "Chuyên mục") { LookupList = ListsName.English.VideoCatList, LookupField = "Title" });
            helper.AddField(new MultipleLinesTextFieldCreator("Description", "Description") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });
            SPList list = helper.Apply();
            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.Required = true;
            titleField.Update();
            list.Update();
        }

        public static void CreateCustomerAdvList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.CustomerAdvList,
                Name = ListsName.English.CustomerAdvList,
                OnQuickLaunch = true
            };

            helper.AddField(new MultipleLinesTextFieldCreator("Description", "Description") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });

            SPList list = helper.Apply();
            list.EnableAttachments = true;
            list.Update();
        }

        public static void CreateAdvList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.AdvList,
                Name = ListsName.English.AdvList,
                OnQuickLaunch = true,
                ListTemplateType = SPListTemplateType.DocumentLibrary
            };
            helper.AddField(new LookupFieldCreator("CustomerID", "Khách hàng") { LookupList = ListsName.English.CustomerAdvList, LookupField = "Title" });
            helper.AddField(new SingleLineTextFieldCreator("Url", "Url"));
            helper.AddField(new NumberFieldCreator("Count", "Số lượt click") { DefaultValue = "0" });
            helper.AddField(new MultipleLinesTextFieldCreator("Description", "Description") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });
            SPList list = helper.Apply();
            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.Required = true;
            titleField.Update();
            list.Update();
        }

        public static void CreateProductCategory(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ProductCategory,
                Name = ListsName.English.ProductCategory,
                OnQuickLaunch = true
            };
            helper.AddField(new NumberFieldCreator(FieldsName.ProductCategory.English.ParentId, FieldsName.ProductCategory.VietNamese.ParentId));
            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.ProductCategory.English.Description, FieldsName.ProductCategory.VietNamese.Description));
            helper.AddField(new BooleanFieldCreator(FieldsName.ProductCategory.English.Status, FieldsName.ProductCategory.VietNamese.Status));

            SPList list = helper.Apply();
            var titleField = list.Fields.GetFieldByInternalName(Constants.Title);
            titleField.Required = true;
            titleField.Title = FieldsName.ProductCategory.VietNamese.Name;
            titleField.Update();
            list.Update();
        }

        public static void CreateProductDetail(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ProductDetail,
                Name = ListsName.English.ProductDetail,
                OnQuickLaunch = true
            };
            helper.AddField(new NumberFieldCreator(FieldsName.ProductDetail.English.CategoryId, FieldsName.ProductDetail.VietNamese.CategoryId));
            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.ProductDetail.English.Description, FieldsName.ProductDetail.VietNamese.Description));
            helper.AddField(new NumberFieldCreator(FieldsName.ProductDetail.English.Price, FieldsName.ProductDetail.VietNamese.Price));
            helper.AddField(new BooleanFieldCreator(FieldsName.ProductDetail.English.Status, FieldsName.ProductDetail.VietNamese.Status));

            SPList list = helper.Apply();
            var titleField = list.Fields.GetFieldByInternalName(Constants.Title);
            titleField.Required = true;
            titleField.Title = FieldsName.ProductDetail.VietNamese.Name;
            titleField.Update();
            list.Update();
        }
    }
}
