import React from "react";
import { Button, Card, CardContent, TextField, Typography, } from "@mui/material";
import { Box } from "@mui/system";
import Center from "../Builder/layout/Center";
import useForm from "../../hooks/useForm";
import { createAPIEndpoint, ENDPOINTS } from "../../api";
import useStateContext from "../../hooks/useStateContext";
import { useNavigate } from "react-router";

const getFreshModel = () => ({
  username: "",
  password: "",
});

export default function Login() {
  const { setContext } = useStateContext();
  const { values, errors, setErrors, handleInputChange } =
    useForm(getFreshModel);
  const navigate = useNavigate();

  const validate = () => {
    let temp = {};
    temp.username = values.username !== "" ? "" : "This field is required.";
    temp.password = values.password !== "" ? "" : "This field is required.";
    setErrors(temp);
    return Object.values(temp).every((x) => x === "");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (validate()) {
      try {
        createAPIEndpoint(ENDPOINTS.login)
          .post(values)
          .then((res) => {
            setContext(res.data);
            if (res.data.username !== null) {
              if (res.data.role === "client") {
                navigate("/viewer/home");
              } else {
                navigate("/builder/home");
              }
            }
            if (res.data.username === null) {
              setErrors({
                ...errors,
                username: res.data.message,
                password: res.data.message,
              });
              navigate("/");
            }
          })
          .catch((err) => console.log(err));
      } catch (err) {
        console.log(err);
      }
    }
  };

  return (
    <Center>
      {/* {context.username} */}
      <Card sx={{ width: 400 }}>
        <CardContent sx={{ textAlign: "center" }}>
          <Typography variant="h3" sx={{ my: 3 }}>
            Application Building Platform
          </Typography>
          <Box sx={{ "& .MuiTextField-root": { m: 1, width: "90%" } }}>
            <form noValidate autoComplete="off" onSubmit={handleSubmit}>
              <TextField
                label="Username"
                name="username"
                value={values.username}
                onChange={handleInputChange}
                variant="outlined"
                {...(errors.username && {
                  error: true,
                  helperText: errors.username,
                })}
              ></TextField>
              <TextField
                label="Password"
                name="password"
                type="password"
                value={values.password}
                onChange={handleInputChange}
                variant="outlined"
                {...(errors.password && {
                  error: true,
                  helperText: errors.password,
                })}
              ></TextField>
              <Button
                type="submit"
                variant="contained"
                size="large"
                sx={{ width: "90%" }}
              >
                Login
              </Button>
            </form>
          </Box>
        </CardContent>
      </Card>
    </Center>
  );
}
