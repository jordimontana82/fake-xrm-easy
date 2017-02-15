Fake Xrm Easy: TDD for Dynamics CRM and Dynamics 365 made simple
=================================================================================

|Build|Line Coverage|Branch Coverage|
|-----------|-----|-----------------|
|[![Build status](https://ci.appveyor.com/api/projects/status/2g8yc8jg817746du?svg=true)](https://ci.appveyor.com/project/Jordi/fake-xrm-easy)|[![Line coverage](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/badge_linecoverage.svg?v=1.19.2)](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/index.htm?v=1.19.2)|[![Branch coverage](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/badge_branchcoverage.svg?v=1.19.2)](https://cdn.rawgit.com/jordimontana82/fake-xrm-easy/master/test/reports/index.htm?v=1.19.2)|

|Version|NuGet|
|-----------|-----|
|Dynamics 365|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.365?v=1.19.2)](https://www.nuget.org/packages/fakexrmeasy.365)|
|Dynamics CRM 2016|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.2016?v=1.19.2)](https://www.nuget.org/packages/fakexrmeasy.2016)|
|Dynamics CRM 2015|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.2015?v=1.19.2)](https://www.nuget.org/packages/fakexrmeasy.2015)|
|Dynamics CRM 2013|[![Nuget](https://buildstats.info/nuget/fakexrmeasy.2013?v=1.19.2)](https://www.nuget.org/packages/fakexrmeasy.2013)|
|Dynamics CRM 2011|[![Nuget](https://buildstats.info/nuget/fakexrmeasy?v=1.19.2)](https://www.nuget.org/packages/fakexrmeasy)|

The framework to streamline unit testing in Dynamics CRM by faking the IOrganizationService to work with an In-Memory context.

The framework supports Dynamics CRM 2011, 2013, 2015, 2016 and Dynamics 365.

Drive your development by unit testing any plugin, code activity, or 3rd party app using the OrganizationService easier & faster than ever before.

##Getting Started

NEW! Check out video tutorials about how to use Fake Xrm Easy. First one [here] (https://www.youtube.com/watch?v=ZLQ2o2P_xJY)

For a general overview of the framework and samples please refer to [this](http://dynamicsvalue.com/get-started/overview?source=git) link. 

If you have any kind of questions, or anything you would like to discuss, please do not hesitate to send me an email and I'll be happy to discuss.

##Like it?

Great! Help us to better maintain & **improve this project by taking this short [survey](https://es.surveymonkey.com/r/TK8PXLK).** It will take you **less than 5 minutes** and will help us prioritise new features.

##Contributing

Please consider the below guidelines for contributing to the project:

* If you detect and raise any issues, they will be resolved much more quickly if you provide a unit test to reproduce the issue. Also, if you raised an issue and still remains open for a while, please do not hesitate to send me an email to see if I could help. 
* Finally, if you are able to even fix the issue yourself, which would be awesome, please do fork the project and submit a pull request. We'll thank you forever and ever. 


##Roadmap / Backlog

*  FetchXml implementation:
*     DONE: Add support for arithmetic operators
*     IN PROGRESS:  Add support for FetchXml aggregations
*     TO DO:  Adding support for date operators...
	-	Check the [Full list of done / to do condition operators](https://github.com/jordimontana82/fake-xrm-easy/blob/master/FakeXrmEasy.Tests.Shared/FakeContextTests/FetchXml/ConditionOperatorTests.cs#L19-L110) and feel free to add missing ones!
*  Increase test coverage
  
