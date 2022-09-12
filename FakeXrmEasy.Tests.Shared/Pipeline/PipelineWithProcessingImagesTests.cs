using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FakeXrmEasy.Tests.PluginsForTesting;
using Crm;
using FakeXrmEasy.Models;
using System.Linq;
using Microsoft.Xrm.Sdk.Messages;

namespace FakeXrmEasy.Tests.Pipeline
{
    public class PipelineWithProcessingImagesTests
    {
        private Account GetAccountForTests(Contact primaryContact)
        {
            var defaultAccount = new Account()
            {
                AccountNumber = "1234567890",
                AccountCategoryCode = new OptionSetValue(1),
                NumberOfEmployees = 5,
                Revenue = new Money(20000),
                PrimaryContactId = primaryContact.ToEntityReference(),
                Telephone1 = "+123456"
            };

            return defaultAccount;
        }

        #region Registered plugins are passed to plugin
        [Fact]
        public void When_preImage_registered_expect_only_preImage()
        {
            // arrange
            var context = new XrmFakedContext() { UsePipelineSimulation = true };
            var service = context.GetOrganizationService();

            var primaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "Old"
            };

            var newPrimaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "New"
            };

            Guid accountId = Guid.NewGuid();
            Account previousValues = GetAccountForTests(primaryContact);
            previousValues.Id = accountId;

            context.Initialize(new Entity[] { primaryContact, newPrimaryContact, previousValues });

            string registeredPreImageName = "PreImage";
            PluginImageDefinition preImageDefinition = new PluginImageDefinition(registeredPreImageName, ProcessingStepImageType.PreImage);

            context.RegisterPluginStep<EntityImagesInPluginPipeline>("Update", registeredImages: new PluginImageDefinition[] { preImageDefinition });

            // act
            var target = new Account()
            {
                AccountId = accountId,
                AccountNumber = "1234",
                AccountCategoryCode = new OptionSetValue(2),
                NumberOfEmployees = 10,
                Revenue = new Money(10000),
                PrimaryContactId = newPrimaryContact.ToEntityReference()
            };

            service.Update(target);

            // assert
            Dictionary<Guid, Entity> allAccounts = null;
            Assert.True(context.Data.TryGetValue("account", out allAccounts));

            Account savedAccount = allAccounts[accountId] as Account;
            Assert.NotNull(savedAccount);

