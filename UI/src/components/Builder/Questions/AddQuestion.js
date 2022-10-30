import React from 'react'
import useStateContext from "../../../hooks/useStateContext";
import { createAPIEndpoint, ENDPOINTS } from "../../../api";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { Card, CardContent, Grid, TextField, Button, Typography } from "@mui/material";
import { Box } from "@mui/system";

export default function AddQuestion() {
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
    body: "",
    type: "",
    category: "",
    conditionId: "",
    options: [],
  });

  const validate = () => {
    let temp = {};
    temp.body = values.body ? "" : "This field is required.";
    temp.type = values.type ? "" : "This field is required.";
    temp.category = values.category ? "" : "This field is required.";
    temp.conditionId = values.conditionId ? "" : "This field is required.";
    setErrors({
      ...temp,
    });
    return Object.values(temp).every((x) => x === "");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (validate()) {
      createAPIEndpoint(ENDPOINTS.questions)
        .post(values)
        .then((res) => {
          console.log(res);
          navigate("/builder/questions");
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

  const handleOptionChange = (e) => {
    const { name, value } = e.target;
    setValues({
      ...values,
      options: [
        ...values.options,
        {
          [name]: value,
        },
      ],
    });
  };

  return (
    <div>
      <br></br>
      <Card>
        <CardContent>
          <Typography variant="h6" gutterBottom>
            Add Question
          </Typography>
          <form onSubmit={handleSubmit}>
            <Grid container spacing={3}>
              <Grid item xs={12}>
                <TextField
                  name="body"
                  label="Body"
                  value={values.body}
                  onChange={handleInputChange}
                  error={errors.body}
                  helperText={errors.body}
                  fullWidth
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  name="type"
                  label="Type"
                  value={values.type}
                  onChange={handleInputChange}
                  error={errors.type}
                  helperText={errors.type}
                  fullWidth
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  name="category"
                  label="Category"
                  value={values.category}
                  onChange={handleInputChange}
                  error={errors.category}
                  helperText={errors.category}
                  fullWidth
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  name="conditionId"
                  label="Condition"
                  value={values.conditionId}
                  onChange={handleInputChange}
                  error={errors.conditionId}
                  helperText={errors.conditionId}
                  fullWidth
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  name="option"
                  label="Options"
                  value={values.option}
                  onChange={handleOptionChange}
                  error={errors.option}
                  helperText={errors.option}
                  fullWidth
                />
              </Grid>
              <Grid item xs={12}>
                <Button type="submit" variant="contained">
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