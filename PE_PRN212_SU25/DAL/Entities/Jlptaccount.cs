using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Jlptaccount
{
    public int AccountId { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? Role { get; set; }

    public DateTime? CreatedDate { get; set; }
}
