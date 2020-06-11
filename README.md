Fake Xrm Easy: TDD for Dynamics CRM and Dynamics 365 (or now Common Data Service for Apps) made simple
=================================================================================

|Build|Line Coverage|Branch Coverage|
|-----------|-----|-----------------|
|[![Build status](https://ci.appveyor.com/api/projects/status/2g8yc8jg817746du?svg=true)](https://ci.appveyor.com/project/Jordi/fake-xrm-easy)|[![Line coverage](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/badge_linecoverage.svg?v=1.55.0)](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/index.htm?v=1.55.0)|[![Branch coverage](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/badge_branchcoverage.svg?v=1.55.0)](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/index.htm?v=1.55.0)|

<b>Streamline unit testing</b> in Dynamics CRM by faking the `IOrganizationService` to work with an in-memory context.

<b>Drive your development</b> by unit testing any plugin, code activity, or 3rd party app using the `OrganizationService` easier and faster than ever before.

<b>Note: To keep up to date with client-side unit testing version of this framework, please [have a look at this repo](http://github.com/jordimontana82/fake-xrm-easy-js) and samples in this other [sample code repo](http://github.com/jordimontana82/fake-xrm-easy-js-samples) </b>


|Version|Package Name|NuGet|
|-----------|------|-----|
|Dynamics v9 (>= 9.x)|FakeXrmEasy.9|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.9?v=1.55.0)](https://www.nuget.org/packages/fakexrmeasy.9)|
|Dynamics 365 (8.2.x)|FakeXrmEasy.365|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.365?v=1.55.0)](https://www.nuget.org/packages/fakexrmeasy.365)|
|Dynamics CRM 2016 ( >= 8.0 && <= 8.1)|FakeXrmEasy.2016|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.2016?v=1.55.0)](https://www.nuget.org/packages/fakexrmeasy.2016)|
|Dynamics CRM 2015 (7.x)|FakeXrmEasy.2015|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.2015?v=1.55.0)](https://www.nuget.org/packages/fakexrmeasy.2015)|
|Dynamics CRM 2013 (6.x)|FakeXrmEasy.2013|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.2013?v=1.55.0)](https://www.nuget.org/packages/fakexrmeasy.2013)|
|Dynamics CRM 2011 (5.x)|FakeXrmEasy|[![Nuget](https://buildstats.info/nuget/fakexrmeasy?v=1.55.0)](https://www.nuget.org/packages/fakexrmeasy)|

Supports Dynamics CRM 2011, 2013, 2015, 2016, and Dynamics 365 (8.x and 9.x). <b>NOTE:</b> With the release of Dynamics 365 v9 we are changing the naming convention for new packages to match the major version.

## Semantic Versioning

The NuGet packages use semantic versioning like this:

    x.y.z  => Major.Minor.Patch
       
x: stands for the major version. The package is very stable so that's why the major version didn't change yet.

y: minor version. Any minor updates add new functionality without breaking changes. An example of these would be a new operator or a new fake message executor.

z: patch. Any update to this number means new bug fixes for the existing functionality. A new minor version might also include bug fixes too.

## Support 

We believe in <b>sustainable</b> Open Source. The software is MIT licensed and provided to you for free but we encourage you (and by you, we mean the whole community) to extend it / improve it by yourselves, of course, with help from us. 

In programming terms: 

    Free Open Source !== Free Support. 

If you're a business entity who delivers solutions on top of the Power Platform and are using this project already, you can help make OSS sustainable while getting more visibility by becoming a sponsor. Please [reach out to me](jordi@dynamicsvalue.com) for sponsorship enquiries and to contribute and give back to this project.  

If you're an individual, feel free to check the Sponsorship tiers, any help is welcome and greatly appreciated.

For contributing, please see section below.

## Contributing

Please consider the below guidelines for contributing to the project:

* Priority: Given the overwhelming number of issues and pull requests, we'll review Pull Requests first, then any outstanding issues. We encourage you to resolve / extend issues by yourselves, as a community, and we'll prioritise those first because we know (as mantainers) the effort it takes. 

    Please do [fork](https://github.com/jordimontana82/fake-xrm-easy/fork) the project and submit a [pull request](https://github.com/jordimontana82/fake-xrm-easy/pulls)
    
    We'll thank you forever and ever. 

    If you don't know how to resolve something or are not familiar with pull requests, don't worry, raise the issue anyway. Those will be revised next.

* When raising an issue:

    * <u>**Please provide a sample unit test**</u> to reproduce any issues detected where possible. This will speed up the resolution.
    * Attach all generated early bound typed entities required (if you're using early bound).

* **If you're using the framework, please do [Star](https://github.com/jordimontana82/fake-xrm-easy/star) the project**, it'll give more visibility to the wider community to keep extending and improving it.



## Roadmap

*  TODO: We're working on a v2.x of this package which targets .net core. That new version has been developed for the last couple of months, and we're VERY close to make it public. In the meantime, PRs and issues will be on hold for the time being to keep track of "where we are" in order to be merged into both versions 1.x and 2.x. [More info here](https://github.com/jordimontana82/fake-xrm-easy/issues/504)

*  TODO:  Add support for date operators. See `ConditionOperator` implementation status [here](https://github.com/jordimontana82/fake-xrm-easy/blob/master/FakeXrmEasy.Tests.Shared/FakeContextTests/FetchXml/ConditionOperatorTests.cs#L19-L110). Feel free to add missing ones!
*  TODO: Implement remaining CRM messages. To know which ones have been implemented so far, see `FakeMessageExecutor` implementation status [here](https://github.com/jordimontana82/fake-xrm-easy/tree/master/FakeXrmEasy.Shared/FakeMessageExecutors).
*  TODO: Increase test coverage.
*  **NEW!** I'm planning a 2.x version, this version will contain all the major improvements I always thought of adding but that will introduce considerable breaking changes. If you want to join a private preview list, let me know.



## Tests disappeared?

Try deleting anything under the VS test explorer cache: `%Temp%\VisualStudioTestExplorerExtensions`

