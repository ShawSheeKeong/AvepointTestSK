import React, { useState, useEffect } from 'react';
import { TextField, Button, RadioGroup, FormControlLabel, Radio, MenuItem, Select, InputLabel, FormControl } from '@mui/material';
import { useNavigate, useParams } from 'react-router-dom';
import { addOrUpdateEmployee, getEmployees, getCafes } from "../../API/api";

const AddEditEmployeePage = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    phoneNumber: '',
    gender: '',
    assignedCafe: '',
  });
  const [cafes, setCafes] = useState([]);
  const { id } = useParams();
  const navigate = useNavigate();

  // Fetch existing data if editing an employee
  useEffect(() => {
    if (id) {
      getEmployees().then((res) => {
        const employee = res.data.find((emp) => emp.id === id);
        if (employee) setFormData(employee);
      });
    }
  }, [id]);

  // Fetch cafes for dropdown
  useEffect(() => {
    getCafes().then((res) => setCafes(res.data));
  }, []);

  const handleSubmit = () => {
    addOrUpdateEmployee(formData, id).then(() => navigate('/employees'));
  };

  const validatePhoneNumber = (phone) => {
    const regex = /^[89]\d{7}$/; // Singapore phone number validation
    return regex.test(phone);
  };

  const handleInputChange = (field, value) => {
    setFormData((prevData) => ({
      ...prevData,
      [field]: value,
    }));
  };

  return (
    <form>
      <h1>{id ? 'Edit Employee' : 'Add Employee'}</h1>
      <TextField
        label="Name"
        value={formData.name}
        onChange={(e) => handleInputChange('name', e.target.value)}
        required
        inputProps={{ minLength: 6, maxLength: 10 }}
        helperText="Name should be between 6 and 10 characters."
        fullWidth
        margin="normal"
      />

      <TextField
        label="Email Address"
        type="email"
        value={formData.email}
        onChange={(e) => handleInputChange('email', e.target.value)}
        required
        helperText="Enter a valid email address."
        fullWidth
        margin="normal"
      />

      <TextField
        label="Phone Number"
        value={formData.phoneNumber}
        onChange={(e) => handleInputChange('phoneNumber', e.target.value)}
        required
        error={!validatePhoneNumber(formData.phoneNumber) && formData.phoneNumber !== ''}
        helperText="Must be an 8-digit number starting with 8 or 9."
        fullWidth
        margin="normal"
      />

      <FormControl component="fieldset" margin="normal">
        <RadioGroup
          row
          value={formData.gender}
          onChange={(e) => handleInputChange('gender', e.target.value)}
        >
          <FormControlLabel value="male" control={<Radio />} label="Male" />
          <FormControlLabel value="female" control={<Radio />} label="Female" />
        </RadioGroup>
      </FormControl>

      <FormControl fullWidth margin="normal">
        <InputLabel id="cafe-select-label">Assigned Café</InputLabel>
        <Select
          labelId="cafe-select-label"
          value={formData.assignedCafe}
          onChange={(e) => handleInputChange('assignedCafe', e.target.value)}
        >
          {cafes.map((cafe) => (
            <MenuItem key={cafe.id} value={cafe.id}>
              {cafe.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>

      <div style={{ marginTop: '20px' }}>
        <Button variant="contained" onClick={handleSubmit}>
          Submit
        </Button>
        <Button variant="outlined" onClick={() => navigate('/employees')} style={{ marginLeft: '10px' }}>
          Cancel
        </Button>
      </div>
    </form>
  );
};

export default AddEditEmployeePage;