            IEnumerable<Entity> allPreImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("preimagename"));
            IEnumerable<Entity> allPostImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("postimagename"));

            Assert.Equal(1, allPreImages.Count());
            Assert.Equal(0, allPostImages.Count());

            Entity preImage = allPreImages.First();

            Assert.Equal(registeredPreImageName, preImage.GetAttributeValue<string>("preimagename"));
        }

        [Fact]
        public void When_multiple_preImages_registered_expect_all_of_them()
        {
            // arrange
            var context = new XrmFakedContext() { UsePipelineSimulation = true };
            var service = context.GetOrganizationService();

            var primaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "Old"
            };

            var newPrimaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "New"
            };

            Guid accountId = Guid.NewGuid();
            Account previousValues = GetAccountForTests(primaryContact);
            previousValues.Id = accountId;

            context.Initialize(new Entity[] { primaryContact, newPrimaryContact, previousValues });

            string registeredPreImageName1 = "PreImageNumberOne";
            PluginImageDefinition preImageDefinition1 = new PluginImageDefinition(registeredPreImageName1, ProcessingStepImageType.PreImage);

            string registeredPreImageName2 = "PreImageNumberTwo";
            PluginImageDefinition preImageDefinition2 = new PluginImageDefinition(registeredPreImageName2, ProcessingStepImageType.PreImage);

            context.RegisterPluginStep<EntityImagesInPluginPipeline>("Update", registeredImages: new PluginImageDefinition[] { preImageDefinition1, preImageDefinition2 });

            // act
            var target = new Account()
            {
                AccountId = accountId,
                AccountNumber = "1234",
                AccountCategoryCode = new OptionSetValue(2),
                NumberOfEmployees = 10,
                Revenue = new Money(10000),
                PrimaryContactId = newPrimaryContact.ToEntityReference()
            };

            service.Update(target);

            // assert
            Dictionary<Guid, Entity> allAccounts = null;
            Assert.True(context.Data.TryGetValue("account", out allAccounts));

            Account savedAccount = allAccounts[accountId] as Account;
            Assert.NotNull(savedAccount);

            IEnumerable<Entity> allPreImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("preimagename"));

            Assert.Equal(2, allPreImages.Count());

            IEnumerable<Entity> preImageOnes = allPreImages.Where(x => x.GetAttributeValue<string>("preimagename") == registeredPreImageName1);
            IEnumerable<Entity> preImageTwos = allPreImages.Where(x => x.GetAttributeValue<string>("preimagename") == registeredPreImageName2);

            Assert.Equal(1, preImageOnes.Count());
            Assert.Equal(1, preImageTwos.Count());
        }

        [Fact]
        public void When_postImage_registered_expect_only_postImage()
        {
            // arrange
            var context = new XrmFakedContext() { UsePipelineSimulation = true };
            var service = context.GetOrganizationService();

            var primaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "Old"
            };

            var newPrimaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "New"
            };

            Guid accountId = Guid.NewGuid();
            Account previousValues = GetAccountForTests(primaryContact);
            previousValues.Id = accountId;

            context.Initialize(new Entity[] { primaryContact, newPrimaryContact, previousValues });

            string registeredPostImageName = "PostImage";
            PluginImageDefinition postImageDefinition = new PluginImageDefinition(registeredPostImageName, ProcessingStepImageType.PostImage);

            context.RegisterPluginStep<EntityImagesInPluginPipeline>("Update", registeredImages: new PluginImageDefinition[] { postImageDefinition });

            // act
            var target = new Account()
            {
                AccountId = accountId,
                AccountNumber = "1234",
                AccountCategoryCode = new OptionSetValue(2),
                NumberOfEmployees = 10,
                Revenue = new Money(10000),
                PrimaryContactId = newPrimaryContact.ToEntityReference()
            };

            service.Update(target);

            // assert
            Dictionary<Guid, Entity> allAccounts = null;
            Assert.True(context.Data.TryGetValue("account", out allAccounts));

            IEnumerable<Entity> allPreImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("preimagename"));
            IEnumerable<Entity> allPostImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("postimagename"));

            Account savedAccount = allAccounts[accountId] as Account;
            Assert.NotNull(savedAccount);

            Assert.Equal(0, allPreImages.Count());
            Assert.Equal(1, allPostImages.Count());

            Entity postImage = allPostImages.First();

            Assert.Equal(registeredPostImageName, postImage.GetAttributeValue<string>("postimagename"));
        }

        [Fact]
        public void When_preImage_and_postImage_registered_expect_both()
        {
            // arrange
            var context = new XrmFakedContext() { UsePipelineSimulation = true };
            var service = context.GetOrganizationService();

            var primaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "Old"
            };

            var newPrimaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "New"
            };

            Guid accountId = Guid.NewGuid();
            Account previousValues = GetAccountForTests(primaryContact);
            previousValues.Id = accountId;

            context.Initialize(new Entity[] { primaryContact, newPrimaryContact, previousValues });
            string registeredPreImageName = "PreImage";
            PluginImageDefinition preImageDefinition = new PluginImageDefinition(registeredPreImageName, ProcessingStepImageType.PreImage);

            string registeredPostImageName = "PostImage";
            PluginImageDefinition postImageDefinition = new PluginImageDefinition(registeredPostImageName, ProcessingStepImageType.PostImage);

            context.RegisterPluginStep<EntityImagesInPluginPipeline>("Update", registeredImages: new PluginImageDefinition[] { preImageDefinition, postImageDefinition });

            // act
            var target = new Account()
            {
                AccountId = accountId,
                AccountNumber = "1234",
                AccountCategoryCode = new OptionSetValue(2),
                NumberOfEmployees = 10,
                Revenue = new Money(10000),
                PrimaryContactId = newPrimaryContact.ToEntityReference()
            };

            service.Update(target);

            // assert
            Dictionary<Guid, Entity> allAccounts = null;
            Assert.True(context.Data.TryGetValue("account", out allAccounts));

            IEnumerable<Entity> allPreImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("preimagename"));
            IEnumerable<Entity> allPostImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("postimagename"));

            Account savedAccount = allAccounts[accountId] as Account;
            Assert.NotNull(savedAccount);

            Assert.Equal(1, allPreImages.Count());
            Assert.Equal(1, allPostImages.Count());

            Entity preImage = allPreImages.First();
            Entity postImage = allPostImages.First();

            Assert.Equal(registeredPreImageName, preImage.GetAttributeValue<string>("preimagename"));
            Assert.Equal(registeredPostImageName, postImage.GetAttributeValue<string>("postimagename"));
        }

        [Fact]
        public void When_bothImage_registered_expect_preImage_and_postImage()
        {
            // arrange
            var context = new XrmFakedContext() { UsePipelineSimulation = true };
            var service = context.GetOrganizationService();

            var primaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "Old"
            };

            var newPrimaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "New"
            };

            Guid accountId = Guid.NewGuid();
            Account previousValues = GetAccountForTests(primaryContact);
            previousValues.Id = accountId;

            context.Initialize(new Entity[] { primaryContact, newPrimaryContact, previousValues });
            string registeredPreImageName = "Both";
            PluginImageDefinition bothImageDefinition = new PluginImageDefinition(registeredPreImageName, ProcessingStepImageType.Both);

            context.RegisterPluginStep<EntityImagesInPluginPipeline>("Update", registeredImages: new PluginImageDefinition[] { bothImageDefinition });

            // act
            var target = new Account()
            {
                AccountId = accountId,
                AccountNumber = "1234",
                AccountCategoryCode = new OptionSetValue(2),
                NumberOfEmployees = 10,
                Revenue = new Money(10000),
                PrimaryContactId = newPrimaryContact.ToEntityReference()
            };

            service.Update(target);

            // assert
            Dictionary<Guid, Entity> allAccounts = null;
            Assert.True(context.Data.TryGetValue("account", out allAccounts));

            IEnumerable<Entity> allPreImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("preimagename"));
            IEnumerable<Entity> allPostImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("postimagename"));

            Account savedAccount = allAccounts[accountId] as Account;
            Assert.NotNull(savedAccount);

            Assert.Equal(1, allPreImages.Count());
            Assert.Equal(1, allPostImages.Count());

            Entity preImage = allPreImages.First();
            Entity postImage = allPostImages.First();

            Assert.Equal(registeredPreImageName, preImage.GetAttributeValue<string>("preimagename"));
            Assert.Equal(registeredPreImageName, postImage.GetAttributeValue<string>("postimagename"));
        }
        #endregion

        [Fact]
        public void When_both_images_with_all_attributes_registered_expect_all_corrrect_values()
        {
            // arrange
            var context = new XrmFakedContext() { UsePipelineSimulation = true };
            var service = context.GetOrganizationService();

            var primaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "Old"
            };

            var newPrimaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "New"
            };

            Guid accountId = Guid.NewGuid();
            Account previousValues = GetAccountForTests(primaryContact);
            previousValues.Id = accountId;

            context.Initialize(new Entity[] { primaryContact, newPrimaryContact, previousValues });
            string registeredPreImageName = "PreImage";
            PluginImageDefinition preImageDefinition = new PluginImageDefinition(registeredPreImageName, ProcessingStepImageType.PreImage);

            string registeredPostImageName = "PostImage";
            PluginImageDefinition postImageDefinition = new PluginImageDefinition(registeredPostImageName, ProcessingStepImageType.PostImage);

            context.RegisterPluginStep<EntityImagesInPluginPipeline>("Update", registeredImages: new PluginImageDefinition[] { preImageDefinition, postImageDefinition });

            // act
            var target = new Account()
            {
                AccountId = accountId,
                AccountNumber = "1234",
                AccountCategoryCode = new OptionSetValue(2),
                NumberOfEmployees = 10,
                Revenue = new Money(10000),
                PrimaryContactId = newPrimaryContact.ToEntityReference()
            };

            service.Update(target);

            // assert
            Dictionary<Guid, Entity> allAccounts = null;
            Assert.True(context.Data.TryGetValue("account", out allAccounts));

            IEnumerable<Entity> allPreImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("preimagename"));
            IEnumerable<Entity> allPostImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("postimagename"));

            Account savedAccount = allAccounts[accountId] as Account;
            Assert.NotNull(savedAccount);

            Assert.Equal(1, allPreImages.Count());

            Entity preImage = allPreImages.First();
            Assert.True(preImage.GetAttributeValue<bool?>("containedid") == true);
            Assert.Equal(registeredPreImageName, preImage.GetAttributeValue<string>("preimagename"));
            Assert.Equal(previousValues.AccountNumber, preImage.GetAttributeValue<string>("accountnumber"));
            Assert.Equal(previousValues.NumberOfEmployees, preImage.GetAttributeValue<int?>("numberofemployees"));
            Assert.Equal(previousValues.AccountCategoryCode, preImage.GetAttributeValue<OptionSetValue>("accountcategorycode"));
            Assert.Equal(previousValues.Revenue, preImage.GetAttributeValue<Money>("revenue"));
            Assert.Equal(previousValues.PrimaryContactId, preImage.GetAttributeValue<EntityReference>("primarycontactid"));
            Assert.Equal(previousValues.Telephone1, preImage.GetAttributeValue<string>("telephone1"));

            Assert.Equal(1, allPostImages.Count());

            Entity postImage = allPostImages.First();
            Assert.True(postImage.GetAttributeValue<bool?>("containedid") == true);
            Assert.Equal(registeredPostImageName, postImage.GetAttributeValue<string>("postimagename"));
            Assert.Equal(target.AccountNumber, postImage.GetAttributeValue<string>("accountnumber"));
            Assert.Equal(target.NumberOfEmployees, postImage.GetAttributeValue<int?>("numberofemployees"));
            Assert.Equal(target.AccountCategoryCode, postImage.GetAttributeValue<OptionSetValue>("accountcategorycode"));
            Assert.Equal(target.Revenue, postImage.GetAttributeValue<Money>("revenue"));
            Assert.Equal(target.PrimaryContactId, postImage.GetAttributeValue<EntityReference>("primarycontactid"));
            Assert.Equal(previousValues.Telephone1, postImage.GetAttributeValue<string>("telephone1"));
        }

        [Fact]
        public void When_both_images_with_specific_attributes_registered_expect_specific_correct_values()
        {
            // arrange
            var context = new XrmFakedContext() { UsePipelineSimulation = true };
            var service = context.GetOrganizationService();

            var primaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "Old"
            };

            var newPrimaryContact = new Contact()
            {
                Id = Guid.NewGuid(),
                LastName = "New"
            };

            Guid accountId = Guid.NewGuid();
            Account previousValues = GetAccountForTests(primaryContact);
            previousValues.Id = accountId;

            context.Initialize(new Entity[] { primaryContact, newPrimaryContact, previousValues });
            string registeredPreImageName = "PreImage";
            PluginImageDefinition preImageDefinition = new PluginImageDefinition(registeredPreImageName, ProcessingStepImageType.PreImage, "accountnumber", "telephone1", "primarycontactid");

            string registeredPostImageName = "PostImage";
            PluginImageDefinition postImageDefinition = new PluginImageDefinition(registeredPostImageName, ProcessingStepImageType.PostImage, "accountnumber", "numberofemployees", "revenue");

            context.RegisterPluginStep<EntityImagesInPluginPipeline>("Update", registeredImages: new PluginImageDefinition[] { preImageDefinition, postImageDefinition });

            // act
            var target = new Account()
            {
                AccountId = accountId,
                AccountNumber = "1234",
                AccountCategoryCode = new OptionSetValue(2),
                NumberOfEmployees = 10,
                Revenue = new Money(10000),
                PrimaryContactId = newPrimaryContact.ToEntityReference()
            };

            service.Update(target);

            // assert
            Dictionary<Guid, Entity> allAccounts = null;
            Assert.True(context.Data.TryGetValue("account", out allAccounts));

            IEnumerable<Entity> allPreImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("preimagename"));
            IEnumerable<Entity> allPostImages = allAccounts.Select(x => x.Value).Where(x => x.Contains("postimagename"));

            Account savedAccount = allAccounts[accountId] as Account;
            Assert.NotNull(savedAccount);

            Assert.Equal(1, allPreImages.Count());

            Entity preImage = allPreImages.First();
            Assert.False(preImage.GetAttributeValue<bool?>("containedid") == true);
            Assert.Equal(registeredPreImageName, preImage.GetAttributeValue<string>("preimagename"));
            Assert.True(preImage.Contains("accountnumber"));
            Assert.False(preImage.Contains("numberofemployees"));
            Assert.False(preImage.Contains("accountcategorycode"));
            Assert.False(preImage.Contains("revenue"));
            Assert.True(preImage.Contains("primarycontactid"));
            Assert.True(preImage.Contains("telephone1"));

            Assert.Equal(1, allPostImages.Count());

            Entity postImage = allPostImages.First();
            Assert.False(postImage.GetAttributeValue<bool?>("containedid") == true);
            Assert.Equal(registeredPostImageName, postImage.GetAttributeValue<string>("postimagename"));
            Assert.True(postImage.Contains("accountnumber"));
            Assert.True(postImage.Contains("numberofemployees"));
            Assert.False(postImage.Contains("accountcategorycode"));
            Assert.True(postImage.Contains("revenue"));
            Assert.False(postImage.Contains("primarycontactid"));
            Assert.False(postImage.Contains("telephone1"));
        }
    }
}