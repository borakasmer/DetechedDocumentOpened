using DAL.Entities;
using DAL.Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentService
{
    public class ReadService : IReadService
    {
        DocumentContext _context;
        public ReadService(DocumentContext context)
        {
            _context = context;
        }
        public async Task<int> IncrementDocumentOpenedCountAsync(int userID, int attachmentID, int actionTypeID)
        {
            var updatedDocument = await _context.DocumentStatistics.FirstOrDefaultAsync(ds => 
            ds.UserId == userID && ds.AttachmentId == attachmentID);
            var count = 0;
            if (updatedDocument != null)
            {
                if (actionTypeID == (int)DocumentType.Opened)
                {
                    count = updatedDocument.OpenedCount + 1;
                    updatedDocument.OpenedCount = count;
                }
                else if (actionTypeID == (int)DocumentType.Clicked)
                {
                    count = updatedDocument.ClickCount + 1;
                    updatedDocument.ClickCount = count;
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                var model = new DocumentStatistics();
                model.UserId = userID;
                model.AttachmentId = attachmentID;
                model.OpenedCount = actionTypeID == (int)DocumentType.Opened ? 1 : 0;
                model.ClickCount = actionTypeID == (int)DocumentType.Clicked ? 1 : 0;
                _context.DocumentStatistics.Add(model);
                await _context.SaveChangesAsync();
            }

            return count;
        }
    }
}
