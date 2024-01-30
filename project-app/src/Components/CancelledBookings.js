import React, { useState, useEffect } from "react";

function CancelledBookings() {
  const [bookingList, setBookingList] = useState([]);
  const [searchPerformed, setSearchPerformed] = useState(false);
  const userName = localStorage.getItem("thisUserName");

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('http://localhost:5041/api/Booking/cancelledbookings', {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            // 'Authorization': 'Bearer ' + localStorage.getItem("token")
          },
          body: JSON.stringify({
            userName: userName, // Use the variable here
          }),
        });

        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json();
        console.log('Server Response:', data);
        setBookingList(data);
        setSearchPerformed(true); // Set searchPerformed to true after successful fetch
      } catch (error) {
        console.error('Error fetching booked seats:', error);
      }
    };

    fetchData();
  }, [userName]); // Include userName as a dependency

  return (
    <div>
      {searchPerformed && (
        <div>
          <h2 className="list-heading">Cancelled Bookings</h2>
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
                  <th>Cancelled Date</th>
                  
                </tr>
              </thead>
              <tbody>
                {bookingList.map((booking, index) => (
                  <tr key={index}>
                    <td>{index + 1}</td>
                    <td>{booking.bookingId}</td>
                    <td>{booking.userName}</td>
                    <td>{booking.busId}</td>
                    <td>{booking.date}</td>
                    <td>{booking.totalFare}</td>
                    <td>{booking.cancelledDate}</td>
                    
                  </tr>
                ))}
              </tbody>
            </table>
          </center>
        </div>
      )}
    </div>
  );
}

export default CancelledBookings;
