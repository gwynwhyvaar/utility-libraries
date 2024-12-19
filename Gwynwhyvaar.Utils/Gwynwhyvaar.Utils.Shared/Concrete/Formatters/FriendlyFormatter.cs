using Gwynwhyvaar.Utils.Shared.Abstract;

namespace Gwynwhyvaar.Utils.Shared.Concrete.Formatters
{
    public class FriendlyFormatter : MsisdnFormatter
    {
        public override string Format(string msisdn, string countryCode)
        {
            if (msisdn.StartsWith(countryCode) && countryCode.StartsWith("+"))
                return string.Format("0{0}", msisdn.Substring(4));
            if (msisdn.StartsWith(countryCode) && !countryCode.StartsWith("+"))
                return string.Format("0{0}", msisdn.Substring(3));
            if (msisdn.StartsWith("0")) return msisdn;
            return string.Format("0{0}", msisdn);
        }
    }
}