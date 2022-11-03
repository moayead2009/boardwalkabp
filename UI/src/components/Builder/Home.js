import React from 'react'
import useStateContext from "../../hooks/useStateContext";
import { Card, CardContent, Typography } from "@mui/material";

export default function Home() {
  const { context } = useStateContext();
  // console.log(context);

  return (
    <div>
      <br></br>
      <Card>
        <CardContent>
          <Typography variant="h5" component="div">
            Welcome back, {context.name}!
          </Typography>
        </CardContent>
      </Card>
    </div>
  );
}