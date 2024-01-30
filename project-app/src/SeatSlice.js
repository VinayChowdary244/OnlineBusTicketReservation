import {createSlice} from '@reduxjs/toolkit';


const seatSlice = createSlice({
    name:"seat",
    initialState:[],
    reducers:{
        addItem:(state,action)=>{
            state.push(action.payload);
        }
    }
})

export const{addItem} = seatSlice.actions;

export default cartSlice.reducer;
