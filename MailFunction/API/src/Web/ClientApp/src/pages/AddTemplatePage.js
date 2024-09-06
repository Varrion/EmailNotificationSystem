import React, { useState } from 'react';
import apiClient from '../configs/ApiConfig';

function AddTemplatePage() {
  const [template, setTemplate] = useState({
    name: '',
    marketingData: '',
  });
  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setTemplate((prevTemplate) => ({
      ...prevTemplate,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await apiClient.post('/template', template);
      setMessage('Template created successfully!');
    } catch (error) {
      console.error(error);
      setMessage('Error creating template');
    }
  };

  return (
    <div className="container mt-5">
      <h2>Add New Template</h2>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Template Name</label>
          <input
            type="text"
            className="form-control"
            name="name"
            value={template.name}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label>Marketing Data</label>
          <textarea
            className="form-control"
            name="marketingData"
            value={template.marketingData}
            onChange={handleChange}
            required
          ></textarea>
        </div>
        <button type="submit" className="btn btn-primary mt-3">
          Add Template
        </button>
      </form>
      {message && <p className="mt-3">{message}</p>}
    </div>
  );
}

export default AddTemplatePage;
