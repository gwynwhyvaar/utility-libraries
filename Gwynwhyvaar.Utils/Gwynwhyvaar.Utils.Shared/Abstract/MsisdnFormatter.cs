using Gwynwhyvaar.Utils.Shared.Concrete.Formatters;
using Gwynwhyvaar.Utils.Shared.Enums;

namespace Gwynwhyvaar.Utils.Shared.Abstract
{
    public abstract class MsisdnFormatter
    {
        public abstract string Format(string msisdn, string countryCode);

        public static MsisdnFormatter GetFormat(MsisdnFormat format)
        {
            switch (format)
            {
                case MsisdnFormat.PlusInternationl:
                    return new PlusInternationalFormatter();

                case MsisdnFormat.International:
                    return new InternationalFormatter();

                case MsisdnFormat.Friendly:
                    return new FriendlyFormatter();

                case MsisdnFormat.Internal:
                    return new InternalFormatter();
                default:
                    return new FriendlyFormatter();
            }
        }
    }
}