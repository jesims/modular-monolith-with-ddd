using System;
using System.Collections.Generic;

namespace CompanyName.MyMeetings.BuildingBlocks.Application;

public class InvalidCommandException : Exception
{
    public InvalidCommandException(List<string> errors)
    {
        Errors = errors;
    }

    public List<string> Errors { get; }
}
