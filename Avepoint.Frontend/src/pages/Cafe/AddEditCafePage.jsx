import React, { useState, useEffect } from 'react';
import { TextField, Button } from '@mui/material';
import { useNavigate, useParams } from 'react-router-dom';
import { addOrUpdateCafe, getCafes } from '../../API/api';

const AddEditCafePage = () => {
  const [formData, setFormData] = useState({
    name: '',
    description: '',
    logo: '',
    location: '',
  });
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      getCafes().then((res) => {
        const cafe = res.data.find((c) => c.id === id);
        if (cafe) setFormData(cafe);
      });
    }
  }, [id]);

  const handleSubmit = () => {
    addOrUpdateCafe(formData, id).then(() => navigate('/cafes'));
  };

  return (
    <form>
      <TextField
        label="Name"
        value={formData.name}
        onChange={(e) => setFormData({ ...formData, name: e.target.value })}
      />
      {/* Additional fields for description, logo, location */}
      <Button onClick={handleSubmit}>Submit</Button>
      <Button onClick={() => navigate('/cafes')}>Cancel</Button>
    </form>
  );
};

export default AddEditCafePage;
