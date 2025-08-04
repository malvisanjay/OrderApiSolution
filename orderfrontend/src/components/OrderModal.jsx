import React, { useState } from 'react';
import '../OrderModal.css';

const OrderModal = ({ order, onClose, onSave }) => {
  const [editOrder, setEditOrder] = useState({ ...order });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEditOrder(prev => ({ ...prev, [name]: value }));
  };

  const handleSave = () => {
    onSave(editOrder);
    onClose();
  };

  return (
    <div className="modal-backdrop fixed inset-0 bg-black bg-opacity-30 flex justify-center items-center z-50">
      <div className="modal-content bg-white p-6 rounded-lg w-[80%] max-h-[90%] overflow-y-auto relative">
        <button onClick={onClose} className="absolute top-2 right-2 text-red-500">X</button>
        <h2 className="text-xl font-bold mb-4">Edit Order: {order.labelNo}</h2>

        <div className="grid grid-cols-1 gap-2">
          <div>
            <label>Contact Name</label>
            <input type="text" name="contactName" value={editOrder.contactName} onChange={handleChange} className="w-full border p-2" />
          </div>
          <div>
            <label>Company Name</label>
            <input type="text" name="companyName" value={editOrder.companyName} onChange={handleChange} className="w-full border p-2" />
          </div>
          <div>
            <label>Sender Street</label>
            <input type="text" name="senderStreet" value={editOrder.senderStreet} onChange={handleChange} className="w-full border p-2" />
          </div>
          <div>
            <label>Sender City</label>
            <input type="text" name="senderCity" value={editOrder.senderCity} onChange={handleChange} className="w-full border p-2" />
          </div>
          <div>
            <label>Sender PostCode</label>
            <input type="text" name="senderPostCode" value={editOrder.senderPostCode} onChange={handleChange} className="w-full border p-2" />
          </div>
          <div>
            <label>Sender Phone</label>
            <input type="text" name="senderPhone" value={editOrder.senderPhone} onChange={handleChange} className="w-full border p-2" />
          </div>
          <div>
            <label>Receiver Street</label>
            <input type="text" name="receiverStreet" value={editOrder.receiverStreet} onChange={handleChange} className="w-full border p-2" />
          </div>
          <div>
            <label>Receiver City</label>
            <input type="text" name="receiverCity" value={editOrder.receiverCity} onChange={handleChange} className="w-full border p-2" />
          </div>
          <div>
            <label>Receiver PostCode</label>
            <input type="text" name="receiverPostCode" value={editOrder.receiverPostCode} onChange={handleChange} className="w-full border p-2" />
          </div>
          <div>
            <label>Receiver Phone</label>
            <input type="text" name="receiverPhone" value={editOrder.receiverPhone} onChange={handleChange} className="w-full border p-2" />
          </div>
        </div>
        <div className="mt-6 flex justify-end gap-2">
          <button onClick={handleSave} className="bg-green-500 text-white px-4 py-2 rounded">Save</button>
        </div>
      </div>
    </div>
  );
};

export default OrderModal;
