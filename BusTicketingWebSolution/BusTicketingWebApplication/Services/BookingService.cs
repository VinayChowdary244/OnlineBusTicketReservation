using BusTicketingWebApplication.Exceptions;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Models.DTOs;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System;
using System.Collections.Generic;

// Service class for handling bus ticket bookings
namespace BusTicketingWebApplication.Services
{
    public class BookingService : IBookingService
    {
        // Repositories for data access
        private readonly IBookingRepository _bookingRepository;
        private readonly IBusRepository _busRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookedSeatRepository _bookedSeatRepository;
        private readonly ICancelledBookingRepository _cancelledBookingRepository;

        // Constructor to initialize repositories
        public BookingService(IBookingRepository bookingRepository, IBusRepository busRepository, IUserRepository userRepository, IBookedSeatRepository bookedSeatRepository, ICancelledBookingRepository cancelledBookingRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _busRepository = busRepository;
            _bookedSeatRepository = bookedSeatRepository;
            _cancelledBookingRepository = cancelledBookingRepository;
        }
        public BookingDTO Add(BookingDTO bookingDTO)
        {
            var bus = _busRepository.GetById(bookingDTO.BusId);
            if (bookingDTO.SelectedSeats.Count <= 0 || bookingDTO.SelectedSeats.Count > 40)
                throw new InvalidNoOfTicketsEnteredException();

            if (bus != null)
            {
                float Fare = bus.Cost;

                if (bookingDTO.SelectedSeats != null)
                {
                    var AllBookedSeats = _bookedSeatRepository.GetAll();

                    if (AllBookedSeats != null)
                    {
                        bool status = false;

                        foreach (var bookedSeat in AllBookedSeats)
                        {
                            if (bookedSeat.Date == bookingDTO.Date && bookedSeat.BusId == bookingDTO.BusId)
                            {
                                bookedSeat.BookedSeats.AddRange(bookingDTO.SelectedSeats);
                                bookedSeat.AvailableSeats -= bookingDTO.SelectedSeats.Count;
                                bookedSeat.BookedSeatCount += bookingDTO.SelectedSeats.Count;
                                status = true;
                                _bookedSeatRepository.Update(bookedSeat);

                                break;
                            }
                        }

                        if (!status)
                        {
                            BookedSeat newBookedSeat = new BookedSeat
                            {
                                BusId = bus.Id,
                                Date = bookingDTO.Date,
                                BookedSeats = bookingDTO.SelectedSeats,
                                AvailableSeats = 37 - bookingDTO.SelectedSeats.Count,
                                BookedSeatCount = bookingDTO.SelectedSeats.Count
                            };

                            _bookedSeatRepository.Add(newBookedSeat);
                        }
                    }
                    else
                    {
                        // If AllBookedSeats is null, create a new BookedSeat
                        BookedSeat newBookedSeat = new BookedSeat
                        {
                            BusId = bus.Id,
                            Date = bookingDTO.Date,
                            BookedSeats = bookingDTO.SelectedSeats,
                            AvailableSeats = 37 - bookingDTO.SelectedSeats.Count,
                            BookedSeatCount = bookingDTO.SelectedSeats.Count
                        };

                        _bookedSeatRepository.Add(newBookedSeat);
                    }

                    Booking booking = new Booking
                    {
                        UserName = bookingDTO.UserName,
                        BusId = bookingDTO.BusId,
                        Date = bookingDTO.Date,
                        Email=bookingDTO.Email,
                        SelectedSeats = bookingDTO.SelectedSeats,
                        TotalFare = bookingDTO.SelectedSeats.Count * Fare
                    };

                    var result = _bookingRepository.Add(booking);
                    ScheduleAndSendEmail(result);
                }
            }
            else
            {
                throw new InvalidBusIdException();
            }

            return bookingDTO;
        }



       // Method to schedule and send a booking confirmation email
        public void ScheduleAndSendEmail(Booking booking)
        {
            // Your email sending logic here
            string to = booking.Email;
            string subject = "Bus Ticket Booking Confirmation Email";
            string body = ($"Dear {booking.UserName}, \n \nYour Bus Tickets with Seat Numbers {string.Join(",", booking.SelectedSeats)} are Confirmed!! \nHave a Safe and Happy Journey!!");

            // Send the email
            SendNotificationEmail(to, subject, body);
        }

        // Method to send a notification email
        public void SendNotificationEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                string email = "nagavenkatasai7896@gmail.com";
                string password = "ufhcbpqnsnfzdqxr";

                // Create the email message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(email);
                mail.To.Add(recipientEmail);
                mail.Subject = subject;
                mail.Body = body;

                // Set up SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(email, password);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;

                // Send the email
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        // Method to share booking details with specified email addresses
        //public bool ShareEvent(int bookingId, List<string> recipientEmails)
        //{
        //    // Retrieve the event to be shared
        //    var bookingToShare = _bookingRepository.GetById(bookingId);
        //    if (bookingToShare != null)
        //    {
        //        // Customize the email subject and body for sharing
        //        string subject = "Shared Event: " + bookingToShare.Date;
        //        string body = $"Dear Recipient,\n\nYour tickets have been scheduled for {bookingToShare.Date}. Don't miss it!";

