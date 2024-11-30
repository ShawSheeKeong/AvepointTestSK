import { AppBar, Toolbar, Typography, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

const Navbar = () => {
  const navigate = useNavigate();

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          Café Manager
        </Typography>
        <Button color="inherit" onClick={() => navigate("/cafes")}>
          Cafes
        </Button>
        <Button color="inherit" onClick={() => navigate("/employees")}>
          Employees
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
