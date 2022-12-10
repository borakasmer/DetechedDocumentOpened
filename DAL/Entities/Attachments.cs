using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Attachments
{
    public int Id { get; set; }

    public string? DocumentName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public string? DocumentType { get; set; }

    public virtual ICollection<DocumentStatistics> DocumentStatistics { get; } = new List<DocumentStatistics>();
}
