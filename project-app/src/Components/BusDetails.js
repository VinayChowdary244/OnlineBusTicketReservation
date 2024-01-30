import React, { useState, useEffect } from "react";
import './Buses.css';
function BusDetails() {
  const [busList, setBusList] = useState([]);
  const [searchPerformed, setSearchPerformed] = useState(false);

  useEffect(() => {
    // Fetch buses when the component is mounted
    getBuses();
  }, []); // Empty dependency array ensures this effect runs once when mounted

  var getBuses = () => {
    fetch("http://localhost:5041/api/bus", {
      method: "GET",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
       // Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then(async (data) => {
        var myData = await data.json();
        console.log(myData);
        await setBusList(myData);
        await setSearchPerformed(true);
        var Id = data.Id;
        localStorage.setItem("Id", Id);
      })
      .catch((e) => {
        console.log(e);
      });
  };

  var checkBuses = busList.length > 0 ? true : false;

  return (
    <div>
      <center>
        <h1 className="alert alert-success">
          <center>Driver Details</center>
        </h1>
      </center>
      {searchPerformed && (
        <div>
          <center>
            <table className="table">
              <thead>
                <tr>
                  <th>S.No</th>
                  <th>BusId</th>
                  <th>Driver Name</th>
                  <th>Driver Age</th>
                  <th>Driver Phone Number</th>
                  <th>Driver Rating</th>
                  <th>Start</th>
                  <th>End</th>
                </tr>
              </thead>
              <tbody>
                {busList.map((bus, index) => (
                  <tr key={bus.busId}>
                    <td>{index + 1}</td>
                    <td>{bus.id}</td>
                    <td>{bus.driverName}</td>
                    <td>{bus.driverAge}</td>
                    <td>{bus.driverPhone}</td>
                    <td>{bus.driverRating}</td>
                    
                    <td>{bus.start}</td>
                    <td>{bus.end}</td>
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

export default BusDetails;
