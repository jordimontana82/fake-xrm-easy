namespace FakeXrmEasy
{
    public enum ErrorCodes : int
    {

        /// <summary>
        /// Access is denied.
        /// </summary>
        AccessDenied = -2147187707,

        /// <summary>
        /// Access denied on SharePoint record in Dynamics 365.
        /// </summary>
        AccessDeniedSharePointRecord = -2147088124,

        /// <summary>
        /// The requested resource requires authentication.
        /// </summary>
        AccessTokenExpired = -2147094271,

        /// <summary>
        /// Account does not exist.
        /// </summary>
        AccountDoesNotExist = -2147220222,

        /// <summary>
        /// Creating this parental association would create a loop in Accounts hierarchy.
        /// </summary>
        AccountLoopBeingCreated = -2147220217,

        /// <summary>
        /// Loop exists in the accounts hierarchy.
        /// </summary>
        AccountLoopExists = -2147220218,

        /// <summary>
        /// Action Card feature is not enabled.
        /// </summary>
        ActionCardDisabledError = -2147020544,

        /// <summary>
        /// Invalid state code passed in expression.
        /// </summary>
        ActionCardInvalidStateCodeException = -2147020541,

        /// <summary>
        /// ActionStep references invalid Pipeline Stage Id.
        /// </summary>
        ActionStepInvalidPipelineStageid = -2147089394,

        /// <summary>
        /// ActionStep {0} references invalid Process Action {1}.
        /// </summary>
        ActionStepInvalidProcessAction = -2147089380,

        /// <summary>
        /// ActionStep references invalid Process Id.
        /// </summary>
        ActionStepInvalidProcessid = -2147089395,

        /// <summary>
        /// ActionStep references invalid Stage Id.
        /// </summary>
        ActionStepInvalidStageid = -2147089396,

        /// <summary>
        /// Business Processes containing an Action Step cannot be exported because Action Step support is still a Public Preview feature and it is not currently enabled for this organization.
        /// </summary>
        ActionSupportNotEnabled = -2147089534,

        /// <summary>
        /// You can't create a property instance for an inactive property.
        /// </summary>
        ActivePropertyValidationFailed = -2147086335,

        /// <summary>
        /// An active queue item already exists for the given object. Cannot create more than one active queue item for this object.
        /// </summary>
        ActiveQueueItemAlreadyExists = -2147220186,

        /// <summary>
        /// You can't edit an active SLA. Deactivate the SLA, and then try editing it.
        /// </summary>
        ActiveSlaCannotEdit = -2147157903,

        /// <summary>
        /// Active Stage ID ‘{0}’ does not match last Stage ID in Traversed Path ‘{1}’. Please contact your system administrator.
        /// </summary>
        ActiveStageIdDoesNotMatchLastStageInTraversedPath = -2147089323,

        /// <summary>
        /// Error updating the Business Process: Active Stage ID cannot be empty.
        /// </summary>
        ActiveStageIDIsNull = -2147089335,

        /// <summary>
        /// Active stage is not on 'Lead' entity.
        /// </summary>
        ActiveStageIsNotOnLeadEntity = -2146435070,

        /// <summary>
        /// Relationship Analytics organization setting can only be enabled if Relationship Analytics solution is installed.
        /// </summary>
        ActivityAnalysisOrganizationUpdateError = -2147204491,

        /// <summary>
        /// A custom entity defined as an activity must not have a relationship with Activities.
        /// </summary>
        ActivityCannotHaveRelatedActivities = -2147159775,

        /// <summary>
        /// An activity entity cannot be also an activity party
        /// </summary>
        ActivityEntityCannotBeActivityParty = -2147187455,

        /// <summary>
        /// An Invalid type code was specified by the throwing method
        /// </summary>
        ActivityInvalidObjectTypeCode = -2147207917,

        /// <summary>
        /// An Invalid session token was passed into the throwing method
        /// </summary>
        ActivityInvalidSessionToken = -2147207918,

        /// <summary>
        /// The metadata specified for activity is invalid.
        /// </summary>
        ActivityMetadataUpdate = -2147159770,

        /// <summary>
        /// A custom entity defined as an activity must have a relationship to Notes by default.
        /// </summary>
        ActivityMustHaveRelatedNotes = -2147159773,

        /// <summary>
        /// Cannot create activity party of specified object type.
        /// </summary>
        ActivityPartyObjectTypeNotAllowed = -2147207930,

        /// <summary>
        /// The System Administrator field security profile cannot be modified or deleted.
        /// </summary>
        AdminProfileCannotBeEditedOrDeleted = -2147158772,

        /// <summary>
        /// An unexpected error occurred executing the search. Try again later.
        /// </summary>
        AdvancedSimilarityAzureSearchUnexpectedError = -2147084650,

        /// <summary>
        /// The Inner Query must not be an aggregate query.
        /// </summary>
        AggregateInnerQuery = -2147167567,

        /// <summary>
        /// The maximum record limit is exceeded. Reduce the number of records.
        /// </summary>
        AggregateQueryRecordLimitExceeded = -2147164125,

        /// <summary>
        /// Given linked attribute is alreadly linked to other attribute.
        /// </summary>
        AlreadyLinkedToAnotherAttribute = -2147159810,

        /// <summary>
        /// In-App Customization App Configuration feature is not enabled.
        /// </summary>
        AppConfigFeatureNotEnabled = -2147016192,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadataConverterFailed = -2147093967,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadatadaCreateFailed = -2147093965,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadatadaNullData = -2147093966,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadatadaUpdateFailed = -2147093964,

        /// <summary>
        /// There was a problem with the server configuration changes.  You can continue using the application, but may experience difficulties, including the inability to save changes. Please contact your Dynamics 365 administrator and give them the information available in ‘more information’.
        /// </summary>
        ApplicationMetadataFailedWithContinue = -2147093951,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadataGetPreviewMetadataUnknownError = -2147093968,

        /// <summary>
        /// We encountered some issues when we tried to prepare your customizations for your users. Users on some clients won't be able to download your customization updates until this issue is resolved.
        /// </summary>
        ApplicationMetadataPrepareCustomizationsAppLock = -2147093961,

        /// <summary>
        /// There was a problem with the server configuration changes.  Users can continue using the application, but may experience difficulties, including the inability to save changes.
        /// </summary>
        ApplicationMetadataPrepareCustomizationsRetrieverError = -2147093963,

        /// <summary>
        /// Sorry, but your client customization changes could not be processed.  This may be due to a large number of entities enabled for your users, or the number of languages enabled.  Users will not receive customizations until this issue is resolved.
        /// </summary>
        ApplicationMetadataPrepareCustomizationsTimeout = -2147093962,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadataPrepareCustomizationsUnknownError = -2147093978,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadataRetrieveUnknownError = -2147093975,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadataRetrieveUserContextUnknownError = -2147093977,

        /// <summary>
        /// Sorry, your server is busy so configurations can’t be downloaded right now. Your changes should be available in a few minutes.  Wait a few minutes, and sign in again.
        /// </summary>
        ApplicationMetadataSyncAppLock = -2147093948,

        /// <summary>
        /// Sorry, your server is busy so configuration changes can’t be downloaded right now. Your changes should be available in a few minutes.  In the meantime, you can continue using the app, and you’ll be reminded later to try downloading the changes. Or, you can wait a few minutes, restart the app, and accept the prompt to try again.
        /// </summary>
        ApplicationMetadataSyncAppLockWithContinue = -2147093947,

        /// <summary>
        /// There was a problem with the server configuration changes.  We are unable to load the application, please contact your Dynamics 365 administrator.
        /// </summary>
        ApplicationMetadataSyncFailed = -2147093952,

        /// <summary>
        /// Sorry, but your server configuration changes could not be downloaded.  This may be due to a slow connection, or due to a large number of entities enabled for mobile use.  Please verify your connection and try again.  If this issue continues please contact your Dynamics 365 administrator.
        /// </summary>
        ApplicationMetadataSyncTimeout = -2147093950,

        /// <summary>
        /// Sorry, but your server configuration changes could not be downloaded.  This may be due to a slow connection, or due to a large number of entities enabled for mobile use.  Please verify your connection and try again. You can continue to use the app with the older configuration, however you may experience problems including errors when saving.  If this issue continues please contact your Dynamics 365 administrator.
        /// </summary>
        ApplicationMetadataSyncTimeoutWithContinue = -2147093949,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadataSyncUnknownError = -2147093976,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        ApplicationMetadataUserValidationUnknownError = -2147093979,

        /// <summary>
        /// Application needs to be registered and enabled at deployment level before it can be created for this organization
        /// </summary>
        ApplicationNotRegisteredWithDeployment = -2147214007,

        /// <summary>
        /// The application profile must contain at least one entity.
        /// </summary>
        ApplicationProfileMustContainEntity = -2147020495,

        /// <summary>
        /// The user representing an OAuth application cannot not be updated
        /// </summary>
        ApplicationUserCannotBeUpdated = -2147214008,

        /// <summary>
        /// Timeout expired before applock could be acquired.
        /// </summary>
        AppLockTimeout = -2147160761,

        /// <summary>
        /// You can only apply active service level agreements (SLAs) to cases.
        /// </summary>
        ApplyActiveSLAOnly = -2147135487,

        /// <summary>
        /// The entity “{0}” must have at least one form or view in the app.
        /// </summary>
        AppModuleComponentEntityMustHaveFormOrView = -2147155691,

        /// <summary>
        /// The feature isn’t turned on for this organization.
        /// </summary>
        AppModuleFeatureNotEnabled = -2147155689,

        /// <summary>
        /// The “{0}” entity isn’t valid for the chosen client, and won’t be shown at runtime.
        /// </summary>
        AppModuleMustHaveOnlyValidClientEntity = -2147155670,

        /// <summary>
        /// App Module with MOCA as a supported client should have at least one MOCA enabled entity
        /// </summary>
        AppModuleNotContainMOCAEnabledEntity = -2147155688,

        /// <summary>
        /// App Module does not reference at least one entity
        /// </summary>
        AppModuleNotReferEntity = -2147155683,

        /// <summary>
        /// An error occurred while importing App Modules
        /// </summary>
        AppModulesImportError = -2147155677,

        /// <summary>
        /// Couldn’t create the app. There’s already an app for this client type.
        /// </summary>
        AppModuleWithClientExists = -2147155673,

        /// <summary>
        /// The appointment entity instance is already deleted.
        /// </summary>
        AppointmentDeleted = -2147163898,

        /// <summary>
        /// Scheduled End and Scheduled Start must be set for Appointments in order to sync with Outlook.
        /// </summary>
        AppointmentScheduleNotSet = -2147220875,

        /// <summary>
        /// Direction mismatch for argument {0}.
        /// </summary>
        ArgumentDirectionMismatch = -2147089528,

        /// <summary>
        /// Type mismatch for argument {0}.
        /// </summary>
        ArgumentTypeMismatch = -2147089529,

        /// <summary>
        /// An array transformation parameter mapping is defined for a single parameter.
        /// </summary>
        ArrayMappingFoundForSingletonParameter = -2147220609,

        /// <summary>
        /// The article cannot be updated or deleted because it is in published state
        /// </summary>
        ArticleIsPublished = -2147220226,

        /// <summary>
        /// The product can't be added to the bundle. You have to use a product unit that belongs to the unit group of the product.
        /// </summary>
        AssociateProductFailureDifferentUom = -2147086272,

        /// <summary>
        /// The association role ordinal is not valid - it must be 1 or 2.
        /// </summary>
        AssociationRoleOrdinalInvalid = -2147187608,

        /// <summary>
        /// A communication error occurred while processing the async operation.
        /// </summary>
        AsyncCommunicationError = -2147204345,

        /// <summary>
        /// An error occurred while accessing the network.
        /// </summary>
        AsyncNetworkError = -2147204346,

        /// <summary>
        /// This system job cannot be canceled.
        /// </summary>
        AsyncOperationCannotCancel = -2147201280,

        /// <summary>
        /// Cannot delete async operation unless it is in Completed state.
        /// </summary>
        AsyncOperationCannotDeleteUnlessCompleted = -2147204758,

        /// <summary>
        /// This system job cannot be paused.
        /// </summary>
        AsyncOperationCannotPause = -2147201279,

        /// <summary>
        /// Cannot update recurrence pattern for a job that is not recurring.
        /// </summary>
        AsyncOperationCannotUpdateNonrecurring = -2147204760,

        /// <summary>
        /// Cannot update recurrence pattern for a job type that is not supported.
        /// </summary>
        AsyncOperationCannotUpdateRecurring = -2147204759,

        /// <summary>
        /// The target state could not be set because the state transition is not valid.
        /// </summary>
        AsyncOperationInvalidStateChange = -2147204766,

        /// <summary>
        /// The target state could not be set to complete because the state transition is not valid.
        /// </summary>
        AsyncOperationInvalidStateChangeToComplete = -2147204763,

        /// <summary>
        /// The target state could not be set to ready because the state transition is not valid.
        /// </summary>
        AsyncOperationInvalidStateChangeToReady = -2147204762,

        /// <summary>
        /// The target state could not be set to suspended because the state transition is not valid.
        /// </summary>
        AsyncOperationInvalidStateChangeToSuspended = -2147204761,

        /// <summary>
        /// The target state could not be set because the state was changed by another process.
        /// </summary>
        AsyncOperationInvalidStateChangeUnexpected = -2147204765,

        /// <summary>
        /// The AsyncOperationId is required to do the update.
        /// </summary>
        AsyncOperationMissingId = -2147204764,

        /// <summary>
        /// This operation has been postponed because it failed for more than {0} times in {1} minutes
        /// </summary>
        AsyncOperationPostponed = -2147220696,

        /// <summary>
        /// Currently, we are unable to complete this action. It has been postponed. We will try again later.
        /// </summary>
        AsyncOperationPostponedByExceptionCountThrottle = -2147088105,

        /// <summary>
        /// &gt;A background job associated with this import is either suspended or locked. In order to delete this import, in the Workplace, click Imports, open the import, click System Jobs, and resume any suspended jobs.
        /// </summary>
        AsyncOperationSuspendedOrLocked = -2147220679,

        /// <summary>
        /// The operation type of the async operation was not recognized.
        /// </summary>
        AsyncOperationTypeIsNotRecognized = -2147204349,

        /// <summary>
        /// The job could not be completed because the server is busy. We will retry the job again soon.
        /// </summary>
        AsyncOperationTypeThrottled = -2147088106,

        /// <summary>
        /// The attachment is either not a valid type or is too large. It cannot be uploaded or downloaded.
        /// </summary>
        AttachmentBlocked = -2147205623,

        /// <summary>
        /// Attachment file name contains invalid characters.
        /// </summary>
        AttachmentInvalidFileName = -2147202552,

        /// <summary>
        /// The reference to the attachment couldn't be found.
        /// </summary>
        AttachmentNotFound = -2147187565,

        /// <summary>
        /// This attachment does not belong to an email.
        /// </summary>
        AttachmentNotRelatedToEmail = -2147155962,

        /// <summary>
        /// Attribute - {0} cannot be updated for a Business Process Flow
        /// </summary>
        AttributeCannotBeUpdated = -2147089389,

        /// <summary>
        /// The {0} attribute cannot be used with an aggregation function in a formula.
        /// </summary>
        AttributeCannotBeUsedInAggregate = -2147089063,

        /// <summary>
        /// "Attribute '{0}' on entity '{1}' is deprecated."
        /// </summary>
        AttributeDeprecated = -2147204299,

        /// <summary>
        /// The specified attribute does not support localized labels.
        /// </summary>
        AttributeDoesNotSupportLocalizedLabels = -2147204712,

        /// <summary>
        /// The formula is empty.
        /// </summary>
        AttributeFormulaDefinitionIsEmpty = -2147089351,

        /// <summary>
        /// The specified attribute is not a custom attribute
        /// </summary>
        AttributeIsNotCustomAttribute = -2147192823,

        /// <summary>
        /// Cannot set user search facets for entity {0} as attribute {1} is not facetable. Kindly remove it from the list to proceed.
        /// </summary>
        AttributeIsNotFacetable = -2147089659,

        /// <summary>
        /// This attribute cannot be created since support to enable attribute for officegraph is not available.
        /// </summary>
        AttributeNotCreatedForOfficeGraphError = -2147204553,

        /// <summary>
        /// This attribute is not mapped to a drop-down list, Boolean, or state/status attribute. However, you have included a ListValueMap element for it.  Fix this inconsistency, and then import this data map again.
        /// </summary>
        AttributeNotOfTypePicklist = -2147220676,

        /// <summary>
        /// This attribute is not mapped as a reference attribute. However, you have included a ReferenceMap for it.  Fix this inconsistency, and then import this data map again.
        /// </summary>
        AttributeNotOfTypeReference = -2147220592,

        /// <summary>
        /// One or more fields are not enabled for field level security. Field level security is not enabled until you publish the customizations.
        /// </summary>
        AttributeNotSecured = -2147158776,

        /// <summary>
        /// This attribute cannot be updated since support to enable attribute for officegraph is not available.
        /// </summary>
        AttributeNotUpdatedForOfficeGraphError = -2147204552,

        /// <summary>
        /// Permission '{0}' for field '{1}' with id={2} is invalid.
        /// </summary>
        AttributePermissionIsInvalid = -2147158770,

        /// <summary>
        /// The user does not have read permissions to a secured field. The requested operation could not be completed.
        /// </summary>
        AttributePermissionReadIsMissing = -2147158780,

        /// <summary>
        /// The user does not have update permissions to a secured field. The requested operation could not be completed.
        /// </summary>
        AttributePermissionUpdateIsMissingDuringShare = -2147158781,

        /// <summary>
        /// The user doesn't have AttributePrivilegeUpdate and not granted shared access for a secured attribute during update operation
        /// </summary>
        AttributePermissionUpdateIsMissingDuringUpdate = -2147158777,

        /// <summary>
        /// The user does not have create permissions to a secured field. The requested operation could not be completed.
        /// </summary>
        AttributePrivilegeCreateIsMissing = -2147158782,

        /// <summary>
        /// You must have sufficient permissions for a secured field before you can change its field level security.
        /// </summary>
        AttributePrivilegeInvalidToUnsecure = -2147158771,

        /// <summary>
        /// Attributes cannot be more than 4
        /// </summary>
        AttributesExceeded = -2147085055,

        /// <summary>
        /// Attribute has already been shared.
        /// </summary>
        AttributeSharingCreateDuplicate = -2147158773,

        /// <summary>
        /// You must set read and/or update access when you share a secured attribute. Attribute ID: {0}
        /// </summary>
        AttributeSharingCreateShouldSetReadOrAndUpdateAccess = -2147158775,

        /// <summary>
        /// Both readAccess and updateAccess are false: call Delete instead of Update.
        /// </summary>
        AttributeSharingUpdateInvalid = -2147158774,

        /// <summary>
        /// Calculated/RollUp Field is not supported for MultiSelectPicklist Attribute Type.
        /// </summary>
        AttributeTypeNotSupportedForCalculatedField = -2147155423,

        /// <summary>
        /// GroupBy or OrderBy Query is not supported for MultiSelectPickList Attribute Type.
        /// </summary>
        AttributeTypeNotSupportedForGroupByOrderByQuery = -2147155420,

        /// <summary>
        /// The Business Process Flow update has failed. The update of attribute "{0}" in workflow "{1}" is not allowed.
        /// </summary>
        AttributeUpdateNotAllowed = -2147089309,

        /// <summary>
        /// Authenticate to serverType: {0} before requesting a proxy.
        /// </summary>
        AuthenticateToServerBeforeRequestingProxy = -2147167704,

        /// <summary>
        /// You don’t have the proper Office 365 license to get untracked emails. Please contact your system administrator.
        /// </summary>
        AutoDataCaptureAuthorizationFailureException = -2146889662,

        /// <summary>
        /// Auto capture feature is not enabled.
        /// </summary>
        AutoDataCaptureDisabledError = -2146889663,

        /// <summary>
        /// Error while fetching untracked emails from Exchange.
        /// </summary>
        AutoDataCaptureResponseRetrievalFailureException = -2146889661,

        /// <summary>
        /// We didn’t find that application ID {0} in your Azure Active Directory (Azure AD) with CorrelationID {1}. Make sure your application is registered in Azure AD.
        /// </summary>
        AzureApplicationIdNotFound = -2147158768,

        /// <summary>
        /// Azure applicationid not found. We didn’t find the application ID {0} in your CRM database. Correct the application ID and resubmit the update.
        /// </summary>
        AzureApplicationIdNotFoundInOrgDB = -2147158766,

        /// <summary>
        /// An Azure operation request did not return a response within stated timeout period. Retry the operation or increase timeout provided for the operation.
        /// </summary>
        AzureOperationResponseTimedOut = -2147084747,

        /// <summary>
        /// The Azure recommendation model build corresponding to the used model version doesn’t exist.
        /// </summary>
        AzureRecommendationModelBuildNotExist = -2147084796,

        /// <summary>
        /// The Azure recommendation model doesn’t exist.
        /// </summary>
        AzureRecommendationModelNotExist = -2147084797,

        /// <summary>
        /// One or more models use the connection. Delete all models using this connection, and try deleting the connection again.
        /// </summary>
        AzureServiceConnectionCascadeDeleteFailed = -2147084746,

        /// <summary>
        /// Provide a valid service URL.
        /// </summary>
        AzureServiceConnectionInvalidUri = -2147084752,

        /// <summary>
        /// The limit of resource for the database has been reached. Please check the exception for more details.
        /// </summary>
        AzureSqlDatabaseResourceLimitExceededError = -2147015901,

        /// <summary>
        /// The storage limit for the database has been met.
        /// </summary>
        AzureSqlDatabaseStorageQuotaExceededError = -2147015899,

        /// <summary>
        /// Too many concurrent requests detected.
        /// </summary>
        AzureSqlMaxConcurrentWorkersLimitExceededError = -2147015900,

        /// <summary>
        /// Azure WebApp based plugins disabled for the organization.
        /// </summary>
        AzureWebAppPluginsDisabled = -2147155391,

        /// <summary>
        /// The ticket specified for authentication is invalid
        /// </summary>
        BadAuthTicket = -2147180286,

        /// <summary>
        /// The request could not be understood by the server.
        /// </summary>
        BadRequest = -2147094272,

        /// <summary>
        /// BaseAttribute name should be present in condition xml.
        /// </summary>
        BaseAttributeNameNotPresentError = -2147204544,

        /// <summary>
        /// The base currency cannot be deactivated.
        /// </summary>
        BaseCurrencyCannotBeDeactivated = -2147185420,

        /// <summary>
        /// The base currency of an organization cannot be deleted.
        /// </summary>
        BaseCurrencyNotDeletable = -2147185409,

        /// <summary>
        /// The exchange rate set for the currency specified in this record has generated a value for {0} that is larger than the maximum allowed for the base currency ({1}).
        /// </summary>
        BaseCurrencyOverflow = -2147185428,

        /// <summary>
        /// The exchange rate set for the currency specified in this record has generated a value for {0} that is smaller than the minimum allowed for the base currency ({1}).
        /// </summary>
        BaseCurrencyUnderflow = -2147185429,

        /// <summary>
        /// Base and Matching attribute should be of same type.
        /// </summary>
        BaseMatchingAttributeNotSameError = -2147204539,

        /// <summary>
        /// The base unit does not exist.
        /// </summary>
        BaseUnitDoesNotExist = -2147206372,

        /// <summary>
        /// The base unit of a schedule cannot be deleted.
        /// </summary>
        BaseUnitNotDeletable = -2147206397,

        /// <summary>
        /// Do not use a base unit as the value for a primary unit. This value should always be null.
        /// </summary>
        BaseUnitNotNull = -2147206377,

        /// <summary>
        /// baseuomname not specified
        /// </summary>
        BaseUomNameNotSpecified = -2147207152,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_ADDRESS_VALIDATION_FAILURE = -2147175104,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_AGREEMENT_ALREADY_SIGNED = -2147175103,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_AUTHORIZATION_FAILED = -2147175102,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_AVS_FAILED = -2147175101,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_BAD_CITYNAME_LENGTH = -2147175100,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_BAD_STATECODE_LENGTH = -2147175099,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_BAD_ZIPCODE_LENGTH = -2147175098,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_BADXML = -2147175097,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_BANNED_PAYMENT_INSTRUMENT = -2147175096,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_BANNEDPERSON = -2147175095,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_CANNOT_EXCEED_MAX_OWNERSHIP = -2147175094,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_COUNTRY_CURRENCY_PI_MISMATCH = -2147175093,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_CREDIT_CARD_EXPIRED = -2147175092,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_DATE_EXPIRED = -2147175091,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_ERROR_COUNTRYCODE_MISMATCH = -2147175090,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_ERROR_COUNTRYCODE_REQUIRED = -2147175089,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_EXTRA_REFERRAL_DATA = -2147175088,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_GUID_EXISTS = -2147175087,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_ADDRESS_ID = -2147175086,

        /// <summary>
        /// {0}  The specified Billing account is invalid.  Or, although the objectID is of the correct type, the account it identifies does not exist in the system.
        /// </summary>
        BDK_E_INVALID_BILLABLE_ACCOUNT_ID = -2147175085,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_BUF_SIZE = -2147175084,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_CATEGORY_NAME = -2147175083,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_COUNTRY_CODE = -2147175082,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_CURRENCY = -2147175081,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_CUSTOMER_TYPE = -2147175080,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_DATE = -2147175079,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_EMAIL_ADDRESS = -2147175078,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_FILTER = -2147175077,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_GUID = -2147175076,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_INPUT_TO_TAXWARE_OR_VERAZIP = -2147175075,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_LOCALE = -2147175074,

        /// <summary>
        /// {0}  The Billing system cannot find the object (e.g. account or subscription or offering).
        /// </summary>
        BDK_E_INVALID_OBJECT_ID = -2147175073,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_OFFERING_GUID = -2147175072,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_PAYMENT_INSTRUMENT_STATUS = -2147175071,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_PAYMENT_METHOD_ID = -2147175070,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_PHONE_TYPE = -2147175069,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_POLICY_ID = -2147175068,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_REFERRALDATA_XML = -2147175067,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_STATE_FOR_COUNTRY = -2147175066,

        /// <summary>
        /// {0}  The subscription id specified is invalid.  Or, although the objectID is of correct type and also points to a valid account in SCS, the subscription it identifies does not exist in SCS.
        /// </summary>
        BDK_E_INVALID_SUBSCRIPTION_ID = -2147175065,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_INVALID_TAX_EXEMPT_TYPE = -2147175064,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_MEG_CONFLICT = -2147175063,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_MULTIPLE_CITIES_FOUND = -2147175062,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_MULTIPLE_COUNTIES_FOUND = -2147175061,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_NON_ACTIVE_ACCOUNT = -2147175060,

        /// <summary>
        /// {0}  The calling partner does not have access to this method or when the requester does not have permission to search against the supplied search PUID.
        /// </summary>
        BDK_E_NOPERMISSION = -2147175059,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_OBJECT_ROLE_LIMIT_EXCEEDED = -2147175058,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_OFFERING_ACCOUNT_CURRENCY_MISMATCH = -2147175057,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_OFFERING_COUNTRY_ACCOUNT_MISMATCH = -2147175056,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_OFFERING_NOT_PURCHASEABLE = -2147175055,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_OFFERING_PAYMENT_INSTRUMENT_MISMATCH = -2147175054,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_OFFERING_REQUIRES_PI = -2147175053,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_PARTNERNOTINBILLING = -2147175052,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_PAYMENT_PROVIDER_CONNECTION_FAILED = -2147175051,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_POLICY_DEAL_COUNTRY_MISMATCH = -2147175049,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_PRIMARY_PHONE_REQUIRED = -2147175050,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_PUID_ROLE_LIMIT_EXCEEDED = -2147175048,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_RATING_FAILURE = -2147175047,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_REQUIRED_FIELD_MISSING = -2147175046,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_STATE_CITY_INVALID = -2147175045,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_STATE_INVALID = -2147175044,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_STATE_ZIP_CITY_INVALID = -2147175043,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_STATE_ZIP_CITY_INVALID2 = -2147175042,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_STATE_ZIP_CITY_INVALID3 = -2147175041,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_STATE_ZIP_CITY_INVALID4 = -2147175040,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_STATE_ZIP_COVERS_MULTIPLE_CITIES = -2147175039,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_STATE_ZIP_INVALID = -2147175038,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_TAXID_EXPDATE = -2147175037,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_TOKEN_BLACKLISTED = -2147175036,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_TOKEN_EXPIRED = -2147175035,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_TOKEN_NOT_VALID_FOR_OFFERING = -2147175034,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_TOKEN_RANGE_BLACKLISTED = -2147175033,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_TRANS_BALANCE_TO_PI_INVALID = -2147175032,

        /// <summary>
        /// {0}  Unknown server failure.
        /// </summary>
        BDK_E_UNKNOWN_SERVER_FAILURE = -2147175031,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_UNSUPPORTED_CHAR_EXIST = -2147175030,

        /// <summary>
        /// {0}  Billing token is already spent.
        /// </summary>
        BDK_E_USAGE_COUNT_FOR_TOKEN_EXCEEDED = -2147175025,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_VATID_DOESNOTHAVEEXPDATE = -2147175029,

        /// <summary>
        /// {0}
        /// </summary>
        BDK_E_ZIP_CITY_MISSING = -2147175028,

        /// <summary>
        /// {0}  Billing zip code error.
        /// </summary>
        BDK_E_ZIP_INVALID = -2147175027,

        /// <summary>
        /// {0}  Billing zip code error.
        /// </summary>
        BDK_E_ZIP_INVALID_FOR_ENTERED_STATE = -2147175026,

        /// <summary>
        /// An error occured while authenticating with server {0}.
        /// </summary>
        BidsAuthenticationError = -2147098621,

        /// <summary>
        /// Authentication failed when trying to connect to server {0}. The username or password is incorrect.
        /// </summary>
        BidsAuthenticationFailed = -2147098618,

        /// <summary>
        /// Input connection string is invalid. Usage: ServerUrl[;OrganizationName][;HomeRealmUrl]
        /// </summary>
        BidsInvalidConnectionString = -2147098624,

        /// <summary>
        /// Input url {0} is invalid.
        /// </summary>
        BidsInvalidUrl = -2147098623,

        /// <summary>
        /// No organizations found for the user.
        /// </summary>
        BidsNoOrganizationsFound = -2147098620,

        /// <summary>
        /// Organization {0} cannot be found for the user.
        /// </summary>
        BidsOrganizationNotFound = -2147098619,

        /// <summary>
        /// Failed to connect to server {0}.
        /// </summary>
        BidsServerConnectionFailed = -2147098622,

        /// <summary>
        /// No Billing application configuration setting [{0}] was found.
        /// </summary>
        BillingNoSettingError = -2147175119,

        /// <summary>
        /// Could not determine the right Partner certificate to use with Billing.  Issuer: {0}  Subject: {1}  Distinguished matches: [{2}]  Name matches: [{3}]  All valid certificates: [{4}].
        /// </summary>
        BillingPartnerCertificate = -2147175120,

        /// <summary>
        /// Could not retrieve Billing session key: "{0}"
        /// </summary>
        BillingRetrieveKeyError = -2147175112,

        /// <summary>
        /// Billing is not available: Call to IsServiceAvailable returned 'False'.
        /// </summary>
        BillingTestConnectionError = -2147175118,

        /// <summary>
        /// Billing TestConnection exception.
        /// </summary>
        BillingTestConnectionException = -2147175117,

        /// <summary>
        /// Billing error code [{0}] was thrown with exception {1}
        /// </summary>
        BillingUnknownErrorCode = -2147175114,

        /// <summary>
        /// Billing error was thrown with exception {0}
        /// </summary>
        BillingUnknownException = -2147175113,

        /// <summary>
        /// Billing error code [{0}] was thrown with exception {1}
        /// </summary>
        BillingUnmappedErrorCode = -2147175115,

        /// <summary>
        /// User Puid is required, but is null.
        /// </summary>
        BillingUserPuidNullError = -2147175116,

        /// <summary>
        /// Failed to book first instance.
        /// </summary>
        BookFirstInstanceFailed = -2147163890,

        /// <summary>
        /// Boolean attribute options must have a value of either 0 or 1.
        /// </summary>
        BooleanOptionOutOfRange = -2147204324,

        /// <summary>
        /// You must provide a name or select a role for both sides of this connection.
        /// </summary>
        BothConnectionSidesAreNeeded = -2147188200,

        /// <summary>
        /// BPF entity with this name already exists.
        /// </summary>
        BPFEntityAlreadyExists = -2147089532,

        /// <summary>
        /// Error creating or updating Business Process: BpfEntityService cannot be null.
        /// </summary>
        BpfEntityServiceIsNull = -2147089338,

        /// <summary>
        /// Error creating or updating Business Process: BpfFactory cannot be null.
        /// </summary>
        BpfFactoryIsNull = -2147089337,

        /// <summary>
        /// A business process flow already exists for the target record and could not be created. Please contact your system administrator.
        /// </summary>
        BpfInstanceAlreadyExists = -2147089513,

        /// <summary>
        /// Error creating or updating Business Process: BpfVisitor cannot be null.
        /// </summary>
        BpfVisitorIsNull = -2147089336,

        /// <summary>
        /// One of the Bulk Delete Child Jobs Failed
        /// </summary>
        BulkDeleteChildFailure = -2147187623,

        /// <summary>
        /// The record cannot be deleted.
        /// </summary>
        BulkDeleteRecordDeletionFailure = -2147187659,

        /// <summary>
        /// The e-mail recipient either does not exist or the e-mail address for the e-mail recipient is not valid.
        /// </summary>
        BulkDetectInvalidEmailRecipient = -2147187678,

        /// <summary>
        /// The bulk e-mail job completed with {0} failures. The failures might be caused by missing e-mail addresses or because you do not have permission to send e-mail. To find records with missing e-mail addresses, use Advanced Find. If you need increased e-mail permissions, contact your system administrator.
        /// </summary>
        BulkMailOperationFailed = -2147200979,

        /// <summary>
        /// The Microsoft Dynamics 365 Bulk E-Mail Service is not running.
        /// </summary>
        BulkMailServiceNotAccessible = -2147187964,

        /// <summary>
        /// You can't add a bundle to a bundle.
        /// </summary>
        BundleCannotContainBundle = -2147157646,

        /// <summary>
        /// You can't add a product family to a bundle.
        /// </summary>
        BundleCannotContainProductFamily = -2147157614,

        /// <summary>
        /// You can't add a kit to a bundle.
        /// </summary>
        BundleCannotContainProductKit = -2147086316,

        /// <summary>
        /// This bundle can't be published because it has too many properties. A bundle in your organization can't have more than {0} properties.
        /// </summary>
        BundleMaxPropertyLimitExceeded = -2146955250,

        /// <summary>
        /// The user Id(s) [{0}] is invalid.
        /// </summary>
        BusinessManagementInvalidUserId = -2147214049,

        /// <summary>
        /// Creating this parental association would create a loop in business hierarchy.
        /// </summary>
        BusinessManagementLoopBeingCreated = -2147214047,

        /// <summary>
        /// Loop exists in the business hierarchy.
        /// </summary>
        BusinessManagementLoopExists = -2147214048,

        /// <summary>
        /// An object with the specified name already exists.
        /// </summary>
        BusinessManagementObjectAlreadyExists = -2147220950,

        /// <summary>
        /// The specified business unit is disabled.
        /// </summary>
        BusinessNotEnabled = -2147220692,

        /// <summary>
        /// This action cannot be completed because the Business Process Flow definition is out of sync with the Process Action. Please contact your Dynamics 365 System Administrator to update the Business Process Flow.
        /// </summary>
        BusinessProcessFlowDefinitionOutdated = -2147089547,

        /// <summary>
        /// Failed to import Business Process '{0}' because solution does not include corresponding Business Process entity '{1}'.
        /// </summary>
        BusinessProcessFlowMissingEntityErrorMessage = -2147089344,

        /// <summary>
        /// {0} parent is not of type {1}
        /// </summary>
        BusinessProcessFlowStepHasInvalidParent = -2147089403,

        /// <summary>
        /// The business rule editor only supports one if condition. Please fix the rule.
        /// </summary>
        BusinessRuleEditorSupportsOnlyIfConditionBranch = -2147090424,

        /// <summary>
        /// Business unit cannot be disabled: no active user with system admin role exists outside of business unit subtree.
        /// </summary>
        BusinessUnitCannotBeDisabled = -2147213991,

        /// <summary>
        /// Business unit default team owns records. Business unit cannot be deleted.
        /// </summary>
        BusinessUnitDefaultTeamOwnsRecords = -2147213982,

        /// <summary>
        /// Business unit has a child business unit and cannot be deleted.
        /// </summary>
        BusinessUnitHasChildAndCannotBeDeleted = -2147213983,

        /// <summary>
        /// Not disabled business unit cannot be deleted.
        /// </summary>
        BusinessUnitIsNotDisabledAndCannotBeDeleted = -2147213984,

        /// <summary>
        /// There are {0} queues referencing this BusinessUnit with id ={1}, Name = {2}. Please delete the queues before deleting this business unit or assign to a different Business Unit.
        /// </summary>
        BusinessUnitQueuesAssociatedWithBU = -2147015386,

        /// <summary>
        /// You can’t set the value {0}, which is of type {1}, to type {2}.
        /// </summary>
        CalculatedFieldsAssignmentMismatch = -2147089365,

        /// <summary>
        /// Field {0} cannot be used in calculated field {1} because it would create a circular reference.
        /// </summary>
        CalculatedFieldsCyclicReference = -2147089359,

        /// <summary>
        /// You can only use a Date Only type of field.
        /// </summary>
        CalculatedFieldsDateOnlyBehaviorTypeMismatch = -2147089350,

        /// <summary>
        /// You can’t create or update the {0} field because the {1} field already has a calculated field chain of {2} deep.
        /// </summary>
        CalculatedFieldsDepthExceeded = -2147089358,

        /// <summary>
        /// You cannot divide by {0}, which evaluates to zero.
        /// </summary>
        CalculatedFieldsDivideByZero = -2147089363,

        /// <summary>
        /// Field {0} cannot be created or updated because field {1} contains an additional formula that uses a parent record.
        /// </summary>
        CalculatedFieldsEntitiesExceeded = -2147089357,

        /// <summary>
        /// Calculated Field feature is not available
        /// </summary>
        CalculatedFieldsFeatureNotEnabled = -2147089374,

        /// <summary>
        /// You can't use {0}, which is of type {1}, with the current function.
        /// </summary>
        CalculatedFieldsFunctionMismatch = -2147089364,

        /// <summary>
        /// The {0} field doesn't exist.
        /// </summary>
        CalculatedFieldsInvalidAttribute = -2147089368,

        /// <summary>
        /// The formula references the following attributes that don't exist: {0}.
        /// </summary>
        CalculatedFieldsInvalidAttributes = -2147089362,

        /// <summary>
        /// The formula contains an invalid reference: {0}.
        /// </summary>
        CalculatedFieldsInvalidEntity = -2147089373,

        /// <summary>
        /// The {0} function doesn't exist.
        /// </summary>
        CalculatedFieldsInvalidFunction = -2147089369,

        /// <summary>
        /// The formula can't be saved because it contains references to the following fields that have invalid definitions: {0}.
        /// </summary>
        CalculatedFieldsInvalidSourceTypeMask = -2147089352,

        /// <summary>
        /// The formula references a value that doesn't exist.
        /// </summary>
        CalculatedFieldsInvalidValue = -2147089361,

        /// <summary>
        /// The formula references the following values that don't exist: {0}.
        /// </summary>
        CalculatedFieldsInvalidValues = -2147089360,

        /// <summary>
        /// The {0} field has an invalid XAML formula definition.
        /// </summary>
        CalculatedFieldsInvalidXaml = -2147089372,

        /// <summary>
        /// Only calculated fields can have a formula definition.
        /// </summary>
        CalculatedFieldsNonCalculatedFieldAssignment = -2147089371,

        /// <summary>
        /// Cannot convert the value {0} into type {1}.
        /// </summary>
        CalculatedFieldsPrimitiveOverflow = -2147089366,

        /// <summary>
        /// You can only use a Time-Zone Independent Date Time type of field.
        /// </summary>
        CalculatedFieldsTimeInvBehaviorTypeMismatch = -2147089349,

        /// <summary>
        /// You can't use {0}, which is of type {1}, with the current operator.
        /// </summary>
        CalculatedFieldsTypeMismatch = -2147089370,

        /// <summary>
        /// You can only use a User Local Date Time type of field.
        /// </summary>
        CalculatedFieldsUserLocBehaviorTypeMismatch = -2147089348,

        /// <summary>
        /// One or more rollup fields depend on this calculated field. This calculated field can't use a rollup field, another calculated field that is using a rollup field or a field from related entity.
        /// </summary>
        CalculatedFieldUsedInRollupFieldCannotBeComplex = -2147089072,

        /// <summary>
        /// The calculation failed due to an overflow error.
        /// </summary>
        CalculateNowOverflowError = -2147089064,

        /// <summary>
        /// The caller cannot change their own domain name
        /// </summary>
        CallerCannotChangeOwnDomainName = -2147204767,

        /// <summary>
        /// Callout Exception occurred.
        /// </summary>
        CalloutException = -2147220901,

        /// <summary>
        /// This campaign activity has been distributed already. Campaign activities cannot be distributed more than one time.
        /// </summary>
        CampaignActivityAlreadyPropagated = -2147220698,

        /// <summary>
        /// This Campaign Activity is closed or canceled. Campaign activities cannot be distributed after they have been closed or canceled.
        /// </summary>
        CampaignActivityClosed = -2147220687,

        /// <summary>
        /// RegardingObjectId is a required field.
        /// </summary>
        CampaignNotSpecifiedForCampaignActivity = -2147220727,

        /// <summary>
        /// RegardingObjectId is a required field.
        /// </summary>
        CampaignNotSpecifiedForCampaignResponse = -2147220726,

        /// <summary>
        /// {0} needs to be enabled for mobile offline.
        /// </summary>
        CanAssociateOnlyMobileOfflineEnabledEntityToProfileItem = -2147087970,

        /// <summary>
        /// This entity needs to be enabled for mobile offline.
        /// </summary>
        CanAssociateOnlyMobileOfflineEnableEntityToProfileItem = -2147087972,

        /// <summary>
        /// You can only add one mobile offline profile item record per entity to a mobile offline profile record.
        /// </summary>
        CanAssociateOnlyOneEntityPerProfileItem = -2147087971,

        /// <summary>
        /// Cancel active child case before canceling parent case.
        /// </summary>
        CancelActiveChildCaseFirst = -2147224495,

        /// <summary>
        /// The email that you are trying to deliver cannot be accepted by Microsoft Dynamics 365. Reason Code: {0}.
        /// </summary>
        CannotAcceptEmail = -2147098101,

        /// <summary>
        /// Exchange optin status is not accessible.
        /// </summary>
        CannotAccessExchangeOptinStatus = -2147020528,

        /// <summary>
        /// Mailbox cannot be activated because the user or queue associated with the mailbox is in disabled state. Mailbox can only be activated for Active User/Queue.
        /// </summary>
        CannotActivateMailboxForDisabledUserOrQueue = -2147098064,

        /// <summary>
        /// You can't activate a retired product family or bundle. Also, you can't activate a retired product that is part of a product family.
        /// </summary>
        CannotActivateRecord = -2146955241,

        /// <summary>
        /// User does not have the privilege to act on behalf another user.
        /// </summary>
        CannotActOnBehalfOfAnotherUser = -2147180272,

        /// <summary>
        /// User does not have the privilege to act on behalf of External Party.
        /// </summary>
        CannotActOnBehalfOfExternalParty = -2147086058,

        /// <summary>
        /// You can’t add the ActivityParty entity to the mobile offline profile item because it’s added automatically when an activity entity is added to the profile.
        /// </summary>
        CannotAddActivityPartyEntityToMobileOfflineProfileItem = -2147087955,

        /// <summary>
        /// You can't add a bundle to itself.
        /// </summary>
        CannotAddBundleToItself = -2147086287,

        /// <summary>
        /// You can't add the {0} bundle to the pricelist because the {1} bundle product isn't in the pricelist.
        /// </summary>
        CannotAddBundleToPricelist = -2147086329,

        /// <summary>
        /// You can’t add the BusinessDataLocalizedLabel entity to the mobile offline profile item because it’s added automatically when the Product entity is added to the profile.
        /// </summary>
        CannotAddBusinessDataLocalizedLabelEntityToMobileOfflineProfileItem = -2147087956,

        /// <summary>
        /// You can't add a product family, a draft product, or a draft bundle.
        /// </summary>
        CannotAddDraftFamilyProductBundleToCases = -2147157628,

        /// <summary>
        /// You can’t add the intersect entity to the mobile offline profile item because it’s added automatically when its parent entities are added to the profile.
        /// </summary>
        CannotAddIntersectEntityToMobileOfflineProfileItem = -2147087957,

        /// <summary>
        /// You can't add a kit to itself.
        /// </summary>
        CannotAddKitToItself = -2147086286,

        /// <summary>
        /// Can't add members to the default business unit team.
        /// </summary>
        CannotAddMembersToDefaultTeam = -2147187957,

        /// <summary>
        /// You cannot add an option to a Boolean attribute.
        /// </summary>
        CannotAddNewBooleanValue = -2147204323,

        /// <summary>
        /// You cannot add state options to a State attribute.
        /// </summary>
        CannotAddNewStateValue = -2147204322,

        /// <summary>
        /// The component {0} {1} cannot be added to the solution because it is not customizable
        /// </summary>
        CannotAddNonCustomizableComponent = -2147160043,

        /// <summary>
        /// Act on Behalf of Another User privilege cannot be added or removed.
        /// </summary>
        CannotAddOrActonBehalfAnotherUserPrivilege = -2147160765,

        /// <summary>
        /// You can't specify a parent record for a kit.
        /// </summary>
        CannotAddParentToAKit = -2147086315,

        /// <summary>
        /// You can't add a product family to a pricelist.
        /// </summary>
        CannotAddPricelistToProductFamily = -2147157758,

        /// <summary>
        /// You can only add Active products.
        /// </summary>
        CannotAddProduct = -2147157752,

        /// <summary>
        /// You can't add a bundle to a kit.
        /// </summary>
        CannotAddProductBundleToKit = -2147086302,

        /// <summary>
        /// You can't add a product family to a kit.
        /// </summary>
        CannotAddProductFamilyToKit = -2147086301,

        /// <summary>
        /// You cannot add products to this bundle.The limit of {0} has been reached for this bundle.
        /// </summary>
        CannotAddProductToBundle = -2147157642,

        /// <summary>
        /// You can't add a product that belongs to a product family to a kit.
        /// </summary>
        CannotAddProductToKit = -2147086300,

        /// <summary>
        /// You can't add a product to a retired kit.
        /// </summary>
        CannotAddProductToRetiredKit = -2147086298,

        /// <summary>
        /// The selected user does not have sufficient permissions to work on items in this queue.
        /// </summary>
        CannotAddQueueItemsToInactiveQueue = -2147220190,

        /// <summary>
        /// You can’t create a product relationship with a retired product.
        /// </summary>
        CannotAddRetiredProduct = -2147086285,

        /// <summary>
        /// You can't add a retired product to a kit.
        /// </summary>
        CannotAddRetiredProductToKit = -2147086297,

        /// <summary>
        /// Retired products can not be added to pricelists.
        /// </summary>
        CannotAddRetiredProductToPricelist = -2147086327,

        /// <summary>
        /// The entity record cannot be added to the queue as it already exists in other queue.
        /// </summary>
        CannotAddSingleQueueEnabledEntityToQueue = -2147220196,

        /// <summary>
        /// This item is not a valid solution component. For more information about solution components, see the Microsoft Dynamics 365 SDK documentation.
        /// </summary>
        CannotAddSolutionComponentWithoutRoots = -2147160040,

        /// <summary>
        /// You can’t add this user to this mobile offline profile because the user’s role is either missing or doesn’t have the Dynamics 365 for mobile privilege.
        /// </summary>
        CannotAddUserToMobileOfflineProfile = -2147087962,

        /// <summary>
        /// Cannot add Workflow Activation to solution
        /// </summary>
        CannotAddWorkflowActivationToSolution = -2147160052,

        /// <summary>
        /// Cannot assign address book filters
        /// </summary>
        CannotAssignAddressBookFilters = -2147187640,

        /// <summary>
        /// Cannot assign offline filters
        /// </summary>
        CannotAssignOfflineFilters = -2147220225,

        /// <summary>
        /// Cannot assign outlook filters
        /// </summary>
        CannotAssignOutlookFilters = -2147220892,

        /// <summary>
        /// Cannot assign roles or profiles to an access team.
        /// </summary>
        CannotAssignRolesOrProfilesToAccessTeam = -2147187919,

        /// <summary>
        /// The support user are read-only, which cannot be assigned with other roles
        /// </summary>
        CannotAssignRolesToSupportUser = -2147213999,

        /// <summary>
        /// The Support User Role cannot be assigned to a user.
        /// </summary>
        CannotAssignSupportUser = -2147214012,

        /// <summary>
        /// You cannot assign a record to the access team. You can assign a record to the owner team.
        /// </summary>
        CannotAssignToAccessTeam = -2147187904,

        /// <summary>
        /// The specified business unit cannot be assigned to because it is disabled.
        /// </summary>
        CannotAssignToDisabledBusiness = -2147220691,

        /// <summary>
        /// You can’t associate more than one external party item with an entity record that has been enabled as an external party.
        /// </summary>
        CannotAssociateExternalPartyItem = -2147086057,

        /// <summary>
        /// Cannot associate an inactive item to a Campaign.
        /// </summary>
        CannotAssociateInactiveItemToCampaign = -2147220732,

        /// <summary>
        /// Invalid object type code.
        /// </summary>
        CannotAssociateInvalidEntityToProfileItem = -2147087973,

        /// <summary>
        /// You can't create a relationship with a product family.
        /// </summary>
        CannotAssociateProductFamily = -2147157607,

        /// <summary>
        /// You can't create a product relationship with a retired bundle.
        /// </summary>
        CannotAssociateRetiredBundles = -2146955247,

        /// <summary>
        /// You can't create a product relationship with a retired product.
        /// </summary>
        CannotAssociateRetiredProducts = -2147157644,

        /// <summary>
        /// Cannot bind to another session, session already bound.
        /// </summary>
        CannotBindToSession = -2147220908,

        /// <summary>
        /// The invoice cannot be cancelled because it is not in active or paid state.
        /// </summary>
        CannotCancelInvoice = -2146435071,

        /// <summary>
        /// Internet Marketing User is a system user. You cannot change its access mode.
        /// </summary>
        CannotChangeAccessModeForInternetMarketingUser = -2147200971,

        /// <summary>
        /// An attribute's required level cannot be changed from SystemRequired
        /// </summary>
        CannotChangeAttributeRequiredLevel = -2147167597,

        /// <summary>
        /// Error occured during activating Convert Rule.Please check your privileges on Workflow and kindly try again or Contact your system administrator.
        /// </summary>
        CannotChangeConvertRuleState = -2147088140,

        /// <summary>
        /// You need to enable this entity for mobile offline before you can set or change the number of days since the record was last modified.
        /// </summary>
        CannotChangeDaysSinceRecordLastModified = -2147087964,

        /// <summary>
        /// Internet Marketing User is a system user. You cannot change its invitation status.
        /// </summary>
        CannotChangeInvitationStatusForInternetMarketingUser = -2147200970,

        /// <summary>
        /// You can't add or modify the product relationship of a retired product.
        /// </summary>
        CannotChangeProductRelationship = -2147086317,

        /// <summary>
        /// If a bundle is selected as an existing product, you can't change it to another value.
        /// </summary>
        CannotChangeSelectedBundleToAnotherValue = -2147157626,

        /// <summary>
        /// If a product is selected as an existing product, you can't change it to a bundle.
        /// </summary>
        CannotChangeSelectedProductWithBundle = -2147157625,

        /// <summary>
        /// Error occured during activating SLA..Please check your privileges on Workflow and kindly try again or Contact your system administrator.
        /// </summary>
        CannotChangeState = -2147157917,

        /// <summary>
        /// Only public views can be deactivated and activated.
        /// </summary>
        CannotChangeStateOfNonpublicView = -2147220871,

        /// <summary>
        /// You cannot modify the type of the team because there are records owned by the team.
        /// </summary>
        CannotChangeTeamTypeDueToOwnership = -2147187913,

        /// <summary>
        /// You cannot modify the type of the team because there are security roles or field security profiles assigned to the team.
        /// </summary>
        CannotChangeTeamTypeDueToRoleOrProfile = -2147187914,

        /// <summary>
        /// The Channel Property Group is used by one or more steps. Delete the properties from the conditions and steps that use the record before you save or activate the rule.
        /// </summary>
        CannotClearChannelPropertyGroupFromConvertRule = -2147088147,

        /// <summary>
        /// You can't create this new bundle because it contains more than the allowed number of {0} products that a bundle can contain.
        /// </summary>
        CannotCloneBundleAsProductLimitExceeded = -2147157627,

        /// <summary>
        /// You can't clone a bundle that contains retired products.
        /// </summary>
        CannotCloneBundleWithRetiredProducts = -2147086284,

        /// <summary>
        /// You can't clone a kit.
        /// </summary>
        CannotCloneProductKit = -2147086304,

        /// <summary>
        /// This operation can't be completed. One or more child cases can't be closed because of the status transition rules that are defined for cases.
        /// </summary>
        CannotCloseCase = -2147158954,

        /// <summary>
        /// Cannot connect a record to itself.
        /// </summary>
        CannotConnectToSelf = -2147188201,

        /// <summary>
        /// You can't convert a bundle to a kit.
        /// </summary>
        CannotConvertBundleToKit = -2147086288,

        /// <summary>
        /// You can't convert a product that is a part of a bundle to a kit.
        /// </summary>
        CannotConvertProductAssociatedWithBundleToKit = -2147086312,

        /// <summary>
        /// You can't convert a product that belongs to a product family to a kit.
        /// </summary>
        CannotConvertProductAssociatedWithFamilyToKit = -2147086314,

        /// <summary>
        /// You can't convert a product family to a kit.
        /// </summary>
        CannotConvertProductFamilyToKit = -2147086295,

        /// <summary>
        /// Cannot copy lists of different types.
        /// </summary>
        CannotCopyIncompatibleListType = -2147220730,

        /// <summary>
        /// This action is valid only for dynamic list.
        /// </summary>
        CannotCopyStaticList = -2147158268,

        /// <summary>
        /// Relationship with activities cannot be created through this operation
        /// </summary>
        CannotCreateActivityRelationship = -2147192826,

        /// <summary>
        /// Cannot create address book filters
        /// </summary>
        CannotCreateAddressBookFilters = -2147187641,

        /// <summary>
        /// You can't create this case as the default entitlement for the specified customer has no remaining terms.
        /// </summary>
        CannotCreateCase = -2147088301,

        /// <summary>
        /// Creation of a new component definition is not supported
        /// </summary>
        CannotCreateComponentDefinition = -2147016703,

        /// <summary>
        /// An external party record already exists with the same correlation key value.
        /// </summary>
        CannotCreateExternalPartyWithSameCorrelationKey = -2147086062,

        /// <summary>
        /// Cannot create a role from Support User Role.
        /// </summary>
        CannotCreateFromSupportUser = -2147214010,

        /// <summary>
        /// You can't create a kit of type bundle or product family.
        /// </summary>
        CannotCreateKitOfTypeFamilyOrBundle = -2147086318,

        /// <summary>
        /// A child position cannot be created/enabled under a disabled parent position.
        /// </summary>
        CannotCreateOrEnablePositionDueToParentPositionIsDisabled = -2147187900,

        /// <summary>
        /// Cannot create outlook filters
        /// </summary>
        CannotCreateOutlookFilters = -2147220893,

        /// <summary>
        /// Can not create instance of a plug-in. Verify that plug-in type is not defined as abstract and it has a public constructor supported by Dynamics 365 SDK.
        /// </summary>
        CannotCreatePluginInstance = -2147180031,

        /// <summary>
        /// You can only create a property option set item record that refers to a property that has its data type set to Option Set.
        /// </summary>
        CannotCreatePropertyOptionSetItem = -2146955245,

        /// <summary>
        /// Deactivated object cannot be added to queue.
        /// </summary>
        CannotCreateQueueItemInactiveObject = -2147220194,

        /// <summary>
        /// CampaignResponse can not be created for Template Campaign.
        /// </summary>
        CannotCreateResponseForTemplate = -2147220718,

        /// <summary>
        /// You can't create a service level agreement (SLA) for this entity because it’s not enabled for creating SLAs
        /// </summary>
        CannotCreateSLAForEntity = -2147135483,

        /// <summary>
        /// The property IsLicensed cannot be set for Sync User Creation.
        /// </summary>
        CannotCreateSyncUserIsLicensedField = -2147214003,

        /// <summary>
        /// This is not a valid Microsoft Online Services ID for this organization.
        /// </summary>
        CannotCreateSyncUserObjectMissing = -2147214005,

        /// <summary>
        /// You can’t create system or default themes. System or default theme can only be created out of box.
        /// </summary>
        CannotCreateSystemOrDefaultTheme = -2147088171,

        /// <summary>
        /// Source Attribute Not Valid For Create/Update if Metric Type is Count.
        /// </summary>
        CannotCreateUpdateSourceAttribute = -2147203068,

        /// <summary>
        /// Default views cannot be deactivated.
        /// </summary>
        CannotDeactivateDefaultView = -2147220870,

        /// <summary>
        /// You can't set this guest channel access profile as inactive.
        /// </summary>
        CannotDeactivateGuestProfile = -2147086075,

        /// <summary>
        /// You cannot define multiple values on this field.
        /// </summary>
        CannotDefineMultipleValuesOnOwnerFieldInProfileItemEntityFilter = -2147020503,

        /// <summary>
        /// You can't delete an active rule. Deactivate the Record Creation and Update Rule, and then try deleting it.
        /// </summary>
        CannotDeleteActiveCaseCreationRule = -2147157888,

        /// <summary>
        /// You can’t delete an active record creation rule item. Deactivate the record creation rule, and then try deleting it.
        /// </summary>
        CannotDeleteActiveRecordCreationRuleItem = -2147157868,

        /// <summary>
        /// You can not delete an active routing rule. Deactivate the rule to delete it.
        /// </summary>
        CannotDeleteActiveRule = -2147157936,

        /// <summary>
        /// You can't delete an active SLA. Deactivate the SLA, and then try deleting it.
        /// </summary>
        CannotDeleteActiveSla = -2147157904,

        /// <summary>
        /// This app can’t be deleted.
        /// </summary>
        CannotDeleteAppModuleAdministration = -2147155664,

        /// <summary>
        /// This app can’t be deleted.
        /// </summary>
        CannotDeleteAppModuleClientType = -2147155671,

        /// <summary>
        /// This record is currently being used by Microsoft Dynamics 365 and cannot be deleted. Try again later. If  the problem persists, contact your system administrator.
        /// </summary>
        CannotDeleteAsBackgroundOperationInProgress = -2147220693,

        /// <summary>
        /// The object cannot be deleted because it is read-only.
        /// </summary>
        CannotDeleteAsItIsReadOnly = -2147220952,

        /// <summary>
        /// This attribute cannot be deleted because it is used in one or more workflows. Cancel any system jobs for workflows that use this attribute, then delete or modify any workflows that use the attribute, and then try to delete the attribute again.
        /// </summary>
        CannotDeleteAttributeUsedInWorkflow = -2147200976,

        /// <summary>
        /// The base money calculation Attribute is not valid for deletion
        /// </summary>
        CannotDeleteBaseMoneyCalculationAttribute = -2147185410,

        /// <summary>
        /// System-defined views cannot be deleted.
        /// </summary>
        CannotDeleteCannedView = -2147220945,

        /// <summary>
        /// The Common Data Service User Role cannot be deleted.
        /// </summary>
        CannotDeleteCDSUser = -2147213990,

        /// <summary>
        /// You can't delete an active channel access profile rule. Deactivate the rule and then delete it.
        /// </summary>
        CannotDeleteChannelAccessProfileRule = -2147086072,

        /// <summary>
        /// You can’t delete a channel property which is being referred in a convert rule.
        /// </summary>
        CannotDeleteChannelProperty = -2147088149,

        /// <summary>
        /// The Child Attribute is not valid for deletion
        /// </summary>
        CannotDeleteChildAttribute = -2147192810,

        /// <summary>
        /// Cannot delete entity because it is used in a workflow.
        /// </summary>
        CannotDeleteCustomEntityUsedInWorkflow = -2147200980,

        /// <summary>
        /// To delete this profile, you first need to set it so that it’s no longer a default mobile offline profile.
        /// </summary>
        CannotDeleteDefaultProfile = -2147087974,

        /// <summary>
        /// Default Status options cannot be deleted.
        /// </summary>
        CannotDeleteDefaultStatusOption = -2147204287,

        /// <summary>
        /// The default business unit team can't be deleted.
        /// </summary>
        CannotDeleteDefaultTeam = -2147187961,

        /// <summary>
        /// The object you tried to delete is associated with another object and cannot be deleted.
        /// </summary>
        CannotDeleteDueToAssociation = -2147220953,

        /// <summary>
        /// You can't delete a Recommendation entity if it has a corresponding Basket entity.
        /// </summary>
        CannotDeleteDueToBasketEntityAssociation = -2147084782,

        /// <summary>
        /// You can't delete a property of a retired product.
        /// </summary>
        CannotDeleteDynamicPropertyInRetiredState = -2147157870,

        /// <summary>
        /// Retired Properties being used in transactions can not be deleted.
        /// </summary>
        CannotDeleteDynamicPropertyInUse = -2146955261,

        /// <summary>
        /// You can delete options only from picklist and status attributes.
        /// </summary>
        CannotDeleteEnumOptionsFromAttributeType = -2147204317,

        /// <summary>
        /// You can't delete this guest channel access profile.
        /// </summary>
        CannotDeleteGuestProfile = -2147086076,

        /// <summary>
        /// You can't delete a property that is inherited from a product family.
        /// </summary>
        CannotDeleteInheritedDynamicProperty = -2146955244,

        /// <summary>
        /// The selected attribute cannot be deleted because it is referenced by one or more duplicate detection rules that are being published.
        /// </summary>
        CannotDeleteInUseAttribute = -2147187688,

        /// <summary>
        /// The {0}({1}) component cannot be deleted because it is referenced by {2} other components. For a list of referenced components, use the RetrieveDependenciesForDeleteRequest.
        /// </summary>
        CannotDeleteInUseComponent = -2147160033,

        /// <summary>
        /// The selected entity cannot be deleted because it is referenced by one or more duplicate detection rules that are in process of being published.
        /// </summary>
        CannotDeleteInUseEntity = -2147187680,

        /// <summary>
        /// This option set cannot be deleted. The current set of entities that reference this option set are: {0}. These references must be removed before this option set can be deleted
        /// </summary>
        CannotDeleteInUseOptionSet = -2147187689,

        /// <summary>
        /// You cannot delete this field because the record type has been enabled for e-mail.
        /// </summary>
        CannotDeleteLastEmailAttribute = -2147192833,

        /// <summary>
        /// The '{2}' operation on the current component(name='{0}', id='{1}') failed during managed property evaluation of condition: '{3}'
        /// </summary>
        CannotDeleteMetadata = -2147160014,

        /// <summary>
        /// This goal metric is being used by one or more goals and cannot be deleted.
        /// </summary>
        CannotDeleteMetricWithGoals = -2147203072,

        /// <summary>
        /// Only a leaf statement can be deleted. This statement is parenting some other statement.
        /// </summary>
        CannotDeleteNonLeafNode = -2147159552,

        /// <summary>
        /// You cannot delete a dynamic property that is inherited from a product family.
        /// </summary>
        CannotDeleteNotOwnedDynamicProperty = -2146955258,

        /// <summary>
        /// You can’t delete this file because it contains links to OneNote notebook sections. To delete notebook contents, open the notebook in OneNote and delete the contents from there. To delete a notebook, open SharePoint and delete the notebook from there.
        /// </summary>
        CannotDeleteOneNoteTableOfContent = -2147088201,

        /// <summary>
        /// You can’t delete this record because it doesn’t exist in the offline mode.
        /// </summary>
        CannotDeleteOnlineRecord = -2147093944,

        /// <summary>
        /// The selected OptionSet cannot be deleted
        /// </summary>
        CannotDeleteOptionSet = -2147187708,

        /// <summary>
        /// You can't cancel or delete this system job because it's required by the system. You can only pause, resume, or postpone this job.
        /// </summary>
        CannotDeleteOrCancelSystemJobs = -2147201278,

        /// <summary>
        /// You can't delete this property because it's overridden for one more or child records. Remove the overridden versions of the property, republish the product family hierarchy, and then try deleting the property.
        /// </summary>
        CannotDeleteOverriddenProperty = -2146955008,

        /// <summary>
        /// Can not delete partner solution as one or more organizations are associated with it
        /// </summary>
        CannotDeletePartnerSolutionWithOrganizations = -2147180274,

        /// <summary>
        /// Can not delete partner as one or more solutions are associated with it
        /// </summary>
        CannotDeletePartnerWithPartnerSolutions = -2147180275,

        /// <summary>
        /// The Primary UI Attribute is not valid for deletion
        /// </summary>
        CannotDeletePrimaryUIAttribute = -2147192830,

        /// <summary>
        /// You can't remove products from a bundle that's either active or under revision.
        /// </summary>
        CannotDeleteProductFromActiveBundle = -2147086320,

        /// <summary>
        /// You can't delete a system-generated status reason.
        /// </summary>
        CannotDeleteProductStatusCode = -2146955242,

        /// <summary>
        /// You can't delete this channel access profile because it's associated to an external party item. Remove the association, and then try again.
        /// </summary>
        CannotDeleteProfileWithExternalPartyItem = -2147086073,

        /// <summary>
        /// You can't delete this channel access profile because it's being used by one or more channel access profile rules. Remove this profile from the channel access profile rules, and then try again.
        /// </summary>
        CannotDeleteProfileWithProfileRules = -2147086074,

        /// <summary>
        /// You can't delete this property because it's overridden in one or more related bundle products. Remove the overridden versions of the property from the related bundle products, publish the bundles that were changed, and then try again.
        /// </summary>
        CannotDeletePropertyOverriddenByBundleItem = -2146955243,

        /// <summary>
        /// You can't delete this queue because it has items assigned to it. Assign these items to another user, team, or queue and try again.
        /// </summary>
        CannotDeleteQueueWithQueueItems = -2140991209,

        /// <summary>
        /// You can't delete this queue because one or more routing rule sets use this queue. Remove the queue from the routing rule sets and try again.
        /// </summary>
        CannotDeleteQueueWithRouteRules = -2139942632,

        /// <summary>
        /// The SLA record couldn't be deleted. Please try again or contact your system administrator
        /// </summary>
        CannotDeleteRelatedSla = -2147157927,

        /// <summary>
        /// Attempting to delete a restricted publisher.
        /// </summary>
        CannotDeleteRestrictedPublisher = -2147160058,

        /// <summary>
        /// Attempting to delete a restricted solution.
        /// </summary>
        CannotDeleteRestrictedSolution = -2147160059,

        /// <summary>
        /// The Support User Role cannot be deleted.
        /// </summary>
        CannotDeleteSupportUser = -2147214014,

        /// <summary>
        /// The System Administrator Role cannot be deleted.
        /// </summary>
        CannotDeleteSysAdmin = -2147214034,

        /// <summary>
        /// The System Customizer Role cannot be deleted.
        /// </summary>
        CannotDeleteSystemCustomizer = -2147214006,

        /// <summary>
        /// System e-mail templates cannot be deleted.
        /// </summary>
        CannotDeleteSystemEmailTemplate = -2147187662,

        /// <summary>
        /// System forms cannot be deleted.
        /// </summary>
        CannotDeleteSystemForm = -2147158446,

        /// <summary>
        /// You can't delete system themes.
        /// </summary>
        CannotDeleteSystemTheme = -2147088166,

        /// <summary>
        /// Can't delete a team which owns records. Reassign the records and try again.
        /// </summary>
        CannotDeleteTeamOwningRecords = -2147187954,

        /// <summary>
        /// The duplicate detection rule is currently in use and cannot be updated or deleted. Please try again later.
        /// </summary>
        CannotDeleteUpdateInUseRule = -2147187672,

        /// <summary>
        /// The mailbox associated to a user or a queue cannot be deleted.
        /// </summary>
        CannotDeleteUserMailbox = -2147098112,

        /// <summary>
        /// You can’t delete an active mobile offline profile. Remove all users from the profile and try again.
        /// </summary>
        CannotDeleteUserProfile = -2147087965,

        /// <summary>
        /// The SDK request could not be deserialized.
        /// </summary>
        CannotDeserializeRequest = -2147204753,

        /// <summary>
        /// Workflow instance cannot be deserialized. A possible reason for this failure is a workflow referencing a custom activity that has been unregistered.
        /// </summary>
        CannotDeserializeWorkflowInstance = -2147200993,

        /// <summary>
        /// Xaml representing workflow cannot be deserialized into a DynamicActivity.
        /// </summary>
        CannotDeserializeXamlWorkflow = -2147200992,

        /// <summary>
        /// You cannot disable the auto create access team setting while there are associated team templates.
        /// </summary>
        CannotDisableAutoCreateAccessTeams = -2147187912,

        /// <summary>
        /// Duplicate detection cannot be disabled because a duplicate detection job is currently in progress. Try again later.
        /// </summary>
        CannotDisableDuplicateDetection = -2147187614,

        /// <summary>
        /// You cannot disable the Internet Marketing Partner user. This user does not consume a user license and is not charged to your organization.
        /// </summary>
        CannotDisableInternetMarketingUser = -2147200973,

        /// <summary>
        /// You cannot disable Mobile Offline flag for this entity as it is being used in Mobile Offline Profiles
        /// </summary>
        CannotDisableMobileOfflineFlagForEntity = -2147087963,

        /// <summary>
        /// You can’t disable mobile offline for the {0} entity using solution import. If you don’t want to use this entity in offline mode, uncheck the ‘Enable for Mobile Offline’ flag from the customization screen
        /// </summary>
        CannotDisableMobileOfflineFlagForImportEntity = -2147020527,

        /// <summary>
        /// This position can’t be deleted until all associated users are removed from this position.
        /// </summary>
        CannotDisableOrDeletePositionDueToAssociatedUsers = -2147187901,

        /// <summary>
        /// The {0} entity is currently syncing to an external search index.  You must remove the entity from the external search index before you can set the "Can Enable Sync to External Search Index" property to False.
        /// </summary>
        CannotDisableRelevanceSearchManagedProperty = -2147089661,

        /// <summary>
        /// A user cannot be disabled if they are the only user that has the System Administrator Role.
        /// </summary>
        CannotDisableSysAdmin = -2147214033,

        /// <summary>
        /// Users who are granted the Microsoft Office 365 Global administrator or Service administrator role cannot be disabled in Microsoft Dynamics 365 Online. You must first remove the Microsoft Office 365 role, and then try again.
        /// </summary>
        CannotDisableTenantAdmin = -2147213979,

        /// <summary>
        /// You can not edit an active routing rule. Deactivate the rule to delete it.
        /// </summary>
        CannotEditActiveRule = -2147157935,

        /// <summary>
        /// You can't delete active SLA .Please deactivate the SLA to delete or Contact your system administrator.
        /// </summary>
        CannotEditActiveSla = -2147157920,

        /// <summary>
        /// Duplicate detection cannot be enabled because one or more rules are being published.
        /// </summary>
        CannotEnableDuplicateDetection = -2147187679,

        /// <summary>
        /// Entity {0} can’t be enabled for relevance search because of the configuration of its managed properties.
        /// </summary>
        CannotEnableEntityForRelevanceSearch = -2147089662,

        /// <summary>
        /// Cannot exceed synchronization filter limit.
        /// </summary>
        CannotExceedFilterLimit = -2147220868,

        /// <summary>
        /// HTTPS protocol is required for this type of request, please enable HTTPS protocol and try again.
        /// </summary>
        CannotExecuteRequestBecauseHttpsIsRequired = -2147187412,

        /// <summary>
        /// Invalid domain account
        /// </summary>
        CannotFindDomainAccount = -2147204286,

        /// <summary>
        /// The object was not found in the given queue
        /// </summary>
        CannotFindObjectInQueue = -2147220245,

        /// <summary>
        /// Cannot find user queue
        /// </summary>
        CannotFindUserQueue = -2147220244,

        /// <summary>
        /// Can't follow inactive record.
        /// </summary>
        CannotFollowInactiveEntity = -2147158365,

        /// <summary>
        /// Cannot grant access to address book filters
        /// </summary>
        CannotGrantAccessToAddressBookFilters = -2147187642,

        /// <summary>
        /// Cannot grant access to offline filters
        /// </summary>
        CannotGrantAccessToOfflineFilters = -2147220879,

        /// <summary>
        /// Cannot grant access to outlook filters
        /// </summary>
        CannotGrantAccessToOutlookFilters = -2147220888,

        /// <summary>
        /// One attribute can be tied to only one yomi at a time
        /// </summary>
        CannotHaveDuplicateYomi = -2147192808,

        /// <summary>
        /// Cannot have multiple default synchronization templates for a single entity.
        /// </summary>
        CannotHaveMultipleDefaultFilterTemplates = -2147220867,

        /// <summary>
        /// The base language translation string present in worksheet {0}, row {1}, column {2} is null.
        /// </summary>
        CannotImportNullStringsForBaseLanguage = -2147204538,

        /// <summary>
        /// An invitation cannot be sent to a disabled user
        /// </summary>
        CannotInviteDisabledUser = -2147167726,

        /// <summary>
        /// A record required by this workflow job could not be found.
        /// </summary>
        CannotLocateRecordForWorkflowActivity = -2147200975,

        /// <summary>
        /// A user cannot be made a read only user if they are the last non read only user that has the System Administrator Role.
        /// </summary>
        CannotMakeReadOnlyUser = -2147214024,

        /// <summary>
        /// You cannot make yourself a read only user
        /// </summary>
        CannotMakeSelfReadOnlyUser = -2147214023,

        /// <summary>
        /// The merge couldn't be performed. One or more of the selected cases couldn't be cancelled because of the status transition rules that are defined for cases.
        /// </summary>
        CannotMergeCase = -2147158953,

        /// <summary>
        /// Cannot modify access for address book filters
        /// </summary>
        CannotModifyAccessToAddressBookFilters = -2147187643,

        /// <summary>
        /// Cannot modify access for offline filters
        /// </summary>
        CannotModifyAccessToOfflineFilters = -2147220878,

        /// <summary>
        /// Cannot modify access for outlook filters
        /// </summary>
        CannotModifyAccessToOutlookFilters = -2147220887,

        /// <summary>
        /// The corresponding record in Microsoft Dynamics 365 has more recent data, so this record was ignored.
        /// </summary>
        CannotModifyOldDataFromImport = -2147220618,

        /// <summary>
        /// Cannot modify solution because it has the following patches: {0}.
        /// </summary>
        CannotModifyPatchedSolution = -2147187400,

        /// <summary>
        /// Managed solution cannot update reports which are not present in solution package.
        /// </summary>
        CannotModifyReportOutsideSolutionIfManaged = -2147160008,

        /// <summary>
        /// Rollup field {0} created this field. It can’t be modified directly.
        /// </summary>
        CannotModifyRollupDependentField = -2147089067,

        /// <summary>
        /// No modifications to the 'SYSTEM' or 'INTEGRATION' user are permitted.
        /// </summary>
        CannotModifySpecialUser = -2147214029,

        /// <summary>
        /// The Support User Role cannot be modified.
        /// </summary>
        CannotModifySupportUser = -2147214013,

        /// <summary>
        /// The System Administrator Role cannot be modified.
        /// </summary>
        CannotModifySysAdmin = -2147214031,

        /// <summary>
        /// You can't override a property that isn't inherited from a product family.
        /// </summary>
        CannotOverrideOwnedDynamicProperty = -2146955259,

        /// <summary>
        /// You can't override this property as another overriden version of this property already exists. Please delete the previously overridden version, and then try again.
        /// </summary>
        CannotOverrideProperty = -2147157881,

        /// <summary>
        /// You can't override a property that belongs to a different product hierarchy.
        /// </summary>
        CannotOverridePropertyFromDifferentHierarchy = -2147157740,

        /// <summary>
        /// A managed solution cannot overwrite the {0} component {1} with Id={2} which has an unmanaged base instance.  The most likely scenario for this error is that an unmanaged solution has installed a new unmanaged {0} component on the target system, and now a managed solution from the same publisher is trying to install that same {0} component as managed.  This will cause an invalid layering of solutions on the target system and is not allowed.
        /// </summary>
        CannotOverwriteActiveComponent = -2147160042,

        /// <summary>
        /// You can't overwrite this property as another overwritten version of this property already exists. Please delete the previously overwritten version, and then try again.
        /// </summary>
        CannotOverwriteProperty = -2147086282,

        /// <summary>
        /// The invoice cannot be paid because it is not in active state.
        /// </summary>
        CannotPayNonActiveInvoice = -2146435072,

        /// <summary>
        /// Cannot execute (distribute) a CampaignActivity for a template Campaign.
        /// </summary>
        CannotPropagateCamapaignActivityForTemplate = -2147220719,

        /// <summary>
        /// Can not provision partner solution as it is either already provisioned or going through provisioning.
        /// </summary>
        CannotProvisionPartnerSolution = -2147180273,

        /// <summary>
        /// We can’t publish the app because it has validation errors.
        /// </summary>
        CannotPublishAppModule = -2147155692,

        /// <summary>
        /// You can't publish this bundle because its associated products are in a draft state, are retired, or are being revised.
        /// </summary>
        CannotPublishBundleWithProductStateDraftOrRetire = -2147157753,

        /// <summary>
        /// You can't publish this record because it belongs to a product family that isn't published.
        /// </summary>
        CannotPublishChildOfNonActiveProductFamily = -2147157751,

        /// <summary>
        /// No criteria have been specified. Add criteria, and then publish the duplicate detection rule.
        /// </summary>
        CannotPublishEmptyRule = -2147187692,

        /// <summary>
        /// The selected duplicate detection rule is marked as Inactive. Before publishing, you must activate the rule.
        /// </summary>
        CannotPublishInactiveRule = -2147187693,

        /// <summary>
        /// You can't publish this kit because its associated products are in a draft state, are retired, or are being revised.
        /// </summary>
        CannotPublishKitWithProductStateDraftOrRetire = -2147157738,

        /// <summary>
        /// The selected record type already has the maximum number of published rules. Unpublish or delete existing rules for this record type, and then try again.
        /// </summary>
        CannotPublishMoreRules = -2147187687,

        /// <summary>
        /// You can't publish a bundle that contains bundles. Remove any bundles from this one, and then try to publish again.
        /// </summary>
        CannotPublishNestedBundle = -2147086319,

        /// <summary>
        /// You can't qualify this lead because you don't have permission to create accounts. Work with your system administrator to create the account and then try again.
        /// </summary>
        CannotQualifyLead = -2146955240,

        /// <summary>
        /// Invalid query on base table.  Aggregates cannot be included in base table query.
        /// </summary>
        CannotQueryBaseTableWithAggregates = -2147160051,

        /// <summary>
        /// Specified Object Type not supported
        /// </summary>
        CannotRelateObjectTypeToCampaign = -2147220729,

        /// <summary>
        /// Specified Object Type not supported
        /// </summary>
        CannotRelateObjectTypeToCampaignActivity = -2147220723,

        /// <summary>
        /// A Solution Component cannot be removed from the Default Solution.
        /// </summary>
        CannotRemoveComponentFromDefaultSolution = -2147160064,

        /// <summary>
        /// Cannot find solution component {0} {1} in solution {2}.
        /// </summary>
        CannotRemoveComponentFromSolution = -2147160031,

        /// <summary>
        /// A Solution Component cannot be removed from the System Solution.
        /// </summary>
        CannotRemoveComponentFromSystemSolution = -2147160011,

        /// <summary>
        /// A user cannot be removed from the Support User Role.
        /// </summary>
        CannotRemoveFromSupportUser = -2147214011,

        /// <summary>
        /// A user cannot be removed from the System Administrator Role if they are the only user that has the role.
        /// </summary>
        CannotRemoveFromSysAdmin = -2147214032,

        /// <summary>
        /// Can't remove members from the default business unit team.
        /// </summary>
        CannotRemoveMembersFromDefaultTeam = -2147187956,

        /// <summary>
        /// Specified Item not a member of the specified List.
        /// </summary>
        CannotRemoveNonListMember = -2147187624,

        /// <summary>
        /// You can't remove this product from the pricelist because one or more bundles refer to it.
        /// </summary>
        CannotRemoveProductFromPricelist = -2147086328,

        /// <summary>
        /// The Sys Admin Profile cannot be removed from a user with a Sys Admin Role
        /// </summary>
        CannotRemoveSysAdminProfileFromSysAdminUser = -2147158779,

        /// <summary>
        /// Users who are granted the Microsoft Office 365 Global administrator or Service administrator role cannot be removed from the Microsoft Dynamics 365 System Administrator security role. You must first remove the Microsoft Office 365 role, and then try again.
        /// </summary>
        CannotRemoveTenantAdminFromSysAdminRole = -2147213980,

        /// <summary>
        /// Appointments cannot be reset to draft.
        /// </summary>
        CannotResetAppointmentsToDraft = -2147220886,

        /// <summary>
        /// An invitation cannot be reset for a user if they are the only user that has the System Administrator Role.
        /// </summary>
        CannotResetSysAdminInvite = -2147167724,

        /// <summary>
        /// You can't retire this product because it belongs to active bundles or price lists. Remove it from any bundles or price lists before you retire it.
        /// </summary>
        CannotRetireProduct = -2147157739,

        /// <summary>
        /// This product cannot be retired because it is a part of some active bundles or pricelists. Please remove it from all bundles or pricelists before retiring.
        /// </summary>
        CannotRetireProductFromActiveBundle = -2147157609,

        /// <summary>
        /// Cannot revoke access for address book filters
        /// </summary>
        CannotRevokeAccessToAddressBookFilters = -2147187644,

        /// <summary>
        /// Cannot revoke access for offline filters
        /// </summary>
        CannotRevokeAccessToOfflineFilters = -2147220877,

        /// <summary>
        /// Cannot revoke access for outlook filters
        /// </summary>
        CannotRevokeAccessToOutlookFilters = -2147220880,

        /// <summary>
        /// You can't route a queue item that has been deactivated.
        /// </summary>
        CannotRouteInactiveQueueItem = -2147220185,

        /// <summary>
        /// This private queue item can't be assigned To the selected User.
        /// </summary>
        CannotRoutePrivateQueueItemNonmember = -2140991199,

        /// <summary>
        /// This item cannot be routed to a non-queue member.
        /// </summary>
        CannotRouteToNonQueueMember = -2139942631,

        /// <summary>
        /// Cannot route to Work in progress queue
        /// </summary>
        CannotRouteToQueue = -2147220246,

        /// <summary>
        /// The queue item cannot be routed to the same queue
        /// </summary>
        CannotRouteToSameQueue = -2147220197,

        /// <summary>
        /// The field '{0}' is not securable
        /// </summary>
        CannotSecureAttribute = -2147158783,

        /// <summary>
        /// The field {0} is not securable as it is part of entity keys ( {1} ). Please remove the field from all entity keys to make it securable.
        /// </summary>
        CannotSecureEntityKeyAttribute = -2147088234,

        /// <summary>
        /// Attempting to  select a readonly publisher for solution.
        /// </summary>
        CannotSelectReadOnlyPublisher = -2147160012,

        /// <summary>
        /// An invitation cannot be sent because there are multiple users with this WLID.
        /// </summary>
        CannotSendInviteToDuplicateWindowsLiveId = -2147167723,

        /// <summary>
        /// You do not have the permissions to set this case to an on hold status type. Please contact your system administrator.
        /// </summary>
        CannotSetCaseOnHold = -2147135488,

        /// <summary>
        /// You do not have appropriate privileges to specify whether entitlement terms can be decremented for this case record.
        /// </summary>
        CannotSetEntitlementTermsDecrementBehavior = -2147088303,

        /// <summary>
        /// You don’t have permission to put this record on hold. Contact your system administrator.
        /// </summary>
        CannotSetEntityOnHold = -2147135484,

        /// <summary>
        /// Inactive views cannot be set as default view.
        /// </summary>
        CannotSetInactiveViewAsDefault = -2147220869,

        /// <summary>
        /// The default business unit team parent can't be set.
        /// </summary>
        CannotSetParentDefaultTeam = -2147187960,

        /// <summary>
        /// You can only select a product family as the parent.
        /// </summary>
        CannotSetProductAsParent = -2147157608,

        /// <summary>
        /// You can't set a published or retired product record to the draft state.
        /// </summary>
        CannotSetPublishRetiredProductsToDraft = -2147086283,

        /// <summary>
        /// System attributes ({0}) cannot be set outside of installation or upgrade.
        /// </summary>
        CannotSetSolutionSystemAttributes = -2147160056,

        /// <summary>
        /// You cannot change the Windows Live ID for the Internet Marketing Partner user. This user does not consume a user license and is not charged to your organization.
        /// </summary>
        CannotSetWindowsLiveIdForInternetMarketingUser = -2147200972,

        /// <summary>
        /// You can't share or unshare a record with a system-generated access team.
        /// </summary>
        CannotShareSystemManagedTeam = -2147187911,

        /// <summary>
        /// An item cannot be shared with the owning user.
        /// </summary>
        CannotShareWithOwner = -2147220972,

        /// <summary>
        /// Cannot specify an attendee for appointment distribution.
        /// </summary>
        CannotSpecifyAttendeeForAppointmentPropagation = -2147220708,

        /// <summary>
        /// You cannot set both 'productid' and 'productdescription' for the same record.
        /// </summary>
        CannotSpecifyBothProductAndProductDesc = -2147206405,

        /// <summary>
        /// You cannot set both 'uomid' and 'productdescription' for the same record.
        /// </summary>
        CannotSpecifyBothUomAndProductDesc = -2147206406,

        /// <summary>
        /// Cannot specify communication attribute on activity for distribution
        /// </summary>
        CannotSpecifyCommunicationAttributeOnActivityForPropagation = -2147220706,

        /// <summary>
        /// Cannot specify an organizer for appointment distribution
        /// </summary>
        CannotSpecifyOrganizerForAppointmentPropagation = -2147220710,

        /// <summary>
        /// Cannot specify owner on activity for distribution
        /// </summary>
        CannotSpecifyOwnerForActivityPropagation = -2147220697,

        /// <summary>
        /// Cannot specify a recipient for activity distribution.
        /// </summary>
        CannotSpecifyRecipientForActivityPropagation = -2147220707,

        /// <summary>
        /// Cannot specify a sender for appointment distribution
        /// </summary>
        CannotSpecifySenderForActivityPropagation = -2147220709,

        /// <summary>
        /// Attempting to undelete a label that is not marked as delete.
        /// </summary>
        CannotUndeleteLabel = -2147160061,

        /// <summary>
        /// This solution cannot be uninstalled because the '{0}' with id '{1}'  is required by the '{2}' solution. Uninstall the {2} solution and try again.
        /// </summary>
        CannotUninstallReferencedProtectedSolution = -2147160032,

        /// <summary>
        /// Solution dependencies exist, cannot uninstall.
        /// </summary>
        CannotUninstallWithDependencies = -2147160035,

        /// <summary>
        /// Cannot update property "{0}" on a published Modern Flow process.
        /// </summary>
        CannotUpdateActiveModernFlow = -2147089306,

        /// <summary>
        /// The default value for a status (statecode) attribute cannot be updated.
        /// </summary>
        CannotUpdateAppDefaultValueForStateAttribute = -2147204285,

        /// <summary>
        /// The default value for a status reason (statuscode) attribute is not used. The default status reason is set in the associated status (statecode) attribute option.
        /// </summary>
        CannotUpdateAppDefaultValueForStatusAttribute = -2147204284,

        /// <summary>
        /// Can’t change the client type of this app.
        /// </summary>
        CannotUpdateAppModuleClientType = -2147155672,

        /// <summary>
        /// You can’t change the unique name .
        /// </summary>
        CannotUpdateAppModuleUniqueName = -2147155687,

        /// <summary>
        /// The property AzureActiveDirectoryObjectId cannot be modified.
        /// </summary>
        CannotUpdateAzureActiveDirectoryObjectIdField = -2147214001,

        /// <summary>
        /// The object cannot be updated because it is read-only.
        /// </summary>
        CannotUpdateBecauseItIsReadOnly = -2147220946,

        /// <summary>
        /// Parent campaign is not updatable.
        /// </summary>
        CannotUpdateCampaignForCampaignActivity = -2147220725,

        /// <summary>
        /// Parent campaign is not updatable.
        /// </summary>
        CannotUpdateCampaignForCampaignResponse = -2147220724,

        /// <summary>
        /// This item is deactivated. To work with this item, reactivate it and then try again.
        /// </summary>
        CannotUpdateDeactivatedQueueItem = -2147220195,

        /// <summary>
        /// You can’t update the isdefaultTheme attribute.
        /// </summary>
        CannotUpdateDefaultField = -2147088168,

        /// <summary>
        /// Default solution attribute{0} {1} can only be set on installation or upgrade.  The value{0} cannot be modified.
        /// </summary>
        CannotUpdateDefaultSolution = -2147160055,

        /// <summary>
        /// We can’t update the delay send time because the email is not a draft or isn’t scheduled to be sent.
        /// </summary>
        CannotUpdateDelaySendTimeForEmailWhenEmailIsNotInProperState = -2147155936,

        /// <summary>
        /// We can’t update the delay send time because Email Engagement isn’t turned on for the organization.
        /// </summary>
        CannotUpdateDelaySendTimeWhenEEFeatureNotEnabled = -2147155935,

        /// <summary>
        /// You can Only update draft products.
        /// </summary>
        CannotUpdateDraftProducts = -2147157643,

        /// <summary>
        /// We can’t update email statistics because the email isn’t being followed.
        /// </summary>
        CannotUpdateEmailStatisticForEmailNotFollowed = -2147155966,

        /// <summary>
        /// We can’t update email statistics because the email hasn’t been sent.
        /// </summary>
        CannotUpdateEmailStatisticForEmailNotSent = -2147155967,

        /// <summary>
        /// We can’t update email statistics because Email Engagement isn’t turned on for the organization.
        /// </summary>
        CannotUpdateEmailStatisticWhenEEFeatureNotEnabled = -2147155944,

        /// <summary>
        /// You can only set Active entitlement records as default.
        /// </summary>
        CannotUpdateEntitlement = -2147088302,

        /// <summary>
        /// EntitySetName cannot be updated for OOB entities
        /// </summary>
        CannotUpdateEntitySetName = -2147158429,

        /// <summary>
        /// An external party record already exists with the same correlation key value.
        /// </summary>
        CannotUpdateExternalPartyWithSameCorrelationKey = -2147086060,

        /// <summary>
        /// You cannot update goal period related attributes on a child goal.
        /// </summary>
        CannotUpdateGoalPeriodInfoChildGoal = -2147202815,

        /// <summary>
        /// You cannot change the time period of this goal because there are one or more closed subordinate goals.
        /// </summary>
        CannotUpdateGoalPeriodInfoClosedGoal = -2147202800,

        /// <summary>
        /// Cannot update solution '{0}' because it is a managed solution.
        /// </summary>
        CannotUpdateManagedSolution = -2147160028,

        /// <summary>
        /// You cannot update metric on a child goal.
        /// </summary>
        CannotUpdateMetricOnChildGoal = -2147202816,

        /// <summary>
        /// You cannot update metric on a goal which has associated child goals.
        /// </summary>
        CannotUpdateMetricOnGoalWithChildren = -2147202814,

        /// <summary>
        /// The changes made to this record cannot be saved because this goal metric is being used by one or more goals.
        /// </summary>
        CannotUpdateMetricWithGoals = -2147203069,

        /// <summary>
        /// The default business unit team name can't be updated.
        /// </summary>
        CannotUpdateNameDefaultTeam = -2147187958,

        /// <summary>
        /// The translation string present in worksheet {0}, row {1}, column {2} is not customizable.
        /// </summary>
        CannotUpdateNonCustomizableString = -2147204537,

        /// <summary>
        /// The object cannot be updated because it is inactive.
        /// </summary>
        CannotUpdateObjectBecauseItIsInactive = -2147220944,

        /// <summary>
        /// The currency cannot be changed because this opportunity has Products Quotes, and/ or Orders associated with it.  If you want to change the currency please delete all of the Products/quotes/orders and then change the currency or create a new opportunity with the appropriate currency.
        /// </summary>
        CannotUpdateOpportunityCurrency = -2147187591,

        /// <summary>
        /// Organization Settings stored in Organization Database cannot be set when offline.
        /// </summary>
        CannotUpdateOrgDBOrgSettingWhenOffline = -2147187435,

        /// <summary>
        /// Cannot update metric or period attributes when parent is being updated.
        /// </summary>
        CannotUpdateParentAndDependents = -2147203060,

        /// <summary>
        /// The private or WIP Bin queue is not allowed to be updated or deleted
        /// </summary>
        CannotUpdatePrivateOrWIPQueue = -2147220242,

        /// <summary>
        /// The currency of the product cannot be updated because there are associated price list items with pricing method percentage.
        /// </summary>
        CannotUpdateProductCurrency = -2147185414,

        /// <summary>
        /// The currency cannot be changed because this quote has Products associated with it. If you want to change the currency please delete all of the Products and then change the currency or create a new quote with the appropriate currency.
        /// </summary>
        CannotUpdateQuoteCurrency = -2147203058,

        /// <summary>
        /// Attempting to update a readonly publisher.
        /// </summary>
        CannotUpdateReadOnlyPublisher = -2147160013,

        /// <summary>
        /// Restricted publisher ({0}) cannot be updated.
        /// </summary>
        CannotUpdateRestrictedPublisher = -2147160041,

        /// <summary>
        /// Restricted solution ({0}) cannot be updated.
        /// </summary>
        CannotUpdateRestrictedSolution = -2147160054,

        /// <summary>
        /// The changes made to the roll-up field definition cannot be saved because the related goal metric is being used by one or more closed goals.
        /// </summary>
        CannotUpdateRollupAttributeWithClosedGoals = -2147203071,

        /// <summary>
        /// You cannot write on rollup fields if isoverride is not set to true in your create/update request.
        /// </summary>
        CannotUpdateRollupFields = -2147202799,

        /// <summary>
        /// Solution patch with version {0} already exists. Updating patch is not supported.
        /// </summary>
        CannotUpdateSolutionPatch = -2147159998,

        /// <summary>
        /// Cannot update the Support User Role.
        /// </summary>
        CannotUpdateSupportUser = -2147214009,

        /// <summary>
        /// The property IsLicensed cannot be modified.
        /// </summary>
        CannotUpdateSyncUserIsLicensedField = -2147214004,

        /// <summary>
        /// The property IsSyncUserWithDirectory cannot be modified.
        /// </summary>
        CannotUpdateSyncUserIsSyncWithDirectoryField = -2147214002,

        /// <summary>
        /// System entity icons cannot be updated.
        /// </summary>
        CannotUpdateSystemEntityIcons = -2147158445,

        /// <summary>
        /// You can’t modify system themes.
        /// </summary>
        CannotUpdateSystemTheme = -2147088170,

        /// <summary>
        /// We can’t update the template because the email has already been sent or is not in a Draft state.
        /// </summary>
        CannotUpdateTemplateIdForEmailInNonDraftState = -2147155945,

        /// <summary>
        /// A trigger cannot be added/deleted/updated for a published rule.
        /// </summary>
        CannotUpdateTriggerForPublishedRules = -2147090430,

        /// <summary>
        /// The component that you are trying to update has been deleted.
        /// </summary>
        CannotUpdateUnpublishedDeleteInstance = -2147160049,

        /// <summary>
        /// This message can not be used to set the state of opportunity to {0}. In order to set state of opportunity to {1}, use the {1} message instead.
        /// </summary>
        CannotUseOpportunitySetStateMessage = -2146435067,

        /// <summary>
        /// Email connector cannot use the credentials specified in the mailbox entity. This might be because user has disallowed it. Please use other mode of credential retrieval or allow the use of credential by email connector.
        /// </summary>
        CannotUseUserCredentials = -2147098071,

        /// <summary>
        /// You can only set product families in a draft or active state as parent.
        /// </summary>
        CanOnlySetActiveOrDraftProductFamilyAsParent = -2147157754,

        /// <summary>
        /// You can't save this record while you're offline.
        /// </summary>
        CantSaveRecordInOffline = -2147093996,

        /// <summary>
        /// You can’t set or change the value of the IsGuestProfile field because it’s for internal use only.
        /// </summary>
        CantSetIsGuestProfile = -2147086054,

        /// <summary>
        /// You can’t update this record because it doesn’t exist in the offline mode.
        /// </summary>
        CantUpdateOnlineRecord = -2147093980,

        /// <summary>
        /// The solution specified an expected assets file but that file was missing or invalid.
        /// </summary>
        CanvasAppsExpectedFileMissing = -2147015850,

        /// <summary>
        /// The request to import a canvas app should contain at least one asset file.
        /// </summary>
        CanvasAppsInvalidSolutionFileContent = -2147015852,

        /// <summary>
        /// Creation and editing of Canvas Apps is not enabled.
        /// </summary>
        CanvasAppsNotEnabled = -2147015855,

        /// <summary>
        /// The request to the PowerApps service failed with a client failure.
        /// </summary>
        CanvasAppsServiceRequestClientFailure = -2147015854,

        /// <summary>
        /// The request to the PowerApps service failed with a server failure.
        /// </summary>
        CanvasAppsServiceRequestServerFailure = -2147015853,

        /// <summary>
        /// The request to the PowerApps service resulted in a new canvasappid when the previously existing value was expected.
        /// </summary>
        CanvasAppsUnexpectedCanvasAppId = -2147015851,

        /// <summary>
        /// The latest published version of the canvas app does not match the version known by the Dynamics service.
        /// </summary>
        CanvasAppVersionDoesNotMatchLatestPublishedVersion = -2147015848,

        /// <summary>
        /// The app version of the canvas app was not set or was an invalid value.
        /// </summary>
        CanvasAppVersionMissingOrInvalid = -2147015849,

        /// <summary>
        /// The user is in an admin restricted location.
        /// </summary>
        CAPolicyValidationFailedLateBind = -2147015327,

        /// <summary>
        /// Object is not allowed to be deleted
        /// </summary>
        CascadeDeleteNotAllowDelete = -2147188477,

        /// <summary>
        /// Failed to create unmanaged data access wrapper
        /// </summary>
        CascadeFailToCreateNativeDAWrapper = -2147188472,

        /// <summary>
        /// Invalid Extra-condition value
        /// </summary>
        CascadeInvalidExtraConditionValue = -2147188479,

        /// <summary>
        /// Invalid CascadeLink Type
        /// </summary>
        CascadeInvalidLinkType = -2147188478,

        /// <summary>
        /// Invalid link type for system entity cascading actions.
        /// </summary>
        CascadeInvalidLinkTypeTransition = -2147204779,

        /// <summary>
        /// Invalid Column Name for Merge Special Casing
        /// </summary>
        CascadeMergeInvalidSpecialColumn = -2147188474,

        /// <summary>
        /// Empty Caller Id
        /// </summary>
        CascadeProxyEmptyCallerId = -2147188469,

        /// <summary>
        /// Invalid pointer of unmanaged data access object
        /// </summary>
        CascadeProxyInvalidNativeDAPtr = -2147188471,

        /// <summary>
        /// Invalid security principal type
        /// </summary>
        CascadeProxyInvalidPrincipalType = -2147188470,

        /// <summary>
        /// CascadeDelete is defined as RemoveLink while the foreign key is not nullable
        /// </summary>
        CascadeRemoveLinkOnNonNullable = -2147188476,

        /// <summary>
        /// Cannot perform Cascade Reparent on Non-UserOwned entities
        /// </summary>
        CascadeReparentOnNonUserOwned = -2147188473,

        /// <summary>
        /// This case has already been resolved. Close and reopen the case record to see the updates.
        /// </summary>
        CaseAlreadyResolved = -2147220273,

        /// <summary>
        /// Because of the status transition rules, you can't resolve a case in the current status. Change the case status, and then try resolving it, or contact your system administrator.
        /// </summary>
        CaseStateChangeInvalid = 134242420,

        /// <summary>
        /// The Data Description for the visualization is invalid. The attribute type for the group by of one of the categories is invalid. Correct the Data Description.
        /// </summary>
        CategoryDataTypeInvalid = -2147164134,

        /// <summary>
        /// Category should be set to BusinessProcessFlow while creating business process flow category
        /// </summary>
        CategoryNotSetToBusinessProcessFlow = -2147089404,

        /// <summary>
        /// Dynamics 365 for Outlook is not supported for this organization.
        /// </summary>
        CDSOrgNotSupported = -2147203824,

        /// <summary>
        /// The given certificate cannot be found.
        /// </summary>
        CertificateNotFound = -2147098055,

        /// <summary>
        /// You can not disable change tracking for this entity since mobile offline is already enabled.
        /// </summary>
        ChangeTrackingDisabledForMobileOfflineError = -2147087967,

        /// <summary>
        /// Entity {0} isn't enabled for change tracking.
        /// </summary>
        ChangeTrackingNotEnabledForEntity = -2147015535,

        /// <summary>
        /// Changes cannot be retrieved for intersect entity {0} since both related entities are not enabled for change tracking.
        /// </summary>
        ChangeTrackingNotEnabledForRelatedEntities = -2147015534,

        /// <summary>
        /// You can't deactivate a draft channel access profile rule.
        /// </summary>
        ChannelAccessProfileRuleAlreadyInDraftState = -2147086059,

        /// <summary>
        /// A record for the specified source type already exists. You can't create another one.
        /// </summary>
        ChannelPropertyGroupAlreadyExistsWithSameSourceType = -2147088148,

        /// <summary>
        /// The channel property name is invalid. The name can only contain '_', numerical, and alphabetical characters. Choose a different name, and try again.
        /// </summary>
        ChannelPropertyNameInvalid = -2147088142,

        /// <summary>
        /// Number of chart areas and number of categories should be same.
        /// </summary>
        ChartAreaCategoryMismatch = -2147164155,

        /// <summary>
        /// This chart type is not supported for comparison charts.
        /// </summary>
        ChartTypeNotSupportedForComparisonChart = -2147164136,

        /// <summary>
        /// Series of chart type {0} is not supported for multi-series charts.
        /// </summary>
        ChartTypeNotSupportedForMultipleSeriesChart = -2147164127,

        /// <summary>
        /// Please select an account that is a member of the PrivUserGroup security group and try again.
        /// </summary>
        CheckPrivilegeGroupForUserOnPremiseError = -2147187711,

        /// <summary>
        /// Please select a Dynamics 365 System Administrator account that belongs to the root business unit and try again.
        /// </summary>
        CheckPrivilegeGroupForUserOnSplaError = -2147187712,

        /// <summary>
        /// The child businesss Id is invalid.
        /// </summary>
        ChildBusinessDoesNotExist = -2147214046,

        /// <summary>
        /// The child user Id is invalid.
        /// </summary>
        ChildUserDoesNotExist = -2147214042,

        /// <summary>
        /// This workflow cannot run because one or more child workflows it uses have not been published or have been deleted. Please check the child workflows and try running this workflow again.
        /// </summary>
        ChildWorkflowNotFound = -2147200977,

        /// <summary>
        /// This workflow cannot run because arguments provided by parent workflow does not match with the specified parameters in linked child workflow. Check the child workflow reference in parent workflow and try running this workflow again.
        /// </summary>
        ChildWorkflowParameterMismatch = -2147200952,

        /// <summary>
        /// The solution operation failed due to a circular dependency with other solutions. Please check the exception for more details: {0}
        /// </summary>
        CircularDependency = -2147020457,

        /// <summary>
        /// Authentication was canceled by the user.
        /// </summary>
        ClientAuthCanceled = -2147167708,

        /// <summary>
        /// There is no connectivity.
        /// </summary>
        ClientAuthNoConnectivity = -2147167706,

        /// <summary>
        /// There is no connectivity when running in offline mode.
        /// </summary>
        ClientAuthNoConnectivityOffline = -2147167707,

        /// <summary>
        /// Offline SDK calls must be made in the offline user context.
        /// </summary>
        ClientAuthOfflineInvalidCallerId = -2147167705,

        /// <summary>
        /// The user signed out.
        /// </summary>
        ClientAuthSignedOut = -2147167711,

        /// <summary>
        /// Synchronization between processes failed.
        /// </summary>
        ClientAuthSyncIssue = -2147167709,

        /// <summary>
        /// Your computer's date/time is out of sync with the server by more than 5 minutes.
        /// </summary>
        ClientServerDateTimeMismatch = -2147203837,

        /// <summary>
        /// The Outlook email address and Dynamics 365 user email address do not match.
        /// </summary>
        ClientServerEmailAddressMismatch = -2147203836,

        /// <summary>
        /// There's an update available for Dynamics 365 for Outlook.
        /// </summary>
        ClientUpdateAvailable = -2147167596,

        /// <summary>
        /// This version of Outlook client isn't compatible with your Dynamics 365 organization (current version {0} is too high).
        /// </summary>
        ClientVersionTooHigh = -2147203839,

        /// <summary>
        /// This version of Outlook client isn't compatible with your Dynamics 365 organization (current version {0} is too low).
        /// </summary>
        ClientVersionTooLow = -2147203840,

        /// <summary>
        /// Operation on clone solution failed.
        /// </summary>
        CloneSolutionException = -2147187399,

        /// <summary>
        /// Patch '{0}' has a matching or higher version ({1}) than that of the patch being installed.
        /// </summary>
        CloneSolutionPatchException = -2147084431,

        /// <summary>
        /// A validation error occurred. The length of the Name attribute of the mobileofflineprofile entity exceeded the maximum allowed length of 200.
        /// </summary>
        CloneTitleTooLong = -2147020526,

        /// <summary>
        /// Close active child case before closing parent case.
        /// </summary>
        CloseActiveChildCaseFirst = -2147224494,

        /// <summary>
        /// Color Strip section cannot have more than 1 attribute
        /// </summary>
        ColorStripAttributesExceeded = -2147085056,

        /// <summary>
        /// Color Strip section can only have attributes of type Two Options, Option Set and Status Reason
        /// </summary>
        ColorStripAttributesInvalid = -2147085054,

        /// <summary>
        /// The evaluation of the current component(name={0}, id={1}) in the current operation ({2}) failed during at least one managed property evaluations: {3}
        /// </summary>
        CombinedManagedPropertyFailure = -2147160025,

        /// <summary>
        /// Command is not supported in offline mode.
        /// </summary>
        CommandNotSupported = -2146088110,

        /// <summary>
        /// Communication is blocked due to a socket exception.
        /// </summary>
        CommunicationBlocked = -2147203834,

        /// <summary>
        /// No component definition exists for the component type {0}.
        /// </summary>
        ComponentDefinitionDoesNotExists = -2147160039,

        /// <summary>
        /// The version of the existing record doesn't match the RowVersion property provided.
        /// </summary>
        ConcurrencyVersionMismatch = -2147088254,

        /// <summary>
        /// The RowVersion property must be provided when the value of ConcurrencyBehavior is IfVersionMatches.
        /// </summary>
        ConcurrencyVersionNotProvided = -2147088253,

        /// <summary>
        /// The current {0} operation failed due to another concurrent operation running at the same time. Please try again later.
        /// </summary>
        ConcurrentOperationFailure = -2147020460,

        /// <summary>
        /// Attributes of the condition are not the subset of attributes in the Step, for the Stage : {0}
        /// </summary>
        ConditionAttributesNotAnSubsetOfStepAttributes = -2147089354,

        /// <summary>
        /// Branch condition can contain only SetNextStage as a child.
        /// </summary>
        ConditionBranchDoesHaveSetNextStageOnlyChildInXaml = -2147089356,

        /// <summary>
        /// {0} cannot have more than one {1}.
        /// </summary>
        ConditionStepCountInXamlExceedsMaxAllowed = -2147089355,

        /// <summary>
        /// The default {0} organization cannot be deleted from the MSCRM_CONFIG database.
        /// </summary>
        ConfigDBCannotDeleteDefaultOrganization = -2147167685,

        /// <summary>
        /// Cannot delete '{0}' with Value = ({1}) in this State = ({2}) from MSCRM_CONFIG database
        /// </summary>
        ConfigDBCannotDeleteObjectDueState = -2147167694,

        /// <summary>
        /// Cannot update '{0}' with Value = ({1}) in this State = ({2}) from MSCRM_CONFIG database
        /// </summary>
        ConfigDBCannotUpdateObjectDueState = -2147167689,

        /// <summary>
        /// Cannot delete '{0}' with Value = ({1}) due to child '{2}' references from MSCRM_CONFIG database
        /// </summary>
        ConfigDBCascadeDeleteNotAllowDelete = -2147167693,

        /// <summary>
        /// Duplicate '{0}' with Value = ({1}) exists in MSCRM_CONFIG database
        /// </summary>
        ConfigDBDuplicateRecord = -2147167695,

        /// <summary>
        /// '{0}' with Value = ({1}) does not exist in MSCRM_CONFIG database
        /// </summary>
        ConfigDBObjectDoesNotExist = -2147167696,

        /// <summary>
        /// Description must be specified.
        /// </summary>
        ConfigMissingDescription = -2147204713,

        /// <summary>
        /// Primary Key cannot be nullable.
        /// </summary>
        ConfigNullPrimaryKey = -2147204714,

        /// <summary>
        /// The solution configuration page must exist within the solution it represents.
        /// </summary>
        ConfigurationPageNotValidForSolution = -2147192803,

        /// <summary>
        /// You must configure claims-based authentication before you can configure an Internet-facing deployment.
        /// </summary>
        ConfigureClaimsBeforeIfd = -2147167642,

        /// <summary>
        /// Configured user is different than supplied user.
        /// </summary>
        ConfiguredUserIsDifferentThanSuppliedUser = -2147203832,

        /// <summary>
        /// This record can't be published. One of the properties that was changed for this record conflicts with its inherited version. Remove the conflicting property, and then try again.
        /// </summary>
        ConflictForOverriddenPropertiesEncountered = -2146955248,

        /// <summary>
        /// The service component {0} has conflicting provision types.
        /// </summary>
        ConflictingProvisionTypes = -2147176404,

        /// <summary>
        /// Connections cannot be enabled on this {0} entity with id {1}.
        /// </summary>
        ConnectionCannotBeEnabledOnThisEntity = -2147188204,

        /// <summary>
        /// Connection already exists.
        /// </summary>
        ConnectionExists = -2147188216,

        /// <summary>
        /// Start date / end date is invalid.
        /// </summary>
        ConnectionInvalidStartEndDate = -2147188215,

        /// <summary>
        /// The selected record does not support connections. You cannot add the connection.
        /// </summary>
        ConnectionNotSupported = -2147188205,

        /// <summary>
        /// Both objects being connected are missing.
        /// </summary>
        ConnectionObjectsMissing = -2147188208,

        /// <summary>
        /// The record type {0} is not defined for use with the connection role {1}.
        /// </summary>
        ConnectionRoleNotValidForObjectType = -2147188203,

        /// <summary>
        /// Unable to copy the documents because the network connection timed out.  Please try again later or contact your system administrator.
        /// </summary>
        ConnectionTimeOut = -2147020764,

        /// <summary>
        /// Creation and editing of Connector is not enabled.
        /// </summary>
        ConnectorNotEnabled = -2147015168,

        /// <summary>
        /// Contact does not exist.
        /// </summary>
        ContactDoesNotExist = -2147220221,

        /// <summary>
        /// Creating this parental association would create a loop in Contacts hierarchy.
        /// </summary>
        ContactLoopBeingCreated = -2147220214,

        /// <summary>
        /// Loop exists in the contacts hierarchy.
        /// </summary>
        ContactLoopExists = -2147220215,

        /// <summary>
        /// Error creating or updating Business Process: context cannot be null.
        /// </summary>
        ContextIsNull = -2147089339,

        /// <summary>
        /// The contract's discount type does not support 'percentage' discounts.
        /// </summary>
        ContractDetailDiscountAmount = -2147204077,

        /// <summary>
        /// Both 'amount' and 'percentage' cannot be set.
        /// </summary>
        ContractDetailDiscountAmountAndPercent = -2147204076,

        /// <summary>
        /// The contract's discount type does not support 'amount' discounts.
        /// </summary>
        ContractDetailDiscountPercent = -2147204078,

        /// <summary>
        /// The allotment type code is invalid.
        /// </summary>
        ContractInvalidAllotmentTypeCode = -2147208699,

        /// <summary>
        /// The bill-to address of the contract is invalid.
        /// </summary>
        ContractInvalidBillToAddress = -2147208689,

        /// <summary>
        /// The bill-to customer of the contract is invalid.
        /// </summary>
        ContractInvalidBillToCustomer = -2147208688,

        /// <summary>
        /// The contract is invalid.
        /// </summary>
        ContractInvalidContract = -2147208685,

        /// <summary>
        /// The contract template is invalid.
        /// </summary>
        ContractInvalidContractTemplate = -2147208687,

        /// <summary>
        /// The customer of the contract is invalid.
        /// </summary>
        ContractInvalidCustomer = -2147208691,

        /// <summary>
        /// The start date / end date of this renewed contract can not overlap with any other invoiced / active contracts with the same contract number.
        /// </summary>
        ContractInvalidDatesForRenew = -2147208680,

        /// <summary>
        /// Discount cannot be greater than total price.
        /// </summary>
        ContractInvalidDiscount = -2147204717,

        /// <summary>
        /// The price is invalid.
        /// </summary>
        ContractInvalidPrice = -2147208683,

        /// <summary>
        /// The service address of the contract is invalid.
        /// </summary>
        ContractInvalidServiceAddress = -2147208690,

        /// <summary>
        /// Start date / end date or billing start date / billing end date is invalid.
        /// </summary>
        ContractInvalidStartEndDate = -2147208702,

        /// <summary>
        /// The state of the contract is invalid.
        /// </summary>
        ContractInvalidState = -2147208701,

        /// <summary>
        /// The state of the contract line item is invalid.
        /// </summary>
        ContractLineInvalidState = -2147208700,

        /// <summary>
        /// There are no contract line items for this contract.
        /// </summary>
        ContractNoLineItems = -2147208692,

        /// <summary>
        /// The contract template does not exist.
        /// </summary>
        ContractTemplateDoesNotExist = -2147208698,

        /// <summary>
        /// Abbreviation can not be NULL.
        /// </summary>
        ContractTemplateNoAbbreviation = -2147208693,

        /// <summary>
        /// Control id {0} in the Xaml is not unique
        /// </summary>
        ControlIdIsNotUnique = -2147089391,

        /// <summary>
        /// Control id cannot be null or empty
        /// </summary>
        ControlIdIsNullOrEmpty = -2147015868,

        /// <summary>
        /// An unexpected error occurred while processing the Fetch data set.
        /// </summary>
        ConvertFetchDataSetError = -2147187922,

        /// <summary>
        /// An unexpected error occurred while converting supplied report to Dynamics 365 format.
        /// </summary>
        ConvertReportToCrmError = -2147187923,

        /// <summary>
        /// This Convert Rule Set cannot be activated or deactivated by someone who is not its owner.
        /// </summary>
        ConvertRuleActivateDeactivateByNonOwner = -2147157882,

        /// <summary>
        /// Selected ConvertRule is already in active state. Please select another record and try again
        /// </summary>
        ConvertRuleAlreadyActive = -2147088591,

        /// <summary>
        /// Selected ConvertRule is already in draft state. Please select another record and try again
        /// </summary>
        ConvertRuleAlreadyInDraftState = -2147088590,

        /// <summary>
        /// Select an email template for an autoresponse or set the autoresponse option to No.
        /// </summary>
        ConvertRuleInvalidAutoResponseSettings = -2147157895,

        /// <summary>
        /// You don't have the required permissions on ConvertRules and processes to perform this action.
        /// </summary>
        ConvertRulePermissionToPerformAction = -2147088589,

        /// <summary>
        /// Queue value required. Provide a value for the queue.
        /// </summary>
        ConvertRuleQueueIdMissingForEmailSource = -2147157866,

        /// <summary>
        /// Please select either a global or case template.
        /// </summary>
        ConvertRuleResponseTemplateValidity = -2147088592,

        /// <summary>
        /// An error has occurred while copying files. Please try again later. If the problem persists, contact your system administrator.
        /// </summary>
        CopyGenericError = -2147020761,

        /// <summary>
        /// Duplicating campaigns that have Internet Marketing Campaign Activities is not allowed
        /// </summary>
        CopyNotAllowedForInternetMarketing = -2147187596,

        /// <summary>
        /// The hidden sheet data is corrupted.
        /// </summary>
        CorruptedHiddensheetData = -2147087945,

        /// <summary>
        /// Yammer OAuth token could not be decrypted. Please try to reconfigure Yammer once again.
        /// </summary>
        CouldNotDecryptOAuthToken = -2147094256,

        /// <summary>
        /// Could not find any queue item associated with the Target in the specified SourceQueueId. Either the SourceQueueId or Target is invalid or the queue item does not exist.
        /// </summary>
        CouldNotFindQueueItemInQueue = -2147220188,

        /// <summary>
        /// Database resource lock could not be obtained. For more information, see http://docs.microsoft.com/dynamics365/customer-engagement/customize/best-practices-workflow-processes#limit-the-number-of-workflows-that-update-the-same-entity
        /// </summary>
        CouldNotObtainLockOnResource = -2147204295,

        /// <summary>
        /// The system was not able to read users Yammer access token although a non-empty code was passed.
        /// </summary>
        CouldNotReadAccessToken = -2147094267,

        /// <summary>
        /// Couldn't set location type of document location to OneNote.
        /// </summary>
        CouldNotSetLocationTypeToOneNote = -2147088123,

        /// <summary>
        /// The Data Description for the visualization is invalid as it does not specify an order node for the count attribute.
        /// </summary>
        CountSpecifiedWithoutOrder = -2147164129,

        /// <summary>
        /// An error occurred while saving the {0} property.
        /// </summary>
        CreatePropertyError = -2147157879,

        /// <summary>
        /// An error occurred while saving the {0} property instance.
        /// </summary>
        CreatePropertyInstanceError = -2147157872,

        /// <summary>
        /// Cannot create a workflow dependency for a published workflow definition.
        /// </summary>
        CreatePublishedWorkflowDefinitionWorkflowDependency = -2147201014,

        /// <summary>
        /// Cannot create the recurrence rule.
        /// </summary>
        CreateRecurrenceRuleFailed = -2147163903,

        /// <summary>
        /// Cannot create a workflow dependency associated with a workflow activation.
        /// </summary>
        CreateWorkflowActivationWorkflowDependency = -2147201015,

        /// <summary>
        /// Cannot create a workflow dependency for a published workflow template.
        /// </summary>
        CreateWorkflowDependencyForPublishedTemplate = -2147200999,

        /// <summary>
        /// Crm constraint evaluation error occurred.
        /// </summary>
        CrmConstraintEvaluationError = -2147220895,

        /// <summary>
        /// Crm constraint parsing error occurred.
        /// </summary>
        CrmConstraintParsingError = -2147220894,

        /// <summary>
        /// Crm expression body parsing error occurred.
        /// </summary>
        CrmExpressionBodyParsingError = -2147220898,

        /// <summary>
        /// Crm expression evaluation error occurred.
        /// </summary>
        CrmExpressionEvaluationError = -2147220896,

        /// <summary>
        /// Crm expression parameters parsing error occurred.
        /// </summary>
        CrmExpressionParametersParsingError = -2147220897,

        /// <summary>
        /// Crm expression parsing error occurred.
        /// </summary>
        CrmExpressionParsingError = -2147220899,

        /// <summary>
        /// A failure occurred in Wep Api in Dynamics 365.
        /// </summary>
        CrmHttpError = -2147088246,

        /// <summary>
        /// Error occurred in the Crm AutoReimpersonator.
        /// </summary>
        CrmImpersonationError = -2147220923,

        /// <summary>
        /// Your subscription has the maximum number of user licenses available.  For additional licenses, please contact our sales organization at 1-877-Dynamics 365-CHOICE (276-2464).
        /// </summary>
        CrmLiveAddOnAddLicenseLimitReached = -2147176362,

        /// <summary>
        /// Your storage consumption has reached the maximum storage limit allotted to this environment. Trial environments are allocated with limited resources. If you are not using a trial environment, please contact support.
        /// </summary>
        CrmLiveAddOnAddStorageLimitReached = -2147176361,

        /// <summary>
        /// Due to recent changes you have made to your account, these changes cannot be made at this time.   Close this wizard, and try again later.  If the problem persists, please contact our sales organization at 1-877-Dynamics 365-CHOICE (276-2464).
        /// </summary>
        CrmLiveAddOnDataChanged = -2147176356,

        /// <summary>
        /// Your changes cannot be processed at this time. Your organization is currently being updated.  No changes have been made to your account.  Close this wizard, and try again later.  If the problem persists, please contact our sales organization at 1-877-Dynamics 365-CHOICE (276-2464).
        /// </summary>
        CrmLiveAddOnOrgInNoUpdateMode = -2147176359,

        /// <summary>
        /// Your organization has the minimum amount of storage allowed.  You can remove only storage that has been added to your organization, and  is not being used.
        /// </summary>
        CrmLiveAddOnRemoveStorageLimitReached = -2147176360,

        /// <summary>
        /// There was an error contacting the billing system.  Your request cannot be processed at this time.  No changes have been made to your account.  Close this wizard, and try again later.  If the problem persists, please contact our sales organization at 1-877-Dynamics 365-CHOICE (276-2464).
        /// </summary>
        CrmLiveAddOnUnexpectedError = -2147176363,

        /// <summary>
        /// External Message Provider could not be located for queue item type of: {0}.
        /// </summary>
        CrmLiveCannotFindExternalMessageProvider = -2147176367,

        /// <summary>
        /// Domain already exists in the DNS table.
        /// </summary>
        CrmLiveDnsDomainAlreadyExists = -2147176376,

        /// <summary>
        /// Domain was not found in the DNS table.
        /// </summary>
        CrmLiveDnsDomainNotFound = -2147176377,

        /// <summary>
        /// A user with this username already exists.
        /// </summary>
        CrmLiveDuplicateWindowsLiveId = -2147176378,

        /// <summary>
        /// Execution of custom code feature for this organization is disabled.
        /// </summary>
        CrmLiveExecuteCustomCodeDisabled = -2147176349,

        /// <summary>
        /// An error has occurred while processing your request.
        /// </summary>
        CrmLiveGenericError = -2147176448,

        /// <summary>
        /// An unexpected error happened in the provisioning system.
        /// </summary>
        CrmLiveInternalProvisioningError = -2147176445,

        /// <summary>
        /// Invalid email address entered.
        /// </summary>
        CrmLiveInvalidEmail = -2147176355,

        /// <summary>
        /// External Message Data has some invalid data.  Data: {0} External Message: {1}
        /// </summary>
        CrmLiveInvalidExternalMessageData = -2147176366,

        /// <summary>
        /// This Invoicing Account Number is not valid because it contains an invalid character.
        /// </summary>
        CrmLiveInvalidInvoicingAccountNumber = -2147176357,

        /// <summary>
        /// Invalid phone number entered.
        /// </summary>
        CrmLiveInvalidPhone = -2147176354,

        /// <summary>
        /// The QueueItem has an invalid schedule of start time {0} and end time {1}.
        /// </summary>
        CrmLiveInvalidQueueItemSchedule = -2147176391,

        /// <summary>
        /// The parameter to Dynamics 365 Online Setup is incorrect or not specified.
        /// </summary>
        CrmLiveInvalidSetupParameter = -2147176443,

        /// <summary>
        /// Invalid TaxId entered.
        /// </summary>
        CrmLiveInvalidTaxId = -2147176348,

        /// <summary>
        /// Invalid zip code entered.
        /// </summary>
        CrmLiveInvalidZipCode = -2147176353,

        /// <summary>
        /// Invoicing Account Number (SAP Id) cannot be empty for an invoicing sku.
        /// </summary>
        CrmLiveInvoicingAccountIdMissing = -2147176379,

        /// <summary>
        /// The specified Active Directory Group does not exist.
        /// </summary>
        CrmLiveMissingActiveDirectoryGroup = -2147176446,

        /// <summary>
        /// The scalegroup is missing some required server roles. 1 Witness Server and 2 Sql Servers are required for Provisioning.
        /// </summary>
        CrmLiveMissingServerRolesInScaleGroup = -2147176441,

        /// <summary>
        /// Only one monitoring organization is allowed in a scalegroup.
        /// </summary>
        CrmLiveMonitoringOrganizationExistsInScaleGroup = -2147176410,

        /// <summary>
        /// The ScaleGroup has multiple witness servers specified. There should be only 1 witness server in a scale group.
        /// </summary>
        CrmLiveMultipleWitnessServersInScaleGroup = -2147176442,

        /// <summary>
        /// An error has occurred when deleting the organization.
        /// </summary>
        CrmLiveOrganizationDeleteFailed = -2147176402,

        /// <summary>
        /// Unable to find organization details.
        /// </summary>
        CrmLiveOrganizationDetailsNotFound = -2147176400,

        /// <summary>
        /// Disabling Organization Failed.
        /// </summary>
        CrmLiveOrganizationDisableFailed = -2147176364,

        /// <summary>
        /// Enabling Organization Failed.
        /// </summary>
        CrmLiveOrganizationEnableFailed = -2147176365,

        /// <summary>
        /// The organization name provided is too long.
        /// </summary>
        CrmLiveOrganizationFriendlyNameTooLong = -2147176398,

        /// <summary>
        /// The organization name provided is too short.
        /// </summary>
        CrmLiveOrganizationFriendlyNameTooShort = -2147176399,

        /// <summary>
        /// An error has occurred when provisioning the organization.
        /// </summary>
        CrmLiveOrganizationProvisioningFailed = -2147176447,

        /// <summary>
        /// The unique name provided is not valid.
        /// </summary>
        CrmLiveOrganizationUniqueNameInvalid = -2147176395,

        /// <summary>
        /// The unique name is already reserved.
        /// </summary>
        CrmLiveOrganizationUniqueNameReserved = -2147176394,

        /// <summary>
        /// The unique name provided is too long.
        /// </summary>
        CrmLiveOrganizationUniqueNameTooLong = -2147176396,

        /// <summary>
        /// The unique name provided is too short.
        /// </summary>
        CrmLiveOrganizationUniqueNameTooShort = -2147176397,

        /// <summary>
        /// Upgrade Of Crm Organization Failed.
        /// </summary>
        CrmLiveOrganizationUpgradeFailed = -2147176428,

        /// <summary>
        /// The specified queue item does not exist in the queue. It may have been deleted or its ID may not have been specified correctly
        /// </summary>
        CrmLiveQueueItemDoesNotExist = -2147176444,

        /// <summary>
        /// A QueueItem cannot be scheduled to start or end in the past.
        /// </summary>
        CrmLiveQueueItemTimeInPast = -2147176384,

        /// <summary>
        /// Registration of custom code feature for this organization is disabled.
        /// </summary>
        CrmLiveRegisterCustomCodeDisabled = -2147176350,

        /// <summary>
        /// A server cannot have both Witness and Data Server Roles.
        /// </summary>
        CrmLiveServerCannotHaveWitnessAndDataServerRoles = -2147176440,

        /// <summary>
        /// Only one support organization is allowed in a scalegroup.
        /// </summary>
        CrmLiveSupportOrganizationExistsInScaleGroup = -2147176411,

        /// <summary>
        /// This Category specified is not valid.
        /// </summary>
        CrmLiveUnknownCategory = -2147176358,

        /// <summary>
        /// This Sku specified is not valid.
        /// </summary>
        CrmLiveUnknownSku = -2147176383,

        /// <summary>
        /// Crm malformed expression error occurred.
        /// </summary>
        CrmMalformedExpressionError = -2147220900,

        /// <summary>
        /// The QueryExpression has not been initialized. Please use the constructor that takes in the entity name to create a correctly initialized instance
        /// </summary>
        CrmQueryExpressionNotInitialized = -2147220915,

        /// <summary>
        /// A failure occurred in CrmSecurity.
        /// </summary>
        CrmSecurityError = -2147220906,

        /// <summary>
        /// A validation error occurred. A string value provided is too long.
        /// </summary>
        CrmSQLCharLengthTooLong = -2147012607,

        /// <summary>
        /// The server is busy and the request was not completed. Try again later.
        /// </summary>
        CrmSqlGovernorDatabaseRequestDenied = -2147180543,

        /// <summary>
        /// A networking error caused this operation to fail. Please try again.
        /// </summary>
        CrmSQLNetworkingError = -2147012608,

        /// <summary>
        /// The operation attempted to insert a duplicate value for an attribute with a unique constraint.
        /// </summary>
        CrmSQLUniqueIndexOrConstraintViolation = -2147012606,

        /// <summary>
        /// No Microsoft Dynamics 365 user exists with the specified domain name and user ID
        /// </summary>
        CRMUserDoesNotExist = -2147220652,

        /// <summary>
        /// Invalid cross-entity stage transition. Specified relationship cannot be modified.
        /// </summary>
        CrossEntityRelationshipInvalidOperation = -2146885626,

        /// <summary>
        /// The currency cannot be null.
        /// </summary>
        CurrencyCannotBeNullDueToNonNullMoneyFields = -2147185413,

        /// <summary>
        /// Record currency is required to calculate rollup field of type currency. Provide a currency and try again.
        /// </summary>
        CurrencyFieldMissing = -2147164122,

        /// <summary>
        /// The currency of the {0} does not match the currency of the {1}.
        /// </summary>
        CurrencyNotEqual = -2147185430,

        /// <summary>
        /// The currency cannot be null for discount type amount.
        /// </summary>
        CurrencyRequiredForDiscountTypeAmount = -2147185417,

        /// <summary>
        /// Custom Action must be marked ‘As a Business Process Flow action step’ to use as BPF action step.
        /// </summary>
        CustomActionMustBeMarked = -2147089535,

        /// <summary>
        /// A custom entity defined as an activity already cannot have MailMerge enabled.
        /// </summary>
        CustomActivityCannotBeMailMergeEnabled = -2147159772,

        /// <summary>
        /// Invalid custom activity.
        /// </summary>
        CustomActivityInvalid = -2147200995,

        /// <summary>
        /// A custom entity defined as an activity must have Offline Availability.
        /// </summary>
        CustomActivityMustHaveOfflineAvailability = -2147159774,

        /// <summary>
        /// Property "{0}" can only be configured after property "{1}" has been assigned a value.
        /// </summary>
        CustomControlsDependentPropertyConfiguration = -2146041854,

        /// <summary>
        /// An error occurred while importing Custom Controls. Try importing this solution again.
        /// </summary>
        CustomControlsImportError = -2146041856,

        /// <summary>
        /// Property "{0}" can only be configured after Corresponding DataSet "{1}" view has been assigned a value.
        /// </summary>
        CustomControlsPropertySetConfiguration = -2146041853,

        /// <summary>
        /// An inactive customer cannot be set as the parent of an object.
        /// </summary>
        CustomerIsInactive = -2147220201,

        /// <summary>
        /// Customer opportunity role exists.
        /// </summary>
        CustomerOpportunityRoleExists = -2147188222,

        /// <summary>
        /// This relationship {1} is required by the {0} attribute and can't be deleted. To delete this relationship, first delete the lookup attribute.
        /// </summary>
        CustomerRelationshipCannotBeDeleted = -2147187587,

        /// <summary>
        /// Customer relationship already exists.
        /// </summary>
        CustomerRelationshipExists = -2147188223,

        /// <summary>
        /// A custom image attribute can only be added to a custom entity.
        /// </summary>
        CustomImageAttributeOnlyAllowedOnCustomEntity = -2147187407,

        /// <summary>
        /// Process action associated with this process is not activated.
        /// </summary>
        CustomOperationNotActivated = -2147200942,

        /// <summary>
        /// A custom entity can not have a parental relationship to a system entity
        /// </summary>
        CustomParentingSystemNotSupported = -2147192574,

        /// <summary>
        /// For a goal of custom period type, fiscal year and fiscal period attributes must be left blank.
        /// </summary>
        CustomPeriodGoalHavingExtraInfo = -2147202812,

        /// <summary>
        /// For a goal of custom period type, goalstartdate and goalenddate attributes must have data.
        /// </summary>
        CustomPeriodGoalMissingInfo = -2147202809,

        /// <summary>
        /// This entity is not valid for a custom reflexive relationship
        /// </summary>
        CustomReflexiveRelationshipNotAllowedForEntity = -2147204308,

        /// <summary>
        /// Custom Workflow Activities are not enabled.
        /// </summary>
        CustomWorkflowActivitiesNotSupported = -2147200943,

        /// <summary>
        /// The specified relationship will result in a cycle.
        /// </summary>
        CyclicalRelationship = -2147192828,

        /// <summary>
        /// Cyclic component dependency detected. Please check the exception for more details. Fix the invalid dependencies and try the operation one more time. Detaisl: {0}
        /// </summary>
        CyclicDependency = -2147020458,

        /// <summary>
        /// The input contains a cyclic reference, which is not supported.
        /// </summary>
        CyclicReferencesNotSupported = -2147204737,

        /// <summary>
        /// This invocation may lead to calls do Database which is not allowed.
        /// </summary>
        DatabaseCallsBlockedFailure = -2147015679,

        /// <summary>
        /// This datacenter endpoint is not currently available for this organization.
        /// </summary>
        DatacenterNotAvailable = -2147176347,

        /// <summary>
        /// The number of fields differs from the number of column headings.
        /// </summary>
        DataColumnsNumberMismatch = -2147220667,

        /// <summary>
        /// This query cannot be executed because it conflicts with throttling optimization.
        /// </summary>
        DataEngineQueryThrottling = -2147187388,

        /// <summary>
        /// ActionStep {0} references invalid DataFieldName {1}.
        /// </summary>
        DatafieldNameShouldBeNull = -2147089381,

        /// <summary>
        /// First-time configuration of the Data Migration Manager has been canceled. You will not be able to use the Data Migration Manager until configuration is completed.
        /// </summary>
        DataMigrationManagerMandatoryUpdatesNotInstalled = -2147204298,

        /// <summary>
        /// The Data Migration Manager encountered an unknown problem and cannot continue. To try again, restart the Data Migration Manager.
        /// </summary>
        DataMigrationManagerUnknownProblem = -2147204301,

        /// <summary>
        /// The data sheet is not available.
        /// </summary>
        DatasheetNotAvailable = -2147087947,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        DataSourceInitializeFailedErrorCode = -2147094000,

        /// <summary>
        /// This operation failed because you're offline. Reconnect and try again.
        /// </summary>
        DataSourceOfflineErrorCode = -2147093999,

        /// <summary>
        /// A non fetch based data source is not permitted on this report
        /// </summary>
        DataSourceProhibited = -2147187955,

        /// <summary>
        /// Not in local store with key '{0}'
        /// </summary>
        DataStoreKeyNotFoundErrorCode = -2147093987,

        /// <summary>
        /// No data sync content
        /// </summary>
        DataSyncNoContent = -2147015406,

        /// <summary>
        /// Data sync request accepted
        /// </summary>
        DataSyncRequestAccepted = -2147015407,

        /// <summary>
        /// The original data table has been deleted or renamed.
        /// </summary>
        DataTableNotAvailable = -2147087952,

        /// <summary>
        /// Data type mismatch found for linked attribute.
        /// </summary>
        DataTypeMismatchForLinkedAttribute = -2147159812,

        /// <summary>
        /// Failed to produce a formatted datetime value.
        /// </summary>
        DateTimeFormatFailed = -2147220902,

        /// <summary>
        /// A validation error occurred. A decimal value provided is outside of the allowed values for this attribute.
        /// </summary>
        DecimalValueOutOfRange = -2147204304,

        /// <summary>
        /// Cannot decouple a child entity.
        /// </summary>
        DecoupleChildEntity = -2147188218,

        /// <summary>
        /// Can only decouple user owned entities.
        /// </summary>
        DecoupleUserOwnedEntity = -2147188217,

        /// <summary>
        /// Decreasing the number of days will delete mobile offline data older than the number of days specified.
        /// </summary>
        DecreasingDaysWillDeleteOlderData = -2147087982,

        /// <summary>
        /// Default site collection url has been changed this organization after this operation was created.
        /// </summary>
        DefaultSiteCollectionUrlChanged = -2147159808,

        /// <summary>
        /// Cannot delete default site map.
        /// </summary>
        DefaultSiteMapDeleteFailure = -2147188624,

        /// <summary>
        /// The delegated admin user cannot be updated
        /// </summary>
        DelegatedAdminUserCannotBeCreateNorUpdated = -2147213977,

        /// <summary>
        /// Cannot delete workflow dependency from a published workflow template .
        /// </summary>
        DeleteActiveWorkflowTemplateDependency = -2147200998,

        /// <summary>
        /// Cannot delete a workflow dependency for a published workflow definition.
        /// </summary>
        DeletePublishedWorkflowDefinitionWorkflowDependency = -2147201018,

        /// <summary>
        /// Cannot delete a workflow activation.
        /// </summary>
        DeleteWorkflowActivation = -2147201020,

        /// <summary>
        /// Cannot delete a workflow dependency associated with a workflow activation.
        /// </summary>
        DeleteWorkflowActivationWorkflowDependency = -2147201019,

        /// <summary>
        /// Cannot delete an active workflow definition.
        /// </summary>
        DeleteWorkflowActiveDefinition = -2147201009,

        /// <summary>
        /// Cannot delete an active workflow template.
        /// </summary>
        DeleteWorkflowActiveTemplate = -2147200996,

        /// <summary>
        /// Attribute not present in exchange oData response.
        /// </summary>
        DelveActionHubAttributeMissingInResponseException = -2147020798,

        /// <summary>
        /// You don’t have the proper Office 365 license to view actions. Please contact your system administrator.
        /// </summary>
        DelveActionHubAuthorizationFailureException = -2147020793,

        /// <summary>
        /// Delve action hub feature is not enabled.
        /// </summary>
        DelveActionHubDisabledError = -2147020800,

        /// <summary>
        /// Invalid response format.
        /// </summary>
        DelveActionHubInvalidResponseFormatException = -2147020796,

        /// <summary>
        /// Invalid state code passed in expression.
        /// </summary>
        DelveActionHubInvalidStateCodeException = -2147020797,

        /// <summary>
        /// Error while fetching actions from Exchange.
        /// </summary>
        DelveActionHubResponseRetievalFailureException = -2147020795,

        /// <summary>
        /// Server to Server Authentication with Exchange for Delve Action Hub is not set up.
        /// </summary>
        DelveActionHubS2SSetupFailureException = -2147020792,

        /// <summary>
        /// A {0} dependency already exists between {1}({2}) and {3}({4}).  Cannot also create {5} dependency.
        /// </summary>
        DependencyAlreadyExists = -2147160038,

        /// <summary>
        /// The dependency table must be empty for initialization to complete successfully.
        /// </summary>
        DependencyTableNotEmpty = -2147160037,

        /// <summary>
        /// Invalid attempt to process a dependency after the current transaction context has been closed.
        /// </summary>
        DependencyTrackingClosed = -2147160027,

        /// <summary>
        /// You cannot change the state of this server because it contains the Deployment Service server role.
        /// </summary>
        DeploymentServiceCannotChangeStateForDeploymentService = -2147167646,

        /// <summary>
        /// The Deployment Service cannot delete the specified operation because it is currently in progress.
        /// </summary>
        DeploymentServiceCannotDeleteOperationInProgress = -2147167643,

        /// <summary>
        /// Deployment Service for {0} does not allow {1} operation.
        /// </summary>
        DeploymentServiceNotAllowOperation = -2147167647,

        /// <summary>
        /// Deployment Service for {0} allows the state Enabled or Disabled. Cannot set state to {1}.
        /// </summary>
        DeploymentServiceNotAllowSetToThisState = -2147167648,

        /// <summary>
        /// The Deployment Service could not find a deferred operation having the specified identifier.
        /// </summary>
        DeploymentServiceOperationIdentifierNotFound = -2147167644,

        /// <summary>
        /// The Deployment Service cannot process the request because one or more validation checks failed.
        /// </summary>
        DeploymentServiceRequestValidationFailure = -2147167645,

        /// <summary>
        /// This form has been deprecated in the previous release and cannot be used anymore. Please migrate your changes to a different form. Deprecated forms will be removed from the system in the future.
        /// </summary>
        DeprecatedFormActivation = -2147158430,

        /// <summary>
        /// Mobile forms have been deprecated. Cannot create new mobile express forms.
        /// </summary>
        DeprecatedMobileFormsCreation = -2147158425,

        /// <summary>
        /// Mobile forms have been deprecated. Cannot open mobile forms editor.
        /// </summary>
        DeprecatedMobileFormsEdit = -2147158424,

        /// <summary>
        /// Relationship Insights can only be turned off by a system administrator.
        /// </summary>
        DeprovisionRIAccessNotAllowed = -2147204492,

        /// <summary>
        /// You do not have enough privileges to perform the requested operation. For more information, contact your administrator.
        /// </summary>
        DesignerAccessDenied = -2147155712,

        /// <summary>
        /// The {0} provided is incorrect or missing. Please try again with the correct {1}.
        /// </summary>
        DesignerInvalidParameter = -2147155711,

        /// <summary>
        /// Unable to copy the documents. The destination document location no longer exists.
        /// </summary>
        DestinationFolderNotExists = -2147020766,

        /// <summary>
        /// "DialogName cannot be null for type Dialog
        /// </summary>
        DialogNameCannotBeNull = -2147088269,

        /// <summary>
        /// An error occurred loading Microsoft Dynamics 365 functionality. Try restarting Outlook. Contact your system administrator if errors persist.
        /// </summary>
        DisabledCRMAddinLoadFailure = -2147204606,

        /// <summary>
        /// The Microsoft Dynamics 365 server needs to be upgraded before Microsoft Dynamics 365 client can be used. Contact your system administrator for assistance.
        /// </summary>
        DisabledCRMClientVersionHigher = -2147204604,

        /// <summary>
        /// You're running a version of Microsoft Dynamics 365 for Outlook that is not supported for offline mode with this Microsoft Dynamics 365 organization {0}. You'll need to upgrade to a compatible version of Dynamics 365 for Outlook. Make sure your current version of Dynamics 365 for Outlook is supported for upgrading to the compatible version.
        /// </summary>
        DisabledCRMClientVersionLower = -2147204605,

        /// <summary>
        /// Microsoft Dynamics 365 functionality is not available while Offline Synchronization is occuring
        /// </summary>
        DisabledCRMGoingOffline = -2147204608,

        /// <summary>
        /// Microsoft Dynamics 365 functionality is not available while Online Synchronization is occuring
        /// </summary>
        DisabledCRMGoingOnline = -2147204607,

        /// <summary>
        /// Microsoft Dynamics 365 server is not available
        /// </summary>
        DisabledCRMOnlineCrmNotAvailable = -2147204602,

        /// <summary>
        /// Microsoft Dynamics 365 functionality is not available until the Microsoft Dynamics 365 client is taken back online
        /// </summary>
        DisabledCRMPostOfflineUpgrade = -2147204603,

        /// <summary>
        /// You need system administrator privileges to turn Relationship Insights off for your organization.
        /// </summary>
        DisableRIFeatureNotAllowed = -2147204480,

        /// <summary>
        /// The discount type does not support 'percentage' discounts.
        /// </summary>
        DiscountAmount = -2147206368,

        /// <summary>
        /// Both 'amount' and 'percentage' cannot be set.
        /// </summary>
        DiscountAmountAndPercent = -2147206369,

        /// <summary>
        /// The discount type does not support 'amount' discounts.
        /// </summary>
        DiscountPercent = -2147206367,

        /// <summary>
        /// The new quantities overlap the range covered by existing quantities.
        /// </summary>
        DiscountRangeOverlap = -2147206398,

        /// <summary>
        /// The currency of the discount needs to match the currency of the price list for discount type amount.
        /// </summary>
        DiscountTypeAndPriceLevelCurrencyNotEqual = -2147185416,

        /// <summary>
        /// There is not enough space in the Temp Folder.
        /// </summary>
        DiskSpaceNotEnough = -2147155676,

        /// <summary>
        /// This campaign activity cannot be distributed. Mail merge activities can be done only on marketing lists that are all the same record type. For this campaign activity, remove marketing lists so that the remaining ones are the same record type, and then try again.
        /// </summary>
        DistributeListAssociatedVary = -2147187629,

        /// <summary>
        /// This campaign activity cannot be distributed. No marketing lists are associated with it. Add at least one marketing list and try again.
        /// </summary>
        DistributeNoListAssociated = -2147187628,

        /// <summary>
        /// Document Management has been disabled for this organization.
        /// </summary>
        DocumentManagementDisabled = -2147159809,

        /// <summary>
        /// Document Management must be enabled on the Email entity in order to follow attachments. Please contact your administrator to enable Document Management.
        /// </summary>
        DocumentManagementDisabledForEmail = -2147155952,

        /// <summary>
        /// You must enable document management for this Entity in order to enable OneNote integration.
        /// </summary>
        DocumentManagementDisabledOnEntity = -2147088120,

        /// <summary>
        /// Document Management is not enabled for this Organization.
        /// </summary>
        DocumentManagementIsDisabled = -2147159278,

        /// <summary>
        /// You must enable document management for this Entity in order to enable Document Recommendations.
        /// </summary>
        DocumentManagementIsDisabledOnEntity = -2147020783,

        /// <summary>
        /// Document management could not be enabled because a primary field is not defined for this entity with name {0} and id {1}.
        /// </summary>
        DocumentManagementNotEnabledNoPrimaryField = -2147159277,

        /// <summary>
        /// The document suggestions feature is not enabled.
        /// </summary>
        DocumentRecommendationsFCBOff = -2147020775,

        /// <summary>
        /// Document template feature is not enabled.
        /// </summary>
        DocumentTemplateFeatureNotEnabled = -2147088183,

        /// <summary>
        /// An error occurred while generating the Word document. Please try again.
        /// </summary>
        DocxExportGeneratingWordFailed = -2147088179,

        /// <summary>
        /// The upload failed because the selected file is not consistent with the template layout. Please try again after selecting a file with the template layout.
        /// </summary>
        DocxValidationFailed = -2147088178,

        /// <summary>
        /// Selected item will not be tracked.
        /// </summary>
        DoNotTrackItem = -2147204568,

        /// <summary>
        /// Download all entity records changed or created within this number of days.
        /// </summary>
        DownloadAllEntityRecordsChangedOrCreatedWithinTheseDays = -2147087985,

        /// <summary>
        /// The entity '{0}' in profile '{1}' is configured with the filter download related data only, however there are no relationships specified for this entity in profile item associations. If an entity is set to download related data only you must specify a profile item association to this entity.
        /// </summary>
        DownloadRelatedDataOnlyMustHaveRelationship = -2147020480,

        /// <summary>
        /// You can only add products to a draft bundle.
        /// </summary>
        DraftBundleToProduct = -2147157612,

        /// <summary>
        /// Data Description is invalid. Duplicate alias found.
        /// </summary>
        DuplicateAliasFound = -2147164149,

        /// <summary>
        /// You are attempting to create an Application ID = {0} that already exists.
        /// </summary>
        DuplicateApplicationUser = -2147158767,

        /// <summary>
        /// The name you entered is already in use.
        /// </summary>
        DuplicateAppModuleUniqueName = -2147155681,

        /// <summary>
        /// Attribute {0} already exists for entity {1}.
        /// </summary>
        DuplicateAttributePhysicalName = -2147089660,

        /// <summary>
        /// An attribute with the specified name already exists
        /// </summary>
        DuplicateAttributeSchemaName = -2147192813,

        /// <summary>
        /// A channel property with the specified name already exists. You can't create another one.
        /// </summary>
        DuplicateChannelPropertyName = -2147088143,

        /// <summary>
        /// Duplicate detection is not enabled. To enable duplicate detection, click Settings, click Data Management, and then click Duplicate Detection Settings.
        /// </summary>
        DuplicateCheckNotEnabled = -2147187694,

        /// <summary>
        /// Duplicate detection is not supported on this record type.
        /// </summary>
        DuplicateCheckNotSupportedOnEntity = -2147187696,

        /// <summary>
        /// Duplicate Component is present in the XML.
        /// </summary>
        DuplicateComponentInSolutionXml = -2147020461,

        /// <summary>
        /// The rule condition cannot be created or updated because duplicate detection is not supported on the data type of the selected attribute.
        /// </summary>
        DuplicateDetectionNotSupportedOnAttributeType = -2147187664,

        /// <summary>
        /// The duplicate detection rules for this entity have been unpublished due to possible modifications to the entity.
        /// </summary>
        DuplicateDetectionRulesWereUnpublished = -2147160007,

        /// <summary>
        /// Microsoft Dynamics 365 could not retrieve the e-mail notification template.
        /// </summary>
        DuplicateDetectionTemplateNotFound = -2147187676,

        /// <summary>
        /// An object with the specified display collection name already exists.
        /// </summary>
        DuplicateDisplayCollectionName = -2147192814,

        /// <summary>
        /// An object with the specified display name already exists.
        /// </summary>
        DuplicateDisplayName = -2147192815,

        /// <summary>
        /// Parameter ImportJobId must be unique.
        /// </summary>
        DuplicatedJobId = -2147020462,

        /// <summary>
        /// Cannot create the solution job using the supplied JobId ({0}) as it is already in use. This may indicate that another solution operation is progress. Please try again later.
        /// </summary>
        DuplicatedJobIdDueToConcurrency = -2147020459,

        /// <summary>
        /// Privilege {0} is duplicated.
        /// </summary>
        DuplicatedPrivilege = -2147216369,

        /// <summary>
        /// Two or more files have the same name. File names must be unique.
        /// </summary>
        DuplicateFileNamesInZip = -2147187580,

        /// <summary>
        /// Data Description is invalid. Same attribute cannot be used as a group by more than once.
        /// </summary>
        DuplicateGroupByFound = -2147164133,

        /// <summary>
        /// A duplicate column heading exists.
        /// </summary>
        DuplicateHeaderColumn = -2147220680,

        /// <summary>
        /// Cannot insert duplicate currency record. Currency with the same currency code already exist in the system.
        /// </summary>
        DuplicateIsoCurrencyCode = -2147185421,

        /// <summary>
        /// A duplicate lookup reference was found
        /// </summary>
        DuplicateLookupFound = -2147220654,

        /// <summary>
        /// A data map with the specified name already exists.
        /// </summary>
        DuplicateMapName = -2147187645,

        /// <summary>
        /// An object with the specified name already exists
        /// </summary>
        DuplicateName = -2147192816,

        /// <summary>
        /// You can create only one local data group for each record type.
        /// </summary>
        DuplicateOfflineFilter = -2147187639,

        /// <summary>
        /// The Appointment being promoted from Outlook is already tracked in Dynamics 365
        /// </summary>
        DuplicateOutlookAppointment = -2147220876,

        /// <summary>
        /// The new {2} attribute is set as the primary name attribute for the {1} entity. The {1} entity already has the {0} attribute set as the primary name attribute. An entity can only have one primary name attribute.
        /// </summary>
        DuplicatePrimaryNameAttribute = -2147192802,

        /// <summary>
        /// The Channel Access Profile privilege array contains duplicate privilege references.
        /// </summary>
        DuplicatePrivilegeInRolecontrol = -2147086056,

        /// <summary>
        /// This product and unit combination has a price for this price list.
        /// </summary>
        DuplicateProductPriceLevel = -2147206392,

        /// <summary>
        /// A product relationship with the same product and related product already exists.
        /// </summary>
        DuplicateProductRelationship = -2147157871,

        /// <summary>
        /// Operation failed due to a SQL integrity violation.
        /// </summary>
        DuplicateRecord = -2147220937,

        /// <summary>
        /// Entity Key {0} violated. A record with the same value for {1} already exists. A duplicate record cannot be created. Select one or more unique values and try again.
        /// </summary>
        DuplicateRecordEntityKey = -2147088238,

        /// <summary>
        /// A record was not created or updated because a duplicate of the current record already exists.
        /// </summary>
        DuplicateRecordsFound = -2147220685,

        /// <summary>
        /// A ReportVisibility with the same ReportId and VisibilityCode already exists. Duplicates are not allowed.
        /// </summary>
        DuplicateReportVisibility = -2147220331,

        /// <summary>
        /// The user you're trying to add is already a member of the sales team.
        /// </summary>
        DuplicateSalesTeamMember = -2147187903,

        /// <summary>
        /// There can be only one root statement for a given uiscript.
        /// </summary>
        DuplicateUIStatementRootsFound = -2147159551,

        /// <summary>
        /// You must specify a default value because this property is required and is read-only.
        /// </summary>
        DynamicPropertyDefaultValueNeeded = -2147086280,

        /// <summary>
        /// The property instance can't be updated. Verify that the following fields are present: dynamicpropertyid, dynamicpropertyoptionsetvalueid, and regardingobjectid.
        /// </summary>
        DynamicPropertyInstanceMissingRequiredColumns = -2146955254,

        /// <summary>
        /// The property instances couldn't be saved because they refer to different product line items.
        /// </summary>
        DynamicPropertyInstanceUpdateValuesDifferentRegarding = -2146955253,

        /// <summary>
        /// You can’t create or change properties for a published or retired product.
        /// </summary>
        DynamicPropertyInvalidRegardingForUpdate = -2146955260,

        /// <summary>
        /// You can't set an inactive property to an active state.
        /// </summary>
        DynamicPropertyInvalidStateChange = -2146955263,

        /// <summary>
        /// You can't delete a property that is in the active state.
        /// </summary>
        DynamicPropertyInvalidStateForDelete = -2146955262,

        /// <summary>
        /// You can't update a property that isn't in the draft state.
        /// </summary>
        DynamicPropertyInvalidStateForUpdate = -2146955264,

        /// <summary>
        /// You can't modify the property option set item for a property that is not in the draft state.
        /// </summary>
        DynamicPropertyOptionSetInvalidStateForUpdate = -2146955252,

        /// <summary>
        /// The rule expression contains logical operator which is not supported. The editor only support And operator for Logical conditions.
        /// </summary>
        EditorOnlySupportAndOperatorForLogicalConditions = -2147090427,

        /// <summary>
        /// You can’t edit the query on a dynamic spreadsheet once the Excel file has been exported. If you’d like to make changes, go back to Dynamics 365 and then re-export.
        /// </summary>
        EditQueryInDynamicExcelNotSupported = -2147087944,

        /// <summary>
        /// Unable to fetch data from site DB.
        /// </summary>
        EESiteDBFetchFailure = -2147155931,

        /// <summary>
        /// You cannot add this e-mail to the selected queue. A queue item for this e-mail already exists in the queue. You can delete the item from the queue, and then try again.
        /// </summary>
        EmailAlreadyExistsInDestinationQueue = -2147220189,

        /// <summary>
        /// Email does not exist for given attachment.
        /// </summary>
        EmailDoesNotExist = -2147155961,

        /// <summary>
        /// Please enable Email Engagement feature for current org to follow or unfollow email attachment.
        /// </summary>
        EmailEngagementFeatureDisabled = -2147155965,

        /// <summary>
        /// Please enable Email Engagement feature for this organization to follow email attachments.
        /// </summary>
        EmailEngagementFeatureDisabledForAttachmentTracking = -2147155947,

        /// <summary>
        /// Unable to fetch email interactions.
        /// </summary>
        EmailInteractionsFetchFailure = -2147155934,

        /// <summary>
        /// Email Size Exceeds the MaximumMessageSizeLimit specified by the deployment.
        /// </summary>
        EmailMessageSizeExceeded = -2147098057,

        /// <summary>
        /// Email engagement feature deprovisioning failed
        /// </summary>
        EmailMonitoringDeProvisionFailed = -2147155948,

        /// <summary>
        /// RI provisioning service failed.
        /// </summary>
        EmailMonitoringNotProvisioned = -2147155951,

        /// <summary>
        /// Email engagement feature provisioning failed
        /// </summary>
        EmailMonitoringProvisionFailed = -2147155950,

        /// <summary>
        /// This attachment cannot be followed as its corresponding email is not followed.
        /// </summary>
        EmailNotFollowed = -2147155960,

        /// <summary>
        /// We can’t create email open action card.
        /// </summary>
        EmailOpenActionCardCreationFailure = -2147155932,

        /// <summary>
        /// The e-mail must have at least one recipient before it can be sent
        /// </summary>
        EmailRecipientNotSpecified = -2147218684,

        /// <summary>
        /// We can’t create email reminder action card.
        /// </summary>
        EmailReminderActionCardCreationFailure = -2147155933,

        /// <summary>
        /// One or more of the email router configuration files is too large to get processed.
        /// </summary>
        EmailRouterFileTooLargeToProcess = -2147094479,

        /// <summary>
        /// The authentication protocol cannot be set to Negotiate or NTLM for your organization because these require Active Directory. Use a different authentication protocol or contact your system administrator to enable an Active Directory-based authentication protocol.
        /// </summary>
        EmailServerProfileADBasedAuthenticationProtocolNotAllowed = -2147098052,

        /// <summary>
        /// Auto discover server URL can location can only be used for an exchange e-mail server type.
        /// </summary>
        EmailServerProfileAutoDiscoverNotAllowed = -2147098108,

        /// <summary>
        /// The specified authentication protocol cannot be used because the protocol requires sending credentials on a secure channel. Use a different authentication protocol or contact your administrator to enable Basic authentication protocol on a non-secure channel.
        /// </summary>
        EmailServerProfileBasicAuthenticationProtocolNotAllowed = -2147098051,

        /// <summary>
        /// For an SMTP email server type, auto-granted delegate access cannot be used.
        /// </summary>
        EmailServerProfileDelegateAccessNotAllowed = -2147098059,

        /// <summary>
        /// For a Non Exchange email server type, impersonation cannot be used.
        /// </summary>
        EmailServerProfileImpersonationNotAllowed = -2147098058,

        /// <summary>
        /// The authentication protocol is invalid for the specified server and connection type. For more information, contact your system administrator.
        /// </summary>
        EmailServerProfileInvalidAuthenticationProtocol = -2147098053,

        /// <summary>
        /// No credentials (Anonymous) cannot be used a connection type for exchange e-mail server type.
        /// </summary>
        EmailServerProfileInvalidCredentialRetrievalForExchange = -2147098109,

        /// <summary>
        /// Windows integrated or Anonymous authentication cannot be used as a connection type for Microsoft Dynamics 365 Online.
        /// </summary>
        EmailServerProfileInvalidCredentialRetrievalForOnline = -2147098110,

        /// <summary>
        /// The specified server location {0} is invalid. Correct the server location and try again.
        /// </summary>
        EmailServerProfileInvalidServerLocation = -2147098102,

        /// <summary>
        /// You cannot specify the incoming/outgoing e-mail server location when Autodiscover server location has been set to true.
        /// </summary>
        EmailServerProfileLocationNotRequired = -2147098107,

        /// <summary>
        /// Email Server Profile is not associated with the current mailbox. Please associate a valid profile to send/receive mails.
        /// </summary>
        EmailServerProfileNotAssociated = -2147098078,

        /// <summary>
        /// You cannot set SSL as false for Microsoft Dynamics 365 Online.
        /// </summary>
        EmailServerProfileSslRequiredForOnline = -2147098111,

        /// <summary>
        /// Usage of SSL while contacting external email servers is mandatory for this Dynamics 365 deployment.
        /// </summary>
        EmailServerProfileSslRequiredForOnPremise = -2147098060,

        /// <summary>
        /// Command or entity name cannot be empty.
        /// </summary>
        EmptyCommandOrEntity = -2146088111,

        /// <summary>
        /// The file is empty.
        /// </summary>
        EmptyContent = -2147220635,

        /// <summary>
        /// The FetchXML is missing.
        /// </summary>
        EmptyEntityFilterXml = -2147020520,

        /// <summary>
        /// The selected file contains no data.
        /// </summary>
        EmptyFileForImport = -2147187577,

        /// <summary>
        /// One or more files in the compressed (.zip) or .cab file don't contain data. Check the files and try again.
        /// </summary>
        EmptyFilesInZip = -2147187578,

        /// <summary>
        /// The column heading cannot be empty.
        /// </summary>
        EmptyHeaderColumn = -2147220681,

        /// <summary>
        /// The first row of the file is empty.
        /// </summary>
        EmptyHeaderRow = -2147220634,

        /// <summary>
        /// Empty row.
        /// </summary>
        EmptyImportFileRow = -2147220665,

        /// <summary>
        /// The record is empty
        /// </summary>
        EmptyRecord = -2147220621,

        /// <summary>
        /// Data Source secrets are not included in solutions. You'll need to edit your data sources to add secrets back following solution import.
        /// </summary>
        EmptySecretInDataSource = -2147203048,

        /// <summary>
        /// Sitemap xml is empty.
        /// </summary>
        EmptySiteMapXml = -2147159038,

        /// <summary>
        /// Empty XML.
        /// </summary>
        EmptyXml = -2147220990,

        /// <summary>
        /// You must enable change tracking for this entity since mobile offline client is enabled.
        /// </summary>
        EnableMobileOfflineDisableChangeTrackingError = -2147087966,

        /// <summary>
        /// You need system administrator privileges to update Relationship Insights tenant information.
        /// </summary>
        EnableRIFeatureNotAllowed = -2147204487,

        /// <summary>
        /// Cannot send Email for EndUserNotification Type: {0}.
        /// </summary>
        EndUserNotificationTypeNotValidForEmail = -2147167599,

        /// <summary>
        /// You can't cover more than five entities in a process flow. Remove some entities and try again.
        /// </summary>
        EntitiesExceedMaxAllowed = -2147089387,

        /// <summary>
        /// One or more entities referenced are not available offline.
        /// </summary>
        EntitiesInViewNotAvailableOffline = -2147020507,

        /// <summary>
        /// One or more entities in this view are not part of this profile.
        /// </summary>
        EntitiesInViewNotInProfile = -2147020508,

        /// <summary>
        /// You can't activate an entitlement when it's in the active state.
        /// </summary>
        EntitlementAlreadyInactiveState = -2147088875,

        /// <summary>
        /// You can't cancel an entitlement when it's in the Canceled state.
        /// </summary>
        EntitlementAlreadyInCanceledState = -2147204600,

        /// <summary>
        /// You can't deactivate an entitlement when it's in the draft state.
        /// </summary>
        EntitlementAlreadyInDraftState = -2147088876,

        /// <summary>
        /// Total terms can't be blank. Enter a value and try again.
        /// </summary>
        EntitlementBlankTerms = -2147088862,

        /// <summary>
        /// An entitlement channel term cannot be created, modified or deleted when the associated entitlement is not in draft state.
        /// </summary>
        EntitlementChannelInvalidState = -2147088893,

        /// <summary>
        /// Associate the entitlement channel with an entitlement or entitlement template.
        /// </summary>
        EntitlementChannelWithoutEntitlementId = -2147088878,

        /// <summary>
        /// You can only edit a draft entitlement.
        /// </summary>
        EntitlementEditDraft = -2147088877,

        /// <summary>
        /// The number of remaining terms can't be greater than the number of total terms.
        /// </summary>
        EntitlementInvalidRemainingTerms = -2147088860,

        /// <summary>
        /// Start Date cannot be more than the End Date
        /// </summary>
        EntitlementInvalidStartEndDate = -2147088896,

        /// <summary>
        /// You cannot delete an entitlement that is in active or waiting state
        /// </summary>
        EntitlementInvalidState = -2147088895,

        /// <summary>
        /// Specify a higher value for total terms so the remaining terms won't be a negative value.
        /// </summary>
        EntitlementInvalidTerms = -2147088892,

        /// <summary>
        /// You can't create a case for this entitlement because the entitlement is not in active state.
        /// </summary>
        EntitlementNotActiveInAssociationToCase = -2147088874,

        /// <summary>
        /// If the allocation type is the number of cases, the total terms can't be a decimal value. Specify a whole number.
        /// </summary>
        EntitlementTemplateTotalTerms = -2147088864,

        /// <summary>
        /// If the allocation type is the number of cases, the total terms can't be a decimal value. Specify a whole number.
        /// </summary>
        EntitlementTotalTerms = -2147088871,

        /// <summary>
        /// This entity is either not valid as a child in a custom parental relationship or is already a child in a parental relationship
        /// </summary>
        EntityCannotBeChildInCustomRelationship = -2147204307,

        /// <summary>
        /// The entity '{0}' in the profile '{1}' has OwnedByMe set to true. This property is not a valid property for the '{0}' entity.
        /// </summary>
        EntityCannotHaveOwnedByMeFilter = -2147020490,

        /// <summary>
        /// The entity '{0}' in the profile '{1}' has OwnedByMyTeam set to true. This property is not a valid property for the '{0}' entity.
        /// </summary>
        EntityCannotHaveOwnedByMyTeamFilter = -2147020489,

        /// <summary>
        /// This entity cannot participate in an entity association
        /// </summary>
        EntityCannotParticipateInEntityAssociation = -2147204302,

        /// <summary>
        /// Duplicate detection is not enabled for one or more of the selected entities. The duplicate detection job cannot be started.
        /// </summary>
        EntityDupCheckNotSupportedSystemWide = -2147187663,

        /// <summary>
        /// The {0} entity exceeds the maximum number of active business process flows. The limit is {1}.
        /// </summary>
        EntityExceedsMaxActiveBusinessProcessFlows = -2147089376,

        /// <summary>
        /// There are no filters specified for the entity '{0}'. You must define at least one filter.
        /// </summary>
        EntityFilterContainerMustNotBeNullFormatString = -2147020494,

        /// <summary>
        /// Missing parameter. You must provide EntityGroupName or EntityNames.
        /// </summary>
        EntityGroupNameOrEntityNamesMustBeProvided = -2147089915,

        /// <summary>
        /// Specified entity does not have a statecode.
        /// </summary>
        EntityHasNoStateCode = -2147192811,

        /// <summary>
        /// Error creating or updating Business Process: entity instance cannot be null.
        /// </summary>
        EntityInstanceIsNull = -2147089340,

        /// <summary>
        /// Instantiation of an Entity instance Service failed.
        /// </summary>
        EntityInstantiationFailed = -2147220925,

        /// <summary>
        /// The specified entity is intersect entity
        /// </summary>
        EntityIsIntersect = -2147187953,

        /// <summary>
        /// This entity is already locked.
        /// </summary>
        EntityIsLocked = -2147206371,

        /// <summary>
        /// The IsBusinessProcessEnabled property of the {0} entity is false.
        /// </summary>
        EntityIsNotBusinessProcessFlowEnabled = -2147089375,

        /// <summary>
        /// The specified entity is not customizable
        /// </summary>
        EntityIsNotCustomizable = -2147192824,

        /// <summary>
        /// You can't create/update an external party item associated to an entity that is not enabled for external party.
        /// </summary>
        EntityIsNotEnabledForExternalParty = -2147086053,

        /// <summary>
        /// This entity is not enabled to be followed.
        /// </summary>
        EntityIsNotEnabledForFollow = -2147158366,

        /// <summary>
        /// This entity is not enabled to be followed.
        /// </summary>
        EntityIsNotEnabledForFollowUser = -2147158367,

        /// <summary>
        /// This entity is already unlocked.
        /// </summary>
        EntityIsUnlocked = -2147206370,

        /// <summary>
        /// An entity key with the name {0} already exists on entity {1}.
        /// </summary>
        EntityKeyNameExists = -2147088237,

        /// <summary>
        /// The specified key attributes are not a defined key for the {0} entity
        /// </summary>
        EntityKeyNotDefined = -2147088240,

        /// <summary>
        /// An entity key with the selected attributes already exists on entity.
        /// </summary>
        EntityKeyWithSelectedAttributesExists = -2147088236,

        /// <summary>
        /// MultiEntitySearch exceeded Entity Limit defined for the Organization.
        /// </summary>
        EntityLimitExceeded = -2147089920,

        /// <summary>
        /// Creating this parental association would create a loop in this entity hierarchy.
        /// </summary>
        EntityLoopBeingCreated = -2147220601,

        /// <summary>
        /// Loop exists in this entity hierarchy.
        /// </summary>
        EntityLoopExists = -2147220602,

        /// <summary>
        /// There were problems with the server configurations.  There was a problem with the server configuration changes.  We are unable to load the application, please contact your Dynamics 365 administrator.
        /// </summary>
        EntityMetadataSyncFailed = -2147093960,

        /// <summary>
        /// There were difficulties with the server configuration changes.  You can continue to use the app with the older configuration, however, you may experience problems including errors when saving.  Please contact your Dynamics 365 administrator.
        /// </summary>
        EntityMetadataSyncFailedWithContinue = -2147093959,

        /// <summary>
        /// This entity is not enabled for auto created access teams.
        /// </summary>
        EntityNotEnabledForAutoCreatedAccessTeams = -2147187916,

        /// <summary>
        /// Charts are not enabled on the specified primary entity type code: {0}.
        /// </summary>
        EntityNotEnabledForCharts = -2147164148,

        /// <summary>
        /// Entity not enabled to be viewed in this device
        /// </summary>
        EntityNotEnabledForThisDevice = -2147094016,

        /// <summary>
        /// The collection name is not a recurrence rule.
        /// </summary>
        EntityNotRule = -2147163886,

        /// <summary>
        /// Required arguments of type EntityReference must be bound to some entity.
        /// </summary>
        EntityReferenceArgumentsNotBound = -2147089515,

        /// <summary>
        /// Custom labels must be provided if an entity relationship role has a display option of UseCustomLabels
        /// </summary>
        EntityRelationshipRoleCustomLabelsMissing = -2147204312,

        /// <summary>
        /// A relationship with the specified name already exists. Please specify a unique name.
        /// </summary>
        EntityRelationshipSchemaNameNotUnique = -2147204309,

        /// <summary>
        /// Entity relationships require a name
        /// </summary>
        EntityRelationshipSchemaNameRequired = -2147204310,

        /// <summary>
        /// {0} entity does not support this message.
        /// </summary>
        EntityTypeNotSupported = -2146435064,

        /// <summary>
        /// An entity type cannot be specified for a dashboard.
        /// </summary>
        EntityTypeSpecifiedForDashboard = -2147163381,

        /// <summary>
        /// Error when trying to connect to customer's discovery service.
        /// </summary>
        ErrorConnectingToDiscoveryService = -2147176346,

        /// <summary>
        /// Error when trying to connect to customer's organization service.
        /// </summary>
        ErrorConnectingToOrganizationService = -2147176344,

        /// <summary>
        /// You cannot delete the UI script statement text because it is being referred by one or more ui script statements.
        /// </summary>
        ErrorDeleteStatementTextIsReferenced = -2147159549,

        /// <summary>
        /// We can't fetch base URL for organization Id {0}. Exception details {1}
        /// </summary>
        ErrorFetchingBaseUrl = -2147204464,

        /// <summary>
        /// We can't fetch RI provisioning status for organization Id {0}. Exception details {1}
        /// </summary>
        ErrorFetchingRIProvisionStatus = -2147204463,

        /// <summary>
        /// An error has occurred. Please try again later.
        /// </summary>
        ErrorGeneratingActionHub = -2147020799,

        /// <summary>
        /// Some Internal error occurred in generating invitation token, Please try again later
        /// </summary>
        ErrorGeneratingInvitation = -2147176429,

        /// <summary>
        /// You cannot save data to a published UI script. Unpublish the UI script, and try again.
        /// </summary>
        ErrorImportInvalidForPublishedScript = -2147159530,

        /// <summary>
        /// The Microsoft Dynamics 365 record could not be created
        /// </summary>
        ErrorIncreate = -2147220647,

        /// <summary>
        /// The Microsoft Dynamics 365 record could not be deleted
        /// </summary>
        ErrorInDelete = -2147220646,

        /// <summary>
        /// Error in fetching email engagement feature provisioning status.
        /// </summary>
        ErrorInFetchingEmailEngagementProvisioningStatus = -2147155949,

        /// <summary>
        /// An error occurred while increasing the field width.
        /// </summary>
        ErrorInFieldWidthIncrease = -2147204271,

        /// <summary>
        /// Cannot process with Bulk Import as Import Configuration has some errors.
        /// </summary>
        ErrorInImportConfig = -2147220701,

        /// <summary>
        /// The row could not be parsed. This is typically caused by a row that is too long.
        /// </summary>
        ErrorInParseRow = -2147220666,

        /// <summary>
        /// The status or status reason of the Microsoft Dynamics 365 record could not be set
        /// </summary>
        ErrorInSetState = -2147220649,

        /// <summary>
        /// An error occurred while storing the import file in database.
        /// </summary>
        ErrorInStoringImportFile = -2147187561,

        /// <summary>
        /// An error occurred while extracting the uploaded compressed (.zip) or .cab file. Make sure that the file isn't password protected, and try uploading the file again. If this problem persists, contact your system administrator.
        /// </summary>
        ErrorInUnzip = -2147187581,

        /// <summary>
        /// An error occurred while the uploaded compressed (.zip) file was being extracted. Try to upload the file again. If the problem persists, contact your system administrator.
        /// </summary>
        ErrorInUnzipAlternate = -2147187453,

        /// <summary>
        /// The Microsoft Dynamics 365 record could not be updated
        /// </summary>
        ErrorInUpdate = -2147220648,

        /// <summary>
        /// The Microsoft Excel file name cannot contain the following characters: *  \ : &gt; &lt;
        /// </summary>
        ErrorInvalidFileNameChars = -2147159532,

        /// <summary>
        /// File type is not supported. Select an xml file for import.
        /// </summary>
        ErrorInvalidUIScriptImportFile = -2147159535,

        /// <summary>
        /// The server is busy handling other migration processes. Please try after some time.
        /// </summary>
        ErrorMigrationProcessExcessOnServer = -2147094476,

        /// <summary>
        /// The MimeType property value of the UploadFromBase64DataUIScriptRequest method is null or empty. Specify a valid property value, and try again.
        /// </summary>
        ErrorMimeTypeNullOrEmpty = -2147159531,

        /// <summary>
        /// Currently there's no active rule to route this case.
        /// </summary>
        ErrorNoActiveRoutingRuleExists = -2147157900,

        /// <summary>
        /// An error has occurred. Either the data does not exist or you do not have sufficient privileges to view the data. Contact your system administrator for help.
        /// </summary>
        ErrorNoQueryData = -2147159520,

        /// <summary>
        /// We can’t enable/disable the {0} feature for organization Id {1}. Exception details {2}.
        /// </summary>
        ErrorOnFeatureStatusChange = -2147204471,

        /// <summary>
        /// There was an error fetching a record for table {0}. Exception details {1}.
        /// </summary>
        ErrorOnGetRecord = -2147204474,

        /// <summary>
        /// We can’t get the Relationship Insights provisioning status for organization ID {0}. Exception details {1}.
        /// </summary>
        ErrorOnGetRIProvisionStatus = -2147204478,

        /// <summary>
        /// We can’t get the Relationship Insights tenant endpoint information for organization ID {0}. Exception details {1}.
        /// </summary>
        ErrorOnGetRITenantEndPoint = -2147204477,

        /// <summary>
        /// The query didn’t return all {0} columns.
        /// </summary>
        ErrorOnQryPropertyBagCollection = -2147204473,

        /// <summary>
        /// We can’t start provisioning for organization ID {0}. Exception details {1}.
        /// </summary>
        ErrorOnStartOfRIProvision = -2147204476,

        /// <summary>
        /// We can’t verify or update tenant information for organization ID {0}. Exception details {1}.
        /// </summary>
        ErrorOnTenantVerifyUpdate = -2147204475,

        /// <summary>
        /// {0} column for table {1} is missing.
        /// </summary>
        ErrorPropertyBagCollectionMissedColumn = -2147204472,

        /// <summary>
        /// After undeleting a label, there is no underlying label to reactivate.
        /// </summary>
        ErrorReactivatingComponentInstance = -2147160060,

        /// <summary>
        /// You cannot delete a UI script that is published. You must unpublish it first.
        /// </summary>
        ErrorScriptCannotDeletePublishedScript = -2147159543,

        /// <summary>
        /// You cannot update a UI script that is published. You must unpublish it first.
        /// </summary>
        ErrorScriptCannotUpdatePublishedScript = -2147159533,

        /// <summary>
        /// Error occurred while parsing the XML file.
        /// </summary>
        ErrorScriptFileParse = -2147159534,

        /// <summary>
        /// The initial statement for this script does not belong to this script.
        /// </summary>
        ErrorScriptInitialStatementNotInScript = -2147159545,

        /// <summary>
        /// The initial statement should the root statement and cannot have a previous statement set.
        /// </summary>
        ErrorScriptInitialStatementNotRoot = -2147159544,

        /// <summary>
        /// The language specified is not supported in your Dynamics 365 install. Please check with your system administrator on the list of "enabled" languages.
        /// </summary>
        ErrorScriptLanguageNotInstalled = -2147159546,

        /// <summary>
        /// The selected UI script cannot be published. The UI script contains one or more paths which do not end in an end-script or next-script action node. Correct the paths and try to publish again.
        /// </summary>
        ErrorScriptPublishMalformedScript = -2147159541,

        /// <summary>
        /// The selected UI script cannot be published. Provide a value for "First statement number" and try to publish again.
        /// </summary>
        ErrorScriptPublishMissingInitialStatement = -2147159542,

        /// <summary>
        /// You cannot create a UI script session for a UI script which is not published.
        /// </summary>
        ErrorScriptSessionCannotCreateForDraftScript = -2147159548,

        /// <summary>
        /// You cannot set the state of a UI script session for a UI script which is not published.
        /// </summary>
        ErrorScriptSessionCannotSetStateForDraftScript = -2147159539,

        /// <summary>
        /// You cannot update a UI script session for a UI script which is not published.
        /// </summary>
        ErrorScriptSessionCannotUpdateForDraftScript = -2147159547,

        /// <summary>
        /// You cannot associate the response control type for a statement which is not a prompt.
        /// </summary>
        ErrorScriptStatementResponseTypeOnlyForPrompt = -2147159538,

        /// <summary>
        /// This script is in use and has active sessions (status-reason=incomplete). Please terminate the active sessions (i.e. status-reason=cancelled) and try to unpublish again.
        /// </summary>
        ErrorScriptUnpublishActiveScript = -2147159540,

        /// <summary>
        /// Invalid File(s) for Email Router Migration
        /// </summary>
        ErrorsInEmailRouterMigrationFiles = -2147094478,

        /// <summary>
        /// Invalid File(s) for Import
        /// </summary>
        ErrorsInImportFiles = -2147220662,

        /// <summary>
        /// You can't activate this profile rule. You don't have the required permissions on the record types that are referenced by this profile rule.
        /// </summary>
        ErrorsInProfileRuleWorkflowActivation = -2147086055,

        /// <summary>
        /// You can't activate this service level agreement (SLA). You don't have the required permissions on the record types that are referenced by this SLA.
        /// </summary>
        ErrorsInSlaWorkflowActivation = -2147187403,

        /// <summary>
        /// The selected workflow has errors and cannot be published. Please open the workflow, remove the errors and try again.
        /// </summary>
        ErrorsInWorkflowDefinition = -2147187627,

        /// <summary>
        /// You cannot delete a UI script statement for a UI script which is not draft.
        /// </summary>
        ErrorStatementDeleteOnlyForDraftScript = -2147159536,

        /// <summary>
        /// You cannot create a UI script statement for a UI script which is not draft.
        /// </summary>
        ErrorStatementOnlyForDraftScript = -2147159537,

        /// <summary>
        /// {0}
        /// </summary>
        ErrorTemplate = -2147155710,

        /// <summary>
        /// The dialog that is being activated has no prompt/response.
        /// </summary>
        ErrorUIScriptPromptMissing = -2147159519,

        /// <summary>
        /// You cannot update this UI script statement text because it is being referred to by one or more published ui scripts.
        /// </summary>
        ErrorUpdateStatementTextIsReferenced = -2147159550,

        /// <summary>
        /// An error occurred while trying to add the report to Microsoft Dynamics 365. Try adding the report again. If this problem persists, contact your system administrator.
        /// </summary>
        ErrorUploadingReport = -2147188072,

        /// <summary>
        /// Event {0} is not supported for client side business rule.
        /// </summary>
        EventNotSupportedForBusinessRule = -2147090431,

        /// <summary>
        /// This combination of event type and control name is unexpected
        /// </summary>
        EventTypeAndControlNameAreMismatched = -2147090429,

        /// <summary>
        /// Database operation failed while creating authorization record for Evo STS.
        /// </summary>
        EvoStsAuthorizationServerRecordCreationFailureException = -2147020794,

        /// <summary>
        /// The custom entity limit has been reached.
        /// </summary>
        ExceedCustomEntityQuota = -2147176382,

        /// <summary>
        /// Cannot set user search facets for entity {0} as the limit for allowed facetable attributes is 4. Kindly remove few attributes to proceed.
        /// </summary>
        ExceededLimitForAllowedFacetableAttributes = -2147089658,

        /// <summary>
        /// You have exceeded the number of records you can follow. Please unfollow some records to start following again.
        /// </summary>
        ExceededNumberOfRecordsCanFollow = -2147158368,

        /// <summary>
        /// You can't add a rollup field with name {4} having id {3} for entity with name {2} and id {1}. You’ve reached the maximum number of {0} allowed for this record type.
        /// </summary>
        ExceededRollupFieldsPerEntityQuota = -2147089085,

        /// <summary>
        /// You can't add a rollup field. You’ve reached the maximum number of {0} allowed for your organization.
        /// </summary>
        ExceededRollupFieldsPerOrgQuota = -2147089086,

        /// <summary>
        /// The requested file was not found.
        /// </summary>
        ExcelFileNotFound = -2147088379,

        /// <summary>
        /// Excel Online file {0} was not updated by the Wopi Server within the timeout specified.
        /// </summary>
        ExcelOnlineNotUpdated = -2147088376,

        /// <summary>
        /// Autodiscover could not find the Exchange Web Services URL for the specified mailbox. Verify that the mailbox address and the credentials provided are correct and that Autodiscover is enabled and has been configured correctly.
        /// </summary>
        ExchangeAutodiscoverError = -2147200966,

        /// <summary>
        /// Attribute not present in exchange oData response.
        /// </summary>
        ExchangeCardAttributeMissingInResponseException = -2147020542,

        /// <summary>
        /// Invalid response format.
        /// </summary>
        ExchangeCardInvalidResponseFormatException = -2147020540,

        /// <summary>
        /// Server to Server Authentication with Exchange for Action Card is not set up.
        /// </summary>
        ExchangeCardS2SSetupFailureException = -2147020539,

        /// <summary>
        /// Exchange optin is not enabled.
        /// </summary>
        ExchangeOptinNotEnabled = -2147020538,

        /// <summary>
        /// The exchange rate of the base currency cannot be modified.
        /// </summary>
        ExchangeRateOfBaseCurrencyNotUpdatable = -2147185419,

        /// <summary>
        /// Workflow must be marked as on-demand or child workflow.
        /// </summary>
        ExecuteNotOnDemandWorkflow = -2147200954,

        /// <summary>
        /// Workflow must be in Published state.
        /// </summary>
        ExecuteUnpublishedWorkflow = -2147200953,

        /// <summary>
        /// The report could not be published for external use because a report of the same name already exists. Delete that report in SQL Server Reporting Services or rename this report, and try again.
        /// </summary>
        ExistingExternalReport = -2147220344,

        /// <summary>
        /// A parental relationship already exists.
        /// </summary>
        ExistingParentalRelationship = -2147188219,

        /// <summary>
        /// The series is already expanded for CutOffWindow.
        /// </summary>
        ExpansionRequestIsOutsideExpansionWindow = -2147163892,

        /// <summary>
        /// There should be a minimum of one Business rule step.
        /// </summary>
        ExpectingAtLeastOneBusinessRuleStep = -2147090415,

        /// <summary>
        /// The ticket specified for authentication is expired
        /// </summary>
        ExpiredAuthTicket = -2147180287,

        /// <summary>
        /// You can't activate an expired entitlement.
        /// </summary>
        ExpiredEntitlementActivate = -2147088873,

        /// <summary>
        /// The key specified to compute a hash value is expired, only active keys are valid.  Expired Key : {0}.
        /// </summary>
        ExpiredKey = -2147180282,

        /// <summary>
        /// The OAuth token has expired
        /// </summary>
        ExpiredOAuthToken = -2147213998,

        /// <summary>
        /// Version stamp associated with the client has expired. Please perform a full sync.
        /// </summary>
        ExpiredVersionStamp = -2147204270,

        /// <summary>
        /// The default solution cannot be exported as a package.
        /// </summary>
        ExportDefaultAsPackagedError = -2147188664,

        /// <summary>
        /// An error occurred while exporting a solution. Managed solutions cannot be exported.
        /// </summary>
        ExportManagedSolutionError = -2147188682,

        /// <summary>
        /// An error occurred while exporting a solution. The solution does not exist in this system.
        /// </summary>
        ExportMissingSolutionError = -2147188681,

        /// <summary>
        /// An error occurred while exporting a Solution.
        /// </summary>
        ExportSolutionError = -2147188683,

        /// <summary>
        /// Export to Excel Online feature is not enabled.
        /// </summary>
        ExportToExcelOnlineFeatureNotEnabled = -2147088380,

        /// <summary>
        /// Export to excel file feature is not enabled.
        /// </summary>
        ExportToXlsxFeatureNotEnabled = -2147088191,

        /// <summary>
        /// Rule contain an expression that is not supported by the editor.
        /// </summary>
        ExpressionNotSupportedForEditor = -2147090428,

        /// <summary>
        /// An entity with the specified name already exists for data source - {0}. Please specify a new external name.
        /// </summary>
        ExternalNameExists = -2147192945,

        /// <summary>
        /// The maximum number of indexed fields has been reached. Update the Relevance Search configuration to reduce the total number of indexed fields {1} below {0}.
        /// </summary>
        ExternalSearchAttributeLimitExceeded = -2147089664,

        /// <summary>
        /// Extra party information should not be provided for this operation.
        /// </summary>
        ExtraPartyInformation = -2147220714,

        /// <summary>
        /// Failed to deserialize async operation data.
        /// </summary>
        FailedToDeserializeAsyncOperationData = -2147204348,

        /// <summary>
        /// Failed to obtain the localized name for NetworkService account
        /// </summary>
        FailedToGetNetworkServiceName = -2147192573,

        /// <summary>
        /// Failed to load assembly
        /// </summary>
        FailedToLoadAssembly = -2147220914,

        /// <summary>
        /// Failed to schedule activity.
        /// </summary>
        FailedToScheduleActivity = -2147192832,

        /// <summary>
        /// Fail to delete the target connector from external partner.
        /// </summary>
        FailToDeleteConnectorFromExternalPartner = -2147015167,

        /// <summary>
        /// This operation can’t be completed. You must have at least one active Card form.
        /// </summary>
        FallbackCardFormDeactivation = -2147158428,

        /// <summary>
        /// This operation can’t be completed. You must have at least one active Main form.
        /// </summary>
        FallbackFormDeactivation = -2147158431,

        /// <summary>
        /// You cannot delete this form because it is the only fallback form of type {0} for the {1} entity. Each entity must have at least one fallback form for each form type.
        /// </summary>
        FallbackFormDeletion = -2147158444,

        /// <summary>
        /// This operation can’t be completed. You must have at least one active MainInteractionCentric form.
        /// </summary>
        FallbackMainInteractionCentricFormDeactivation = -2147158426,

        /// <summary>
        /// This operation can’t be completed. You must have at least one active Quick form.
        /// </summary>
        FallbackQuickFormDeactivation = -2147158427,

        /// <summary>
        /// The fax cannot be sent because there is no data to send. Specify at least one of the following: a cover page, a fax attachment, a fax description.
        /// </summary>
        FaxNoData = -2147207914,

        /// <summary>
        /// The fax cannot be sent because this type of attachment is not allowed or does not support virtual printing to a fax device.
        /// </summary>
        FaxNoSupport = -2147207913,

        /// <summary>
        /// The recipient is marked as "Do Not Fax".
        /// </summary>
        FaxSendBlocked = -2147207920,

        /// <summary>
        /// The Microsoft Windows fax service is not running or is not installed.
        /// </summary>
        FaxServiceNotRunning = -2147207919,

        /// <summary>
        /// This operation couldn't be completed because this feature isn’t enabled for your organization.
        /// </summary>
        FeatureNotEnabled = -2147086061,

        /// <summary>
        /// The username ADFS endpoint is enabled, which is blocking the intended authentication endpoint from being reached.
        /// </summary>
        FederatedEndpointError = -2147203835,

        /// <summary>
        /// Feedback feature is not enabled.
        /// </summary>
        FeedbackFeatureNotEnabled = -2147084432,

        /// <summary>
        /// The minimum and maximum values are required.
        /// </summary>
        FeedbackMinMaxRequired = -2147084430,

        /// <summary>
        /// The submitted minimum rating value {0} must be less than the submitted maximum rating value {1}.
        /// </summary>
        FeedbackMinRatingValue = -2147084428,

        /// <summary>
        /// The rating must be a value from {0} through {1}.
        /// </summary>
        FeedbackRatingValue = -2147084429,

        /// <summary>
        /// The fetch data set query timed out after {0} seconds. Increase the query timeout, and try again.
        /// </summary>
        FetchDataSetQueryTimeout = -2147098610,

        /// <summary>
        /// Field level security is not supported for virtual entity.
        /// </summary>
        FieldLevelSecurityNotSupported = -2147203049,

        /// <summary>
        /// Could not read the file because another application is using the file.
        /// </summary>
        FileInUse = -2147186633,

        /// <summary>
        /// The attachment file was not found.
        /// </summary>
        FileNotFound = -2147187648,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        FilePickerErrorApplicationInSnapView = -2147094003,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        FilePickerErrorAttachmentTypeBlocked = -2147094012,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        FilePickerErrorFileSizeBreached = -2147094011,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        FilePickerErrorFileSizeCannotBeZero = -2147094010,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        FilePickerErrorUnableToOpenFile = -2147094009,

        /// <summary>
        /// There was an error reading the file from the file system. Make sure you have read permission for this file, and then try migrating the file again.
        /// </summary>
        FileReadError = -2147187657,

        /// <summary>
        /// Unable to copy the documents. The selected file exceeds the maximium size limit of 128 MB.
        /// </summary>
        FileSizeExceeded = -2147020762,

        /// <summary>
        /// Feature not enabled for this organization
        /// </summary>
        FileStoreFeatureNotEnabled = -2147015392,

        /// <summary>
        /// The specified file type is not supported as template.
        /// </summary>
        FileTypeNotSupported = -2147087948,

        /// <summary>
        /// This customer is filtered due to AntiSpam settings.
        /// </summary>
        FilteredDuetoAntiSpam = -2147220699,

        /// <summary>
        /// This customer is filtered due to inactive state.
        /// </summary>
        FilteredDuetoInactiveState = -2147220694,

        /// <summary>
        /// This customer is filtered due to missing email address.
        /// </summary>
        FilteredDuetoMissingEmailAddress = -2147220690,

        /// <summary>
        /// Error creating or updating Business Process: final merged entity cannot be null.
        /// </summary>
        FinalMergedEntityIsNull = -2147089341,

        /// <summary>
        /// First Stage ID in traversed path ‘{0}’ does not match first Stage ID in Business Process ‘{1}’. Please contact your system administrator.
        /// </summary>
        FirstStageIdInTraversedPathDoesNotMatchFirstStageIdInBusinessProcess = -2147089322,

        /// <summary>
        /// For a goal of fiscal period type, the fiscal period attribute must be set.
        /// </summary>
        FiscalPeriodGoalMissingInfo = -2147202813,

        /// <summary>
        /// Fiscal settings have already been updated. They can be updated only once.
        /// </summary>
        FiscalSettingsAlreadyUpdated = -2147207159,

        /// <summary>
        /// Modern Flow must be active to be used on Flow Step.
        /// </summary>
        FlowIsNotActive = -2147089303,

        /// <summary>
        /// You need to select at least one record to trigger this flow.
        /// </summary>
        FlowMissingRecord = -2147155358,

        /// <summary>
        /// Flow client error returned with status code "{0}" and details "{1}".
        /// </summary>
        FlowServiceClientError = -2147089305,

        /// <summary>
        /// Flow trigger notifications are disabled for the organization.
        /// </summary>
        FlowTriggerNotificationDisabled = -2147015870,

        /// <summary>
        /// Flow trigger notification call failed during http post. Please check the exception for more details.
        /// </summary>
        FlowTriggerNotificationFailed = -2147015871,

        /// <summary>
        /// Folder doesn't exist.
        /// </summary>
        FolderDoesNotExist = -2147088127,

        /// <summary>
        /// The server refuses to fulfill the request.
        /// </summary>
        Forbidden = -2147094270,

        /// <summary>
        /// Form doesn't exist
        /// </summary>
        FormDoesNotExist = -2147187706,

        /// <summary>
        /// The import has failed because the system cannot transition the entity form {0} from unmanaged to managed. Add at least one full (root) component to the managed solution, and then try to import it again.
        /// </summary>
        FormTransitionError = -2147220926,

        /// <summary>
        /// A forward mailbox cannot be created for a specific user or a queue.  Please remove the regarding field and try again.
        /// </summary>
        ForwardMailboxCannotAssociateWithUser = -2147098105,

        /// <summary>
        /// An e-mail address is a required field in case of forward mailbox.
        /// </summary>
        ForwardMailboxEmailAddressRequired = -2147098095,

        /// <summary>
        /// Forward mailbox incoming delivery method can only be none or router.
        /// </summary>
        ForwardMailboxUnexpectedIncomingDeliveryMethod = -2147098094,

        /// <summary>
        /// Forward mailbox outgoing delivery method can only be none.
        /// </summary>
        ForwardMailboxUnexpectedOutgoingDeliveryMethod = -2147098093,

        /// <summary>
        /// Active Directory Error.
        /// </summary>
        GenericActiveDirectoryError = -2147214025,

        /// <summary>
        /// Azure Active Directory Error.
        /// </summary>
        GenericAzureActiveDirectoryError = -2147213996,

        /// <summary>
        /// Errors were encountered while processing the translations import file.
        /// </summary>
        GenericImportTranslationsError = -2147088558,

        /// <summary>
        /// The evaluation of the current component(name={0}, id={1}) in the current operation ({2}) failed during managed property evaluation of condition: {3}
        /// </summary>
        GenericManagedPropertyFailure = -2147160026,

        /// <summary>
        /// Sorry, something went wrong. Please try again, or restart the app.
        /// </summary>
        GenericMetadataSyncFailed = -2147093946,

        /// <summary>
        /// Sorry, something went wrong downloading server configuration changes.  You can continue to use the app with the older configuration, however you may experience problems including errors when saving.  If this issue continues please contact your Dynamics 365 administrator and provide the information available when you choose ‘more information’.
        /// </summary>
        GenericMetadataSyncFailedWithContinue = -2147093945,

        /// <summary>
        /// The transformation returned invalid data.
        /// </summary>
        GenericTransformationInvocationError = -2147220613,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        GetPhotoFromGalleryFailed = -2147094008,

        /// <summary>
        /// Error occurred while getting TenantId.
        /// </summary>
        GetTenantIdFailure = -2147020535,

        /// <summary>
        /// The Metric Detail for Specified Goal Attribute already exists.
        /// </summary>
        GoalAttributeAlreadyMapped = -2147203065,

        /// <summary>
        /// Goal Period Type needs to be specified when creating a goal. This field cannot be null.
        /// </summary>
        GoalMissingPeriodTypeInfo = -2147202808,

        /// <summary>
        /// The percentage achieved value has been set to 0 because the calculated value is not in the allowed range.
        /// </summary>
        GoalPercentageAchievedValueOutOfRange = -2147158398,

        /// <summary>
        /// Client was not able to download BCP file. Contact your system administrator for assistance and try going offline again.
        /// </summary>
        GoOfflineBCPFileSize = -2147204572,

        /// <summary>
        /// You have exceeded the storage limit for your offline database. You must reduce the amount of data to be taken offline by changing your Local Data Groups.
        /// </summary>
        GoOfflineDbSizeLimit = -2147204574,

        /// <summary>
        /// Data file for delete is empty.
        /// </summary>
        GoOfflineEmptyFileForDelete = -2147204560,

        /// <summary>
        /// Client was not able to download data. Contact your system administrator for assistance and try going offline again.
        /// </summary>
        GoOfflineFailedMoveData = -2147204571,

        /// <summary>
        /// Prepare MSDE failed. Contact your system administrator for assistance and try going offline again.
        /// </summary>
        GoOfflineFailedPrepareMsde = -2147204570,

        /// <summary>
        /// The Microsoft Dynamics 365 for Outlook was unable to go offline. Please try going offline again.
        /// </summary>
        GoOfflineFailedReloadMetadataCache = -2147204569,

        /// <summary>
        /// Data file was deleted on server before it was sent to client.
        /// </summary>
        GoOfflineFileWasDeleted = -2147204567,

        /// <summary>
        /// Dynamics 365 server was not able to process your request. Contact your system administrator for assistance and try going offline again.
        /// </summary>
        GoOfflineGetBCPFileException = -2147204575,

        /// <summary>
        /// Client and Server metadata versions are different due to new customization on the server. Please try going offline again.
        /// </summary>
        GoOfflineMetadataVersionsMismatch = -2147204576,

        /// <summary>
        /// Dynamics 365 server was not able to generate BCP file. Contact your system administrator for assistance and try going offline again.
        /// </summary>
        GoOfflineServerFailedGenerateBCPFile = -2147204573,

        /// <summary>
        /// Server to Server Authentication with Exchange for Office Graph Api  is not set up.
        /// </summary>
        GraphApiS2SSetupFailureException = -2147204512,

        /// <summary>
        /// The required globally unique identifier (GUID) in this row is not present
        /// </summary>
        GuidNotPresent = -2147220638,

        /// <summary>
        /// The column heading does not match the attribute display label.
        /// </summary>
        HeaderValueDoesNotMatchAttributeDisplayLabel = -2147220624,

        /// <summary>
        /// You can't create a property instance for a hidden property.
        /// </summary>
        HiddenPropertyValidationFailed = -2147086336,

        /// <summary>
        /// The hidden sheet is not available.
        /// </summary>
        HiddensheetNotAvailable = -2147087946,

        /// <summary>
        /// This operation couldn't be completed on this hierarchy. An error occurred while performing this operation for {0}. You can perform the operation separately on this product to fix the error, and then try the operation again for the complete hierarchy.
        /// </summary>
        HierarchicalOperationFailed = -2146955249,

        /// <summary>
        /// Calculations can't be performed online because the master record hierarchy depth limit of {0} has been reached.
        /// </summary>
        HierarchyCalculateLimitReached = -2147089081,

        /// <summary>
        /// Invalid Certificate for using HIP.
        /// </summary>
        HipInvalidCertificate = -2147160763,

        /// <summary>
        /// No Hip application configuration setting [{0}] was found.
        /// </summary>
        HipNoSettingError = -2147160764,

        /// <summary>
        /// SLA can be set to honor pause and resume only if Use SLA KPI is set to Yes.
        /// </summary>
        HonorPauseWithoutSLAKPIError = -2147201024,

        /// <summary>
        /// Certificate used for S2S authentication of Dynamics 365 Onpremise with Exchange Online has expired
        /// </summary>
        HybridSSSExchangeOnlineS2SCertActsExpired = -2146233088,

        /// <summary>
        /// Certificate used for S2S authentication of Dynamics 365 Onpremise with Exchange Online has expired
        /// </summary>
        HybridSSSExchangeOnlineS2SCertExpired = -2146233079,

        /// <summary>
        /// There was an error in parsing the article templates in Import Xml
        /// </summary>
        ImportArticleTemplateError = -2147188723,

        /// <summary>
        /// Invalid name for attribute {0}.  Custom attribute names must start with a valid customization prefix. The prefix for a solution component should match the prefix that is specified for the publisher of the solution.
        /// </summary>
        ImportAttributeNameError = -2147188638,

        /// <summary>
        /// An error occurred while importing Channel Property Group.
        /// </summary>
        ImportChannelPropertyGroupError = -2147088141,

        /// <summary>
        /// You cannot update this component because it does not exist in this Microsoft Dynamics 365 organization.
        /// </summary>
        ImportComponentDeletedIgnored = -2147187588,

        /// <summary>
        /// Cannot process with Bulk Import as Import Configuration not specified.
        /// </summary>
        ImportConfigNotSpecified = -2147220702,

        /// <summary>
        /// There was an error in parsing the contract templates in Import Xml
        /// </summary>
        ImportContractTemplateError = -2147188725,

        /// <summary>
        /// An error occurred while importing Convert Rules.
        /// </summary>
        ImportConvertRuleError = -2147157911,

        /// <summary>
        /// The solution file is invalid. The compressed file must contain the following files at its root: solution.xml, customizations.xml, and [Content_Types].xml. Customization files exported from previous versions of Microsoft Dynamics 365 are not supported.
        /// </summary>
        ImportCustomizationsBadZipFileError = -2147188640,

        /// <summary>
        /// A dashboard with the same id is marked as deleted in the system. Please first publish the system form entity and import again.
        /// </summary>
        ImportDashboardDeletedError = -2147163384,

        /// <summary>
        /// The package supplied for the default solution is trying to install it in managed mode. The default solution cannot be managed. In the XML for the default solution, set the Managed value back to "false" and try to import the solution again.
        /// </summary>
        ImportDefaultAsPackageError = -2147188663,

        /// <summary>
        /// {0} requires solutions that are not currently installed. Import the following solutions before Importing this one. {1}
        /// </summary>
        ImportDependencySolutionError = -2147188684,

        /// <summary>
        /// This import has failed because a different entity with the identical name, {0}, already exists in the target organization.
        /// </summary>
        ImportDuplicateEntity = -2147188468,

        /// <summary>
        /// There was an error in parsing the email templates in Import Xml
        /// </summary>
        ImportEmailTemplateError = -2147188724,

        /// <summary>
        /// E-mail Template '{0}' import: The attachment '{1}' was not found in the import zip file.
        /// </summary>
        ImportEmailTemplateErrorMissingFile = -2147188693,

        /// <summary>
        /// E-mail Template was not imported. The Template is a personal template on the target system; import cannot overwrite personal templates.
        /// </summary>
        ImportEmailTemplatePersonalError = -2147188716,

        /// <summary>
        /// Invalid Custom Resources in the Import File
        /// </summary>
        ImportEntityCustomResourcesError = -2147188734,

        /// <summary>
        /// Invalid Entity new string in the Custom Resources
        /// </summary>
        ImportEntityCustomResourcesNewStringError = -2147188733,

        /// <summary>
        /// Invalid Icon in the Import File
        /// </summary>
        ImportEntityIconError = -2147188735,

        /// <summary>
        /// The number of format parameters passed into the input string is incorrect
        /// </summary>
        ImportEntityNameMismatchError = -2147188728,

        /// <summary>
        /// The systemuser entity was imported but customized forms for the entity were not imported. Systemuser entity forms from on-premises or hosted versions of Microsoft Dynamics 365 cannot be imported into Microsoft Dynamics 365 Online.
        /// </summary>
        ImportEntitySystemUserLiveMismatchError = -2147188699,

        /// <summary>
        /// The systemuser entity was imported, but customized forms for the entity were not imported. Systemuser entity forms from Microsoft Dynamics 365 Online cannot be imported into on-premises or hosted versions of Microsoft Dynamics 365.
        /// </summary>
        ImportEntitySystemUserOnPremiseMismatchError = -2147188700,

        /// <summary>
        /// This message is no longer available. Please consult the SDK for alternative messages.
        /// </summary>
        ImportExportDeprecatedError = -2147188667,

        /// <summary>
        /// Some field security permissions could not be imported because the following fields are not in the system: {0}.
        /// </summary>
        ImportFieldSecurityProfileAttributesMissingError = -2147188636,

        /// <summary>
        /// Some field security permissions could not be imported because the following fields are not securable: {0}.
        /// </summary>
        ImportFieldSecurityProfileIsSecuredMissingError = -2147188637,

        /// <summary>
        /// The number of format parameters passed into the input string is incorrect
        /// </summary>
        ImportFieldXmlError = -2147188730,

        /// <summary>
        /// Import and extraction of the file failed.
        /// </summary>
        ImportFileFailed = -2147155675,

        /// <summary>
        /// The import file has an invalid digital signature.
        /// </summary>
        ImportFileSignatureInvalid = -2147188635,

        /// <summary>
        /// The import file is too large to upload.
        /// </summary>
        ImportFileTooLargeToUpload = -2147220619,

        /// <summary>
        /// The number of format parameters passed into the input string is incorrect
        /// </summary>
        ImportFormXmlError = -2147188729,

        /// <summary>
        /// An error occurred while importing generic entities.
        /// </summary>
        ImportGenericEntitiesError = -2147188704,

        /// <summary>
        /// The import failed. For more information, see the related error messages.
        /// </summary>
        ImportGenericError = -2147188706,

        /// <summary>
        /// A hierarchy rule with the same id is marked as deleted in the system,So first publish the customized entity and import again.
        /// </summary>
        ImportHierarchyRuleDeletedError = -2147157599,

        /// <summary>
        /// Cannot reuse existing hierarchy rule.
        /// </summary>
        ImportHierarchyRuleExistingError = -2147157598,

        /// <summary>
        /// There was an error processing hierarchy rules of the same object type code.(unresolvable system collision)
        /// </summary>
        ImportHierarchyRuleOtcMismatchError = -2147157597,

        /// <summary>
        /// Invalid Import File
        /// </summary>
        ImportInvalidFileError = -2147188736,

        /// <summary>
        /// This solution package cannot be imported because it contains invalid XML. You can attempt to repair the file by manually editing the XML contents using the information found in the schema validation errors, or you can contact your solution provider.
        /// </summary>
        ImportInvalidXmlError = -2147188692,

        /// <summary>
        /// There was an error parsing the IsvConfig during Import
        /// </summary>
        ImportIsvConfigError = -2147188722,

        /// <summary>
        /// Translated labels for the following languages could not be imported because they have not been enabled for this organization: {0}
        /// </summary>
        ImportLanguagesIgnoredError = -2147188698,

        /// <summary>
        /// The {0} mail merge template was not imported because the {1} entity associated with this template is not in the target system.
        /// </summary>
        ImportMailMergeTemplateEntityMissingError = -2147187584,

        /// <summary>
        /// There was an error in parsing the mail merge templates in Import Xml
        /// </summary>
        ImportMailMergeTemplateError = -2147187626,

        /// <summary>
        /// One or more of the selected data maps cannot be deleted because it is currently used in a data import.
        /// </summary>
        ImportMapInUse = -2147187611,

        /// <summary>
        /// The XML file has one or more invalid IDs. The specified ID cannot be used as a unique identifier.
        /// </summary>
        ImportMappingsInvalidIdSpecified = -2147187673,

        /// <summary>
        /// This customization file contains a reference to an entity map that does not exist on the target system.
        /// </summary>
        ImportMappingsMissingEntityMapError = -2147188720,

        /// <summary>
        /// Import cannot create system attribute mappings
        /// </summary>
        ImportMappingsSystemMapError = -2147188721,

        /// <summary>
        /// Cannot add a Root Component {0} of type {1} because it is not in the target system.
        /// </summary>
        ImportMissingComponent = -2147188705,

        /// <summary>
        /// The following solution cannot be imported: {0}. Some dependencies are missing.
        /// </summary>
        ImportMissingDependenciesError = -2147188707,

        /// <summary>
        /// The import has failed because component {0} of type {1} is not declared in the solution file as a root component. To fix this, import again using the XML file that was generated when you exported the solution.
        /// </summary>
        ImportMissingRootComponentEntry = -2147188678,

        /// <summary>
        /// An error occurred while importing Mobile Offline Profiles.
        /// </summary>
        ImportMobileOfflineProfileError = -2147087969,

        /// <summary>
        /// Existing plug-in types have been removed. Please update major or minor verion of plug-in assembly.
        /// </summary>
        ImportNewPluginTypesError = -2147188623,

        /// <summary>
        /// Invalid customization file. This file is not well formed.
        /// </summary>
        ImportNonWellFormedFileError = -2147188717,

        /// <summary>
        /// One or more imports are not in completed state. Imported records can only be deleted from completed jobs. Wait until job completes, and then try again.
        /// </summary>
        ImportNotComplete = -2147187598,

        /// <summary>
        /// One or more of the Import Child Jobs Failed
        /// </summary>
        ImportOperationChildFailure = -2147204300,

        /// <summary>
        /// Attribute '{0}' was not imported as it references a non-existing global Option Set ('{1}').
        /// </summary>
        ImportOptionSetAttributeError = -2147188679,

        /// <summary>
        /// An error occurred while importing OptionSets.
        /// </summary>
        ImportOptionSetsError = -2147188688,

        /// <summary>
        /// There was an error parsing the Organization Settings during Import.
        /// </summary>
        ImportOrgSettingsError = -2147188711,

        /// <summary>
        /// An error occurred while importing plug-in types.
        /// </summary>
        ImportPluginTypesError = -2147188718,

        /// <summary>
        /// The number of format parameters passed into the input string is incorrect
        /// </summary>
        ImportRelationshipRoleMapsError = -2147188726,

        /// <summary>
        /// The number of format parameters passed into the input string is incorrect
        /// </summary>
        ImportRelationshipRolesError = -2147188727,

        /// <summary>
        /// {0} cannot be imported. The {1} privilege is required to import this component.
        /// </summary>
        ImportRelationshipRolesPrivilegeError = -2147188689,

        /// <summary>
        /// An error occurred while importing Reports.
        /// </summary>
        ImportReportsError = -2147188686,

        /// <summary>
        /// Solution ID provided is restricted and cannot be imported.
        /// </summary>
        ImportRestrictedSolutionError = -2147160057,

        /// <summary>
        /// An error occurred while importing Ribbons.
        /// </summary>
        ImportRibbonsError = -2147188687,

        /// <summary>
        /// Cannot import security role. The role with specified role id is not updatable or role name is not unique.
        /// </summary>
        ImportRoleError = -2147188713,

        /// <summary>
        /// You do not have the necessary privileges to import security roles.
        /// </summary>
        ImportRolePermissionError = -2147188712,

        /// <summary>
        /// An error occurred while importing Routing Rule Sets.
        /// </summary>
        ImportRoutingRuleError = -2147157913,

        /// <summary>
        /// A saved query with the same id is marked as deleted in the system. Please first publish the customized entity and import again.
        /// </summary>
        ImportSavedQueryDeletedError = -2147188709,

        /// <summary>
        /// The number of format parameters passed into the input string is incorrect
        /// </summary>
        ImportSavedQueryExistingError = -2147188731,

        /// <summary>
        /// There was an error processing saved queries of the same object type code (unresolvable system collision)
        /// </summary>
        ImportSavedQueryOtcMismatchError = -2147188732,

        /// <summary>
        /// An error occurred while importing Sdk Messages.
        /// </summary>
        ImportSdkMessagesError = -2147188714,

        /// <summary>
        /// An error occurred while importing the Site Map.
        /// </summary>
        ImportSiteMapError = -2147188719,

        /// <summary>
        /// An error occurred while importing SLAs.
        /// </summary>
        ImportSlaError = -2147157912,

        /// <summary>
        /// An error occurred while importing a Solution.
        /// </summary>
        ImportSolutionError = -2147188685,

        /// <summary>
        /// ISV Config was overwritten.
        /// </summary>
        ImportSolutionIsvConfigWarning = -2147188670,

        /// <summary>
        /// Solution '{0}' already exists in this system as managed and cannot be upgraded.
        /// </summary>
        ImportSolutionManagedError = -2147188680,

        /// <summary>
        /// The solution is already installed on this system as an unmanaged solution and the package supplied is attempting to install it in managed mode. Import can only update solutions when the modes match. Uninstall the current solution and try again.
        /// </summary>
        ImportSolutionManagedToUnmanagedMismatch = -2147188672,

        /// <summary>
        /// Organization settings were overwritten.
        /// </summary>
        ImportSolutionOrganizationSettingsWarning = -2147188668,

        /// <summary>
        /// The solution package you are importing was generated on Microsoft Dynamics 365 Online, it cannot be imported into on-premises or hosted versions of Microsoft Dynamics 365.
        /// </summary>
        ImportSolutionPackageInvalidSku = -2147188629,

        /// <summary>
        /// You can only import solutions with a package version of {0} or earlier into this organization. Also, you can't import any solutions into this organization that were exported from Microsoft Dynamics 365 2011 or earlier.
        /// </summary>
        ImportSolutionPackageInvalidSolutionPackageVersion = -2147188632,

        /// <summary>
        /// Deprecated, not removing now as it might cause issues during integrations.
        /// </summary>
        ImportSolutionPackageMinimumVersionNeeded = 1,

        /// <summary>
        /// The solution package you are importing was generated on a different version of Microsoft Dynamics 365. The system will attempt to transform the package prior to import. Package Version: {0} {1}, System Version: {2} {3}.
        /// </summary>
        ImportSolutionPackageNeedsUpgrade = -2147188633,

        /// <summary>
        /// The solution package you are importing was generated on a version of Microsoft Dynamics 365 that cannot be imported into this system. Package Version: {0} {1}, System Version: {2} {3}.
        /// </summary>
        ImportSolutionPackageNotValid = -2147188634,

        /// <summary>
        /// Some components in the solution package you are importing require opt in. Opt in is available, please consult your administrator.
        /// </summary>
        ImportSolutionPackageRequiresOptInAvailable = -2147188631,

        /// <summary>
        /// The solution package you are importing was generated on a SKU of Microsoft Dynamics 365 that supports opt in. It cannot be imported in your system.
        /// </summary>
        ImportSolutionPackageRequiresOptInNotAvailable = -2147188630,

        /// <summary>
        /// SiteMap was overwritten.
        /// </summary>
        ImportSolutionSiteMapWarning = -2147188669,

        /// <summary>
        /// The solution is already installed on this system as a managed solution and the package supplied is attempting to install it in unmanaged mode. Import can only update solutions when the modes match. Uninstall the current solution and try again.
        /// </summary>
        ImportSolutionUnmanagedToManagedMismatch = -2147188671,

        /// <summary>
        /// System solution cannot be imported.
        /// </summary>
        ImportSystemSolutionError = -2147188666,

        /// <summary>
        /// You cannot import this template because its language is not enabled in your Microsoft Dynamics 365 organization.
        /// </summary>
        ImportTemplateLanguageIgnored = -2147187590,

        /// <summary>
        /// You cannot import this template because it is set as "personal" in your Microsoft Dynamics 365 organization.
        /// </summary>
        ImportTemplatePersonalIgnored = -2147187589,

        /// <summary>
        /// An error occurred while importing the translations. The solution associated with the translations does not exist in this system.
        /// </summary>
        ImportTranslationMissingSolutionError = -2147188665,

        /// <summary>
        /// The translation file is invalid. The compressed file must contain the following files at its root: {0}, [Content_Types].xml.
        /// </summary>
        ImportTranslationsBadZipFileError = -2147188639,

        /// <summary>
        /// A saved query visualization with id {0} is marked for deletion in the system. Please publish the customized entity first and then import again.
        /// </summary>
        ImportVisualizationDeletedError = -2147164141,

        /// <summary>
        /// A saved query visualization with id {0} already exists in the system, and cannot be resused by a new custom entity.
        /// </summary>
        ImportVisualizationExistingError = -2147164140,

        /// <summary>
        /// This import process is trying to import {0} new custom entities. This would exceed the custom entity limits for this organization.
        /// </summary>
        ImportWillExceedCustomEntityQuota = -2147176381,

        /// <summary>
        /// Cannot import workflow definition. Required attribute dependency is missing.
        /// </summary>
        ImportWorkflowAttributeDependencyError = -2147188702,

        /// <summary>
        /// Cannot import workflow definition. Required entity dependency is missing.
        /// </summary>
        ImportWorkflowEntityDependencyError = -2147188701,

        /// <summary>
        /// Cannot import workflow definition. The workflow with specified workflow id is not updatable or workflow name is not unique.
        /// </summary>
        ImportWorkflowError = -2147188703,

        /// <summary>
        /// Workflow {0} cannot be imported because a workflow with same name and different unique identifier exists in the target system. Change the name of this workflow, and then try again.
        /// </summary>
        ImportWorkflowNameConflictError = -2147188697,

        /// <summary>
        /// Workflow {0}({1}) cannot be imported because a workflow with same unique identifier is published on the target system. Unpublish the workflow on the target system before attempting to import this workflow again.
        /// </summary>
        ImportWorkflowPublishedError = -2147188696,

        /// <summary>
        /// The following managed solution cannot be imported: {0}. The publisher name cannot be changed from {1} to {2}.
        /// </summary>
        ImportWrongPublisherError = -2147188708,

        /// <summary>
        /// The import file is invalid. XSD validation failed with the following error: '{0}'. The validation failed at: '...{1} &lt;&lt;&lt;&lt;<error location="">&gt;&gt;&gt;&gt; {2}...'."</error>
        /// </summary>
        ImportXsdValidationError = -2147188710,

        /// <summary>
        /// Cannot reach to the smtp server. Please check that the smtp server is accessible.
        /// </summary>
        InaccessibleSmtpServer = -2147098073,

        /// <summary>
        /// Email server profile is disabled. Cannot process email for disabled profile.
        /// </summary>
        InactiveEmailServerProfile = -2147098072,

        /// <summary>
        /// The mailbox is in inactive state. Send/Receive mails are allowed only for active mailboxes.
        /// </summary>
        InactiveMailbox = -2147098087,

        /// <summary>
        /// An inactive metric cannot be set on a goal.
        /// </summary>
        InactiveMetricSetOnGoal = -2147158394,

        /// <summary>
        /// An inactive rollup query cannot be set on a goal.
        /// </summary>
        InactiveRollupQuerySetOnGoal = -2147158395,

        /// <summary>
        /// The incident can not be cancelled because there are open activities for this incident.
        /// </summary>
        IncidentCannotCancel = -2147204082,

        /// <summary>
        /// The contract does not have enough allotments. The case can not be created against this contract.
        /// </summary>
        IncidentContractDoesNotHaveAllotments = -2147204093,

        /// <summary>
        /// The allotment type for the contract is invalid.
        /// </summary>
        IncidentInvalidAllotmentType = -2147204085,

        /// <summary>
        /// The case can not be created against this contract line item because the contract line item is cancelled or expired.
        /// </summary>
        IncidentInvalidContractLineStateForCreate = -2147204083,

        /// <summary>
        /// The case can not be created against this contract because of the contract state.
        /// </summary>
        IncidentInvalidContractStateForCreate = -2147204096,

        /// <summary>
        /// Already Closed or Canceled
        /// </summary>
        IncidentIsAlreadyClosedOrCancelled = -2147204079,

        /// <summary>
        /// The incident id is missing.
        /// </summary>
        IncidentMissingActivityRegardingObject = -2147204087,

        /// <summary>
        /// The contract detail id is missing.
        /// </summary>
        IncidentMissingContractDetail = -2147204095,

        /// <summary>
        /// The timespent on the Incident is NULL or IncidentResolution Activity's IsBilled is NULL.
        /// </summary>
        IncidentNullSpentTimeOrBilled = -2147204084,

        /// <summary>
        /// Cannot poll mails from the mailbox. Its incoming delivery method is Forward mailbox.
        /// </summary>
        IncomingDeliveryIsForwardMailbox = -2147098077,

        /// <summary>
        /// The URL specified for Incoming Server Location uses HTTPS but the Use SSL for Incoming Connection option is set to No. Set this option to Yes, and then try again.
        /// </summary>
        IncomingServerLocationAndSslSetToNo = -2147098050,

        /// <summary>
        /// The URL specified for Incoming Server Location uses HTTP but the Use SSL for Incoming Connection option is set to Yes. Specify a server location that uses HTTPS, and then try again.
        /// </summary>
        IncomingServerLocationAndSslSetToYes = -2147098048,

        /// <summary>
        /// You can't enable the EnforceReadOnlyPlugins setting because plug-ins that change data are registered on read-only SDK messages. {0}
        /// </summary>
        IncompatibleStepsEncountered = -2147088245,

        /// <summary>
        /// One or more mandatory transformation parameters do not have mappings defined for them.
        /// </summary>
        IncompleteTransformationParameterMappingsFound = -2147220611,

        /// <summary>
        /// Detected inconsistent attribute name casing, expected: {0}, actual: {1}.
        /// </summary>
        InconsistentAttributeNameCasing = -2147159997,

        /// <summary>
        /// The other row for the product relationship is not available.
        /// </summary>
        InconsistentProductRelationshipState = -2147157610,

        /// <summary>
        /// Active stage is not on '{0}' entity.
        /// </summary>
        IncorrectActiveStageEntity = -2147089310,

        /// <summary>
        /// Invalid Attribute Value Type for {0}. Expected: {1}, Found: {2}
        /// </summary>
        IncorrectAttributeValueType = -2147204268,

        /// <summary>
        /// The entity set name {0} must start with a valid customization prefix.
        /// </summary>
        IncorrectEntitySetName = -2147088228,

        /// <summary>
        /// There should be two or more Entity Mappings defined when EntitiesPerFile in ImportMap is set to Multiple
        /// </summary>
        IncorrectSingleFileMultipleEntityMap = -2147187454,

        /// <summary>
        /// Increasing the number of days will cause a reset of mobile offline data and a resynchronization with mobile devices.
        /// </summary>
        IncreasingDaysWillResetMobileOfflineData = -2147087983,

        /// <summary>
        /// The index {0} is out of range for {1}. Number of elements present are {2}.
        /// </summary>
        IndexOutOfRange = -2147098616,

        /// <summary>
        /// Index size exceeded the size limit of {0} bytes. The key is too large. Try removing some columns or making the strings in string columns shorter.
        /// </summary>
        IndexSizeConstraintViolated = -2147088235,

        /// <summary>
        /// The operation could not be completed because you donot have read access on some of the fields in {0} record.
        /// </summary>
        InitializeErrorNoReadOnSource = -2147158016,

        /// <summary>
        /// Input parameter “{0}” does not match the input parameter field configured. Contact your system administrator to check the configuration metadata if the error persists.
        /// </summary>
        InputParameterFieldIncorrect = -2147089544,

        /// <summary>
        /// You can add option values only to picklist and status attributes.
        /// </summary>
        InsertOptionValueInvalidType = -2147204320,

        /// <summary>
        /// Cannot perform the operation. An instance is outside of series effective expansion range.
        /// </summary>
        InstanceOutsideEffectiveRange = -2147163883,

        /// <summary>
        /// User does not have read-write access to the Dynamics 365 organization.
        /// </summary>
        InsufficientAccessMode = -2147203838,

        /// <summary>
        /// The ticket specified for authentication didn't meet policy
        /// </summary>
        InsufficientAuthTicket = -2147180285,

        /// <summary>
        /// One or more columns required by the outer query are not available from the sub-query.
        /// </summary>
        InsufficientColumnsInSubQuery = -2147164126,

        /// <summary>
        /// External Party don't have sufficient privilege to create new record with given parameters.
        /// </summary>
        InsufficientCreatePrivilege = -2147086070,

        /// <summary>
        /// Support user does not have permission to perform this operation.
        /// </summary>
        InsufficientPrivilegesSupportUser = -2147187899,

        /// <summary>
        /// The owner of this queue does not have sufficient privileges to work with the queue.
        /// </summary>
        InsufficientPrivilegeToQueueOwner = -2147220192,

        /// <summary>
        /// External Party don't have sufficient privilege to retrieve record.
        /// </summary>
        InsufficientRetrievePrivilege = -2147086071,

        /// <summary>
        /// Insufficient parameters to execute transformation mapping.
        /// </summary>
        InsufficientTransformationParameters = -2147187450,

        /// <summary>
        /// External Party don't have sufficient privilege to update record.
        /// </summary>
        InsufficientUpdatePrivilege = -2147086069,

        /// <summary>
        /// A validation error occurred. An integer provided is outside of the allowed values for this attribute.
        /// </summary>
        IntegerValueOutOfRange = -2147204305,

        /// <summary>
        /// Integrated authentication is not allowed.
        /// </summary>
        IntegratedAuthenticationIsNotAllowed = -2147204351,

        /// <summary>
        /// The absolute url contains invalid characters. Please use a different name. Valid absolute url cannot ends with the following strings: .aspx, .ashx, .asmx, .svc
        /// </summary>
        InvalidAbsoluteUrlFormat = -2147188651,

        /// <summary>
        /// Invalid access mask is specified for team template.
        /// </summary>
        InvalidAccessMaskForTeamTemplate = -2147187915,

        /// <summary>
        /// The client access license cannot be changed because the user does not have a Microsoft Dynamics 365 Online license. To change the access mode, you must first add a license for this user in the Microsoft Online Service portal.
        /// </summary>
        InvalidAccessModeTransition = -2147213978,

        /// <summary>
        /// Invalid access rights.
        /// </summary>
        InvalidAccessRights = -2147220979,

        /// <summary>
        /// Invalid Access Rights passed.
        /// </summary>
        InvalidAccessRightsPassed = -2147187897,

        /// <summary>
        /// Invalid activityMimeAttachmentId.
        /// </summary>
        InvalidActivityMimeAttachmentId = -2147155963,

        /// <summary>
        /// A custom entity defined as an activity must be user or team owned.
        /// </summary>
        InvalidActivityOwnershipTypeMask = -2147159776,

        /// <summary>
        /// One or more activity parties have invalid addresses.
        /// </summary>
        InvalidActivityPartyAddress = -2147207912,

        /// <summary>
        /// An invalid object type was specified for distributing activities.
        /// </summary>
        InvalidActivityType = -2147220703,

        /// <summary>
        /// Must specify a valid CommunicationActivity
        /// </summary>
        InvalidActivityTypeForCampaignActivityPropagate = -2147220721,

        /// <summary>
        /// Cannot create activities of the specified list type.
        /// </summary>
        InvalidActivityTypeForList = -2147220731,

        /// <summary>
        /// Invalid Xml in an activity config file.
        /// </summary>
        InvalidActivityXml = -2147207916,

        /// <summary>
        /// Allotments: remaining + used != total
        /// </summary>
        InvalidAllotmentsCalc = -2147220241,

        /// <summary>
        /// Allotment overage is invalid.
        /// </summary>
        InvalidAllotmentsOverage = -2147204341,

        /// <summary>
        /// The allotments remaining is invalid
        /// </summary>
        InvalidAllotmentsRemaining = -2147220238,

        /// <summary>
        /// The total allotments is invalid
        /// </summary>
        InvalidAllotmentsTotal = -2147220240,

        /// <summary>
        /// The allotments used is invalid
        /// </summary>
        InvalidAllotmentsUsed = -2147220239,

        /// <summary>
        /// The resource type {0} cannot have an amount free value of {1}.
        /// </summary>
        InvalidAmountFreeResourceLimit = -2147176352,

        /// <summary>
        /// The service component {0} cannot have a provide {1} of resource type {2}.
        /// </summary>
        InvalidAmountProvided = -2147176403,

        /// <summary>
        /// The client type value passed is incorrect and not in the valid range.
        /// </summary>
        InvalidAppModuleClientType = -2147155674,

        /// <summary>
        /// The ID {0} doesn’t exist or isn’t valid for the component type “{1}”.
        /// </summary>
        InvalidAppModuleComponent = -2147155693,

        /// <summary>
        /// An app can’t reference the component type “{0}”.
        /// </summary>
        InvalidAppModuleComponentType = -2147155694,

        /// <summary>
        /// The event handlers provided for the app are invalid.
        /// </summary>
        InvalidAppModuleEventHandlers = -2147155665,

        /// <summary>
        /// The app ID is invalid or you don’t have access to the app.
        /// </summary>
        InvalidAppModuleId = -2147155690,

        /// <summary>
        /// The customized site map for this app module could not be used because it is configured incorrectly. To resolve this issue, navigate to the full experience to repair the customized site map and import it again.
        /// </summary>
        InvalidAppModuleSiteMap = -2147155696,

        /// <summary>
        /// The App Module SiteMap is invalid.
        /// </summary>
        InvalidAppModuleSiteMapXml = -2147155703,

        /// <summary>
        /// The unique name exceeds the maximum length of 40 characters or contains invalid characters. Only letters and numbers are allowed.
        /// </summary>
        InvalidAppModuleUniqueName = -2147155682,

        /// <summary>
        /// The app URL is not unique or the format is invalid.
        /// </summary>
        InvalidAppModuleUrl = -2147155686,

        /// <summary>
        /// Invalid appointment entity instance.
        /// </summary>
        InvalidAppointmentInstance = -2147163900,

        /// <summary>
        /// You are trying to approve an article that has a status of draft. You can only approve an article with the status of unapproved.
        /// </summary>
        InvalidApproveFromDraftArticle = -2147185155,

        /// <summary>
        /// You are trying to approve an article that has a status of published. You can only approve an article with the status of unapproved.
        /// </summary>
        InvalidApproveFromPublishedArticle = -2147185157,

        /// <summary>
        /// Invalid argument.
        /// </summary>
        InvalidArgument = -2147220989,

        /// <summary>
        /// The article state is undefined
        /// </summary>
        InvalidArticleState = -2147220229,

        /// <summary>
        /// This article state transition is invalid because of the current state of the article
        /// </summary>
        InvalidArticleStateTransition = -2147220228,

        /// <summary>
        /// The article template state is undefined
        /// </summary>
        InvalidArticleTemplateState = -2147220227,

        /// <summary>
        /// The given plugin assembly was built with an unsupported target platform and cannot be loaded.
        /// </summary>
        InvalidAssemblyProcessorArchitecture = -2147204738,

        /// <summary>
        /// The given plugin assembly source type is not supported for isolated plugin assemblies.
        /// </summary>
        InvalidAssemblySourceType = -2147204739,

        /// <summary>
        /// Invalid assignee id.
        /// </summary>
        InvalidAssigneeId = -2147220976,

        /// <summary>
        /// Selected saved query does not belong to associated entity of the mobile offline profile item.
        /// </summary>
        InvalidAssociatedSavedQuery = -2147087954,

        /// <summary>
        /// The compressed (.zip) file can't be uploaded because the folder "Attachments" contains one or more subfolders. Remove the subfolders and try again.
        /// </summary>
        InvalidAttachmentsFolder = -2147187568,

        /// <summary>
        /// Attribute {0} cannot be found for entity {1}.
        /// </summary>
        InvalidAttribute = -2147098615,

        /// <summary>
        /// Attribute data type: {0} is not valid for this entity.
        /// </summary>
        InvalidAttributeDataType = -2147203051,

        /// <summary>
        /// Attribute field type: {0} is not valid for virtual entity.
        /// </summary>
        InvalidAttributeFieldType = -2147203050,

        /// <summary>
        /// A dashboard Form XML cannot contain attribute: {0}.
        /// </summary>
        InvalidAttributeFound = -2147163389,

        /// <summary>
        /// Attribute - {0} in the XAML is invalid
        /// </summary>
        InvalidAttributeInXaml = -2147089390,

        /// <summary>
        /// InvalidAttributeMap Error Occurred
        /// </summary>
        InvalidAttributeMap = -2147196413,

        /// <summary>
        /// One or more attribute mappings is invalid.
        /// </summary>
        InvalidAttributeMapping = -2147187656,

        /// <summary>
        /// Attributes must be part of the requested EntityMetadata properties when an AttributeQuery is specified. Expect property = {0} in requested entity properties list.
        /// </summary>
        InvalidAttributeQuery = -2147015385,

        /// <summary>
        /// Organization Authentication does not match the current discovery service Role.
        /// </summary>
        InvalidAuth = -2147187434,

        /// <summary>
        /// The ticket specified for authentication didn't pass validation
        /// </summary>
        InvalidAuthTicket = -2147180288,

        /// <summary>
        /// Invalid Base attribute.
        /// </summary>
        InvalidBaseAttributeError = -2147204542,

        /// <summary>
        /// The base unit does not belong to the schedule.
        /// </summary>
        InvalidBaseUnit = -2147206389,

        /// <summary>
        /// The Behavior value of this attribute can't be changed.
        /// </summary>
        InvalidBehavior = -2147088223,

        /// <summary>
        /// The behavior of this Date and Time field can only be changed to “Date Only".
        /// </summary>
        InvalidBehaviorSelection = -2147088224,

        /// <summary>
        /// Browser not compatible to configure organization
        /// </summary>
        InvalidBrowserToConfigureOrganization = -2147167659,

        /// <summary>
        /// Invalid Business Process.
        /// </summary>
        InvalidBusinessProcess = -2147089527,

        /// <summary>
        /// Cannot switch ExecutionContext to system user without setting Caller first.
        /// </summary>
        InvalidCaller = -2147220905,

        /// <summary>
        /// The cascade link type is not valid for the cascade action.
        /// </summary>
        InvalidCascadeLinkType = -2147188220,

        /// <summary>
        /// Category is invalid. All the measures in the category either do not have same primary group by or are a mix of aggregate and non-aggregate data.
        /// </summary>
        InvalidCategory = -2147164151,

        /// <summary>
        /// The given certificate is invalid.
        /// </summary>
        InvalidCertificate = -2147098054,

        /// <summary>
        /// Invalid change process status request. Current process status is {0}, which cannot transition to {1}.
        /// </summary>
        InvalidChangeProcess = -2146885630,

        /// <summary>
        /// Cannot distribute activities for campaign activities of the specified channel type.
        /// </summary>
        InvalidChannelForCampaignActivityPropagate = -2147220720,

        /// <summary>
        /// An entitlement channel term with the same channel already exists. Specify a different channel and try again.
        /// </summary>
        InvalidChannelOrigin = -2147088894,

        /// <summary>
        /// The field '{0}' contains one or more invalid characters.
        /// </summary>
        InvalidCharactersInField = -2147220872,

        /// <summary>
        /// Reference Panel section can have only sub-grid, quick view form, knowledge base search, i-frame and HTML web resource controls. Found control with invalid classid {0}.
        /// </summary>
        InvalidClassIdInReferencePanelSection = -2147085053,

        /// <summary>
        /// An entity with that collection name already exists. Specify a unique name.
        /// </summary>
        InvalidCollectionName = -2147088242,

        /// <summary>
        /// ColumnMapping is Invalid. Check that the target attribute exists.
        /// </summary>
        InvalidColumnMapping = -2147220617,

        /// <summary>
        /// The column number specified in the data map does not exist.
        /// </summary>
        InvalidColumnNumber = -2147220682,

        /// <summary>
        /// Invalid command.
        /// </summary>
        InvalidCommand = -2147098368,

        /// <summary>
        /// The complex control id is invalid.
        /// </summary>
        InvalidComplexControlId = -2147098365,

        /// <summary>
        /// The connection string not found or invalid.
        /// </summary>
        InvalidConnectionString = -2147220929,

        /// <summary>
        /// The Contract detail id is invalid
        /// </summary>
        InvalidContractDetailId = -2147220234,

        /// <summary>
        /// The dashboard Form XML cannot contain controls elements with class id: {0}.
        /// </summary>
        InvalidControlClass = -2147163385,

        /// <summary>
        /// The ConversionRule specified {0} is invalid. Please specify a valid ConversionRule.
        /// </summary>
        InvalidConversionRule = -2147088138,

        /// <summary>
        /// You cannot create {0} {1}. Creation cannot be performed when {0} is managed.
        /// </summary>
        InvalidCreateOnProtectedComponent = -2147160047,

        /// <summary>
        /// For a POP3 email server type, you can only connect using credentials that are specified by a user or queue.
        /// </summary>
        InvalidCredentialTypeForNonExchangeIncomingConnection = -2147098092,

        /// <summary>
        /// Invalid CrmDateTime.
        /// </summary>
        InvalidCrmDateTime = -2147163901,

        /// <summary>
        /// Invalid cross-entity stage transition. Target entity must be specified.
        /// </summary>
        InvalidCrossEntityOperation = -2146885628,

        /// <summary>
        /// Invalid cross-entity stage transition. Specified target must match {0}.
        /// </summary>
        InvalidCrossEntityTargetOperation = -2146885627,

        /// <summary>
        /// The currency is invalid.
        /// </summary>
        InvalidCurrency = -2147185412,

        /// <summary>
        /// A custom entity defined as an activity must be of communicaton activity type.
        /// </summary>
        InvalidCustomActivityType = -2147159771,

        /// <summary>
        /// You can’t set custom download filters because Record Distribution Criteria isn’t set to Other Data Filters.
        /// </summary>
        InvalidCustomDataDownloadFilters = -2147087978,

        /// <summary>
        /// The customer is invalid.
        /// </summary>
        InvalidCustomer = -2147220947,

        /// <summary>
        /// Invalid wizard xml
        /// </summary>
        InvalidCustomReportingWizardXml = -2147220335,

        /// <summary>
        /// The data description for the visualization is invalid.
        /// </summary>
        InvalidDataDescription = -2147164160,

        /// <summary>
        /// For an entity owned by the Business Owner, you can only use the following data download filters: All records or Download related data only.
        /// </summary>
        InvalidDataDownloadFilterBusinessUnit = -2147093982,

        /// <summary>
        /// For an entity owned by the Organization, you can only use the following data download filters: All records or Download related data only.
        /// </summary>
        InvalidDataDownloadFilterOrganization = -2147093981,

        /// <summary>
        /// You can’t set Records Owned By Me or Records Owned By My Team for business unit-owned entities.
        /// </summary>
        InvalidDataFiltersForBUOwnedEntities = -2147087980,

        /// <summary>
        /// You can’t set the Other Data filter for organization-owned entities.
        /// </summary>
        InvalidDataFiltersForOrgOwnedEntities = -2147087979,

        /// <summary>
        /// You can’t set the All Record or Other Data filters for unowned entities.
        /// </summary>
        InvalidDataFiltersForUnownedEntities = -2147087981,

        /// <summary>
        /// The source data is not in the required format
        /// </summary>
        InvalidDataFormat = -2147220650,

        /// <summary>
        /// Invalid URI: A fully qualified URI without a query string must be provided.
        /// </summary>
        InvalidDataSourceEndPoint = -2147203034,

        /// <summary>
        /// Invalid data xml.
        /// </summary>
        InvalidDataXml = -2147098367,

        /// <summary>
        /// Date Attribute specified is not an attribute of Source Entity.
        /// </summary>
        InvalidDateAttribute = -2147203067,

        /// <summary>
        /// The date-time format is invalid, or value is outside the supported range.
        /// </summary>
        InvalidDateTime = -2147220935,

        /// <summary>
        /// You can’t change the format value of this attribute to “Date and Time” when the behavior is “Date Only.”
        /// </summary>
        InvalidDateTimeFormat = -2147088222,

        /// <summary>
        /// February 29 can occur only when pattern start date is in a leap year.
        /// </summary>
        InvalidDaysInFebruary = -2147163868,

        /// <summary>
        /// You can’t deactivate {0} forms. Only Main forms can be inactive.
        /// </summary>
        InvalidDeactivateFormType = -2147158432,

        /// <summary>
        /// A system relationship's delete cascading action cannot be modified.
        /// </summary>
        InvalidDeleteModification = -2147188221,

        /// <summary>
        /// You cannot delete {0} {1}. Deletion cannot be performed when {0} is managed.
        /// </summary>
        InvalidDeleteOnProtectedComponent = -2147160045,

        /// <summary>
        /// This process can't be deleted because it is a system-generated process.
        /// </summary>
        InvalidDeleteProcess = -2147088751,

        /// <summary>
        /// The {2} component {1} (Id={0}) does not exist.  Failure trying to associate it with {3} (Id={4}) as a dependency. Missing dependency lookup type = {5}.
        /// </summary>
        InvalidDependency = -2147160010,

        /// <summary>
        /// The required component {1} (Id={0}) that was defined for the {2} could not be found in the system.
        /// </summary>
        InvalidDependencyComponent = -2147160000,

        /// <summary>
        /// The required component {1} (Name={0}) that was defined for the {2} could not be found in the system.
        /// </summary>
        InvalidDependencyEntity = -2147159999,

        /// <summary>
        /// The FetchXml ({2}) is invalid.  Failure while calculating dependencies for {1} (Id={0}).
        /// </summary>
        InvalidDependencyFetchXml = -2147160009,

        /// <summary>
        /// Mobile device cannot be used to configured organization
        /// </summary>
        InvalidDeviceToConfigureOrganization = -2147167660,

        /// <summary>
        /// The specified display name is not valid
        /// </summary>
        InvalidDisplayName = -2147192820,

        /// <summary>
        /// Invalid document template.
        /// </summary>
        InvalidDocumentTemplate = -2147088181,

        /// <summary>
        /// The domain logon for this user is invalid. Select another domain logon and try again.
        /// </summary>
        InvalidDomainName = -2147188715,

        /// <summary>
        /// The presentation description is not valid for dundas chart.
        /// </summary>
        InvalidDundasPresentationDescription = -2147164138,

        /// <summary>
        /// A dashboard Form XML cannot contain element: {0}.
        /// </summary>
        InvalidElementFound = -2147163392,

        /// <summary>
        /// Email generated from the template is not valid
        /// </summary>
        InvalidEmail = -2147176426,

        /// <summary>
        /// Invalid e-mail address. For more information, contact your system administrator.
        /// </summary>
        InvalidEmailAddressFormat = -2147204718,

        /// <summary>
        /// The email address in the mailbox is not correct. Please enter the correct email address to process mails.
        /// </summary>
        InvalidEmailAddressInMailbox = -2147098079,

        /// <summary>
        /// The server location is either not present or is not valid. Please correct the server location.
        /// </summary>
        InvalidEmailServerLocation = -2147098088,

        /// <summary>
        /// Must specify a valid Template Id
        /// </summary>
        InvalidEmailTemplate = -2147220717,

        /// <summary>
        /// You can't activate an expired, waiting or canceled entitlement.
        /// </summary>
        InvalidEntitlementActivate = -2147088890,

        /// <summary>
        /// You can't create a case for this entitlement because there are no available terms.
        /// </summary>
        InvalidEntitlementAssociationToCase = -2147088887,

        /// <summary>
        /// You can't cancel an entitlement that's in the Draft or Expired state.
        /// </summary>
        InvalidEntitlementCancel = -2147088889,

        /// <summary>
        /// Total terms for a specific case origin on an entitlement channel cannot be more than the total terms of the corresponding entitlement.
        /// </summary>
        InvalidEntitlementChannelTerms = -2147088891,

        /// <summary>
        /// The specified contact isn’t associated with the selected customer.
        /// </summary>
        InvalidEntitlementContacts = -2147204601,

        /// <summary>
        /// You can deactivate only entitlements that are active or waiting
        /// </summary>
        InvalidEntitlementDeactivate = -2147088888,

        /// <summary>
        /// You can't set an entitlement to the Expired state. Active entitlements automatically expire when their end date passes.
        /// </summary>
        InvalidEntitlementExpire = -2147088872,

        /// <summary>
        /// Select an active entitlement that belongs to the specified customer, contact, or product, and then try again.
        /// </summary>
        InvalidEntitlementForSelectedCustomerOrProduct = -2147157914,

        /// <summary>
        /// You can renew only the entitlements that are expired or canceled.
        /// </summary>
        InvalidEntitlementRenew = -2147088880,

        /// <summary>
        /// You can only associate a case with an active entitlement.
        /// </summary>
        InvalidEntitlementStateAssociateToCase = -2147088879,

        /// <summary>
        /// Total terms for an entitlement cannot be less than the total terms of any of its corresponding EntitlementChannels.
        /// </summary>
        InvalidEntitlementTotalTerms = -2147204097,

        /// <summary>
        /// Entity {0} cannot be found.
        /// </summary>
        InvalidEntity = -2147098612,

        /// <summary>
        /// Invalid entity class.
        /// </summary>
        InvalidEntityClassException = -2147220919,

        /// <summary>
        /// Entity For Date Attribute can be either source entity or its parent.
        /// </summary>
        InvalidEntityForDateAttribute = -2147203054,

        /// <summary>
        /// Not a valid entity for linked attribute.
        /// </summary>
        InvalidEntityForLinkedAttribute = -2147159811,

        /// <summary>
        /// The entity {0} is not a valid entity for rollup.
        /// </summary>
        InvalidEntityForRollup = -2147203053,

        /// <summary>
        /// Invalid EntityKey Operation performed : {0}
        /// </summary>
        InvalidEntityKeyOperation = -2147088241,

        /// <summary>
        /// Entity name '{0}' is not a valid logical name.
        /// </summary>
        InvalidEntityLogicalName = -2147015533,

        /// <summary>
        /// The record type does not match the base record type and the matching record type of the duplicate detection rule.
        /// </summary>
        InvalidEntityName = -2147187690,

        /// <summary>
        /// An entity with the specified entity set name {0} already exists. Specify a unique name.
        /// </summary>
        InvalidEntitySetName = -2147088229,

        /// <summary>
        /// The entity is not specified in the template.
        /// </summary>
        InvalidEntitySpecified = -2147087951,

        /// <summary>
        /// The exchange rate is invalid.
        /// </summary>
        InvalidExchangeRate = -2147185411,

        /// <summary>
        /// Failed to export Business Process “{0}” because solution does not include corresponding Business Process entity “{1}”. If this is a newly created Business Process in Draft state, activate it once to generate the Business Process entity and include it in the solution. For more information, see http://support.microsoft.com/kb/4337537.
        /// </summary>
        InvalidExportProcessFlowNotActivated = -2147089546,

        /// <summary>
        /// The specified External Collection name is not valid.
        /// </summary>
        InvalidExternalCollectionName = -2147193945,

        /// <summary>
        /// The specified External name is not valid.
        /// </summary>
        InvalidExternalName = -2147193920,

        /// <summary>
        /// Multiple External Party Items are present for request parameters.
        /// </summary>
        InvalidExternalPartyConfiguration = -2147086065,

        /// <summary>
        /// External Party is not allowed.
        /// </summary>
        InvalidExternalPartyOperation = -2147086063,

        /// <summary>
        /// External Party has invalid parent attribute.
        /// </summary>
        InvalidExternalPartyParent = -2147086064,

        /// <summary>
        /// The feature type isn’t valid.
        /// </summary>
        InvalidFeatureType = -2147204494,

        /// <summary>
        /// The fetch collection for the visualization is invalid.
        /// </summary>
        InvalidFetchCollection = -2147164135,

        /// <summary>
        /// Malformed FetchXml.
        /// </summary>
        InvalidFetchXml = -2147220733,

        /// <summary>
        /// The file could not be uploaded because it contains invalid character(s)
        /// </summary>
        InvalidFileBadCharacters = -2147220586,

        /// <summary>
        /// Invalid File Type.
        /// </summary>
        InvalidFileType = -2147088180,

        /// <summary>
        /// The visualization cannot be rendered for the given filter criteria.
        /// </summary>
        InvalidFilterCriteriaForVisualization = -2147164130,

        /// <summary>
        /// The fiscal period {0} does not fall in the permitted range of fiscal periods as per organization's fiscal settings.
        /// </summary>
        InvalidFiscalPeriod = -2147203052,

        /// <summary>
        /// Flow clientdata is in invalid format. Details: "{0}".
        /// </summary>
        InvalidFlowProcessClientData = -2147089304,

        /// <summary>
        /// Invalid Precision Parameter specified for control {0}. It Dosent Contain Expected Value
        /// </summary>
        InvalidFormatForControl = -2147088267,

        /// <summary>
        /// Mismatched data delimiter: only one delimiter was found.
        /// </summary>
        InvalidFormatForDataDelimiter = -2147220651,

        /// <summary>
        /// The file that you uploaded is invalid and cannot be used for updating records.
        /// </summary>
        InvalidFormatForUpdateMode = -2147158527,

        /// <summary>
        /// The number of format parameters passed into the input string is incorrect
        /// </summary>
        InvalidFormatParameters = -2147192575,

        /// <summary>
        /// The type of the form must be set to {0} in the Form XML.
        /// </summary>
        InvalidFormType = -2147163386,

        /// <summary>
        /// "Invalid Formtype used in Create call
        /// </summary>
        InvalidFormTypeCalledThroughSdk = -2147088268,

        /// <summary>
        /// One or both entities are not enabled for officegraph and they cannot be used for officegraph.
        /// </summary>
        InvalidForOfficeGraph = -2147204559,

        /// <summary>
        /// Goal Attribute does not match the specified metric type.
        /// </summary>
        InvalidGoalAttribute = -2147203061,

        /// <summary>
        /// The manager of a goal can only be a user and not a team.
        /// </summary>
        InvalidGoalManager = -2147158396,

        /// <summary>
        /// The Granularity column value is incorrect. Each rule part must be a name-value pair separated by an equal sign (=). For example: FREQ=Minutes;INTERVAL=15
        /// </summary>
        InvalidGranularityValue = -2147176392,

        /// <summary>
        /// Data Description is invalid. Same group by alias cannot be used for different attributes.
        /// </summary>
        InvalidGroupByAlias = -2147164145,

        /// <summary>
        /// Group by not allowed on the attribute.
        /// </summary>
        InvalidGroupByColumn = -2147164131,

        /// <summary>
        /// The globally unique identifier (GUID) in this row is invalid
        /// </summary>
        InvalidGuid = -2147220637,

        /// <summary>
        /// Guid - {0} in the Xaml is not valid
        /// </summary>
        InvalidGuidInXaml = -2147089401,

        /// <summary>
        /// The column heading contains an invalid combination of data delimiters.
        /// </summary>
        InvalidHeaderColumn = -2147220668,

        /// <summary>
        /// Only hexadecimal values are allowed.
        /// </summary>
        InvalidHexColorValue = -2147088176,

        /// <summary>
        /// This relationship is not self-referential and therefore cannot be made hierarchical.
        /// </summary>
        InvalidHierarchicalRelationship = -2147192801,

        /// <summary>
        /// You can’t change this entity’s hierarchy because the {0} hierarchical relationship can’t be customized.
        /// </summary>
        InvalidHierarchicalRelationshipChange = -2147192806,

        /// <summary>
        /// The content of the import file is not valid. You must select a text file.
        /// </summary>
        InvalidImportFileContent = -2147220620,

        /// <summary>
        /// The data is not in the required format
        /// </summary>
        InvalidImportFileData = -2147220655,

        /// <summary>
        /// Field and data delimiters for this file are not specified.
        /// </summary>
        InvalidImportFileParseData = -2147220663,

        /// <summary>
        /// The requested importjob does not exist.
        /// </summary>
        InvalidImportJobId = -2147204526,

        /// <summary>
        /// The ImportJobTemplate.xml file is invalid.
        /// </summary>
        InvalidImportJobTemplateFile = -2147204527,

        /// <summary>
        /// The incoming delivery method is not email connector. To receive mails its incoming delivery method should be Email Connector.
        /// </summary>
        InvalidIncomingDeliveryExpectingEmailConnector = -2147098076,

        /// <summary>
        /// Invalid instance entity name.
        /// </summary>
        InvalidInstanceEntityName = -2147163891,

        /// <summary>
        /// Invalid instance type code.
        /// </summary>
        InvalidInstanceTypeCode = -2147163897,

        /// <summary>
        /// You have reached the maximum number of interactive/full users.
        /// </summary>
        InvalidInteractiveUserQuota = -2147176375,

        /// <summary>
        /// Cannot use existing non intersect entity {0} as an intersect entity for defining many to many relationships.
        /// </summary>
        InvalidIntersectEntity = -2147015360,

        /// <summary>
        /// A user with this e-mail address was not found. Sign in to Windows Live ID with the same e-mail address where you received the invitation.  If you do not have a Windows Live ID, please create one using that e-mail address.
        /// </summary>
        InvalidInvitationLiveId = -2147167730,

        /// <summary>
        /// The invitation token {0} is not correctly formatted.
        /// </summary>
        InvalidInvitationToken = -2147167731,

        /// <summary>
        /// The first row of the file does not contain column headings.
        /// </summary>
        InvalidIsFirstRowHeaderForUseSystemMap = -2147220636,

        /// <summary>
        /// Invalid ISO currency code.
        /// </summary>
        InvalidIsoCurrencyCode = -2147185422,

        /// <summary>
        /// Keys must be part of the requested EntityMetadata properties when a KeyQuery is specified. Expect property = {0} in requested entity properties list.
        /// </summary>
        InvalidKeyQuery = -2147015383,

        /// <summary>
        /// The product is not a kit.
        /// </summary>
        InvalidKit = -2147206403,

        /// <summary>
        /// You cannot add a product kit to itself. Select a different product or product kit.
        /// </summary>
        InvalidKitProduct = -2147206402,

        /// <summary>
        /// The specified language code is not valid for this organization.
        /// </summary>
        InvalidLanguageCode = -2147204715,

        /// <summary>
        /// Rows with localizable attributes can only be created when the user interface (UI) language for the current user is set to the organization's base language.
        /// </summary>
        InvalidLanguageForCreate = -2147088560,

        /// <summary>
        /// Process configuration is not available since your language does not match system base language.
        /// </summary>
        InvalidLanguageForProcessConfiguration = -2147098366,

        /// <summary>
        /// Solution and Publisher Options are not available since your language does not match system base language.
        /// </summary>
        InvalidLanguageForSolution = -2147192807,

        /// <summary>
        /// Localizable attributes can only be updated via the string property when the user interface (UI) language for the current user is set to the organization's base language. Use SetLocLabels to update the localized values for the following attributes: [{0}].
        /// </summary>
        InvalidLanguageForUpdate = -2147088559,

        /// <summary>
        /// Invalid license. MPC code cannot be read from MPC.txt file with this path {0}.
        /// </summary>
        InvalidLicenseCannotReadMpcFile = -2147167675,

        /// <summary>
        /// Invalid license key ({0}).
        /// </summary>
        InvalidLicenseKey = -2147167680,

        /// <summary>
        /// Invalid license. Invalid MPC code ({0}).
        /// </summary>
        InvalidLicenseMpcCode = -2147167674,

        /// <summary>
        /// Invalid license. Invalid PID (Product Id) ({0}).
        /// </summary>
        InvalidLicensePid = -2147167678,

        /// <summary>
        /// Invalid license. PidGen.dll cannot be loaded from this path {0}
        /// </summary>
        InvalidLicensePidGenCannotLoad = -2147167677,

        /// <summary>
        /// Invalid license. Cannot generate PID (Product Id) from License key. PidGen error code ({0}).
        /// </summary>
        InvalidLicensePidGenOtherError = -2147167676,

        /// <summary>
        /// Language with Locale ID {0}, does not exist
        /// </summary>
        InvalidLocaleIdForKnowledgeArticle = -2147085312,

        /// <summary>
        /// Invalid logo image web resource id.
        /// </summary>
        InvalidLogoImageId = -2147088173,

        /// <summary>
        /// Invalid WebResource Type for Logo Image.
        /// </summary>
        InvalidLogoImageWebResourceType = -2147088167,

        /// <summary>
        /// The lookup entity provided is not valid for the given target attribute.
        /// </summary>
        InvalidLookupMapNode = -2147187583,

        /// <summary>
        /// Invalid mailboxId passed in. Please check the mailboxid.
        /// </summary>
        InvalidMailbox = -2147098089,

        /// <summary>
        /// Managed property {0} does not contain enough information to be created.  Please provide (assembly, class), or (entity, attribute) or set the managed property to custom.
        /// </summary>
        InvalidManagedPropertyException = -2147160016,

        /// <summary>
        /// Failed to locate the manifest file in the specified location
        /// </summary>
        InvalidManifestFilePath = -2147187405,

        /// <summary>
        /// Invalid Matching attribute.
        /// </summary>
        InvalidMatchingAttributeError = -2147204540,

        /// <summary>
        /// The resource type {0} cannot have a maximum limit of {1}.
        /// </summary>
        InvalidMaximumResourceLimit = -2147176405,

        /// <summary>
        /// Invalid MaxLength Parameter specified for control {0}.Maxlength must be in between {1} and {2} .
        /// </summary>
        InvalidMaxLengthForControl = -2147088263,

        /// <summary>
        /// Invalid MaxValue Parameter specified for control {0}.Max Value must be in between {1} and {2} .
        /// </summary>
        InvalidMaxValueForControl = -2147088261,

        /// <summary>
        /// Measure collection is invalid. Not all the measures in the measure collection have the same group bys.
        /// </summary>
        InvalidMeasureCollection = -2147164150,

        /// <summary>
        /// Invalid Metadata.
        /// </summary>
        InvalidMetadata = -2147220934,

        /// <summary>
        /// SQL exception has been thrown on current metadata operation. Please check the exception for more details.
        /// </summary>
        InvalidMetadataSqlOperation = -2147015869,

        /// <summary>
        /// The content of the import file is not valid. You must select a text file.
        /// </summary>
        InvalidMigrationFileContent = -2147094477,

        /// <summary>
        /// Invalid MinValue and MaxValue Parameter specified for control {0}.Min Value must be less than Max Value .
        /// </summary>
        InvalidMinAndMaxValueForControl = -2147088260,

        /// <summary>
        /// The resource type {0} cannot have a minimum limit of {1}.
        /// </summary>
        InvalidMinimumResourceLimit = -2147176406,

        /// <summary>
        /// Invalid MinValue Parameter specified for control {0}.Min Value must be in between {1} and {2} .
        /// </summary>
        InvalidMinValueForControl = -2147088262,

        /// <summary>
        /// XML Format mismatch. Check for the correctness of XML.
        /// </summary>
        InvalidMobileOfflineFiltersFetchXml = -2147020525,

        /// <summary>
        /// A source field is mapped to more than one Dynamics 365 fields of lookup/picklist type.
        /// </summary>
        InvalidMultipleMapping = -2147187560,

        /// <summary>
        /// An app can’t have multiple site maps.
        /// </summary>
        InvalidMultipleSiteMapReferenceSingleAppModule = -2147155695,

        /// <summary>
        /// The schema name {0} for type {2} is invalid or missing.Custom attribute, entity, entitykey, option set and relationship names must start with a valid customization prefix.The prefix for a solution component should match the prefix that is specified for the publisher of the solution.
        /// </summary>
        InvalidNamePrefix = -2147204250,

        /// <summary>
        /// The net price is invalid
        /// </summary>
        InvalidNetPrice = -2147220237,

        /// <summary>
        /// You have reached the maximum number of non-interactive users/
        /// </summary>
        InvalidNonInteractiveUserQuota = -2147176368,

        /// <summary>
        /// Invalid input string for numbergroupformat. The input string should contain an array of integers. Every element in the value array should be between one and nine, except for the last element, which can be zero.
        /// </summary>
        InvalidNumberGroupFormat = -2147207424,

        /// <summary>
        /// Number of sections in a card form must be 4. Found {0}.
        /// </summary>
        InvalidNumberOfCardFormSections = -2147085051,

        /// <summary>
        /// You cannot delete audit data in the partitions that are currently in use, or delete the partitions that are created for storing future audit data.
        /// </summary>
        InvalidNumberOfPartitions = -2147163648,

        /// <summary>
        /// MainInteractionCentric form can have only 1 reference panel section. Found {0}.
        /// </summary>
        InvalidNumberOfReferencePanelSections = -2147085052,

        /// <summary>
        /// A dialog Form XML cannot contain more than one section.
        /// </summary>
        InvalidNumberOfSectionsInTab = -2147088270,

        /// <summary>
        /// A dialog Form XML cannot contain more than one tab.
        /// </summary>
        InvalidNumberOfTabsInDialog = -2147088271,

        /// <summary>
        /// The OAuth token is invalid
        /// </summary>
        InvalidOAuthToken = -2147214000,

        /// <summary>
        /// Invalid object type.
        /// </summary>
        InvalidObjectTypes = -2147220961,

        /// <summary>
        /// The effective end date of the series cannot be earlier than today. Select a valid occurrence number.
        /// </summary>
        InvalidOccurrenceNumber = -2147163867,

        /// <summary>
        /// Operation not valid when offline.
        /// </summary>
        InvalidOfflineOperation = -2147204850,

        /// <summary>
        /// OneToMany Entity Relationship with EntityRelationshipId '{0}' has null ReferencingEntityRole
        /// </summary>
        InvalidOneToManyRelationship = -2147015424,

        /// <summary>
        /// An invalid OneToManyRelationship has been specified for RelatedEntitiesQuery. Referenced Entity {0} should be the same as primary entity {1}
        /// </summary>
        InvalidOneToManyRelationshipForRelatedEntitiesQuery = -2147204337,

        /// <summary>
        /// The left side of the '{0}' operator must be a property of the entity.
        /// </summary>
        InvalidOperandOnLeftHandSide = -2147015423,

        /// <summary>
        /// Invalid Operation performed.
        /// </summary>
        InvalidOperation = -2147220933,

        /// <summary>
        /// Can not add items to closed (cancelled) campaignactivity.
        /// </summary>
        InvalidOperationForClosedOrCancelledCampaignActivity = -2147220716,

        /// <summary>
        /// This action is not available for a dynamic marketing list.
        /// </summary>
        InvalidOperationForDynamicList = -2147158271,

        /// <summary>
        /// List is not active. Cannot perform this operation.
        /// </summary>
        InvalidOperationWhenListIsNotActive = -2147220678,

        /// <summary>
        /// List is Locked. Cannot perform this action.
        /// </summary>
        InvalidOperationWhenListLocked = -2147220734,

        /// <summary>
        /// The party is not active. Cannot perform this operation.
        /// </summary>
        InvalidOperationWhenPartyIsNotActive = -2147220677,

        /// <summary>
        /// The operator is not valid or it is not supported.
        /// </summary>
        InvalidOperatorCode = -2147187691,

        /// <summary>
        /// Invalid operator code.
        /// </summary>
        InvalidOperatorCodeError = -2147204525,

        /// <summary>
        /// An invalid OptionSetId specified for control {0}.OptionSet Id is an non-empty Guid.
        /// </summary>
        InvalidOptionSetIdForControl = -2147088266,

        /// <summary>
        /// Cannot update OptionSet Name {0}, Id: {1} because OptionSet name provided value ({2}) is in use by another existing OptionSet (id: {3})
        /// </summary>
        InvalidOptionSetNameChange = -2147187703,

        /// <summary>
        /// Invalid OptionSet
        /// </summary>
        InvalidOptionSetOperation = -2147187709,

        /// <summary>
        /// An OptionSet with the specified name already exists. Please specify a unique name.
        /// </summary>
        InvalidOptionSetSchemaName = -2147204283,

        /// <summary>
        /// The RelationshipId of Mobile profile item association is invalid or empty.
        /// </summary>
        InvalidOrEmptyRelationshipId = -2147020510,

        /// <summary>
        /// Invalid organization friendly name ({0}). Reason: ({1})
        /// </summary>
        InvalidOrganizationFriendlyName = -2147167662,

        /// <summary>
        /// The Organization ID present in the translations file does not match the current Organization ID.
        /// </summary>
        InvalidOrganizationId = -2147204536,

        /// <summary>
        /// Organization Settings are not properly configured for External Party.
        /// </summary>
        InvalidOrganizationSettings = -2147086067,

        /// <summary>
        /// Invalid organization unique name ({0}). Reason: ({1})
        /// </summary>
        InvalidOrganizationUniqueName = -2147167663,

        /// <summary>
        /// Invalid Organization Setting passed in. Please check the datatype and pass in an appropriate value.
        /// </summary>
        InvalidOrgDBOrgSetting = -2147187436,

        /// <summary>
        /// Cascade User-Owned is not a valid cascade link type for org-owned entity relationships.
        /// </summary>
        InvalidOrgOwnedCascadeLinkType = -2147204778,

        /// <summary>
        /// You should select at least one option from Download My Records, My Team Records or My Business Unit's Records for Other Data Filter
        /// </summary>
        InvalidOtherDataFilterOptions = -2147087987,

        /// <summary>
        /// The outgoing delivery method is not email connector. To send mails its outgoing delivery method should be Email Connector.
        /// </summary>
        InvalidOutgoingDeliveryExpectingEmailConnector = -2147098074,

        /// <summary>
        /// The owner ID is invalid or missing.
        /// </summary>
        InvalidOwnerID = -2147220951,

        /// <summary>
        /// The specified ownership type mask is not valid for this operation
        /// </summary>
        InvalidOwnershipTypeMask = -2147192819,

        /// <summary>
        /// Invalid Page Response generated.
        /// </summary>
        InvalidPageResponse = -2147164147,

        /// <summary>
        /// The parent object is invalid or missing.
        /// </summary>
        InvalidParent = -2147220987,

        /// <summary>
        /// The parent id is invalid or missing.
        /// </summary>
        InvalidParentId = -2147220986,

        /// <summary>
        /// Invalid partner solution customization provider type
        /// </summary>
        InvalidPartnerSolutionCustomizationProvider = -2147180279,

        /// <summary>
        /// Invalid party mapping.
        /// </summary>
        InvalidPartyMapping = -2147207915,

        /// <summary>
        /// Plug-in assembly does not contain the required types or assembly content cannot be updated.
        /// </summary>
        InvalidPluginAssemblyContent = -2147204725,

        /// <summary>
        /// Plug-in assembly fullnames must be unique (ignoring the version build and revision number).
        /// </summary>
        InvalidPluginAssemblyVersion = -2147204741,

        /// <summary>
        /// The plug-in assembly registration configuration is invalid.
        /// </summary>
        InvalidPluginRegistrationConfiguration = -2147204752,

        /// <summary>
        /// Plug-in assembly is not strong name signed.
        /// </summary>
        InvalidPluginStrongNameRequired = -2146954988,

        /// <summary>
        /// Plug-in type must implement exactly one of the following classes or interfaces: Microsoft.Crm.Sdk.IPlugin, Microsoft.Xrm.Sdk.IPlugin, System.Activities.Activity and System.Workflow.ComponentModel.Activity.
        /// </summary>
        InvalidPluginTypeImplementation = -2147204724,

        /// <summary>
        /// The object is disposed.
        /// </summary>
        InvalidPointer = -2147220968,

        /// <summary>
        /// Invalid Precision Parameter specified for control {0}.Precision must be in between {1} and {2} .
        /// </summary>
        InvalidPrecisionForControl = -2147088259,

        /// <summary>
        /// The presentation description is invalid.
        /// </summary>
        InvalidPresentationDescription = -2147164158,

        /// <summary>
        /// You can’t perform this operation in preview mode.
        /// </summary>
        InvalidPreviewModeOperation = -2147093991,

        /// <summary>
        /// The currency of the price list needs to match the currency of the product for pricing method percentage.
        /// </summary>
        InvalidPriceLevelCurrencyForPricingMethod = -2147185415,

        /// <summary>
        /// The price per unit is invalid.
        /// </summary>
        InvalidPricePerUnit = -2147206384,

        /// <summary>
        /// The specified contact doesn't belong to the account selected as the customer. Specify a contact that belongs to the selected account, and then try again.
        /// </summary>
        InvalidPrimaryContactBasedOnAccount = -2147157916,

        /// <summary>
        /// The specified contact doesn't belong to the contact that was specified in the customer field. Remove the value from the contact field, or select a contact associated to the selected customer, and then try again.
        /// </summary>
        InvalidPrimaryContactBasedOnContact = -2147157915,

        /// <summary>
        /// A custom entity defined as an activity cannot have primary attribute other than subject.
        /// </summary>
        InvalidPrimaryFieldForActivity = -2147159769,

        /// <summary>
        /// Primary UI Attribute has to be of type String
        /// </summary>
        InvalidPrimaryFieldType = -2147192818,

        /// <summary>
        /// Invalid primary key.
        /// </summary>
        InvalidPrimaryKey = -2147220890,

        /// <summary>
        /// Passed Guid is empty.
        /// </summary>
        InvalidPrincipalId = -2147187896,

        /// <summary>
        /// Invalid Principal Type passed.
        /// </summary>
        InvalidPrincipalType = -2147187898,

        /// <summary>
        /// Invalid privilege type.
        /// </summary>
        InvalidPriv = -2147220917,

        /// <summary>
        /// Invalid privilege depth.
        /// </summary>
        InvalidPrivilegeDepth = -2147216373,

        /// <summary>
        /// The process control definition contains an invalid attribute.
        /// </summary>
        InvalidProcessControlAttribute = -2147098363,

        /// <summary>
        /// The process control definition contains an invalid entity or invalid entity order.
        /// </summary>
        InvalidProcessControlEntity = -2147098364,

        /// <summary>
        /// Invalid operation. Process ID cannot be modified.
        /// </summary>
        InvalidProcessIdOperation = -2146885631,

        /// <summary>
        /// ProcessState is not valid for given ProcessSession instance.
        /// </summary>
        InvalidProcessStateData = -2147200951,

        /// <summary>
        /// You can't add a product family.
        /// </summary>
        InvalidProduct = -2147088861,

        /// <summary>
        /// Publisher uniquename is required.
        /// </summary>
        InvalidPublisherUniqueName = -2147160036,

        /// <summary>
        /// You cannot publish {0} {1}. Publish cannot be performed when {0} is managed.
        /// </summary>
        InvalidPublishOnProtectedComponent = -2147160044,

        /// <summary>
        /// The quantity decimal code is invalid.
        /// </summary>
        InvalidQuantityDecimalCode = -2147206404,

        /// <summary>
        /// The query specified for this operation is invalid
        /// </summary>
        InvalidQuery = -2147204733,

        /// <summary>
        /// The query specified is not supported for virtual entity.
        /// </summary>
        InvalidQueryForVirtualEntity = -2147203038,

        /// <summary>
        /// To set recurrence, you must specify an interval that is between 1 and 365.
        /// </summary>
        InvalidRecurrenceInterval = -2147167583,

        /// <summary>
        /// To set recurrence, you must specify an interval that should be greater than 1 hour.
        /// </summary>
        InvalidRecurrenceIntervalForRollupJobs = -2147167582,

        /// <summary>
        /// Invalid recurrence pattern.
        /// </summary>
        InvalidRecurrencePattern = -2147163904,

        /// <summary>
        /// Error in RecurrencePatternFactory.
        /// </summary>
        InvalidRecurrenceRule = -2147220922,

        /// <summary>
        /// Bulk Delete and Duplicate Detection recurrence must be specified as daily.
        /// </summary>
        InvalidRecurrenceRuleForBulkDeleteAndDuplicateDetection = -2147167584,

        /// <summary>
        /// The regarding Object Type Code is not valid for the Bulk Operation.
        /// </summary>
        InvalidRegardingObjectTypeCode = -2147220711,

        /// <summary>
        /// Invalid registry key specified.
        /// </summary>
        InvalidRegistryKey = -2147220916,

        /// <summary>
        /// The specified relationship cannot be created
        /// </summary>
        InvalidRelationshipDescription = -2147192829,

        /// <summary>
        /// This relationship doesn’t exist with the entity selected in the parent profile item.
        /// </summary>
        InvalidRelationshipInMOPIAssociation = -2147087975,

        /// <summary>
        /// Relationship Name not specified for control {0}.Relationship Name is an mandatory Field.
        /// </summary>
        InvalidRelationshipNameForControl = -2147088265,

        /// <summary>
        /// Atleast one of the relationship properties must be part of the requested EntityMetadata properties when a RelationshipQuery is specified.Expect atleast one of property = {0}, {1} or {2} in requested entity properties list.
        /// </summary>
        InvalidRelationshipQuery = -2147015384,

        /// <summary>
        /// The specified relationship type is not valid for this operation
        /// </summary>
        InvalidRelationshipType = -2147192817,

        /// <summary>
        /// An accessory relationship is always unidirectional and can't be changed.
        /// </summary>
        InvalidRelationshipTypeForAccessory = -2147157623,

        /// <summary>
        /// An upsell relationship is always unidirectional and can't be changed.
        /// </summary>
        InvalidRelationshipTypeForUpSell = -2147157624,

        /// <summary>
        /// The relative url contains invalid characters. Please use a different name. Valid relative url names cannot ends with the following strings: .aspx, .ashx, .asmx, .svc , cannot begin or end with a dot, cannot contain consecutive dots and cannot contain any of the following characters: ~ " # % &amp; * : &lt; &gt; ? / \ {
        /// </summary>
        InvalidRelativeUrlFormat = -2147188652,

        /// <summary>
        /// Passed entity object cannot be null or empty.
        /// </summary>
        InvalidRequestBody = -2147015376,

        /// <summary>
        /// The updated configuration includes invalid data.
        /// </summary>
        InvalidRequestDataFormat = -2147204495,

        /// <summary>
        /// Both name and value should be specified for request parameter.
        /// </summary>
        InvalidRequestParameter = -2147203032,

        /// <summary>
        /// Request parameters are not valid to server External Party request.
        /// </summary>
        InvalidRequestParameters = -2147086066,

        /// <summary>
        /// The requested action is not valid for resource type {0}.
        /// </summary>
        InvalidResourceType = -2147176407,

        /// <summary>
        /// RestoreCaller must be called after SwitchToSystemUser.
        /// </summary>
        InvalidRestore = -2147220904,

        /// <summary>
        /// You have not assigned roles to this user
        /// </summary>
        InvalidRole = -2147176430,

        /// <summary>
        /// There can't be more than two entity relationship roles for a one-to-many relationship {0}.
        /// </summary>
        InvalidRoleOccurrencesForOneToManyRelationship = -2147088230,

        /// <summary>
        /// This relationship role type isn't valid for a one-to-many relationship {0}.
        /// </summary>
        InvalidRoleTypeForOneToManyRelationship = -2147088231,

        /// <summary>
        /// A Rollup Query cannot be set for a Rollup Field that is not defined in the Goal Metric.
        /// </summary>
        InvalidRollupQueryAttributeSet = -2147158397,

        /// <summary>
        /// The rollup type is invalid.
        /// </summary>
        InvalidRollupType = -2147220940,

        /// <summary>
        /// You can use server-to-server authentication only for email server profiles created for a Microsoft Dynamics 365 Online organization that was set up through the Microsoft online services environment (Office 365).
        /// </summary>
        InvalidS2SAuthentication = -2147098043,

        /// <summary>
        /// An entity with the specified name already exists. Please specify a unique name.
        /// </summary>
        InvalidSchemaName = -2147192821,

        /// <summary>
        /// Search - {0} did not find any valid Entities.
        /// </summary>
        InvalidSearchEntities = -2147089918,

        /// <summary>
        /// Invalid Search Entity - {0}.
        /// </summary>
        InvalidSearchEntity = -2147089919,

        /// <summary>
        /// Invalid Search Name - {0}.
        /// </summary>
        InvalidSearchName = -2147089916,

        /// <summary>
        /// SeriesId is null or invalid.
        /// </summary>
        InvalidSeriesId = -2147163899,

        /// <summary>
        /// Invalid seriesid or original start date.
        /// </summary>
        InvalidSeriesIdOriginalStart = -2147163895,

        /// <summary>
        /// Invalid series status.
        /// </summary>
        InvalidSeriesStatus = -2147163889,

        /// <summary>
        /// Invalid share id.
        /// </summary>
        InvalidSharee = -2147220980,

        /// <summary>
        /// The URL must conform to the http or https schema.
        /// </summary>
        InvalidSharePointSiteCollectionUrl = -2147188654,

        /// <summary>
        /// Invalid similarity rule state.
        /// </summary>
        InvalidSimilarityRuleStateError = -2147204524,

        /// <summary>
        /// Crm Internal Exception: Singleton Retrieve Query should not return more than 1 record.
        /// </summary>
        InvalidSingletonResults = -2147220913,

        /// <summary>
        /// The relative url contains invalid characters. Please use a different name. Valid relative url names cannot end with the following strings: .aspx, .ashx, .asmx, .svc , cannot begin or end with a dot or /, cannot contain consecutive dots or / and cannot contain any of the following characters: ~ " # % &amp; * : &lt; &gt; ? \ {
        /// </summary>
        InvalidSiteRelativeUrlFormat = -2147188653,

        /// <summary>
        /// An entity cannot be declared as solution-aware on an update operation. Entity: {0}
        /// </summary>
        InvalidSolutionAwarenessDeclaration = -2147016704,

        /// <summary>
        /// The specified configuration page for this solution is invalid.
        /// </summary>
        InvalidSolutionConfigurationPage = -2147192805,

        /// <summary>
        /// Invalid character specified for solution unique name. Only characters within the ranges [A-Z], [a-z], [0-9] or _ are allowed. The first character may only be in the ranges [A-Z], [a-z] or _.
        /// </summary>
        InvalidSolutionUniqueName = -2147160062,

        /// <summary>
        /// An invalid solution version was specified.
        /// </summary>
        InvalidSolutionVersion = -2147160034,

        /// <summary>
        /// Source Attribute Type does not match the Amount Data Type specified.
        /// </summary>
        InvalidSourceAttributeType = -2147203064,

        /// <summary>
        /// Attribute {0} is not an attribute of Entity {1}.
        /// </summary>
        InvalidSourceEntityAttribute = -2147203066,

        /// <summary>
        /// The source state specified for the entity is invalid.
        /// </summary>
        InvalidSourceStateValue = -2147203056,

        /// <summary>
        /// The source status specified for the entity is invalid.
        /// </summary>
        InvalidSourceStatusValue = -2147203055,

        /// <summary>
        /// SourceType {0} isn't valid for the {1} data type.
        /// </summary>
        InvalidSourceType = -2147089353,

        /// <summary>
        /// Please select valid property bag for the selected source type.
        /// </summary>
        InvalidSourceTypeCode = -2147088150,

        /// <summary>
        /// Validation error: invalid stage.
        /// </summary>
        InvalidStage = -2147089326,

        /// <summary>
        /// Invalid stage transition. Transition to stage {0} is not in the process active path.
        /// </summary>
        InvalidStageTransition = -2146885629,

        /// <summary>
        /// Invalid stage transition. Stage transition is not allowed on inactive processes.
        /// </summary>
        InvalidStageTransitionOnInactiveInstance = -2147089545,

        /// <summary>
        /// The object is not in a valid state to perform this operation.
        /// </summary>
        InvalidState = -2147220941,

        /// <summary>
        /// State code is invalid or state code is valid but status code is invalid for a specified state code.
        /// </summary>
        InvalidStateCodeStatusCode = -2147187704,

        /// <summary>
        /// The specified ProductFamily, Product or Bundle can only be published from Draft state or ActiveDraft status
        /// </summary>
        InvalidStateForPublish = -2147157750,

        /// <summary>
        /// The {0} (Id={1}) entity or component has attempted to transition from an invalid state: {2}.
        /// </summary>
        InvalidStateTransition = -2147160050,

        /// <summary>
        /// You are trying to submit an article that has a status of published. You can only submit an article with the status of draft.
        /// </summary>
        InvalidSubmitFromPublishedArticle = -2147185158,

        /// <summary>
        /// You are trying to submit an article that has a status of unapproved. You can only submit an article with the status of draft.
        /// </summary>
        InvalidSubmitFromUnapprovedArticle = -2147185153,

        /// <summary>
        /// A product can't have a relationship with itself.
        /// </summary>
        InvalidSubstituteProduct = -2147206401,

        /// <summary>
        /// The sync direction is invalid as per the allowed sync direction for one or more attribute mappings.
        /// </summary>
        InvalidSyncDirectionValueForUpdate = -2147088574,

        /// <summary>
        /// The specified target record type does not exist.
        /// </summary>
        InvalidTargetEntity = -2147220631,

        /// <summary>
        /// Target Entity Type not specified for control {0}.Target Entity is an mandatory Field.
        /// </summary>
        InvalidTargetEntityTypeForControl = -2147088264,

        /// <summary>
        /// Plug-in assembly targets a version of .NET Framework that is not supported.
        /// </summary>
        InvalidTargetFrameworkVersion = -2147204597,

        /// <summary>
        /// Task Flow is invalid.
        /// </summary>
        InvalidTaskFlow = -2147089518,

        /// <summary>
        /// The Invitation Email template is not valid
        /// </summary>
        InvalidTemplate = -2147176432,

        /// <summary>
        /// The template content is invalid.
        /// </summary>
        InvalidTemplateContent = -2147087950,

        /// <summary>
        /// That’s not a valid template.
        /// </summary>
        InvalidTemplateId = -2147155943,

        /// <summary>
        /// Exchange Online Tenant ID value is not valid.The Field value should be a string in the structure of GUID.
        /// </summary>
        InvalidTenantIDValue = -2147098021,

        /// <summary>
        /// You can’t delete system or default themes.
        /// </summary>
        InvalidThemeDeleteOperation = -2147088169,

        /// <summary>
        /// Invalid theme id.
        /// </summary>
        InvalidThemeId = -2147088172,

        /// <summary>
        /// Time Zone Code {0} specified is not recognized. Please specify a valid Time Zone Code value.
        /// </summary>
        InvalidTimeZoneCode = -2147088137,

        /// <summary>
        /// The token is invalid.
        /// </summary>
        InvalidToken = -2147176351,

        /// <summary>
        /// The total discount is invalid
        /// </summary>
        InvalidTotalDiscount = -2147220236,

        /// <summary>
        /// The total price is invalid
        /// </summary>
        InvalidTotalPrice = -2147220235,

        /// <summary>
        /// A parameter for the transformation is either missing or invalid.
        /// </summary>
        InvalidTransformationParameter = -2147220599,

        /// <summary>
        /// The data type of one or more of the transformation parameters is unsupported.
        /// </summary>
        InvalidTransformationParameterDataType = -2147220608,

        /// <summary>
        /// The transformation parameter: {0} has an invalid input value length: {1}. The parameter length cannot be an empty collection.
        /// </summary>
        InvalidTransformationParameterEmptyCollection = -2147187439,

        /// <summary>
        /// The transformation parameter mapping defined is invalid. Check that the target attribute name exists.
        /// </summary>
        InvalidTransformationParameterMapping = -2147220606,

        /// <summary>
        /// One or more transformation parameter mappings are invalid or do not match the transformation parameter description.
        /// </summary>
        InvalidTransformationParameterMappings = -2147220612,

        /// <summary>
        /// The transformation parameter: {0} has an invalid input value: {1}. The parameter is out of the permissible range: {2}.
        /// </summary>
        InvalidTransformationParameterOutsideRange = -2147187440,

        /// <summary>
        /// One or more input transformation parameter values are outside the permissible range: {0}.
        /// </summary>
        InvalidTransformationParameterOutsideRangeGeneric = -2147187438,

        /// <summary>
        /// The transformation parameter: {0} has an invalid input value: {1}. The parameter must be of type: {2}.
        /// </summary>
        InvalidTransformationParametersGeneric = -2147187449,

        /// <summary>
        /// The transformation parameter: {0} has an invalid input value: {1}. The parameter must be a string that is not empty.
        /// </summary>
        InvalidTransformationParameterString = -2147187448,

        /// <summary>
        /// The transformation parameter: {0} has an invalid input value: {1}. The parameter value must be greater than 0 and less than the length of the parameter 1.
        /// </summary>
        InvalidTransformationParameterZeroToRange = -2147187447,

        /// <summary>
        /// The specified transformation type is not supported.
        /// </summary>
        InvalidTransformationType = -2147220614,

        /// <summary>
        /// The translations file is invalid or does not confirm to the required schema.
        /// </summary>
        InvalidTranslationsFile = -2147204535,

        /// <summary>
        /// Invalid traversed path.
        /// </summary>
        InvalidTraversedPath = -2146885625,

        /// <summary>
        /// Invalid unique name for action.
        /// </summary>
        InvalidUniqueName = -2147089530,

        /// <summary>
        /// You are trying to unpublish an article that has a status of draft. You can only unpublish an article with the status of published.
        /// </summary>
        InvalidUnpublishFromDraftArticle = -2147185156,

        /// <summary>
        /// You are trying to unpublish an article that has a status of unapproved. You can only unpublish an article with the status of publish.
        /// </summary>
        InvalidUnpublishFromUnapprovedArticle = -2147185154,

        /// <summary>
        /// You cannot update {0} {1}. Updates cannot be performed when {0} is managed.
        /// </summary>
        InvalidUpdateOnProtectedComponent = -2147160046,

        /// <summary>
        /// The Url contains consecutive slashes which is not allowed.
        /// </summary>
        InvalidUrlConsecutiveSlashes = -2147188650,

        /// <summary>
        /// The specified URL is invalid.
        /// </summary>
        InvalidUrlProtocol = -2147163377,

        /// <summary>
        /// User does not have the privilege to perform this action.
        /// </summary>
        InvalidUserAuth = -2147220988,

        /// <summary>
        /// Cannot purchase {0} user licenses for the Offering {1}.
        /// </summary>
        InvalidUserLicenseCount = -2147176409,

        /// <summary>
        /// You must enter the user name in the format <name>@<domain>. Correct the format and try again.</domain></name>
        /// </summary>
        InvalidUserName = -2147188587,

        /// <summary>
        /// You have reached the maximum number of user quota
        /// </summary>
        InvalidUserQuota = -2147176431,

        /// <summary>
        /// You don't have permission to import this file. Only the user who exported this data can import this file.
        /// </summary>
        InvalidUserToImportExcelOnlineFile = -2147088377,

        /// <summary>
        /// You don't have permission to view this file. Only the user who exported this data can view this file.
        /// </summary>
        InvalidUserToViewExcelOnlineFile = -2147088378,

        /// <summary>
        /// Account Country/Region code must not be {0}
        /// </summary>
        InvalidValueForCountryCode = -2147176414,

        /// <summary>
        /// Account currency code must not be {0}
        /// </summary>
        InvalidValueForCurrency = -2147176413,

        /// <summary>
        /// The data delimiter is invalid.
        /// </summary>
        InvalidValueForDataDelimiter = -2147220671,

        /// <summary>
        /// The field delimiter is invalid.
        /// </summary>
        InvalidValueForFieldDelimiter = -2147220672,

        /// <summary>
        /// The file type is invalid.
        /// </summary>
        InvalidValueForFileType = -2147220664,

        /// <summary>
        /// Account locale code must not be {0}
        /// </summary>
        InvalidValueForLocale = -2147176412,

        /// <summary>
        /// The date in the Process Email From field is earlier than what is allowed for your organization. Enter a date that is later than the one specified, and try again.
        /// </summary>
        InvalidValueProcessEmailAfter = -2147098044,

        /// <summary>
        /// Unhandled Version mismatch found.
        /// </summary>
        InvalidVersion = -2147220932,

        /// <summary>
        /// The view is not specified or is invalid.
        /// </summary>
        InvalidViewReference = -2147087949,

        /// <summary>
        /// The web resource type {0} is not supported for visualizations.
        /// </summary>
        InvalidWebResourceForVisualization = -2147164137,

        /// <summary>
        /// The webresource ID is invalid.
        /// </summary>
        InvalidWebresourceId = -2147155669,

        /// <summary>
        /// The web resource provided for the app icon is invalid.
        /// </summary>
        InvalidWebresourceType = -2147155667,

        /// <summary>
        /// The redirectto is invalid for web2lead redirect.
        /// </summary>
        InvalidWebToLeadRedirect = -2147187594,

        /// <summary>
        /// The welcome page ID is invalid.
        /// </summary>
        InvalidWelcomePageId = -2147155668,

        /// <summary>
        /// The web resource provided for the app Welcome page is invalid.
        /// </summary>
        InvalidWelcomePageType = -2147155666,

        /// <summary>
        /// The document template is not valid.
        /// </summary>
        InvalidWordDocumentTemplate = -2147088145,

        /// <summary>
        /// The file type isn't supported.
        /// </summary>
        InvalidWordFileType = -2147088146,

        /// <summary>
        /// The template content is not valid.
        /// </summary>
        InvalidWordTemplateContent = -2147088133,

        /// <summary>
        /// Only Microsoft Word xml format files can be uploaded.
        /// </summary>
        InvalidWordXmlFile = -2147187647,

        /// <summary>
        /// Invalid workflow or workflow does not exist.
        /// </summary>
        InvalidWorkflowOrWorkflowDoesNotExist = -2147089398,

        /// <summary>
        /// XAML for workflow is NULL or Empty
        /// </summary>
        InvalidXaml = -2147089385,

        /// <summary>
        /// Invalid XML.
        /// </summary>
        InvalidXml = -2147220991,

        /// <summary>
        /// Invalid Xml collection name.
        /// </summary>
        InvalidXmlCollectionNameException = -2147220921,

        /// <summary>
        /// Invalid Xml entity name.
        /// </summary>
        InvalidXmlEntityNameException = -2147220920,

        /// <summary>
        /// Parameters node for ControlStep have invalid XML in it
        /// </summary>
        InvalidXmlForParameters = -2147089392,

        /// <summary>
        /// The data file can’t be imported because it contains invalid entity data or it’s in the wrong format. Make sure that the file contains correct data and that it’s in the XML Spreadsheet 2003 format, and then try uploading again.
        /// </summary>
        InvalidXmlSSContent = -2147220656,

        /// <summary>
        /// Plug-in assembly references a version of Microsoft.Xrm.Sdk that is higher than the server version
        /// </summary>
        InvalidXrmSdkReference = -2147204709,

        /// <summary>
        /// The selected compressed (.zip) file contains files that can't be imported. A .zip file can contain only .xlsx, .csv, or .xml files.
        /// </summary>
        InvalidZipFileForImport = -2147187582,

        /// <summary>
        /// The file that you're trying to upload isn't a valid file. Check the file and try again.
        /// </summary>
        InvalidZipFileFormat = -2147187576,

        /// <summary>
        /// You are not a billing administrator for this organization and therefore, you cannot send invitations.  You can either contact your billing administrator and ask him or her to send the invitation, or the billing administrator can visit https://billing.microsoft.com and make you a delegate billing administrator. You can then send invitations.
        /// </summary>
        InvitationBillingAdminUnknown = -2147167725,

        /// <summary>
        /// The invitation for the user cannot be reset.
        /// </summary>
        InvitationCannotBeReset = -2147167728,

        /// <summary>
        /// {0} -- Invitation has already been accepted -- Token={1} Puid={2} Id={3} Status={4}
        /// </summary>
        InvitationIsAccepted = -2147167736,

        /// <summary>
        /// {0} -- Invitation is expired -- Token={1} Puid={2} Id={3} Status={4}
        /// </summary>
        InvitationIsExpired = -2147167737,

        /// <summary>
        /// {0} -- Invitation has already been rejected by the new user-- Token={1} Puid={2} Id={3} Status={4}
        /// </summary>
        InvitationIsRejected = -2147167735,

        /// <summary>
        /// {0} -- Invitation has been revoked by the organization -- Token={1} Puid={2} Id={3} Status={4}
        /// </summary>
        InvitationIsRevoked = -2147167734,

        /// <summary>
        /// {0} -- Invitation not found or status is not Open -- Token={1} Puid={2} Id={3} Status={4}
        /// </summary>
        InvitationNotFound = -2147167740,

        /// <summary>
        /// The organization for the invitation is not enabled.
        /// </summary>
        InvitationOrganizationNotEnabled = -2147167721,

        /// <summary>
        /// The invitation cannot be sent to yourself.
        /// </summary>
        InvitationSendToSelf = -2147167729,

        /// <summary>
        /// "The invitation has status {0}."
        /// </summary>
        InvitationStatusError = -2147167732,

        /// <summary>
        /// {0} -- The pre-created userorg relation {1} is wrong.  Authentication {2} is already used by another user
        /// </summary>
        InvitationWrongUserOrgRelation = -2147167738,

        /// <summary>
        /// {0} -- The crm user {1} is already added, but to organization {2} instead of the inviting organization {3}
        /// </summary>
        InvitedUserAlreadyAdded = -2147167739,

        /// <summary>
        /// {0} -- Invited user is already in an organization -- {1}
        /// </summary>
        InvitedUserAlreadyExists = -2147167742,

        /// <summary>
        /// {0} -- The user {1} has authentication {2} and is already related to organization {3} with relation id {4}
        /// </summary>
        InvitedUserIsOrganization = -2147167741,

        /// <summary>
        /// The Dynamics 365 user {0} has been invited multiple times.
        /// </summary>
        InvitedUserMultipleTimes = -2147167733,

        /// <summary>
        /// {0} -- Inviting organization not found -- {1}
        /// </summary>
        InvitingOrganizationNotFound = -2147167744,

        /// <summary>
        /// {0} -- Inviting user is not in the inviting organization -- {1}
        /// </summary>
        InvitingUserNotInOrganization = -2147167743,

        /// <summary>
        /// Attribute iskit cannot be null
        /// </summary>
        IsKitCannotBeNull = -2147204776,

        /// <summary>
        /// This functionality is not supported, its only available for Online solution.
        /// </summary>
        IsNotLiveToSendInvitation = -2147176439,

        /// <summary>
        /// ISV code aborted the operation.
        /// </summary>
        IsvAborted = -2147220891,

        /// <summary>
        /// To import ISV.Config, your user account must be associated with a security role that includes the ISV Extensions privilege.
        /// </summary>
        IsvExtensionsPrivilegeNotPresent = -2147188695,

        /// <summary>
        /// An unexpected error occurred from ISV code.
        /// </summary>
        IsvUnExpected = -2147220956,

        /// <summary>
        /// Job Name can not be null or empty.
        /// </summary>
        JobNameIsEmptyOrNull = -2147187625,

        /// <summary>
        /// This KB article is already linked to the {0}.
        /// </summary>
        KBInvalidCreateAssociation = -2147088287,

        /// <summary>
        /// An active configuration already exists for source entity {0}. Only one active configuration is allowed per source entity.
        /// </summary>
        KnowledgeSearchActiveModelsAlreadyExist = -2147084672,

        /// <summary>
        /// The label ID {0} doesn’t match the step ID {1}.
        /// </summary>
        LabelIdDoesNotMatchStepId = -2147089383,

        /// <summary>
        /// The Microsoft Dynamics 365 Reporting Extensions must be installed before the language can be provisioned for this organization.
        /// </summary>
        LanguageProvisioningSrsDataConnectorNotInstalled = -2147158256,

        /// <summary>
        /// The lead is already closed.
        /// </summary>
        LeadAlreadyInClosedState = -2147220199,

        /// <summary>
        /// The lead is already in the open state.
        /// </summary>
        LeadAlreadyInOpenState = -2147220200,

        /// <summary>
        /// Legacy .xlsx files cannot be used for Excel Templates.
        /// </summary>
        LegacyXlsxFileNotSupported = -2147088177,

        /// <summary>
        /// The provided configuration file {0} has invalid formatting.
        /// </summary>
        LicenseConfigFileInvalid = -2147167664,

        /// <summary>
        /// There are not enough licenses available for the number of users being activated.
        /// </summary>
        LicenseNotEnoughToActivate = -2147209452,

        /// <summary>
        /// The registration period for Microsoft Dynamics 365 has expired.
        /// </summary>
        LicenseRegistrationExpired = -2147204771,

        /// <summary>
        /// The licensing for this installation of Microsoft Dynamics 365 has been tampered with. The system is unusable. Please contact Microsoft Product Support Services.
        /// </summary>
        LicenseTampered = -2147204769,

        /// <summary>
        /// The trial installation of Microsoft Dynamics 365 has expired.
        /// </summary>
        LicenseTrialExpired = -2147204772,

        /// <summary>
        /// Cannot upgrade to specified license type.
        /// </summary>
        LicenseUpgradePathNotAllowed = -2147167673,

        /// <summary>
        /// Linked Entities Are Not Allowed in the filter
        /// </summary>
        LinkedEntitiesAreNotAllowed = -2147020512,

        /// <summary>
        /// Unknown administration command {0}
        /// </summary>
        LiveAdminUnknownCommand = -2147167687,

        /// <summary>
        /// Unknown administration target {0}
        /// </summary>
        LiveAdminUnknownObject = -2147167688,

        /// <summary>
        /// The "Body" parameter is blank or null
        /// </summary>
        LivePlatformEmailInvalidBody = -2147175132,

        /// <summary>
        /// The "From" parameter is blank or null
        /// </summary>
        LivePlatformEmailInvalidFrom = -2147175134,

        /// <summary>
        /// The "Subject" parameter is blank or null
        /// </summary>
        LivePlatformEmailInvalidSubject = -2147175133,

        /// <summary>
        /// The "To" parameter is blank or null
        /// </summary>
        LivePlatformEmailInvalidTo = -2147175135,

        /// <summary>
        /// An Email Error Occurred
        /// </summary>
        LivePlatformGeneralEmailError = -2147109600,

        /// <summary>
        /// The browser operation stopped. Please try again.
        /// </summary>
        LocalDataSourceAbortError = -2147015597,

        /// <summary>
        /// The browser operation failed due to browser database errors. Please try again. If it doesn’t work, try the same operation again by ensuring that your device remains unlocked until the operation completes.
        /// </summary>
        LocalDataSourceDatabaseError = -2147015595,

        /// <summary>
        /// The operation failed. Please try again.
        /// </summary>
        LocalDataSourceError = -2147015599,

        /// <summary>
        /// The operation failed because there was not enough space in the browser storage quota or the browser storage quota was reached, and the user declined to provide more space to the browser database.
        /// </summary>
        LocalDataSourceQuotaExceededError = -2147015598,

        /// <summary>
        /// The operation timed out. Please try again.
        /// </summary>
        LocalDataSourceTimeOutError = -2147015596,

        /// <summary>
        /// Lock Status cannot be specified for a dynamic list.
        /// </summary>
        LockStatusNotValidForDynamicList = -2147158269,

        /// <summary>
        /// Logo Image node in organization cache theme data doesnot exist.
        /// </summary>
        LogoImageNodeDoesNotExist = -2147088174,

        /// <summary>
        /// The row is too long to import
        /// </summary>
        LongParseRow = -2147220622,

        /// <summary>
        /// The lookup reference could not be resolved
        /// </summary>
        LookupNotFound = -2147220653,

        /// <summary>
        /// The import solution must have a higher version than the existing solution it is upgrading.
        /// </summary>
        LowerVersionUpgrade = -2147187391,

        /// <summary>
        /// Low quantity should be less than high quantity.
        /// </summary>
        LowQuantityGreaterThanHighQuantity = -2147206399,

        /// <summary>
        /// Low quantity should be greater than zero.
        /// </summary>
        LowQuantityLessThanZero = -2147206400,

        /// <summary>
        /// Access to the app hasn’t been enabled for Appointments for this Microsoft Dynamics 365 organization. Contact your system administrator to enable access for appointments.
        /// </summary>
        MailApp_AppointmentFeatureNotEnabled = -2147085800,

        /// <summary>
        /// Try adding the following URLs to your Trusted Sites:{0} {1} {2}
        /// </summary>
        MailApp_DifferentSecurityZoneError = -2147085808,

        /// <summary>
        /// It looks like you're trying to access the CRM App for Outlook from an email address that we don't recognize. Either sign out and sign in with the email address you use for Dynamics CRM or have your system administrator update your email Mailbox settings to reflect this email address.
        /// </summary>
        MailApp_EmailAddressMismatch = -2147085807,

        /// <summary>
        /// Access to the app hasn’t been enabled for this Dynamics 365 organization. Contact your system administrator to enable access to this app.
        /// </summary>
        MailApp_FeatureControlBitDisabled = -2147085820,

        /// <summary>
        /// Email account isn't configured with server-side synchronization for incoming email
        /// </summary>
        MailApp_MailboxNotConfiguredWithServerSideSync = -2147085822,

        /// <summary>
        /// Email account isn’t configured with server-side sync for appointments, contacts, and tasks
        /// </summary>
        MailApp_MailboxNotConfiguredWithServerSideSyncForACT = -2147085801,

        /// <summary>
        /// Microsoft Dynamics 365 server-side synchronization failed for incoming emails.
        /// </summary>
        MailApp_MailboxServerSideSyncConfigurationFailure = -2147085792,

        /// <summary>
        /// Microsoft Dynamics 365 server-side synchronization failed for appointments.
        /// </summary>
        MailApp_MailboxServerSideSyncConfigurationFailureForACT = -2147085791,

        /// <summary>
        /// The mobile browser version of Outlook is currently unsupported. Please try again from the Outlook desktop application.
        /// </summary>
        MailApp_MobileBrowserIsNotSupported = -2147085816,

        /// <summary>
        /// Can't check to see if the recipients are in Dynamics 365 because user doesn't have sufficient privileges
        /// </summary>
        MailApp_PermissionsToReadContactRequired = -2147085799,

        /// <summary>
        /// User doesn't have permission to access app
        /// </summary>
        MailApp_PermissionToUseCrmForOfficeAppsRequired = -2147085819,

        /// <summary>
        /// User only has administrative access to Dynamics 365
        /// </summary>
        MailApp_ReadWriteAccessRequired = -2147085821,

        /// <summary>
        /// This version of Outlook doesn't support tracking new emails.
        /// </summary>
        MailApp_TrackingIsNotSupported = -2147085817,

        /// <summary>
        /// Your browser is currently unsupported.
        /// </summary>
        MailApp_UnsupportedBrowser = -2147085823,

        /// <summary>
        /// Your device is currently unsupported.
        /// </summary>
        MailApp_UnsupportedDevice = -2147085824,

        /// <summary>
        /// User's mailbox is inactive
        /// </summary>
        MailApp_UserMailboxInactive = -2147085802,

        /// <summary>
        /// Generic Error when Server-side sync isn't configured properly and MailApp is only allowed to be loaded in LimitedMode
        /// </summary>
        MailAppLimitedMode = -2147085790,

        /// <summary>
        /// The Delete Emails after Processing option cannot be set to Yes for user mailboxes.
        /// </summary>
        MailboxCannotDeleteEmails = -2147098061,

        /// <summary>
        /// E-mail Address of a mailbox cannot be updated when associated with an user/queue.
        /// </summary>
        MailboxCannotModifyEmailAddress = -2147098104,

        /// <summary>
        /// Username is not specified
        /// </summary>
        MailboxCredentialNotSpecified = -2147098103,

        /// <summary>
        /// The mailbox tracking folder mapping cannot be updated.
        /// </summary>
        MailboxTrackingFolderMappingCannotBeUpdated = -2147088244,

        /// <summary>
        /// Server-side synchronization for appointments, contacts, and tasks isn't supported for POP3 or SMTP server types. Select a supported email type or change the synchronization method for appointments, contacts, and tasks to None.
        /// </summary>
        MailboxUnsupportedEmailServerType = -2147098041,

        /// <summary>
        /// The business process flow is part of a managed solution and cannot be individually deleted. Uninstall the parent solution to remove the business process flow.
        /// </summary>
        ManagedBpfDeletionInvalid = -2147089533,

        /// <summary>
        /// The process is part of a managed solution and cannot be individually deleted. Uninstall the parent solution to remove the process.
        /// </summary>
        ManagedProcessDeletionError = -2147015593,

        /// <summary>
        /// Failed to parse the specified manifest file to retrieve OrganizationId
        /// </summary>
        ManifestParsingFailure = -2147187404,

        /// <summary>
        /// The import manifest file is invalid. XSD validation failed with the following error: '{0}'."
        /// </summary>
        ManifestXsdValidationError = -2146041855,

        /// <summary>
        /// An N:N relationship between virtual entities is not supported.
        /// </summary>
        ManyToManyVirtualEntityNotSupported = -2147203040,

        /// <summary>
        /// This attribute is mapped more than once. Remove any duplicate mappings, and then import this data map again.
        /// </summary>
        MappingExistsForTargetAttribute = -2147220674,

        /// <summary>
        /// Error occurred while disabling Mars connector.
        /// </summary>
        MarsConnectorDisableFailure = -2147020536,

        /// <summary>
        /// Error occurred while enabling Mars connector.
        /// </summary>
        MarsConnectorEnableFailure = -2147020537,

        /// <summary>
        /// Matching attribute name should be null single entity rule.
        /// </summary>
        MatchingAttributeNameNotNullError = -2147204541,

        /// <summary>
        /// You can’t activate this SLA because you’ve exceeded the maxiumum number of entities that can have active SLAs for your organization.
        /// </summary>
        MaxActiveSLAError = -2147157865,

        /// <summary>
        /// You can’t activate this SLA because you’ve exceeded the maxiumum number of SLA KPIs that are allowed per entity for your organization.
        /// </summary>
        MaxActiveSLAKPIError = -2147157864,

        /// <summary>
        /// A Parent Case cannot have more than maximum child cases allowed. Contact your administrator for more details
        /// </summary>
        MaxChildCasesLimitExceeded = -2147224492,

        /// <summary>
        /// You can only define 3 Mobile offline Org  filter for each entity.
        /// </summary>
        MaxConditionsMobileOfflineFilters = -2147020524,

        /// <summary>
        /// The dashboard Form XML contains more than the maximum allowed number of control elements: {0}.
        /// </summary>
        MaximumControlsLimitExceeded = -2147163391,

        /// <summary>
        /// In an update operation, you can import only one file at a time.
        /// </summary>
        MaximumCountForUpdateModeExceeded = -2147158526,

        /// <summary>
        /// This solution adds form event handlers so the number of event handlers for a form event exceeds the maximum number.
        /// </summary>
        MaximumNumberHandlersExceeded = -2147187451,

        /// <summary>
        /// The maximum number of attributes allowed for an entity has already been reached. The attribute cannot be created.
        /// </summary>
        MaximumNumberOfAttributesForEntityReached = -2147187686,

        /// <summary>
        /// Only three metric details per metric can be created.
        /// </summary>
        MaxLimitForRollupAttribute = -2147203062,

        /// <summary>
        /// The rule condition cannot be created or updated because it would cause the matchcode length to exceed the maximum limit.
        /// </summary>
        MaxMatchCodeLengthExceeded = -2147187671,

        /// <summary>
        /// You cannot create more than {0} products.
        /// </summary>
        MaxProductsAllowed = -2147020768,

        /// <summary>
        /// You can only define 6 Mobile offline entity filter conditions for each entity.
        /// </summary>
        MaxprofileItemFilterConditionsAllowed = -2147020522,

        /// <summary>
        /// The selected compressed (.zip) file can't be unzipped because it's too large.
        /// </summary>
        MaxUnzipFolderSizeExceeded = -2147187559,

        /// <summary>
        /// The Data Description for the visualization is invalid. The attribute type for one of the non aggregate measures is invalid. Correct the Data Description.
        /// </summary>
        MeasureDataTypeInvalid = -2147164144,

        /// <summary>
        /// This marketing list member was not contacted, because the member has previously received this communication.
        /// </summary>
        MemberHasAlreadyBeenContacted = -2147216370,

        /// <summary>
        /// Merge cannot be performed on sub-entity that has active quote.
        /// </summary>
        MergeActiveQuoteError = -2147200254,

        /// <summary>
        /// Merge could create cyclical parenting.
        /// </summary>
        MergeCyclicalParentingError = -2147200256,

        /// <summary>
        /// Merge warning: sub-entity will be differently parented.
        /// </summary>
        MergeDifferentlyParentedWarning = -2147200234,

        /// <summary>
        /// Merge cannot be performed on master and sub-entities that are identical.
        /// </summary>
        MergeEntitiesIdenticalError = -2147200251,

        /// <summary>
        /// Merge cannot be performed on entity that is inactive.
        /// </summary>
        MergeEntityNotActiveError = -2147200252,

        /// <summary>
        /// Merge warning: sub-entity might lose parenting
        /// </summary>
        MergeLossOfParentingWarning = -2147200233,

        /// <summary>
        /// Merge is not allowed: caller does not have the privilege or access.
        /// </summary>
        MergeSecurityError = -2147200255,

        /// <summary>
        /// The mapping between specified entities does not exist
        /// </summary>
        MetadataNoMapping = -2147217919,

        /// <summary>
        /// Metadata not found.
        /// </summary>
        MetadataNotFound = -2147220918,

        /// <summary>
        /// The given metadata entity is not serializable
        /// </summary>
        MetadataNotSerializable = -2147217917,

        /// <summary>
        /// The metadata record being deleted cannot be deleted by the end user
        /// </summary>
        MetadataRecordNotDeletable = -2147204528,

        /// <summary>
        /// Metadata sync required
        /// </summary>
        MetadataSyncRequired = -2147015408,

        /// <summary>
        /// The entity or field that is referenced in the goal metric is not valid
        /// </summary>
        MetricEntityOrFieldDeleted = -2147158393,

        /// <summary>
        /// A goal metric with the same name already exists. Specify a different name, and try again.
        /// </summary>
        MetricNameAlreadyExists = -2147203070,

        /// <summary>
        /// The value is out of range.
        /// </summary>
        MinMaxValidationFailed = -2147086332,

        /// <summary>
        /// Bulk Operation related workflow rules are missing.
        /// </summary>
        MissingBOWFRules = -2147220695,

        /// <summary>
        /// The business id is missing or invalid.
        /// </summary>
        MissingBusinessId = -2147220966,

        /// <summary>
        /// The property bag is missing an entry for {0}.
        /// </summary>
        MissingColumn = -2147176408,

        /// <summary>
        /// Required control step is missing.
        /// </summary>
        MissingControlStep = -2147089531,

        /// <summary>
        /// CrmAuthenticationToken is missing.
        /// </summary>
        MissingCrmAuthenticationToken = -2147204352,

        /// <summary>
        /// Organization Name must be specified in CrmAuthenticationToken.
        /// </summary>
        MissingCrmAuthenticationTokenOrganizationName = -2147204344,

        /// <summary>
        /// This query uses a hierarchical operator, but the {0} entity doesn't have a hierarchical relationship.
        /// </summary>
        MissingHierarchicalRelationshipForOperator = -2147192800,

        /// <summary>
        /// The opportunity id is missing or invalid.
        /// </summary>
        MissingOpportunityId = -2147206379,

        /// <summary>
        /// Cannot install Dynamics 365 without an organization friendly name.
        /// </summary>
        MissingOrganizationFriendlyName = -2147176438,

        /// <summary>
        /// Cannot install Dynamics 365 without an organization unique name.
        /// </summary>
        MissingOrganizationUniqueName = -2147176437,

        /// <summary>
        /// The RedirId parameter is missing for the partner redirect.
        /// </summary>
        MissingOrInvalidRedirectId = -2147187597,

        /// <summary>
        /// Item does not have an owner.
        /// </summary>
        MissingOwner = -2147220971,

        /// <summary>
        /// A required parameter is missing for the Bulk Operation
        /// </summary>
        MissingParameter = -2147220705,

        /// <summary>
        /// Missing parameter {0} to method {1}
        /// </summary>
        MissingParameterToMethod = -2147176415,

        /// <summary>
        /// Missing parameter to stored procedure:  {0}
        /// </summary>
        MissingParameterToStoredProcedure = -2147172352,

        /// <summary>
        /// The price level id is missing.
        /// </summary>
        MissingPriceLevelId = -2147206382,

        /// <summary>
        /// The product id is missing.
        /// </summary>
        MissingProductId = -2147206383,

        /// <summary>
        /// The Quantity is missing.
        /// </summary>
        MissingQuantity = -2146955246,

        /// <summary>
        /// The query type is missing.
        /// </summary>
        MissingQueryType = -2147220939,

        /// <summary>
        /// The fax must have a recipient before it can be sent.
        /// </summary>
        MissingRecipient = -2147207923,

        /// <summary>
        /// The property couldn’t be created or updated because the regardingobjectid, datatype, or name attribute is missing.
        /// </summary>
        MissingRequiredAttributes = -2147086281,

        /// <summary>
        /// Required attribute should not be null. Attribute: {0}
        /// </summary>
        MissingRequiredComponentAttributes = -2147016702,

        /// <summary>
        /// The team name was unexpectedly missing.
        /// </summary>
        MissingTeamName = -2147214069,

        /// <summary>
        /// The unit id is missing.
        /// </summary>
        MissingUomId = -2147206387,

        /// <summary>
        /// The unit schedule id is missing.
        /// </summary>
        MissingUomScheduleId = -2147206390,

        /// <summary>
        /// The user id or the team id is missing.
        /// </summary>
        MissingUserId = -2147220965,

        /// <summary>
        /// The redirectto is missing for web2lead redirect.
        /// </summary>
        MissingWebToLeadRedirect = -2147187593,

        /// <summary>
        /// The application could not find a supported language on the server. Contact an administrator to enable a supported language
        /// </summary>
        MobileClientLanguageNotSupported = -2147094015,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        MobileClientNotConfiguredForCurrentUser = -2147094002,

        /// <summary>
        /// Mobile Client version is not supported
        /// </summary>
        MobileClientVersionNotSupported = -2147094014,

        /// <summary>
        /// Mobile export to excel feature is not enabled.
        /// </summary>
        MobileExcelFeatureNotEnabled = -2147088182,

        /// <summary>
        /// No records will be available in the mobile offline mode if the value for number of days is 0.
        /// </summary>
        MobileOfflineDaysSinceRecordLastModifiedZero = -2147087984,

        /// <summary>
        /// A mobile offline profile item with this name already exists for this mobile offline profile. Enter a unique name.
        /// </summary>
        MobileOfflineProfileItemNameAlreadyExists = -2147087960,

        /// <summary>
        /// The mobile offline profile item name can’t be null or empty. Enter a name for this profile item.
        /// </summary>
        MobileOfflineProfileItemNameCanNotBeNullOrEmpty = -2147087958,

        /// <summary>
        /// A mobile offline profile with this name already exists. Enter a unique name.
        /// </summary>
        MobileOfflineProfileNameAlreadyExists = -2147087961,

        /// <summary>
        /// The mobile offline profile name can’t be null or empty. Enter a name for this profile.
        /// </summary>
        MobileOfflineProfileNameCanNotBeNullOrEmpty = -2147087959,

        /// <summary>
        /// This feature is not enabled for your organization. Please contact your system administrator for help.
        /// </summary>
        MobileOfflineRuleEnhancementFeatureNotAvailaible = -2147020521,

        /// <summary>
        /// Error communicating with mobile service
        /// </summary>
        MobileServiceError = -2147176336,

        /// <summary>
        /// Creation of Modern Flow processes is not enabled.
        /// </summary>
        ModernFlowProcessesNotEnabled = -2147089308,

        /// <summary>
        /// Creation of Modern Flow processes is only available online.
        /// </summary>
        ModernFlowProcessesOnlyAvailableOnline = -2147089307,

        /// <summary>
        /// Supplied value exceeded the MIN/MAX value of Money Type field.
        /// </summary>
        MoneySizeExceeded = -2147220713,

        /// <summary>
        /// The Mobile Offline Profile Item Association name can’t be a space or an empty string.
        /// </summary>
        MOPIAssociationNameCannotBeEmptyOrSpace = -2147087977,

        /// <summary>
        /// Move operation would put both instances on the same server:  Database = {0}  Old Primary = {1}  Old Secondary = {2}  New Secondary = {3}
        /// </summary>
        MoveBothToPrimary = -2147167692,

        /// <summary>
        /// Move operation would put both instances on the same server:  Database = {0}  Old Primary = {1}  Old Secondary = {2}  New Secondary = {3}
        /// </summary>
        MoveBothToSecondary = -2147167691,

        /// <summary>
        /// Move operation failed because organization {0} is not disabled
        /// </summary>
        MoveOrganizationFailedNotDisabled = -2147167690,

        /// <summary>
        /// Associating child cases to the existing child case is not allowed.
        /// </summary>
        MultilevelParentChildRelationshipNotAllowed = -2147224493,

        /// <summary>
        /// Multiple Chart Areas are not supported.
        /// </summary>
        MultipleChartAreasFound = -2147164152,

        /// <summary>
        /// Crm Internal Exception: Picklists with more than one childAttribute are not supported.
        /// </summary>
        MultipleChildPicklist = -2147220912,

        /// <summary>
        /// The attachment file name is not unique.
        /// </summary>
        MultipleFilesFound = -2147187655,

        /// <summary>
        /// A dashboard Form XML can contain only one form element.
        /// </summary>
        MultipleFormElementsFound = -2147163388,

        /// <summary>
        /// A user dashboard can have at most one label for a form element.
        /// </summary>
        MultipleLabelsInUserDashboard = -2147163379,

        /// <summary>
        /// More than one measure collection is not supported for charts with subcategory i.e. comparison charts
        /// </summary>
        MultipleMeasureCollectionsFound = -2147164132,

        /// <summary>
        /// More than one measure is not supported for charts with subcategory i.e. comparison charts
        /// </summary>
        MultipleMeasuresFound = -2147164153,

        /// <summary>
        /// Only one organization and one root business are allowed.
        /// </summary>
        MultipleOrganizationsNotAllowed = -2147214027,

        /// <summary>
        /// More than one report link found. Each report can have only one parent.
        /// </summary>
        MultipleParentReportsFound = -2147220347,

        /// <summary>
        /// An entity can have only one parental relationship
        /// </summary>
        MultipleParentsNotSupported = -2147192825,

        /// <summary>
        /// More than one security role found for partner user
        /// </summary>
        MultiplePartnerSecurityRoleWithSameInformation = -2147180278,

        /// <summary>
        /// More than one partner user found with same information
        /// </summary>
        MultiplePartnerUserWithSameInformation = -2147180277,

        /// <summary>
        /// This item occurs in more than one queue and cannot be routed from this list. Locate the item in a queue and try to route the item again.
        /// </summary>
        MultipleQueueItemsFound = -2147220187,

        /// <summary>
        /// More than one record exists for entity {0} with entity key involving attributes {1}
        /// </summary>
        MultipleRecordsFoundByEntityKey = -2147088227,

        /// <summary>
        /// Multiple relationships are not supported
        /// </summary>
        MultipleRelationshipsNotSupported = -2147188224,

        /// <summary>
        /// More than one root business unit found
        /// </summary>
        MultipleRootBusinessUnit = -2147180276,

        /// <summary>
        /// Found {0} unpublished site maps but expected only 1
        /// </summary>
        MultipleSitemapsFound = -2147155680,

        /// <summary>
        /// The data XML for the visualization cannot contain more than two Group By clauses.
        /// </summary>
        MultipleSubcategoriesFound = -2147164154,

        /// <summary>
        /// Fetch xml parameter {0} cannot obtain multiple values. Change report parameter {0} to single value parameter and try again.
        /// </summary>
        MultiValueParameterFound = -2147098614,

        /// <summary>
        /// The display name must contain atleast one non-whitespace character.
        /// </summary>
        MustContainAtLeastACharInMention = -2147158364,

        /// <summary>
        /// NavigationPropertyName {0} is not unique within an entity
        /// </summary>
        NavigationPropertyAlreadyExists = -2147015343,

        /// <summary>
        /// Navigation Property Name cannot be the same on both sides of a self-referential relationship. SchemaName - {0}
        /// </summary>
        NavigationPropertyNameCannotBeTheSameOnBothSidesOfRel = -2147015344,

        /// <summary>
        /// The nav pane properties for this relationship are not customizable
        /// </summary>
        NavPaneNotCustomizable = -2147204311,

        /// <summary>
        /// The provided nav pane order value is not allowed
        /// </summary>
        NavPaneOrderValueNotAllowed = -2147204313,

        /// <summary>
        /// Request failed due to unknown network issues or GateWay issues or server issues.
        /// </summary>
        NetworkIssue = -2147094268,

        /// <summary>
        /// The state value that was provided for this new status option does not exist.
        /// </summary>
        NewStatusHasInvalidState = -2147204318,

        /// <summary>
        /// The new status option must have an associated state value.
        /// </summary>
        NewStatusRequiresAssociatedState = -2147204319,

        /// <summary>
        /// No active location found.
        /// </summary>
        NoActiveLocation = -2147088128,

        /// <summary>
        /// No component is referenced
        /// </summary>
        NoAppModuleComponentReferred = -2147155685,

        /// <summary>
        /// No attributes for Create Entity action.
        /// </summary>
        NoAttributesForEntityCreate = -2147192812,

        /// <summary>
        /// The "Show error message" and "Set value" actions can't be used in a business rule that doesn't have a condition.
        /// </summary>
        NoConditionRuleCreationNotAllowedForSetValueShowError = -2147090413,

        /// <summary>
        /// There are no ConnectionRoleId and AssociatedObjectTypeCode pairs present. Entities being connected: ({0},{1}); Entity Records being connected: ({2},{3}); Record1ConnectionRoleName: {4}, Record2ConnectionRoleName: {5}. ConnectionRoleIds : {6}
        /// </summary>
        NoConnectionRoleAndObjectTypeCodePairPresent = -2147159518,

        /// <summary>
        /// A ConversionRule is required for the tool to run.
        /// </summary>
        NoConversionRule = -2147088139,

        /// <summary>
        /// The entity '{0}' in the profile '{1}' contained the Data Download Filter 'Other data filter' however no data filter option was selected. The entity must specify a data filter option.
        /// </summary>
        NoDataFilterSelectedForOtherDataFilter = -2147020488,

        /// <summary>
        /// There is no data to create this visualization.
        /// </summary>
        NoDataForVisualization = -2147164143,

        /// <summary>
        /// Arguments of type OptionSet must have a default value set.
        /// </summary>
        NoDefaultValueForOptionSetArgument = -2147089514,

        /// <summary>
        /// The Profile Item Association entity doesn’t have any defined relationships.
        /// </summary>
        NoDefinedRelationshipsForMOPIAssociation = -2147087976,

        /// <summary>
        /// There is no fax number specified on the fax or for the recipient.
        /// </summary>
        NoDialNumber = -2147207921,

        /// <summary>
        /// The Bulk Delete Wizard cannot be opened because there are no valid entities for deletion.
        /// </summary>
        NoEntitiesForBulkDelete = -2147187646,

        /// <summary>
        /// At least one Entity is expected by the tool to process.
        /// </summary>
        NoEntitySpecified = -2147088134,

        /// <summary>
        /// No documents are selected to copy. Please select a document and try again.
        /// </summary>
        NoFilesSelected = -2147020767,

        /// <summary>
        /// A column heading is missing.
        /// </summary>
        NoHeaderColumnFound = -2147220632,

        /// <summary>
        /// Child cases having different parent case associated can not be merged.
        /// </summary>
        NoIncidentMergeHavingSameParent = -2147224496,

        /// <summary>
        /// {0} does not have any labels associated with it
        /// </summary>
        NoLabelsAssociatedWithStep = -2147089400,

        /// <summary>
        /// There is no language provisioned for this organization.
        /// </summary>
        NoLanguageProvisioned = -2147204711,

        /// <summary>
        /// No license exists in MSCRM_CONFIG database.
        /// </summary>
        NoLicenseInConfigDB = -2147167679,

        /// <summary>
        /// You do not have sufficient permissions on the server to load the application.\nPlease contact your administrator to update your permissions.
        /// </summary>
        NoMinimumRequiredPrivilegesForTabletApp = -2147094001,

        /// <summary>
        /// All available custom option values have been used.
        /// </summary>
        NoMoreCustomOptionValuesExist = -2147204321,

        /// <summary>
        /// The input XML does not comply with the XML schema.
        /// </summary>
        NoncompliantXml = -2147187675,

        /// <summary>
        /// This interactive workflow cannot be created, updated or published because it was created outside the Microsoft Dynamics 365 Web application.
        /// </summary>
        NonCrmUIInteractiveWorkflowNotSupported = -2147200956,

        /// <summary>
        /// This workflow cannot be created, updated or published because it was created outside the Microsoft Dynamics 365 Web application. Your organization does not allow this type of workflow.
        /// </summary>
        NonCrmUIWorkflowsNotSupported = -2147200960,

        /// <summary>
        /// Product Association cannot be updated when bundle is in invalid state.
        /// </summary>
        NonDraftBundleUpdate = -2147086279,

        /// <summary>
        /// Non-interactive users cannot access the web user interface. Contact your organization system administrator.
        /// </summary>
        NonInteractiveUserCannotAccessUI = -2147204768,

        /// <summary>
        /// NonMappableEntity Error Occurred
        /// </summary>
        NonMappableEntity = -2147196416,

        /// <summary>
        /// The data description for the visualization is invalid .The data description for the visualization can only have attributes either from the primary entity of the view or the linked entities.
        /// </summary>
        NonPrimaryEntityDataDescriptionFound = -2147164159,

        /// <summary>
        /// There is no output transformation parameter mapping defined. A transformation mapping must have atleast one output transformation parameter mapping.
        /// </summary>
        NoOutputTransformationParameterMappingFound = -2147220604,

        /// <summary>
        /// This chart uses a custom Web resource. You cannot preview this chart.
        /// </summary>
        NoPreviewForCustomWebResource = -2147164128,

        /// <summary>
        /// You don't have appropriate permissions to apply Servie Level Agreement (SLA) to this case record.
        /// </summary>
        NoPrivilegeToApplyManualSLA = -2147135486,

        /// <summary>
        /// User does not have sufficient privileges to publish workflows.
        /// </summary>
        NoPrivilegeToPublishWorkflow = -2147201002,

        /// <summary>
        /// You cannot add items to an inactive queue. Select another queue and try again.
        /// </summary>
        NoPrivilegeToWorker = -2147220191,

        /// <summary>
        /// There are no published duplicate detection rules in the system. To run duplicate detection, you must create and publish one or more rules.
        /// </summary>
        NoPublishedDuplicateDetectionRules = -2147187658,

        /// <summary>
        /// Entity - {0} did not have a valid Quickfind query.
        /// </summary>
        NoQuickFindFound = -2147089917,

        /// <summary>
        /// For rollup to succeed atleast one rollup attribute needs to be associated with the goal metric
        /// </summary>
        NoRollupAttributesDefined = -2147158399,

        /// <summary>
        /// No configdb configuration setting [{0}] was found.
        /// </summary>
        NoSettingError = -2147160762,

        /// <summary>
        /// App Module does not contain Site Map
        /// </summary>
        NoSiteMapReferenceInAppModule = -2147155684,

        /// <summary>
        /// The input XML is not well-formed XML.
        /// </summary>
        NotAWellFormedXml = -2147187674,

        /// <summary>
        /// Not enough privileges to complete the operation. Only the deployment administrator can create or update workflows that are created outside the Microsoft Dynamics 365 Web application.
        /// </summary>
        NotEnoughPrivilegesForXamlWorkflows = -2147200959,

        /// <summary>
        /// There is not sufficient privilege to perform the test access.
        /// </summary>
        NoTestEmailAccessPrivilege = -2147098062,

        /// <summary>
        /// Argument {0} does not exist in Action.
        /// </summary>
        NotExistArgumentInAction = -2147089517,

        /// <summary>
        /// Business Process does not exist.
        /// </summary>
        NotExistBusinessProcess = -2147089519,

        /// <summary>
        /// The TimeZoneCode property is required when the value of the ConversionRule property is SpecificTimeZone.
        /// </summary>
        NoTimeZoneCodeForConversionRule = -2147088135,

        /// <summary>
        /// The requested functionality is not yet implemented.
        /// </summary>
        NotImplemented = -2147220967,

        /// <summary>
        /// You can't view this type of record on your tablet. Contact your system administrator.
        /// </summary>
        NotMobileEnabled = -2147093995,

        /// <summary>
        /// You can't create this type of record on your device. Contact your system administrator.
        /// </summary>
        NotMobileWriteEnabled = -2147093988,

        /// <summary>
        /// This action is not supported.
        /// </summary>
        NotSupported = -2147220715,

        /// <summary>
        /// You need an enterprise account with Yammer in order to complete this setup. Please sign in with a Yammer administrator account or contact a Yammer administrator for help.
        /// </summary>
        NotVerifiedAdmin = -2147094266,

        /// <summary>
        /// You do not have sufficient permissions.
        /// </summary>
        NoUserPrivilege = -2146088112,

        /// <summary>
        /// You do not have Write permissions to copy the documents.
        /// </summary>
        NoWritePermission = -2147020765,

        /// <summary>
        /// You are not authorized for any Yammer network. Please reauthorize the Yammer setup with a Yammer administrator account or contact a Yammer administrator for help.
        /// </summary>
        NoYammerNetworksFound = -2147094264,

        /// <summary>
        /// The article template formatxml cannot be NULL
        /// </summary>
        NullArticleTemplateFormatXml = -2147220232,

        /// <summary>
        /// The article template structurexml cannot be NULL
        /// </summary>
        NullArticleTemplateStructureXml = -2147220231,

        /// <summary>
        /// The article xml cannot be NULL
        /// </summary>
        NullArticleXml = -2147220233,

        /// <summary>
        /// The name of a dashboard cannot be null.
        /// </summary>
        NullDashboardName = -2147163387,

        /// <summary>
        /// The kbarticletemplateid cannot be NULL
        /// </summary>
        NullKBArticleTemplateId = -2147220230,

        /// <summary>
        /// Attribute - {0} of {1} cannot be null or empty
        /// </summary>
        NullOrEmptyAttributeInXaml = -2147089402,

        /// <summary>
        /// Failed to produce a formatted numeric value.
        /// </summary>
        NumberFormatFailed = -2147220903,

        /// <summary>
        /// Yammer OAuth token is not found. You should configure Yammer before accessing any related feature.
        /// </summary>
        OAuthTokenNotFound = -2147094263,

        /// <summary>
        /// An object with id {0} already exists. Please change the id and try again.
        /// </summary>
        ObjectAlreadyExists = -2147163382,

        /// <summary>
        /// The specified object was not found.
        /// </summary>
        ObjectDoesNotExist = -2147220969,

        /// <summary>
        /// The object does not exist in active directory.
        /// </summary>
        ObjectNotFoundInAD = -2147214038,

        /// <summary>
        /// Specified Object not related to the parent Campaign
        /// </summary>
        ObjectNotRelatedToCampaign = -2147220722,

        /// <summary>
        /// Two occurrences cannot overlap.
        /// </summary>
        OccurrenceCrossingBoundary = -2147163872,

        /// <summary>
        /// Cannot reschedule an occurrence of the recurring appointment if it skips over an earlier occurrence of the same appointment.
        /// </summary>
        OccurrenceSkipsOverBackward = -2147163869,

        /// <summary>
        /// Cannot reschedule an occurrence of the recurring appointment if it skips over a later occurrence of the same appointment.
        /// </summary>
        OccurrenceSkipsOverForward = -2147163870,

        /// <summary>
        /// Cannot perform the operation. An instance is outside of series effective expansion range.
        /// </summary>
        OccurrenceTimeSpanTooBig = -2147163871,

        /// <summary>
        /// Offer category and Billing Token are both missing, but at least one is required.
        /// </summary>
        OfferingCategoryAndTokenNull = -2147176436,

        /// <summary>
        /// This version does not support search for offering id.
        /// </summary>
        OfferingIdNotSupported = -2147176435,

        /// <summary>
        /// Document Recommendations has been disabled for this organization.
        /// </summary>
        OfficeGraphDisabledError = -2147204551,

        /// <summary>
        /// No default SharePoint site has been configured.
        /// </summary>
        OfficeGraphSiteNotConfigured = -2147204521,

        /// <summary>
        /// Office Groups Exception occured in RetrieveOfficeGroupsSetting: {0}.
        /// </summary>
        OfficeGroupsExceptionRetrieveSetting = -2147086101,

        /// <summary>
        /// Office Groups feature is not enabled.
        /// </summary>
        OfficeGroupsFeatureNotEnabled = -2147086102,

        /// <summary>
        /// Invalid setting type for Office Groups feature: {0}.
        /// </summary>
        OfficeGroupsInvalidSettingType = -2147086100,

        /// <summary>
        /// Office Groups feature could not find any authorization servers.
        /// </summary>
        OfficeGroupsNoAuthServersFound = -2147086098,

        /// <summary>
        /// Office Groups feature attempted an unsupported call.
        /// </summary>
        OfficeGroupsNotSupportedCall = -2147086099,

        /// <summary>
        /// You cannot use nested date time conditions within an OR clause in a local data group.
        /// </summary>
        OfflineFilterNestedDateTimeOR = -2147187632,

        /// <summary>
        /// You cannot use the Parent Downloaded condition in a local data group.
        /// </summary>
        OfflineFilterParentDownloaded = -2147187631,

        /// <summary>
        /// Following attachments requires OneDrive for Business. Please contact your administrator to enable OneDrive for Business in the organization.
        /// </summary>
        OneDriveForBusinessDisabled = -2147155964,

        /// <summary>
        /// No One Drive for Business active location found.
        /// </summary>
        OneDriveForBusinessLocationNotFound = -2147155959,

        /// <summary>
        /// OneNote creation failed.
        /// </summary>
        OneNoteCreationFailed = -2147088126,

        /// <summary>
        /// The location mapping for OneNote is inactive. Contact your administrator to activate the OneNote location record for this Dynamics 365 record.
        /// </summary>
        OneNoteLocationDeactivated = -2147088121,

        /// <summary>
        /// OneNote location not created.
        /// </summary>
        OneNoteLocationNotCreated = -2147088122,

        /// <summary>
        /// OneNote render failed.
        /// </summary>
        OneNoteRenderFailed = -2147088125,

        /// <summary>
        /// Can not delete enabled organization. Organization must be disabled before it can be deleted.
        /// </summary>
        OnlyDisabledOrganizationCanBeDeleted = -2147176401,

        /// <summary>
        /// Extra parameter. You only need to provide EntityGroupName or EntityNames, not both.
        /// </summary>
        OnlyOneSearchParameterMustBeProvided = -2147089914,

        /// <summary>
        /// Only the owner of an object can revoke the owner's access to that object.
        /// </summary>
        OnlyOwnerCanRevoke = -2147220957,

        /// <summary>
        /// Cannot import component {0}: {1} because managed property {2} with value {3} is different than the current value {4} and the publisher of the solution that is being imported does not match the publisher of the solution that installed this component.
        /// </summary>
        OnlyOwnerCanSetManagedProperties = -2147160015,

        /// <summary>
        /// Only products can be converted to kits.
        /// </summary>
        OnlyProductCanBeConvertedToKit = -2147086313,

        /// <summary>
        /// Invalid plug-in registration stage. Steps can only be modified in stages BeforeMainOperationOutsideTransaction, BeforeMainOperationInsideTransaction, AfterMainOperationInsideTransaction and AfterMainOperationOutsideTransaction.
        /// </summary>
        OnlyStepInPredefinedStagesCanBeModified = -2147204732,

        /// <summary>
        /// Only SdkMessageProcessingStep with ServerOnly supported deployment can have secure configuration.
        /// </summary>
        OnlyStepInServerOnlyCanHaveSecureConfiguration = -2147204731,

        /// <summary>
        /// Only SdkMessageProcessingStep in parent pipeline and in stages outside transaction can create CrmService to prevent deadlock.
        /// </summary>
        OnlyStepOutsideTransactionCanCreateCrmService = -2147204730,

        /// <summary>
        /// Only workflow definition or draft workflow template can be published.
        /// </summary>
        OnlyWorkflowDefinitionOrTemplateCanBePublished = -2147201011,

        /// <summary>
        /// Only workflow definition or workflow template can be unpublished.
        /// </summary>
        OnlyWorkflowDefinitionOrTemplateCanBeUnpublished = -2147201010,

        /// <summary>
        /// Failed to restore Organization's configdb state from manifest
        /// </summary>
        OnPremiseRestoreOrganizationManifestFailed = -2147187406,

        /// <summary>
        /// Db Connection is Open, when it should be Closed.
        /// </summary>
        OpenCrmDBConnection = -2147220930,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        OpenDocumentErrorCodeGeneric = -2147094004,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        OpenDocumentErrorCodeUnableToFindAnActivity = -2147094006,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        OpenDocumentErrorCodeUnableToFindTheDataId = -2147094005,

        /// <summary>
        /// Exception occurs while opening mailbox for Exchaange mail server.
        /// </summary>
        OpenMailboxException = -2147098090,

        /// <summary>
        /// The specified action can be done only one time.
        /// </summary>
        OperationCanBeCalledOnlyOnce = -2147220684,

        /// <summary>
        /// Refresh was canceled by user.
        /// </summary>
        OperationCanceled = -2147088110,

        /// <summary>
        /// Operation could not be performed at the moment. Please try again.
        /// </summary>
        OperationFailedTryAgain = -2146088109,

        /// <summary>
        /// The {1} operation failed because organization {0} is not fully disabled yet.  Use FORCE to override
        /// </summary>
        OperationOrganizationNotFullyDisabled = -2147167686,

        /// <summary>
        /// OperatorCode should be present in condition xml.
        /// </summary>
        OperatorCodeNotPresentError = -2147204543,

        /// <summary>
        /// One or more operators in this view are not supported offline.
        /// </summary>
        OperatorsInViewNotSupportedOffline = -2147020505,

        /// <summary>
        /// The opportunity is already in the open state.
        /// </summary>
        OpportunityAlreadyInOpenState = -2147220198,

        /// <summary>
        /// The opportunity cannot be closed.
        /// </summary>
        OpportunityCannotBeClosed = -2147220202,

        /// <summary>
        /// Unable to update the opportunity, currency comparison could not be made.
        /// </summary>
        OpportunityCurrencyComparisonNotPossible = -2146435068,

        /// <summary>
        /// The opportunity is already closed.
        /// </summary>
        OpportunityIsAlreadyClosed = -2147220203,

        /// <summary>
        /// Unable to get opportunity entity for update.
        /// </summary>
        OpportunityNotFound = -2146435066,

        /// <summary>
        /// Unable to find pre-image of the opportunity before the update.
        /// </summary>
        OpportunityPreImageNotFound = -2146435065,

        /// <summary>
        /// Optimistic concurrency isn't enabled for entity type {0}. The IfVersionMatches value of ConcurrencyBehavior can only be used if optimistic concurrency is enabled.
        /// </summary>
        OptimisticConcurrencyNotEnabled = -2147088243,

        /// <summary>
        /// The array of option values that were provided for reordering does not match the number of options for the attribute.
        /// </summary>
        OptionReorderArrayIncorrectLength = -2147204316,

        /// <summary>
        /// The value is out of range.
        /// </summary>
        OptionSetValidationFailed = -2147086331,

        /// <summary>
        /// CustomizationOptionValuePrefix must be a number between {0} and {1}
        /// </summary>
        OptionValuePrefixOutOfRange = -2147187710,

        /// <summary>
        /// The Dynamics 365 organization you are attempting to access is currently disabled.  Please contact your system administrator
        /// </summary>
        OrganizationDisabled = -2147180284,

        /// <summary>
        /// Organization migration is already underway.
        /// </summary>
        OrganizationMigrationUnderway = -2147176380,

        /// <summary>
        /// Organization is not configured yet
        /// </summary>
        OrganizationNotConfigured = -2147167661,

        /// <summary>
        /// The organization {0} is already purchased by another customer.
        /// </summary>
        OrganizationTakenBySomeoneElse = -2147176433,

        /// <summary>
        /// The organization {0} is already purchased by you.
        /// </summary>
        OrganizationTakenByYou = -2147176434,

        /// <summary>
        /// The OrganizationUI entity is deprecated. It has been replaced by the SystemForm entity.
        /// </summary>
        OrganizationUIDeprecated = -2147204775,

        /// <summary>
        /// Organization not found in customer's discovery service
        /// </summary>
        OrgDoesNotExistInDiscoveryService = -2147176345,

        /// <summary>
        /// Error. The current organization ID couldn’t be determined
        /// </summary>
        OrgIdNotDetermined = -2147204269,

        /// <summary>
        /// The client access license (CAL) results were not returned because one or more organizations in the deployment cannot be accessed.
        /// </summary>
        OrgsInaccessible = -2147167670,

        /// <summary>
        /// Mailbox is a forward mailbox. A forward mailbox cannot send the mails.
        /// </summary>
        OutgoingNotAllowedForForwardMailbox = -2147098075,

        /// <summary>
        /// The URL specified for Outgoing Server Location uses HTTPS but the Use SSL for Outgoing Connection option is set to No. Set this option to Yes, and then try again.
        /// </summary>
        OutgoingServerLocationAndSslSetToNo = -2147098049,

        /// <summary>
        /// The URL specified for Outgoing Server Location uses HTTP but the Use SSL for Outgoing Connection option is set to Yes. Specify a server location that uses HTTPS, and then try again.
        /// </summary>
        OutgoingServerLocationAndSslSetToYes = -2147098047,

        /// <summary>
        /// Different outgoing connection settings cannot be specified because the "Use Same Settings for Outgoing Connections" flag is set to True.
        /// </summary>
        OutgoingSettingsUpdateNotAllowed = -2147098056,

        /// <summary>
        /// Dynamics 365 Outlook client configuration action failed.
        /// </summary>
        OutlookClientConfigActionFailed = -2147203831,

        /// <summary>
        /// The data required to display the next dialog page cannot be found. To resolve this issue, contact the dialog owner or the system administrator.
        /// </summary>
        OutOfScopeSlug = -2147200944,

        /// <summary>
        /// Two instances of the series cannot overlap.
        /// </summary>
        OverlappingInstances = -2147163896,

        /// <summary>
        /// Over upper limit of records that can be requested without a paging cookie. A paging cookie is required when retrieving a high page number.
        /// </summary>
        OverRetrievalUpperLimitWithoutPagingCookie = -2147204342,

        /// <summary>
        /// Owner Attribute is not present in the request.
        /// </summary>
        OwnerAttributeMissing = -2147086068,

        /// <summary>
        /// The data map already contains this owner mapping.
        /// </summary>
        OwnerMappingExistsWithSourceSystemUserName = -2147220669,

        /// <summary>
        /// The owner value is not mapped
        /// </summary>
        OwnerValueNotMapped = -2147220639,

        /// <summary>
        /// Page not found. The record might not exist, or the link might be incorrect.
        /// </summary>
        PageNotFound = -2147093990,

        /// <summary>
        /// The parent business Id is invalid.
        /// </summary>
        ParentBusinessDoesNotExist = -2147214045,

        /// <summary>
        /// You can't add a parent case as a child case
        /// </summary>
        ParentCaseNotAllowedAsAChildCase = -2147224491,

        /// <summary>
        /// The metricid of child goal should be same as the parent goal.
        /// </summary>
        ParentChildMetricIdDiffers = -2147202811,

        /// <summary>
        /// The period settings of child goal should be same as the parent goal.
        /// </summary>
        ParentChildPeriodAttributesDiffer = -2147202810,

        /// <summary>
        /// A parent should exist for each node in hierarchy except the root node.
        /// </summary>
        ParentHierarchyExistProperty = -2147157880,

        /// <summary>
        /// The parent is read only and cannot be edited.
        /// </summary>
        ParentReadOnly = -2147206391,

        /// <summary>
        /// This record cannot be added because it already has a parent record.
        /// </summary>
        ParentRecordAlreadyExists = -2147187592,

        /// <summary>
        /// Specified parent report does not reference the current one. Only SQL Reporting Services reports can have parent reports.
        /// </summary>
        ParentReportDoesNotReferenceChild = -2147220346,

        /// <summary>
        /// Parent report already links to another report with the same name.
        /// </summary>
        ParentReportLinksToSameNameChild = -2147220330,

        /// <summary>
        /// Parent report is not supported for the type of report specified. Only SQL Reporting Services reports can have parent reports.
        /// </summary>
        ParentReportNotSupported = -2147220345,

        /// <summary>
        /// The parent user Id is invalid.
        /// </summary>
        ParentUserDoesNotExist = -2147214041,

        /// <summary>
        /// Cannot call transform before parse.
        /// </summary>
        ParseMustBeCalledBeforeTransform = -2147220623,

        /// <summary>
        /// Data required to parse the file, such as the data delimiter, field delimiter, or column headings, was not found.
        /// </summary>
        ParsingMetadataNotFound = -2147220633,

        /// <summary>
        /// Failed to retrieve partial expansion settings from the configuration database.
        /// </summary>
        PartialExpansionSettingLoadError = -2147163902,

        /// <summary>
        /// Partial holiday schedule can not be created.
        /// </summary>
        PartialHolidayScheduleCreation = -2147157901,

        /// <summary>
        /// Error displaying Business Process control. Participating entity must be part of traversed path, but entity '{0}' does not appear in path '{1}'. Please contact your system administrator.
        /// </summary>
        ParticipatingEntityDoesNotAppearInTraversedPath = -2147089343,

        /// <summary>
        /// The entitytype of participating query should be the same as the entity specified in fetchxml.
        /// </summary>
        ParticipatingQueryEntityMismatch = -2147202807,

        /// <summary>
        /// Type in a password and save again
        /// </summary>
        PasswordRequiredForImpersonation = -2147098034,

        /// <summary>
        /// You can't import the patch ({0}) for the solution ({1}) because the solution isn't present. The operation has been canceled.
        /// </summary>
        PatchMissingBase = -2147187392,

        /// <summary>
        /// Currency cannot be set when discount type is percentage.
        /// </summary>
        PercentageDiscountCannotHaveCurrency = -2147185423,

        /// <summary>
        /// A system dashboard cannot contain personal reports.
        /// </summary>
        PersonalReportFound = -2147163383,

        /// <summary>
        /// This list value is mapped more than once. Remove any duplicate mappings, and then import this data map again.
        /// </summary>
        PickListMappingExistsForTargetValue = -2147220673,

        /// <summary>
        /// The data map already contains this list value mapping.
        /// </summary>
        PickListMappingExistsWithSourceValue = -2147220670,

        /// <summary>
        /// The file specifies a list value that does not exist in Microsoft Dynamics 365.
        /// </summary>
        PicklistValueNotFound = -2147220589,

        /// <summary>
        /// The record could not be processed as the Option set value could not be mapped.
        /// </summary>
        PicklistValueNotMapped = -2147220640,

        /// <summary>
        /// The picklist value already exists.  Picklist values must be unique.
        /// </summary>
        PicklistValueNotUnique = -2147204336,

        /// <summary>
        /// The picklist value is out of the range.
        /// </summary>
        PicklistValueOutOfRange = -2147204326,

        /// <summary>
        /// The system couldn't reconnect with your {#Brand_CRM} server.
        /// </summary>
        PingFailureErrorCode = -2147093998,

        /// <summary>
        /// "The assembly content size '{0} bytes' has exceeded the maximum value allowed for isolated plug-ins '{1} bytes'."
        /// </summary>
        PluginAssemblyContentSizeExceeded = -2147204721,

        /// <summary>
        /// Public assembly must have public key token.
        /// </summary>
        PluginAssemblyMustHavePublicKeyToken = -2147204756,

        /// <summary>
        /// The plug-in specified does not implement the required interface Microsoft.Xrm.Sdk.IPlugin or Microsoft.Crm.Sdk.IPlugin.
        /// </summary>
        PluginDoesNotImplementCorrectInterface = -2147180032,

        /// <summary>
        /// Unable to AcquireToken for resource
        /// </summary>
        PluginSecureStoreAdalAcquireToken = -2146889723,

        /// <summary>
        /// Unable to initialize KeyVaultClientProvider under Sandbox WorkerProcess
        /// </summary>
        PluginSecureStoreKeyVaultClient = -2146889728,

        /// <summary>
        /// Unable to Decrypt using KeyVault
        /// </summary>
        PluginSecureStoreKeyVaultClientDecrypt = -2146889725,

        /// <summary>
        /// Unable to Encrypt using KeyVault
        /// </summary>
        PluginSecureStoreKeyVaultClientEncrypt = -2146889724,

        /// <summary>
        /// Unable to GetSecret from KeyVault
        /// </summary>
        PluginSecureStoreKeyVaultClientGetSecret = -2146889727,

        /// <summary>
        /// Unable to SetSecret to KeyVault
        /// </summary>
        PluginSecureStoreKeyVaultClientSetSecret = -2146889726,

        /// <summary>
        /// Certificate not stored as a Base64String in KeyVault
        /// </summary>
        PluginSecureStoreKeyVaultServiceCertFormat = -2146889715,

        /// <summary>
        /// Missing AppId / Secrets in KeyVault
        /// </summary>
        PluginSecureStoreKeyVaultServiceProviderGetData = -2146889716,

        /// <summary>
        /// Unable to get data from LocalConfigStore
        /// </summary>
        PluginSecureStoreLocalConfigStoreGetData = -2146889718,

        /// <summary>
        /// Unable to set data to LocalConfigStore
        /// </summary>
        PluginSecureStoreLocalConfigStoreSetData = -2146889717,

        /// <summary>
        /// Assembly not fully signed
        /// </summary>
        PluginSecureStoreNoFullySigned = -2146889713,

        /// <summary>
        /// S2S Credentials missing
        /// </summary>
        PluginSecureStoreS2SMissing = -2146889720,

        /// <summary>
        /// Assembly is not registered in TPS
        /// </summary>
        PluginSecureStoreTPSAssemblyNotRegistered = -2146889721,

        /// <summary>
        /// Unable to create TPS Client
        /// </summary>
        PluginSecureStoreTPSClient = -2146889719,

        /// <summary>
        /// KeyVaultURI was not configured for an Assembly in TPS
        /// </summary>
        PluginSecureStoreTPSKeyVaultUnconfigured = -2146889722,

        /// <summary>
        /// Multiple plug-in types from the same assembly and with the same typename are not allowed.
        /// </summary>
        PluginTypeMustBeUnique = -2147204740,

        /// <summary>
        /// Exception occur while polling mails using Pop3 protocol.
        /// </summary>
        Pop3UnexpectedException = -2147098091,

        /// <summary>
        /// A Power BI Dashboard cannot be a System Dashboard.
        /// </summary>
        PowerBICannotBeSystemDashboard = -2147088132,

        /// <summary>
        /// A Power BI Dashboard can only contain one control and that control must be a Power BI control.
        /// </summary>
        PowerBIDashboardControlLimitation = -2147088131,

        /// <summary>
        /// You can either specify a contacts parent contact or its account, but not both.
        /// </summary>
        PresentParentAccountAndParentContact = -2147220216,

        /// <summary>
        /// An operation on which this operation depends did not complete successfully.
        /// </summary>
        PreviousOperationNotComplete = -2147187612,

        /// <summary>
        /// The name already exists.
        /// </summary>
        PriceLevelNameExists = -2147206385,

        /// <summary>
        /// The name can not be null.
        /// </summary>
        PriceLevelNoName = -2147206386,

        /// <summary>
        /// Invalid primary entity.
        /// </summary>
        PrimaryEntityInvalid = -2147200994,

        /// <summary>
        /// Primary Entity cannot be NULL while creating business process flow category
        /// </summary>
        PrimaryEntityIsNull = -2147089407,

        /// <summary>
        /// PrimaryName attribute not found for Entity: {0}
        /// </summary>
        PrimaryNameAttributeNotFound = -2147204267,

        /// <summary>
        /// Validation error: primary participating entity is not present and is required for every Business Process entity record.
        /// </summary>
        PrimaryParticipatingEntityIsNotPresent = -2147089325,

        /// <summary>
        /// Target user or team does not hold required privileges.
        /// </summary>
        PrincipalPrivilegeDenied = -2147220943,

        /// <summary>
        /// Privilege Create is disabled for organization.
        /// </summary>
        PrivilegeCreateIsDisabledForOrganization = -2147220874,

        /// <summary>
        /// The user does not hold the necessary privileges.
        /// </summary>
        PrivilegeDenied = -2147220960,

        /// <summary>
        /// Process Action does not exist.
        /// </summary>
        ProcessActionDoesNotExist = -2147200940,

        /// <summary>
        /// Process Action should be active to be used on Action Step.
        /// </summary>
        ProcessActionIsNotActive = -2147200941,

        /// <summary>
        /// Process Action “{0}” does not match the name configured: “{1}”. Contact your system administrator to check the configuration metadata if the error persists.
        /// </summary>
        ProcessActionNameIncorrect = -2147089543,

        /// <summary>
        /// Process Action contains a parameter that is not supported. Name: {0}, type: {1}, direction: {2}.
        /// </summary>
        ProcessActionWithInvalidInputOutputParam = -2147200936,

        /// <summary>
        /// Process Action contains a field in input parameter that is unsupported on Action Steps. Refer to {0}
        /// </summary>
        ProcessActionWithInvalidInputParam = -2147200937,

        /// <summary>
        /// Process Action contains a field in output parameter that is unsupported on Action Steps. Refer to {0}.
        /// </summary>
        ProcessActionWithInvalidOutputParam = -2147200938,

        /// <summary>
        /// Process Action or Workflow must be enabled for on-demand execution to be available for action steps.
        /// </summary>
        ProcessActionWorkflowNotEnabledForOnDemand = -2147089536,

        /// <summary>
        /// This process contains empty branches. Define or delete these branches and try again.
        /// </summary>
        ProcessEmptyBranches = -2147089511,

        /// <summary>
        /// Validation error: Process ID does not match Business Process definition.
        /// </summary>
        ProcessIdDoesNotMatchBusinessProcessDefinition = -2147089312,

        /// <summary>
        /// Validation error: Process ID cannot be empty.
        /// </summary>
        ProcessIdIsEmpty = -2147089319,

        /// <summary>
        /// Error occured when processing image. Reason: {0}
        /// </summary>
        ProcessImageFailure = -2147015341,

        /// <summary>
        /// The business process name contains invalid characters.
        /// </summary>
        ProcessNameContainsInvalidCharacters = -2147089512,

        /// <summary>
        /// The business process flow name is NULL or empty.
        /// </summary>
        ProcessNameIsNullOrEmpty = -2147089384,

        /// <summary>
        /// Validation error: Primary Stage ID cannot be empty.
        /// </summary>
        ProcessStageIdIsEmpty = -2147089311,

        /// <summary>
        /// Product, product family and bundle can only be updated in draft state.
        /// </summary>
        ProductCanOnlyBeUpdatedInDraft = -2147157611,

        /// <summary>
        /// You can't clone a child record of a retired product family.
        /// </summary>
        ProductCloneFailed = -2147086330,

        /// <summary>
        /// The product does not exist.
        /// </summary>
        ProductDoesNotExist = -2147206364,

        /// <summary>
        /// A property can only be created for a product family.
        /// </summary>
        ProductFamilyCanCreateDynamicProperty = -2146955257,

        /// <summary>
        /// The product family root parent record is locked by some other process.
        /// </summary>
        ProductFamilyRootParentisLocked = -2146955233,

        /// <summary>
        /// Product is already in Active State.
        /// </summary>
        ProductFromActiveToActiveState = -2147157630,

        /// <summary>
        /// You can't set a published product to the draft state.
        /// </summary>
        ProductFromActiveToDraftState = -2147157742,

        /// <summary>
        /// Product is already in Draft State.
        /// </summary>
        ProductFromDraftToDraftState = -2147157631,

        /// <summary>
        /// You can't retire a product that's in the draft state.
        /// </summary>
        ProductFromDraftToRetiredState = -2147157640,

        /// <summary>
        /// You can't revise a draft or a retired product.
        /// </summary>
        ProductFromDraftToRevisedState = -2147157741,

        /// <summary>
        /// You can't set a retired property to an active state.
        /// </summary>
        ProductFromRetiredToActiveState = -2147157641,

        /// <summary>
        /// It is not possible to move product from Retired to Draft State.
        /// </summary>
        ProductFromRetiredToDraftState = -2147157639,

        /// <summary>
        /// Product is already in Retired State.
        /// </summary>
        ProductFromRetiredToRetiredState = -2147157632,

        /// <summary>
        /// You can't retire this product family because one or more of its child records aren't retired.
        /// </summary>
        ProductHasUnretiredChild = -2147157744,

        /// <summary>
        /// The pricing percentage must be greater than or equal to zero and less than 100000.
        /// </summary>
        ProductInvalidPriceLevelPercentage = -2147206388,

        /// <summary>
        /// The number of decimal places on the quantity is invalid.
        /// </summary>
        ProductInvalidQuantityDecimal = -2147206393,

        /// <summary>
        /// The specified unit is not valid for this product.
        /// </summary>
        ProductInvalidUnit = -2147206380,

        /// <summary>
        /// You can’t add a kit to itself.
        /// </summary>
        ProductKitLoopBeingCreated = -2147206365,

        /// <summary>
        /// Loop exists in the kit hierarchy.
        /// </summary>
        ProductKitLoopExists = -2147206366,

        /// <summary>
        /// This product can't be published because it has too many properties. A product in your organization can't have more than {0} properties.
        /// </summary>
        ProductMaxPropertyLimitExceeded = -2146955251,

        /// <summary>
        /// The unit schedule id of the product is missing.
        /// </summary>
        ProductMissingUomSheduleId = -2147206381,

        /// <summary>
        /// The product number can not be null.
        /// </summary>
        ProductNoProductNumber = -2147206395,

        /// <summary>
        /// The substituted Product number cannot be a NULL.
        /// </summary>
        ProductNoSubstitutedProductNumber = -2147157616,

        /// <summary>
        /// Only Product Families can be parents to products.
        /// </summary>
        ProductOrBundleCannotBeAsParent = -2147157645,

        /// <summary>
        /// The specified product ID conflicts with the product ID of an existing record. Specify a different product ID and try again.
        /// </summary>
        ProductProductNumberExists = -2147206394,

        /// <summary>
        /// Product Recommendations feature is not enabled.
        /// </summary>
        ProductRecommendationsFeatureNotEnabled = -2147084800,

        /// <summary>
        /// The profile '{0}' has a circular reference which will prevent your data download. Please review the circular reference chain: {1} and remove the profile item association that causes the circular reference.
        /// </summary>
        ProfileContainsCircularReference = -2147020479,

        /// <summary>
        /// The profile '{0}' has a profile item {1} which contains a profile item association for {2}, however there does not exist a profile item for {2}. Please include the profile item and publish.
        /// </summary>
        ProfileContainsRelationshipWithoutEntity = -2147020478,

        /// <summary>
        /// The total number of users ('{0}') in this profile exceeds the allowed limit ('{1}'). Please limit the total number of users to be within the supported limit.
        /// </summary>
        ProfileCountUserLimitExceeded = -2147020492,

        /// <summary>
        /// This Profile Rule cannot be activated or deactivated by someone who is not its owner.
        /// </summary>
        ProfileRuleActivateDeactivateByNonOwner = -2147086078,

        /// <summary>
        /// You can't activate this rule until you resolve any missing rule criteria information in the rule items.
        /// </summary>
        ProfileRuleMissingRuleCriteria = -2147086080,

        /// <summary>
        /// Your rule can't be activated until the current active rule is deactivated. The active rule can only be deactivated by the rule owner.
        /// </summary>
        ProfileRulePublishedByOwner = -2147086077,

        /// <summary>
        /// An error occurred while authoring workflow. Please fix workflow definition and try again.
        /// </summary>
        ProfileRuleWorkflowAuthorGenericError = -2147086079,

        /// <summary>
        /// To enable auto capture, you need to&nbsp;set up Cortana Intelligence Customer Insights in Relationship Insights settings.
        /// </summary>
        ProvisioningNotCompleted = -2146889660,

        /// <summary>
        /// You need system administrator privileges to turn Relationship Insights on for your organization.
        /// </summary>
        ProvisionRIAccessNotAllowed = -2147204496,

        /// <summary>
        /// There is more than one approved version of the {0} language. You can only publish one version of each language.
        /// </summary>
        PublishArticle_TranslationWithMoreThanOneApprovedVersion = -2147085311,

        /// <summary>
        /// This workflow cannot be published because your organization has reached its limit for the number of workflows that can be published at the same time. (There is no limit on the number of draft workflows.) You can publish this workflow by unpublishing a different workflow, or by upgrading your license to a license that supports more workflows.
        /// </summary>
        PublishedWorkflowLimitForSkuReached = -2147201003,

        /// <summary>
        /// A published workflow can only be assigned to the caller.
        /// </summary>
        PublishedWorkflowOwnershipChange = -2147201012,

        /// <summary>
        /// Publishing Workflows while acting on behalf of another user is not allowed.
        /// </summary>
        PublishWorkflowWhileActingOnBehalfOfAnotherUserError = -2147200974,

        /// <summary>
        /// Publishing Workflows while impersonating another user is not allowed.
        /// </summary>
        PublishWorkflowWhileImpersonatingError = -2147200967,

        /// <summary>
        /// Do not modify the Quantity field when you update the primary unit.
        /// </summary>
        QuantityReadonly = -2147206376,

        /// <summary>
        /// The Inner and Outer Queries must be for the same entity.
        /// </summary>
        QueriesForDifferentEntities = -2147167568,

        /// <summary>
        /// The specified alias for the given entity in the condition does not exist.
        /// </summary>
        QueryBuilderAlias_Does_Not_Exist = -2147217142,

        /// <summary>
        /// An alias cannot be specified for an order clause for a non-aggregate Query. Use an attribute.
        /// </summary>
        QueryBuilderAliasNotAllowedForNonAggregateOrderBy = -2147217102,

        /// <summary>
        /// An alias is required for an order clause for an aggregate Query.
        /// </summary>
        QueryBuilderAliasRequiredForAggregateOrderBy = -2147217100,

        /// <summary>
        /// Attributes can not be returned when aggregate operation is specified.
        /// </summary>
        QueryBuilderAttribute_With_Aggregate = -2147217145,

        /// <summary>
        /// An attribute can either be an aggregate or a Group By but not both
        /// </summary>
        QueryBuilderAttributeCannotBeGroupByAndAggregate = -2147217097,

        /// <summary>
        /// An attribute cannot be specified for an order clause for an aggregate Query. Use an alias.
        /// </summary>
        QueryBuilderAttributeNotAllowedForAggregateOrderBy = -2147217103,

        /// <summary>
        /// A required attribute was not specified.
        /// </summary>
        QueryBuilderAttributeNotFound = -2147217122,

        /// <summary>
        /// AttributeFrom and AttributeTo must be either both specified or both omitted.
        /// </summary>
        QueryBuilderAttributePairMismatch = -2147217135,

        /// <summary>
        /// An attribute is required for an order clause for a non-aggregate Query.
        /// </summary>
        QueryBuilderAttributeRequiredForNonAggregateOrderBy = -2147217101,

        /// <summary>
        /// Incorrect filter condition or conditions.
        /// </summary>
        QueryBuilderBad_Condition = -2147217146,

        /// <summary>
        /// QueryByAttribute must specify a non-empty value array with the same number of elements as in the attributes array.
        /// </summary>
        QueryBuilderByAttributeMismatch = -2147217137,

        /// <summary>
        /// QueryByAttribute must specify a non-empty attribute array.
        /// </summary>
        QueryBuilderByAttributeNonEmpty = -2147217136,

        /// <summary>
        /// The specified columnset version is invalid.
        /// </summary>
        QueryBuilderColumnSetVersionMissing = -2147217133,

        /// <summary>
        /// Xml String can't be null.
        /// </summary>
        QueryBuilderDeserializeEmptyXml = -2147217116,

        /// <summary>
        /// An error occurred while processing Aggregates in Query
        /// </summary>
        QueryBuilderDeserializeInvalidAggregate = -2147217126,

        /// <summary>
        /// The only valid values for descending attribute are 'true', 'false', '1', and '0'.
        /// </summary>
        QueryBuilderDeserializeInvalidDescending = -2147217127,

        /// <summary>
        /// The only valid values for distinct attribute are 'true', 'false', '1', and '0'.
        /// </summary>
        QueryBuilderDeserializeInvalidDistinct = -2147217131,

        /// <summary>
        /// The only valid values for GetMinActiveRowVersion attribute are 'true', 'false', '1', and '0'.
        /// </summary>
        QueryBuilderDeserializeInvalidGetMinActiveRowVersion = -2147217125,

        /// <summary>
        /// The only valid values for groupby attribute are 'true', 'false', '1', and '0'.
        /// </summary>
        QueryBuilderDeserializeInvalidGroupBy = -2147217106,

        /// <summary>
        /// The only valid values for link-type attribute are 'natural', 'inner','in','exists','matchfirstrowusingcrossapply' and 'outer'.
        /// </summary>
        QueryBuilderDeserializeInvalidLinkType = -2147217129,

        /// <summary>
        /// The only valid values for mapping are 'logical' or 'internal' which is deprecated.
        /// </summary>
        QueryBuilderDeserializeInvalidMapping = -2147217130,

        /// <summary>
        /// The element node encountered is invalid.
        /// </summary>
        QueryBuilderDeserializeInvalidNode = -2147217124,

        /// <summary>
        /// The only valid values for no-lock attribute are 'true', 'false', '1', and '0'.
        /// </summary>
        QueryBuilderDeserializeInvalidNoLock = -2147217128,

        /// <summary>
        /// The only valid values for useraworderby attribute are 'true', 'false', '1', and '0'.
        /// </summary>
        QueryBuilderDeserializeInvalidUseRawOrderBy = -2147217155,

        /// <summary>
        /// The utc-offset attribute is not supported for deserialization.
        /// </summary>
        QueryBuilderDeserializeInvalidUtcOffset = -2147217123,

        /// <summary>
        /// Document Element can't be null.
        /// </summary>
        QueryBuilderDeserializeNoDocElemXml = -2147217115,

        /// <summary>
        /// FetchXML should have unique aliases.
        /// </summary>
        QueryBuilderDuplicateAlias = -2147217104,

        /// <summary>
        /// A required element was not specified.
        /// </summary>
        QueryBuilderElementNotFound = -2147217117,

        /// <summary>
        /// The entity name specified in fetchxml does not match the entity name specified in the Entity or Query Expression.
        /// </summary>
        QueryBuilderEntitiesDontMatch = -2147217112,

        /// <summary>
        /// Invalid alias for aggregate operation.
        /// </summary>
        QueryBuilderInvalid_Alias = -2147217143,

        /// <summary>
        /// Invalid value specified for type.
        /// </summary>
        QueryBuilderInvalid_Value = -2147217144,

        /// <summary>
        /// Aggregate {0} is not supported for attribute of type {1}.
        /// </summary>
        QueryBuilderInvalidAggregateAttribute = -2147217105,

        /// <summary>
        /// The attribute value provided is invalid.
        /// </summary>
        QueryBuilderInvalidAttributeValue = -2147217095,

        /// <summary>
        /// The specified columnset version is invalid.
        /// </summary>
        QueryBuilderInvalidColumnSetVersion = -2147217134,

        /// <summary>
        /// Unsupported condition operator.
        /// </summary>
        QueryBuilderInvalidConditionOperator = -2147217120,

        /// <summary>
        /// An invalid value was specified for dategrouping.
        /// </summary>
        QueryBuilderInvalidDateGrouping = -2147217099,

        /// <summary>
        /// Unsupported filter type. Valid values are 'and', or 'or'.
        /// </summary>
        QueryBuilderInvalidFilterType = -2147217118,

        /// <summary>
        /// Unsupported join operator.
        /// </summary>
        QueryBuilderInvalidJoinOperator = -2147217119,

        /// <summary>
        /// Unsupported logical operator: {0}.  Accepted values are ('and', 'or').
        /// </summary>
        QueryBuilderInvalidLogicalOperator = -2147217154,

        /// <summary>
        /// A valid order type must be set in the order before calling this method.
        /// </summary>
        QueryBuilderInvalidOrderType = -2147217121,

        /// <summary>
        /// Invalid page number in paging cookie.
        /// </summary>
        QueryBuilderInvalidPagingCookie = -2147217110,

        /// <summary>
        /// An attempt was made to update a non-updateable field.
        /// </summary>
        QueryBuilderInvalidUpdate = -2147217152,

        /// <summary>
        /// Converting from Query to EntityExpression failed. Link Node for order was not found.
        /// </summary>
        QueryBuilderLinkNodeForOrderNotFound = -2147217114,

        /// <summary>
        /// More than one intersect entity exists between the two entities specified.
        /// </summary>
        QueryBuilderMultipleIntersectEntities = -2147217138,

        /// <summary>
        /// No alias for the given entity in the condition was found.
        /// </summary>
        QueryBuilderNoAlias = -2147217141,

        /// <summary>
        /// The specified attribute does not exist on this entity.
        /// </summary>
        QueryBuilderNoAttribute = -2147217149,

        /// <summary>
        /// The no-attrs tag cannot be used in conjuction with Distinct set to true.
        /// </summary>
        QueryBuilderNoAttrsDistinctConflict = -2147217108,

        /// <summary>
        /// The specified entity was not found.
        /// </summary>
        QueryBuilderNoEntity = -2147217150,

        /// <summary>
        /// The specified entitykey was not found.
        /// </summary>
        QueryBuilderNoEntityKey = -2147217088,

        /// <summary>
        /// Order by columns do not match those in paging cookie.
        /// </summary>
        QueryBuilderPagingOrderBy = -2147217111,

        /// <summary>
        /// A report view does not exist for the specified entity.
        /// </summary>
        QueryBuilderReportView_Does_Not_Exist = -2147217139,

        /// <summary>
        /// The only valid values for isquickfindfields attribute are 'true', 'false', '1', and '0'.
        /// </summary>
        QueryBuilderSerializationInvalidIsQuickFindFilter = -2147217096,

        /// <summary>
        /// Fetch does not support where clause with conditions from linkentity.
        /// </summary>
        QueryBuilderSerialzeLinkTopCriteria = -2147217132,

        /// <summary>
        /// An unexpected error occurred.
        /// </summary>
        QueryBuilderUnexpected = -2147217151,

        /// <summary>
        /// A value greater than zero must be specified.
        /// </summary>
        QueryBuilderValue_GreaterThanZero = -2147217140,

        /// <summary>
        /// The Query contained a secured attribute to which the caller does not have access
        /// </summary>
        QueryContainedSecuredAttributeWithoutAccess = -2147158778,

        /// <summary>
        /// The query references a field that does not exist in Dynamics 365: "{0}"
        /// </summary>
        QueryFilterConditionAttributeNotPresentInExpressionEntity = -2147020519,

        /// <summary>
        /// Query cannot be specified for a static list.
        /// </summary>
        QueryNotValidForStaticList = -2147158270,

        /// <summary>
        /// Query parameter {0} must be defined only once within the data set.
        /// </summary>
        QueryParameterNotUnique = -2147098613,

        /// <summary>
        /// You must enter the target queue. Provide a valid value in the Queue field and try again.
        /// </summary>
        QueueIdNotPresent = -2147220184,

        /// <summary>
        /// You must enter the name of the record that you would like to put in the queue. Provide a valid value in the Queue Item field and try again.
        /// </summary>
        QueueItemNotPresent = -2147220183,

        /// <summary>
        /// Delivery method for mailbox associated with a queue cannot be outlook client.
        /// </summary>
        QueueMailboxUnexpectedDeliveryMethod = -2147098096,

        /// <summary>
        /// The {0} entity doesn't have a quick create form or the number of nested quick create forms has exceeded the maximum number allowed.
        /// </summary>
        QuickCreateDisabledOnEntity = -2147088111,

        /// <summary>
        /// The entityLogicalName isn't valid. This value can't be null or empty, and it must represent an entity in the organization.
        /// </summary>
        QuickCreateInvalidEntityName = -2147088112,

        /// <summary>
        /// QuickFindQueryRecordLimit exceeded. Cannot perform this operation.
        /// </summary>
        QuickFindQueryRecordLimitExceeded = -2147164124,

        /// <summary>
        /// "Only one quickfind saved query can exist for an entity. There already exists a quick-find saved query for entity with objecttypecode: {0}"
        /// </summary>
        QuickFindSavedQueryAlreadyExists = -2147187398,

        /// <summary>
        /// The SDK does not support creating a form of type "Quick". This form type is reserved for internal use only.
        /// </summary>
        QuickFormNotCustomizableThroughSdk = -2147158439,

        /// <summary>
        /// The currency of the record does not match the currency of the price list.
        /// </summary>
        QuoteAndSalesOrderCurrencyNotEqual = -2147185426,

        /// <summary>
        /// Quote cannot be revised as there already exists another quote in Draft/Active state and with same quote number.
        /// </summary>
        QuoteReviseExistingActiveQuote = -2147185408,

        /// <summary>
        /// Reactivate entity key is only supported for failed job
        /// </summary>
        ReactivateEntityKeyOnlyForFailedJobs = -2147088233,

        /// <summary>
        /// Plugin Execution Intent of current execution context is not compatible with its parent context
        /// </summary>
        ReadIntentIncompatible = -2147088255,

        /// <summary>
        /// You can't create and assign a value to a property instance for a read-only property.
        /// </summary>
        ReadOnlyCreateValidationFailed = -2147086334,

        /// <summary>
        /// You can't update the property instance for a read-only property.
        /// </summary>
        ReadOnlyUpdateValidationFailed = -2147086333,

        /// <summary>
        /// The read-only access mode is not supported
        /// </summary>
        ReadOnlyUserNotSupported = -2147214016,

        /// <summary>
        /// Field {0} of type {1} does not support Recalculate action. Recalculate action can only be invoked for rollup field.
        /// </summary>
        RecalculateNotSupportedOnNonRollupField = -2147089068,

        /// <summary>
        /// One or more recommendation models couldn't be activated. Try activating the existing recommendation models separately from the Azure service connection.
        /// </summary>
        RecommendationAzureConnectionCascadeActivateFailed = -2147084749,

        /// <summary>
        /// Failed to connect to the Azure Recommendations service. Check that the service URL and the Azure account key are valid and the service subscription is active.
        /// </summary>
        RecommendationAzureConnectionFailed = -2147084751,

        /// <summary>
        /// The Azure Machine Learning recommendation service connection must be activated before the model can be activated. Please activate the recommendation service connection and try again.
        /// </summary>
        RecommendationModelActivateConnectionMustBeActive = -2147084793,

        /// <summary>
        /// The model version used must be successfully built before the model can be activated.
        /// </summary>
        RecommendationModelActiveVersionInvalidStatus = -2147084798,

        /// <summary>
        /// The model version used is empty. To activate the model, specify the model version.
        /// </summary>
        RecommendationModelActiveVersionNotSet = -2147084799,

        /// <summary>
        /// The Azure Machine Learning recommendation service connection must be activated before building a recommendation model. Please activate the recommendation service connection and try again.
        /// </summary>
        RecommendationModelBuildConnectionMustBeActive = -2147084794,

        /// <summary>
        /// The recommendation model has expired. Change the Valid Until date and try to activate the model again.
        /// </summary>
        RecommendationModelExpired = -2147084792,

        /// <summary>
        /// The recommendation model mapping values for entity, mapping type and version must be unique.
        /// </summary>
        RecommendationModelMappingDuplicateRecord = -2147084784,

        /// <summary>
        /// You can't modify a Recommendation entity if it has a corresponding Basket entity.
        /// </summary>
        RecommendationModelMappingReadOnly = -2147084783,

        /// <summary>
        /// The RecommendationModel Version is selected as the active version on a model and cannot be deleted.
        /// </summary>
        RecommendationModelVersionActive = -2147084768,

        /// <summary>
        /// A workflow to build a model is already in progress. You can't start another build workflow until the current workflow has finished.
        /// </summary>
        RecommendationModelVersionBuildInProgress = -2147084767,

        /// <summary>
        /// A model version with the same name already exists. Specify a different name.
        /// </summary>
        RecommendationModelVersionDuplicateName = -2147084766,

        /// <summary>
        /// Azure Machine Learning product recommendations are temporarily unavailable. Only catalog recommendations are available.
        /// </summary>
        RecommendationsUnavailable = -2147084795,

        /// <summary>
        /// Unable to retrieve document suggestions from the document source.
        /// </summary>
        RecommendedDocumentsRetrievalFailure = -2147020779,

        /// <summary>
        /// Unable to retrieve document suggestions from the document source.
        /// </summary>
        RecommendedDocumentsRetrievalFailureWhenSPSiteNotConfigured = -2147020760,

        /// <summary>
        /// The currency of the record does not match the currency of the price list.
        /// </summary>
        RecordAndOpportunityCurrencyNotEqual = -2147185425,

        /// <summary>
        /// The currency of the record does not match the currency of the price list.
        /// </summary>
        RecordAndPricelistCurrencyNotEqual = -2147185418,

        /// <summary>
        /// You can only revise an active product.
        /// </summary>
        RecordCanOnlyBeRevisedFromActiveState = -2147157885,

        /// <summary>
        /// Expected records count is {0}. Current records count is:{1}
        /// </summary>
        RecordCountExceededForExcelOnlineError = -2147015594,

        /// <summary>
        /// A record with the specified key values does not exist in {0} entity
        /// </summary>
        RecordNotFoundByEntityKey = -2147088239,

        /// <summary>
        /// The record could not be updated because the original record no longer exists in Microsoft Dynamics 365.
        /// </summary>
        RecordResolutionFailed = -2147158525,

        /// <summary>
        /// The calendar type is not supported.
        /// </summary>
        RecurrenceCalendarTypeNotSupported = -2147163882,

        /// <summary>
        /// The recurrence pattern end date is invalid.
        /// </summary>
        RecurrenceEndDateTooBig = -2147163879,

        /// <summary>
        /// The recurrence pattern has no occurrences.
        /// </summary>
        RecurrenceHasNoOccurrence = -2147163881,

        /// <summary>
        /// Cannot delete a rule that is attached to an existing rule master. Delete the rule by using the parent entity.
        /// </summary>
        RecurrenceRuleDeleteFailure = -2147163887,

        /// <summary>
        /// Cannot update a rule that is attached to an existing rule master. Update the rule by using the parent entity.
        /// </summary>
        RecurrenceRuleUpdateFailure = -2147163888,

        /// <summary>
        /// The recurrence pattern start date is invalid.
        /// </summary>
        RecurrenceStartDateTooSmall = -2147163880,

        /// <summary>
        /// The series has invalid ExpansionStateCode.
        /// </summary>
        RecurringSeriesCompleted = -2147163893,

        /// <summary>
        /// The recurring series master record is locked by some other process.
        /// </summary>
        RecurringSeriesMasterIsLocked = -2147163885,

        /// <summary>
        /// The entity relationship role of the referencing entity is required when creating a new one-to-many entity relationship.
        /// </summary>
        RefEntityRelationshipRoleRequired = -2147187600,

        /// <summary>
        /// This entity has a primary field that is logical and therefore cannot be the referenced entity in a one-to-many relationship
        /// </summary>
        ReferencedEntityHasLogicalPrimaryNameField = -2147204306,

        /// <summary>
        /// Referenced entities of a relationship must have a lookup view
        /// </summary>
        ReferencedEntityMustHaveLookupView = -2147204297,

        /// <summary>
        /// Referencing entities in a relationship cannot be a component in a solution.
        /// </summary>
        ReferencingEntityCannotBeSolutionAware = -2147204272,

        /// <summary>
        /// Referencing entities of a relationship must have an association view
        /// </summary>
        ReferencingEntityMustHaveAssociationView = -2147204296,

        /// <summary>
        /// Found unpublished row outside of active solution: SiteMapId = {0}, SolutionId = {1}
        /// </summary>
        RefferedSolutionIsDifferent = -2147155678,

        /// <summary>
        /// Either the parent or child entity does not exist
        /// </summary>
        ReflexiveEntityParentOrChildDoesNotExist = -2147220600,

        /// <summary>
        /// The NavPaneDisplayOption attribute is required for the Referencing Role of a one-to-many relationship {0}.
        /// </summary>
        RefRoleNavPaneDisplayOptionRequired = -2147088232,

        /// <summary>
        /// Failed to retrieve regarding object values.
        /// </summary>
        RegardingObjectValuesRetrievalFailure = -2147020782,

        /// <summary>
        /// The related entity already exists in this profile.
        /// </summary>
        RelatedEntityAlreadyExistsInProfile = -2147093986,

        /// <summary>
        /// The related entity {0} of the mobile offline profile item association {1} of the mobile offline profile item {2} doesn’t exist in the profile items of profile {3}.
        /// </summary>
        RelatedEntityDoesNotExistInProfileItem = -2147087986,

        /// <summary>
        /// The related entity doesn’t exist in the profile items.
        /// </summary>
        RelatedEntityDoesNotExistsInProfile = -2147093985,

        /// <summary>
        /// An unexpected error occurred while creating the profile association. Please try again.
        /// </summary>
        RelatedEntityGenericError = -2147093984,

        /// <summary>
        /// Failed to retrieve related records.
        /// </summary>
        RelatedRecordsFailure = -2147020781,

        /// <summary>
        /// You have specified one or more profile item associations for the entities defined in the profile '{0}'. Please review the profile item associations involving the entity '{1}' which has an association count of {2} and exceeds the supported limit of {3}.
        /// </summary>
        RelationshipGraphLimitExceeded = -2147020487,

        /// <summary>
        /// Relationship Insights feature can't be disabled
        /// </summary>
        RelationshipInsightsFeatureDisableError = -2147204462,

        /// <summary>
        /// Relationship Insights feature is not enabled or RI package is not installed
        /// </summary>
        RelationshipInsightsFeatureNotEnabledError = -2147204461,

        /// <summary>
        /// You need Dynamics 365 (online) to use the Relationship Insights SDK.
        /// </summary>
        RelationshipIntelligenceSDKInvocationError = -2147204490,

        /// <summary>
        /// The specified relationship is not a custom relationship
        /// </summary>
        RelationshipIsNotCustomRelationship = -2147192822,

        /// <summary>
        /// Identifiers cannot be more than {1} characters long. The name provided: {0} length is greater than maxlength {1} characters.
        /// </summary>
        RelationshipNameLengthExceedsLimit = -2147188694,

        /// <summary>
        /// This relationship cannot be created because neither entity is enabled for officegraph.
        /// </summary>
        RelationshipNotCreatedForOfficeGraphError = -2147204555,

        /// <summary>
        /// This relationship cannot be updated for officegraph because neither entity is enabled for officegraph.
        /// </summary>
        RelationshipNotUpdatedForOfficeGraphError = -2147204554,

        /// <summary>
        /// The relationship role name {0} does not match either expected entity name of {1} or {2}.
        /// </summary>
        RelationshipRoleMismatch = -2147187609,

        /// <summary>
        /// There must be two entity relationship role nodes when creating a new many-to-many entity relationship.
        /// </summary>
        RelationshipRoleNodeNumberInvalid = -2147187607,

        /// <summary>
        /// Selected Relationship for entity already exists in profile.
        /// </summary>
        RelatioshipAlreadyExists = -2147093983,

        /// <summary>
        /// Report does not exist. ReportId:{0}
        /// </summary>
        ReportDoesNotExist = -2147220327,

        /// <summary>
        /// The file is too large and cannot be uploaded. Please reduce the size of the file and try again.
        /// </summary>
        ReportFileTooBig = -2147188073,

        /// <summary>
        /// You have uploaded an empty file.  Please select a new file and try again.
        /// </summary>
        ReportFileZeroLength = -2147188074,

        /// <summary>
        /// A category option for the reports was not found.
        /// </summary>
        ReportImportCategoryOptionNotFound = -2147160024,

        /// <summary>
        /// The report is not a Reporting Services report.
        /// </summary>
        ReportingServicesReportExpected = -2147220348,

        /// <summary>
        /// Error occurred while viewing locally processed report. ReportId:{0}
        /// </summary>
        ReportLocalProcessingError = -2147187952,

        /// <summary>
        /// Creating this parental association would create a loop in Reports hierarchy.
        /// </summary>
        ReportLoopBeingCreated = -2147220328,

        /// <summary>
        /// Loop exists in the reports hierarchy.
        /// </summary>
        ReportLoopExists = -2147220329,

        /// <summary>
        /// Credentials have not been supplied for a data source used by the report. ReportId:{0}
        /// </summary>
        ReportMissingDataSourceCredentialsError = -2147187951,

        /// <summary>
        /// A data source expected by the report has not been supplied. ReportId:{0}
        /// </summary>
        ReportMissingDataSourceError = -2147187950,

        /// <summary>
        /// The SOAP endpoint used by the ReportViewer control could not be accessed.
        /// </summary>
        ReportMissingEndpointError = -2147187949,

        /// <summary>
        /// A parameter expected by the report has not been supplied. ReportId:{0}
        /// </summary>
        ReportMissingParameterError = -2147187948,

        /// <summary>
        /// No source has been specified for the report. ReportId:{0}
        /// </summary>
        ReportMissingReportSourceError = -2147187947,

        /// <summary>
        /// Report not available
        /// </summary>
        ReportNotAvailable = -2147188071,

        /// <summary>
        /// The report could not be updated because either the parent report or the child report is not customizable.
        /// </summary>
        ReportParentChildNotCustomizable = -2147187921,

        /// <summary>
        /// Report upload failed due to RDL Sandboxing limitations on the Report Server.
        /// </summary>
        ReportRdlSandboxing = -2147188077,

        /// <summary>
        /// An error occurred during report rendering.
        /// </summary>
        ReportRenderError = -2147220332,

        /// <summary>
        /// The report contains a security violation. ReportId:{0}
        /// </summary>
        ReportSecurityError = -2147187946,

        /// <summary>
        /// Cannot contact report server from given URL
        /// </summary>
        ReportServerInvalidUrl = -2147187967,

        /// <summary>
        /// Not enough privilege to configure report server
        /// </summary>
        ReportServerNoPrivilege = -2147187966,

        /// <summary>
        /// Report server SP2 Workgroup does not have the hotfix for role creation
        /// </summary>
        ReportServerSP2HotFixNotApplied = -2147187959,

        /// <summary>
        /// Unknown exception thrown by report server
        /// </summary>
        ReportServerUnknownException = -2147187968,

        /// <summary>
        /// Report server does not meet the minimal version requirement
        /// </summary>
        ReportServerVersionLow = -2147187965,

        /// <summary>
        /// The report is not a valid type.  It cannot be uploaded or downloaded.
        /// </summary>
        ReportTypeBlocked = -2147188075,

        /// <summary>
        /// Reporting Services reports cannot be uploaded. If you want to create a new report, please use the Report Wizard.
        /// </summary>
        ReportUploadDisabled = -2147188076,

        /// <summary>
        /// An error occurred during report rendering. ReportId:{0}
        /// </summary>
        ReportViewerError = -2147187924,

        /// <summary>
        /// Request is not authenticated.
        /// </summary>
        RequestIsNotAuthenticated = -2147204350,

        /// <summary>
        /// Request message length is too large.
        /// </summary>
        RequestLengthTooLarge = -2147204726,

        /// <summary>
        /// You can't delete this bundle item because it's a required product in the bundle.
        /// </summary>
        RequiredBundleItemCannotBeUpdated = -2146955255,

        /// <summary>
        /// You can't delete this product record because it's a required product in a bundle.
        /// </summary>
        RequiredBundleProductCannotBeDeleted = -2146955256,

        /// <summary>
        /// A category option for the reports was not found.
        /// </summary>
        RequiredChildReportHasOtherParent = -2147160023,

        /// <summary>
        /// One or more source columns used in the transformation do not exist in the source file.
        /// </summary>
        RequiredColumnsNotFoundInImportFile = -2147220605,

        /// <summary>
        /// Required field missing.
        /// </summary>
        RequiredFieldMissing = -2147220992,

        /// <summary>
        /// To move to the next stage, complete the required steps.
        /// </summary>
        RequiredProcessStepIsNull = -2147089382,

        /// <summary>
        /// The update operation cannot be completed because the import map used for the update is invalid.
        /// </summary>
        RequireValidImportMapForUpdate = -2147158528,

        /// <summary>
        /// The solution unique name '{0}' is restricted and can only be used by internal solutions.
        /// </summary>
        RestrictedSolutionName = -2147160030,

        /// <summary>
        /// Inherited roles cannot be modified.
        /// </summary>
        RestrictInheritedRole = -2147204782,

        /// <summary>
        /// You can't add a retired product to a bundle.
        /// </summary>
        RetiredProductToBundle = -2147157613,

        /// <summary>
        /// Cannot retrieve properties for provided entity image
        /// </summary>
        RetrieveImagePropertiesFail = -2147015342,

        /// <summary>
        /// Unexpected error during retrieve organization information. The dependent services might not be available at this time. Please retry later.
        /// </summary>
        RetrieveOrganizationInfoUnexpectedError = -2147015935,

        /// <summary>
        /// This record isn't available while you're offline.  Reconnect and try again.
        /// </summary>
        RetrieveRecordOfflineErrorCode = -2147093997,

        /// <summary>
        /// Unexpected error during retrieve user license information. The dependent services might not be available at this time. Please retry later.
        /// </summary>
        RetrieveUserLicenseUnexpectedError = -2147015936,

        /// <summary>
        /// The action was failed after {0} times of retry. InnerException is: {1}.
        /// </summary>
        RetryFailed = -2147159995,

        /// <summary>
        /// The ribbon item '{0}' is dependent on entity {1}.
        /// </summary>
        RibbonImportDependencyMissingEntity = -2147159804,

        /// <summary>
        /// The ribbon item '{0}' is dependent on ribbon control id='{1}'.
        /// </summary>
        RibbonImportDependencyMissingRibbonControl = -2147159801,

        /// <summary>
        /// The ribbon item '{0}' is dependent on &lt;{1} Id="{2}" /&gt;.
        /// </summary>
        RibbonImportDependencyMissingRibbonElement = -2147159803,

        /// <summary>
        /// The ribbon item '{0}' is dependent on Web resource id='{1}'.
        /// </summary>
        RibbonImportDependencyMissingWebResource = -2147159802,

        /// <summary>
        /// The ribbon element with the Id:{0} cannot be imported because an existing ribbon element with the same Id already exists.
        /// </summary>
        RibbonImportDuplicateElementId = -2147159797,

        /// <summary>
        /// The solution cannot be imported because the {0} entity contains a Ribbon definition, which is not supported for that entity. Remove the RibbonDiffXml node from the entity definition and try to import again.
        /// </summary>
        RibbonImportEntityNotSupported = -2147159805,

        /// <summary>
        /// The definition of the ribbon being imported will remove the Microsoft Dynamics 365 home tab. Include a home tab definition, or a ribbon will not be displayed in areas of the application that display the home tab.
        /// </summary>
        RibbonImportHidingBasicHomeTab = -2147159807,

        /// <summary>
        /// Ribbon customizations cannot hide the <jewel> node. Any ribbon customization that hides this node is ignored during import and will not be exported.</jewel>
        /// </summary>
        RibbonImportHidingJewel = -2147159798,

        /// <summary>
        /// The RibbonDiffXml in this solution contains a reference to an invalid privilege: {0}. Update the RibbonDiffXml to reference a valid privilege and try importing again.
        /// </summary>
        RibbonImportInvalidPrivilegeName = -2147159806,

        /// <summary>
        /// CustomAction Id '{0}' cannot override '{1}' because '{2}' does not match the CustomAction Location value.
        /// </summary>
        RibbonImportLocationAndIdDoNotMatch = -2147159799,

        /// <summary>
        /// Ribbon customizations cannot be made to the following top-level ribbon nodes: <ribbon>, <contextualgroups>, and <tabs>.</tabs></contextualgroups></ribbon>
        /// </summary>
        RibbonImportModifyingTopLevelNode = -2147159800,

        /// <summary>
        /// We can’t import this ribbon element because the ID length exceeds the maximum length of 128 characters: {0}
        /// </summary>
        RibbonImportRibbonDiffIdInvalidLength = -2147159796,

        /// <summary>
        /// Relationship Insights hasn’t been turned on for your organization {0}.
        /// </summary>
        RINotProvisioned = -2147204479,

        /// <summary>
        /// A role with the specified name '{0}' already exists on business unit {1} and Solution Id {3}. Role id: {2}
        /// </summary>
        RoleAlreadyExists = -2147216381,

        /// <summary>
        /// You haven't been authorized to use this app.\nCheck with your system administrator to update your settings.
        /// </summary>
        RoleNotEnabledForTabletApp = -2147094013,

        /// <summary>
        /// Calculations can't be performed online because the calculation limit of {0} related records has been reached.
        /// </summary>
        RollupAggregateQueryRecordLimitExceeded = -2147164123,

        /// <summary>
        /// Calculations can't be performed at this time because the calculation limit has been reached. Please wait and try again.
        /// </summary>
        RollupCalculationLimitReached = -2147089055,

        /// <summary>
        /// Required dependent field {0} for rollup field cannot be created as another field with same name already exists. Please use an alternative name to create the rollup field.
        /// </summary>
        RollupDependentFieldNameAlreadyExists = -2147089066,

        /// <summary>
        /// The aggregate function {0} isn’t allowed.
        /// </summary>
        RollupFieldAggregateFunctionNotAllowed = -2147089082,

        /// <summary>
        /// The aggregate function {0} isn’t allowed when the rollup field is a {1} data type.
        /// </summary>
        RollupFieldAggregateFunctionNotAllowedForRollupFieldDataType = -2147089083,

        /// <summary>
        /// The {0} data type with format {1} isn’t allowed for the aggregated field when the rollup field is a {2} data type with format {3}.
        /// </summary>
        RollupFieldAndAggregateFieldDataTypeFormatMismatch = -2147089084,

        /// <summary>
        /// The calculation failed because the rollup field definition is invalid. Contact your system administrator.
        /// </summary>
        RollupFieldDefinitionNotValid = -2147089069,

        /// <summary>
        /// Rollup field {0} depends on this field. It can only be deleted by deleting the corresponding rollup field {0}.
        /// </summary>
        RollupFieldDependentFieldCannotDeleted = -2147089087,

        /// <summary>
        /// User does not have write permission on {0} record {1} with ID:{2} to calculate rollup field.
        /// </summary>
        RollupFieldNoWriteAccess = -2147164121,

        /// <summary>
        /// The {0} data type isn’t allowed for the aggregated field when the rollup field is a {1} data type.
        /// </summary>
        RollupFieldsAggregateFieldDataTypeNotAllowedSimilarRollupFieldDataType = -2147089091,

        /// <summary>
        /// The aggregated field {0} doesn’t belong to the related entity {1}.
        /// </summary>
        RollupFieldsAggregateFieldNotBelongToRelatedEntity = -2147089088,

        /// <summary>
        /// The aggregated field {0} doesn’t belong to the source entity {1}.
        /// </summary>
        RollupFieldsAggregateFieldNotBelongToSourceEntity = -2147089089,

        /// <summary>
        /// Aggregated field {0} does not belong to entity {1}
        /// </summary>
        RollupFieldsAggregateFieldNotPartOfEntity = -2147089097,

        /// <summary>
        /// The {0} data type isn’t allowed for the aggregated field when the aggregate function is {1}.
        /// </summary>
        RollupFieldsAggregateFunctionTypeMismatch = -2147089094,

        /// <summary>
        /// An aggregate function and an aggregated field must be provided for the rollup.
        /// </summary>
        RollupFieldsAggregateNotDefined = -2147089098,

        /// <summary>
        /// The aggregated field must be either a simple field or a basic calculated field.
        /// </summary>
        RollupFieldsAggregateOnRollupFieldOrComplexCalcFieldNotAllowed = -2147089092,

        /// <summary>
        /// The {0} data type isn’t valid for the rollup field.
        /// </summary>
        RollupFieldsDataTypeNotValid = -2147089090,

        /// <summary>
        /// The rollup field definition isn't valid.
        /// </summary>
        RollupFieldsGeneric = -2147089093,

        /// <summary>
        /// The source entity filter must use either a simple field or a basic calculated field. It can't use a rollup field, or a calculated field that is using a rollup field.
        /// </summary>
        RollupFieldSourceFilterFieldNotAllowed = -2147089080,

        /// <summary>
        /// The source entity {0} hierarchy doesn’t exist.
        /// </summary>
        RollupFieldsSourceEntityNotHierarchical = -2147089099,

        /// <summary>
        /// The source entity {0} filter condition {1} isn’t valid.
        /// </summary>
        RollupFieldsSourceFilterConditionInvalid = -2147089096,

        /// <summary>
        /// Related entity {0} is not allowed for rollups.
        /// </summary>
        RollupFieldsTargetEntityNotValid = -2147089070,

        /// <summary>
        /// The related entity {0} filter condition {1} isn’t valid.
        /// </summary>
        RollupFieldsTargetFilterConditionInvalid = -2147089095,

        /// <summary>
        /// 1:N relationship {0} from the source entity {1} to the related entity {2} doesn’t exist.
        /// </summary>
        RollupFieldsTargetRelationshipNotPartOfOneToNRelationship = -2147089100,

        /// <summary>
        /// The related entity is empty. It must be provided when the source entity hierarchy isn’t used for the rollup.
        /// </summary>
        RollupFieldsTargetRelationshipNull = -2147089101,

        /// <summary>
        /// Self referential 1:N relationships are not allowed for the rollup field.
        /// </summary>
        RollupFieldsTargetSameAsSourceEntity = -2147089071,

        /// <summary>
        /// The feature is not supported in the current version of the product
        /// </summary>
        RollupFieldsV2FeatureNotEnabled = -2147089051,

        /// <summary>
        /// The target entity filter must use either a simple field or a basic calculated field. It can't use a rollup field, or a calculated field that is using a rollup field.
        /// </summary>
        RollupFieldTargetFilterFieldNotAllowed = -2147089079,

        /// <summary>
        /// The formula field isn’t valid.
        /// </summary>
        RollupFormulaFieldInvalid = -2147089056,

        /// <summary>
        /// The {0} attribute is not allowed for filter condition.
        /// </summary>
        RollupInvalidAttributeForFilterCondition = -2147089052,

        /// <summary>
        /// The field {0} is either a rollup field or a rollup dependent field or a calculated field. Such fields are not allowed in workflow wait condition.
        /// </summary>
        RollupOrCalcNotAllowedInWorkflowWaitCondition = -2147089065,

        /// <summary>
        /// Target related entity can only be used for {0} entity for rollup over {1} type entities.
        /// </summary>
        RollupTargetLinkedEntityCanOnlyUsedForActivityPartyEntities = -2147089053,

        /// <summary>
        /// Target related entity is only supported for rollup over {0} type entities.
        /// </summary>
        RollupTargetLinkedEntityOnlySupportedForActivityEntities = -2147089054,

        /// <summary>
        /// Target Linked Relationship {0} is not valid.
        /// </summary>
        RollupTargetLinkedRelationshipNotValid = -2147089050,

        /// <summary>
        /// Root Business unit cannot be disabled.
        /// </summary>
        RootBusinessUnitCannotBeDisabled = -2147213981,

        /// <summary>
        /// Microsoft Dynamics 365 has been configured to use server-side synchronization to process email. This error occurs because some clients are still configured to use the legacy Email router. You need to uninstall the Email router client application on any servers it was installed on.
        /// </summary>
        RouterIsDisabled = -2147098042,

        /// <summary>
        /// The route type is unsupported
        /// </summary>
        RouteTypeUnsupported = -2147220247,

        /// <summary>
        /// This object type can not be routed.
        /// </summary>
        RoutingNotAllowed = -2147220249,

        /// <summary>
        /// This Routing Rule Set cannot be activated or deactivated by someone who is not its owner.
        /// </summary>
        RoutingRuleActivateDeactivateByNonOwner = -2147157883,

        /// <summary>
        /// You can't activate this rule until you resolve any missing rule criteria information in the rule items.
        /// </summary>
        RoutingRuleMissingRuleCriteria = -2147157897,

        /// <summary>
        /// The Routing Rule Set cannot be published or unpublished by someone who is not its owner.
        /// </summary>
        RoutingRulePublishedByNonOwner = -2147157896,

        /// <summary>
        /// Your rule can't be activated until the current active rule is deactivated. The active rule can only be deactivated by the rule owner.
        /// </summary>
        RoutingRulePublishedByOwner = -2147157898,

        /// <summary>
        /// rowguid is a reserved name and cannot be used as an identifier
        /// </summary>
        RowGuidIsNotValidName = -2147192831,

        /// <summary>
        /// Error occurred while canceling the batch operation.
        /// </summary>
        RSCancelBatchError = -2147187940,

        /// <summary>
        /// Error occurred while creating a batch operation.
        /// </summary>
        RSCreateBatchError = -2147187936,

        /// <summary>
        /// Error occurred while deleting an item from the report server.
        /// </summary>
        RSDeleteItemError = -2147187945,

        /// <summary>
        /// Error occurred while performing the batch operation.
        /// </summary>
        RSExecuteBatchError = -2147187939,

        /// <summary>
        /// Error occurred while finding an item on the report server.
        /// </summary>
        RSFindItemsError = -2147187944,

        /// <summary>
        /// Error occurred while getting the data source contents.
        /// </summary>
        RSGetDataSourceContentsError = -2147187942,

        /// <summary>
        /// Error occurred while fetching current data sources.
        /// </summary>
        RSGetItemDataSourcesError = -2147187935,

        /// <summary>
        /// Error occurred while fetching the report.
        /// </summary>
        RSGetItemTypeError = -2147187925,

        /// <summary>
        /// Error occurred while fetching the number of snapshots stored for the report.
        /// </summary>
        RSGetReportHistoryLimitError = -2147187938,

        /// <summary>
        /// Error occurred while getting report parameters.
        /// </summary>
        RSGetReportParametersError = -2147187933,

        /// <summary>
        /// Error occurred while fetching the list of data extensions installed on the report server.
        /// </summary>
        RSListExtensionsError = -2147187941,

        /// <summary>
        /// Error occurred while fetching the report history snapshots.
        /// </summary>
        RSListReportHistoryError = -2147187937,

        /// <summary>
        /// Cannot move report item {0} to {1}
        /// </summary>
        RSMoveItemError = -2147187920,

        /// <summary>
        /// The parameter type of the report is not valid.
        /// </summary>
        RSReportParameterTypeMismatchError = -2147187927,

        /// <summary>
        /// Error occurred while setting the data source contents.
        /// </summary>
        RSSetDataSourceContentsError = -2147187943,

        /// <summary>
        /// Error occurred while setting execution options.
        /// </summary>
        RSSetExecutionOptionsError = -2147187931,

        /// <summary>
        /// Error occurred while setting the data source.
        /// </summary>
        RSSetItemDataSourcesError = -2147187934,

        /// <summary>
        /// Error occurred while setting property values for the report.
        /// </summary>
        RSSetPropertiesError = -2147187926,

        /// <summary>
        /// Error occurred while setting report history snapshot limit.
        /// </summary>
        RSSetReportHistoryLimitError = -2147187929,

        /// <summary>
        /// Error occurred while setting report history snapshot options.
        /// </summary>
        RSSetReportHistoryOptionsError = -2147187930,

        /// <summary>
        /// Error occurred while setting report parameters.
        /// </summary>
        RSSetReportParametersError = -2147187932,

        /// <summary>
        /// Error occurred while taking snapshot of a report.
        /// </summary>
        RSUpdateReportExecutionSnapshotError = -2147187928,

        /// <summary>
        /// You can't activate this rule because it contains a deleted stage or stage category. Fix the rule and try again.
        /// </summary>
        RuleActivationNotAllowedWithDeletedStages = -2147090412,

        /// <summary>
        /// Record creation rule for the specified channel and queue already exists. You can't create another one.
        /// </summary>
        RuleAlreadyExistsWithSameQueueAndChannel = -2147157884,

        /// <summary>
        /// This routing rule is already in Active state.
        /// </summary>
        RuleAlreadyInactiveState = -2147157934,

        /// <summary>
        /// You can not deactivate a draft routing rule.
        /// </summary>
        RuleAlreadyInDraftState = -2147157933,

        /// <summary>
        /// The selected duplicate detection rule is already being published.
        /// </summary>
        RuleAlreadyPublishing = -2147187660,

        /// <summary>
        /// You can't create this rule because it contains a cyclical reference. Fix the rule and try again.
        /// </summary>
        RuleCreationNotAllowedForCyclicReferences = -2147090414,

        /// <summary>
        /// No rules were found that match the criteria.
        /// </summary>
        RuleNotFound = -2147187661,

        /// <summary>
        /// The current rule definition cannot be edited in the Business rule editor.
        /// </summary>
        RuleNotSupportedForEditor = -2147090425,

        /// <summary>
        /// One or more rules cannot be unpublished, either because they are in the process of being published, or are in a state where they cannot be unpublished.
        /// </summary>
        RulesInInconsistentStateFound = -2147187677,

        /// <summary>
        /// The most recent customized ribbon for a tab on this page cannot be generated. The out-of-box version of the ribbon is displayed instead.
        /// </summary>
        RuntimeRibbonXmlValidation = -2147158415,

        /// <summary>
        /// Failed to acquire S2S access token from authorization server.
        /// </summary>
        S2SAccessTokenCannotBeAcquired = -2147098045,

        /// <summary>
        /// Office Graph Integration relies on server-based SharePoint integration. To use this feature, enable server-based integration and have at least one active SharePoint site.
        /// </summary>
        S2SNotConfigured = -2147204519,

        /// <summary>
        /// The currency of the record does not match the currency of the price list.
        /// </summary>
        SalesOrderAndInvoiceCurrencyNotEqual = -2147185427,

        /// <summary>
        /// Fiscal calendar already exists for this salesperson/year
        /// </summary>
        SalesPeopleDuplicateCalendarNotAllowed = -2147207165,

        /// <summary>
        /// Fiscal calendar effective date cannot be empty
        /// </summary>
        SalesPeopleEmptyEffectiveDate = -2147207167,

        /// <summary>
        /// Parent salesperson cannot be empty
        /// </summary>
        SalesPeopleEmptySalesPerson = -2147207168,

        /// <summary>
        /// Territory manager cannot belong to other territory
        /// </summary>
        SalesPeopleManagerNotAllowed = -2147207163,

        /// <summary>
        /// The plug-in execution failed because the operation has timed-out at the Sandbox Client.
        /// </summary>
        SandboxClientPluginTimeout = -2147204751,

        /// <summary>
        /// The plug-in execution failed because no Sandbox Hosts are currently available. Please check that you have a Sandbox server configured and that it is running.
        /// </summary>
        SandboxHostNotAvailable = -2147204722,

        /// <summary>
        /// The plug-in execution failed because the operation has timed-out at the Sandbox Host.
        /// </summary>
        SandboxHostPluginTimeout = -2147204750,

        /// <summary>
        /// Sandbox Plug-in execution is disabled.
        /// </summary>
        SandboxPluginDisabled = -2146954987,

        /// <summary>
        /// The plug-in execution failed because the Sandbox Client encountered an error during initialization.
        /// </summary>
        SandboxSdkListenerStartFailed = -2147204748,

        /// <summary>
        /// The plug-in execution failed because no Sandbox Worker processes are currently available. Please try again.
        /// </summary>
        SandboxWorkerNotAvailable = -2147204723,

        /// <summary>
        /// Didn’t receive a response from the {0} plug-in within the 2:20-minute limit.
        /// </summary>
        SandboxWorkerPluginExecuteTimeout = -2146954991,

        /// <summary>
        /// The plug-in execution failed because the operation has timed-out at the Sandbox Worker.
        /// </summary>
        SandboxWorkerPluginTimeout = -2147204749,

        /// <summary>
        /// Maximum processes allocated for plug-in business logic exceeded. Fatal errors in plug-ins for this environment have occurred {0} times in the last {1} minutes. Each error requires an additional process to recover. Processes for plug-ins are being recycled. All plug-ins for this environment will fail during this period. More information: https://go.microsoft.com/fwlink/?linkid=2038718
        /// </summary>
        SandboxWorkerThrottleLimit = -2146954986,

        /// <summary>
        /// AllowSaveAsDraftAppointment is turned off.
        /// </summary>
        SaveAsDraftAppointmentNotAllowed = -2147220885,

        /// <summary>
        /// Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.
        /// </summary>
        SaveDataFileErrorOutOfSpace = -2147094007,

        /// <summary>
        /// The specified view is not customizable
        /// </summary>
        SavedQueryIsNotCustomizable = -2147192809,

        /// <summary>
        /// You can’t publish profile {0} because one of its profile items {1} has an entity {2} in the saved query {3}, which isn’t part of this profile.
        /// </summary>
        SavedQueryValidationError = -2147087968,

        /// <summary>
        /// Save operation is already running in the background.
        /// </summary>
        SavePending = -2147088109,

        /// <summary>
        /// After you select a price list, you must save the record before you can add a bundle with optional products.
        /// </summary>
        SaveRecordBeforeAddingBundle = -2147157629,

        /// <summary>
        /// The specified scalegroup is disabled. Access to organizations in this scalegroup are not allowed.
        /// </summary>
        ScaleGroupDisabled = -2147180281,

        /// <summary>
        /// Book or Reschedule operation failed due to booking validation.
        /// </summary>
        SchedulingFailedForBookingValidation = -2147088107,

        /// <summary>
        /// Book or Reschedule operation failed due to invalid data.
        /// </summary>
        SchedulingFailedForInvalidData = -2147088108,

        /// <summary>
        /// Identifier {0} for type {2} can only consist of alpha-numeric and underscore characters.
        /// </summary>
        SchemaNameContainsNonAlphaNumericCharacters = -2147204252,

        /// <summary>
        /// The schema name {0} for type {1} is not unique. An {0} with same name already exists.
        /// </summary>
        SchemaNameisNotUnique = -2147204253,

        /// <summary>
        /// Identifiers cannot be more than {1} characters long. The provided schema name {0} length for type {2} is greater than maxlength {1} characters.
        /// </summary>
        SchemaNameLengthExceedsLimit = -2147204249,

        /// <summary>
        /// Identifiers cannot match existing object names. An object of type {1} with same name {0} already exists.
        /// </summary>
        SchemaNameMatchesExistingIdentifier = -2147204255,

        /// <summary>
        /// Identifiers cannot match reserved SQL keywords. The name provided :{0} for type {1} matches KeyWord:{2}
        /// </summary>
        SchemaNameMatchesReservedSqlKeywords = -2147204254,

        /// <summary>
        /// Identifers should start with a letter. The schema name {0} for type {2} doesn't start with letter.
        /// </summary>
        SchemaNameNotStartwithLetter = -2147204251,

        /// <summary>
        /// Scope should be set to Global while creating business process flow category
        /// </summary>
        ScopeNotSetToGlobal = -2147089405,

        /// <summary>
        /// This workflow job was canceled because the workflow that started it included an infinite loop. Correct the workflow logic and try again. For information about workflow logic, see Help.
        /// </summary>
        SdkCorrelationTokenDepthTooHigh = -2147204734,

        /// <summary>
        /// Custom SdkMessageProcessingStep is not allowed on the specified message and entity.
        /// </summary>
        SdkCustomProcessingStepIsNotAllowed = -2147204729,

        /// <summary>
        /// The method being invoked does not support provided entity type.
        /// </summary>
        SdkEntityDoesNotSupportMessage = -2147219456,

        /// <summary>
        /// Entity '{0}' is not allowed in offline queue playback.
        /// </summary>
        SdkEntityOfflineQueuePlaybackIsNotAllowed = -2147204728,

        /// <summary>
        /// Message property name '{0}' is not valid on message {1}.
        /// </summary>
        SdkInvalidMessagePropertyName = -2147204757,

        /// <summary>
        /// Message '{0}' does not support image registration.
        /// </summary>
        SdkMessageDoesNotSupportImageRegistration = -2147204727,

        /// <summary>
        /// PreEvent step registration does not support Post Image.
        /// </summary>
        SdkMessageDoesNotSupportPostImageRegistration = -2147204754,

        /// <summary>
        /// Message {0} does not support this image type.
        /// </summary>
        SdkMessageInvalidImageTypeRegistration = -2147204755,

        /// <summary>
        /// Sdk message is not implemented.
        /// </summary>
        SdkMessageNotImplemented = -2147203036,

        /// <summary>
        /// The message requested is not supported on the client.
        /// </summary>
        SdkMessageNotSupportedOnClient = -2147204735,

        /// <summary>
        /// The message requested is not supported on the server.
        /// </summary>
        SdkMessageNotSupportedOnServer = -2147204736,

        /// <summary>
        /// This message is no longer available. Please consult the SDK for alternative messages.
        /// </summary>
        SdkMessagesDeprecatedError = -2147157757,

        /// <summary>
        /// Caller does not have enough privilege to set CallerOriginToken to the specified value.
        /// </summary>
        SdkNotEnoughPrivilegeToSetCallerOriginToken = -2147204343,

        /// <summary>
        /// Search Text Length Exceeded.
        /// </summary>
        SearchTextLenExceeded = -2147220993,

        /// <summary>
        /// Unable to copy the documents. The source file no longer exists.
        /// </summary>
        SelectedFileNotFound = -2147020763,

        /// <summary>
        /// Number of series for chart area and number of measure collections for category should be same.
        /// </summary>
        SeriesMeasureCollectionMismatch = -2147164157,

        /// <summary>
        /// The URL specified for Server Location uses HTTP but Secure Sockets Layer(SSL) is required for Exchange Online.
        /// </summary>
        ServerLocationAndSSLSetToYes = -2147098027,

        /// <summary>
        /// Server Location Field cannot be Empty
        /// </summary>
        ServerLocationIsEmpty = -2147098032,

        /// <summary>
        /// More no of service account mailboxes is associated to emailserver profile
        /// </summary>
        ServiceAccountMailboxesCountIsGreaterThanOne = -2147098038,

        /// <summary>
        /// No service account mailbox is associated for the email server profile.
        /// </summary>
        ServiceAccountMailboxesCountIsZero = -2147098039,

        /// <summary>
        /// Configuration of required credentials must be completed before messages can be sent.
        /// </summary>
        ServiceBusEndpointNotConfigured = -2146954990,

        /// <summary>
        /// Failed to retrieve the additional token for service bus post.
        /// </summary>
        ServiceBusExtendedTokenFailed = -2147204744,

        /// <summary>
        /// Service integration issuer certificate error.
        /// </summary>
        ServiceBusIssuerCertificateError = -2147204745,

        /// <summary>
        /// Cannot find service integration issuer information.
        /// </summary>
        ServiceBusIssuerNotFound = -2147204746,

        /// <summary>
        /// The service bus call failed because the request payload has exceeded maximum allowed size. Please reduce your request size and retry.
        /// </summary>
        ServiceBusMaxSizeExceeded = -2147155448,

        /// <summary>
        /// Service bus post is disabled for the organization.
        /// </summary>
        ServiceBusPostDisabled = -2147204742,

        /// <summary>
        /// The service bus post failed.
        /// </summary>
        ServiceBusPostFailed = -2147204747,

        /// <summary>
        /// Service bus post is being postponed.
        /// </summary>
        ServiceBusPostPostponed = -2147204743,

        /// <summary>
        /// Service Endpoint with ACS authentication type is no longer supported. Please change your endpoint configuration to use a supported authentication type
        /// </summary>
        ServiceEndpointAcsAuthNotSupported = -2147155447,

        /// <summary>
        /// Instantiation of an Entity failed.
        /// </summary>
        ServiceInstantiationFailed = -2147220924,

        /// <summary>
        /// Session token is not available unless there is a transaction in place.
        /// </summary>
        SessionTokenUnavailable = -2147220909,

        /// <summary>
        /// SetActiveProcess is not supported on new records.
        /// </summary>
        SetActiveNotSupportedOnNewRecords = -2147089548,

        /// <summary>
        /// Both absolute URL and relative URL cannot be null.
        /// </summary>
        SharePointAbsoluteAndRelativeUrlEmpty = -2147188407,

        /// <summary>
        /// Microsoft Dynamics 365 cannot authenticate this user {0} . Verify that the information for this user is correct, and then try again.
        /// </summary>
        SharePointAuthenticationFailure = -2147088205,

        /// <summary>
        /// Microsoft Dynamics 365 cannot authorize this user {0} . Verify that the information for this user is correct, and then try again.
        /// </summary>
        SharePointAuthorizationFailure = -2147088204,

        /// <summary>
        /// Certificate used for Sharepoint validation has expired
        /// </summary>
        SharePointCertificateExpired = -2147088207,

        /// <summary>
        /// Microsoft Dynamics 365 cannot connect this user {0} to SharePoint. Verify that the information for this user is correct and exists in SharePoint, and then try again.
        /// </summary>
        SharePointConnectionFailure = -2147088203,

        /// <summary>
        /// The SharePoint and Microsoft Dynamics 365 Servers are on different domains. Please ensure a trust relationship between the two domains.
        /// </summary>
        SharePointCrmDomainValidator = -2147159294,

        /// <summary>
        /// The Microsoft Dynamics 365 Grid component must be installed on the SharePoint server. This component is required for SharePoint integration to work correctly.
        /// </summary>
        SharePointCrmGridIsInstalledValidator = -2147159287,

        /// <summary>
        /// The URL exceeds the maximum number of 256 characters. Use shorter names for sites and folders, and try again.
        /// </summary>
        SharePointErrorAbsoluteUrlClipped = -2147159276,

        /// <summary>
        /// An error occurred while retrieving the absolute and site collection url for a SharePoint object.
        /// </summary>
        SharePointErrorRetrieveAbsoluteUrl = -2147159280,

        /// <summary>
        /// Entity Does not support SharePoint Url Validation.
        /// </summary>
        SharePointInvalidEntityForValidation = -2147159279,

        /// <summary>
        /// Sharepoint realm ID entered does not match with the registered realm at Sharepoint side.
        /// </summary>
        SharePointRealmMismatch = -2147088206,

        /// <summary>
        /// There is already a record with the same Url.
        /// </summary>
        SharePointRecordWithDuplicateUrl = -2147188649,

        /// <summary>
        /// A system job to provision the selected security role is pending. Any changes made to the security role record before this system job starts will be applied to this system job.
        /// </summary>
        SharePointRoleProvisionJobAlreadyExists = -2147159814,

        /// <summary>
        /// SharePoint server-based SharePoint integration not enabled.
        /// </summary>
        SharePointS2SIsDisabled = -2147020777,

        /// <summary>
        /// The URL is incorrect or the site is not running.
        /// </summary>
        SharePointServerDiscoveryValidator = -2147159293,

        /// <summary>
        /// Microsoft Dynamics 365 and Microsoft Office SharePoint Server must have the same base language.
        /// </summary>
        SharePointServerLanguageValidator = -2147159288,

        /// <summary>
        /// The SharePoint Site Collection must be running a supported version of Microsoft Office SharePoint Server or Microsoft Windows SharePoint Services. Please refer the implementation guide.
        /// </summary>
        SharePointServerVersionValidator = -2147159292,

        /// <summary>
        /// The URL is incorrect or the site is not running.
        /// </summary>
        SharePointSiteCollectionIsAccessibleValidator = -2147159291,

        /// <summary>
        /// Failed to create the site {0} in SharePoint.
        /// </summary>
        SharePointSiteCreationFailure = -2147159816,

        /// <summary>
        /// SharePointSite is not configured, it need to be configured.
        /// </summary>
        SharePointSiteNotConfigured = -2147020780,

        /// <summary>
        /// Site {0} does not exists in SharePoint.
        /// </summary>
        SharePointSiteNotPresentInSharePoint = -2147159821,

        /// <summary>
        /// The current user does not have the appropriate privileges. You must be a SharePoint site administrator on the SharePoint site.
        /// </summary>
        SharePointSitePermissionsValidator = -2147159289,

        /// <summary>
        /// SharePoint provisioning job has failed.
        /// </summary>
        SharePointSiteWideProvisioningJobFailed = -2147159813,

        /// <summary>
        /// A system job to provision the selected team is pending. Any changes made to the team record before this system job starts will be applied to this system job.
        /// </summary>
        SharePointTeamProvisionJobAlreadyExists = -2147159815,

        /// <summary>
        /// Unable to ACL site {0} in SharePoint.
        /// </summary>
        SharePointUnableToAclSite = -2147159818,

        /// <summary>
        /// Unable to ACL site {0} with privilege {1} in SharePoint.
        /// </summary>
        SharePointUnableToAclSiteWithPrivilege = -2147159819,

        /// <summary>
        /// Microsoft Dynamics 365 cannot add this user {0} to the group {1} in SharePoint. Verify that the information for this user and group are correct and that the group exists in SharePoint, and then try again.
        /// </summary>
        SharePointUnableToAddUserToGroup = -2147159823,

        /// <summary>
        /// Unable to create site group {0} in SharePoint.
        /// </summary>
        SharePointUnableToCreateSiteGroup = -2147159817,

        /// <summary>
        /// Unable to remove user {0} from group {1} in SharePoint.
        /// </summary>
        SharePointUnableToRemoveUserFromGroup = -2147159822,

        /// <summary>
        /// Unable to retrieve the group {0} from SharePoint.
        /// </summary>
        SharePointUnableToRetrieveGroup = -2147159820,

        /// <summary>
        /// The URL cannot be resolved into an IP.
        /// </summary>
        SharePointUrlHostValidator = -2147159295,

        /// <summary>
        /// The URL is not valid. The URL must be a valid site collection and cannot include a subsite. The URL must be in a valid form, such as http://SharePointServer/sites/CrmSite.
        /// </summary>
        SharePointUrlIsRootWebValidator = -2147159290,

        /// <summary>
        /// Microsoft Dynamics 365 cannot connect to Sharepoint as the Sharepoint Version is unsupported. Install the correct version, and then try again.
        /// </summary>
        SharePointVersionUnsupported = -2147088202,

        /// <summary>
        /// No similarity rule active for this entity.
        /// </summary>
        SimilarityRuleDisabled = -2147020778,

        /// <summary>
        /// Similarity rules not enabled.
        /// </summary>
        SimilarityRuleFCBOff = -2147020776,

        /// <summary>
        /// A single transformation parameter mapping is defined for an array parameter.
        /// </summary>
        SingletonMappingFoundForArrayParameter = -2147220610,

        /// <summary>
        /// You don’t have permissions for these records or something may be wrong with the site map. Contact your system administrator.If you are the administrator, you can go to the solutions page and import a different solution.
        /// </summary>
        SiteMapMissing = -2147155946,

        /// <summary>
        /// Sitemap xml failed XSD validation with the following error: '{0}' at line {1} position {2}.
        /// </summary>
        SiteMapXsdValidationError = -2147159039,

        /// <summary>
        /// The behavior value for this field was ignored. A System Customizer will need to configure the behavior value for this field directly.
        /// </summary>
        SkipValidDateTimeBehavior = -2147088221,

        /// <summary>
        /// This SLA cannot be activated or deactivated by someone who is not its owner.
        /// </summary>
        SlaActivateDeactivateByNonOwner = -2147157902,

        /// <summary>
        /// You can't activate this record because it's already active.
        /// </summary>
        SlaAlreadyInactiveState = -2147157919,

        /// <summary>
        /// You can't deactivate this record because it's in a draft state.
        /// </summary>
        SlaAlreadyInDraftState = -2147157918,

        /// <summary>
        /// SLA is not enabled for this entity.
        /// </summary>
        SlaNotEnabledEntity = -2147135485,

        /// <summary>
        /// You don't have the required permissions on SLAs and processes to perform this action.
        /// </summary>
        SlaPermissionToPerformAction = -2147157899,

        /// <summary>
        /// The selected report is not ready for viewing. The report is still being created or a report snapshot is not available. ReportId:{0}
        /// </summary>
        SnapshotReportNotReady = -2147220343,

        /// <summary>
        /// There's a problem communicating with the Dynamics 365 Organization. The organization might be unavailable or the feature is set so that it can't receive social data. Try again later. If the problem persists, contact your Microsoft Dynamics 365 administrator.
        /// </summary>
        SocialCareDisabledError = -2147088863,

        /// <summary>
        /// The solution installation or removal failed due to the installation or removal of another solution at the same time. Please try again later.
        /// </summary>
        SolutionConcurrencyFailure = -2147020463,

        /// <summary>
        /// The solution configuration page must exist within the solution it represents.
        /// </summary>
        SolutionConfigurationPageMustBeHtmlWebResource = -2147192804,

        /// <summary>
        /// The operation timed out. This may be because a solution is currently being imported into this environment. Please try again after the solution import is completed. Solutions should be imported outside of working hours if possible.
        /// </summary>
        SolutionImportCauseTimeout = -2147187389,

        /// <summary>
        /// Component cannot be created because it already has solution-aware columns. Entity: {0}, Existing Attribute: {1}
        /// </summary>
        SolutionRestrictedAttributes = -2147016701,

        /// <summary>
        /// The solution unique name '{0}' is already being used and cannot be used again.
        /// </summary>
        SolutionUniqueNameViolation = -2147160029,

        /// <summary>
        /// Solution Upgrade action failed after import as holding. InnerException is: {1}.
        /// </summary>
        SolutionUpgradeFailed = -2147159994,

        /// <summary>
        /// "The {0} solution doesn’t have an upgrade that is ready to be applied."
        /// </summary>
        SolutionUpgradeNotAvailable = -2147187397,

        /// <summary>
        /// "To use this action, you must first select the old solution and then try again."
        /// </summary>
        SolutionUpgradeWrongSolutionSelected = -2147187396,

        /// <summary>
        /// Column headers must be 160 or fewer characters. Fix the column headers, and then run Data Migration Manager again.
        /// </summary>
        SourceAttributeHeaderTooBig = -2147204288,

        /// <summary>
        /// This source entity is mapped to more than one Microsoft Dynamics 365 entity. Remove any duplicate mappings, and then import this data map again.
        /// </summary>
        SourceEntityMappedToMultipleTargets = -2147220675,

        /// <summary>
        /// Exception occured while fetching account name from Sharepoint.
        /// </summary>
        SPAccountNameFetchFailure = -2147088598,

        /// <summary>
        /// One or more sites in all files view of SharePointDocument failed.
        /// </summary>
        SPAllFilesErrorScenario = -2147088544,

        /// <summary>
        /// The file in the collection has bad lock
        /// </summary>
        SPBadLockInFileCollectionErrorCode = -2147088630,

        /// <summary>
        /// S2STokenIssuer certificate not found.
        /// </summary>
        SPCertificationError = -2147088537,

        /// <summary>
        /// Failed to connect to SharePointSite.
        /// </summary>
        SPConnectionFailure = -2147088543,

        /// <summary>
        /// Current document location is disabled by administrator
        /// </summary>
        SPCurrentDocumentLocationDisabledErrorCode = -2147088608,

        /// <summary>
        /// Record already present in db
        /// </summary>
        SPCurrentFolderAlreadyExistErrorCode = -2147088607,

        /// <summary>
        /// Data validation has failed on the field and the list
        /// </summary>
        SPDataValidationFiledOnFieldAndListErrorCode = -2147088621,

        /// <summary>
        /// Data validation has failed on the field
        /// </summary>
        SPDataValidationFiledOnFieldErrorCode = -2147088623,

        /// <summary>
        /// Data validation has failed on the list
        /// </summary>
        SPDataValidationFiledOnListErrorCode = -2147088622,

        /// <summary>
        /// OneDrive activation needs a default SharePoint site.
        /// </summary>
        SPDefaultSiteNotPresent = -2147088583,

        /// <summary>
        /// Can't map documents to their location.
        /// </summary>
        SPDocumentMappingFailure = -2147088588,

        /// <summary>
        /// The list item could not be updated because duplicate values were found for one or more field(s) in the list
        /// </summary>
        SPDuplicateValuesFoundErrorCode = -2147088632,

        /// <summary>
        /// Specify password for Incoming Connection
        /// </summary>
        SpecifyIncomingPassword = -2147098029,

        /// <summary>
        /// Specify Incomming Port and save again
        /// </summary>
        SpecifyInComingPort = -2147098033,

        /// <summary>
        /// Specify the URL of the incoming server location
        /// </summary>
        SpecifyIncomingServerLocation = -2147098037,

        /// <summary>
        /// Specify username for Incoming Connection
        /// </summary>
        SpecifyIncomingUserName = -2147098031,

        /// <summary>
        /// Specify password for Outgoing Connection
        /// </summary>
        SpecifyOutgoingPassword = -2147098028,

        /// <summary>
        /// Specify Outgoing Port and save again
        /// </summary>
        SpecifyOutgoingPort = -2147098026,

        /// <summary>
        /// Specify the URL of the outgoing server location
        /// </summary>
        SpecifyOutgoingServerLocation = -2147098036,

        /// <summary>
        /// Specify username for Outgoing Connection
        /// </summary>
        SpecifyOutgoingUserName = -2147098030,

        /// <summary>
        /// Exclusive lock on the file
        /// </summary>
        SPExclusiveLockOnFileErrorCode = -2147088635,

        /// <summary>
        /// File is already checked out
        /// </summary>
        SPFileAlreadyCheckedOutErrorCode = -2147088638,

        /// <summary>
        /// Checkout arguments are not valid
        /// </summary>
        SPFileCheckedOutInvalidArgsErrorCode = -2147088637,

        /// <summary>
        /// Content of the file creation information must not be null
        /// </summary>
        SPFileContentNullErrorCode = -2147088627,

        /// <summary>
        /// File is checked out to a user other than the current user
        /// </summary>
        SPFileIsCheckedOutByOtherUser = -2147088600,

        /// <summary>
        /// Field is read-only
        /// </summary>
        SPFileIsReadOnlyErrorCode = -2147088625,

        /// <summary>
        /// The folder can't be found. If you changed the automatically generated folder name for this document location directly in SharePoint, you must change the folder name in Dynamics 365 to match the renamed folder. To do this, select Edit Location and type the matching name in Folder Name field.
        /// </summary>
        SPFileNameModifiedErrorCode = -2147088599,

        /// <summary>
        /// File is not checked out
        /// </summary>
        SPFileNotCheckedOutErrorCode = -2147088640,

        /// <summary>
        /// File cannot be found
        /// </summary>
        SPFileNotFoundErrorCode = -2147088634,

        /// <summary>
        /// The file in the collection is not locked
        /// </summary>
        SPFileNotLockedErrorCode = -2147088633,

        /// <summary>
        /// There is a mismatch between the size of the document stream written and the size of the input document stream
        /// </summary>
        SPFileSizeMismatchErrorCode = -2147088626,

        /// <summary>
        /// Virus checking indicates the file is infected with a virus or the file is too large
        /// </summary>
        SPFileTooLargeOrInfectedErrorCode = -2147088631,

        /// <summary>
        /// Cannot Create Folder with this name
        /// </summary>
        SPFolderCreationFailure = -2147159824,

        /// <summary>
        /// Folder Not Found
        /// </summary>
        SPFolderNotFoundErrorCode = -2147088613,

        /// <summary>
        /// Exception occurred while Editing Sharepoint Document Proeprties.
        /// </summary>
        SPFolderRenameFailure = -2147088596,

        /// <summary>
        /// Error while doing this operation on SharePoint
        /// </summary>
        SPGenericErrorCode = -2147088615,

        /// <summary>
        /// Illegal characters in filename
        /// </summary>
        SPIllegalCharactersInFileNameErrorCode = -2147088609,

        /// <summary>
        /// Illegal file type
        /// </summary>
        SPIllegalFileTypeErrorCode = -2147088611,

        /// <summary>
        /// List item is an instance of a recurring event which is not a recurrence exception, the list item is a workflow task whose parent workflow is in the recycle bin, or the parent list is a document library
        /// </summary>
        SPInstanceOfRecurringEventErrorCode = -2147088618,

        /// <summary>
        /// Intermittent error occured. Please refresh the grid and try again
        /// </summary>
        SPIntermittentError = -2147088540,

        /// <summary>
        /// Invalid Sharepoint Document Location type
        /// </summary>
        SPInvalidDocumentLocation = -2147088542,

        /// <summary>
        /// Invalid Field Value
        /// </summary>
        SPInvalidFieldValueErrorCode = -2147088610,

        /// <summary>
        /// List item could not be updated because invalid lookup values were found for one or more field(s) in the list
        /// </summary>
        SPInvalidLookupValuesErrorCode = -2147088629,

        /// <summary>
        /// Error while doing this operation on SharePoint
        /// </summary>
        SPInvalidSavedQueryErrorCode = -2147088616,

        /// <summary>
        /// SubSite url is incorrectly formed
        /// </summary>
        SPInvalidSubSite = -2147088541,

        /// <summary>
        /// List item does not exist
        /// </summary>
        SPItemNotExistErrorCode = -2147088617,

        /// <summary>
        /// Relative URL should not have preceding or trailing spaces.
        /// </summary>
        SPMalFormedRelativeUrl = -2147088538,

        /// <summary>
        /// List item was modified on the server so changes cannot be committed
        /// </summary>
        SPModifiedOnServerErrorCode = -2147088624,

        /// <summary>
        /// More than one site with One Drive enabled is not allowed.
        /// </summary>
        SPMultipleOdbSitesError = -2147088595,

        /// <summary>
        /// No Active Document Location
        /// </summary>
        SPNoActiveDocumentLocationErrorCode = -2147088612,

        /// <summary>
        /// Only crm admin who is tenant admin can perform create operation
        /// </summary>
        SPNotAdminError = -2147088539,

        /// <summary>
        /// SharePoint integration is not enabled on Entity
        /// </summary>
        SPNotEnabledError = -2147088582,

        /// <summary>
        /// SharePoint Integration and Microsoft Teams Integration is not enabled on Entity
        /// </summary>
        SPNotEnabledForEntity = -2147088581,

        /// <summary>
        /// URL of the file creation information must not be null and URL of the file creation information must not be invalid
        /// </summary>
        SPNullFileUrlErrorCode = -2147088628,

        /// <summary>
        /// Regarding object id is null
        /// </summary>
        SPNullRegardingObjectErrorCode = -2147088605,

        /// <summary>
        /// Please enable ODB(One Drive for Business) feature to create ODB site.
        /// </summary>
        SPOdbDisabledError = -2147088594,

        /// <summary>
        /// More than one ODB (OneDrive for Business) location is not allowed.
        /// </summary>
        SPOdbDuplicateLocationError = -2147088587,

        /// <summary>
        /// You cannot update or delete ODB(One Drive for Business) site.
        /// </summary>
        SPOdbUpdateDeleteError = -2147088593,

        /// <summary>
        /// You cannot update or delete SharePoint Document Location of type ODB (OneDrive for Business).
        /// </summary>
        SPOdbUpdateDeleteLocationError = -2147088586,

        /// <summary>
        /// List does not support this operation
        /// </summary>
        SPOperationNotSupportedErrorCode = -2147088619,

        /// <summary>
        /// {0} does not support the selected operator
        /// </summary>
        SPOperatorNotSupportedErrorCode = -2147088604,

        /// <summary>
        /// Personal Site not found for the user.
        /// </summary>
        SPPersonalSiteNotFound = -2147088597,

        /// <summary>
        /// Exception occurred while doing document check-in as some columns are made required at SharePoint
        /// </summary>
        SPRequiredColCheckInErrorCode = -2147088603,

        /// <summary>
        /// OneDrive location is not created yet. Please create the location before searching.
        /// </summary>
        SPSearchOneDriveNotCreated = -2147088585,

        /// <summary>
        /// Shared lock on the file
        /// </summary>
        SPSharedLockOnFileErrorCode = -2147088636,

        /// <summary>
        /// Site Not Found
        /// </summary>
        SPSiteNotFoundErrorCode = -2147088614,

        /// <summary>
        /// Protocol error in accessing SharePoint
        /// </summary>
        SPSiteProtocolError = -2147088584,

        /// <summary>
        /// Throttling limit is exceeded by the operation
        /// </summary>
        SPThrottlingLimitExceededErrorCode = -2147088620,

        /// <summary>
        /// Current user have insufficient privileges
        /// </summary>
        SPUnauthorizedAccessErrorCode = -2147088639,

        /// <summary>
        /// Upload failed on SharePoint due to unknown reasons. Probably the file is too large
        /// </summary>
        SPUploadFailure = -2147088576,

        /// <summary>
        /// A SQL arithmetic overflow error occurred
        /// </summary>
        SqlArithmeticOverflowError = -2147217098,

        /// <summary>
        /// There was an error in Data Encryption.
        /// </summary>
        SqlEncryption = -2147187432,

        /// <summary>
        /// Cannot open Symmetric Key because Database Master Key does not exist in the database or is not opened.
        /// </summary>
        SqlEncryptionCannotOpenSymmetricKeyBecauseDatabaseMasterKeyDoesNotExistOrIsNotOpened = -2147187430,

        /// <summary>
        /// Certificate with Name='{0}' does not exist in the database.
        /// </summary>
        SqlEncryptionCertificateDoesNotExist = -2147187427,

        /// <summary>
        /// 'Change' encryption key has already been executed {0} times in the last {1} minutes. Please wait a couple of minutes and then try again.
        /// </summary>
        SqlEncryptionChangeEncryptionKeyExceededQuotaForTheInterval = -2147187415,

        /// <summary>
        /// Cannot create Certificate with Name='{0}' because it already exists.
        /// </summary>
        SqlEncryptionCreateCertificateError = -2147187426,

        /// <summary>
        /// Cannot create Database Master Key because already exists.
        /// </summary>
        SqlEncryptionCreateDatabaseMasterKeyError = -2147187429,

        /// <summary>
        /// Cannot create Symmetric Key with Name='{0}' because it already exists.
        /// </summary>
        SqlEncryptionCreateSymmetricKeyError = -2147187423,

        /// <summary>
        /// Database Master Key does not exist in the database.
        /// </summary>
        SqlEncryptionDatabaseMasterKeyDoesNotExist = -2147187431,

        /// <summary>
        /// Cannot delete Certificate with Name='{0}' because it does not exist.
        /// </summary>
        SqlEncryptionDeleteCertificateError = -2147187425,

        /// <summary>
        /// Cannot delete Database Master Key because it does not exist.
        /// </summary>
        SqlEncryptionDeleteDatabaseMasterKeyError = -2147187428,

        /// <summary>
        /// Cannot delete the encryption key.
        /// </summary>
        SqlEncryptionDeleteEncryptionKeyError = -2147187418,

        /// <summary>
        /// Cannot delete Symmetric Key with Name='{0}' because it does not exist.
        /// </summary>
        SqlEncryptionDeleteSymmetricKeyError = -2147187422,

        /// <summary>
        /// Error while testing data encryption and decryption.
        /// </summary>
        SqlEncryptionEncryptionDecryptionTestError = -2147187421,

        /// <summary>
        /// The new encryption key does not meet the strong encryption key requirements. The key must be between 10 and 100 characters in length, and must have at least one numeral, at least one letter, and one symbol or special character. {0}
        /// </summary>
        SqlEncryptionEncryptionKeyValidationError = -2147187416,

        /// <summary>
        /// Cannot perform 'activate' encryption key because the encryption key is already set and is working. Use 'change' encryption key instead.
        /// </summary>
        SqlEncryptionIsActiveCannotRestoreEncryptionKey = -2147187419,

        /// <summary>
        /// Cannot perform 'change' encryption key because the encryption key is not already set or is not working. First use 'activate' encryption key instead to set the correct current encryption key and then use 'change' encryption if you want to re-encrypt data using a new encryption key.
        /// </summary>
        SqlEncryptionIsInactiveCannotChangeEncryptionKey = -2147187417,

        /// <summary>
        /// Cannot decrypt existing encrypted data (Entity='{0}', Attribute='{1}') using the current encryption key. Use 'activate' encryption key to set the correct encryption key.
        /// </summary>
        SqlEncryptionKeyCannotDecryptExistingData = -2147187420,

        /// <summary>
        /// Cannot perform 'activate' because the encryption key doesn’t match the original encryption key that was used to encrypt the data.
        /// </summary>
        SqlEncryptionRestoreEncryptionKeyCannotDecryptExistingData = -2147187413,

        /// <summary>
        /// The system is currently running a request to 'change' or 'activate' the encryption key. Please wait before making another request.
        /// </summary>
        SqlEncryptionSetEncryptionKeyIsAlreadyRunningCannotRunItInParallel = -2147187414,

        /// <summary>
        /// Cannot open encryption Symmetric Key because the password is wrong.
        /// </summary>
        SqlEncryptionSymmetricKeyCannotOpenBecauseWrongPassword = -2147187408,

        /// <summary>
        /// Symmetric Key with Name='{0}' does not exist in the database.
        /// </summary>
        SqlEncryptionSymmetricKeyDoesNotExist = -2147187424,

        /// <summary>
        /// Cannot open encryption Symmetric Key because it does not exist in the database or user does not have permission.
        /// </summary>
        SqlEncryptionSymmetricKeyDoesNotExistOrNoPermission = -2147187409,

        /// <summary>
        /// Encryption Symmetric Key password does not exist in Config DB.
        /// </summary>
        SqlEncryptionSymmetricKeyPasswordDoesNotExistInConfigDB = -2147187410,

        /// <summary>
        /// Encryption Symmetric Key Source does not exist in Config DB.
        /// </summary>
        SqlEncryptionSymmetricKeySourceDoesNotExistInConfigDB = -2147187411,

        /// <summary>
        /// SQL error {0} occurred in stored procedure {1}
        /// </summary>
        SqlErrorInStoredProcedure = -2147172351,

        /// <summary>
        /// The maximum recursion has reached before statement completion.
        /// </summary>
        SqlMaxRecursionExceeded = -2147204777,

        /// <summary>
        /// MSCRM Data Connector Not Installed
        /// </summary>
        SrsDataConnectorNotInstalled = -2147220334,

        /// <summary>
        /// This report can’t upload because Dynamics 365 Reporting Extensions, required components for reporting, are not installed on the server that is running Microsoft SQL Server Reporting Services.
        /// </summary>
        SrsDataConnectorNotInstalledUpload = -2147188078,

        /// <summary>
        /// Please re-login to refresh your session.
        /// </summary>
        SSM_MaxPCI_Exceeded = -2147015312,

        /// <summary>
        /// Failed to refresh login session.
        /// </summary>
        SSM_RefreshToken_Failed = -2147015311,

        /// <summary>
        /// Validation error: stage entity cannot be null.
        /// </summary>
        StageEntityIsNull = -2147089327,

        /// <summary>
        /// Validation error: Stage ID cannot be empty.
        /// </summary>
        StageIdIsEmpty = -2147089324,

        /// <summary>
        /// Validation error: Stage ID ‘{0}’ is not present in Business Process. Please contact your system administrator.
        /// </summary>
        StageIdIsNotPresentInBusinessProcess = -2147089328,

        /// <summary>
        /// Validation error: Stage ID is not valid for Business Process.
        /// </summary>
        StageIdIsNotValid = -2147089320,

        /// <summary>
        /// You can't activate this record because of the status transition rules.Contact your system administrator.
        /// </summary>
        StateTransitionActivateNewStatus = -2147157929,

        /// <summary>
        /// Because of the status transition rules, you can't cancel the case in the current status.Change the case status, and then try canceling it, or contact your system administrator.
        /// </summary>
        StateTransitionActiveToCanceled = -2147157931,

        /// <summary>
        /// Because of the status transition rules, you can't resolve a case in the current status.Change the case status, and then try resolving it, or contact your system administrator.
        /// </summary>
        StateTransitionActiveToResolve = -2147157932,

        /// <summary>
        /// You can't deactivate this record because of the status transition rules.Contact your system administrator.
        /// </summary>
        StateTransitionDeactivateNewStatus = -2147157928,

        /// <summary>
        /// Because of the status transition rules, you can't activate the case from the current status.Contact your system administrator.
        /// </summary>
        StateTransitionResolvedOrCanceledToActive = -2147157930,

        /// <summary>
        /// The original sdkmessageprocessingstep has been disabled and replaced.
        /// </summary>
        StepAutomaticallyDisabled = -2147200957,

        /// <summary>
        /// There are {0} {1} in the Xaml. Max allowed is {2}.
        /// </summary>
        StepCountInXamlExceedsMaxAllowed = -2147089388,

        /// <summary>
        /// {0} does not have at least one {1} as its child.
        /// </summary>
        StepDoesNotHaveAnyChildInXaml = -2147089386,

        /// <summary>
        /// Step {0} is not supported for client side business rule.
        /// </summary>
        StepNotSupportedForClientBusinessRule = -2147090432,

        /// <summary>
        /// StepStep does not have any ControlStep as its children
        /// </summary>
        StepStepDoesNotHaveAnyControlStepAsItsChildren = -2147089399,

        /// <summary>
        /// Dynamics 365 error {0} in {1}:{2}
        /// </summary>
        StoredProcedureContext = -2147172350,

        /// <summary>
        /// One of the attributes of the selected entity is a part of database index and so it cannot be greater than 900 bytes.
        /// </summary>
        StringAttributeIndexError = -2147167598,

        /// <summary>
        /// A validation error occurred. A string value provided is too long.
        /// </summary>
        StringLengthTooLong = -2147204303,

        /// <summary>
        /// Subcomponent {0} of type {1} is not found in the organization, it can not be added to the SolutionComponents.
        /// </summary>
        SubcomponentDoesNotExist = -2147187401,

        /// <summary>
        /// Subcomponent {0} cannot be added to the solution because the root component {1} is missing.
        /// </summary>
        SubcomponentMissingARoot = -2147187402,

        /// <summary>
        /// Subject does not exist.
        /// </summary>
        SubjectDoesNotExist = -2147205630,

        /// <summary>
        /// Creating this parental association would create a loop in Subjects hierarchy.
        /// </summary>
        SubjectLoopBeingCreated = -2147205631,

        /// <summary>
        /// Loop exists in the subjects hierarchy.
        /// </summary>
        SubjectLoopExists = -2147205632,

        /// <summary>
        /// Subreport does not exist. ReportId:{0}
        /// </summary>
        SubReportDoesNotExist = -2147220333,

        /// <summary>
        /// Subscription expired
        /// </summary>
        SubscriptionGone = -2147015405,

        /// <summary>
        /// Support login is expired
        /// </summary>
        SupportLogOnExpired = -2147180280,

        /// <summary>
        /// The support user cannot be updated
        /// </summary>
        SupportUserCannotBeCreateNorUpdated = -2147214015,

        /// <summary>
        /// The sync attribute mapping cannot be updated.
        /// </summary>
        SyncAttributeMappingCannotBeUpdated = -2147088575,

        /// <summary>
        /// Failed to start or connect to the offline mode MSDE database.
        /// </summary>
        SyncToMsdeFailure = -2147187705,

        /// <summary>
        /// SystemAttributeMap Error Occurred
        /// </summary>
        SystemAttributeMap = -2147196411,

        /// <summary>
        /// SystemEntityMap Error Occurred
        /// </summary>
        SystemEntityMap = -2147196414,

        /// <summary>
        /// The entity for the Target and the SourceId must match.
        /// </summary>
        SystemFormCopyUnmatchedEntity = -2147158442,

        /// <summary>
        /// The form type of the SourceId is not valid for the Target entity.
        /// </summary>
        SystemFormCopyUnmatchedFormType = -2147158441,

        /// <summary>
        /// The label '{0}', id: '{1}' already exists. Supply unique labelid values.
        /// </summary>
        SystemFormCreateWithExistingLabel = -2147158440,

        /// <summary>
        /// The unmanaged solution you are importing has displaycondition XML attributes that refer to security roles that are missing from the target system. Any displaycondition attributes that refer to these security roles will be removed.
        /// </summary>
        SystemFormImportMissingRoles = -2147158443,

        /// <summary>
        /// The system user was disabled therefore the ticket expired.
        /// </summary>
        SystemUserDisabled = -2147180270,

        /// <summary>
        /// The ticket specified for authentication has been tampered with or invalidated.
        /// </summary>
        TamperedAuthTicket = -2147180283,

        /// <summary>
        /// Target attribute name should be empty when the processcode is ignore.
        /// </summary>
        TargetAttributeInvalidForIgnore = -2147187456,

        /// <summary>
        /// This attribute is not valid for mapping.
        /// </summary>
        TargetAttributeInvalidForMap = -2147220588,

        /// <summary>
        /// The file specifies an attribute that does not exist in Microsoft Dynamics 365.
        /// </summary>
        TargetAttributeNotFound = -2147220590,

        /// <summary>
        /// The file specifies an entity that is not valid for data migration.
        /// </summary>
        TargetEntityInvalidForMap = -2147220587,

        /// <summary>
        /// The file specifies an entity that does not exist in Microsoft Dynamics 365.
        /// </summary>
        TargetEntityNotFound = -2147220591,

        /// <summary>
        /// Target Entity Name not defined for source:{0} file.
        /// </summary>
        TargetEntityNotMapped = -2147187616,

        /// <summary>
        /// The user can't be added to the team because the user doesn't have the "{0}" privilege.
        /// </summary>
        TargetUserInsufficientPrivileges = -2147187902,

        /// <summary>
        /// The name field cannot be empty. Please enter a name.
        /// </summary>
        TaskFlowEmptyName = -2147084526,

        /// <summary>
        /// Invalid attribute type: {0}.{1}.
        /// </summary>
        TaskFlowEntityAttributeIsNotValid = -2147084521,

        /// <summary>
        /// Invalid relationship type: {0}.
        /// </summary>
        TaskFlowEntityRelationshipIsNotValid = -2147084522,

        /// <summary>
        /// Could not find the system form {0} for Task flow {1}.
        /// </summary>
        TaskFlowFormXmlNotFound = -2147084525,

        /// <summary>
        /// The name field can only contain alphanumeric characters.
        /// </summary>
        TaskFlowInvalidCharactersInName = -2147084527,

        /// <summary>
        /// The task flow has exceeded the maximum number of controls allowed ({0}). To continue, you need to remove some controls.
        /// </summary>
        TaskFlowMaxNumberControls = -2147084519,

        /// <summary>
        /// The task flow has exceeded the maximum number of pages allowed ({0}). To continue, you need to remove some pages.
        /// </summary>
        TaskFlowMaxNumberPages = -2147084520,

        /// <summary>
        /// A task flow with the specified name already exists.  Please specify a unique name.
        /// </summary>
        TaskFlowNameIsNotUnique = -2147084528,

        /// <summary>
        /// A Task Flow which is trying to launch is not available on this device. You may not have permission to access it or it may not be available on your organization. Please contact your system administrator.
        /// </summary>
        TaskFlowNotFound = -2147084512,

        /// <summary>
        /// Task flow definition is invalid.
        /// </summary>
        TaskFlowNotValid = -2147084497,

        /// <summary>
        /// Could not find the pages {0} for Task flow {1} Step {2}.
        /// </summary>
        TaskFlowPageMissingFormXmlTab = -2147084524,

        /// <summary>
        /// The following entities are not enabled for Task flows: {0}.
        /// </summary>
        TaskFlowUnsupportedEntities = -2147084523,

        /// <summary>
        /// The team administrator does not have privilege read team.
        /// </summary>
        TeamAdministratorMissedPrivilege = -2147214070,

        /// <summary>
        /// The team with id={0} belongs to a different business unit={1} than the role with roleId={2} and roleBusinessUnit={3}.
        /// </summary>
        TeamInWrongBusiness = -2147216371,

        /// <summary>
        /// The specified name for the team is too long.
        /// </summary>
        TeamNameTooLong = -2147187963,

        /// <summary>
        /// The team has not been assigned any roles.
        /// </summary>
        TeamNotAssignedRoles = -2147209462,

        /// <summary>
        /// Creating Templates with Internet Marketing Campaign Activities is not allowed
        /// </summary>
        TemplateNotAllowedForInternetMarketing = -2147187595,

        /// <summary>
        /// This template type is not supported for unsubscribe acknowledgement.
        /// </summary>
        TemplateTypeNotSupportedForUnsubscribeAcknowledgement = -2147220700,

        /// <summary>
        /// Exchange Online Tenant ID field cannot be empty.
        /// </summary>
        TenantIDIsEmpty = -2147098022,

        /// <summary>
        /// The detected tenantId for your exchange is different than the once you saved.
        /// </summary>
        TenantIDValueChanged = -2147098020,

        /// <summary>
        /// Test email configuration scheduled is in progress. Please save after completion of test.
        /// </summary>
        TestEmailConfigurationScheduledInProgress = -2147098040,

        /// <summary>
        /// Active configuration does not exist for entity.
        /// </summary>
        TextAnalyticsAPIActiveConfigurationDoesNotExist = -2147084656,

        /// <summary>
        /// No active similarity rule exists. The system administrator must set up a similarity rule configuration.
        /// </summary>
        TextAnalyticsAPIActiveSimilarityConfigurationDoesNotExist = -2147084651,

        /// <summary>
        /// Text Analytics feature is available for organizations with base language as English.
        /// </summary>
        TextAnalyticsAPIAllowedOnlyForEnglishLanguage = -2147084655,

        /// <summary>
        /// Dynamics 365 failed to connect with the Azure text analytics service. Verify that the service URI and account key are valid, and the Azure subscription is active.
        /// </summary>
        TextAnalyticsAPIAzureUnableToConnectWithBuild = -2147084654,

        /// <summary>
        /// One or more text analytics models couldn't be activated. Try activating the existing text analytics models separately from the Azure service connection.
        /// </summary>
        TextAnalyticsAzureConnectionCascadeActivateFailed = -2147084748,

        /// <summary>
        /// Unable to connect to Text Analytics API.
        /// </summary>
        TextAnalyticsAzureConnectionFailed = -2147084720,

        /// <summary>
        /// Dynamics 365 failed to connect with the Azure text analytics service. Please try again and if the problem persists contact your system administrator.
        /// </summary>
        TextAnalyticsAzureSchedulerError = -2147084653,

        /// <summary>
        /// Failed to connect to the Azure Text Analytics service. Check that the service URL and the Azure account key are valid and the service subscription is active.
        /// </summary>
        TextAnalyticsAzureTestConnectionFailed = -2147084750,

        /// <summary>
        /// Dynamics 365 failed to connect with the Azure text analytics service. Verify that the service URI and account key are valid, and the Azure subscription is active.
        /// </summary>
        TextAnalyticsAzureUnableToConnectWithBuild = -2147084715,

        /// <summary>
        /// The Azure Text Analytics feature isn’t activated. The system administrator must activate this feature and set up the required configuration.
        /// </summary>
        TextAnalyticsFeatureNotEnabled = -2147084718,

        /// <summary>
        /// This text analytics entity mapping is used for an active configuration. It can’t be modified or deleted while it is used by an active config.
        /// </summary>
        TextAnalyticsMappingUsedForActiveConfiguration = -2147084697,

        /// <summary>
        /// Maximum number of topic models allowed for your organization has been reached.
        /// </summary>
        TextAnalyticsMaxLimitForTopicModelReached = -2147084652,

        /// <summary>
        /// The Azure Machine Learning Text Analytics service connection must be activated before the model can be activated. Please activate the text analytics service connection and try again.
        /// </summary>
        TextAnalyticsModelActivateConnectionMustBeActive = -2147084713,

        /// <summary>
        /// Theme Id or Update Timestamp value is not present in theme data.
        /// </summary>
        ThemeIdOrUpdateTimestampIsNull = -2147088175,

        /// <summary>
        /// Too many requests.
        /// </summary>
        Throttling = -2147094269,

        /// <summary>
        /// Number of requests exceeded the limit of {0} over time window of {1} seconds.
        /// </summary>
        ThrottlingBurstRequestLimitExceededError = -2147015902,

        /// <summary>
        /// Number of concurrent requests exceeded the limit of {0}.
        /// </summary>
        ThrottlingConcurrencyLimitExceededError = -2147015898,

        /// <summary>
        /// Combined execution time of incoming requests exceeded limit of {0} milliseconds over time window of {1} seconds. Decrease number of concurrent requests or reduce the duration of requests and try again later.
        /// </summary>
        ThrottlingTimeExceededError = -2147015903,

        /// <summary>
        /// The stream being read from has too many bytes.
        /// </summary>
        TooManyBytesInInputStream = -2147157759,

        /// <summary>
        /// Number of calculated fields in query exceeded maximum limit of {0}.
        /// </summary>
        TooManyCalculatedFieldsInQuery = -2147089367,

        /// <summary>
        /// Number of parameters in a condition exceeded maximum limit.
        /// </summary>
        TooManyConditionParametersInQuery = -2147204338,

        /// <summary>
        /// Number of conditions in query exceeded maximum limit.
        /// </summary>
        TooManyConditionsInQuery = -2147204340,

        /// <summary>
        /// Too many entities enabled for auto created access teams.
        /// </summary>
        TooManyEntitiesEnabledForAutoCreatedAccessTeams = -2147187918,

        /// <summary>
        /// Number of link entities in query exceeded maximum limit.
        /// </summary>
        TooManyLinkEntitiesInQuery = -2147204339,

        /// <summary>
        /// Number of multiselect condition parameters in query exceeded maximum limit: {0}.
        /// </summary>
        TooManyMultiSelectConditionParametersInQuery = -2147155421,

        /// <summary>
        /// Number of distinct picklist values exceed the limit.
        /// </summary>
        TooManyPicklistValues = -2147187566,

        /// <summary>
        /// Sending to multiple recipients is not supported.
        /// </summary>
        TooManyRecipients = -2147207922,

        /// <summary>
        /// Number of selections for MultiSelectPicklist Attribute Type exceeded maximum limit: {0}.
        /// </summary>
        TooManySelectionsForAttributeType = -2147155422,

        /// <summary>
        /// Current number of teams: {0} is greater than teams limit: {1} for entity with ObjectTypeCode {2}
        /// </summary>
        TooManyTeamTemplatesForEntityAccessTeams = -2147187917,

        /// <summary>
        /// The configuration used for the build is invalid. Topic determination fields are required for the configuration used for topic analysis.
        /// </summary>
        TopicModelActivateWithInvalidConfiguration = -2147084714,

        /// <summary>
        /// Cannot update or delete topic model configuration because it is associated with an active topic model.
        /// </summary>
        TopicModelConfigurationAssociatedModelAlreadyActive = -2147084688,

        /// <summary>
        /// Activation requires specifying the build configuration. Specify the configuration used for the build before activation.
        /// </summary>
        TopicModelConfigurationUsedEmpty = -2147084717,

        /// <summary>
        /// Activation requires setting the build schedule. Specify the schedule build settings before activation.
        /// </summary>
        TopicModelScheduleBuildSettingsEmpty = -2147084719,

        /// <summary>
        /// Specify the configuration used for the build.
        /// </summary>
        TopicModelTestWithoutConfiguration = -2147084716,

        /// <summary>
        /// The trace record has an invalid trace code or an insufficient number of trace parameters.
        /// </summary>
        TraceMessageConstructionError = -2147157760,

        /// <summary>
        /// Transaction Aborted.
        /// </summary>
        TransactionAborted = -2147220907,

        /// <summary>
        /// Transaction not committed.
        /// </summary>
        TransactionNotCommited = -2147220910,

        /// <summary>
        /// Transaction not started.
        /// </summary>
        TransactionNotStarted = -2147220911,

        /// <summary>
        /// The operation that you are trying to perform does not support transactions.
        /// </summary>
        TransactionNotSupported = -2147098617,

        /// <summary>
        /// The resume/retry of Transformation job of Import is not supported.
        /// </summary>
        TransformationResumeNotSupported = -2147187613,

        /// <summary>
        /// Cannot call import before transform.
        /// </summary>
        TransformMustBeCalledBeforeImport = -2147220683,

        /// <summary>
        /// This article is a translation of the original article. It cannot be translated again. If you want another translation, start with the original article rather than this one.
        /// </summary>
        TranslateArticle_OnlyPrimaryArticlesCanBeTranslated = -2147085310,

        /// <summary>
        /// A translation for this language already exists for this version of the article
        /// </summary>
        TranslateArticle_TranslationCanNotBeCreatedForTheSameLanguage = -2147085309,

        /// <summary>
        /// We can't get to the trending documents. Try again later.
        /// </summary>
        TrendingDocumentsDataRetrievalFailure = -2147204556,

        /// <summary>
        /// Trending Documents is disabled for your Microsoft Dynamics 365 account.
        /// </summary>
        TrendingDocumentsIntegrationDisabledError = -2147204557,

        /// <summary>
        /// Trending Documents is turned off. Please contact your system administrator to turn this feature on in System Settings.
        /// </summary>
        TrendingDocumentsIntegrationTurnedOffError = -2147204523,

        /// <summary>
        /// Trending Documents isn't available in offline mode.
        /// </summary>
        TrendingDocumentsOfflineModeError = -2147204520,

        /// <summary>
        /// The Trending Documents dashboard component isn't supported by your company's Microsoft Office service.
        /// </summary>
        TrendingDocumentsOnpremiseDeploymentError = -2147204558,

        /// <summary>
        /// An error has occurred when trying to run this flow.
        /// </summary>
        TriggerFlowFailure = -2147155359,

        /// <summary>
        /// Type should be set to Definition while creating business process flow category
        /// </summary>
        TypeNotSetToDefinition = -2147089406,

        /// <summary>
        /// There was an error generating the UIData from XAML.
        /// </summary>
        UIDataGenerationFailed = -2147200969,

        /// <summary>
        /// The workflow does not contain UIData.
        /// </summary>
        UIDataMissingInWorkflow = -2147187599,

        /// <summary>
        /// A variable or input argument with the same name already exists. Choose a different name, and try again.
        /// </summary>
        UIScriptIdentifierDuplicate = -2147159529,

        /// <summary>
        /// The variable or input argument name is invalid. The name can only contain '_', numerical, and alphabetical characters. Choose a different name, and try again.
        /// </summary>
        UIScriptIdentifierInvalid = -2147159528,

        /// <summary>
        /// The variable or input argument name is too long. Choose a smaller name, and try again.
        /// </summary>
        UIScriptIdentifierInvalidLength = -2147159527,

        /// <summary>
        /// The quote cannot be activated because it is not in draft state.
        /// </summary>
        UnableToActivateQuoteNotInDraft = -2146435069,

        /// <summary>
        /// Unable to load the custom activity.
        /// </summary>
        UnableToLoadCustomActivity = -2147200934,

        /// <summary>
        /// Unable to load plug-in assembly.
        /// </summary>
        UnableToLoadPluginAssembly = -2147204719,

        /// <summary>
        /// Unable to load plug-in type.
        /// </summary>
        UnableToLoadPluginType = -2147204720,

        /// <summary>
        /// Unable to load the transformation assembly.
        /// </summary>
        UnableToLoadTransformationAssembly = -2147220616,

        /// <summary>
        /// Unable to load the transformation type.
        /// </summary>
        UnableToLoadTransformationType = -2147220615,

        /// <summary>
        /// The specified user name and password can not logon.
        /// </summary>
        UnableToLogOnUserFromUserNameAndPassword = -2147204335,

        /// <summary>
        /// Some Internal error occurred in sending invitation, Please try again later
        /// </summary>
        UnableToSendEmail = -2147176427,

        /// <summary>
        /// The mailbox is not in approved state. Send/Receive mails are allowed only for approved mailboxes.
        /// </summary>
        UnapprovedMailbox = -2147098080,

        /// <summary>
        /// Attempted to perform an unauthorized operation.
        /// </summary>
        UnauthorizedAccess = -2147220873,

        /// <summary>
        /// An unexpected error occurred.
        /// </summary>
        UnExpected = -2147220970,

        /// <summary>
        /// There was an unexpected error during mail merge.
        /// </summary>
        UnexpectedErrorInMailMerge = -2147220688,

        /// <summary>
        /// Unexpected null reference error: {0}.
        /// </summary>
        UnexpectedNullReferenceError = -2147159996,

        /// <summary>
        /// The right operand array in the expression contain unexpected no. of operand.
        /// </summary>
        UnexpectedRightOperandCount = -2147090426,

        /// <summary>
        /// The unit does not exist.
        /// </summary>
        UnitDoesNotExist = -2147206373,

        /// <summary>
        /// Using this base unit would create a loop in the unit hierarchy.
        /// </summary>
        UnitLoopBeingCreated = -2147206374,

        /// <summary>
        /// Loop exists in the unit hierarchy.
        /// </summary>
        UnitLoopExists = -2147206375,

        /// <summary>
        /// The unit name cannot be null.
        /// </summary>
        UnitNoName = -2147206362,

        /// <summary>
        /// The unit does not exist in the specified unit schedule.
        /// </summary>
        UnitNotInSchedule = -2147206378,

        /// <summary>
        /// One or more input transformation parameter values are invalid: {0}.
        /// </summary>
        UnknownInvalidTransformationParameterGeneric = -2147187437,

        /// <summary>
        /// The child entity supplied is not a child.
        /// </summary>
        unManagedchildentityisnotchild = -2147220250,

        /// <summary>
        /// Child-of condition is only allowed on offline filters.
        /// </summary>
        unManagedcihldofconditionforoffilefilters = -2147220311,

        /// <summary>
        /// Found {0} dependency records where unmanaged component is the parent of a managed component. First record (dependentcomponentobjectid = {1}, type = {2}, requiredcomponentobjectid = {3}, type= {4}, solution = {5}).
        /// </summary>
        UnmanagedComponentParentsManagedComponent = -2147188677,

        /// <summary>
        /// No data specified for ProcessLiteralCondition.
        /// </summary>
        unManagedemptyprocessliteralcondition = -2147220304,

        /// <summary>
        /// The entity is not an intersect entity.
        /// </summary>
        unManagedentityisnotintersect = -2147220310,

        /// <summary>
        /// An error occurred adding a filter to the query plan.
        /// </summary>
        unManagederroraddingfiltertoqueryplan = -2147220278,

        /// <summary>
        /// An unexpected error occurred processing the filter nodes.
        /// </summary>
        unManagederrorprocessingfilternodes = -2147220284,

        /// <summary>
        /// A field was not validated by the platform.
        /// </summary>
        unManagedfieldnotvalidatedbyplatform = -2147220306,

        /// <summary>
        /// The filter index is out of range.
        /// </summary>
        unManagedfilterindexoutofrange = -2147220309,

        /// <summary>
        /// Not enough privilege to access the Microsoft Dynamics 365 object or perform the requested operation.
        /// </summary>
        unManagedIdsAccessDenied = -2147187962,

        /// <summary>
        /// The Account has child opportunities.
        /// </summary>
        unManagedidsaccounthaschildopportunities = -2147220207,

        /// <summary>
        /// Activity duration does not match start/end time
        /// </summary>
        unManagedidsactivitydurationdoesnotmatch = -2147207926,

        /// <summary>
        /// Invalid activity duration
        /// </summary>
        unManagedidsactivityinvalidduration = -2147207927,

        /// <summary>
        /// Activity regarding object type is invalid
        /// </summary>
        unManagedidsactivityinvalidobjecttype = -2147207933,

        /// <summary>
        /// Activity party object type is invalid
        /// </summary>
        unManagedidsactivityinvalidpartyobjecttype = -2147207931,

        /// <summary>
        /// Invalid activity regarding object, it probably does not exist
        /// </summary>
        unManagedidsactivityinvalidregardingobject = -2147207929,

        /// <summary>
        /// Invalid activity state
        /// </summary>
        unManagedidsactivityinvalidstate = -2147207936,

        /// <summary>
        /// Invalid activity time, check format
        /// </summary>
        unManagedidsactivityinvalidtimeformat = -2147207928,

        /// <summary>
        /// Invalid activity type code
        /// </summary>
        unManagedidsactivityinvalidtype = -2147207935,

        /// <summary>
        /// This type of activity is not routable
        /// </summary>
        unManagedidsactivitynotroutable = -2147207925,

        /// <summary>
        /// Activity regarding object Id or type is missing
        /// </summary>
        unManagedidsactivityobjectidortypemissing = -2147207934,

        /// <summary>
        /// Activity party object Id or type is missing
        /// </summary>
        unManagedidsactivitypartyobjectidortypemissing = -2147207932,

        /// <summary>
        /// The logged-in user was not found in the Active Directory.
        /// </summary>
        unManagedidsanonymousenabled = -2147220954,

        /// <summary>
        /// Cannot change article template because there are knowledge base articles using it.
        /// </summary>
        unManagedidsarticletemplatecontainsarticles = -2147205627,

        /// <summary>
        /// KB article template is inactive.
        /// </summary>
        unManagedidsarticletemplateisnotactive = -2147205625,

        /// <summary>
        /// Cannot create temporary attachment file.
        /// </summary>
        unManagedidsattachmentcannotcreatetempfile = -2147202555,

        /// <summary>
        /// Cannot get temporary attachment file size.
        /// </summary>
        unManagedidsattachmentcannotgetfilesize = -2147202559,

        /// <summary>
        /// Cannot open temporary attachment file.
        /// </summary>
        unManagedidsattachmentcannotopentempfile = -2147202560,

        /// <summary>
        /// Cannot read temporary attachment file.
        /// </summary>
        unManagedidsattachmentcannotreadtempfile = -2147202557,

        /// <summary>
        /// Cannot truncate temporary attachment file.
        /// </summary>
        unManagedidsattachmentcannottruncatetempfile = -2147202553,

        /// <summary>
        /// Cannot unmap temporary attachment file.
        /// </summary>
        unManagedidsattachmentcannotunmaptempfile = -2147202554,

        /// <summary>
        /// Attachment file size is too big.
        /// </summary>
        unManagedidsattachmentinvalidfilesize = -2147202558,

        /// <summary>
        /// Attachment is empty.
        /// </summary>
        unManagedidsattachmentisempty = -2147202556,

        /// <summary>
        /// The business is not in the same merchant as parent business.
        /// </summary>
        unManagedidsbizmgmtbusinessparentdiffmerchant = -2147214076,

        /// <summary>
        /// The caller is not from partner business.
        /// </summary>
        unManagedidsbizmgmtcallernotinpartnerbusiness = -2147214060,

        /// <summary>
        /// The caller is not from primary business.
        /// </summary>
        unManagedidsbizmgmtcallernotinprimarybusiness = -2147214062,

        /// <summary>
        /// A local user cannot be added to the Dynamics 365.
        /// </summary>
        unManagedidsbizmgmtcannotaddlocaluser = -2147214030,

        /// <summary>
        /// This is a sub-business. Use IBizMerchant::Delete to delete this sub-business.
        /// </summary>
        unManagedidsbizmgmtcannotdeletebusiness = -2147214056,

        /// <summary>
        /// This is a provisioned root-business. Use IBizProvision::Delete to delete this root-business.
        /// </summary>
        unManagedidsbizmgmtcannotdeleteprovision = -2147214055,

        /// <summary>
        /// This business unit cannot be disabled.
        /// </summary>
        unManagedidsbizmgmtcannotdisablebusiness = -2147214054,

        /// <summary>
        /// This is a provisioned root-business. Use IBizProvision::Disable to disable this root-business.
        /// </summary>
        unManagedidsbizmgmtcannotdisableprovision = -2147214053,

        /// <summary>
        /// This is a sub-business. Use IBizMerchant::Enable to enable this sub-business.
        /// </summary>
        unManagedidsbizmgmtcannotenablebusiness = -2147214052,

        /// <summary>
        /// This is a provisioned root-business. Use IBizProvision::Enable to enable this root-business.
        /// </summary>
        unManagedidsbizmgmtcannotenableprovision = -2147214051,

        /// <summary>
        /// unManagedidsbizmgmtcannotmovedefaultuser
        /// </summary>
        unManagedidsbizmgmtcannotmovedefaultuser = -2147214075,

        /// <summary>
        /// Insufficient permissions to the specified Active Directory user. Contact your System Administrator.
        /// </summary>
        unManagedidsbizmgmtcannotreadaccountcontrol = -2147214035,

        /// <summary>
        /// The default user of a partnership can not be removed.
        /// </summary>
        unManagedidsbizmgmtcannotremovepartnershipdefaultuser = -2147214057,

        /// <summary>
        /// The organization name cannot be changed.
        /// </summary>
        unManagedidsbizmgmtcantchangeorgname = -2147214026,

        /// <summary>
        /// The default user is not in the business.
        /// </summary>
        unManagedidsbizmgmtdefaultusernotinbusiness = -2147214077,

        /// <summary>
        /// The default user is not from partner business.
        /// </summary>
        unManagedidsbizmgmtdefaultusernotinpartnerbusiness = -2147214059,

        /// <summary>
        /// The default user is not from primary business.
        /// </summary>
        unManagedidsbizmgmtdefaultusernotinprimarybusiness = -2147214061,

        /// <summary>
        /// The business name was unexpectedly missing.
        /// </summary>
        unManagedidsbizmgmtmissbusinessname = -2147214080,

        /// <summary>
        /// The parent business was unexpectedly missing.
        /// </summary>
        unManagedidsbizmgmtmissparentbusiness = -2147214078,

        /// <summary>
        /// The partnership partner business was unexpectedly missing.
        /// </summary>
        unManagedidsbizmgmtmisspartnerbusiness = -2147214065,

        /// <summary>
        /// The partnership primary business was unexpectedly missing.
        /// </summary>
        unManagedidsbizmgmtmissprimarybusiness = -2147214066,

        /// <summary>
        /// The user's domain name was unexpectedly missing.
        /// </summary>
        unManagedidsbizmgmtmissuserdomainname = -2147214079,

        /// <summary>
        /// The specified business does not have a parent business.
        /// </summary>
        unManagedidsbizmgmtnoparentbusiness = -2147214039,

        /// <summary>
        /// A partnership between specified primary business and partner business already exists.
        /// </summary>
        unManagedidsbizmgmtpartnershipalreadyexists = -2147214063,

        /// <summary>
        /// The partnership has been accepted or declined.
        /// </summary>
        unManagedidsbizmgmtpartnershipnotinpendingstatus = -2147214058,

        /// <summary>
        /// The primary business is the same as partner business.
        /// </summary>
        unManagedidsbizmgmtprimarysameaspartner = -2147214064,

        /// <summary>
        /// The user can not be its own parent user.
        /// </summary>
        unManagedidsbizmgmtusercannotbeownparent = -2147214074,

        /// <summary>
        /// This user does not have a parent user.
        /// </summary>
        unManagedidsbizmgmtuserdoesnothaveparent = -2147214050,

        /// <summary>
        /// The specified user's settings have not yet been created.
        /// </summary>
        unManagedidsbizmgmtusersettingsnotcreated = -2147214037,

        /// <summary>
        /// The calendar is invalid.
        /// </summary>
        unManagedidscalendarinvalidcalendar = -2147201792,

        /// <summary>
        /// The calendar rule does not exist.
        /// </summary>
        unManagedidscalendarruledoesnotexist = -2147200768,

        /// <summary>
        /// Callout code throws exception
        /// </summary>
        unManagedidsCalloutException = -2147197179,

        /// <summary>
        /// Invalid callout configuration
        /// </summary>
        unManagedidscalloutinvalidconfig = -2147197181,

        /// <summary>
        /// Invalid callout event
        /// </summary>
        unManagedidscalloutinvalidevent = -2147197180,

        /// <summary>
        /// Callout ISV code aborted the operation
        /// </summary>
        unManagedidscalloutisvabort = -2147197183,

        /// <summary>
        /// Callout ISV code throws exception
        /// </summary>
        unManagedidscalloutisvexception = -2147197184,

        /// <summary>
        /// Callout ISV code stopped the operation
        /// </summary>
        unManagedidscalloutisvstop = -2147197182,

        /// <summary>
        /// Cannot assign an object to a merchant.
        /// </summary>
        unManagedidscannotassigntobusiness = -2147220959,

        /// <summary>
        /// The price level cannot be deactivated because it is the default price level of an account, contact or product.
        /// </summary>
        unManagedidscannotdeactivatepricelevel = -2147206396,

        /// <summary>
        /// Private views cannot be default.
        /// </summary>
        unManagedidscannotdefaultprivateview = -2147220936,

        /// <summary>
        /// Cannot grant or revoke access rights to a merchant.
        /// </summary>
        unManagedidscannotgrantorrevokeaccesstobusiness = -2147220962,

        /// <summary>
        /// The user cannot be disabled because they have workflow rules running under their context.
        /// </summary>
        unManagedidscantdisable = -2147204780,

        /// <summary>
        /// The relationship link is empty
        /// </summary>
        unManagedidscascadeemptylinkerror = -2147199486,

        /// <summary>
        /// Cascade map information is inconsistent.
        /// </summary>
        unManagedidscascadeinconsistencyerror = -2147199488,

        /// <summary>
        /// Relationship type is not supported
        /// </summary>
        unManagedidscascadeundefinedrelationerror = -2147199487,

        /// <summary>
        /// Unexpected error occurred in cascading operation
        /// </summary>
        unManagedidscascadeunexpectederror = -2147199485,

        /// <summary>
        /// More than one sender specified
        /// </summary>
        unManagedidscommunicationsbadsender = -2147218687,

        /// <summary>
        /// Participation type is missing from an activity
        /// </summary>
        unManagedidscommunicationsnoparticipationmask = -2147218682,

        /// <summary>
        /// Object address not found on party or party is marked as non-emailable
        /// </summary>
        unManagedidscommunicationsnopartyaddress = -2147218688,

        /// <summary>
        /// At least one system user or queue in the organization must be a recipient
        /// </summary>
        unManagedidscommunicationsnorecipients = -2147218683,

        /// <summary>
        /// No email address was specified, and the calling user does not have an email address set
        /// </summary>
        unManagedidscommunicationsnosender = -2147218686,

        /// <summary>
        /// The sender does not have an email address on the party record
        /// </summary>
        unManagedidscommunicationsnosenderaddress = -2147218680,

        /// <summary>
        /// The template body is invalid
        /// </summary>
        unManagedidscommunicationstemplateinvalidtemplate = -2147218681,

        /// <summary>
        /// The Contact has child opportunities.
        /// </summary>
        unManagedidscontacthaschildopportunities = -2147220206,

        /// <summary>
        /// Account is required to save a contract.
        /// </summary>
        unManagedidscontractaccountmissing = -2147208703,

        /// <summary>
        /// The contract does not exist.
        /// </summary>
        unManagedidscontractdoesnotexist = -2147208697,

        /// <summary>
        /// The owner of the contract is invalid.
        /// </summary>
        unManagedidscontractinvalidowner = -2147208686,

        /// <summary>
        /// The start date of the renewed contract can not be earlier than the end date of the originating contract.
        /// </summary>
        unManagedidscontractinvalidstartdateforrenewedcontract = -2147208681,

        /// <summary>
        /// The totalallotments is invalid.
        /// </summary>
        unManagedidscontractinvalidtotalallotments = -2147208684,

        /// <summary>
        /// The contract line item does not exist.
        /// </summary>
        unManagedidscontractlineitemdoesnotexist = -2147208696,

        /// <summary>
        /// There are open cases against this contract line item.
        /// </summary>
        unManagedidscontractopencasesexist = -2147208694,

        /// <summary>
        /// The value for abbreviation already exists.
        /// </summary>
        unManagedidscontracttemplateabbreviationexists = -2147208682,

        /// <summary>
        /// An unexpected error occurred in Contracts.
        /// </summary>
        unManagedidscontractunexpected = -2147208704,

        /// <summary>
        /// Incorrect password for the specified customer portal user.
        /// </summary>
        unManagedidscpbadpassword = -2147211007,

        /// <summary>
        /// Decryption of the password failed.
        /// </summary>
        unManagedidscpdecryptfailed = -2147211005,

        /// <summary>
        /// Encryption of the supplied password failed.
        /// </summary>
        unManagedidscpencryptfailed = -2147211006,

        /// <summary>
        /// The customer portal user does not exist, or the password was incorrect.
        /// </summary>
        unManagedidscpuserdoesnotexist = -2147211008,

        /// <summary>
        /// Custom entity interface already initialized on this thread.
        /// </summary>
        unManagedidscustomentityalreadyinitialized = -2147198719,

        /// <summary>
        /// More than one relationship between the requested entities exists.
        /// </summary>
        unManagedidscustomentityambiguousrelationship = -2147198707,

        /// <summary>
        /// There is an existing loop in the database.
        /// </summary>
        unManagedidscustomentityexistingloop = -2147198713,

        /// <summary>
        /// The supplied child passed in is not a valid entity.
        /// </summary>
        unManagedidscustomentityinvalidchild = -2147198711,

        /// <summary>
        /// Custom entity ownership type mask is improperly set.
        /// </summary>
        unManagedidscustomentityinvalidownership = -2147198717,

        /// <summary>
        /// The supplied parent passed in is not a valid entity.
        /// </summary>
        unManagedidscustomentityinvalidparent = -2147198710,

        /// <summary>
        /// Supplied entity found, but it is not a custom entity.
        /// </summary>
        unManagedidscustomentitynameviolation = -2147198720,

        /// <summary>
        /// No relationship exists between the requested entities.
        /// </summary>
        unManagedidscustomentitynorelationship = -2147198708,

        /// <summary>
        /// Custom entity interface was not properly initialized.
        /// </summary>
        unManagedidscustomentitynotinitialized = -2147198718,

        /// <summary>
        /// The supplied parent and child entities are identical.
        /// </summary>
        unManagedidscustomentityparentchildidentical = -2147198709,

        /// <summary>
        /// Custom entity MD stack overflow.
        /// </summary>
        unManagedidscustomentitystackoverflow = -2147198715,

        /// <summary>
        /// Custom entity MD stack underflow.
        /// </summary>
        unManagedidscustomentitystackunderflow = -2147198714,

        /// <summary>
        /// Custom entity MD TLS not initialized.
        /// </summary>
        unManagedidscustomentitytlsfailure = -2147198716,

        /// <summary>
        /// This association would create a loop in the database.
        /// </summary>
        unManagedidscustomentitywouldcreateloop = -2147198712,

        /// <summary>
        /// Invalid customer address type.
        /// </summary>
        unManagedidscustomeraddresstypeinvalid = -2147220204,

        /// <summary>
        /// Transformation is not supported for this object.
        /// </summary>
        unManagedidscustomizationtransformationnotsupported = -2147203328,

        /// <summary>
        /// Unexpected error in data access.  DB Connection may not have been opened successfully.
        /// </summary>
        unManagedidsdataaccessunexpected = -2147212544,

        /// <summary>
        /// Data out of range
        /// </summary>
        unManagedidsdataoutofrange = -2147220948,

        /// <summary>
        /// Evaluation aborted.
        /// </summary>
        unManagedidsevalaborted = -2147210237,

        /// <summary>
        /// Evaluation aborted and stop further processing.
        /// </summary>
        unManagedidsevalallaborted = -2147210238,

        /// <summary>
        /// Evaluation completed and stop further processing.
        /// </summary>
        unManagedidsevalallcompleted = -2147210228,

        /// <summary>
        /// Assign action should have 4 parameters.
        /// </summary>
        unManagedidsevalassignshouldhave4parameters = -2147210239,

        /// <summary>
        /// Change type error.
        /// </summary>
        unManagedidsevalchangetypeerror = -2147210227,

        /// <summary>
        /// Evaluation completed.
        /// </summary>
        unManagedidsevalcompleted = -2147210236,

        /// <summary>
        /// Create action should have 2 parameters.
        /// </summary>
        unManagedidsevalcreateshouldhave2parameters = -2147210180,

        /// <summary>
        /// Error occurred when evaluating a WFPM_ABS parameter.
        /// </summary>
        unManagedidsevalerrorabsparameter = -2147210196,

        /// <summary>
        /// Error in action activity attachment.
        /// </summary>
        unManagedidsevalerroractivityattachment = -2147210216,

        /// <summary>
        /// Error occurred when evaluating a WFPM_ADD parameter.
        /// </summary>
        unManagedidsevalerroraddparameter = -2147210225,

        /// <summary>
        /// unManagedidsevalerrorappendtoactivityparty
        /// </summary>
        unManagedidsevalerrorappendtoactivityparty = -2147210177,

        /// <summary>
        /// Error in action assign.
        /// </summary>
        unManagedidsevalerrorassign = -2147210206,

        /// <summary>
        /// Error occurred when evaluating a WFPM_BEGIN_WITH parameter.
        /// </summary>
        unManagedidsevalerrorbeginwithparameter = -2147210184,

        /// <summary>
        /// Error occurred when evaluating a WFPM_BETWEEN parameter.
        /// </summary>
        unManagedidsevalerrorbetweenparameter = -2147210189,

        /// <summary>
        /// Error occurred when evaluating a WFPM_CONTAIN parameter.
        /// </summary>
        unManagedidsevalerrorcontainparameter = -2147210182,

        /// <summary>
        /// Error in create update.
        /// </summary>
        unManagedidsevalerrorcreate = -2147210181,

        /// <summary>
        /// Error in action create activity.
        /// </summary>
        unManagedidsevalerrorcreateactivity = -2147210217,

        /// <summary>
        /// Error in action create incident.
        /// </summary>
        unManagedidsevalerrorcreateincident = -2147210211,

        /// <summary>
        /// Error in action create note.
        /// </summary>
        unManagedidsevalerrorcreatenote = -2147210213,

        /// <summary>
        /// Divided by zero.
        /// </summary>
        unManagedidsevalerrordividedbyzero = -2147210218,

        /// <summary>
        /// Error occurred when evaluating a WFPM_DIVISION parameter.
        /// </summary>
        unManagedidsevalerrordivisionparameter = -2147210221,

        /// <summary>
        /// Division parameter can have only two subparameters.
        /// </summary>
        unManagedidsevalerrordivisionparameters = -2147210222,

        /// <summary>
        /// Error in action email template.
        /// </summary>
        unManagedidsevalerroremailtemplate = -2147210207,

        /// <summary>
        /// Error occurred when evaluating a WFPM_END_WITH parameter.
        /// </summary>
        unManagedidsevalerrorendwithparameter = -2147210183,

        /// <summary>
        /// Error occurred when evaluating a WFPM_EQ parameter.
        /// </summary>
        unManagedidsevalerroreqparameter = -2147210191,

        /// <summary>
        /// Error in action exec.
        /// </summary>
        unManagedidsevalerrorexec = -2147210201,

        /// <summary>
        /// Error happens when evaluating WFPM_FORMAT_BOOLEAN parameter.
        /// </summary>
        unManagedidsevalerrorformatbooleanparameter = -2147210171,

        /// <summary>
        /// Error happens when evaluating WFPM_FORMAT_DATETIME parameter.
        /// </summary>
        unManagedidsevalerrorformatdatetimeparameter = -2147210172,

        /// <summary>
        /// Error happens when evaluating WFPM_FORMAT_DECIMAL parameter.
        /// </summary>
        unManagedidsevalerrorformatdecimalparameter = -2147210166,

        /// <summary>
        /// Error happens when evaluating WFPM_FORMAT_INTEGER parameter.
        /// </summary>
        unManagedidsevalerrorformatintegerparameter = -2147210167,

        /// <summary>
        /// Error happens when evaluating WFPM_FORMAT_LOOKUP parameter.
        /// </summary>
        unManagedidsevalerrorformatlookupparameter = -2147210164,

        /// <summary>
        /// Error happens when evaluating WFPM_FORMAT_PICKLIST parameter.
        /// </summary>
        unManagedidsevalerrorformatpicklistparameter = -2147210170,

        /// <summary>
        /// unManagedidsevalerrorformattimezonecodeparameter
        /// </summary>
        unManagedidsevalerrorformattimezonecodeparameter = -2147210165,

        /// <summary>
        /// Error occurred when evaluating a WFPM_GEQ parameter.
        /// </summary>
        unManagedidsevalerrorgeqparameter = -2147210194,

        /// <summary>
        /// Error occurred when evaluating a WFPM_GT parameter.
        /// </summary>
        unManagedidsevalerrorgtparameter = -2147210195,

        /// <summary>
        /// Error in action halt.
        /// </summary>
        unManagedidsevalerrorhalt = -2147210200,

        /// <summary>
        /// Error in action handle activity.
        /// </summary>
        unManagedidsevalerrorhandleactivity = -2147210215,

        /// <summary>
        /// Error in action handle incident.
        /// </summary>
        unManagedidsevalerrorhandleincident = -2147210210,

        /// <summary>
        /// Failed to evaluate INCIDENT_QUEUE.
        /// </summary>
        unManagedidsevalerrorincidentqueue = -2147210199,

        /// <summary>
        /// unManagedidsevalerrorinlistparameter
        /// </summary>
        unManagedidsevalerrorinlistparameter = -2147210174,

        /// <summary>
        /// Error occurred when evaluating a WFPM_IN parameter.
        /// </summary>
        unManagedidsevalerrorinparameter = -2147210188,

        /// <summary>
        /// Invalid parameter.
        /// </summary>
        unManagedidsevalerrorinvalidparameter = -2147210197,

        /// <summary>
        /// Invalid email recipient.
        /// </summary>
        unManagedidsevalerrorinvalidrecipient = -2147210187,

        /// <summary>
        /// unManagedidsevalerrorisnulllistparameter
        /// </summary>
        unManagedidsevalerrorisnulllistparameter = -2147210173,

        /// <summary>
        /// Error occurred when evaluating a WFPM_LEQ parameter.
        /// </summary>
        unManagedidsevalerrorleqparameter = -2147210192,

        /// <summary>
        /// Error occurred when evaluating a WFPM_LT parameter.
        /// </summary>
        unManagedidsevalerrorltparameter = -2147210193,

        /// <summary>
        /// Error occurred when evaluating a WFPM_MODULUR parameter.
        /// </summary>
        unManagedidsevalerrormodulusparameter = -2147210219,

        /// <summary>
        /// Modulus parameter can have only two subparameters.
        /// </summary>
        unManagedidsevalerrormodulusparameters = -2147210220,

        /// <summary>
        /// Error occurred when evaluating a WFPM_MULTIPLICATION parameter.
        /// </summary>
        unManagedidsevalerrormultiplicationparameter = -2147210223,

        /// <summary>
        /// Error occurred when evaluating a WFPM_NEQ parameter.
        /// </summary>
        unManagedidsevalerrorneqparameter = -2147210190,

        /// <summary>
        /// Error in action note attachment.
        /// </summary>
        unManagedidsevalerrornoteattachment = -2147210212,

        /// <summary>
        /// Error happens when evaluating WFPM_GetObjectType parameter.
        /// </summary>
        unManagedidsevalerrorobjecttype = -2147210168,

        /// <summary>
        /// Error in action posturl.
        /// </summary>
        unManagedidsevalerrorposturl = -2147210202,

        /// <summary>
        /// unManagedidsevalerrorqueueidparameter
        /// </summary>
        unManagedidsevalerrorqueueidparameter = -2147210169,

        /// <summary>
        /// unManagedidsevalerrorremovefromactivityparty
        /// </summary>
        unManagedidsevalerrorremovefromactivityparty = -2147210176,

        /// <summary>
        /// Error in action route.
        /// </summary>
        unManagedidsevalerrorroute = -2147210204,

        /// <summary>
        /// Error in action send email.
        /// </summary>
        unManagedidsevalerrorsendemail = -2147210208,

        /// <summary>
        /// unManagedidsevalerrorsetactivityparty
        /// </summary>
        unManagedidsevalerrorsetactivityparty = -2147210175,

        /// <summary>
        /// Error in action set state.
        /// </summary>
        unManagedidsevalerrorsetstate = -2147210203,

        /// <summary>
        /// Error occurred when evaluating a WFPM_STRLEN parameter.
        /// </summary>
        unManagedidsevalerrorstrlenparameter = -2147210185,

        /// <summary>
        /// Error occurred when evaluating a WFPM_SUBSTR parameter.
        /// </summary>
        unManagedidsevalerrorsubstrparameter = -2147210186,

        /// <summary>
        /// Error occurred when evaluating a WFPM_SUBTRACTION parameter.
        /// </summary>
        unManagedidsevalerrorsubtractionparameter = -2147210224,

        /// <summary>
        /// Error in action unhandle activity.
        /// </summary>
        unManagedidsevalerrorunhandleactivity = -2147210214,

        /// <summary>
        /// Error in action unhandle incident.
        /// </summary>
        unManagedidsevalerrorunhandleincident = -2147210209,

        /// <summary>
        /// Error in action update.
        /// </summary>
        unManagedidsevalerrorupdate = -2147210205,

        /// <summary>
        /// Evaluation error.
        /// </summary>
        unManagedidsevalgenericerror = -2147210198,

        /// <summary>
        /// The specified metabase attribute does not exist.
        /// </summary>
        unManagedidsevalmetabaseattributenotfound = -2147210232,

        /// <summary>
        /// The specified refattributeid does not the query for a WFPM_SELECT parameter.
        /// </summary>
        unManagedidsevalmetabaseattributenotmatchquery = -2147210229,

        /// <summary>
        /// The specified metabase object has compound keys. We do not support compound-key entities yet.
        /// </summary>
        unManagedidsevalmetabaseentitycompoundkeys = -2147210233,

        /// <summary>
        /// The specified refentityid does not the query for a WFPM_SELECT parameter.
        /// </summary>
        unManagedidsevalmetabaseentitynotmatchquery = -2147210230,

        /// <summary>
        /// Missing the query subparameter in a select parameter.
        /// </summary>
        unManagedidsevalmissselectquery = -2147210226,

        /// <summary>
        /// The required object does not exist.
        /// </summary>
        unManagedidsevalobjectnotfound = -2147210235,

        /// <summary>
        /// The required property of the object was not set.
        /// </summary>
        unManagedidsevalpropertyisnull = -2147210231,

        /// <summary>
        /// The required property of the object was not found.
        /// </summary>
        unManagedidsevalpropertynotfound = -2147210234,

        /// <summary>
        /// Failed to calculate the schedule time for the timer action.
        /// </summary>
        unManagedidsevaltimererrorcalculatescheduletime = -2147210178,

        /// <summary>
        /// Invalid parameters for Timer action.
        /// </summary>
        unManagedidsevaltimerinvalidparameternumber = -2147210179,

        /// <summary>
        /// Update action should have 3 parameters.
        /// </summary>
        unManagedidsevalupdateshouldhave3parameters = -2147210240,

        /// <summary>
        /// Failure in obtaining user token.
        /// </summary>
        unManagedidsfailureinittoken = -2147220977,

        /// <summary>
        /// between, not-between, in, and not-in operators are not allowed on attributes of type text or ntext.
        /// </summary>
        unManagedidsfetchbetweentext = -2147204781,

        /// <summary>
        /// Full text operation failed.
        /// </summary>
        unManagedidsfulltextoperationfailed = -2147205626,

        /// <summary>
        /// The activity associated with this case is corrupted.
        /// </summary>
        unManagedidsincidentassociatedactivitycorrupted = -2147204091,

        /// <summary>
        /// The incident can not be closed because there are open activities for this incident.
        /// </summary>
        unManagedidsincidentcannotclose = -2147204086,

        /// <summary>
        /// The contract line item is not in the specified contract.
        /// </summary>
        unManagedidsincidentcontractdetaildoesnotmatchcontract = -2147204094,

        /// <summary>
        /// The activitytypecode is wrong.
        /// </summary>
        unManagedidsincidentinvalidactivitytypecode = -2147204090,

        /// <summary>
        /// Incident state is invalid.
        /// </summary>
        unManagedidsincidentinvalidstate = -2147204092,

        /// <summary>
        /// Missing object type code.
        /// </summary>
        unManagedidsincidentmissingactivityobjecttype = -2147204088,

        /// <summary>
        /// The activitytypecode can't be NULL.
        /// </summary>
        unManagedidsincidentnullactivitytypecode = -2147204089,

        /// <summary>
        /// You should specify a parent contact or account.
        /// </summary>
        unManagedidsincidentparentaccountandparentcontactnotpresent = -2147204080,

        /// <summary>
        /// You can either specify a parent contact or account, but not both.
        /// </summary>
        unManagedidsincidentparentaccountandparentcontactpresent = -2147204081,

        /// <summary>
        /// Invalid association.
        /// </summary>
        unManagedidsinvalidassociation = -2147220975,

        /// <summary>
        /// Invalid business id.
        /// </summary>
        unManagedidsinvalidbusinessid = -2147220983,

        /// <summary>
        /// Invalid item id.
        /// </summary>
        unManagedidsinvaliditemid = -2147220981,

        /// <summary>
        /// Invalid organization id.
        /// </summary>
        unManagedidsinvalidorgid = -2147220982,

        /// <summary>
        /// Item does not have an owning user.
        /// </summary>
        unManagedidsinvalidowninguser = -2147220974,

        /// <summary>
        /// Invalid team id.
        /// </summary>
        unManagedidsinvalidteamid = -2147220984,

        /// <summary>
        /// The user id is invalid or missing.
        /// </summary>
        unManagedidsinvaliduserid = -2147220985,

        /// <summary>
        /// One of the following occurred: invalid user id, invalid business id or the user does not belong to the business.
        /// </summary>
        unManagedidsinvaliduseridorbusinessidorusersbusinessinvalid = -2147220963,

        /// <summary>
        /// Invalid visibility.
        /// </summary>
        unManagedidsinvalidvisibility = -2147220978,

        /// <summary>
        /// User does not have access to modify the visibility of this item.
        /// </summary>
        unManagedidsinvalidvisibilitymodificationaccess = -2147220973,

        /// <summary>
        /// The Invoice Close API is deprecated. It has been replaced by the Pay and Cancel APIs.
        /// </summary>
        unManagedidsinvoicecloseapideprecated = -2147206363,

        /// <summary>
        /// Invalid event type.
        /// </summary>
        unManagedidsjournalinginvalideventtype = -2147219453,

        /// <summary>
        /// Account Id missed.
        /// </summary>
        unManagedidsjournalingmissingaccountid = -2147219450,

        /// <summary>
        /// Contact Id missed.
        /// </summary>
        unManagedidsjournalingmissingcontactid = -2147219448,

        /// <summary>
        /// Event direction code missed.
        /// </summary>
        unManagedidsjournalingmissingeventdirection = -2147219454,

        /// <summary>
        /// Event type missed.
        /// </summary>
        unManagedidsjournalingmissingeventtype = -2147219452,

        /// <summary>
        /// Incident Id missed.
        /// </summary>
        unManagedidsjournalingmissingincidentid = -2147219447,

        /// <summary>
        /// Lead Id missed.
        /// </summary>
        unManagedidsjournalingmissingleadid = -2147219451,

        /// <summary>
        /// Opportunity Id missed.
        /// </summary>
        unManagedidsjournalingmissingopportunityid = -2147219449,

        /// <summary>
        /// Unsupported type of objects passed in operation.
        /// </summary>
        unManagedidsjournalingunsupportedobjecttype = -2147219455,

        /// <summary>
        /// Lead does not exist.
        /// </summary>
        unManagedidsleaddoesnotexist = -2147220223,

        /// <summary>
        /// The lead does not have a parent.
        /// </summary>
        unManagedidsleadnoparent = -2147220213,

        /// <summary>
        /// The lead has not been assigned.
        /// </summary>
        unManagedidsleadnotassigned = -2147220212,

        /// <summary>
        /// The lead is not being assigned to the caller for acceptance.
        /// </summary>
        unManagedidsleadnotassignedtocaller = -2147220205,

        /// <summary>
        /// A lead can be associated with only one account.
        /// </summary>
        unManagedidsleadoneaccount = -2147220208,

        /// <summary>
        /// The user does not have the privilege to reject a lead, so he cannot be assigned the lead for acceptance.
        /// </summary>
        unManagedidsleadusercannotreject = -2147220211,

        /// <summary>
        /// Merge cannot be performed on entities from different business entity.
        /// </summary>
        unManagedidsmergedifferentbizorgerror = -2147200253,

        /// <summary>
        /// The specified entity does not exist
        /// </summary>
        unManagedidsmetadatanoentity = -2147217920,

        /// <summary>
        /// The relationship does not exist
        /// </summary>
        unManagedidsmetadatanorelationship = -2147217918,

        /// <summary>
        /// No relationship exists between the objects specified.
        /// </summary>
        unManagedidsnorelationship = -2147220938,

        /// <summary>
        /// The specified note is already attached to an object.
        /// </summary>
        unManagedidsnotesalreadyattached = -2147215615,

        /// <summary>
        /// Creating this parental association would create a loop in the annotation hierarchy.
        /// </summary>
        unManagedidsnotesloopbeingcreated = -2147215613,

        /// <summary>
        /// A loop exists in the annotation hierarchy.
        /// </summary>
        unManagedidsnotesloopexists = -2147215614,

        /// <summary>
        /// The specified note has no attachments.
        /// </summary>
        unManagedidsnotesnoattachment = -2147215612,

        /// <summary>
        /// The specified note does not exist.
        /// </summary>
        unManagedidsnotesnotedoesnotexist = -2147215616,

        /// <summary>
        /// Opportunity does not exist.
        /// </summary>
        unManagedidsopportunitydoesnotexist = -2147220224,

        /// <summary>
        /// The parent of an opportunity must be an account or contact.
        /// </summary>
        unManagedidsopportunityinvalidparent = -2147220220,

        /// <summary>
        /// The parent of the opportunity is missing.
        /// </summary>
        unManagedidsopportunitymissingparent = -2147220219,

        /// <summary>
        /// An opportunity can be associated with only one account.
        /// </summary>
        unManagedidsopportunityoneaccount = -2147220210,

        /// <summary>
        /// Removing this association will make the opportunity an orphan.
        /// </summary>
        unManagedidsopportunityorphan = -2147220209,

        /// <summary>
        /// Out of memory.
        /// </summary>
        unManagedidsoutofmemory = -2147220958,

        /// <summary>
        /// The specified owner has been disabled.
        /// </summary>
        unManagedidsownernotenabled = -2147220949,

        /// <summary>
        /// Both the user id and team id are present. Only one should be present.
        /// </summary>
        unManagedidspresentuseridandteamid = -2147220964,

        /// <summary>
        /// One of the attributes passed has already been set
        /// </summary>
        unManagedidspropbagattributealreadyset = -2147213249,

        /// <summary>
        /// One of the attributes passed cannot be NULL
        /// </summary>
        unManagedidspropbagattributenotnullable = -2147213250,

        /// <summary>
        /// The bag index in the collection was out of range.
        /// </summary>
        unManagedidspropbagcolloutofrange = -2147213282,

        /// <summary>
        /// The property bag interface could not be found.
        /// </summary>
        unManagedidspropbagnointerface = -2147213311,

        /// <summary>
        /// The specified property was null in the property bag.
        /// </summary>
        unManagedidspropbagnullproperty = -2147213310,

        /// <summary>
        /// The specified property was not found in the property bag.
        /// </summary>
        unManagedidspropbagpropertynotfound = -2147213312,

        /// <summary>
        /// Missing businessunitid.
        /// </summary>
        unManagedidsqueuemissingbusinessunitid = -2147205629,

        /// <summary>
        /// Callers' organization Id does not match businessunit's organization Id.
        /// </summary>
        unManagedidsqueueorganizationidnotmatch = -2147205628,

        /// <summary>
        /// Cannot go offline: missing access rights on required entity.
        /// </summary>
        unManagedidsrcsyncfilternoaccess = -2147204849,

        /// <summary>
        /// Invalid filter specified.
        /// </summary>
        unManagedidsrcsyncinvalidfiltererror = -2147204851,

        /// <summary>
        /// The specified subscription does not exist.
        /// </summary>
        unManagedidsrcsyncinvalidsubscription = -2147204855,

        /// <summary>
        /// The specified sync time is invalid.  Sync times must not be earlier than those returned by the previous sync.  Please reinitialize your subscription.
        /// </summary>
        unManagedidsrcsyncinvalidsynctime = -2147204864,

        /// <summary>
        /// Synchronization tasks can’t be performed on this computer since the synchronization method is set to None.
        /// </summary>
        unManagedidsrcsyncmethodnone = -2147204844,

        /// <summary>
        /// unManagedidsrcsyncmsxmlfailed
        /// </summary>
        unManagedidsrcsyncmsxmlfailed = -2147204863,

        /// <summary>
        /// Client does not exist.
        /// </summary>
        unManagedidsrcsyncnoclient = -2147204845,

        /// <summary>
        /// No primary client exists.
        /// </summary>
        unManagedidsrcsyncnoprimary = -2147204846,

        /// <summary>
        /// Cannot sync: not the primary OutlookSync client.
        /// </summary>
        unManagedidsrcsyncnotprimary = -2147204847,

        /// <summary>
        /// unManagedidsrcsyncsoapconnfailed
        /// </summary>
        unManagedidsrcsyncsoapconnfailed = -2147204861,

        /// <summary>
        /// unManagedidsrcsyncsoapfaulterror
        /// </summary>
        unManagedidsrcsyncsoapfaulterror = -2147204858,

        /// <summary>
        /// unManagedidsrcsyncsoapgenfailed
        /// </summary>
        unManagedidsrcsyncsoapgenfailed = -2147204862,

        /// <summary>
        /// unManagedidsrcsyncsoapparseerror
        /// </summary>
        unManagedidsrcsyncsoapparseerror = -2147204856,

        /// <summary>
        /// unManagedidsrcsyncsoapreaderror
        /// </summary>
        unManagedidsrcsyncsoapreaderror = -2147204857,

        /// <summary>
        /// unManagedidsrcsyncsoapsendfailed
        /// </summary>
        unManagedidsrcsyncsoapsendfailed = -2147204860,

        /// <summary>
        /// unManagedidsrcsyncsoapservererror
        /// </summary>
        unManagedidsrcsyncsoapservererror = -2147204859,

        /// <summary>
        /// unManagedidsrcsyncsqlgenericerror
        /// </summary>
        unManagedidsrcsyncsqlgenericerror = -2147204848,

        /// <summary>
        /// unManagedidsrcsyncsqlpausederror
        /// </summary>
        unManagedidsrcsyncsqlpausederror = -2147204852,

        /// <summary>
        /// unManagedidsrcsyncsqlstoppederror
        /// </summary>
        unManagedidsrcsyncsqlstoppederror = -2147204853,

        /// <summary>
        /// The caller id does not match the subscription owner id.  Only subscription owners may perform subscription operations.
        /// </summary>
        unManagedidsrcsyncsubscriptionowner = -2147204854,

        /// <summary>
        /// Cannot delete a role that is inherited from a parent business.
        /// </summary>
        unManagedidsrolesdeletenonparentrole = -2147216372,

        /// <summary>
        /// The role data is invalid.
        /// </summary>
        unManagedidsrolesinvalidroledata = -2147216384,

        /// <summary>
        /// Invalid role ID.
        /// </summary>
        unManagedidsrolesinvalidroleid = -2147216383,

        /// <summary>
        /// The role name is invalid.
        /// </summary>
        unManagedidsrolesinvalidrolename = -2147216374,

        /// <summary>
        /// Invalid role template ID.
        /// </summary>
        unManagedidsrolesinvalidtemplateid = -2147216380,

        /// <summary>
        /// The role's business unit ID was unexpectedly missing.
        /// </summary>
        unManagedidsrolesmissbusinessid = -2147216378,

        /// <summary>
        /// The privilege ID was unexpectedly missing.
        /// </summary>
        unManagedidsrolesmissprivid = -2147216376,

        /// <summary>
        /// The role ID was unexpectedly missing.
        /// </summary>
        unManagedidsrolesmissroleid = -2147216379,

        /// <summary>
        /// The role name was unexpectedly missing.
        /// </summary>
        unManagedidsrolesmissrolename = -2147216377,

        /// <summary>
        /// The specified role does not exist.
        /// </summary>
        unManagedidsrolesroledoesnotexist = -2147216382,

        /// <summary>
        /// The DB info for the recordset property bag has already been set.
        /// </summary>
        unManagedidsrspropbagdbinfoalreadyset = -2147213251,

        /// <summary>
        /// The DB info for the recordset property bag has not been set.
        /// </summary>
        unManagedidsrspropbagdbinfonotset = -2147213252,

        /// <summary>
        /// Duplicate fiscal calendars found for this salesperson/year
        /// </summary>
        unManagedidssalespeopleduplicatecalendarfound = -2147207166,

        /// <summary>
        /// Invalid fiscal calendar type
        /// </summary>
        unManagedidssalespeopleinvalidfiscalcalendartype = -2147207160,

        /// <summary>
        /// Invalid fiscal period index
        /// </summary>
        unManagedidssalespeopleinvalidfiscalperiodindex = -2147207161,

        /// <summary>
        /// Territories cannot be retrieved by this kind of object
        /// </summary>
        unManagedidssalespeopleinvalidterritoryobjecttype = -2147207164,

        /// <summary>
        /// Generic SQL error.
        /// </summary>
        unManagedidssqlerror = -2147204784,

        /// <summary>
        /// SQL timeout expired.
        /// </summary>
        unManagedidssqltimeouterror = -2147204783,

        /// <summary>
        /// The state is not valid for this object.
        /// </summary>
        unManagedidsstatedoesnotexist = -2147206407,

        /// <summary>
        /// The specified user is either disabled or is not a member of any business unit.
        /// </summary>
        unManagedidsusernotenabled = -2147220955,

        /// <summary>
        /// The view is not sharable.
        /// </summary>
        unManagedidsviewisnotsharable = -2147220942,

        /// <summary>
        /// The collection name specified is incorrect
        /// </summary>
        unManagedidsxmlinvalidcollectionname = -2147214845,

        /// <summary>
        /// A field that is not valid for create was specified
        /// </summary>
        unManagedidsxmlinvalidcreate = -2147214847,

        /// <summary>
        /// Invalid attributes
        /// </summary>
        unManagedidsxmlinvalidentityattributes = -2147214842,

        /// <summary>
        /// The entity name specified is incorrect
        /// </summary>
        unManagedidsxmlinvalidentityname = -2147214848,

        /// <summary>
        /// An invalid value was passed in for a field
        /// </summary>
        unManagedidsxmlinvalidfield = -2147214841,

        /// <summary>
        /// A field that is not valid for read was specified
        /// </summary>
        unManagedidsxmlinvalidread = -2147214840,

        /// <summary>
        /// A field that is not valid for update was specified
        /// </summary>
        unManagedidsxmlinvalidupdate = -2147214846,

        /// <summary>
        /// A parse error was encountered in the XML
        /// </summary>
        unManagedidsxmlparseerror = -2147214844,

        /// <summary>
        /// An unexpected error has occurred
        /// </summary>
        unManagedidsxmlunexpected = -2147214843,

        /// <summary>
        /// The platform cannot handle dbtime fields.
        /// </summary>
        unManagedinvalddbtimefield = -2147220263,

        /// <summary>
        /// An invalid number of arguments was supplied to a condition.
        /// </summary>
        unManagedinvalidargumentsforcondition = -2147220297,

        /// <summary>
        /// The platform cannot handle binary fields.
        /// </summary>
        unManagedinvalidbinaryfield = -2147220260,

        /// <summary>
        /// The businessunitid is missing or invalid.
        /// </summary>
        unManagedinvalidbusinessunitid = -2147220313,

        /// <summary>
        /// Character data is not valid when clearing an aggregate.
        /// </summary>
        unManagedinvalidcharacterdataforaggregate = -2147220258,

        /// <summary>
        /// The count value is invalid or missing.
        /// </summary>
        unManagedinvalidcountvalue = -2147220287,

        /// <summary>
        /// The platform cannot handle dbdate fields.
        /// </summary>
        unManagedinvaliddbdatefield = -2147220262,

        /// <summary>
        /// SetParam failed processing the DynamicParameterAccessor parameter.
        /// </summary>
        unManagedinvaliddynamicparameteraccessor = -2147220267,

        /// <summary>
        /// Only QB_LITERAL is supported for equality operand.
        /// </summary>
        unManagedinvalidequalityoperand = -2147220308,

        /// <summary>
        /// Escaped xml size not as expected.
        /// </summary>
        unManagedinvalidescapedxml = -2147220319,

        /// <summary>
        /// The platform cannot handle the specified field type.
        /// </summary>
        unManagedinvalidfieldtype = -2147220264,

        /// <summary>
        /// Invalid link entity, link to attribute, or link from attribute.
        /// </summary>
        unManagedinvalidlinkobjects = -2147220294,

        /// <summary>
        /// The operator provided is not valid.
        /// </summary>
        unManagedinvalidoperator = -2147220281,

        /// <summary>
        /// The organizationid is missing or invalid.
        /// </summary>
        unManagedinvalidorganizationid = -2147220290,

        /// <summary>
        /// The owningbusinessunit is missing or invalid.
        /// </summary>
        unManagedinvalidowningbusinessunit = -2147220312,

        /// <summary>
        /// The owningbusinessunit or businessunitid is missing or invalid.
        /// </summary>
        unManagedinvalidowningbusinessunitorbusinessunitid = -2147220292,

        /// <summary>
        /// The owninguser is mising or invalid.
        /// </summary>
        unManagedinvalidowninguser = -2147220291,

        /// <summary>
        /// The page value is invalid or missing.
        /// </summary>
        unManagedinvalidpagevalue = -2147220286,

        /// <summary>
        /// A parameterized query is not supported for the supplied parameter type.
        /// </summary>
        unManagedinvalidparametertypeforparameterizedquery = -2147220266,

        /// <summary>
        /// The principal id is missing or invalid.
        /// </summary>
        unManagedinvalidprincipal = -2147220322,

        /// <summary>
        /// Invalid privilege depth for user.
        /// </summary>
        unManagedinvalidprivilegeedepth = -2147220293,

        /// <summary>
        /// The privilege id is invalid or missing.
        /// </summary>
        unManagedinvalidprivilegeid = -2147220274,

        /// <summary>
        /// The privilege user group id is invalid or missing.
        /// </summary>
        unManagedinvalidprivilegeusergroup = -2147220275,

        /// <summary>
        /// ProcessChildOfCondition was called with non-child-of condition.
        /// </summary>
        unManagedinvalidprocesschildofcondition = -2147220300,

        /// <summary>
        /// ProcessLiteralCondition is only valid for use with Rollup queries.
        /// </summary>
        unManagedinvalidprocessliternalcondition = -2147220303,

        /// <summary>
        /// The security principal is invalid or missing.
        /// </summary>
        unManagedinvalidsecurityprincipal = -2147220270,

        /// <summary>
        /// The platform cannot handle stream fields.
        /// </summary>
        unManagedinvalidstreamfield = -2147220265,

        /// <summary>
        /// Failed to retrieve TLS Manager.
        /// </summary>
        unManagedinvalidtlsmananger = -2147220318,

        /// <summary>
        /// The transaction count was expected to be 1 in order to commit.
        /// </summary>
        unManagedinvalidtrxcountforcommit = -2147220254,

        /// <summary>
        /// The transaction count was expected to be 1 in order to rollback.
        /// </summary>
        unManagedinvalidtrxcountforrollback = -2147220255,

        /// <summary>
        /// A invalid value tag was found outside of it's condition tag.
        /// </summary>
        unManagedinvalidvaluettagoutsideconditiontag = -2147220289,

        /// <summary>
        /// The version value is invalid or missing.
        /// </summary>
        unManagedinvalidversionvalue = -2147220288,

        /// <summary>
        /// The platform cannot handle idispatch fields.
        /// </summary>
        unManagedinvaludidispatchfield = -2147220261,

        /// <summary>
        /// The address entity could not be found.
        /// </summary>
        unManagedmissingaddressentity = -2147220277,

        /// <summary>
        /// An expected attribute was not found for the tag specified.
        /// </summary>
        unManagedmissingattributefortag = -2147220283,

        /// <summary>
        /// The data access could not be retrieved from the ExecutionContext.
        /// </summary>
        unManagedmissingdataaccess = -2147220257,

        /// <summary>
        /// Missing filter attribute.
        /// </summary>
        unManagedmissingfilterattribute = -2147220307,

        /// <summary>
        /// Unexpected error locating link entity.
        /// </summary>
        unManagedmissinglinkentity = -2147220302,

        /// <summary>
        /// Object type must be specified for one of the attributes.
        /// </summary>
        unManagedMissingObjectType = -2147213309,

        /// <summary>
        /// The parent attribute was not found on the expected entity.
        /// </summary>
        unManagedmissingparentattributeonentity = -2147220299,

        /// <summary>
        /// The parent entity could not be located.
        /// </summary>
        unManagedmissingparententity = -2147220251,

        /// <summary>
        /// Unable to determine the previous owner's type.
        /// </summary>
        unManagedmissingpreviousownertype = -2147220272,

        /// <summary>
        /// Unable to access a relationship in an entity's ReferencesFrom collection.
        /// </summary>
        unManagedmissingreferencesfromrelationship = -2147220279,

        /// <summary>
        /// The relationship's ReferencingAttribute is missing or invalid.
        /// </summary>
        unManagedmissingreferencingattribute = -2147220280,

        /// <summary>
        /// More than one sort attributes were defined.
        /// </summary>
        unManagedmorethanonesortattribute = -2147220314,

        /// <summary>
        /// Object type was specified for one of the attributes that does not allow it.
        /// </summary>
        unManagedObjectTypeUnexpected = -2147213308,

        /// <summary>
        /// The parent attribute was not found for the child attribute.
        /// </summary>
        unManagedparentattributenotfound = -2147220316,

        /// <summary>
        /// Attributes of type partylist are not supported.
        /// </summary>
        unManagedpartylistattributenotsupported = -2147220296,

        /// <summary>
        /// A pending transaction already exists.
        /// </summary>
        unManagedpendingtrxexists = -2147220253,

        /// <summary>
        /// Cannot create an instance of managed proxy.
        /// </summary>
        unManagedproxycreationfailed = -2147220321,

        /// <summary>
        /// The TrxInteropHandler has already been set.
        /// </summary>
        unManagedtrxinterophandlerset = -2147220259,

        /// <summary>
        /// Failed to retrieve execution context (TLS).
        /// </summary>
        unManagedunablegetexecutioncontext = -2147220252,

        /// <summary>
        /// Unable to retrieve the session token.
        /// </summary>
        unManagedunablegetsessiontoken = -2147220269,

        /// <summary>
        /// Unable to retrieve the session token as there are no pending transactions.
        /// </summary>
        unManagedunablegetsessiontokennotrx = -2147220268,

        /// <summary>
        /// Cannot set to a different user context.
        /// </summary>
        unManagedunableswitchusercontext = -2147220256,

        /// <summary>
        /// Unable to access the query plan.
        /// </summary>
        unManagedunabletoaccessqueryplan = -2147220315,

        /// <summary>
        /// Unable to access a filter in the query plan.
        /// </summary>
        unManagedunabletoaccessqueryplanfilter = -2147220282,

        /// <summary>
        /// Unexpected error locating the filter for the condition.
        /// </summary>
        unManagedunabletolocateconditionfilter = -2147220285,

        /// <summary>
        /// Failed to retrieve privileges.
        /// </summary>
        unManagedunabletoretrieveprivileges = -2147220320,

        /// <summary>
        /// Unexpected type for the property.
        /// </summary>
        unManagedunexpectedpropertytype = -2147220276,

        /// <summary>
        /// Primary key attribute was not as expected.
        /// </summary>
        unManagedunexpectedrimarykey = -2147220301,

        /// <summary>
        /// An unknown aggregate operation was supplied.
        /// </summary>
        unManagedunknownaggregateoperation = -2147220298,

        /// <summary>
        /// Variant supplied contains data in an unusable format.
        /// </summary>
        unManagedunusablevariantdata = -2147220305,

        /// <summary>
        /// One or more outputs returned by the transformation is not mapped to target fields.
        /// </summary>
        UnmappedTransformationOutputDataFound = -2147220607,

        /// <summary>
        /// Primary Key must be populated for calls to platform on rich client in offline mode.
        /// </summary>
        UnpopulatedPrimaryKey = -2147220931,

        /// <summary>
        /// The connection roles are not related.
        /// </summary>
        UnrelatedConnectionRoles = -2147188202,

        /// <summary>
        /// A user dashboard Form XML cannot have Security = false.
        /// </summary>
        UnrestrictedIFrameInUserDashboard = -2147163380,

        /// <summary>
        /// Must specify an Activity Xml for CampaignActivity Execute/Distribute
        /// </summary>
        UnspecifiedActivityXmlForCampaignActivityPropagate = -2147220712,

        /// <summary>
        /// Missing Control Step.
        /// </summary>
        UnsupportedActionType = -2147089520,

        /// <summary>
        /// Unsupported arguments should not be marked as required.
        /// </summary>
        UnsupportedArgumentsMarkedRequired = -2147089516,

        /// <summary>
        /// The rule contain an attribute which is not supported.
        /// </summary>
        UnsupportedAttributeForEditor = -2147090416,

        /// <summary>
        /// Attribute {0} is not supported in the filter query option.
        /// </summary>
        UnsupportedAttributeInInProfileItemEntityFilters = -2147020509,

        /// <summary>
        /// Attribute or Operator “{0}” is not supported for Mobile Offline Org Filter.
        /// </summary>
        UnsupportedAttributeOrOperatorMobileOfflineFilters = -2147020523,

        /// <summary>
        /// Attribute type {0} is not supported. Remove attribute {1} from the query and try again.
        /// </summary>
        UnsupportedAttributeType = -2147098611,

        /// <summary>
        /// {0} is not recognized as a supported operation.
        /// </summary>
        UnsupportedComponentOperation = -2147160048,

        /// <summary>
        /// You can't create a property for a kit.
        /// </summary>
        UnsupportedCudOperationForDynamicProperties = -2147086311,

        /// <summary>
        /// The dashboard could not be opened.
        /// </summary>
        UnsupportedDashboardInEditor = -2147163378,

        /// <summary>
        /// The email server isn't supported.
        /// </summary>
        UnsupportedEmailServer = -2147098046,

        /// <summary>
        /// Sorry, your import failed because the {0} component isn’t supported for import and export.
        /// </summary>
        UnsupportedImportComponent = -2147085566,

        /// <summary>
        /// Unsupported list member type.
        /// </summary>
        UnsupportedListMemberType = -2147220735,

        /// <summary>
        /// Operator {0} is not supported with attribute {1} in the filter query option.
        /// </summary>
        UnsupportedOperatorForAttributeInProfileItemEntityFilters = -2147020511,

        /// <summary>
        /// A parameter specified is not supported by the Bulk Operation
        /// </summary>
        UnsupportedParameter = -2147220704,

        /// <summary>
        /// The process code is not supported on this entity.
        /// </summary>
        UnsupportedProcessCode = -2147220603,

        /// <summary>
        /// This message isn't supported for products of type bundle.
        /// </summary>
        UnsupportedSdkMessageForBundles = -2147086299,

        /// <summary>
        /// The rule contain a step which is not supported by the editor.
        /// </summary>
        UnsupportedStepForBusinessRuleEditor = -2147090423,

        /// <summary>
        /// The compressed (.zip) or .cab file couldn't be uploaded because the file is corrupted or doesn't contain valid importable files.
        /// </summary>
        UnsupportedZipFileForImport = -2147187563,

        /// <summary>
        /// Cannot start a new process to unzip.
        /// </summary>
        UnzipProcessCountLimitReached = -2147187564,

        /// <summary>
        /// Timeout happened in unzipping the zip file uploaded for import.
        /// </summary>
        UnzipTimeout = -2147187562,

        /// <summary>
        /// UpdateAttributeMap Error Occurred
        /// </summary>
        UpdateAttributeMap = -2147196412,

        /// <summary>
        /// UpdateEntityMap Error Occurred
        /// </summary>
        UpdateEntityMap = -2147196415,

        /// <summary>
        /// Cannot update a report from a template if the report was not created from a template
        /// </summary>
        UpdateNonCustomReportFromTemplate = -2147220336,

        /// <summary>
        /// Cannot update a published workflow definition.
        /// </summary>
        UpdatePublishedWorkflowDefinition = -2147201022,

        /// <summary>
        /// Cannot update a workflow dependency for a published workflow definition.
        /// </summary>
        UpdatePublishedWorkflowDefinitionWorkflowDependency = -2147201016,

        /// <summary>
        /// Cannot update a published workflow template.
        /// </summary>
        UpdatePublishedWorkflowTemplate = -2147200997,

        /// <summary>
        /// Failed to update the recurrence rule. A corresponding recurrence rule cannot be found.
        /// </summary>
        UpdateRecurrenceRuleFailed = -2147163884,

        /// <summary>
        /// This feature configuration can only be updated by a system administrator.
        /// </summary>
        UpdateRIOrganizationDataAccessNotAllowed = -2147204493,

        /// <summary>
        /// Cannot update a workflow activation.
        /// </summary>
        UpdateWorkflowActivation = -2147201021,

        /// <summary>
        /// Cannot update a workflow dependency associated with a workflow activation.
        /// </summary>
        UpdateWorkflowActivationWorkflowDependency = -2147201017,

        /// <summary>
        /// The specified Active Directory user already exists as a Dynamics 365 user.
        /// </summary>
        UserAlreadyExists = -2147214036,

        /// <summary>
        /// The mail merge operation was cancelled by the user.
        /// </summary>
        UserCancelledMailMerge = -2147220689,

        /// <summary>
        /// Cannot enable an unlicensed user
        /// </summary>
        UserCannotEnableWithoutLicense = -2147167668,

        /// <summary>
        /// The user data could not be found.
        /// </summary>
        UserDataNotFound = -2147167727,

        /// <summary>
        /// User does not have access to the tenant.
        /// </summary>
        UserDoesNotHaveAccessToTheTenant = -2147203833,

        /// <summary>
        /// User does not have required privileges (or role membership) to access the org when it is in Admin Only mode.
        /// </summary>
        UserDoesNotHaveAdminOnlyModePermissions = -2147180269,

        /// <summary>
        /// You must be a system administrator to execute this request.
        /// </summary>
        UserDoesNotHavePrivilegesToRunTheTool = -2147088136,

        /// <summary>
        /// User does not have send-as privilege
        /// </summary>
        UserDoesNotHaveSendAsAllowed = -2147203059,

        /// <summary>
        /// You do not have sufficient privileges to send e-mail as the selected queue. Contact your system administrator for assistance.
        /// </summary>
        UserDoesNotHaveSendAsForQueue = -2147203057,

        /// <summary>
        /// Primary User Id or Destination Queue Type code not set
        /// </summary>
        UserIdOrQueueNotSet = -2147220248,

        /// <summary>
        /// Invitation cannot be sent because user invitations are disabled.
        /// </summary>
        UserInviteDisabled = -2147167722,

        /// <summary>
        /// The user with id={0} belongs to a different business unit={1} than the role with roleId={2} and rolebusinessUnit={3}.
        /// </summary>
        UserInWrongBusiness = -2147216375,

        /// <summary>
        /// Current user is not a system admin in customer's organization
        /// </summary>
        UserIsNotSystemAdminInOrganization = -2147176343,

        /// <summary>
        /// You cannot set the selected user as the manager for this user because the selected user is either already the manager or is in the user's immediate management hierarchy.  Either select another user to be the manager or do not update the record.
        /// </summary>
        UserLoopBeingCreated = -2147214043,

        /// <summary>
        /// A manager for this user cannot be set because an existing relationship in the management hierarchy is causing a circular relationship.  This is usually caused by a manual edit of the Microsoft Dynamics 365 database. To fix this, the hierarchy in the database must be changed to remove the circular relationship.
        /// </summary>
        UserLoopExists = -2147214044,

        /// <summary>
        /// Type in a user name and save again
        /// </summary>
        UserNameRequiredForImpersonation = -2147098035,

        /// <summary>
        /// To follow other users, you must be logged in to Yammer. Log in to your Yammer account, and try again.
        /// </summary>
        UserNeverLoggedIntoYammer = -2147094255,

        /// <summary>
        /// The user has not been assigned any License
        /// </summary>
        UserNotAssignedLicense = -2147167669,

        /// <summary>
        /// The user has not been assigned any roles.
        /// </summary>
        UserNotAssignedRoles = -2147209463,

        /// <summary>
        /// The user is not in parent user's business hierarchy.
        /// </summary>
        UserNotInParentHierarchy = -2147214073,

        /// <summary>
        /// The user is not a member of the organization.
        /// </summary>
        UserNotMemberOfOrg = -2147015328,

        /// <summary>
        /// Invalid advanced find startup mode.
        /// </summary>
        UserSettingsInvalidAdvancedFindStartupMode = -2147214028,

        /// <summary>
        /// Invalid search experience value.
        /// </summary>
        UserSettingsInvalidSearchExperienceValue = -2147213997,

        /// <summary>
        /// Paging limit over maximum configured value.
        /// </summary>
        UserSettingsOverMaxPagingLimit = -2147204347,

        /// <summary>
        /// Failed to convert user time zone information.
        /// </summary>
        UserTimeConvertException = -2147220927,

        /// <summary>
        /// Failed to retrieve user time zone information.
        /// </summary>
        UserTimeZoneException = -2147220928,

        /// <summary>
        /// A system dashboard cannot contain user views and visualizations.
        /// </summary>
        UserViewsOrVisualizationsFound = -2147163390,

        /// <summary>
        /// Validate method is not supported for recurring appointment master.
        /// </summary>
        ValidateNotSupported = -2147163894,

        /// <summary>
        /// Error creating or updating Business Process: validation context cannot be null.
        /// </summary>
        ValidationContextIsNull = -2147089342,

        /// <summary>
        /// An error occurred while saving the {0} property.
        /// </summary>
        ValidationFailedForDynamicProperty = -2147086303,

        /// <summary>
        /// The {0} field will be a User Local Date and Time since the Date Only and Time Zone Independent behaviors won't work in earlier versions of Dynamics 365.
        /// </summary>
        ValidDateTimeBehaviorExportAsWarning = -2147088219,

        /// <summary>
        /// The behavior of this field was changed. You should review all the dependencies of this field, such as business rules, workflows, and calculated or rollup fields, to ensure that issues won't occur. Attribute: {0}
        /// </summary>
        ValidDateTimeBehaviorWarning = -2147088220,

        /// <summary>
        /// This API is only valid for Dynamics 365 online.
        /// </summary>
        ValidOnlyForDynamicsOnline = -2147015934,

        /// <summary>
        /// The options array is missing a value.
        /// </summary>
        ValueMissingInOptionOrderArray = -2147204315,

        /// <summary>
        /// Error parsing parameter {0} of type {1} with value {2}
        /// </summary>
        ValueParsingError = -2147176393,

        /// <summary>
        /// Required versioned row was not found in TempDB; the TempDB is likely out of space; try again at a later time.
        /// </summary>
        VersionedRowNotFoundInTempDB = -2147187390,

        /// <summary>
        /// Unsupported version - This is {0} version {1}, but version {2} was requested.
        /// </summary>
        VersionMismatch = -2147176416,

        /// <summary>
        /// One of the files in the compressed (.zip) or .cab file that you're trying to import exceeds the size limit.
        /// </summary>
        VeryLargeFileInZipImport = -2147187567,

        /// <summary>
        /// The condition {0} is not supported.
        /// </summary>
        ViewConditionTypeNotSupportedOffline = -2147020496,

        /// <summary>
        /// Required view for viewing duplicates of an entity not defined.
        /// </summary>
        ViewForDuplicateDetectionNotDefined = -2147186632,

        /// <summary>
        /// Currently view is not available Offline. Please try switching view or contact administrator
        /// </summary>
        ViewNotAvailableForMobileOffline = -2147093989,

        /// <summary>
        /// This view is not available on mobile.
        /// </summary>
        ViewNotAvailableOnMobile = -2147020506,

        /// <summary>
        /// This view is supported only in grid mode offline. It is not supported in calendar mode offline.
        /// </summary>
        ViewNotSupportedInCalendarModeOffline = -2147020504,

        /// <summary>
        /// Virtual entities are not supported.
        /// </summary>
        VirtualEntitiesNotSupported = -2147012576,

        /// <summary>
        /// Virtual Entity Operation Failed.
        /// </summary>
        VirtualEntityFailure = -2147155357,

        /// <summary>
        /// Feature Bit for VirtualEntity not enabled.
        /// </summary>
        VirtualEntityFCBOFF = -2146954989,

        /// <summary>
        /// The entity {0} is a virtual entity that’s not available in mobile offline.
        /// </summary>
        VirtualEntityNotSupportedInMobileOffline = -2147203039,

        /// <summary>
        /// No visualization module found with the given name.
        /// </summary>
        VisualizationModuleNotFound = -2147164142,

        /// <summary>
        /// Object type code is not specified for the visualization.
        /// </summary>
        VisualizationOtcNotFoundError = -2147164139,

        /// <summary>
        /// An error occurred while the chart was rendering
        /// </summary>
        VisualizationRenderingError = -2147164146,

        /// <summary>
        /// The webhook call failed because the Http request timed out at client side. Please check your webhook request handler.
        /// </summary>
        WebhooksHttpRequestTimedOut = -2147155454,

        /// <summary>
        /// The webhook call failed because of invalid http headers in authValue. Check if the authValue format, header names and values are valid for your Service Endpoint entity.
        /// </summary>
        WebhooksInvalidHttpHeaders = -2147155453,

        /// <summary>
        /// The webhook call failed because of invalid http query params in authValue. Check if the authValue format, query parameter names and values are valid for your Service Endpoint entity.
        /// </summary>
        WebhooksInvalidHttpQueryParams = -2147155452,

        /// <summary>
        /// The webhook call failed because the http request payload has exceeded maximum allowed size. Please reduce your request size and retry.
        /// </summary>
        WebhooksMaxSizeExceeded = -2147155449,

        /// <summary>
        /// The webhook call failed because the http request received non-success httpStatus code. Please check your webhook request handler.
        /// </summary>
        WebhooksNonSuccessHttpResponse = -2147155455,

        /// <summary>
        /// The Webhook post is disabled for the organization.
        /// </summary>
        WebhooksPostDisabled = -2147155450,

        /// <summary>
        /// The webhook call failed during http post. Please check the exception for more details.
        /// </summary>
        WebhooksPostRequestFailed = -2147155451,

        /// <summary>
        /// Webresource content size is too big.
        /// </summary>
        WebResourceContentSizeExceeded = -2147159788,

        /// <summary>
        /// A webresource with the same name already exists. Use a different name.
        /// </summary>
        WebResourceDuplicateName = -2147159787,

        /// <summary>
        /// Webresource name cannot be null or empty.
        /// </summary>
        WebResourceEmptyName = -2147159786,

        /// <summary>
        /// Silverlight version cannot be empty for silverlight web resources.
        /// </summary>
        WebResourceEmptySilverlightVersion = -2147159790,

        /// <summary>
        /// An error occurred while importing a Web resource. Try importing this solution again. For further assistance, contact Microsoft Dynamics 365 technical support.
        /// </summary>
        WebResourceImportError = -2147159781,

        /// <summary>
        /// The file for this Web resource does not exist in the solution file.
        /// </summary>
        WebResourceImportMissingFile = -2147159782,

        /// <summary>
        /// Silverlight version can only be of the format xx.xx[.xx.xx].
        /// </summary>
        WebResourceInvalidSilverlightVersion = -2147159789,

        /// <summary>
        /// Invalid web resource type specified.
        /// </summary>
        WebResourceInvalidType = -2147159791,

        /// <summary>
        /// Web resource names may only include letters, numbers, periods, and nonconsecutive forward slash characters.
        /// </summary>
        WebResourceNameInvalidCharacters = -2147159785,

        /// <summary>
        /// A Web resource cannot have the following file extensions: .aspx, .ascx, .asmx or .ashx.
        /// </summary>
        WebResourceNameInvalidFileExtension = -2147159783,

        /// <summary>
        /// Webresource name does not contain a valid prefix.
        /// </summary>
        WebResourceNameInvalidPrefix = -2147159784,

        /// <summary>
        /// Error in retrieving information from WOPI application url.
        /// </summary>
        WopiApplicationUrl = -2147088382,

        /// <summary>
        /// Request for retrieving the WOPI discovery XML failed.
        /// </summary>
        WopiDiscoveryFailed = -2147088384,

        /// <summary>
        /// {0} file exceeded size limit of {1}.
        /// </summary>
        WopiMaxFileSizeExceeded = -2147088381,

        /// <summary>
        /// Word document template feature is not enabled.
        /// </summary>
        WordTemplateFeatureNotEnabled = -2147088165,

        /// <summary>
        /// This workflow cannot be created, updated or published because it's referring unsupported workflow step.
        /// </summary>
        WorkflowActivityNotSupported = -2147200955,

        /// <summary>
        /// The original workflow definition has been deactivated and replaced.
        /// </summary>
        WorkflowAutomaticallyDeactivated = -2147200958,

        /// <summary>
        /// An error has occurred during compilation of the workflow.
        /// </summary>
        WorkflowCompileFailure = -2147201023,

        /// <summary>
        /// Incorrect formation of binary operator.
        /// </summary>
        WorkflowConditionIncorrectBinaryOperatorFormation = -2147201007,

        /// <summary>
        /// Incorrect formation of unary operator.
        /// </summary>
        WorkflowConditionIncorrectUnaryOperatorFormation = -2147201008,

        /// <summary>
        /// Condition operator not supported for specified type.
        /// </summary>
        WorkflowConditionOperatorNotSupported = -2147201006,

        /// <summary>
        /// Invalid type specified on condition.
        /// </summary>
        WorkflowConditionTypeNotSupport = -2147201005,

        /// <summary>
        /// Workflow does not exist.
        /// </summary>
        WorkflowDoesNotExist = -2147089397,

        /// <summary>
        /// Expression operator not supported for specified type.
        /// </summary>
        WorkflowExpressionOperatorNotSupported = -2147200978,

        /// <summary>
        /// Workflow Id cannot be NULL while creating business process flow category
        /// </summary>
        WorkflowIdIsNull = -2147089408,

        /// <summary>
        /// Workflow must be active to be used on Action Step.
        /// </summary>
        WorkflowIsNotActive = -2147200939,

        /// <summary>
        /// Workflow must be marked as On Demand.
        /// </summary>
        WorkflowIsNotOnDemand = -2147200935,

        /// <summary>
        /// The workflow cannot be published or unpublished by someone who is not its owner.
        /// </summary>
        WorkflowPublishedByNonOwner = -2147201013,

        /// <summary>
        /// Automatic workflow cannot be published if no activation parameters have been specified.
        /// </summary>
        WorkflowPublishNoActivationParameters = -2147201000,

        /// <summary>
        /// The workflow definition contains a step that references and invalid custom activity. Remove the invalid references and try again.
        /// </summary>
        WorkflowReferencesInvalidActivity = -2147200968,

        /// <summary>
        /// Workflow should be paused by system.
        /// </summary>
        WorkflowSystemPaused = -2147201001,

        /// <summary>
        /// Validation failed on the specified workflow.
        /// </summary>
        WorkflowValidationFailure = -2147201004,

        /// <summary>
        /// Boolean attributes must have exactly two option values.
        /// </summary>
        WrongNumberOfBooleanOptions = -2147204325,

        /// <summary>
        /// This feature is not available in offline mode.
        /// </summary>
        XamlNotFound = -2146088113,

        /// <summary>
        /// Failed to generate excel.
        /// </summary>
        XlsxExportGeneratingExcelFailed = -2147088185,

        /// <summary>
        /// Invalid columns.
        /// </summary>
        XlsxImportColumnHeadersInvalid = -2147088186,

        /// <summary>
        /// Failed to import data.
        /// </summary>
        XlsxImportExcelFailed = -2147088184,

        /// <summary>
        /// Required columns missing.
        /// </summary>
        XlsxImportHiddenColumnAbsent = -2147088188,

        /// <summary>
        /// Column mismatch.
        /// </summary>
        XlsxImportInvalidColumnCount = -2147088187,

        /// <summary>
        /// Invalid file to import.
        /// </summary>
        XlsxImportInvalidExcelDocument = -2147088190,

        /// <summary>
        /// Invalid format in import file.
        /// </summary>
        XlsxImportInvalidFileData = -2147088189,

        /// <summary>
        /// You have waited too long to complete the Yammer authorization. Please try again.
        /// </summary>
        YammerAuthTimedOut = -2147094265,

        /// <summary>
        /// Number of YValuesPerPoint for series and number of measures for measure collection for category should be same.
        /// </summary>
        YValuesPerPointMeasureMismatch = -2147164156,

        /// <summary>
        /// There were no email available in the mailbox or could not be retrieved.
        /// </summary>
        ZeroEmailReceived = -2147098063,

        /// <summary>
        /// The compressed (.zip) file that you're trying to upload contains more than one type of file. The file can contain either Excel (.xlsx) files, comma-delimited (.csv) files or XML Spreadsheet 2003 (.xml) files, but not a combination of file types.
        /// </summary>
        ZipFileHasMixOfCsvAndXmlFiles = -2147187579,

        /// <summary>
        /// The compressed (.zip) file that you are trying to upload contains another .zip file within it.
        /// </summary>
        ZipInsideZip = -2147187575,
    }
}