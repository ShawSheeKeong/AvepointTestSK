import React, { useState, useEffect } from 'react';
import { AgGridReact } from 'ag-grid-react';
import { Button, TextField } from '@mui/material';
import { getCafes } from '../../API/api';
import { useNavigate } from 'react-router-dom';

const CafePage = () => {
  const [cafes, setCafes] = useState([]);
  const [locationFilter, setLocationFilter] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    getCafes().then((res) => {setCafes(res.data)});
  }, []);

  const handleFilter = (e) => setLocationFilter(e.target.value);

  const filteredCafes = cafes.filter((cafe) =>
    cafe.location.toLowerCase().includes(locationFilter.toLowerCase())
  );

  return (
    <>
      <h1>Cafes</h1>
      <TextField
        label="Filter by Location"
        value={locationFilter}
        onChange={handleFilter}
      />
      <Button variant="contained" onClick={() => navigate('/add-cafe')}>
        Add New Café
      </Button>
      <div className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
        <AgGridReact
          rowData={filteredCafes}
          columnDefs={[
            { field: 'logo', cellRenderer: (params) => <img src={params.value} alt="Logo" /> },
            { field: 'name' },
            { field: 'description' },
            { field: 'employees', cellRenderer: (params) => (
              <Button onClick={() => navigate(`/employees?cafe=${params.data.id}`)}>
                View Employees
              </Button>
            ) },
            { field: 'location' },
            {
              field: 'actions',
              cellRenderer: (params) => (
                <>
                  <Button onClick={() => navigate(`/edit-cafe/${params.data.id}`)}>Edit</Button>
                  <Button onClick={() => handleDelete('café', params.data.id)}>Delete</Button>
                </>
              ),
            },
          ]}
        />
      </div>
    </>
  );
};

export default CafePage;
