const endpoint = "/api/virtualmachine";

export const getVmStatus = async () => {
  const response = await fetch(`${endpoint}/details`);
  return await response.json();
};

export const startVm = async () => {
  const response = await fetch(`${endpoint}/start`, {
    method: 'POST'
  });
  debugger;
  return await response.json();
};

export const stopVm = async () => {
  const response = await fetch(`${endpoint}/stop`, {
    method: 'POST'
  });
  return await response.json();
};