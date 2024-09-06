import React, { useState } from 'react';
import apiClient from "../configs/ApiConfig";

function AddClientPage() {
  const [client, setClient] = useState({
    emailAddress: '',
  });
  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setClient((prevClient) => ({
      ...prevClient,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await apiClient.post('/client', client);
      setMessage('Client created successfully!');
    } catch (error) {
      console.error(error);
      setMessage('Error creating client');
    }
  };

  return (
    <div className="container mt-5">
      <h2>Add New Client</h2>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Email Address</label>
          <input
            type="email"
            className="form-control"
            name="emailAddress"
            value={client.emailAddress}
            onChange={handleChange}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary mt-3">
          Add Client
        </button>
      </form>
      {message && <p className="mt-3">{message}</p>}
    </div>
  );
}

export default AddClientPage;
