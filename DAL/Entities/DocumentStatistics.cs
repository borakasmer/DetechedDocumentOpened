using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class DocumentStatistics
{
    public int Id { get; set; }

    public int AttachmentId { get; set; }

    public int UserId { get; set; }

    public int OpenedCount { get; set; }

    public int ClickCount { get; set; }

    public virtual Attachments Attachment { get; set; } = null!;

    public virtual Users User { get; set; } = null!;
}
