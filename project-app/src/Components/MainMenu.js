import { Link, useNavigate } from "react-router-dom";
import './Menu.css';
import home from './home.png';
import bus from './bus.jpg';

function MainMenu() {
    const navigate = useNavigate();
    const role = localStorage.getItem("role");
    const logout = () => {
      localStorage.clear();
      navigate('/UserLogin');
      window.location.reload();
    };
  
    return (
      <div className="container">
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav">
              {role === "User" && (
                <>
                  <li className="nav-item">
                    <Link className="nav-link" to="/RedBus">
                      <img src={home} alt="home" className="icon" />
                      Home
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/UserHistory">
                      UserHistory
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/CancelledBookings">
                      Cancelled bookings
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/updateUser">
                      UpdateUserDetails
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/Logout" onClick={logout}>
                      Logout
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/BusTicket">
                      BusTicket
                    </Link>
                  </li>
                  
                </>
              )}


              {role === null && (<>
                <li className="nav-item">
                  <Link className="nav-link" to="/RedBus">
                    <img src={home} alt="home" className="icon" />
                    Home
                  </Link>
                </li>
                <li>
                <Link className="nav-link" to="/UserLogin">Login/Register</Link>

                </li>
                </>
              )}


              {role === "Admin" && (
                <>
                  <li className="nav-item">
                    <Link className="nav-link" to="/Buses">
                      <img src={bus} alt="bus" className="icon" />
                      Buses
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/Users">
                      Users
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/BookingList">
                      Bookings
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/addBus">
                      AddBus
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/UpdateBus">
                      UpdateBus
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/Logout" onClick={logout}>
                      Logout
                    </Link>
                  </li>
                </>
              )}
            </ul>
          </div>
        </nav>
      </div>
    );
  }
  
  export default MainMenu;
  