

import { useState } from 'react';
import './RedBus.css'; // Import the CSS file
import axios from 'axios';
import fromIcon from './fromIcon.png'; // Replace with your actual path
import toIcon from './toIcon.png'; // Replace with your actual path
import { useNavigate } from 'react-router-dom';

function RedBus() {
  const [fromLocation, setFromLocation] = useState('');
  const [toLocation, setToLocation] = useState('');
  const [selectedDate, setSelectedDate] = useState('');
  const [startError, setStartError] = useState('');
  const [searchError, setSearchError] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [searchPerformed, setSearchPerformed] = useState(false);
  const [thisBus, setThisBus] = useState(null);
  const [thisDate, setThisDate] = useState(null);
  const [type, setType] = useState(null);
  const [startTime, setStarttime] = useState(null);

  

  const checkUserData = () => {
    if (fromLocation === '') {
      setStartError('Start location cannot be empty');
      return false;
    }
    return true;
  };

  const swapLocations = (event) => {
    event.preventDefault();
    const temp = fromLocation;
    setFromLocation(toLocation);
    setToLocation(temp);
  };

  const navigate = useNavigate();

  const handleBook = (id, selectedDate, cost,type,startTime) => {
    setThisBus(id);
    setThisDate(selectedDate);
  
    localStorage.setItem('cost', cost);
    localStorage.setItem('thisBus', id);
    localStorage.setItem('thisDate', selectedDate);
    localStorage.setItem('type', type);
    localStorage.setItem('startTime', startTime);

  
    
    navigate('/BusSeatSelection');
  };

  const handleSearch = (event) => {
    event.preventDefault();
    setStartError('');
    setSearchError('');

    const isValidData = checkUserData();

    if (!isValidData) {
      setSearchError('Please check your data');
      return;
    }
    const selectedDateObject = new Date(selectedDate);
    const currentDate = new Date();
    localStorage.setItem('toLocation', toLocation);
    localStorage.setItem('fromLocation', fromLocation);


    var yesterday = new Date(currentDate);
    yesterday.setDate(currentDate.getDate() - 1);
    
    if (selectedDateObject < yesterday) {
      alert('Please enter a valid date (today or later).');
      return;
    }

    axios
      .post('http://localhost:5041/api/Customer/BusSearch', {
        start: fromLocation,
        end: toLocation,
        date: selectedDate,
       
      })
      .then((response) => {
        console.log(response.data);
        setSearchResults(response.data);
        setSearchPerformed(true);
      })
      .catch((err) => {
        console.error(err);
        setSearchError('Error searching buses. Please try again.');
      });
  };

  return (
    <div>
      <center>
      <h1>Search Your Bus</h1>
      </center>
      {!searchPerformed && (
        <form className="redbus-search">
          <div className="location-container">
            <div className="location-input from">
              <img src={fromIcon} alt="From Icon" className="icon" />
              <label htmlFor="src" className="label-text">
                From
              </label>
              <input
                type="text"
                id="src"
                className="input-field"
                placeholder="Enter origin location"
                value={fromLocation}
                onChange={(e) => setFromLocation(e.target.value)}
              />
            </div>
            <button className="swap-icon" onClick={swapLocations}>
              &#8596;
            </button>
            <div className="location-input to">
              <img src={toIcon} alt="To Icon" className="icon" />
              <label htmlFor="dest" className="label-text">
                To
              </label>
              <input
                type="text"
                id="dest"
                className="input-field"
                placeholder="Enter destination location"
                value={toLocation}
                onChange={(e) => setToLocation(e.target.value)}
              />
            </div> 
          </div>
          <div className="date-container">
            <div className="calendar-icon">
              <input
                type="date"
                id="calendar"
                className="date-input"
                value={selectedDate}
                onChange={(e) => setSelectedDate(e.target.value)}
              />
            </div>
          </div>
          <button className="search-button" onClick={handleSearch}>
            Search
          </button>
        </form>
      )}

      {searchPerformed && (
        <div>
          <h2>Available Busses</h2>
          <br />
          <center>
            <table className="table">
              <thead>
                <tr>
                  <th>S.No</th>
                  <th>BusId</th>
                  <th>Type</th>
                  <th>From</th>
                  <th>To</th>
                  <th>Date</th>
                
                  <th>Fare</th>
                  <th>
                    <center>Book now</center>
                  </th>
                </tr>
              </thead>
              <tbody>
                {searchResults.map((bus, index) => (
                  <tr key={bus.busId}>
                    <td>{index + 1}</td>
                    <td>{bus.id}</td>
                    <td>{bus.type}</td>
                    <td>{bus.start}</td>
                    <td>{bus.end}</td>
                    <td>{selectedDate}</td>
                    
                    <td>₹{bus.cost}/-</td>
                    <td>
                      <center>
                        <button
                          className="book-button"
                          onClick={() => handleBook(bus.id, selectedDate,bus.cost,bus.type,bus.startTime)}
                        >
                          Book
                        </button>
                      </center>
                    </td>
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

export default RedBus;