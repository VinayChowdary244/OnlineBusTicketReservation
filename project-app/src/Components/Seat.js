import { useState } from "react";

function Seat(props){
    
    return(
        <div>
          {props.bookSeat.length==0?<div>No Seats are Booked!!</div>:
           <div>
            <h1>Your Seats</h1> 
            {props.bookSeat.map((item)=>
               <li>{item}</li>
            )}
             </div>} 
        </div>
    )
}

export default Seat;