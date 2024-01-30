// AddBus.js

import React, { useState } from "react";
import './AddBus.css'
import { redirect } from "react-router-dom";

function AddBus() {
  const [type, setType] = useState("");
  const [cost, setCost] = useState();
  const [start, setStart] = useState("");
  const [end, setEnd] = useState("");
  const [driverName, setDriverName] = useState("");
  const [driverPhone, setDriverPhone] = useState("");
  const [driverAge, setDriverAge] = useState("");
  const [driverRating, setDriverRating] = useState("");
  const [startTime, setStartTime] = useState("");
  const [duration, setDuration] = useState("");

  var bus;

  var clickAdd = () => {
    bus = {
      "type": type,
      "availableSeats": 37,
      "bookedSeats": 0,
      "cost": cost,
      "start": start,
      "end": end,
      "startTime": startTime,
      "duration": duration,
      "driverName": driverName,
      "driverPhone": driverPhone,
      "driverAge": driverAge,
      "driverRating": driverRating
    };

    fetch('http://localhost:5041/api/bus', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("token")
      },
      body: JSON.stringify(bus)
    }).then(
      () => {
        console.log(bus);
        alert('Bus Added');
      }
    ).catch((e) => {
      console.log(e)
    });
  }

  return (
    <div className="add">
      <h2>Add Bus</h2>

      <label className="form-control" for="pname"><b>Bus Type</b></label>
      <input id="pname" type="text" className="form-control" value={type} onChange={(e) => { setType(e.target.value) }} />
      <label className="form-control" for="pname"><b>Start</b></label>
      <input id="pname" type="text" className="form-control" value={start} onChange={(e) => { setStart(e.target.value) }} />
      <label className="form-control" for="pname"><b>End</b></label>
      <input id="pname" type="text" className="form-control" value={end} onChange={(e) => { setEnd(e.target.value) }} />
      <label className="form-control" for="pprice"><b>Ticket Cost</b></label>
      <input id="pprice" type="number" className="form-control" value={cost} onChange={(e) => { setCost(e.target.value) }} />
      <label className="form-control" for="pprice"><b>Start Time </b></label>
      <input id="pprice" type="time" className="form-control" value={startTime} onChange={(e) => { setStartTime(e.target.value) }} />
      <label className="form-control" for="pprice"><b>Journey Duration</b></label>
      <input id="pprice" type="number" className="form-control" value={duration} onChange={(e) => { setDuration(e.target.value) }} />
      <label className="form-control" for="pname"><b>DriverName</b></label>
      <input id="pname" type="text" className="form-control" value={driverName} onChange={(e) => { setDriverName(e.target.value) }} />
      <label className="form-control" for="pname"><b>DriverPhone</b></label>
      <input id="pname" type="text" className="form-control" value={driverPhone} onChange={(e) => { setDriverPhone(e.target.value) }} />
      <label className="form-control" for="pname"><b>DriverAge</b></label>
      <input id="pname" type="text" className="form-control" value={driverAge} onChange={(e) => { setDriverAge(e.target.value) }} />
      <label className="form-control" for="pname"><b>DriverRating</b></label>
      <input id="pname" type="text" className="form-control" value={driverRating} onChange={(e) => { setDriverRating(e.target.value) }} />

      <button onClick={clickAdd} className="btn btn-primary"><b>Add Bus</b></button>
    </div>
  );
}

export default AddBus;
