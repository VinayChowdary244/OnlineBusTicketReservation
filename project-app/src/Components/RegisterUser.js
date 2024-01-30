// RegisterUser.js

import { useState } from "react";
import './RegisterUser.css';
import axios from "axios";
import { useNavigate } from "react-router-dom";

function RegisterUser() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [city, setCity] = useState("");
  const [pincode, setPincode] = useState("");
  const [password, setPassword] = useState("");
  const [repassword, setRePassword] = useState("");
  const [usernameError, setUsernameError] = useState("");
  const [emailError, setEmailError] = useState("");
  const [phoneError, setPhoneError] = useState("");
  const [cityError, setCityError] = useState("");
  const [pincodeError, setPincodeError] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [repasswordError, setRePasswordError] = useState("");
  const navigate = useNavigate();

  var checkUserData = () => {
    if (username === '') {
      setUsernameError("Username cannot be empty");
      return false;
    }
    if (email === '') {
      setEmailError("Email cannot be empty");
      return false;
    }
    if (phone === '') {
      setPhoneError("Phone Number cannot be empty");
      return false;
    }
    if (city === '') {
      setCityError("City cannot be empty");
      return false;
    }
    if (pincode === '') {
      setPincodeError("Pincode cannot be empty");
      return false;
    }

    if (password === '') {
      setPasswordError("Password cannot be Empty");
      return false;
    }
    if (password !== repassword) {
      setRePasswordError("Password and Repassword should be the same");
      return false;
    }

    return true;
  };

  const signUp = (event) => {
    event.preventDefault();
    var checkData = checkUserData();
    if (checkData === false) {
      alert('Please check your data');
      return;
    }

    axios.post("http://localhost:5041/api/Customer", {
      username: username,
      role: "User",
      password: password,
      email: email,
      city: city,
      phone: phone,
      pincode: pincode
    })
      .then((userData) => {
        console.log(userData);
        alert("Registration Successful!!");
      })
      .catch((err) => {
        console.log(err);
      });
  };

  return (
    <div className="register-container">
      <h2 className="register-heading">Register</h2>
      <form className="registerForm">
        <div className="label-input-container">
          <input
            type="text"
            placeholder="Username"
            className="form-control"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <label>{usernameError}</label>
        </div>

        <div className="label-input-container">
          <input
            type="email"
            placeholder="Email"
            className="form-control"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <label>{emailError}</label>
        </div>

        <div className="label-input-container">
          <input
            type="text"
            placeholder="Phone"
            className="form-control"
            value={phone}
            onChange={(e) => setPhone(e.target.value)}
          />
          <label>{phoneError}</label>
        </div>

        <div className="label-input-container">
          <input
            type="text"
            placeholder="City"
            className="form-control"
            value={city}
            onChange={(e) => setCity(e.target.value)}
          />
          <label>{cityError}</label>
        </div>

        <div className="label-input-container">
          <input
            type="text"
            placeholder="Pincode"
            className="form-control"
            value={pincode}
            onChange={(e) => setPincode(e.target.value)}
          />
          <label>{pincodeError}</label>
        </div>

        <div className="label-input-container">
          <input
            type="password"
            placeholder="Password"
            className="form-control"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <label>{passwordError}</label>
        </div>

        <div className="label-input-container">
          <input
            type="password"
            placeholder="Re-Type Password"
            className="form-control"
            value={repassword}
            onChange={(e) => setRePassword(e.target.value)}
          />
          <label>{repasswordError}</label>
        </div>

        <div className="button-container">
          <button className="btn btn-primary button" onClick={signUp}>
            Sign Up
          </button>
          <button className="btn btn-danger button"onClick={navigate('/UserLogin')}>Cancel</button>
        </div>
      </form>
    </div>
  );
}

export default RegisterUser;
