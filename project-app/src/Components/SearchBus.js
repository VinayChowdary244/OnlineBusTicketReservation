import { useState } from "react";
import axios from "axios";
import "./SearchBus.css";

function SearchBus() {
  const [start, setStart] = useState("");
  const [end, setEnd] = useState("");
  const [selectedDate, setSelectedDate] = useState("");
  const [startError, setStartError] = useState("");
  const [searchError, setSearchError] = useState("");
  const [searchResults, setSearchResults] = useState([]);
  const [searchPerformed, setSearchPerformed] = useState(false);

  const checkUserData = () => {
    if (start === "") {
      setStartError("Start location cannot be empty");
      return false;
    }

    if (end === "") {
      setStartError("End location cannot be empty");
      return false;
    }

    if (selectedDate === "") {
      setStartError("Please select a date");
      return false;
    }

    return true;
  };

  const handleSearch = (event) => {
    event.preventDefault();
    setStartError("");
    setSearchError("");

    const isValidData = checkUserData();

    if (!isValidData) {
      setSearchError("Please check your data");
      return;
    }

    axios
      .post("http://localhost:5041/api/Customer/BusSearch", {
        start: start,
        end: end,
        date: selectedDate,
      })
      .then((response) => {
        console.log(response.data);
        setSearchResults(response.data);
        setSearchPerformed(true);
      })
      .catch((err) => {
        console.error(err);
        setSearchError("Error searching buses. Please try again.");
      });
  };

  return (
    <div>
      {!searchPerformed && (
        <form className="StartEndInput">
          <label className="Start_End_Form">From</label>
          <input
            type="text"
            className="form-control"
            value={start}
            onChange={(e) => setStart(e.target.value)}
          />
          {startError && <p className="error-message">{startError}</p>}

          <label className="Start_End_Form">To</label>
          <input
            type="text"
            className="form-control"
            value={end}
            onChange={(e) => setEnd(e.target.value)}
          />
          {searchError && <p className="error-message">{searchError}</p>}

          <label className="Start_End_Form">Date</label>
          <input
            type="date"
            className="form-control"
            value={selectedDate}
            onChange={(e) => setSelectedDate(e.target.value)}
          />
          {searchError && <p className="error-message">{searchError}</p>}

          <button className="btn btn-primary button" onClick={handleSearch}>
            Search Bus
          </button>
        </form>
      )}

      {searchPerformed && (
        <div>
          <h2>Search Results</h2>
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
              </tr>
            </thead>
            <tbody>
              {searchResults.map((bus,index) => (
                <tr key={bus.busId}>
                  <td>{index + 1}</td>
                  <td>{bus.id}</td>
                  <td>{bus.type}</td>
                  <td>{bus.start}</td>
                  <td>{bus.end}</td>
                  <td>{selectedDate}</td>
                  <td>{bus.cost}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}

export default SearchBus;
