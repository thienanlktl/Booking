﻿namespace Booking.Application.Abstractions.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
    
    string IdentityId { get; }
}