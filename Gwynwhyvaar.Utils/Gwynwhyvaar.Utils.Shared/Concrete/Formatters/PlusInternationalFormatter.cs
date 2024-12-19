using Gwynwhyvaar.Utils.Shared.Abstract;

namespace Gwynwhyvaar.Utils.Shared.Concrete.Formatters
{
    public class PlusInternationalFormatter : MsisdnFormatter
    {
        public override string Format(string msisdn, string countryCode)
        {
            if (msisdn.StartsWith(countryCode) && countryCode.StartsWith("+")) return msisdn;
            if (msisdn.StartsWith(countryCode) && !countryCode.StartsWith("+"))
                return string.Format("+{0}{1}", countryCode, msisdn);
            if (msisdn.StartsWith("0")) return string.Format("+{0}{1}", countryCode, msisdn.Substring(1));
            return string.Format("+{0}{1}", countryCode, msisdn);
        }
    }
}