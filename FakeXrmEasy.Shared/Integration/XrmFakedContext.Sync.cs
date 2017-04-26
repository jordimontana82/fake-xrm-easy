using FakeItEasy;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Messages;
using System.Dynamic;
using System.Linq.Expressions;
using FakeXrmEasy.Extensions;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext : IXrmFakedContext
    {
        protected bool UsesIntegration { get; set; }
    

        protected internal IOrganizationService _integrationService;

        /// <summary>
        /// Syncs a list of entities In Memory with a real Organization database.
        /// When the context is running in Integration mode, it will call Sync to synchronize the entities passed to the Initialize
        /// method, so that tests will have the required records in the destination environment before test execution
        /// </summary>
        protected void Sync(IOrganizationService realService)
        {
            //Note to self: this could be improved  via ExecuteMultiple requests... doing this for now

            //Iterate through every entity type and guid, and create the required entities
            foreach(var sEnttiyName in Data.Keys)
            {
                foreach(var guid in Data[sEnttiyName].Keys)
                {
                    //1) Check if record exists
                    var entityExists = realService.Retrieve(sEnttiyName, guid, new ColumnSet(true));

                    //2) Create record using a real organization service
                    realService.Create(Data[sEnttiyName][guid]);
                }
            }
        }

        /// <summary>
        /// Kind of TearDown method. All entities created / updated / deleted during initialization and test run will be deleted after the test execution
        /// </summary>
        /// <param name="realService"></param>
        public void CleanUp(IOrganizationService realService)
        {
            foreach (var sEnttiyName in Data.Keys)
            {
                foreach (var guid in Data[sEnttiyName].Keys)
                {
                    realService.Delete(sEnttiyName, guid);
                }
            }
        }

        /// <summary>
        /// Clone Data dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        protected static Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>(Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
                                                                    original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }
    }
}