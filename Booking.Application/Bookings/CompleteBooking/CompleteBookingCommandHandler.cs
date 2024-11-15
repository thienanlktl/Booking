using Booking.Application.Abstractions.Messaging;
using Booking.Domain.Abstractions;
using Booking.Domain.Bookings;

namespace Booking.Application.Bookings.CompleteBooking;

internal sealed class CompleteBookingCommandHandler : ICommandHandler<CompleteBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteBookingCommandHandler(
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CompleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        var result = booking.Complete(DateTime.UtcNow);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}