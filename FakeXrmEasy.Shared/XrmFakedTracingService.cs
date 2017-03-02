using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;

namespace FakeXrmEasy
{
    public class XrmFakedTracingService : ITracingService
    {
        protected string _trace { get; set; }

        public XrmFakedTracingService()
        {
            _trace = "";
        }

        public void Trace(string format, params object[] args)
        {
            _trace += string.Format(format, args) + System.Environment.NewLine;
        }

        public string DumpTrace()
        {
            return _trace;
        }
    }
}
