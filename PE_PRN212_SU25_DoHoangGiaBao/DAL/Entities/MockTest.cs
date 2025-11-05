using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class MockTest
{
    public int TestId { get; set; }

    public string TestTitle { get; set; } = null!;

    public string SkillArea { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int? CandidateId { get; set; }

    public double? Score { get; set; }

    public virtual Candidate? Candidate { get; set; }
}
