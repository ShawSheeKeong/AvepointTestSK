import React, { useState, useEffect } from "react";
import { AgGridReact } from "ag-grid-react";
import { Button, TextField } from "@mui/material";
import { getEmployees, deleteEntity } from "../../Api/api";
import { useNavigate, useSearchParams } from "react-router-dom";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

const EmployeePage = () => {
  const [employees, setEmployees] = useState([]);
  const [searchText, setSearchText] = useState("");
  const [searchParams] = useSearchParams();
  const cafeId = searchParams.get("cafeId");
  const navigate = useNavigate();

  useEffect(() => {
    fetchEmployees(cafeId);
  }, [cafeId]);

  const fetchEmployees = (cafeId) => {
    if (cafeId) {
      getEmployees().then((res) =>
        setEmployees(res.data.filter((x) => x.cafeId == cafeId))
      );
    } else {
      getEmployees().then((res) => setEmployees(res.data));
    }
  };

  const handleDelete = (id) => {
    if (window.confirm("Are you sure you want to delete this employee?")) {
      deleteEntity("employee", id).then(() => fetchEmployees());
    }
  };

  const filteredEmployees = employees.filter((employee) => {
    if (
      employee.name.toLowerCase().includes(searchText.toLowerCase()) ||
      employee.emailAddress.toLowerCase().includes(searchText.toLowerCase())
    ) {
      return employee;
    } else {
      return null;
    }
  });

  return (
    <div>
      <h1>Employees</h1>
      <TextField
        label="Search by Name or Email"
        value={searchText}
        onChange={(e) => setSearchText(e.target.value)}
        fullWidth
        margin="normal"
      />
      <Button variant="contained" onClick={() => navigate("/add-employee")}>
        Add New Employee
      </Button>
      <div
        className="ag-theme-alpine"
        style={{ height: 500, width: "100%", marginTop: "20px" }}
      >
        <AgGridReact
          rowData={filteredEmployees}
          columnDefs={[
            { headerName: "Employee ID", field: "id" },
            { headerName: "Name", field: "name" },
            { headerName: "Email Address", field: "emailAddress" },
            { headerName: "Phone Number", field: "phoneNumber" },
            { headerName: "Days Worked in the café", field: "daysWorked" },
            { headerName: "Café Name", field: "cafeName" },
            {
              headerName: "Actions",
              cellRenderer: (params) => (
                <>
                  <Button
                    variant="outlined"
                    size="small"
                    onClick={() => navigate(`/edit-employee/${params.data.id}`)}
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

export default EmployeePage;
