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
            //CreateCompanyCategoryList(web);
            //CreateCompanyRecordList(web);
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
            CreateDownloadCatList(web);
            CreateDownloadList(web);
            CreateQNConfigList(web);
            CreateQNTVList(web);
            CreateStatisticsList(web);
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

            StringCollection collect1 = new StringCollection();
            collect1.AddRange(new string[] { FieldsName.NewsCategory.FieldValuesDefault.TinTuc, FieldsName.NewsCategory.FieldValuesDefault.DoanhNghiep, FieldsName.NewsCategory.FieldValuesDefault.DuLich, FieldsName.NewsCategory.FieldValuesDefault.TinhUy });
            helper.AddField(new ChoiceFieldCreator(FieldsName.NewsCategory.English.TypeCategory, FieldsName.NewsCategory.VietNamese.TypeCategory)
            {
                Choices = collect1
            });

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

            helper.AddField(new NumberFieldCreator(FieldsName.NewsRecord.English.ViewsCount, FieldsName.NewsRecord.VietNamese.ViewsCount));

            helper.AddField(new LookupFieldCreator(FieldsName.NewsRecord.English.CategoryName, FieldsName.NewsRecord.VietNamese.CategoryName) { LookupList = ListsName.English.NewsCategory, LookupField = FieldsName.NewsCategory.English.Heading, AllowMultipleValues = true});

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsRecord.English.ThumbnailImage, FieldsName.NewsRecord.VietNamese.ThumbnailImage));
            
            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsRecord.English.Content, FieldsName.NewsRecord.VietNamese.Content) { RichText = true, RichTextMode = SPRichTextMode.FullHtml });

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsRecord.English.ShortContent, FieldsName.NewsRecord.VietNamese.ShortContent));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsRecord.English.FocusNews, FieldsName.NewsRecord.VietNamese.FocusNews));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsRecord.English.ShowInHomePage, FieldsName.NewsRecord.VietNamese.ShowInHomePage));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();

            var title = list.Fields.GetFieldByInternalName(FieldsName.Title);
            title.Title = FieldsName.NewsRecord.English.Heading;

            //Set readonly field
            var viewscount = list.Fields.GetFieldByInternalName(FieldsName.NewsRecord.English.ViewsCount);
            viewscount.ShowInNewForm = false;
            viewscount.ShowInEditForm = false;

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

        public static void CreateDownloadCatList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.DownloadCatList,
                Name = ListsName.English.DownloadCatList,
                OnQuickLaunch = true
            };

            helper.AddField(new MultipleLinesTextFieldCreator("Description", "Description") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });

            SPList list = helper.Apply();
            list.EnableAttachments = false;
            list.Update();
        }

        public static void CreateDownloadList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.DownloadList,
                Name = ListsName.English.DownloadList,
                OnQuickLaunch = true,
                ListTemplateType = SPListTemplateType.DocumentLibrary
            };
            helper.AddField(new LookupFieldCreator("CatID", "Chuyên mục") { LookupList = ListsName.English.DownloadCatList, LookupField = "Title" });
            StringCollection collect1 = new StringCollection();
            collect1.AddRange(new string[] { "Văn bản", "Tiện ích" });
            helper.AddField(new ChoiceFieldCreator("FileType", "Thể loại")
            {
                Choices = collect1
            });
            helper.AddField(new MultipleLinesTextFieldCreator("Description", "Description") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });
            SPList list = helper.Apply();
            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.Required = true;
            titleField.Update();
            list.Update();
        }

        public static void CreateQNConfigList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.QNConfigList,
                Name = ListsName.English.QNConfigList,
                OnQuickLaunch = true
            };

            helper.AddField(new MultipleLinesTextFieldCreator("Value", "Giá trị") { RichText = true, RichTextMode = SPRichTextMode.FullHtml, NumberOfLines = 6 });

            SPList list = helper.Apply();

            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.ShowInEditForm = false;
            titleField.ShowInViewForms = false;
            titleField.ShowInListSettings = false;
            titleField.Update();
            list.EnableAttachments = false;
            list.Update();
        }

        public static void CreateQNTVList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.QNTVList,
                Name = ListsName.English.QNTVList,
                OnQuickLaunch = true
            };

            helper.AddField(new MultipleLinesTextFieldCreator("Value", "Embed code") { RichText = false, RichTextMode = SPRichTextMode.Compatible, NumberOfLines = 6 });
            helper.AddField(new SingleLineTextFieldCreator("Logo", "Logo url"));
            helper.AddField(new NumberFieldCreator("Position", "Thứ tự"));
            SPList list = helper.Apply();

            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.Title = "Tên kênh";
            titleField.Update();
            list.EnableAttachments = false;
            list.Update();
        }

        public static void CreateStatisticsList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.StatisticsList,
                Name = ListsName.English.StatisticsList,
                OnQuickLaunch = true
            };

            helper.AddField(new SingleLineTextFieldCreator("Url", "Url"));
            helper.AddField(new SingleLineTextFieldCreator("Browser", "Browser"));
            helper.AddField(new SingleLineTextFieldCreator("IP", "IP"));
            helper.AddField(new DateTimeFieldCreator("DateHit", "Ngày tháng"));
            SPList list = helper.Apply();

            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.Title = "Người truy cập";
            titleField.Update();
            list.EnableAttachments = false;
            list.Update();
        }

        public static void CreateContactList(SPWeb web)
        {
            var helper = new ListHelper(web)
            {
                Title = ListsName.VietNamese.ContactList,
                Name = ListsName.English.ContactList,
                OnQuickLaunch = true
            };

            helper.AddField(new SingleLineTextFieldCreator("FullName", "FullName"));
            helper.AddField(new SingleLineTextFieldCreator("Browser", "Browser"));
            helper.AddField(new SingleLineTextFieldCreator("IP", "IP"));
            helper.AddField(new SingleLineTextFieldCreator("Address", "Address"));
            helper.AddField(new SingleLineTextFieldCreator("Mobile", "Mobile"));
            helper.AddField(new MultipleLinesTextFieldCreator("Content", "Content") { RichText = false, RichTextMode = SPRichTextMode.Compatible, NumberOfLines = 6 });
            SPList list = helper.Apply();

            var titleField = list.Fields.GetFieldByInternalName("Title");
            titleField.Title = "Tiêu đề";
            titleField.Required = false;
            titleField.Update();
            list.EnableAttachments = false;
            list.Update();
        }
    }
}
