
function BusListing(props){


    return(
        <div>
            {props.buses.map((bus)=>
                    <div key={bus.seatNo} className="alert alert-primary">
                        Bus Type: {bus.type}
                        <br/>
                        Ticket Cost : {bus.cost}
                        <br/>
                        Seat Number: {bus.seatNo}
                        <br/>
                        <button className="btn btn-primary" onClick={()=>{props.onAddClick(bus.seatNo)}}>Book Seat</button>
                </div>)}
        </div>
    )
}

export default BusListing;