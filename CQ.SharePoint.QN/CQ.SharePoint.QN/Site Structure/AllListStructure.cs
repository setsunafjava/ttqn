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

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsRecord.English.Heading, FieldsName.NewsRecord.VietNamese.Heading));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsRecord.English.Content, FieldsName.NewsRecord.VietNamese.Content));

            helper.AddField(new NumberFieldCreator(FieldsName.NewsRecord.English.CategoryId, FieldsName.NewsRecord.VietNamese.CategoryId));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();

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

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.UserName, FieldsName.SupportUser.English.UserName));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.Phone, FieldsName.SupportUser.VietNamese.Phone));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.TelePhone, FieldsName.SupportUser.VietNamese.TelePhone));

            helper.AddField(new NumberFieldCreator(FieldsName.SupportUser.English.NickType, FieldsName.SupportUser.VietNamese.NickType));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.NickName, FieldsName.SupportUser.VietNamese.NickName));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.Email, FieldsName.SupportUser.VietNamese.Email));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();

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

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateCategory.English.Name, FieldsName.CorporateCategory.VietNamese.Name));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateCategory.English.Phone, FieldsName.CorporateCategory.VietNamese.Phone));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateCategory.English.TelePhone, FieldsName.CorporateCategory.VietNamese.TelePhone));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.CorporateCategory.English.Information, FieldsName.CorporateCategory.VietNamese.Information));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateCategory.English.Email, FieldsName.CorporateCategory.VietNamese.Email));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();

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

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateRecord.English.Name, FieldsName.CorporateRecord.VietNamese.Name));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateRecord.English.Phone, FieldsName.CorporateRecord.VietNamese.Phone));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.CorporateRecord.English.TelePhone, FieldsName.CorporateRecord.VietNamese.TelePhone));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.CorporateRecord.English.Information, FieldsName.CorporateRecord.VietNamese.Information));

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.SupportUser.English.Email, FieldsName.SupportUser.VietNamese.Email));

            helper.AddField(new BooleanFieldCreator(FieldsName.NewsCategory.English.Status, FieldsName.NewsCategory.VietNamese.Status));

            SPList list = helper.Apply();

            list.EnableAttachments = true;

            list.Update();
        }
    }
}
