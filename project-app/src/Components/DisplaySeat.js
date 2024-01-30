function DisplaySeat(props){

    return(
        <div>
            Bus Type : {props.bus.type}
            <br/>
            Ticket Cost: {props.bus.cost}
            <br/>
            Seat Number: {props.bus.seatNo}
            <br/>
            <button className="btn btn-primary" onClick={()=>{props.onAddClick(props.bus.seatNo)}}>Book Seat</button>
        </div>
    );
    
    }
    
    export default DisplaySeat;
    
