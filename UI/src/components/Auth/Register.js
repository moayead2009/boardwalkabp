import React from "react";
import { Button, Card, CardContent, TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import Center from "../Builder/layout/Center";
import useForm from "../../hooks/useForm";
import { createAPIEndpoint, ENDPOINTS } from '../../api'

const getFreshModel = () => ({
    name: "",
    username: "",
    email: "",
    password: "",
    role: "",
  });

export default function Register() {
    const { values, errors, setErrors, handleInputChange } =
    useForm(getFreshModel);

  const register = (e) => {
    e.preventDefault();
    // console.log(values);
    if (validate())
        createAPIEndpoint(ENDPOINTS.registerUser)
        .post(values)
        .then(res => console.log(res))
        .catch(err => console.log(err))
  };

  const validate = () => {
    let temp = {};
    temp.name = values.name !== "" ? "" : "This field is required.";
    temp.username = values.username !== "" ? "" : "This field is required.";
    temp.email = (/\S+@\S+\.\S+/).test(values.email) ? "" : "Email is not valid."
    temp.password = values.password !== "" ? "" : "This field is required.";
    setErrors(temp);
    return Object.values(temp).every((x) => x === "");
  };

  return (
    <Center>
      <Card sx={{ width: 400 }}>
        <CardContent sx={{ textAlign: "center" }}>
          <Typography variant="h3" sx={{ my: 3 }}>
            Application Building Platform
          </Typography>
          <Box sx={{ "& .MuiTextField-root": { m: 1, width: "90%" } }}>
            <form noValidate autoComplete="on" onSubmit={register}>
            <TextField
                label="Name"
                name="name"
                value={values.name}
                onChange={handleInputChange}
                variant="outlined"
                {...(errors.name && { error: true, helperText: errors.name })}
              ></TextField>
              <TextField
                label="Username"
                name="username"
                value={values.username}
                onChange={handleInputChange}
                variant="outlined"
                {...(errors.username && { error: true, helperText: errors.username })}
              ></TextField>
              <TextField
                label="Email"
                name="email"
                value={values.email}
                onChange={handleInputChange}
                variant="outlined"
                {...(errors.email && { error: true, helperText: errors.email })}
              ></TextField>
              <TextField
                label="Password"
                name="password"
                type="password"
                value={values.password}
                onChange={handleInputChange}
                variant="outlined"
                {...(errors.password && { error: true, helperText: errors.password })}
              ></TextField>
              <Button
                type="submit"
                variant="contained"
                size="large"
                sx={{ width: "90%" }}
              >
                Register
              </Button>
            </form>
          </Box>
        </CardContent>
      </Card>
    </Center>
  )
}
