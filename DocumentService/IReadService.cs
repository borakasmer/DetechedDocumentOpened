using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentService
{
    public interface IReadService
    {
        public Task<int> IncrementDocumentOpenedCountAsync(int userID, int attachmentID, int actionTypeID);
    }
}
