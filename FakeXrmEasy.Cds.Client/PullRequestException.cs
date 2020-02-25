using System;

namespace FakeXrmEasy
{
    public class PullRequestException : Exception
    {
        public PullRequestException(string sMessage) :
            base(string.Format("Exception: {0}. This functionality is not available yet. Please consider contributing to the following Git project https://github.com/jordimontana82/fake-xrm-easy by cloning the repository and issuing a pull request.", sMessage))
        {
        }

        public static PullRequestException NotImplementedOrganizationRequest(Type t)
        {
            return new PullRequestException(string.Format("The organization request type '{0}' is not yet supported... but we DO love pull requests so please feel free to submit one! :)", t.ToString()));
        }

        public static PullRequestException PartiallyNotImplementedOrganizationRequest(Type t, string missingImplementation)
        {
            return new PullRequestException(string.Format("The organization request type '{0}' is not yet fully supported... {1}... but we DO love pull requests so please feel free to submit one! :)", t.ToString(), missingImplementation));
        }

        public static PullRequestException FetchXmlOperatorNotImplemented(string op)
        {
            return new PullRequestException(string.Format("The fetchxml operator '{0}' is not yet supported... but we DO love pull requests so please feel free to submit one! :)", op));
        }
    }
}