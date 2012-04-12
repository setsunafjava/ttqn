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
            CreateCorporateRecordList(web);
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

            //helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsCategory.English.Heading, FieldsName.NewsCategory.VietNamese.Heading));

            helper.AddField(new NumberFieldCreator(FieldsName.NewsCategory.English.ParentId, FieldsName.NewsCategory.VietNamese.ParentId));

            helper.AddField(new NumberFieldCreator(FieldsName.NewsCategory.English.ChildNumber, FieldsName.NewsCategory.VietNamese.ChildNumber));

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

            //helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsRecord.English.Heading, FieldsName.NewsRecord.VietNamese.Heading));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsRecord.English.Content, FieldsName.NewsRecord.VietNamese.Content));

            helper.AddField(new NumberFieldCreator(FieldsName.NewsRecord.English.ViewsCount, FieldsName.NewsRecord.VietNamese.ViewsCount));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsRecord.English.TieuBieu, FieldsName.NewsRecord.VietNamese.TieuBieu));

            helper.AddField(new NumberFieldCreator(FieldsName.NewsRecord.English.CategoryId, FieldsName.NewsRecord.VietNamese.CategoryId));

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
    }
}
