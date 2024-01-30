// UserLogin.js

import React, { useState } from "react";
import axios from "axios";
import './UserLogin.css';
import { useNavigate } from 'react-router-dom';

function UserLogin() {
  const roles = ["User"];
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  var [role, setRole] = useState("");
  const [usernameError, setUsernameError] = useState("");
  const navigate = useNavigate();

  var checkUserData = () => {
    if (username === "") {
      setUsernameError("Username cannot be empty");
      return false;
    }

    if (password === "") return false;

    if (username === "Admin-Vinay" || username === "Admin-Naga") {
      role = "Admin";
    } else {
      role = "User";
    }

    return true;
  };

  const logIn = (event) => {
    event.preventDefault();
    var checkData = checkUserData();
    if (checkData === false) {
      alert("Please check your data");
      return;
    }

    axios
      .post("http://localhost:5041/api/Customer/Login", {
        username: username,
        role: role,
        password: password,
      })
      .then((userData) => {
        console.log(userData);
        alert("Successfully Logged In!!");
        localStorage.setItem("thisUserName", username);
        var token = userData.data.token;
        localStorage.setItem("token", token);
        var cost = userData.data.cost;
        localStorage.setItem("cost", cost);
        var email = userData.data.email;
        localStorage.setItem("email", email);
        var userRole = userData.data.role;
        localStorage.setItem("role", userRole);

        // if (userRole === "Admin") navigate('/AdminMenu');
        // else navigate('/MainMenu');
        navigate('/Redbus');
      })
      .catch((err) => {
        console.log(err);
      });
  };

  return (
    <div className="login-container">
      <h1 className="login-heading">Login</h1>
      <form className="login-form">
        <label className="form-label">Username</label>
        <input
          type="text"
          className="form-input"
          value={username}
          onChange={(e) => {
            setUsername(e.target.value);
          }}
        />
        <label className="error-label">{usernameError}</label>

        <label className="form-label">Password</label>
        <input
          type="password"
          className="form-input"
          value={password}
          onChange={(e) => {
            setPassword(e.target.value);
          }}
        />

        <div className="btn-container">
          <button className="btn-primary button" onClick={logIn}>
            Login
          </button>

          <button className="btn-danger button">Cancel</button>
        </div>

        <div className="signin-container">
          <p>New User? Register Here <a href="/">Register</a>.</p>
        </div>
      </form>
    </div>
  );
}

export default UserLogin;
