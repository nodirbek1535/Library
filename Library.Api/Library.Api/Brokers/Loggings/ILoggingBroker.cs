//===============================================
//@nodirbek1535 library api program (C)
//===============================================

namespace Library.Api.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogError(Exception exception);
        void LogCritical(Exception exception);

    }
}
