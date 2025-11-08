using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Member
{
    public string MemberId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int RoleId { get; set; }
}
