import React, { useEffect, useState } from 'react';
import './BusTicket.css';
import { useLocation } from 'react-router-dom';

const BusTicket = () => {
  const location = useLocation();
  const [ticketDetails, setTicketDetails] = useState({
    thisBus: localStorage.getItem('thisBus') || '',
    thisDate: localStorage.getItem('thisDate') || '',
    type: 'Standard', // You can change this based on your logic
    selectedSeats: JSON.parse(localStorage.getItem('selectedSeats') || '[]'),
    toLocation: localStorage.getItem('toLocation') || '',
    fromLocation: localStorage.getItem('fromLocation') || '',
    startTime: '12:00 PM', // You can change this based on your logic
    cost: localStorage.getItem('cost') || '',
  });

  useEffect(() => {
    // Fetch additional details from the server if needed
    // For example, you might want to fetch the bus type, start time, etc.
    // Update the ticketDetails state accordingly
  }, []);

  return (
    <div className="ticket-container">
      <div className="ticket-header">
        <h1>Bus Ticket</h1>
      </div>
      <div className="ticket-details">
        <div className="detail-row">
          <span>Bus ID:</span>
          <span>{ticketDetails.thisBus}</span>
        </div>
        <div className="detail-row">
          <span>Date:</span>
          <span>{ticketDetails.thisDate}</span>
        </div>
        <div className="detail-row">
          <span>Type:</span>
          <span>{ticketDetails.type}</span>
        </div>
        <div className="detail-row">
          <span>Selected Seats:</span>
          <span>{ticketDetails.selectedSeats.join(', ')}</span>
        </div>
        <div className="detail-row">
          <span>To:</span>
          <span>{ticketDetails.toLocation}</span>
        </div>
        <div className="detail-row">
          <span>From:</span>
          <span>{ticketDetails.fromLocation}</span>
        </div>
        <div className="detail-row">
          <span>Start Time:</span>
          <span>{ticketDetails.startTime}</span>
        </div>
        <div className="detail-row">
          <span>Cost:</span>
          <span>₹{ticketDetails.cost}</span>
        </div>
      </div>
      <div className="ticket-download">
        <a
          href="#"
          download={`BusTicket_${ticketDetails.thisBus}_${ticketDetails.thisDate}.pdf`}
        >
          Download Ticket
        </a>
      </div>
    </div>
  );
};

export default BusTicket;
