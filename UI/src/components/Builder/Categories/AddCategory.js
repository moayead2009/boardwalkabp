import React from 'react'
import useStateContext from "../../../hooks/useStateContext";
import { createAPIEndpoint, ENDPOINTS } from "../../../api";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { Card, CardContent, Grid, TextField, Button, Typography } from "@mui/material";
import { Box } from "@mui/system";

export default function AddCategory() {
  const { context } = useStateContext();
  // console.log(context);

  const [category, setCategory] = useState("");
  const [error, setError] = useState(null);
  const navigate = useNavigate();
  const { id } = useParams();
  const [values, setValues] = useState({
    name: "",
    applicationId: "",
    questionId: "",
    // questions: [],
  });
  const [errors, setErrors] = useState({});

  const validate = () => {
    let temp = {};
    temp.name = values.name ? "" : "This field is required.";
    temp.applicationId = values.applicationId ? "" : "This field is required.";
    temp.questionId = values.questionId ? "" : "This field is required.";
    setErrors({
      ...temp,
    });
    return Object.values(temp).every((x) => x === "");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (validate()) {
      createAPIEndpoint(ENDPOINTS.categories)
        .post(values)
        .then((res) => {
          navigate("/builder/categories");
        })
        .catch((err) => console.log(err));
    }
  };

  useEffect(() => {
    if (id) {
      createAPIEndpoint(ENDPOINTS.categories)
        .fetchById(id)
        .then((res) => {
          setValues(res.data);
        })
        .catch((err) => console.log(err));
    }
  }, [id]);

  return (
    <div>
      <br></br>
      <Card>
      <CardContent>
        <Typography variant="h6" component="div">
          {id ? "Edit Category" : "Add Category"}
        </Typography>
        <Box sx={{ mt: 3 }}>
          <form autoComplete="off" noValidate onSubmit={handleSubmit}>
            <Grid container spacing={3}>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="Name"
                  name="name"
                  onChange={(e) => {
                    setValues({
                      ...values,
                      name: e.target.value,
                    });
                  }}
                  required
                  value={values.name}
                  variant="outlined"
                />
                {errors.name && (
                  <div style={{ color: "red" }}>{errors.name}</div>
                )}
              </Grid>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="Application ID"
                  name="applicationId"
                  onChange={(e) => {
                    setValues({
                      ...values,
                      applicationId: e.target.value,
                    });
                  }}
                  required
                  value={values.applicationId}
                  variant="outlined"
                />
                {errors.applicationId && (
                  <div style={{ color: "red" }}>{errors.applicationId}</div>
                )}
              </Grid>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="Question ID"
                  name="questionId"
                  onChange={(e) => {
                    setValues({
                      ...values,
                      questionId: e.target.value,
                    });
                  }}
                  required
                  value={values.questionId}
                  variant="outlined"
                />
                {errors.questionId && (
                  <div style={{ color: "red" }}>{errors.questionId}</div>
                )}
              </Grid>
              <Grid item xs={12}>
                <Button
                  color="primary"
                  variant="contained"
                  type="submit"
                  fullWidth
                >
                  {id ? "Update" : "Save"}
                </Button>
              </Grid>
            </Grid>
          </form>
        </Box>
      </CardContent>
    </Card>
    </div>
  );
}