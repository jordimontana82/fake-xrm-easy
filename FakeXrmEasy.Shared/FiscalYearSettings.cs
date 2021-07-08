using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace FakeXrmEasy
{
    [EntityLogicalName("organization")]
    public class FiscalYearSettings
    {
        [AttributeLogicalName("fiscalcalendarstart")]
        public DateTime StartDate { get; set; }

        [AttributeLogicalName("fiscalperiodtype")]
        public Template FiscalPeriodTemplate { get; set; }

        public enum Template
        {
            Annually = 2000,
            SemiAnnually = 2001,
            Quarterly = 2002,
            Monthly = 2003,
            FourWeek = 2004
        }

        [AttributeLogicalName("fiscalyeardisplaycode")]
        public FiscalYearDisplayCode FiscalYearDisplay { get; set; }

        public enum FiscalYearDisplayCode
        {
            StartDate = 1,
            EndDate = 2
        }

        public static void GetFiscalYearStartAndEndDate(FiscalYearSettings fiscalYearSettings, int year, out DateTime startDate, out DateTime endDate)
        {
            var basicStartDate = fiscalYearSettings?.StartDate ?? new DateTime(DateTime.Today.Year, 4, 1);

            if (fiscalYearSettings?.FiscalYearDisplay == FiscalYearDisplayCode.EndDate)
            {
                DateTime basicEndDate = basicStartDate.AddYears(1).AddDays(-1);
                endDate = new DateTime(year, basicEndDate.Month, basicEndDate.Day);
                startDate = endDate.AddYears(-1).AddDays(1);
            }
            else
            {
                startDate = new DateTime(year, basicStartDate.Month, basicStartDate.Day);
                endDate = startDate.AddYears(1).AddDays(-1);
            }
        }
    }
}
