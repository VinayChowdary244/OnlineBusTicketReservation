

import React, { useState, useEffect } from "react";
import './BookingList.css';

function BookingList() {
  const [bookingList, setBookingList] = useState([]);
  const [searchPerformed, setSearchPerformed] = useState(false);
  const [showPastBookings, setShowPastBookings] = useState(true);
  const currentDate = new Date();

  useEffect(() => {
    // Fetch bookings when the component is mounted
    getBookings();
  }, []); 

  const getBookings = () => {
    fetch('http://localhost:5041/api/booking', {
      method: "GET",
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("token")
      }
    })
    .then(async (data) => {
      var myData = await data.json();
      await setBookingList(myData);
      await setSearchPerformed(true);
    })
    .catch((e) => {
      console.log(e);
    });
  }

  const classifyBookings = () => {
    const pastBookings = [];
    const upcomingBookings = [];

    bookingList.forEach((booking) => {
      const bookingDate = new Date(booking.date);

      if (bookingDate < currentDate) {
        pastBookings.push(booking);
      } else {
        upcomingBookings.push(booking);
      }
    });

    return { pastBookings, upcomingBookings };
  }

  const { pastBookings, upcomingBookings } = classifyBookings();

  return (
    <div>
      {/* <h1 className="alert-success"><center>Bookings</center></h1> */}

      {searchPerformed && (
        <div>
          <h2 className="list-heading">{showPastBookings ? 'Past Bookings' : 'Upcoming Bookings'}</h2>
          
          <div className="switch-buttons">
            <button onClick={() => setShowPastBookings(true)} className={showPastBookings ? 'active' : ''}>Past Bookings</button>
            <button onClick={() => setShowPastBookings(false)} className={!showPastBookings ? 'active' : ''}>Upcoming Bookings</button>
          </div>

          {showPastBookings && (
            <RenderBookings bookings={pastBookings} />
          )}
          {!showPastBookings && (
            <RenderBookings bookings={upcomingBookings} />
          )}
        </div>
      )}
    </div>
  );
}

const RenderBookings = ({ bookings }) => (
  <center>
  <table className="table">
    <thead>
      <tr>
        <th>S.No</th>
        <th>BookingId</th>
        <th>UserName</th>
        <th>BusId</th>
        <th>Date</th>
        <th>TotalCost</th>
        <th>SelectedSeats</th>
      </tr>
    </thead>
    <tbody>
      {bookings.map((booking, index) => (
        <tr key={booking.userName}>
          <td>{index + 1}</td>
          <td>{booking.bookingId}</td>
          <td>{booking.userName}</td>
          <td>{booking.busId}</td>
          <td>{booking.date}</td>
          <td>{booking.totalFare}</td>
          <td>{booking.selectedSeats.join(',')}</td>
        </tr>
      ))}
    </tbody>
  </table>
  </center>
);

export default BookingList;