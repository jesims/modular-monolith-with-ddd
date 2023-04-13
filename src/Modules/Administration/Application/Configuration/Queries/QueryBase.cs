﻿using System;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;

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
