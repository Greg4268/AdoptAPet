const BASE_URL = "https://adoptapet-production-1bb7.up.railway.app/api";

const API_ENDPOINTS = {
  login_user: `${BASE_URL}/UserAccounts/by-credentials`,
  login_shelter: `${BASE_URL}/Shelters/by-credentials`,
  appointments: `${BASE_URL}/Appointments`,
  pets: `${BASE_URL}/Pets`,
  fav_pet: `${BASE_URL}/Favorite`,
  shelters: `${BASE_URL}/Shelters`,
  user: `${BASE_URL}/UserAccounts`,
};

export default API_ENDPOINTS;