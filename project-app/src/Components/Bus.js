function Bus(){
    var bus={
        "type":"General",
        "cost":600,
        "availableSeats":40,
        "bookedSeats":0,
        "start":"Eluru",
        "end":"Chennai"

    }
    var checkSeats=availableSeats>0?true:false;
    return(
        <div className='bus'>
            {checkSeats?
                <div>
               Seat Type : {bus.type}
                 <br/>
                  Price : {bus.cost}
                 <br/>
                 Available Seats: {bus.availableSeats}
                 <br/>
                 Booked Seats : {bus.bookedSeats}
                </div>
                :
                <div>No Buses avaialable for booking</div>}
        </div>
        );
}

export default Bus;