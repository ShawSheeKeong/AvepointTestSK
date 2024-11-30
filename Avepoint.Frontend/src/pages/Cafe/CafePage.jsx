import React, { useState, useEffect } from "react";
import { AgGridReact } from "ag-grid-react";
import { Button, TextField } from "@mui/material";
import { getCafes, deleteEntity } from "../../API/api";
import { useNavigate } from "react-router-dom";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

const CafePage = () => {
  const [cafe, setCafe] = useState([]);
  const [searchText, setSearchText] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    fetchCafe();
  }, []);

  const fetchCafe = () => {
    getCafes().then((res) => setCafe(res.data));
  };

  const handleDelete = (id) => {
    if (window.confirm("Are you sure you want to delete this cafe?")) {
      deleteEntity("cafe", id).then(() => fetchCafe());
    }
  };

  const filteredCafe = cafe.filter((c) => {
    if (c.location.toLowerCase().includes(searchText.toLowerCase())) {
      return c;
    } else {
      return null;
    }
  });

  return (
    <div>
      <h1>Cafe</h1>
      <TextField
        label="Search by Location"
        value={searchText}
        onChange={(e) => setSearchText(e.target.value)}
        fullWidth
        margin="normal"
      />
      <Button variant="contained" onClick={() => navigate("/add-cafe")}>
        Add New Cafe
      </Button>
      <div
        className="ag-theme-alpine"
        style={{ height: 500, width: "100%", marginTop: "20px" }}
      >
        <AgGridReact
          rowData={filteredCafe}
          columnDefs={[
            {
              headerName: "Logo",
              field: "logo",
              cellRenderer: (params) => (
                <img
                  src={params.value}
                  alt="Cafe Logo"
                  style={{ width: 50, height: 40, borderRadius: "50%" }}
                />
              ),
              maxWidth: 100,
            },
            { headerName: "Name", field: "name" },
            {
              headerName: "Employees",
              field: "employees",
              cellRenderer: (params) => (
                <Button
                  variant="text"
                  style={{ color: "blue", textDecoration: "underline" }}
                  onClick={() =>
                    navigate(`/employees?cafeId=${params.data.id}`)
                  }
                >
                  {params.data.employees}
                </Button>
              ),
              flex: 1,
            },
            { headerName: "Description", field: "description" },
            { headerName: "Location", field: "location" },
            {
              headerName: "Actions",
              cellRenderer: (params) => (
                <>
                  <Button
                    variant="outlined"
                    size="small"
                    onClick={() => navigate(`/edit-cafe/${params.data.id}`)}
                  >
                    Edit
                  </Button>
                  <Button
                    variant="contained"
                    color="error"
                    size="small"
                    style={{ marginLeft: "10px" }}
                    onClick={() => handleDelete(params.data.id)}
                  >
                    Delete
                  </Button>
                </>
              ),
            },
          ]}
          defaultColDef={{ flex: 1, minWidth: 150, resizable: true }}
          pagination={true}
          paginationPageSize={10}
        />
      </div>
    </div>
  );
};

export default CafePage;
