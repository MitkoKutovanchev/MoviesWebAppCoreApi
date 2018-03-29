using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Entities.LogService
{
    public interface ILog
    {
        Task LogCustomExceptionAsync(Exception ex, CustomId id);
        Task LogSendedEmailAsync();
        Task LogPerformerRequestAsync();
        Task LogVenueOwnerRequestAsync();
    }
}
