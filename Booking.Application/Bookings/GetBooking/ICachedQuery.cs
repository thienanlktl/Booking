using Booking.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Bookings.GetBooking
{
    public interface ICachedQuery<TResponse> : IQuery<TResponse>
    {
    }
}
