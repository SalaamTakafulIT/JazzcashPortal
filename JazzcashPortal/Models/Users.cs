using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JazzcashPortal.Models
{
    [Keyless]
    [Table("SY_USERS")]
    public class Users
    {
        public string? UMS_TYPE { get; set; }
        public string? LINE_MANAGER { get; set; }
        public string? IS_HEAD { get; set; }
        public string? USER_CD { get; set; }
        public string? USER_NAME { get; set; }
        public string? USER_PASS { get; set; }
        public string? ACTIVE { get; set; }
        public string? LOCK_USER { get; set; }
        public DateTime LAST_PASS_DATE { get; set; }
        public string? REMARKS { get; set; }
        public string? COST_CODE { get; set; }
        public string? USER_CATEGORY { get; set; }
        public string? DESIGNATION { get; set; }
        public string? PHONE_NO { get; set; }
        public string? MAIL_ADDRESS { get; set; }
        public string? RES_TEL_NO { get; set; }
        public string? NIC_NO { get; set; }
        public string? N_TAX_NO { get; set; }
        public string? FAX_NO { get; set; }
        public string? MOBILE_NO { get; set; }
        public string? EMAIL { get; set; }
        public string? BRANCH_CODE { get; set; }
        public string? USER_TYPE { get; set; }
        public string? CLASS_CODE { get; set; }
        public string? ENT_BY { get; set; }
        public DateTime ENT_DATE { get; set; }
        public string? ENT_BY_IP { get; set; }
        public string? LAST_UPDATE_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public string? LAST_UPDATE_BY_IP { get; set; }
        public int REIMBURSEMENT_SER_NO { get; set; }
        public string? BANK_ACC_NO { get; set; }
        public string? IS_EMAIL { get; set; }
        public string? PARTTAKER_CODE { get; set; }
        public string? USER_LOGO { get; set; }
        public string? PL_EMAIL { get; set; }
        public string? TRAVEL_CLIENT_CODE { get; set; }
        public string? TRAVEL_AGENT_CODE { get; set; }
        public string? PARENT_USER { get; set; }
        public string? IMC_CODE { get; set; }
        public string? REGION_CODE { get; set; }
        public string? PS_RATE { get; set; }
        public string? PS_RATE_CAR { get; set; }
        public string? PS_RATE_BIKE { get; set; }
        public string? IS_LEAD_AGENT { get; set; }
        public string? IS_LEAD_TRANSFERABLE { get; set; }
        public string? REGION_NAME { get; set; }

    }
}
