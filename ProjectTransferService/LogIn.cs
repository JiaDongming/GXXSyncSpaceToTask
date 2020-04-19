//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectTransferService
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogIn
    {
        public int PersonID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string HomePhone { get; set; }
        public string Login1 { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<int> AccountStatus { get; set; }
        public Nullable<System.DateTime> DateAccountCreated { get; set; }
        public Nullable<System.DateTime> DateAccountClosed { get; set; }
        public bool IsManager { get; set; }
        public bool IsActivePerson { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsInSampleProject { get; set; }
        public Nullable<int> IsSWiseUser { get; set; }
        public Nullable<int> IsSWiseLightUser { get; set; }
        public string Initial { get; set; }
        public string Pager { get; set; }
        public Nullable<int> PagerType { get; set; }
        public string PagerServiceProvider { get; set; }
        public string PagerEmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public string MobileServiceProvider { get; set; }
        public string MobileEmailAddress { get; set; }
        public Nullable<int> IfPalmByPassLogin { get; set; }
        public string PalmID { get; set; }
        public Nullable<int> NumPerPalmPage { get; set; }
        public string NTLoginName { get; set; }
        public Nullable<int> IsAssetAdmin { get; set; }
        public Nullable<int> IsFormAdmin { get; set; }
        public Nullable<int> Fax { get; set; }
        public Nullable<int> TimeZoneID { get; set; }
        public string EmailSignature { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> BaseProjectID { get; set; }
        public Nullable<int> CompanyDivisionID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public Nullable<int> ProductAccessID { get; set; }
        public Nullable<int> UseOwnAddress { get; set; }
        public string FaxNumber { get; set; }
        public Nullable<int> LicenseType { get; set; }
        public Nullable<int> IsHDeskUser { get; set; }
        public Nullable<int> IsHDeskLightUser { get; set; }
        public string PagerUserID { get; set; }
        public string MobileUserID { get; set; }
        public Nullable<int> AccessDPlusAdmin { get; set; }
        public Nullable<int> AccessDPlusAnalysis { get; set; }
        public string DomesticCountries { get; set; }
        public Nullable<int> IfPersonalAddress4Quote { get; set; }
        public Nullable<int> IfPersonalFax4Phone { get; set; }
        public Nullable<int> IfPersonalPhone4Fax { get; set; }
        public Nullable<int> NeedToEditOwnWorkHours { get; set; }
        public Nullable<int> WorkScheduleID { get; set; }
        public Nullable<int> IsActiveDevTrackUser { get; set; }
        public Nullable<int> IsActiveDevTestUser { get; set; }
        public Nullable<int> UpdateNo { get; set; }
        public Nullable<int> SystemAdminAccountID { get; set; }
        public Nullable<int> IsActiveDevPlanUser { get; set; }
        public Nullable<int> ScopeID { get; set; }
        public Nullable<int> IsActiveTimeSheetUser { get; set; }
        public Nullable<int> TimeSheetProject { get; set; }
        public string ShortCutScopeID { get; set; }
        public Nullable<int> IsActiveDevTimeUser { get; set; }
        public Nullable<int> SysDivID { get; set; }
        public Nullable<int> IsSysDivAdministrator { get; set; }
        public Nullable<int> SWLicenseType { get; set; }
        public Nullable<int> HDLicenseType { get; set; }
        public Nullable<int> HSCompanyID { get; set; }
        public Nullable<int> IsHSCompanyAdministrator { get; set; }
        public Nullable<int> IsActiveKnowledgeWiseUser { get; set; }
        public Nullable<int> MaxUnit { get; set; }
        public Nullable<int> IsActiveDevSpecUser { get; set; }
        public Nullable<int> IsActiveAddDevTestUser { get; set; }
        public Nullable<int> AddDevTestLicType { get; set; }
        public Nullable<int> IsDevTestMappingUser { get; set; }
        public Nullable<int> CanPerformOutlookSync { get; set; }
        public string Memo { get; set; }
        public Nullable<int> HolidayScheduleID { get; set; }
        public Nullable<int> PortfolioProjectAdmin { get; set; }
        public Nullable<int> PasswordLocked { get; set; }
        public Nullable<System.DateTime> PasswordLockedTime { get; set; }
        public Nullable<System.DateTime> PasswordUnlockTime { get; set; }
        public Nullable<int> PasswordResetOption { get; set; }
        public Nullable<int> UserAccountType { get; set; }
        public Nullable<System.DateTime> UserAccountExpirationDate { get; set; }
        public Nullable<int> LoginFailedTimes { get; set; }
        public Nullable<System.DateTime> PasswordResetTime { get; set; }
        public Nullable<System.DateTime> PasswordStartTime { get; set; }
        public Nullable<int> CanUseDTClient { get; set; }
        public Nullable<int> CanUseMyDevSuite { get; set; }
        public Nullable<int> DevSuiteSysAdminAccountID { get; set; }
        public string APIAccessToken { get; set; }
        public Nullable<int> IsLocked { get; set; }
        public Nullable<int> NumOfTries { get; set; }
        public Nullable<System.DateTime> DateLastLoginTry { get; set; }
        public Nullable<int> IsActivePPMUser { get; set; }
        public Nullable<int> JobRoleID { get; set; }
        public string LastLoggedNotifyID { get; set; }
        public string LastLoggedDeviceID { get; set; }
        public Nullable<int> LastLoggedDeviceType { get; set; }
        public Nullable<int> IfAppLogged { get; set; }
        public string OldPassword1 { get; set; }
        public string OldPassword2 { get; set; }
        public string OldPassword3 { get; set; }
        public string OldPassword4 { get; set; }
        public string OldPassword5 { get; set; }
        public Nullable<int> IfPasswordIsTemp { get; set; }
        public Nullable<System.DateTime> PasswordChangeTime { get; set; }
        public string WeChatID { get; set; }
        public Nullable<int> WorkStatusID { get; set; }
        public string AutoResponderMsg { get; set; }
        public Nullable<System.DateTime> AutoResponderStartDate { get; set; }
        public Nullable<System.DateTime> AutoResponderEndDate { get; set; }
        public Nullable<int> IsPeerTimeUser { get; set; }
    }
}
