using System;

namespace salvia.Data.Entities;

internal class UserEntity
{
    public long Id { get; set; }
    public DateTime StartUsing { get; set; }
    public bool IsWhiteListed { get; set; }
    public bool IsAllowedToLoadFiles { get; set; }
}
