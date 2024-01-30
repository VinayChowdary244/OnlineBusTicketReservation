import  {useEffect} from "react";

const Logout = () => {
  

  const handleLogout = () => {
    localStorage.removeItem('thisUserName');
    localStorage.removeItem('token');

    // Navigate to the login page
  

    alert('Logout Successfully!!');
  };

  useEffect(() => {
    handleLogout();
  }, []);

  return null;
};


export default Logout;
