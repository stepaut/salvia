using System;

namespace salvia.Data.Entities;

public class UserDto
{
    public long Id { get; set; }
    public DateTime StartUsing { get; set; }
    public bool IsWhiteListed { get; set; }
}
