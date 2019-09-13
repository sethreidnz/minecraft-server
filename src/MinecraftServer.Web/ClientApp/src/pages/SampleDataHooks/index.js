// node_modules
import React, { useState, useEffect } from "react";

// local imports
import { getForecasts } from '../../api/sampleData';

export const SampleDataHooks = () => {
  const [forecasts, updateForecasts] = useState([]);
  const [loading, updateLoading] = useState(true);

  const fetchData = async () => {
    const forecasts = await getForecasts();
    updateForecasts(forecasts);
    updateLoading(false);
  }

  useEffect(() => {
    fetchData();
  }, []);


  if(loading) return <p><em>Loading...</em></p>;
  return (
    <div>
      <h1>Weather forecast</h1>
      <p>This component demonstrates fetching data from the server.</p>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast => (
            <tr key={forecast.dateFormatted}>
              <td>{forecast.dateFormatted}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};