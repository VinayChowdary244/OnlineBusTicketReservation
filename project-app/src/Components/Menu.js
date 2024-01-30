import { Link, useNavigate } from "react-router-dom";
import './Menu.css';
import home from './home.png';
import bus from './bus.jpg';

function Menu() {
  const navigate=useNavigate();
  const role =localStorage.getItem("role");
  const logout=()=>{
    localStorage.clear();
    navigate('/UserLogin');
    window.location.reload();
  }
  return (
    <div className="container">
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="collapse navbar-collapse" id="navbarNav">
        
       {role==="User"  &&
          <ul className="navbar-nav">
            <li className="nav-item">
              <Link className="nav-link" to="/RedBus">
                <img src={home} alt="home" className="icon" />
                Home
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/UserHistory">UserHistory</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/CancelledBookings">Cancelled bookings</Link>
            </li>
            <li className="nav-item">
           <Link className="nav-link" to="/updateUser">UpdateUserDetails</Link>
         </li>
             <li className="nav-item">
              <Link className="nav-link" to="/Logout">Logout</Link>
            </li>
            
          </ul>}
        </div>
        <div className="ml-auto">
          <Link className="nav-link" to="/UserLogin">Register/Login</Link>
        </div> <div className="collapse navbar-collapse" id="navbarNav">
       
        </div>
      </nav>
    </div>
  );
}

export default Menu;

 