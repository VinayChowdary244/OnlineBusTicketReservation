

import React, { useState } from 'react';
import axios from 'axios';
import './UpdateUser.css';

function UpdateUser() {
  const [userName, setUserName] = useState('');
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');
  const [city, setCity] = useState('');
  const [pincode, setPincode] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [startError, setStartError] = useState('');
  const [searchError, setSearchError] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [searchPerformed, setSearchPerformed] = useState(false);

  const checkUpdateUser = () => {
    if (userName === '') {
      setStartError('User name cannot be empty');
      return false;
    }
    return true;
  };

  const handleSearch = (event) => {
    event.preventDefault();
    setStartError('');
    setSearchError('');

    const isValidData = checkUpdateUser();

    if (!isValidData) {
      setSearchError('Please check your data');
      return;
    }

    axios
      .put('http://localhost:5041/api/Customer/UserProfiles', {
        userName: userName,
        email: email,
        phone: phone,
        city: city,
        pincode: pincode,
      })
      .then((response) => {
        console.log(response.data);
        alert("Updated Successfully!!");
        setSearchResults(response.data);
        setSearchPerformed(true);
      })
      .catch((err) => {
        console.error(err);
        setSearchError('Error updating the user details. Please try again.');
      });
  };

  return (
    <form className="update-form">
      <h2>Update User</h2>
      <label className="form-label">Username</label>
      <input
        type="text"
        className="form-input"
        value={userName}
        onChange={(e) => setUserName(e.target.value)}
      />

      <label className="form-label">Email</label>
      <input
        type="text"
        className="form-input"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <label className="form-label">Phone Number</label>
      <input
        type="text"
        className="form-input"
        value={phone}
        onChange={(e) => setPhone(e.target.value)}
      />
      <label className="form-label">City</label>
      <input
        type="text"
        className="form-input"
        value={city}
        onChange={(e) => setCity(e.target.value)}
      />
      <label className="form-label">Pincode</label>
      <input
        type="text"
        className="form-input"
        value={pincode}
        onChange={(e) => setPincode(e.target.value)}
      />

      <br />
      {searchError && <p className="error-message">{searchError}</p>}

      <button className="btn btn-primary update-button" onClick={handleSearch}>
        Update Details
      </button>
      <button className="btn btn-danger cancel-button" onClick={() => alert('Cancelled')}>
        Cancel
      </button>
    </form>
  );
}

export default UpdateUser;
