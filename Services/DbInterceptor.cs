using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace PersonAPI.Services
{
    public class DbInterceptor : DbCommandInterceptor
    {
        private ILoggerManager _logger;

        public DbInterceptor(ILoggerManager log)
        {
            _logger = log;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
			//TODO: Uncomment next line to log all CRUD queries
            // LogQueryDetails(command);
            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        private void LogQueryDetails(DbCommand command)
        {
            _logger.LogInfo(command.CommandText.Replace("\r\n", " "));
        }
    }
}
