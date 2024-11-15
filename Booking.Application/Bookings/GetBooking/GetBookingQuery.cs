using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Bookings.GetBooking
{
    public record GetBookingQuery(Guid BookingId) : ICachedQuery<BookingResponse>
    {
    }
}
