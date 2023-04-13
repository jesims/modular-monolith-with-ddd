﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing;

internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T>
    where T : ICommand
{
    private readonly ICommandHandler<T> _decorated;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MeetingsContext _meetingContext;

    public UnitOfWorkCommandHandlerDecorator(
        ICommandHandler<T> decorated,
        IUnitOfWork unitOfWork,
        MeetingsContext meetingContext)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _meetingContext = meetingContext;
    }

    public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
    {
        await _decorated.Handle(command, cancellationToken);

        if (command is InternalCommandBase)
        {
            var internalCommand =
                await _meetingContext.InternalCommands.FirstOrDefaultAsync(
                    x => x.Id == command.Id,
                    cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = DateTime.UtcNow;
            }
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
