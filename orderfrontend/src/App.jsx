import React, { useState, useEffect } from 'react';
import OrderList from './components/OrderList';
import OrderModal from './components/OrderModal';
import './App.css';

function App() {
  const API_URL ="https://localhost:7033/api/order";
  const [orders, setOrders] = useState([]);
  const [selectedOrder, setSelectedOrder] = useState(null);
  const [loading, setLoading] = useState(true);


   useEffect(() => {
    fetch(API_URL) 
      .then(res => res.json())
      .then(data => {
        setOrders(data);
        setLoading(false);
      })
      .catch(err => {
        console.error("Failed to fetch orders:", err);
        setLoading(false);
      });
  }, []);


  const handleDelete = (label) => {
    setOrders(prev => prev.filter(order => order.labelNo !== label));
  };

  const handleSave = (updatedOrder) => {
    setOrders(prev => prev.map(order => 
      order.labelNo === updatedOrder.labelNo ? updatedOrder : order
    ));
  };
   
 return (
    <div className="App p-6">
      <h1 className="text-2xl font-bold mb-4">Order Management</h1>
      {loading ? (
        <p>Loading orders...</p>
      ) : (
        <>
          <OrderList data={orders} onRowClick={setSelectedOrder} onDelete={handleDelete} />
          {selectedOrder && (
            <OrderModal order={selectedOrder} onClose={() => setSelectedOrder(null)} onSave={handleSave} />
          )}
        </>
      )}
    </div>
  );
}

export default App;
