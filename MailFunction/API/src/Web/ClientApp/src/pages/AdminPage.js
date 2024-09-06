import React, { useState, useEffect } from 'react';
import apiClient from '../configs/ApiConfig';
import { saveAs } from 'file-saver';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';

function AdminPage() {
  const [clients, setClients] = useState([]);
  const [templates, setTemplates] = useState([]);
  const [selectedTemplates, setSelectedTemplates] = useState({});
  const [marketingData, setMarketingData] = useState({});
  const [editedTemplates, setEditedTemplates] = useState({});
  const [showModal, setShowModal] = useState(false);
  const [currentClientId, setCurrentClientId] = useState(null);
  const [message, setMessage] = useState('');
  const [selectedFile, setSelectedFile] = useState(null);
  const [isValidate, setIsValidate] = useState(false);

  // Fetch clients and templates from the API
  useEffect(() => {
    const fetchData = async () => {
      try {
        const clientsResponse = await apiClient.get('/client');
        const templatesResponse = await apiClient.get('/template');
        setClients(clientsResponse.data);
        setTemplates(templatesResponse.data);
      } catch (error) {
        console.error(error);
        setMessage('Error fetching data.');
      }
    };
    fetchData();
  }, []);

  // Handle dropdown change for each client
  const handleTemplateChange = (clientId, templateId) => {
    setSelectedTemplates((prevSelectedTemplates) => ({
      ...prevSelectedTemplates,
      [clientId]: templateId,
    }));
  };

  // Open the modal to edit marketingData
  const handleEditTemplate = (clientId) => {
    const templateId = selectedTemplates[clientId];
    const template = templates.find((t) => t.id === parseInt(templateId, 10));

    if (template) {
      setCurrentClientId(clientId);
      setMarketingData(template.marketingData);
      setShowModal(true);
    }
  };

  // Handle saving marketing data from modal
  const handleSaveMarketingData = () => {
    setEditedTemplates((prev) => ({
      ...prev,
      [currentClientId]: marketingData,
    }));
    setShowModal(false);
  };

  const handleFileChange = (event) => {
    setSelectedFile(event.target.files[0]);
  };

  // Delete client by ID
  const handleDeleteClient = async (clientId) => {
    try {
      await apiClient.delete(`/client/${clientId}`);
      setClients(clients.filter(client => client.id !== clientId));
      setMessage('Client deleted successfully.');
    } catch (error) {
      console.error(error);
      setMessage('Error deleting client.');
    }
  };

  const handleTriggerQueue = async (clientId) => {
    const templateId = selectedTemplates[clientId];
    const template = templates.find((t) => t.id === parseInt(templateId, 10));

    const updatedMarketingData = editedTemplates[clientId] || template.marketingData;

    const payload = {
      To: clients.find(client => client.id === clientId).emailAddress,
      MarketingData: updatedMarketingData,  // Example Title, can come from UI or logic
    };

    try {
      await apiClient.post(`/client/send-email`, payload);
      setMessage(`Triggered action for client ID: ${clientId}`);
    } catch (error) {
      console.error(error);
      setMessage('Error triggering action.');
    }
  };

  // Generate the XML file based on the selected templates and edited marketing data
  const generateXmlFile = () => {
    let xml = `<Clients>\n`;

    clients.forEach((client) => {
      const templateId = selectedTemplates[client.id];
      const template = templates.find((t) => t.id === parseInt(templateId, 10));
      const updatedMarketingData = editedTemplates[client.id] || template?.marketingData;

      if (template) {
        xml += `
    <Client ID="${client.id}">
        <Template Id="${template.id}">
            <Name>${template.name}</Name>
            <MarketingData>${updatedMarketingData}</MarketingData>
        </Template>
    </Client>`;
      }
    });

    xml += `\n</Clients>`;

    const blob = new Blob([xml], { type: 'application/xml' });
    saveAs(blob, 'clients_templates.xml');
  };

  const handleUploadFile = async () => {
    if (!selectedFile) {
      setMessage('Please select an XML file to upload.');
      return;
    }

    const formData = new FormData();
    formData.append('file', selectedFile);
    formData.append('validate', isValidate);  // Pass checkbox value

    try {
      await apiClient.post('/client/upload-xml', formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      setMessage('File uploaded successfully.');
    } catch (error) {
      console.error(error);
      setMessage('Error uploading file.');
    }
  };

  const handleCheckboxChange = (event) => {
    setIsValidate(event.target.checked);
  };

  return (
    <div className="container mt-5">
      <h2>Admin Page - Assign Templates to Clients</h2>
      <table className="table table-bordered">
        <thead>
        <tr>
          <th>Client ID</th>
          <th>Client Email</th>
          <th>Select Template</th>
          <th>Actions</th>
        </tr>
        </thead>
        <tbody>
        {clients.map((client) => (
          <tr key={client.id}>
            <td>{client.id}</td>
            <td>{client.emailAddress}</td>
            <td>
              <select
                className="form-control"
                value={selectedTemplates[client.id] || ''}
                onChange={(e) =>
                  handleTemplateChange(client.id, e.target.value)
                }
              >
                <option value="">Select Template</option>
                {templates.map((template) => (
                  <option key={template.id} value={template.id}>
                    {template.name}
                  </option>
                ))}
              </select>
            </td>
            <td>
              <button className="btn btn-danger" onClick={() => handleDeleteClient(client.id)}>
                Delete
              </button>
              <button className="btn btn-secondary ml-2" onClick={() => handleEditTemplate(client.id)}>
                Edit
              </button>
              <button className="btn btn-warning ml-2" onClick={() => handleTriggerQueue(client.id)}>
                Send Email
              </button>
            </td>
          </tr>
        ))}
        </tbody>
      </table>
      <button className="btn btn-primary mt-3" onClick={generateXmlFile}>
        Generate and Download XML
      </button>

      <h3 className="mt-5">Upload XML File</h3>
      <input type="file" accept=".xml" onChange={handleFileChange}/>
      <div className="form-check mt-2">
        <input
          type="checkbox"
          className="form-check-input"
          id="validateCheckbox"
          checked={isValidate}
          onChange={handleCheckboxChange}
        />
        <label className="form-check-label" htmlFor="validateCheckbox">
          Validate XML
        </label>
      </div>
      <button className="btn btn-success mt-3" onClick={handleUploadFile}>
        Upload XML
      </button>


      {message && <p className="mt-3">{message}</p>}

      {/* Modal for editing marketing data */}
      <Modal show={showModal} onHide={() => setShowModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Edit Marketing Data</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <textarea
            className="form-control"
            rows="5"
            value={marketingData}
            onChange={(e) => setMarketingData(e.target.value)}
          ></textarea>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>
            Close
          </Button>
          <Button variant="primary" onClick={handleSaveMarketingData}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}

export default AdminPage;
