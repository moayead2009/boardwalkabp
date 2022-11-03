import axios from "axios";

export const BASE_URL = "https://localhost:7022/";


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
  clients: "clients",
  registerClient: "clients/register",
  loginClient: "clients/login",
  EditClient: "clients/edit",
  DeleteClient: "clients/delete",
  ViewClient: "clients/view",

  users: "users",
  registerUser: "users/register",
  loginUser: "users/login",
  EditUser: "users/edit",
  DeleteUser: "users/delete",
  ViewUser: "users/view",

  categories: "categories",
  createCategory: "categories/create",
  editCategory: "categories/edit",
  deleteCategory: "categories/delete",
  viewCategory: "categories/view",

  applications: "applications",
  createApplication: "applications/create",
  editApplication: "applications/edit",
  deleteApplication: "applications/delete",
  viewApplication: "applications/view",

  questions: "questions",
  createQuestion: "questions/create",
  editQuestion: "questions/edit",
  deleteQuestion: "questions/delete",
  viewQuestion: "questions/view",
};
