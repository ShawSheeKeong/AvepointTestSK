import React, { useState, useEffect } from "react";
import { Box, Button, TextField, Typography } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { addOrUpdateCafe, getCafes } from "../../API/api";

const AddEditCafePage = () => {
  const [formData, setFormData] = useState({
    name: "",
    description: "",
    logo: "",
    location: "",
    createdBy: navigator.userAgent,
    lastUpdatedBy: navigator.userAgent,
  });
  const [isSubmitting, setIsSubmitting] = useState(false);
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      getCafes()
        .then((res) => {
          res.data.filter((x) => {
            if (x.id == id) {
              setFormData({
                name: x.name,
                description: x.description,
                logo: x.logo,
                location: x.location,
                createdBy: navigator.userAgent,
                lastUpdatedBy: navigator.userAgent,
              });
            }
          });
        })
        .catch((err) => console.error("Error fetching cafe:", err));
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      if (file.size > 2 * 1024 * 1024) {
        alert("File size must be less than 2MB");
        return;
      }
      const reader = new FileReader();
      reader.onload = () => {
        setFormData({ ...formData, logo: reader.result });
      };
      reader.readAsDataURL(file);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setIsSubmitting(true);

    const apiCall = id
      ? addOrUpdateCafe(formData, id)
      : addOrUpdateCafe(formData);

    apiCall
      .then(() => {
        alert(`Cafe ${id ? "updated" : "added"} successfully!`);
        navigate("/cafes");
      })
      .catch((err) => {
        console.error("Error saving cafe:", err);
        alert("Failed to save cafe. Please try again.");
      })
      .finally(() => setIsSubmitting(false));
  };

  return (
    <>
      <Typography variant="h4" gutterBottom>
        {id ? "Edit Cafe" : "Add New Cafe"}
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Name"
          name="name"
          value={formData.name || ""}
          onChange={handleChange}
          variant="outlined"
          fullWidth
          margin="normal"
          inputProps={{ minLength: 6, maxLength: 10 }}
          helperText="Name should be between 6 and 10 characters."
          required
        />
        <TextField
          label="Description"
          name="description"
          value={formData.description || ""}
          onChange={handleChange}
          variant="outlined"
          fullWidth
          margin="normal"
          inputProps={{ maxLength: 256 }}
          required
        />
        <Button
          variant="contained"
          component="label"
          style={{ marginTop: "16px" }}
        >
          Upload Logo
          <input
            type="file"
            accept="image/*"
            hidden
            onChange={handleFileChange}
          />
        </Button>
        {formData.logo && (
          <img
            src={formData.logo || ""}
            alt="Logo Preview"
            style={{ marginTop: 16, maxWidth: "100%", height: "100px" }}
          />
        )}
        <TextField
          label="Location"
          name="location"
          value={formData.location || ""}
          onChange={handleChange}
          variant="outlined"
          fullWidth
          margin="normal"
          required
        />

        <Box sx={{ marginTop: 4, display: "flex", gap: 2 }}>
          <Button
            type="submit"
            variant="contained"
            color="primary"
            disabled={isSubmitting}
          >
            {isSubmitting ? "Saving..." : "Submit"}
          </Button>
          <Button
            variant="outlined"
            color="secondary"
            onClick={() => navigate("/cafes")}
          >
            Cancel
          </Button>
        </Box>
      </form>
    </>
  );
};

export default AddEditCafePage;
