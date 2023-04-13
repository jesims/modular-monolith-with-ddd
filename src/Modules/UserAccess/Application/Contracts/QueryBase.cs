﻿using System;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

public abstract class QueryBase<TResult> : IQuery<TResult>
{
    protected QueryBase()
    {
        Id = Guid.NewGuid();
    }

    protected QueryBase(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
