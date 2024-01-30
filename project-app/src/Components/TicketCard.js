import React, { useState } from 'react';
import styles from './TicketCardModule.css';
import cardImage from './cardImage.jpg';
import upiImage from './upiImage.jpg';
import { useNavigate } from 'react-router-dom';

const TicketCard = () => {
  const [paymentMethod, setPaymentMethod] = useState(null);
  const thisEmail = localStorage.getItem('email');
  const thisBus = localStorage.getItem('thisBus');
  const thisDate = localStorage.getItem('thisDate');
  const thisUserName = localStorage.getItem('thisUserName');
  const navigate = useNavigate();

  const handlePaymentMethodChange = (method) => {
    setPaymentMethod(method);
  };

  const handlePaymentSubmit = () => {
    // fetch('http://localhost:5041/api/Booking', {
    //   method: 'POST',
    //   headers: {
    //     'Accept': 'application/json',
    //     'Content-Type': 'application/json',
    //     // 'Authorization': 'Bearer ' + localStorage.getItem("token")
    //   },
    //   body: JSON.stringify({
    //     busId: thisBus,
    //     userName: thisUserName,
    //     selectedSeats: localStorage.getItem('selectedSeats'),
    //     date: thisDate,
    //     email: thisEmail,
    //     paymentMethod: paymentMethod, // Use the selected payment method directly
      //}),
   // })
      // .then((response) => {
      //   if (!response.ok) {
      //     throw new Error(`HTTP error! Status: ${response.status}/-`);
      //   }
      //   return response.json();
      // })
      // .then((data) => {
      //   console.log('Booking response from server:', data);
         alert('Booking successful.\nPlease check your Email!!');
      // })
      // .catch((error) => console.error('Error booking seats:', error));

      navigate('/Redbus');
  };

  return (
    <center>
    <div className="container">
      <h1>Select Payment Method</h1>
      <div>
        <label className="radioLabel">
          <input
            type="radio"
            name="paymentMethod"
            value="card"
            onChange={() => handlePaymentMethodChange('card')}
          />
          Card <img src={cardImage} alt="Card" className="paymentIcon" />
        </label>
        <label className="radioLabel">
          <input
            type="radio"
            name="paymentMethod"
            value="upi"
            onChange={() => handlePaymentMethodChange('upi')}
          />
          UPI <img src={upiImage} alt="UPI" className="paymentIcon" />
        </label>
      </div>
      {paymentMethod === 'card' && (
        <div>
          <h2>Enter Card Details</h2>
          <div className="cardDetailsContainer">
            <input
              type="text"
              placeholder="Card Number"
              className="textInput cardNumber"
            />
            <div className="secondLine">
              <input
                type="text"
                placeholder="Cardholder Name"
                className="textInput wideInput"
              />
              <input
                type="text"
                placeholder="Expiration Date (MM/YYYY)"
                className="textInput narrowInput"
              />
            </div>
          </div>
        </div>
      )}
      {paymentMethod === 'upi' && (
        <div>
          <h2>Enter UPI ID</h2>
          <input
            type="text"
            placeholder="UPI ID"
            className="textInput"
          />
        </div>
      )}
      <button onClick={handlePaymentSubmit} className="submitButton">
        Submit Payment
      </button>
    </div>
    </center>
  );
};

export default TicketCard;
