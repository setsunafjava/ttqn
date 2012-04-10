using System.Globalization;

namespace CQ.SharePoint.QN.Common
{
    public class Constants
    {
        public const string Title = "Title";
        public const string Modified = "Modified";
        public const string Created = "Created";
        public const string FieldTitleLinkToItem = "LinkTitle";
        public const string EditColumn = "Edit";
        public const string FieldLinkToFileName = "LinkFilename";
        public const string LinkTitleNoMenu = "LinkTitleNoMenu";
        public const string CreatedBy = "Author";
        public const string AssignedTo = "AssignedTo";
        public const string LinkToItem = "伝票の参照はここをクリック";
        public const string Status = "Status";
        public const string Completed = "Completed";
        public const string Description = "Description";
        public const string ApproverComments = "ApproverComments";
        public const string Outcome = "Outcome";
        public const string WorkflowOutcome = "WorkflowOutcome";
        public const string DateOccurred = "DateOccurred";
        public const string UserId = "UserID";
        public const string NumberFormat = "{0:N0}";
        public const string DateTimeFormat = "{0:yyyy/MM/dd}";
        public const string DateTimeFormatFull = "{0:yyyy/MM/dd HH:mm:ss}";
        public const string MaxLength16 = "16文字";
        public const string HistoryTable = "HistoryTable";

        public const string ViewNameAllItems = "All Items";
        public const string ExpenseClaimView1 = "処理待伝票";
        public const string ExpenseClaimView2 = "処理済伝票番号順";
        public const string ExpenseClaimView3 = "処理済処理時間順";
        public const string ExpenseClaimView4 = "処理済申請日順";
        public const string ExpenseClaimView5 = "個人履歴";
        public const string ExpenseClaimView6 = "海外出張設定";
        public const int ViewLimit = 100;

        public class WorkflowStatusEnglish
        {
            public const string Submitted = "Submitted";
            public const string ReSubmitted = "Re-submitted";
            public const string Approved = "Approved";
            public const string Rejected = "Rejected";
            public const string Approve = "Approve";
            public const string Reject = "Reject";
        }
        public class WorkflowStatusJapan
        {
            public const string Submitted = "起票済";
            public const string ReSubmitted = "再起票済";
            public const string Approved = "承認済";
            public const string Rejected = "却下";
            public const string Approve = "承認";
            public const string Reject = "却下";
        }

        public class ExpenseClaimWorkflow
        {
            public const string NintexWorkflowTask = "Nintex Workflow Task";
            public const string NintexWorkflowTaskJp = "Nintex Workflow タスク";//Nintex Workflow タスク
            public const string ExpenseClaimWorkflowTask = "Expense Claim Workflow Task";
            public const string NintexWorkflow = "Nintex Workflow";
            public const string WorkflowTasks = "Workflow Tasks";
            public const string WorkflowTasksJp = "ワークフロー タスク";
            public const string IpAddress = "IPAddress";
            public const string IpAddressJapan = "IP Address";
            public const string WorkflowStatus = "進行中で";
            public const string WfTitle = "ワークフロータスク";
            public const string WorkflowInstanceID = "WorkflowInstanceID";
            public const string WorkflowItemId = "WorkflowItemId";
            public const string WorkflowName = "WorkflowName";
            public const string ProcessPerson = "ProcessPersion";
            public const string ProcessPersonJapan = "処理者氏名";
            public const string FirstAssignPerson = "FirstAssignPerson";
            public const string FirstAssignPersonJapan = "First Assign Person";
        }

        public class ErrorMessage
        {
            public const string ResourceFileName = "CQ.SharePoint.QN";
            public const string Msg1 = "MSG1";
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
            public const string AccountingDepartmentGroup = "CQQN経理部";

            public const string FullControlGroup = "CQQN所有者";

            public const string ManagementGroup = "CQQN管理者";

            public const string DeleteGroup = "CQQN削除者";

            public const string EditGroup = "CQQN編集者";

            public const string SubmitterGroup = "CQQN投稿者";

            public const string ViewGroup = "CQQN閲覧者";

            public const string SystemAdmin = "CQQNシステム管理者";

            public const string Approver = "CQQN承認者";
        }
        #endregion

        #region GroupDescription
        public class GroupDescription
        {
            public const string AccountingDepartmentGroup = "CQQN経理部";

            public const string FullControlGroup = "CQQN所有者 - 完全な制御が可能です。";

            public const string ManagementGroup = "CQQN管理者 - 閲覧表示, 新規作成、, 削除, SharePointＧroup内メンバ操作, 文書単位のアクセス権閲覧";

            public const string DeleteGroup = "CQQN削除者 - 閲覧表示, 新規作成、編集, 削除";

            public const string EditGroup = "CQQN編集者 - 閲覧表示, 新規作成、編集";

            public const string SubmitterGroup = "CQQN投稿者 - 閲覧表示, 新規作成";

            public const string ViewGroup = "CQQN閲覧者 - 閲覧表示（全文書）";

            public const string SystemAdmin = "CQQNシステム管理者";

            public const string Approver = "CQQN承認者";

            public const string Submitter = "CQQN閲覧者";

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
