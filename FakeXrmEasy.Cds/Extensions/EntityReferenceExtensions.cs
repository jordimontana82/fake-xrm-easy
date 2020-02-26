using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeXrmEasy.Extensions
{
    public static class EntityReferenceExtensions
    {
        public static bool HasKeyAttributes(this EntityReference er)
        {
            if(er == null)
            {
                return false;
            }

#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015
            return er.KeyAttributes.Count > 0;
#else
            return false;
#endif
        }
    }
}
