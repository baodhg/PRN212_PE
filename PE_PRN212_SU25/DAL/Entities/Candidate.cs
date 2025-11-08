using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Candidate
{
    public int CandidateId { get; set; }

    public string FullName { get; set; } = null!;

    public string Jlptlevel { get; set; } = null!;

    public string StudyGoal { get; set; } = null!;

    public virtual ICollection<MockTest> MockTests { get; set; } = new List<MockTest>();
}
