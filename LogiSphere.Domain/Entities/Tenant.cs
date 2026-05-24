using LogiSphere.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogiSphere.Domain.Entities;

public class Tenant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public TenantTier Tier { get; set; }
    public string? ConnectionString { get; set; }
}
