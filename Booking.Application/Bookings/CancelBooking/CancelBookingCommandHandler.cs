using Booking.Application.Abstractions.Messaging;
using Booking.Domain.Abstractions;
using Booking.Domain.Bookings;

namespace Booking.Application.Bookings.CancelBooking;

internal sealed class CancelBookingCommandHandler : ICommandHandler<CancelBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelBookingCommandHandler(
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        CancelBookingCommand request,
        CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        var result = booking.Cancel(DateTime.UtcNow);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}