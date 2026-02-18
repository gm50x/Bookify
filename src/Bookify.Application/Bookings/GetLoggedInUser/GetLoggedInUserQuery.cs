using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Bookings.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
