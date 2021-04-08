using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.NetCore
{
    public class FaultException<T>: System.ServiceModel.FaultException<T>
    {
        public FaultException(T detail, string reason)
            :base(detail, new FaultReason(reason))
        {

        }
    }

    public class FaultException : System.ServiceModel.FaultException
    {
        public FaultException(FaultReason reason)
            : base(reason.ReasonText)
        {

        }
    }

    public class FaultReason: System.ServiceModel.FaultReason
    {
        public string ReasonText { get; }
        public FaultReason(string reason)
            :base(reason)
        {
            ReasonText = reason;
        }
    }
}
