import axios from "axios";

export const BASE_URL = "https://localhost:7178/";

export const createAPIEndpoint = (endpoint) => {
  let url = BASE_URL + "api/" + endpoint + "/";
  return {
    fetch: () => axios.get(url),
    fetchById: (id) => axios.get(url + id),
    post: (newRecord) => axios.post(url, newRecord),
    put: (id, updatedRecord) => axios.put(url + id, updatedRecord),
    delete: (id) => axios.delete(url + id),
  };
};

export const ENDPOINTS = {
  // authemtication
  login: "Auth/Login",
  registerAdmin: "Auth/RegisterAdmin",
  registerClient: "Auth/RegisterClient",
  registerUser: "Auth/RegisterUser",

  admins: "admins",
  EditAdmin: "admins/edit",
  DeleteAdmin: "admins/delete",
  ViewAdmin: "admins/view",

  clients: "clients",
  createClient: "clients/create",
  EditClient: "clients/edit",
  DeleteClient: "clients/delete",
  ViewClient: "clients/view",

  users: "users",
  createUser: "users/create",
  EditUser: "users/edit",
  DeleteUser: "users/delete",
  ViewUser: "users/view",

  categories: "categories",
  editCategory: "categories/edit",
  deleteCategory: "categories/delete",
  viewCategory: "categories/view",
  createCategory: "categories/create",

  applications: "applications",
  editApplication: "applications/edit",
  deleteApplication: "applications/delete",
  viewApplication: "applications/view",
  createApplication: "applications/create",

  questions: "questions",
  editQuestion: "questions/edit",
  deleteQuestion: "questions/delete",
  viewQuestion: "questions/view",
  createQuestion: "questions/create",
};
