import { useState, useEffect } from 'react';
import './App.css';
import BusSeatSelection from './Components/BusSeatSelection';
import UpdateUser from './Components/UpdateUser';
import Seat from './Components/Seat';
import Menu from './Components/Menu';
import RegisterUser from './Components/RegisterUser';
import UserLogin from './Components/UserLogin';
import { BrowserRouter, Route, Routes ,Switch} from 'react-router-dom';
import DisplaySeat from './Components/DisplaySeat';
import Buses from './Components/Buses';
import Protected from './Protected';
import UserHistory from './Components/UserHistory';
import Users from './Components/Users';
import BookingList from './Components/BookingList';
import RedBus from './Components/RedBus';
import Logout from './Components/Logout';
import AddBus from './Components/AddBus';
import UpdateBus from './Components/UpdateBus';
import BusDetails from './Components/BusDetails';
import AdminMenu from './Components/AdminMenu';
import { AuthProvider } from './Components/AuthContext';
import CancelledBookings from './Components/CancelledBookings';
import TicketCard from './Components/TicketCard.js';
import MainMenu from './Components/MainMenu.js';
import BusTicket from './Components/BusTicket';

function App() {
  return (
    <AuthProvider>
    <BrowserRouter>
    <div>
      
      <MainMenu/>
      <Routes>
      <Route path="Buses" element={<Protected><Buses /></Protected>} />
      <Route path="addBus" element={<Protected><AddBus/></Protected>}/>
      <Route path="updateBus" element={<Protected><UpdateBus/></Protected>}/>
      <Route path="UpdateUser" element={<UpdateUser />} />
      <Route path="Users" element={<Protected><Users/></Protected>} />
      <Route path="BookingList" element={<Protected><BookingList /></Protected>} />
      <Route path="Logout" element={<Logout/>}/>
      <Route path="CancelledBookings" element={<Protected><CancelledBookings/></Protected>}/>
      <Route path='/' element={<RegisterUser />} />
        <Route path="/UserLogin" element={<UserLogin />} />
        
        <Route path="BusSeatSelection" element={<Protected><BusSeatSelection /></Protected>} />

        <Route path="UserHistory" element={<Protected><UserHistory /></Protected>} />
        <Route path="RedBus" element={<RedBus />} />
        <Route path="AdminMenu" element={<Protected><AdminMenu /></Protected>} />
        <Route path="Menu" element={<Protected><Menu /></Protected>} />
        <Route path="MainMenu" element={<Protected><MainMenu /></Protected>} />

        <Route path="Logout" element={<Logout/>}/>
        <Route path="TicketCard" element={<TicketCard/>}/>
        <Route path="BusTicket" element={<BusTicket/>}/>


        
      </Routes>
      </div>
    </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
