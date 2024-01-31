import React, { useState, useEffect } from 'react';
import './BusSeatSelection.css';
import { useNavigate } from 'react-router-dom';
import { useLocation } from 'react-router-dom';

const BusSeatSelection = () => {
  const totalRows = 8;
  const seatsPerRow = 4;
  const seatPrice =localStorage.getItem("thisCost");
  const [selectedSeats, setSelectedSeats] = useState([]);
  const [bookedSeats, setBookedSeats] = useState([]);
  const [isBooked, setIsBooked] = useState(false);
  const thisEmail=localStorage.getItem('email');
  const thisBus = localStorage.getItem('thisBus');
  const thisDate = localStorage.getItem('thisDate');
  const thisUserName = localStorage.getItem('thisUserName');
  const thisToken = localStorage.getItem('thisToken');
  const cost=localStorage.getItem("cost");

  useEffect(() => {
    fetch('http://localhost:5041/api/Booking/BookedSeats', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        busId: thisBus, 
        
        date:thisDate
      }),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json();
      })
      .then((data) => {
        console.log('Server Response:', data);
        setBookedSeats(data.bookedSeats || []);
      })
      .catch((error) => console.error('Error fetching booked seats:', error));
  }, []);

  // Function to check if a seat is booked
  const isSeatBooked = (seatNumber) => {
    return bookedSeats.includes(seatNumber);
  };

  // Handle click on a seat
  const handleSeatClick = (seatNumber) => {
    // If the seat is not booked, toggle the selection status
    if (!isSeatBooked(seatNumber)) {
      if (selectedSeats.includes(seatNumber)) {
        setSelectedSeats(selectedSeats.filter((seat) => seat !== seatNumber));
      } else {
        setSelectedSeats([...selectedSeats, seatNumber]);
      }
    }
  };

  // Handle booking
  const navigate = useNavigate();
  
const handleBookClick = () => {
  // booking logic

  fetch('http://localhost:5041/api/Booking', {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("token")
    },
    
    body: JSON.stringify({
      busId:thisBus, 
      userName: thisUserName, 
      selectedSeats: selectedSeats,
      date: thisDate, 
      email:thisEmail,
    }),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}/-`);
      }
            

      return response.json();
    })
    .then((data) => {
      console.log('Booking response from server:', data);

      setSelectedSeats([]);
      localStorage.setItem('selectedSeats', JSON.stringify(selectedSeats));

      setIsBooked(true);
      navigate('/TicketCard');
      
    })
    .catch((error) => console.error('Error booking seats:', error));
};

  // Calculate total price based on selected seats
  const calculateTotalPrice = () => {
    return selectedSeats.length * cost;
  };

  return (
    <div className="comtainer-master">
    
    <div className="seat-selection-container">
      <h2>Bus Seat Selection</h2>
      <div>
    <h6>Booked Seats <span class="seat-status booked"></span></h6>

    <h6>Available Seats <span class="seat-status available"></span></h6>
    
    <h6>Selected Seats <span class="seat-status selected"></span></h6>
</div>
      <div className="bus-seats">
        {[...Array(totalRows)].map((_, rowIndex) => (
          <div key={rowIndex} className="seat-row">
            {[...Array(seatsPerRow)].map((_, seatIndex) => {
              const seatNumber = rowIndex * seatsPerRow + seatIndex + 1;
              return (
                <div
                  key={seatNumber}
                  className={`seat ${
                    selectedSeats.includes(seatNumber) ? 'selected' : isSeatBooked(seatNumber) ? 'booked' : 'available'
                  }`}
                  onClick={() => handleSeatClick(seatNumber)}
                >
                  {seatNumber}
                </div>
              );
            })}
          </div>
        ))}
        <div className="seat-row">
          {[...Array(5)].map((_, seatIndex) => {
            const seatNumber = totalRows * seatsPerRow + seatIndex + 1;
            return (
              <div
                key={seatNumber}
                className={`seat ${
                  selectedSeats.includes(seatNumber) ? 'selected' : isSeatBooked(seatNumber) ? 'booked' : 'available'
                }`}
                onClick={() => handleSeatClick(seatNumber)}
              >
                {seatNumber}
              </div>
            );
          })}
        </div>
      </div>
      <div className="total-price-and-button-container">
        <p>Selected Seats: {selectedSeats.join(', ')}</p>
        <div className="total-price-and-button">
          <p>Total Price: ₹{calculateTotalPrice()}</p>
          <button onClick={handleBookClick}>Book</button>
        </div>
      </div>
    </div>
    </div>
    
  );
};

export default BusSeatSelection;