        //        // Loop through recipient emails and send individual emails
        //        foreach (var recipientEmail in recipientEmails)
        //        {
        //            SendNotificationEmail(recipientEmail, subject, body);
        //        }

        //        return true; // Sharing successful
        //    }
        //    return false; // Booking not found
        //}



        // Method to get booked seats for a specific bus and date
        public BookedSeat BookedSeatsInTheBus(BookedSeatsDTO bookedSeatsDTO)
        {
            var BookedSeat = _bookedSeatRepository.GetAll();
            if (BookedSeat != null)
            {
                for (int i = 0; i < BookedSeat.Count; i++)
                {
                    if (BookedSeat[i].BusId == bookedSeatsDTO.BusId)
                    {
                        if (BookedSeat[i].Date == bookedSeatsDTO.Date)
                        {
                            return BookedSeat[i];
                        }
                    }
                }
            }
            return null;
        }

        // Method to get a list of all bookings
        public List<Booking> GetBookings()
        {
            var bookings = _bookingRepository.GetAll();
            if (bookings != null)
            {
                return bookings.ToList();
            }
            throw new NoBookingsAvailableException();
        }



        public BookingIdDTO RemoveBooking(BookingIdDTO bookingIdDTO)
        {
            var BookingToBeRemoved = _bookingRepository.GetById(bookingIdDTO.Id);
            //var CurrentBooking = _userRepository.GetById(BookingToBeRemoved.UserName);
            //var CurrentEmail= _userRepository.GetById(BookingToBeRemoved.Email);
            if (BookingToBeRemoved != null)
            {
                CancelledBooking cancelledBooking = new CancelledBooking();
                cancelledBooking.BookingId = BookingToBeRemoved.BookingId;
                cancelledBooking.UserName = BookingToBeRemoved.UserName;
                cancelledBooking.BusId = BookingToBeRemoved.BusId;
                cancelledBooking.Date = BookingToBeRemoved.Date;
                cancelledBooking.Email = BookingToBeRemoved.Email;
                cancelledBooking.CancelledSeats = BookingToBeRemoved.SelectedSeats;
                cancelledBooking.TotalFare = BookingToBeRemoved.TotalFare;
                cancelledBooking.CancelledDate = DateTime.Now;
                _cancelledBookingRepository.Add(cancelledBooking);

                var result = _bookingRepository.Delete(bookingIdDTO.Id);
                if (result != null)
                {

                    var AllBookedSeats = _bookedSeatRepository.GetAll();
                    if (AllBookedSeats != null)
                    {
                        foreach (var bookedSeat in AllBookedSeats)
                        {
                            if (bookedSeat.Date == BookingToBeRemoved.Date && bookedSeat.BusId == BookingToBeRemoved.BusId)
                            {
                                bookedSeat.BookedSeats.RemoveAll(seat => BookingToBeRemoved.SelectedSeats.Contains(seat));
                                bookedSeat.AvailableSeats += BookingToBeRemoved.SelectedSeats.Count;
                                bookedSeat.BookedSeatCount -= BookingToBeRemoved.SelectedSeats.Count;

                                _bookedSeatRepository.Update(bookedSeat);

                                break;
                            }
                        }
                    }

                    // var bookedBusSeats = _bookedSeatRepository.GetById(BookingToBeRemoved.BusId);

                    //bookedBusSeats.BookedSeats.RemoveAll(seat => BookingToBeRemoved.SelectedSeats.Contains(seat));

                    // _bookedSeatRepository.Update(bookedBusSeats);
                    CancelAndSendEmail(bookingIdDTO);
                    return bookingIdDTO;
                    
                }
            }
            return null;
        }
        public void CancelAndSendEmail(BookingIdDTO booking)
        {
            // Your email sending logic here
            string to = booking.Email;
            string subject = "Bus Tickets Cancellation Email";
            string body = ($"Dear {booking.UserName}, \n \nYour Bus Tickets are Cancelled!! \nPlease Visit Again!!");

            // Send the email
            SendNotificationEmail(to, subject, body);
        }
        public List<CancelledBooking> CancelledBookingsList(UserNameDTO userNameDTO)
        {
            var bookings = _cancelledBookingRepository.GetAll();
            if (bookings != null)
            {
                List<CancelledBooking> CancelledBookingsList = new List<CancelledBooking>();
                foreach (var booking in bookings)
                {
                    if (userNameDTO.UserName == booking.UserName)
                    {
                        CancelledBookingsList.Add(booking);
                    }

                }
                if (CancelledBookingsList.Count > 0) return CancelledBookingsList;
                else throw new NoCancelledBookingsException();
            }
            throw new NoBookingsAvailableException();
        }

    }
}
