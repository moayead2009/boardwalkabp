import React from 'react'
import useStateContext from "../../../hooks/useStateContext";
import { createAPIEndpoint, ENDPOINTS } from "../../../api";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { Card, CardContent, Grid, TextField, Button, Typography } from "@mui/material";
import { Box } from "@mui/system";

export default function AddClient() {
  const { context } = useStateContext();
  // console.log(context)

  const [clients, setClients] = useState([]);
  const [loading, setLoading] = useState(false);
  const [search, setSearch] = useState("");
  const [searchKeyword, setSearchKeyword] = useState("");
  const [searchResults, setSearchResults] = useState([]);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [total, setTotal] = useState(0);
  const [error, setError] = useState(null);
  const [selected, setSelected] = useState([]);
  const [selectedRow, setSelectedRow] = useState(null);
  const [errors, setErrors] = useState({});
  const navigate = useNavigate();
  const { id } = useParams();
  const [values, setValues] = useState({
    name: "",
    username: "",
    email: "",
    password: "",
    phoneNumber: "",
    address: "",
    applicationId: "",
  });

  const validate = () => {
    let temp = {};
    temp.name = values.name ? "" : "This field is required.";
    temp.username = values.username ? "" : "This field is required.";
    temp.email = values.email ? "" : "This field is required.";
    temp.password = values.password ? "" : "This field is required.";
    temp.phoneNumber = values.phoneNumber ? "" : "This field is required.";
    temp.address = values.address ? "" : "This field is required.";
    temp.applicationId = values.applicationId ? "" : "This field is required.";
    setErrors({
      ...temp,
    });
    return Object.values(temp).every((x) => x === "");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (validate()) {
      createAPIEndpoint(ENDPOINTS.clients)
        .post(values)
        .then((res) => {
          console.log(res);
          navigate("/builder/clients");
        })
        .catch((err) => console.log(err));
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setValues({
      ...values,
      [name]: value,
    });
  };

  return (
    <div>
      <br></br>
      <Card>
        <CardContent>
          <Typography variant="h6" gutterBottom>
            Add Client
          </Typography>
          <form autoComplete="off" noValidate onSubmit={handleSubmit}>
            <Grid container spacing={3}>
              <Grid item xs={12} sm={6}>
                <TextField
                  name="name"
                  label="Name"
                  value={values.name}
                  onChange={handleInputChange}
                  {...(errors.name && { error: true, helperText: errors.name })}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  name="username"
                  label="Username"
                  value={values.username}
                  onChange={handleInputChange}
                  {...(errors.username && { error: true, helperText: errors.username })}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  name="email"
                  label="Email"
                  type="email"
                  value={values.email}
                  onChange={handleInputChange}
                  {...(errors.email && { error: true, helperText: errors.email })}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  name="password"
                  label="Password"
                  type="password"
                  value={values.password}
                  onChange={handleInputChange}
                  {...(errors.password && { error: true, helperText: errors.password })}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  name="phoneNumber"
                  label="Phone Number"
                  value={values.phoneNumber}
                  onChange={handleInputChange}
                  {...(errors.phoneNumber && { error: true, helperText: errors.phoneNumber })}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  name="address"
                  label="Address"
                  value={values.address}
                  onChange={handleInputChange}
                  {...(errors.address && { error: true, helperText: errors.address })}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  name="applicationId"
                  label="Application Id"
                  value={values.applicationId}
                  onChange={handleInputChange}
                  {...(errors.applicationId && { error: true, helperText: errors.applicationId })}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <Button
                  variant="contained"
                  color="primary"
                  type="submit"
                  style={{ marginTop: "10px" }}
                >
                  Submit
                </Button>
              </Grid>
            </Grid>
          </form>
        </CardContent>
      </Card>
    </div>
  );
}
