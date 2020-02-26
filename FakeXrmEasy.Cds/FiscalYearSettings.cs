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
    }
}
