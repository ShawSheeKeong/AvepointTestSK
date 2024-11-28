import React, { useState, useEffect } from 'react';
import { AgGridReact } from 'ag-grid-react';
import { Button, TextField } from '@mui/material';
import { getEmployees, deleteEntity } from '../../API/api';
import { useNavigate } from 'react-router-dom';

const EmployeePage = () => {
  const [employees, setEmployees] = useState([]);
  const [searchText, setSearchText] = useState('');
  const navigate = useNavigate();

  // Fetch employees on component load
  useEffect(() => {
    fetchEmployees();
  }, []);

  const fetchEmployees = () => {
    getEmployees().then((res) => setEmployees(res.data));
  };

  const handleDelete = (id) => {
    if (window.confirm('Are you sure you want to delete this employee?')) {
      deleteEntity('employee', id).then(() => fetchEmployees());
    }
  };

  const filteredEmployees = employees.filter(
    (employee) =>
      employee.name.toLowerCase().includes(searchText.toLowerCase()) ||
      employee.email.toLowerCase().includes(searchText.toLowerCase())
  );

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
      <Button variant="contained" onClick={() => navigate('/add-employee')}>
        Add New Employee
      </Button>
      <div className="ag-theme-alpine" style={{ height: 500, width: '100%', marginTop: '20px' }}>
        <AgGridReact
          rowData={filteredEmployees}
          columnDefs={[
            { headerName: 'Employee ID', field: 'id' },
            { headerName: 'Name', field: 'name' },
            { headerName: 'Email', field: 'email' },
            { headerName: 'Phone Number', field: 'phoneNumber' },
            { headerName: 'Days Worked', field: 'daysWorked' },
            { headerName: 'Café Name', field: 'cafeName' },
            {
              headerName: 'Actions',
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
                    style={{ marginLeft: '10px' }}
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
