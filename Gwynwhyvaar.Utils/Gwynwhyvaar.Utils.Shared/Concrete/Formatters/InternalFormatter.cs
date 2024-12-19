using Gwynwhyvaar.Utils.Shared.Abstract;

namespace Gwynwhyvaar.Utils.Shared.Concrete.Formatters
{
    public class InternalFormatter : MsisdnFormatter
    {
        public override string Format(string msisdn, string countryCode)
        {
            if (msisdn.StartsWith(countryCode) && countryCode.StartsWith("+")) return msisdn.Substring(4);
            if (msisdn.StartsWith(countryCode) && !countryCode.StartsWith("+")) return msisdn.Substring(3);
            if (msisdn.StartsWith("0")) return msisdn.Substring(1);
            return msisdn;
        }
    }
}