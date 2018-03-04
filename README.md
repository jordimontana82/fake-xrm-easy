Fake Xrm Easy: TDD for Dynamics CRM and Dynamics 365 made simple
=================================================================================

|Build|Line Coverage|Branch Coverage|
|-----------|-----|-----------------|
|[![Build status](https://ci.appveyor.com/api/projects/status/2g8yc8jg817746du?svg=true)](https://ci.appveyor.com/project/Jordi/fake-xrm-easy)|[![Line coverage](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/badge_linecoverage.svg?v=1.40.0)](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/index.htm?v=1.40.0)|[![Branch coverage](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/badge_branchcoverage.svg?v=1.40.0)](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/index.htm?v=1.40.0)|

<b>Streamline unit testing</b> in Dynamics CRM by faking the `IOrganizationService` to work with an in-memory context.

<b>Drive your development</b> by unit testing any plugin, code activity, or 3rd party app using the `OrganizationService` easier and faster than ever before.

|Version|NuGet|
|-----------|-----|
|Dynamics v9|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.9?v=1.40.0)](https://www.nuget.org/packages/fakexrmeasy.9)|
|Dynamics 365|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.365?v=1.40.0)](https://www.nuget.org/packages/fakexrmeasy.365)|
|Dynamics CRM 2016|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.2016?v=1.40.0)](https://www.nuget.org/packages/fakexrmeasy.2016)|
|Dynamics CRM 2015|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.2015?v=1.40.0)](https://www.nuget.org/packages/fakexrmeasy.2015)|
|Dynamics CRM 2013|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.2013?v=1.40.0)](https://www.nuget.org/packages/fakexrmeasy.2013)|
|Dynamics CRM 2011|[![Nuget](https://buildstats.info/nuget/fakexrmeasy?v=1.40.0)](https://www.nuget.org/packages/fakexrmeasy)|

Supports Dynamics CRM 2011, 2013, 2015, 2016, and Dynamics 365 (8.x and 9.x). <b>NOTE:</b> With the release of Dynamics 365 v9 we are changing the naming convention for new packages to match the major version.

## Getting Started

<b>NEW!</b> Check out video tutorials about how to use Fake Xrm Easy starting [here](https://www.youtube.com/watch?v=ZLQ2o2P_xJY).

For a general overview of the framework and samples please refer to [this](http://dynamicsvalue.com/get-started/overview?source=git) link. 

If you have any questions, or anything you would like to discuss, please do not hesitate to send me an email and I'll be happy to discuss.

## Contributing

Please consider the below guidelines for contributing to the project:

* <u>Provide a unit test</u> to reproduce any issues detected where possible. 
* Attach all generated early bound typed entities required (if you're using early bound).
* Finally, if you are able to even fix the issue yourself, which would be <i>awesome</i>, please do [fork](https://github.com/jordimontana82/fake-xrm-easy/fork) the project and submit a [pull request](https://github.com/jordimontana82/fake-xrm-easy/pulls). We'll thank you forever and ever. 

## Roadmap

*  TODO:  Add support for date operators. See `ConditionOperator` implementation status [here](https://github.com/jordimontana82/fake-xrm-easy/blob/master/FakeXrmEasy.Tests.Shared/FakeContextTests/FetchXml/ConditionOperatorTests.cs#L19-L110). Feel free to add missing ones!
*  TODO: Implement remaining CRM messages. To know which ones have been implemented so far, see `FakeMessageExecutor` implementation status [here](https://github.com/jordimontana82/fake-xrm-easy/tree/master/FakeXrmEasy.Shared/FakeMessageExecutors).
*  TODO: Increase test coverage.

## Backlog

FetchXml implementation:
*   DONE: Add support for arithmetic operators
*   DONE:  Add support for FetchXml aggregations


## Tests disappeared?

Try deleting anything under the VS test explorer cache: `%Temp%\VisualStudioTestExplorerExtensions`
