import React from 'react'
import useStateContext from "../../../hooks/useStateContext";
import { createAPIEndpoint, ENDPOINTS } from "../../../api";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { Card, CardContent, Grid, TextField, Button, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useParams } from "react-router-dom";

export default function AddQuestion() {
  const [values, setValues] = useState({
    body: "",
    type: "",
  });
  
  const [errors, setErrors] = useState({});
  const navigate = useNavigate();
  const { id } = useParams();
  const { state } = useStateContext();

  const validate = () => {
    let temp = {};
    temp.body = values.body ? "" : "This field is required.";
    temp.type = values.type ? "" : "This field is required.";
    setErrors({
      ...temp,
    });
    return Object.values(temp).every((x) => x === "");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (validate()) {
      if (id) {
        createAPIEndpoint(ENDPOINTS.questions)
          .put(id, values)
          .then((res) => {
            navigate("/builder/questions");
          })
          .catch((err) => console.log(err));
      } else {
        createAPIEndpoint(ENDPOINTS.questions)
          .post(values)
          .then((res) => {
            navigate("/builder/questions");
          })
          .catch((err) => console.log(err));
      }
    }
  };

  useEffect(() => {
    if (id) {
      createAPIEndpoint(ENDPOINTS.questions)
        .fetchById(id)
        .then((res) => {
          setValues(res.data);
        })
        .catch((err) => console.log(err));
    }
  }, [id]);

  return (
    <Card>
      <CardContent>
        <Typography variant="h6" component="div">
          {id ? "Edit Question" : "Add Question"}
        </Typography>
        <Box sx={{ mt: 3 }}>
          <form autoComplete="off" noValidate onSubmit={handleSubmit}>
            <Grid container spacing={3}>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="Body"
                  name="body"
                  onChange={(e) =>
                    setValues({
                      ...values,
                      body: e.target.value,
                    })
                  }{...(errors.body && {
                    error: true,
                    helperText: errors.body,
                  })}
                  required
                  value={values.body}
                  variant="outlined"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="Type"
                  name="type"
                  onChange={(e) =>
                    setValues({
                      ...values,
                      type: e.target.value,
                    })
                  }{...(errors.type && {
                    error: true,
                    helperText: errors.type,
                  })}
                  required
                  value={values.type}
                  variant="outlined"
                />
              </Grid>
              <Grid item xs={12}>
                <Button
                  color="primary"
                  fullWidth
                  size="large"
                  type="submit"
                  variant="contained"
                >
                  {id ? "Update" : "Save"}
                </Button>
              </Grid>
            </Grid>
          </form>
        </Box>
      </CardContent>
    </Card>
  );
}