import React, { useState } from 'react';

const pageSize = 5;

const OrderList = ({ data, onRowClick, onDelete }) => {
  const [page, setPage] = useState(0);
  const [search, setSearch] = useState("");

  const filteredData = data.filter(order =>
    order.labelNo.toLowerCase().includes(search.toLowerCase()) ||
    order.contactName.toLowerCase().includes(search.toLowerCase()) ||
    order.companyName.toLowerCase().includes(search.toLowerCase())
  );
  
  const start = page * pageSize;
  const paginatedData = filteredData.slice(start, start + pageSize);

  const dateFormat = (dateString) => {
    const parts = dateString.split('-');
    const day = parseInt(parts[2], 10);
    const month = parseInt(parts[1], 10) - 1; // Month is 0-indexed in JavaScript Date
    const year = parseInt(parts[0], 10); 
    // // Create a new Date object
    return `${month < 10 ? '0'+ month : month}/${day < 10 ? '0'+ day: day}/${year}` ;  
  };

  return (
    <>
      <input
        type="text"
        placeholder="Search by Label, Contact or Company"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        className="mb-4 p-2 border rounded w-full"
      />
      <table className="w-full border">
        <thead>
          <tr>
            <th>Label No</th>
            <th>Carrier</th>
            <th>Contact Name</th>
            <th>Company Name</th>
            <th>Sender Street</th>
             <th>Sender City</th>
              <th>Sender Post Code</th>

            <th>Sender Phone</th>
              <th>Receiver Street</th>
             <th>Receiver City</th>
              <th>Receiver Post Code</th>

            <th>Receiver Phone</th>
            <th>Date Created</th>

            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {paginatedData.map((order, idx) => (
            <tr key={idx} className="hover:bg-gray-100">
              <td className="cursor-pointer" onClick={() => onRowClick(order)}>{order.labelNo}</td>
              <td>{order.carrier}</td>
              <td>{order.contactName}</td>
              <td>{order.companyName}</td>
              <td>{order.senderStreet}</td>
              <td>{order.senderCity}</td>
              <td>{order.senderPostCode}</td>
              <td>{order.senderPhone}</td>
              <td>{order.receiverStreet}</td>
              <td>{order.receiverCity}</td>
              <td>{order.receiverPostCode}</td>
              <td>{order.receiverPhone}</td>
              <td>{dateFormat(order.dateCreated)}</td>

              <td>
                <button onClick={() => onRowClick(order)} className="mr-2 text-blue-500">Edit</button>
                <button onClick={() => onDelete(order.labelNo)} className="text-red-500">Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div className="mt-4">
        <button onClick={() => setPage(p => Math.max(p - 1, 0))}>Previous</button>
        <span className="mx-2">{page + 1} / {data.length/ pageSize}</span>
        <button onClick={() => setPage(p => p + 1)}>Next</button>
      </div>
    </>
  );
};

export default OrderList;
