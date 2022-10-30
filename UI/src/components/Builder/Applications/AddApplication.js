import React from "react";
import useStateContext from "../../../hooks/useStateContext";
import { createAPIEndpoint, ENDPOINTS } from "../../../api";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import {
  Card,
  CardContent,
  Grid,
  TextField,
  Button,
  Typography,
} from "@mui/material";
import { Box } from "@mui/system";

export default function AddApplication() {
  const { context } = useStateContext();
  const [values, setValues] = useState({
    title: "",
    slug: "",
    conditionId: "",
    QuestionId: "",
    // category: "",
    // questions: [],
  });

  const [errors, setErrors] = useState({});
  const navigate = useNavigate();
  const { id } = useParams();

  const validate = () => {
    let temp = {};
    temp.title = values.title ? "" : "This field is required.";
    temp.slug = values.slug ? "" : "This field is required.";
    temp.conditionId = values.conditionId ? "" : "This field is required.";
    temp.QuestionId = values.QuestionId ? "" : "This field is required.";
    // temp.category = values.category ? "" : "This field is required.";
    setErrors({
      ...temp,
    });
    return Object.values(temp).every((x) => x === "");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (validate()) {
      if (id) {
        createAPIEndpoint(ENDPOINTS.applications)
          .put(id, values)
          .then((res) => {
            navigate("/applications");
          })
          .catch((err) => console.log(err));
      } else {
        createAPIEndpoint(ENDPOINTS.applications)
          .post(values)
          .then((res) => {
            navigate("/builder/applications");
          })
          .catch((err) => console.log(err));
      }
    }
  };

  useEffect(() => {
    if (id) {
      createAPIEndpoint(ENDPOINTS.applications)
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
            {id ? "Edit Application" : "Add Application"}
          </Typography>
          <br></br>
          <form autoComplete="off" noValidate onSubmit={handleSubmit}>
            <Grid container spacing={3}>
              <Grid item xs={6}>
                <TextField
                  fullWidth
                  name="title"
                  label="Title"
                  value={values.title}
                  onChange={(e) =>
                    setValues({
                      ...values,
                      title: e.target.value,
                    })
                  }
                  {...(errors.title && {
                    error: true,
                    helperText: errors.title,
                  })}
                />
              </Grid>
              <Grid item xs={6}>
                <TextField
                  fullWidth
                  name="slug"
                  label="Slug"
                  value={values.slug}
                  onChange={(e) =>
                    setValues({
                      ...values,
                      slug: e.target.value,
                    })
                  }
                  {...(errors.slug && {
                    error: true,
                    helperText: errors.slug,
                  })}
                />
              </Grid>
              <Grid item xs={6}>
                <TextField
                  fullWidth
                  name="conditionId"
                  label="Condition ID"
                  value={values.conditionId}
                  onChange={(e) =>
                    setValues({
                      ...values,
                      conditionId: e.target.value,
                    })
                  }
                  {...(errors.conditionId && {
                    error: true,
                    helperText: errors.conditionId,
                  })}
                />
              </Grid>
              <Grid item xs={6}>
                <TextField
                  fullWidth
                  name="QuestionId"
                  label="Question ID"
                  value={values.QuestionId}
                  onChange={(e) =>
                    setValues({
                      ...values,
                      QuestionId: e.target.value,
                    })
                  }
                  {...(errors.QuestionId && {
                    error: true,
                    helperText: errors.QuestionId,
                  })}
                />
              </Grid>
              <Grid item xs={12}>
                <Button
                  fullWidth
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
