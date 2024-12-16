using Gwynwhyvaar.Utils.Shared.Abstract;

namespace Gwynwhyvaar.Utils.Shared.Concrete
{
    public class InternationalFormatter : MsisdnFormatter
    {
        public override string Format(string msisdn, string countryCode)
        {
            msisdn = msisdn.Trim();

            if (msisdn.StartsWith(countryCode) && countryCode.StartsWith("+")) return msisdn.Substring(1);
            if (msisdn.StartsWith(countryCode) && !countryCode.StartsWith("+")) return msisdn;
            if (msisdn.StartsWith("0")) return string.Format("{0}{1}", msisdn.Substring(1), countryCode);
            return string.Format("{0}{1}", msisdn, countryCode);
        }
    }
}